using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class VentaServiceTests
{
    [Fact]
    public async Task Guardar_NuevaVenta_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            // no dependencies to seed
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new VentaService(context);

        var nuevaVenta = new Venta
        {
            FechaVenta = DateTime.Now,
            Total = 1250.75m,
            MetodoPago = "Efectivo"
        };

        // Act
        var resultado = await servicio.Guardar(nuevaVenta);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.Ventas.Count());
        Assert.Equal(1250.75m, context.Ventas.First().Total);
    }

    [Fact]
    public async Task Eliminar_VentaExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var ventaPrueba = new Venta { Total = 300m, MetodoPago = "Tarjeta" };
            seedContext.Ventas.Add(ventaPrueba);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new VentaService(context);
        var ventaGuardada = await context.Ventas.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(ventaGuardada.VentaId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.Ventas);
    }
}
