using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class PagosProveedoreServiceTests
{
    [Fact]
    public async Task Guardar_NuevoPagoProv_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = new Proveedore { NombreEmpresa = "Prov" };
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 2000m };
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();

            var cxp = new CuentasPorPagar
            {
                ProveedorId = proveedor.ProveedorId,
                CompraId = compra.CompraId,
                MontoInicial = 2000m,
                SaldoPendiente = 2000m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            seedContext.CuentasPorPagars.Add(cxp);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var cxpGuardada = await context.CuentasPorPagars.FirstAsync();
        var servicio = new PagosProveedoreService(context);

        var nuevoPago = new PagosProveedore
        {
            CxPid = cxpGuardada.CxPid,
            MontoPagado = 1000m,
            MetodoPago = "Cheque"
        };

        // Act
        var resultado = await servicio.Guardar(nuevoPago);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.PagosProveedores.Count());
    }

    [Fact]
    public async Task Eliminar_PagoProvExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = new Proveedore { NombreEmpresa = "P" };
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 100m };
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();

            var cxp = new CuentasPorPagar
            {
                ProveedorId = proveedor.ProveedorId,
                CompraId = compra.CompraId,
                MontoInicial = 100m,
                SaldoPendiente = 100m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            seedContext.CuentasPorPagars.Add(cxp);
            await seedContext.SaveChangesAsync();

            var pago = new PagosProveedore { CxPid = cxp.CxPid, MontoPagado = 100m };
            seedContext.PagosProveedores.Add(pago);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new PagosProveedoreService(context);
        var pagoGuardado = await context.PagosProveedores.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(pagoGuardado.PagoProveedorId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.PagosProveedores);
    }
}
