using System.ComponentModel;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaSoft.Forms;

namespace PharmaSoft;

public partial class PharmaSoft : Form
{
    private readonly PharmaContext _context;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;
    private readonly VentaService _ventaService;
    private readonly CuentasPorCobrarService _cuentasCobrarService;

    public PharmaSoft(
        PharmaContext context,
        MedicamentoService medicamentoService,
        LotesInventarioService lotesService,
        VentaService ventaService,
        CuentasPorCobrarService cuentasCobrarService)
    {
        InitializeComponent();
        _context = context;
        _medicamentoService = medicamentoService;
        _lotesService = lotesService;
        _ventaService = ventaService;
        _cuentasCobrarService = cuentasCobrarService;
    }

    private async void PharmaSoft_Load(object sender, EventArgs e)
    {
        foreach (Control control in panelLateral.Controls)
        {
            if (control is Button btn)
            {
                btn.Click += (s, args) => ActivarBoton(s);
            }
        }
        ActivarBoton(btnInicio);
        await CargarDashboard();
    }

    private async Task CargarDashboard()
    {
        try
        {
            var stockTotal = await _lotesService.GetList(l => true);
            int cantidadStock = stockTotal.Sum(l => l.CantidadActual);
            lbCantidadStock.Text = cantidadStock.ToString("N0");

            var ventasHoy = await _ventaService.GetList(v => v.FechaVenta != null && v.FechaVenta.Value.Date == DateTime.Today);
            decimal totalVentas = ventasHoy.Sum(v => v.Total);
            lbCatidadVentas.Text = $"RD$ {totalVentas:N2}";

            var cuentasCobrar = await _cuentasCobrarService.GetList(c => c.Estado == "Pendiente");
            decimal totalCuentas = cuentasCobrar.Sum(c => c.SaldoPendiente);
            lbTotalCxCobrar.Text = $"RD$ {totalCuentas:N2}";

            lblTotalProductos.Text = $"Total Productos: {cantidadStock}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ActivarBoton(object senderBoton)
    {
        if (senderBoton != null)
        {
            Button botonActivo = (Button)senderBoton;
            foreach (Control previousBtn in panelLateral.Controls)
            {
                if (previousBtn is Button btn)
                {
                    btn.BackColor = Color.FromArgb(248, 249, 250);
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }
            botonActivo.BackColor = Color.FromArgb(25, 118, 210);
            botonActivo.ForeColor = Color.White;
            botonActivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
    }

    private void btnInventario_Click(object sender, EventArgs e)
    {
        var inventarioForm = Program.ServiceProvider.GetRequiredService<InventarioForm>();
        inventarioForm.Show();
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
        ActivarBoton(btnInicio);
    }

    private void btnVentas_Click(object sender, EventArgs e)
    {
        var ventasForm = Program.ServiceProvider.GetRequiredService<VentaForm>();
        ventasForm.Show();
    }

    private void btnClientes_Click(object sender, EventArgs e)
    {
        var clienteForm = Program.ServiceProvider.GetRequiredService<ClienteForm>();
        clienteForm.Show();
    }

    private void btnCompras_Click(object sender, EventArgs e)
    {
        var compraForm = Program.ServiceProvider.GetRequiredService<CompraForm>();
        compraForm.Show();
    }

    private void btnReportes_Click(object sender, EventArgs e)
    {
        var reportesForm = Program.ServiceProvider.GetRequiredService<ReportesForm>();
        reportesForm.Show();
    }

    private void btnCuentasCobrar_Click(object sender, EventArgs e)
    {
        var cuentasCobrarForm = Program.ServiceProvider.GetRequiredService<CuentasCobrarForm>();
        cuentasCobrarForm.Show();
    }

    private void btnCuentasPagar_Click(object sender, EventArgs e)
    {
        var cuentasPagarForm = Program.ServiceProvider.GetRequiredService<CuentasPagarForm>();
        cuentasPagarForm.Show();
    }

    private void btnConfiguracion_Click(object sender, EventArgs e)
    {
        var configForm = Program.ServiceProvider.GetRequiredService<ConfiguracionForm>();
        configForm.Show();
    }
}