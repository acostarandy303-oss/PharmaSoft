using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PharmaSoft.Forms;

public partial class VentaForm : Form
{
    private readonly PharmaContext _context;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;
    private readonly VentaService _ventaService;
    private readonly DetalleVentaService _detalleVentaService;
    private readonly ClienteService _clienteService;
    private readonly CuentasPorCobrarService _cuentasService;

    private DataTable dtCarrito;
    private bool _isUpdatingCantidad = false;
    private List<ProductoBuscadoDTO> _productosTodos = new();

    public VentaForm(
        PharmaContext context,
        MedicamentoService medicamentoService,
        LotesInventarioService lotesService,
        VentaService ventaService,
        DetalleVentaService detalleVentaService,
        ClienteService clienteService,
        CuentasPorCobrarService cuentasService)
    {
        InitializeComponent();
        _context = context;
        _medicamentoService = medicamentoService;
        _lotesService = lotesService;
        _ventaService = ventaService;
        _detalleVentaService = detalleVentaService;
        _clienteService = clienteService;
        _cuentasService = cuentasService;

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

        QuestPDF.Settings.License = LicenseType.Community;
    }

    private async void VentaForm_Load(object? sender, EventArgs e)
    {
        await CargarCombos();
        await CargarMedicamentosBuscar();
        
        txtBuscar.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtBuscar.AutoCompleteSource = AutoCompleteSource.CustomSource;
        txtBuscar.DropDownStyle = ComboBoxStyle.DropDown;
        
        txtBuscar.KeyDown += txtBuscar_KeyDown;
        txtBuscar.SelectedIndexChanged += txtBuscar_SelectedIndexChanged;
        numDowDinero.ValueChanged += numDowDinero_ValueChanged;
        cmbTFactura.SelectedIndexChanged += cmbTFactura_SelectedIndexChanged;

        foreach (DataGridViewColumn col in dgvCarrito.Columns)
        {
            if (col.Name != "Cantidad")
                col.ReadOnly = true;
        }

        dgvCarrito.CellValueChanged += dgvCarrito_CellValueChanged;
    }

    private async Task CargarCombos()
    {
        var clientes = await _clienteService.GetList(c => true);
        cmbCliente.DataSource = clientes;
        cmbCliente.DisplayMember = "Nombre";
        cmbCliente.ValueMember = "ClienteId";
        cmbCliente.SelectedIndex = -1;

        cmbTFactura.Items.AddRange(new[] { "Al Contado", "A Crédito" });
        cmbTFactura.SelectedIndex = 0;

        cmbComprobante.Items.AddRange(new[] { "Consumidor Final", "Crédito Fiscal" });
        cmbComprobante.SelectedIndex = 0;
    }

    private async Task CargarMedicamentosBuscar()
    {
        var lotesConStock = await _lotesService.GetList(l => l.CantidadActual > 0);
        var medicamentos = await _medicamentoService.GetList(m => true);
        
        _productosTodos = (from m in medicamentos
            join l in lotesConStock on m.MedicamentoId equals l.MedicamentoId into joinLotes
            from lote in joinLotes.DefaultIfEmpty()
            where joinLotes.Any() || true
            select new ProductoBuscadoDTO
            {
                LoteId = lote?.LoteId ?? 0,
                MedicamentoId = m.MedicamentoId,
                Nombre = m.Nombre,
                CodigoBarras = m.CodigoBarras,
                CantidadActual = lote?.CantidadActual ?? 0,
                PrecioVenta = m.PrecioVenta
            }).ToList();

        var uniqueProducts = _productosTodos
            .GroupBy(p => p.MedicamentoId)
            .Select(g => g.First())
            .ToList();

        txtBuscar.Items.Clear();
        foreach (var p in uniqueProducts)
        {
            txtBuscar.Items.Add(p);
        }
        txtBuscar.DisplayMember = "Nombre";
        txtBuscar.ValueMember = "MedicamentoId";

        var autoComplete = new AutoCompleteStringCollection();
        foreach (var p in uniqueProducts)
        {
            autoComplete.Add(p.Nombre);
        }
        txtBuscar.AutoCompleteCustomSource = autoComplete;
    }

