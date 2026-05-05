using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class CuentasPorPagarServiceTests
{
    [Fact]
    public async Task Guardar_NuevaCxP_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Prov CxP");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 1000m };
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var proveedorGuardado = await context.Proveedores.FirstAsync();
        var compraGuardada = await context.Compras.FirstAsync();
        var servicio = new CuentasPorPagarService(context);

        var nuevaCxP = new CuentasPorPagar
        {
            ProveedorId = proveedorGuardado.ProveedorId,
            CompraId = compraGuardada.CompraId,
            MontoInicial = 1000m,
            SaldoPendiente = 1000m,
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
            Estado = "Pendiente"
        };

        // Act
        var resultado = await servicio.Guardar(nuevaCxP);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.CuentasPorPagars.Count());
    }

    [Fact]
    public async Task Eliminar_CxPExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Prov");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 1000m };
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
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CuentasPorPagarService(context);

        var cxpGuardada = await context.CuentasPorPagars.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(cxpGuardada.CxPid);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.CuentasPorPagars);
    }

    [Fact]
    public async Task Buscar_CuandoExisteCxP_RetornaEntidad()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Prov B");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 250m };
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();

            seedContext.CuentasPorPagars.Add(new CuentasPorPagar
            {
                ProveedorId = proveedor.ProveedorId,
                CompraId = compra.CompraId,
                MontoInicial = 250m,
                SaldoPendiente = 250m,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now)
            });
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CuentasPorPagarService(context);

        // Act
        var cxp = await servicio.Buscar(1);

        // Assert
        Assert.NotNull(cxp);
        Assert.Equal(1, cxp!.CxPid);
        Assert.Equal(250m, cxp.SaldoPendiente);
        Assert.Empty(context.ChangeTracker.Entries());
    }

    private static Proveedore CreateProveedor(string nombre)
    {
        return new Proveedore
        {
            NombreEmpresa = nombre,
            Contacto = string.Empty,
            Telefono = string.Empty,
            Email = string.Empty
        };
    }
}
