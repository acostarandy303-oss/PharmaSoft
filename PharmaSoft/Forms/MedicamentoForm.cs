using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Diagnostics;

namespace PharmaSoft.Forms;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public partial class MedicamentoForm : Form
{
    private readonly PharmaContext _context;
    private readonly CategoriaService _categoriaService;
    private readonly ProveedoreService _proveedorService;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;

    public Medicamento Medicamento { get; private set; }
    public LotesInventario? Lote { get; private set; }

    public MedicamentoForm(Medicamento? medicamento = null)
    {
        InitializeComponent();
        _context = new PharmaContext();
        _categoriaService = new CategoriaService(_context);
        _proveedorService = new ProveedoreService(_context);
        _medicamentoService = new MedicamentoService(_context);
        _lotesService = new LotesInventarioService(_context);

        Medicamento = medicamento ?? new Medicamento();
        if (medicamento != null)
        {
            CargarDatos(medicamento);
        }
    }

    private async void MedicamentoForm_Load(object? sender, EventArgs e)
    {
        await CargarCombos();

        await CargarInventario();

        if (Medicamento != null && Medicamento.MedicamentoId > 0)
        {
            var lotes = await _lotesService.GetList(l => l.MedicamentoId == Medicamento.MedicamentoId);
            Lote = lotes.FirstOrDefault();
            if (Lote != null)
            {
                txtNumeroLote.Text = Lote.NumeroLote;
                nudCantidadLote.Value = Lote.CantidadActual;
                
                DateTime fechaVenc = Lote.FechaVencimiento.ToDateTime(TimeOnly.MinValue);
                if (fechaVenc < dtpFechaVencimiento.MinDate)
                {
                    dtpFechaVencimiento.MinDate = fechaVenc;
                }
                dtpFechaVencimiento.Value = fechaVenc;
            }
        }
    }

    private async Task CargarInventario()
    {
        var medicamentos = await _medicamentoService.GetList(m => m.Activo);
        var lotes = await _lotesService.GetList(l => true);

        var inventario = medicamentos.Select(m => new
        {
            m.MedicamentoId,
            m.CodigoBarras,
            m.Nombre,
            m.Laboratorio,
            Cantidad = lotes.Where(l => l.MedicamentoId == m.MedicamentoId).Sum(l => l.CantidadActual),
            m.PrecioVenta
        }).ToList();

        dgvMedicamentos.DataSource = inventario;
        dgvMedicamentos.Columns["MedicamentoId"].Visible = false;
        dgvMedicamentos.Columns["CodigoBarras"].HeaderText = "Código";
        dgvMedicamentos.Columns["Nombre"].HeaderText = "Nombre";
        dgvMedicamentos.Columns["Laboratorio"].HeaderText = "Laboratorio";
        dgvMedicamentos.Columns["Cantidad"].HeaderText = "Cantidad";
        dgvMedicamentos.Columns["PrecioVenta"].HeaderText = "Precio";
    }

    private async Task CargarCombos()
    {
        var categorias = await _categoriaService.GetList(c => true);
        var proveedores = await _proveedorService.GetList(p => true);

        cmbCategoria.DataSource = categorias;
        cmbCategoria.DisplayMember = "Nombre";
        cmbCategoria.ValueMember = "CategoriaId";

        cmbProveedor.DataSource = proveedores;
        cmbProveedor.DisplayMember = "NombreEmpresa";
        cmbProveedor.ValueMember = "ProveedorId";
    }

    private void CargarDatos(Medicamento m)
    {
        txtCodigoBarras.Text = m.CodigoBarras;
        txtNombre.Text = m.Nombre;
        txtDescripcion.Text = m.Descripcion;
        txtLaboratorio.Text = m.Laboratorio;
        nudPrecioCompra.Value = m.PrecioCompra;
        nudPrecioVenta.Value = m.PrecioVenta;
        nudStockMinimo.Value = m.StockMinimo ?? 10;
        cmbCategoria.SelectedValue = m.CategoriaId;
        cmbProveedor.SelectedValue = m.ProveedorId;
        
    }

    private async void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(cmbCategoria.Text))
        {
            MessageBox.Show("La categoría es obligatoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(cmbProveedor.Text))
        {
            MessageBox.Show("El proveedor es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string nombreCategoria = cmbCategoria.Text.Trim();
        int categoriaId = 0;

        var categoriaExistente = await _categoriaService.GetList(c => c.Nombre.ToLower() == nombreCategoria.ToLower());
        if (categoriaExistente.Any())
        {
            categoriaId = categoriaExistente.First().CategoriaId;
        }
        else
        {
            var nuevaCategoria = new Categoria { Nombre = nombreCategoria };
            await _categoriaService.Guardar(nuevaCategoria);
            var categoriasActualizadas = await _categoriaService.GetList(c => c.Nombre.ToLower() == nombreCategoria.ToLower());
            categoriaId = categoriasActualizadas.First().CategoriaId;
        }

        string nombreProveedor = cmbProveedor.Text.Trim();
        int proveedorId = 0;

        var proveedorExistente = await _proveedorService.GetList(p => p.NombreEmpresa.ToLower() == nombreProveedor.ToLower());
        if (proveedorExistente.Any())
        {
            proveedorId = proveedorExistente.First().ProveedorId;
        }
        else
        {
            var nuevoProveedor = new Proveedore { NombreEmpresa = nombreProveedor };
            await _proveedorService.Guardar(nuevoProveedor);
            var proveedoresActualizados = await _proveedorService.GetList(p => p.NombreEmpresa.ToLower() == nombreProveedor.ToLower());
            proveedorId = proveedoresActualizados.First().ProveedorId;
        }

        Medicamento.CodigoBarras = txtCodigoBarras.Text.Trim();
        Medicamento.Nombre = txtNombre.Text.Trim();
        Medicamento.Laboratorio = txtLaboratorio.Text.Trim();
        Medicamento.PrecioCompra = nudPrecioCompra.Value;
        Medicamento.PrecioVenta = nudPrecioVenta.Value;
        Medicamento.Descripcion = txtDescripcion.Text.Trim();
        Medicamento.StockMinimo = (int)nudStockMinimo.Value;
        Medicamento.CategoriaId = categoriaId;
        Medicamento.ProveedorId = proveedorId;

        if (Medicamento.MedicamentoId == 0)
        {
            if (nudCantidadLote.Value > 0)
            {
                Lote = new LotesInventario
                {
                    NumeroLote = txtNumeroLote.Text.Trim(),
                    CantidadActual = (int)nudCantidadLote.Value,
                    FechaVencimiento = DateOnly.FromDateTime(dtpFechaVencimiento.Value),
                    FechaIngreso = DateTime.Now
                };
            }
        }
        else
        {
            if (Lote != null)
            {
                Lote.NumeroLote = txtNumeroLote.Text.Trim();
                Lote.CantidadActual = (int)nudCantidadLote.Value;
                Lote.FechaVencimiento = DateOnly.FromDateTime(dtpFechaVencimiento.Value);
            }
            else if (nudCantidadLote.Value > 0)
            {
                Lote = new LotesInventario
                {
                    MedicamentoId = Medicamento.MedicamentoId,
                    NumeroLote = txtNumeroLote.Text.Trim(),
                    CantidadActual = (int)nudCantidadLote.Value,
                    FechaVencimiento = DateOnly.FromDateTime(dtpFechaVencimiento.Value),
                    FechaIngreso = DateTime.Now
                };
            }
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _context.Dispose();
        base.OnFormClosing(e);
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
