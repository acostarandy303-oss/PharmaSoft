using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Forms;

public partial class MedicamentoForm : Form
{
    private readonly PharmaContext _context;
    private readonly CategoriaService _categoriaService;
    private readonly ProveedoreService _proveedorService;

    public Medicamento Medicamento { get; private set; }
    public LotesInventario? Lote { get; private set; }

    public MedicamentoForm(Medicamento? medicamento = null)
    {
        InitializeComponent();
        _context = new PharmaContext();
        _categoriaService = new CategoriaService(_context);
        _proveedorService = new ProveedoreService(_context);
        Medicamento = medicamento ?? new Medicamento();
        CargarCombos();
        if (medicamento != null)
        {
            CargarDatos(medicamento);
        }
    }

    private async void CargarCombos()
    {
        var categorias = await _categoriaService.GetList(c => true);
        var proveedores = await _proveedorService.GetList(p => true);

        cmbCategoria.DataSource = categorias;
        cmbCategoria.DisplayMember = "Nombre";
        cmbCategoria.ValueMember = "CategoriaId";

        cmbProveedor.DataSource = proveedores;
        cmbProveedor.DisplayMember = "Nombre";
        cmbProveedor.ValueMember = "ProveedorId";
    }

    private void CargarDatos(Medicamento m)
    {
        txtCodigoBarras.Text = m.CodigoBarras;
        txtNombre.Text = m.Nombre;
        txtPrincipioActivo.Text = m.PrincipioActivo;
        txtPresentacion.Text = m.Presentacion;
        txtLaboratorio.Text = m.Laboratorio;
        txtDosis.Text = m.Dosis;
        nudPrecioCompra.Value = m.PrecioCompra;
        nudPrecioVenta.Value = m.PrecioVenta;
        nudStockMinimo.Value = m.StockMinimo ?? 10;
        chkRequiereReceta.Checked = m.RequiereReceta ?? false;
        cmbCategoria.SelectedValue = m.CategoriaId;
        cmbProveedor.SelectedValue = m.ProveedorId;
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Medicamento.CodigoBarras = txtCodigoBarras.Text.Trim();
        Medicamento.Nombre = txtNombre.Text.Trim();
        Medicamento.PrincipioActivo = txtPrincipioActivo.Text.Trim();
        Medicamento.Presentacion = txtPresentacion.Text.Trim();
        Medicamento.Laboratorio = txtLaboratorio.Text.Trim();
        Medicamento.Dosis = txtDosis.Text.Trim();
        Medicamento.PrecioCompra = nudPrecioCompra.Value;
        Medicamento.PrecioVenta = nudPrecioVenta.Value;
        Medicamento.StockMinimo = (int)nudStockMinimo.Value;
        Medicamento.RequiereReceta = chkRequiereReceta.Checked;
        Medicamento.CategoriaId = cmbCategoria.SelectedValue is int catId ? catId : 0;
        Medicamento.ProveedorId = cmbProveedor.SelectedValue is int provId ? provId : 0;

        if (Medicamento.MedicamentoId == 0 && nudCantidadLote.Value > 0)
        {
            Lote = new LotesInventario
            {
                NumeroLote = txtNumeroLote.Text.Trim(),
                CantidadActual = (int)nudCantidadLote.Value,
                FechaVencimiento = DateOnly.FromDateTime(dtpFechaVencimiento.Value),
                FechaIngreso = DateTime.Now
            };
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
}