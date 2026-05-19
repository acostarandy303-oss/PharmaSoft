using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Data;

namespace PharmaSoft.Forms;

public partial class CompraForm : Form
{
    private readonly PharmaContext _context;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;
    private readonly CompraService _compraService;
    private readonly DetalleCompraService _detalleCompraService;
    private readonly ProveedoreService _proveedorService;
    private readonly DataTable dtCarrito;

    public CompraForm(
        PharmaContext context,
        MedicamentoService medicamentoService,
        LotesInventarioService lotesService,
        CompraService compraService,
        DetalleCompraService detalleCompraService,
        ProveedoreService proveedorService)
    {
        InitializeComponent();
        _context = context;
        _medicamentoService = medicamentoService;
        _lotesService = lotesService;
        _compraService = compraService;
        _detalleCompraService = detalleCompraService;
        _proveedorService = proveedorService;

        dtCarrito = new DataTable();
        dtCarrito.Columns.Add("MedicamentoId", typeof(int));
        dtCarrito.Columns.Add("Nombre", typeof(string));
        dtCarrito.Columns.Add("PrecioCompra", typeof(decimal));
        dtCarrito.Columns.Add("Cantidad", typeof(int));
        dtCarrito.Columns.Add("Subtotal", typeof(decimal));
    }

    private async void CompraForm_Load(object sender, EventArgs e)
    {
        await CargarCombos();
        InicializarDataGridView();
    }

    private async Task CargarCombos()
    {
        var proveedores = await _proveedorService.GetList(p => true);
        cmbProveedor.DataSource = proveedores;
        cmbProveedor.DisplayMember = "Nombre";
        cmbProveedor.ValueMember = "ProveedorId";
        cmbProveedor.SelectedIndex = -1;

        cmbTipoPago.Items.Add("Contado");
        cmbTipoPago.Items.Add("Crédito");
        cmbTipoPago.SelectedIndex = 0;
    }

    private void InicializarDataGridView()
    {
        dgvCarrito.DataSource = dtCarrito;
        dgvCarrito.Columns["MedicamentoId"].Visible = false;
        dgvCarrito.Columns["Nombre"].HeaderText = "Medicamento";
        dgvCarrito.Columns["PrecioCompra"].HeaderText = "Precio Compra";
        dgvCarrito.Columns["Cantidad"].HeaderText = "Cantidad";
        dgvCarrito.Columns["Subtotal"].HeaderText = "Subtotal";

        foreach (DataGridViewColumn col in dgvCarrito.Columns)
        {
            if (col.Name != "Cantidad")
                col.ReadOnly = true;
        }

        dgvCarrito.CellValueChanged += dgvCarrito_CellValueChanged;
    }

