using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaSoft.Data.Context;
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

        var connectionString = "Data Source=.\\SqlExpress;Database=PharmaDb;Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True";
        services.AddDbContext<PharmaContext>(options =>
            options.UseSqlServer(connectionString),
            ServiceLifetime.Transient);

        services.AddTransient<MedicamentoService>();
        services.AddTransient<PharmaSoft>();
        ServiceProvider = services.BuildServiceProvider();

        Application.Run(ServiceProvider.GetRequiredService<PharmaSoft>());
    }
}