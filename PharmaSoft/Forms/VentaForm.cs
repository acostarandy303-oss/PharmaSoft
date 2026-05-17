using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Data;

namespace PharmaSoft.Forms;

public partial class VentaForm : Form
{
    private readonly PharmaContext _context;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;
    private readonly VentaService _ventaService;
    private readonly DetalleVentaService _detalleVentaService;

    private DataTable dtCarrito;

    public VentaForm()
    {
        InitializeComponent();
        _context = new PharmaContext();
        _medicamentoService = new MedicamentoService(_context);
        _lotesService = new LotesInventarioService(_context);
        _ventaService = new VentaService(_context);
        _detalleVentaService = new DetalleVentaService(_context);

        dtCarrito = new DataTable();
        dtCarrito.Columns.Add("MedicamentoId", typeof(int));
        dtCarrito.Columns.Add("LoteId", typeof(int));
        dtCarrito.Columns.Add("Nombre", typeof(string));
        dtCarrito.Columns.Add("Precio", typeof(decimal));
        dtCarrito.Columns.Add("Cantidad", typeof(int));
        dtCarrito.Columns.Add("Subtotal", typeof(decimal));
        dgvCarrito.DataSource = dtCarrito;

        cmbMetodoPago.Items.AddRange(new[] { "Efectivo", "Tarjeta", "Transferencia" });
        cmbMetodoPago.SelectedIndex = 0;
    }

    private async void VentaForm_Load(object? sender, EventArgs e)
    {
        await CargarProductos();
    }

    private async Task CargarProductos(string? criterio = null)
    {
        var medicamentos = await _medicamentoService.GetList(m => m.Activo);
        var lotes = await _lotesService.GetList(l => l.CantidadActual > 0);

        var productos = from m in medicamentos
                        join l in lotes on m.MedicamentoId equals l.MedicamentoId
                        where string.IsNullOrEmpty(criterio) ||
                              m.Nombre.Contains(criterio) ||
                              (m.CodigoBarras != null && m.CodigoBarras.Contains(criterio))
                        select new
                        {
                            l.LoteId,
                            m.MedicamentoId,
                            m.Nombre,
                            m.CodigoBarras,
                            l.CantidadActual,
                            m.PrecioVenta,
                            l.FechaVencimiento
                        };

        dgvProductos.DataSource = productos.ToList();
        ConfigurarColumnasProductos();
    }

    private void ConfigurarColumnasProductos()
    {
        if (dgvProductos.Columns["LoteId"] != null) dgvProductos.Columns["LoteId"].Visible = false;
        if (dgvProductos.Columns["MedicamentoId"] != null) dgvProductos.Columns["MedicamentoId"].Visible = false;
        if (dgvProductos.Columns["Nombre"] != null) dgvProductos.Columns["Nombre"].HeaderText = "Producto";
        if (dgvProductos.Columns["CodigoBarras"] != null) dgvProductos.Columns["CodigoBarras"].HeaderText = "Código";
        if (dgvProductos.Columns["CantidadActual"] != null) dgvProductos.Columns["CantidadActual"].HeaderText = "Stock";
        if (dgvProductos.Columns["PrecioVenta"] != null) dgvProductos.Columns["PrecioVenta"].HeaderText = "Precio";
        if (dgvProductos.Columns["FechaVencimiento"] != null) dgvProductos.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
    }

    private async void txtBuscar_TextChanged(object? sender, EventArgs e)
    {
        await CargarProductos(txtBuscar.Text.Trim());
    }

    private void btnAgregar_Click(object sender, EventArgs e)
    {
        if (dgvProductos.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var fila = dgvProductos.SelectedRows[0];
        int loteId = Convert.ToInt32(fila.Cells["LoteId"].Value);
        string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
        decimal precio = Convert.ToDecimal(fila.Cells["PrecioVenta"].Value);
        int stock = Convert.ToInt32(fila.Cells["CantidadActual"].Value);

        var existente = dtCarrito.AsEnumerable()
            .FirstOrDefault(r => r.Field<int>("LoteId") == loteId);

        if (existente != null)
        {
            int cantidadActual = existente.Field<int>("Cantidad");
            if (cantidadActual < stock)
            {
                existente["Cantidad"] = cantidadActual + 1;
                existente["Subtotal"] = (cantidadActual + 1) * precio;
            }
            else
            {
                MessageBox.Show("Stock insuficiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        else
        {
            var row = dtCarrito.NewRow();
            row["LoteId"] = loteId;
            row["MedicamentoId"] = Convert.ToInt32(fila.Cells["MedicamentoId"].Value);
            row["Nombre"] = nombre;
            row["Precio"] = precio;
            row["Cantidad"] = 1;
            row["Subtotal"] = precio;
            dtCarrito.Rows.Add(row);
        }

        dgvCarrito.DataSource = dtCarrito;
        CalcularTotal();
    }

    private void btnQuitar_Click(object sender, EventArgs e)
    {
        if (dgvCarrito.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un producto en el carrito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int index = dgvCarrito.SelectedRows[0].Index;
        dtCarrito.Rows[index].Delete();
        CalcularTotal();
    }

    private void CalcularTotal()
    {
        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        lblMontoTotal.Text = $"RD$ {total:N2}";
        lblSubtotalValue.Text = $"RD$ {total:N2}";
    }

    private void nudPagoCon_ValueChanged(object? sender, EventArgs e)
    {
        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        decimal pago = nudPagoCon.Value;
        decimal cambio = pago - total;

        if (cambio >= 0)
        {
            lblMontoCambio.Text = $"RD$ {cambio:N2}";
            lblMontoCambio.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
        }
        else
        {
            lblMontoCambio.Text = $"RD$ 0.00";
            lblMontoCambio.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
        }
    }

    private async void btnFinalizarVenta_Click(object sender, EventArgs e)
    {
        if (dtCarrito.Rows.Count == 0)
        {
            MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        decimal pago = nudPagoCon.Value;

        if (pago < total)
        {
            MessageBox.Show("El monto de pago es insuficiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                Total = total,
                MetodoPago = cmbMetodoPago.SelectedItem?.ToString()
            };

            var resultadoVenta = await _ventaService.Guardar(venta);
            if (!resultadoVenta)
            {
                MessageBox.Show("Error al guardar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataRow row in dtCarrito.Rows)
            {
                int loteId = Convert.ToInt32(row["LoteId"]);
                int cantidad = Convert.ToInt32(row["Cantidad"]);
                decimal precio = Convert.ToDecimal(row["Precio"]);
                decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                var detalleVenta = new DetalleVenta
                {
                    VentaId = venta.VentaId,
                    LoteId = loteId,
                    Cantidad = cantidad,
                    PrecioUnitario = precio,
                    Subtotal = subtotal
                };

                await _detalleVentaService.Guardar(detalleVenta);

                var lote = await _lotesService.Buscar(loteId);
                if (lote != null)
                {
                    lote.CantidadActual -= cantidad;
                    await _lotesService.Guardar(lote);
                }
            }

            decimal cambio = pago - total;
            MessageBox.Show($"Venta realizada exitosamente!\n\nTotal: RD$ {total:N2}\nPago: RD$ {pago:N2}\nCambio: RD$ {cambio:N2}",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al procesar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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