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

        //Services
        services.AddTransient<MedicamentoService>();
        services.AddTransient<PharmaSoft>(); // invetarioForm




        ServiceProvider = services.BuildServiceProvider();

        Application.Run(ServiceProvider.GetRequiredService<PharmaSoft>());
    }
}