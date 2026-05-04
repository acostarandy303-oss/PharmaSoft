using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class CuentasPorPagarServiceTests
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

    public CuentasPorPagarServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevaCxP_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedor = new Proveedore { NombreEmpresa = "Prov CxP" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 1000m };
        contexto.Compras.Add(compra);
        await contexto.SaveChangesAsync();

        var servicio = new CuentasPorPagarService(contexto);
        var nuevaCxP = new CuentasPorPagar
        {
            ProveedorId = proveedor.ProveedorId,
            CompraId = compra.CompraId,
            MontoInicial = 1000m,
            SaldoPendiente = 1000m,
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
            Estado = "Pendiente"
        };

        var resultado = await servicio.Guardar(nuevaCxP);

        Assert.True(resultado);
        Assert.Equal(1, contexto.CuentasPorPagars.Count());
    }

    [Fact]
    public async Task Eliminar_CxPExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedor = new Proveedore { NombreEmpresa = "Prov" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 1000m };
        contexto.Compras.Add(compra);
        await contexto.SaveChangesAsync();

        var cxp = new CuentasPorPagar { ProveedorId = proveedor.ProveedorId, CompraId = compra.CompraId, MontoInicial = 100, SaldoPendiente = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.CuentasPorPagars.Add(cxp);
        await contexto.SaveChangesAsync();

        var servicio = new CuentasPorPagarService(contexto);

        var resultadoEliminar = await servicio.Eliminar(cxp.CxPid);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.CuentasPorPagars);
    }
}