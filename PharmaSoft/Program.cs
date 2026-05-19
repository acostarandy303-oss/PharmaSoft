using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaSoft.Data.Context;
using PharmaSoft.Forms;
using PharmaSoft.Services;
using System.Windows.Forms;

namespace PharmaSoft;

internal static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();


        var connectionString = System.Configuration.ConfigurationManager
        .ConnectionStrings["PharmaDb"].ConnectionString;

        services.AddDbContext<PharmaContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            }), ServiceLifetime.Transient);

        //Forms
        services.AddTransient<MedicamentoForm>();
        services.AddTransient<InventarioForm>();
        services.AddTransient<VentaForm>();
        services.AddTransient<ClienteForm>();
        services.AddTransient<ReportesForm>();
        services.AddTransient<CompraForm>();
        services.AddTransient<ConfiguracionForm>();
        services.AddTransient<CuentasCobrarForm>();
        services.AddTransient<CuentasPagarForm>();

        //Services
        services.AddTransient<MedicamentoService>();
        services.AddTransient<LotesInventarioService>();
        services.AddTransient<VentaService>();
        services.AddTransient<DetalleVentaService>();
        services.AddTransient<CuentasPorCobrarService>();
        services.AddTransient<ClienteService>();
        services.AddTransient<PagosClienteService>();
        services.AddTransient<CuentasPorPagarService>();
        services.AddTransient<ProveedoreService>();
        services.AddTransient<PagosProveedoreService>();
        services.AddTransient<CompraService>();
        services.AddTransient<DetalleCompraService>();
        services.AddTransient<CategoriaService>();
        services.AddTransient<PharmaSoft>();




        ServiceProvider = services.BuildServiceProvider();

        var loginForm = new LoginForm();
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Application.Run(ServiceProvider.GetRequiredService<PharmaSoft>());
        }
    }
}