using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class DetalleCompraServiceTests
{
    [Fact]
    public async Task Guardar_NuevoDetalle_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var categoria = new Categoria { Nombre = "Cat" };
            var proveedor = new Proveedore { NombreEmpresa = "Prov" };
            seedContext.Categorias.Add(categoria);
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var medicamento = new Medicamento
            {
                Nombre = "Med",
                CategoriaId = categoria.CategoriaId,
                ProveedorId = proveedor.ProveedorId,
                PrecioCompra = 10m,
                PrecioVenta = 20m
            };

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 100m };
            seedContext.Medicamentos.Add(medicamento);
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var compraGuardada = await context.Compras.FirstAsync();
        var medicamentoGuardado = await context.Medicamentos.FirstAsync();
        var servicio = new DetalleCompraService(context);

        var detalle = new DetalleCompra
        {
            CompraId = compraGuardada.CompraId,
            MedicamentoId = medicamentoGuardado.MedicamentoId,
            Cantidad = 10,
            PrecioCostoUnitario = 10m
        };

        // Act
        var resultado = await servicio.Guardar(detalle);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.DetalleCompras.Count());
    }

    [Fact]
    public async Task Eliminar_DetalleExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var categoria = new Categoria { Nombre = "C" };
            var proveedor = new Proveedore { NombreEmpresa = "P" };
            seedContext.Categorias.Add(categoria);
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var medicamento = new Medicamento
            {
                Nombre = "M",
                CategoriaId = categoria.CategoriaId,
                ProveedorId = proveedor.ProveedorId,
                PrecioCompra = 1m,
                PrecioVenta = 2m
            };

            var compra = new Compra { ProveedorId = proveedor.ProveedorId, TotalCompra = 10m };
            seedContext.Medicamentos.Add(medicamento);
            seedContext.Compras.Add(compra);
            await seedContext.SaveChangesAsync();

            var detalle = new DetalleCompra
            {
                CompraId = compra.CompraId,
                MedicamentoId = medicamento.MedicamentoId,
                Cantidad = 1,
                PrecioCostoUnitario = 10m
            };
            seedContext.DetalleCompras.Add(detalle);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new DetalleCompraService(context);
        var detalleGuardado = await context.DetalleCompras.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(detalleGuardado.DetalleCompraId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.DetalleCompras);
    }
}
