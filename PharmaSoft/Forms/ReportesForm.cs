using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PharmaSoft.Forms;

public partial class ReportesForm : Form
{
    private readonly PharmaContext _context;
    private readonly VentaService _ventaService;
    private readonly DetalleVentaService _detalleVentaService;

    public ReportesForm(
        PharmaContext context,
        VentaService ventaService,
        DetalleVentaService detalleVentaService)
    {
        InitializeComponent();
        _context = context;
        _ventaService = ventaService;
        _detalleVentaService = detalleVentaService;
        dtpDesde.Value = DateTime.Today.AddDays(-30);
        dtpHasta.Value = DateTime.Today;
    }

    private async void ReportesForm_Load(object sender, EventArgs e)
    {
        await CargarVentas();
    }

    private async Task CargarVentas()
    {
        try
        {
            DateTime fechaInicio = dtpDesde.Value.Date;
            DateTime fechaFin = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            var ventas = await _ventaService.GetList(v => 
                v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin);

            var reporteData = ventas.Select(v => new
            {
                v.VentaId,
                FechaVenta = v.FechaVenta?.ToString("dd/MM/yyyy HH:mm") ?? "",
                v.Total,
                v.MetodoPago,
                Estado = v.MetodoPago == "Crédito" ? "Pendiente" : "Pagado"
            }).ToList();

            dgvReportes.DataSource = reporteData;
            
            if (dgvReportes.Columns["VentaId"] != null)
                dgvReportes.Columns["VentaId"].HeaderText = "ID";
            if (dgvReportes.Columns["FechaVenta"] != null)
                dgvReportes.Columns["FechaVenta"].HeaderText = "Fecha";
            if (dgvReportes.Columns["Total"] != null)
                dgvReportes.Columns["Total"].HeaderText = "Total (RD$)";
            if (dgvReportes.Columns["MetodoPago"] != null)
                dgvReportes.Columns["MetodoPago"].HeaderText = "Método de Pago";
            if (dgvReportes.Columns["Estado"] != null)
                dgvReportes.Columns["Estado"].HeaderText = "Estado";

            decimal totalVentas = reporteData.Sum(r => r.Total);
            lblTotal.Text = $"Total de Ventas: RD$ {totalVentas:N2}";
            lblCantidad.Text = $"Cantidad de Ventas: {reporteData.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnFiltrar_Click(object sender, EventArgs e)
    {
        await CargarVentas();
    }

    private async void btnImprimir_Click(object sender, EventArgs e)
    {
        if (dgvReportes.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione una venta para imprimir", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int ventaId = Convert.ToInt32(dgvReportes.SelectedRows[0].Cells["VentaId"].Value);
        await ImprimirFacturaPDF(ventaId);
    }

    private async Task ImprimirFacturaPDF(int ventaId)
    {
        try
        {
            var venta = await _ventaService.Buscar(ventaId);
            if (venta == null)
            {
                MessageBox.Show("Venta no encontrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var detalles = await _detalleVentaService.GetList(d => d.VentaId == ventaId);

            string filename = $"Factura_{ventaId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

            QuestPDF.Settings.License = LicenseType.Community;

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
                                col.Item().Text($"Factura N° {ventaId}").FontSize(14);
                                col.Item().Text($"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}");
                                col.Item().Text($"Método de Pago: {venta.MetodoPago}");
                            });
                        });
                    });

                    page.Content().Element(compose =>
                    {
                        compose.PaddingVertical(1, Unit.Centimetre).Column(col =>
                        {
                            col.Item().Text("Detalle de la Venta").FontSize(14).SemiBold();
                            col.Item().PaddingTop(10);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(80);
                                    columns.ConstantColumn(80);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Cant.").SemiBold();
                                    header.Cell().Text("Descripción").SemiBold();
                                    header.Cell().Text("Precio").SemiBold().AlignRight();
                                    header.Cell().Text("Total").SemiBold().AlignRight();
                                    header.Cell().ColumnSpan(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                                });

                                foreach (var detalle in detalles)
                                {
                                    table.Cell().Text(detalle.Cantidad.ToString());
                                    table.Cell().Text($"Lote #{detalle.LoteId}");
                                    table.Cell().Text($"RD$ {detalle.PrecioUnitario:N2}").AlignRight();
                                    table.Cell().Text($"RD$ {detalle.Subtotal:N2}").AlignRight();
                                }
                            });

                            col.Item().PaddingTop(20).AlignRight().Text($"Total a Pagar: RD$ {venta.Total:N2}").FontSize(14).SemiBold();
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

            MessageBox.Show($"Factura guardada en: {filePath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        catch (Exception ex)
        {
            MessageBox.Show($"Error al generar PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCerrar_Click(object sender, EventArgs e)
    {
        Close();
    }
}