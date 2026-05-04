using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class PagosProveedoreServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones() =>
        new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;

    public PagosProveedoreServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoPagoProv_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedor = new Proveedore { NombreEmpresa = "Prov" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 2000m };
        contexto.Compras.Add(compra);
        await contexto.SaveChangesAsync();

        var cxp = new CuentasPorPagar { ProveedorId = proveedor.ProveedorId, CompraId = compra.CompraId, MontoInicial = 2000m, SaldoPendiente = 2000m, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.CuentasPorPagars.Add(cxp);
        await contexto.SaveChangesAsync();

        var servicio = new PagosProveedoreService(contexto);
        var nuevoPago = new PagosProveedore
        {
            CxPid = cxp.CxPid,
            MontoPagado = 1000m,
            MetodoPago = "Cheque"
        };

        var resultado = await servicio.Guardar(nuevoPago);

        Assert.True(resultado);
        Assert.Equal(1, contexto.PagosProveedores.Count());
    }

    [Fact]
    public async Task Eliminar_PagoProvExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedor = new Proveedore { NombreEmpresa = "P" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 100 };
        contexto.Compras.Add(compra);
        await contexto.SaveChangesAsync();

        var cxp = new CuentasPorPagar { ProveedorId = proveedor.ProveedorId, CompraId = compra.CompraId, MontoInicial = 100, SaldoPendiente = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.CuentasPorPagars.Add(cxp);
        await contexto.SaveChangesAsync();

        var pago = new PagosProveedore { CxPid = cxp.CxPid, MontoPagado = 100 };
        contexto.PagosProveedores.Add(pago);
        await contexto.SaveChangesAsync();

        var servicio = new PagosProveedoreService(contexto);
        var resultadoEliminar = await servicio.Eliminar(pago.PagoProveedorId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.PagosProveedores);
    }
}