    private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            await BuscarMedicamentos();
        }
    }

    private async Task BuscarMedicamentos()
    {
        string criterio = txtBuscar.Text.Trim();
        if (string.IsNullOrEmpty(criterio)) return;

        var medicamentos = await _medicamentoService.GetList(m =>
            m.Activo && (m.Nombre.Contains(criterio) ||
            (m.CodigoBarras != null && m.CodigoBarras.Contains(criterio))));

        if (medicamentos.Count == 0)
        {
            MessageBox.Show("No se encontraron medicamentos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (medicamentos.Count == 1)
        {
            AgregarAlCarrito(medicamentos[0]);
            txtBuscar.Clear();
            txtBuscar.Focus();
        }
        else
        {
            var selectionForm = new SelectionForm<Medicamento>(medicamentos, "Nombre", "Seleccionar Medicamento");
            if (selectionForm.ShowDialog() == DialogResult.OK && selectionForm.ItemSelected != null)
            {
                AgregarAlCarrito(selectionForm.ItemSelected);
            }
        }
    }

    private void AgregarAlCarrito(Medicamento medicamento)
    {
        var existente = dtCarrito.AsEnumerable()
            .FirstOrDefault(r => r.Field<int>("MedicamentoId") == medicamento.MedicamentoId);

        if (existente != null)
        {
            int cantidadActual = existente.Field<int>("Cantidad");
            existente["Cantidad"] = cantidadActual + 1;
            existente["Subtotal"] = (cantidadActual + 1) * Convert.ToDecimal(existente["PrecioCompra"]);
        }
        else
        {
            var row = dtCarrito.NewRow();
            row["MedicamentoId"] = medicamento.MedicamentoId;
            row["Nombre"] = medicamento.Nombre;
            row["PrecioCompra"] = medicamento.PrecioCompra;
            row["Cantidad"] = 1;
            row["Subtotal"] = medicamento.PrecioCompra;
            dtCarrito.Rows.Add(row);
        }

        dgvCarrito.DataSource = dtCarrito;
        CalcularTotal();
    }

    private void CalcularTotal()
    {
        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        lblTotal.Text = $"Total: RD$ {total:N2}";
    }

    private void dgvCarrito_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && dgvCarrito.Columns[e.ColumnIndex].Name == "Cantidad")
        {
            var fila = dgvCarrito.Rows[e.RowIndex];
            if (fila.Cells["Cantidad"].Value != null && int.TryParse(fila.Cells["Cantidad"].Value.ToString(), out int nuevaCantidad))
            {
                if (nuevaCantidad > 0)
                {
                    decimal precio = Convert.ToDecimal(fila.Cells["PrecioCompra"].Value);
                    fila.Cells["Subtotal"].Value = nuevaCantidad * precio;
                }
                else
                {
                    dtCarrito.Rows.RemoveAt(e.RowIndex);
                }
                CalcularTotal();
            }
        }
    }

    private void btnQuitar_Click(object sender, EventArgs e)
    {
        if (dgvCarrito.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un producto para quitar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int index = dgvCarrito.SelectedRows[0].Index;
        dtCarrito.Rows[index].Delete();
        dgvCarrito.DataSource = dtCarrito;
        CalcularTotal();
    }

    private async void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (dtCarrito.Rows.Count == 0)
        {
            MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbProveedor.SelectedValue == null)
        {
            MessageBox.Show("Seleccione un proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
        {
            MessageBox.Show("Ingrese el número de factura del proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
            int proveedorId = (int)cmbProveedor.SelectedValue;

            var compra = new Compra
            {
                ProveedorId = proveedorId,
                FechaCompra = DateTime.Now,
                TotalCompra = total,
                TipoPago = cmbTipoPago.SelectedItem?.ToString() ?? "Contado",
                NumeroFacturaProveedor = txtNumeroFactura.Text.Trim()
            };

            var resultado = await _compraService.Guardar(compra);
            if (!resultado)
            {
                MessageBox.Show("Error al guardar la compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataRow row in dtCarrito.Rows)
            {
                int medicamentoId = Convert.ToInt32(row["MedicamentoId"]);
                int cantidad = Convert.ToInt32(row["Cantidad"]);
                decimal precio = Convert.ToDecimal(row["PrecioCompra"]);
                decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                var detalleCompra = new DetalleCompra
                {
                    CompraId = compra.CompraId,
                    MedicamentoId = medicamentoId,
                    Cantidad = cantidad,
                    PrecioCostoUnitario = precio
                };

                await _detalleCompraService.Guardar(detalleCompra);

                var lotesExistentes = await _lotesService.GetList(l => l.MedicamentoId == medicamentoId);
                if (lotesExistentes.Any())
                {
                    var lote = lotesExistentes.First();
                    lote.CantidadActual += cantidad;
                    await _lotesService.Guardar(lote);
                }
                else
                {
                    var nuevoLote = new LotesInventario
                    {
                        MedicamentoId = medicamentoId,
                        CantidadActual = cantidad,
                        NumeroLote = $"LOTE-{DateTime.Now:yyyyMMddHHmmss}",
                        FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
                    };
                    await _lotesService.Guardar(nuevoLote);
                }
            }

            MessageBox.Show($"Compra registrada exitosamente!\n\nTotal: RD$ {total:N2}\nProveedor: {cmbProveedor.Text}",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dtCarrito.Clear();
            dgvCarrito.DataSource = dtCarrito;
            txtNumeroFactura.Clear();
            CalcularTotal();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al registrar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        Close();
    }
}

public class SelectionForm<T> : Form
{
    private DataGridView dgv;
    private List<T> items;
    private string displayMember;
    public T? ItemSelected { get; private set; }

    public SelectionForm(List<T> items, string displayMember, string title)
    {
        this.items = items;
        this.displayMember = displayMember;
        InitializeComponent();
        Text = title;
    }

    private void InitializeComponent()
    {
        dgv = new DataGridView
        {
            Dock = DockStyle.Fill,
            DataSource = items,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false
        };
        dgv.DoubleClick += (s, e) =>
        {
            if (dgv.SelectedRows.Count > 0)
            {
                ItemSelected = (T)dgv.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
                Close();
            }
        };

        var btnAceptar = new Button { Text = "Aceptar", Dock = DockStyle.Bottom, Height = 40 };
        btnAceptar.Click += (s, e) =>
        {
            if (dgv.SelectedRows.Count > 0)
            {
                ItemSelected = (T)dgv.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
            }
            Close();
        };

        var btnCancelar = new Button { Text = "Cancelar", Dock = DockStyle.Bottom, Height = 40 };
        btnCancelar.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(dgv);
        Controls.Add(btnCancelar);
        Controls.Add(btnAceptar);
        Size = new System.Drawing.Size(500, 400);
        StartPosition = FormStartPosition.CenterParent;
    }
}