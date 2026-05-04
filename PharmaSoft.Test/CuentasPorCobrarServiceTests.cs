using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class CuentasPorCobrarServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        // GitHub Actions automáticamente establece esta variable en "true"
        bool enGitHub = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

        // Si estamos en GitHub usa LocalDB, si estamos en tu PC usa SqlExpress
        string servidor = enGitHub ? "(localdb)\\MSSQLLocalDB" : ".\\SqlExpress";

        string connectionString = $"Data Source={servidor};Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer(connectionString)
            .Options;
    }

    public CuentasPorCobrarServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevaCxC_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        // Dependencias
        var cliente = new Cliente { Nombre = "Cliente CxC" };
        var venta = new Venta { Total = 500m };
        contexto.Clientes.Add(cliente);
        contexto.Ventas.Add(venta);
        await contexto.SaveChangesAsync();

        var servicio = new CuentasPorCobrarService(contexto);
        var nuevaCxC = new CuentasPorCobrar
        {
            ClienteId = cliente.ClienteId,
            VentaId = venta.VentaId,
            MontoInicial = 500m,
            SaldoPendiente = 500m,
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
            Estado = "Pendiente"
        };

        var resultado = await servicio.Guardar(nuevaCxC);

        Assert.True(resultado);
        Assert.Equal(1, contexto.CuentasPorCobrars.Count());
        Assert.Equal(500m, contexto.CuentasPorCobrars.First().SaldoPendiente);
    }

    [Fact]
    public async Task Eliminar_CxCExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var cliente = new Cliente { Nombre = "Cliente Prueba" };
        var venta = new Venta { Total = 100m };
        contexto.Clientes.Add(cliente);
        contexto.Ventas.Add(venta);
        await contexto.SaveChangesAsync();

        var cxc = new CuentasPorCobrar { ClienteId = cliente.ClienteId, VentaId = venta.VentaId, MontoInicial = 100, SaldoPendiente = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.CuentasPorCobrars.Add(cxc);
        await contexto.SaveChangesAsync();

        var servicio = new CuentasPorCobrarService(contexto);

        var resultadoEliminar = await servicio.Eliminar(cxc.CxCid);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.CuentasPorCobrars);
    }
}