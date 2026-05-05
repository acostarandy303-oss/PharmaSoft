using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class PagosClienteServiceTests
{
    [Fact]
    public async Task Guardar_NuevoPago_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var cliente = new Cliente { Nombre = "Deudor" };
            var venta = new Venta { Total = 1000m };
            seedContext.Clientes.Add(cliente);
            seedContext.Ventas.Add(venta);
            await seedContext.SaveChangesAsync();

            var cxc = new CuentasPorCobrar
            {
                ClienteId = cliente.ClienteId,
                VentaId = venta.VentaId,
                MontoInicial = 1000m,
                SaldoPendiente = 1000m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            seedContext.CuentasPorCobrars.Add(cxc);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var cxcGuardada = await context.CuentasPorCobrars.FirstAsync();
        var servicio = new PagosClienteService(context);

        var nuevoPago = new PagosCliente
        {
            CxCid = cxcGuardada.CxCid,
            FechaPago = DateTime.Now,
            MontoPagado = 500m,
            MetodoPago = "Transferencia"
        };

        // Act
        var resultado = await servicio.Guardar(nuevoPago);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.PagosClientes.Count());
        Assert.Equal(500m, context.PagosClientes.First().MontoPagado);
    }

    [Fact]
    public async Task Eliminar_PagoExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var cliente = new Cliente { Nombre = "C" };
            var venta = new Venta { Total = 100m };
            seedContext.Clientes.Add(cliente);
            seedContext.Ventas.Add(venta);
            await seedContext.SaveChangesAsync();

            var cxc = new CuentasPorCobrar
            {
                ClienteId = cliente.ClienteId,
                VentaId = venta.VentaId,
                MontoInicial = 100m,
                SaldoPendiente = 100m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            seedContext.CuentasPorCobrars.Add(cxc);
            await seedContext.SaveChangesAsync();

            var pago = new PagosCliente { CxCid = cxc.CxCid, MontoPagado = 50m };
            seedContext.PagosClientes.Add(pago);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new PagosClienteService(context);
        var pagoGuardado = await context.PagosClientes.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(pagoGuardado.PagoClienteId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.PagosClientes);
    }
}