    private void txtBuscar_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (txtBuscar.SelectedItem is ProductoBuscadoDTO producto)
        {
            var productoConStock = _productosTodos
                .FirstOrDefault(p => p.MedicamentoId == producto.MedicamentoId && p.CantidadActual > 0);
            
            if (productoConStock != null)
            {
                AgregarAlCarrito(productoConStock);
            }
            else
            {
                MessageBox.Show("Este producto no tiene stock disponible.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            txtBuscar.SelectedIndex = -1;
            txtBuscar.Text = "";
        }
    }

    private void cmbTFactura_SelectedIndexChanged(object? sender, EventArgs e)
    {
        bool esCredito = cmbTFactura.SelectedItem?.ToString() == "A Crédito";
        
        numDowDinero.Enabled = !esCredito;
        nudPagoCon.Enabled = !esCredito;
        
        if (esCredito)
        {
            numDowDinero.Value = 0;
            nudPagoCon.Value = 0;
            lblMontoCambio.Text = "RD$ 0.00";
            lblMontoCambio.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
        }
    }

    private async void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            string criterio = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                await BuscarYAgregarProducto(criterio);
            }
        }
    }

    private class ProductoBuscadoDTO
    {
        public int LoteId { get; set; }
        public int MedicamentoId { get; set; }
        public string Nombre { get; set; } = "";
        public string? CodigoBarras { get; set; }
        public int CantidadActual { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateOnly FechaVencimiento { get; set; }
    }

    private async Task BuscarYAgregarProducto(string criterio)
    {
        var medicamentos = await _medicamentoService.GetList(m => m.Activo);
        var lotes = await _lotesService.GetList(l => true);

        var productos = (from m in medicamentos
                         join l in lotes on m.MedicamentoId equals l.MedicamentoId into lotesGroup
                         from l in lotesGroup.DefaultIfEmpty()
                         where m.Nombre.ToLower().Contains(criterio.ToLower()) ||
                               (m.CodigoBarras != null && m.CodigoBarras.ToLower().Contains(criterio.ToLower())) ||
                               m.MedicamentoId.ToString() == criterio
                         select new ProductoBuscadoDTO
                         {
                             LoteId = l != null ? l.LoteId : 0,
                             MedicamentoId = m.MedicamentoId,
                             Nombre = m.Nombre,
                             CodigoBarras = m.CodigoBarras,
                             CantidadActual = l != null ? l.CantidadActual : 0,
                             PrecioVenta = m.PrecioVenta,
                             FechaVencimiento = l != null ? l.FechaVencimiento : DateOnly.FromDateTime(DateTime.Now.AddYears(1))
                         }).ToList();

        if (productos.Count == 0)
        {
            MessageBox.Show("No se encontraron productos que coincidan con la búsqueda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        ProductoBuscadoDTO? productoSeleccionado = null;

        if (productos.Count == 1)
        {
            productoSeleccionado = productos[0];
        }
        else
        {
            var exactMatch = productos.FirstOrDefault(p => p.CodigoBarras == criterio);
            if (exactMatch != null)
            {
                productoSeleccionado = exactMatch;
            }
            else
            {
                MessageBox.Show($"Se encontraron {productos.Count} coincidencias. Por favor, sea más específico en su búsqueda (ej. use código de barras o ID exacto).", "Múltiples Coincidencias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        if (productoSeleccionado != null)
        {
            AgregarAlCarrito(productoSeleccionado);
            txtBuscar.Focus();
        }
    }

    private void AgregarAlCarrito(ProductoBuscadoDTO producto)
    {
        int cantidadAAgregar = 1;

        var existente = dtCarrito.AsEnumerable()
            .FirstOrDefault(r => r.Field<int>("LoteId") == producto.LoteId);

        if (existente != null)
        {
            int cantidadActual = existente.Field<int>("Cantidad");
            if (cantidadActual + cantidadAAgregar <= producto.CantidadActual)
            {
                existente["Cantidad"] = cantidadActual + cantidadAAgregar;
                existente["Subtotal"] = (cantidadActual + cantidadAAgregar) * producto.PrecioVenta;
            }
            else
            {
                MessageBox.Show("Stock insuficiente para agregar más de este producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            if (cantidadAAgregar > producto.CantidadActual)
            {
                MessageBox.Show("Stock insuficiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var row = dtCarrito.NewRow();
            row["LoteId"] = producto.LoteId;
            row["MedicamentoId"] = producto.MedicamentoId;
            row["Nombre"] = producto.Nombre;
            row["Precio"] = producto.PrecioVenta;
            row["Cantidad"] = cantidadAAgregar;
            row["Subtotal"] = cantidadAAgregar * producto.PrecioVenta;
            dtCarrito.Rows.Add(row);
        }

        dgvCarrito.DataSource = dtCarrito;
        dgvCarrito.ClearSelection();
        CalcularTotal();
    }

    private void dgvCarrito_SelectionChanged(object? sender, EventArgs e)
    {
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
                    decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
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

    private void numDowDinero_ValueChanged(object? sender, EventArgs e)
    {
        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        decimal pago = numDowDinero.Value;
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

    private async void btnCobrar_Click(object sender, EventArgs e)
    {
        if (dtCarrito.Rows.Count == 0)
        {
            MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        decimal pago = numDowDinero.Value;
        bool esCredito = cmbTFactura.SelectedItem?.ToString() == "A Crédito";

        if (!esCredito && pago < total)
        {
            MessageBox.Show("El monto de pago es insuficiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var confirmResult = MessageBox.Show($"¿Confirma el cobro de la venta por RD$ {total:N2}?",
                                     "Confirmar Cobro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmResult != DialogResult.Yes) return;

        try
        {
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                Total = total,
                MetodoPago = cmbMetodoPago.SelectedItem?.ToString() ?? "Efectivo"
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

            if (esCredito && cmbCliente.SelectedValue != null)
            {
                var cuentaPorCobrar = new CuentasPorCobrar
                {
                    VentaId = venta.VentaId,
                    ClienteId = (int)cmbCliente.SelectedValue,
                    MontoInicial = total,
                    SaldoPendiente = total,
                    FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                    Estado = "Pendiente"
                };
                await _cuentasService.Guardar(cuentaPorCobrar);
            }

            decimal cambio = esCredito ? 0 : pago - total;
            string tipoStr = esCredito ? "Crédito" : "Contado";

            string mensaje = esCredito 
                ? $"Venta a crédito realizada exitosamente!\n\nTotal: RD$ {total:N2}\nCliente: {cmbCliente.SelectedItem}"
                : $"Venta realizada exitosamente!\n\nTotal a pagar: RD$ {total:N2}\nTotal a devolver: RD$ {cambio:N2}";

            var printResult = MessageBox.Show($"{mensaje}\n\n¿Desea imprimir la factura en PDF?",
                "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (printResult == DialogResult.Yes)
            {
                GenerarFacturaPDF(venta.VentaId, total, esCredito ? 0 : pago, cambio, tipoStr);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al procesar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnAddCuenta_Click(object sender, EventArgs e)
    {
        if (dtCarrito.Rows.Count == 0)
        {
            MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbCliente.SelectedValue == null)
        {
            MessageBox.Show("Por favor seleccione un cliente para la venta a crédito.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int clienteId = (int)cmbCliente.SelectedValue;
        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));

        var confirmResult = MessageBox.Show($"¿Desea facturar a crédito por RD$ {total:N2} a la cuenta del cliente seleccionado?",
                                     "Confirmar Cuenta a Crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmResult != DialogResult.Yes) return;

        try
        {
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                Total = total,
                MetodoPago = "Crédito"
            };

            if (!await _ventaService.Guardar(venta)) return;

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

            var cxc = new CuentasPorCobrar
            {
                VentaId = venta.VentaId,
                ClienteId = clienteId,
                MontoInicial = total,
                SaldoPendiente = total,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                Estado = "Pendiente"
            };
            await _cuentasService.Guardar(cxc);

            var printResult = MessageBox.Show($"Venta a crédito realizada exitosamente!\n\n¿Desea imprimir la factura en PDF?",
                "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (printResult == DialogResult.Yes)
            {
                GenerarFacturaPDF(venta.VentaId, total, 0, 0, "Crédito");
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al procesar la venta a crédito: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnPreCompra_Click(object sender, EventArgs e)
    {
        if (dtCarrito.Rows.Count == 0)
        {
            MessageBox.Show("El carrito está vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        decimal total = dtCarrito.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
        GenerarFacturaPDF(0, total, 0, 0, "Pre-Factura");
        MessageBox.Show("Pre-factura generada y guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void GenerarFacturaPDF(int ventaId, decimal total, decimal pago, decimal cambio, string tipo)
    {
        string filename = $"{(tipo == "Pre-Factura" ? "PreFactura" : "Factura")}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

        string clienteNombre = cmbCliente.Text;
        string comprobante = cmbComprobante.Text;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Element(compose =>
                {
                    compose.Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("PharmaSoft").FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                            col.Item().Text($"{(tipo == "Pre-Factura" ? "Pre-Factura" : "Factura N° " + ventaId)}").FontSize(14);
                            col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
                            if (!string.IsNullOrEmpty(clienteNombre))
                                col.Item().Text($"Cliente: {clienteNombre}");
                            col.Item().Text($"Comprobante: {comprobante}");
                        });
                    });
                });

                page.Content().Element(compose =>
                {
                    compose.PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50); // Cantidad
                                columns.RelativeColumn();   // Descripción
                                columns.ConstantColumn(80); // Precio
                                columns.ConstantColumn(80); // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Cant.").SemiBold();
                                header.Cell().Text("Descripción").SemiBold();
                                header.Cell().Text("Precio").SemiBold().AlignRight();
                                header.Cell().Text("Total").SemiBold().AlignRight();
                                header.Cell().ColumnSpan(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });

                            foreach (DataRow row in dtCarrito.Rows)
                            {
                                table.Cell().Text(row["Cantidad"].ToString());
                                table.Cell().Text(row["Nombre"].ToString());
                                table.Cell().Text($"RD$ {Convert.ToDecimal(row["Precio"]):N2}").AlignRight();
                                table.Cell().Text($"RD$ {Convert.ToDecimal(row["Subtotal"]):N2}").AlignRight();
                            }
                        });

                        col.Item().PaddingTop(20).AlignRight().Text($"Total a Pagar: RD$ {total:N2}").FontSize(14).SemiBold();
                        if (tipo == "Contado")
                        {
                            col.Item().AlignRight().Text($"Pago: RD$ {pago:N2}");
                            col.Item().AlignRight().Text($"Cambio: RD$ {cambio:N2}");
                        }
                    });
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        })
        .GeneratePdf(filePath);

        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
        catch { }
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

    private void pnlAcciones_Paint(object sender, PaintEventArgs e)
    {

    }

    private void txtBuscar_TextChanged(object sender, EventArgs e)
    {

    }
}