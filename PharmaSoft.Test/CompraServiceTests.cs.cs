using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class CompraServiceTests
{
    [Fact]
    public async Task Guardar_NuevaCompra_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Distribuidora Salud");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var proveedorGuardado = await context.Proveedores.FirstAsync();
        var servicio = new CompraService(context);

        var nuevaCompra = new Compra
        {
            ProveedorId = proveedorGuardado.ProveedorId,
            FechaCompra = DateTime.Now,
            TotalCompra = 15000.50m,
            TipoPago = "Crédito",
            NumeroFacturaProveedor = "FACT-001"
        };

        // Act
        var resultado = await servicio.Guardar(nuevaCompra);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.Compras.Count());
        Assert.Equal(15000.50m, context.Compras.First().TotalCompra);
    }

    [Fact]
    public async Task Eliminar_CompraExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Laboratorios X");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var compraPrueba = new Compra
            {
                ProveedorId = proveedor.ProveedorId,
                TotalCompra = 5000m
            };
            seedContext.Compras.Add(compraPrueba);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CompraService(context);

        var compraGuardada = await context.Compras.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(compraGuardada.CompraId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.Compras);
    }

    [Fact]
    public async Task Buscar_CuandoExisteCompra_RetornaEntidad()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Proveedor A");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            seedContext.Compras.Add(new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 1234.56m });
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CompraService(context);

        // Act
        var compra = await servicio.Buscar(1);

        // Assert
        Assert.NotNull(compra);
        Assert.Equal(1, compra!.CompraId);
        Assert.Equal(1234.56m, compra.TotalCompra);
        Assert.Empty(context.ChangeTracker.Entries());
    }

    [Fact]
    public async Task Listar_CuandoSeFiltraPorTotal_RetornaCoincidencias()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedor = CreateProveedor(nombre: "Prov");
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            seedContext.Compras.AddRange(
                new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 100m },
                new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 5000m },
                new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 10000m }
            );
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new CompraService(context);

        // Act
        var resultado = await servicio.GetList(c => c.TotalCompra >= 5000m);

        // Assert
        Assert.Equal(2, resultado.Count);
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
