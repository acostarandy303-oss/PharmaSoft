using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class CuentasPorCobrarServiceTests
{
    [Fact]
    public async Task Guardar_NuevaCxC_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var cliente = new Cliente { Nombre = "Cliente CxC" };
            var venta = new Venta { Total = 500m };
            seedContext.Clientes.Add(cliente);
            seedContext.Ventas.Add(venta);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var clienteGuardado = await context.Clientes.FirstAsync();
        var ventaGuardada = await context.Ventas.FirstAsync();
        var servicio = new CuentasPorCobrarService(context);

        var nuevaCxC = new CuentasPorCobrar
        {
            ClienteId = clienteGuardado.ClienteId,
            VentaId = ventaGuardada.VentaId,
            MontoInicial = 500m,
            SaldoPendiente = 500m,
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(15)),
            Estado = "Pendiente"
        };

        // Act
        var resultado = await servicio.Guardar(nuevaCxC);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.CuentasPorCobrars.Count());
        Assert.Equal(500m, context.CuentasPorCobrars.First().SaldoPendiente);
    }

    [Fact]
    public async Task Eliminar_CxCExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var cliente = new Cliente { Nombre = "Cliente Prueba" };
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
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CuentasPorCobrarService(context);

        var cxcGuardada = await context.CuentasPorCobrars.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(cxcGuardada.CxCid);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.CuentasPorCobrars);
    }

    [Fact]
    public async Task Buscar_CuandoExisteCxC_RetornaEntidad()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var cliente = new Cliente { Nombre = "Cliente B" };
            var venta = new Venta { Total = 250m };
            seedContext.Clientes.Add(cliente);
            seedContext.Ventas.Add(venta);
            await seedContext.SaveChangesAsync();

            seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar
            {
                ClienteId = cliente.ClienteId,
                VentaId = venta.VentaId,
                MontoInicial = 250m,
                SaldoPendiente = 250m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            });
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CuentasPorCobrarService(context);

        // Act
        var cxc = await servicio.Buscar(1);

        // Assert
        Assert.NotNull(cxc);
        Assert.Equal(1, cxc!.CxCid);
        Assert.Equal(250m, cxc.SaldoPendiente);
        Assert.Empty(context.ChangeTracker.Entries());
    }
}
