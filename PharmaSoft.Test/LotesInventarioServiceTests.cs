using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class LotesInventarioServiceTests
{
    [Fact]
    public async Task Guardar_NuevoLote_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var categoria = new Categoria { Nombre = "Gripales" };
            var proveedor = new Proveedore { NombreEmpresa = "FarmaRD" };
            seedContext.Categorias.Add(categoria);
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var medicamento = new Medicamento
            {
                Nombre = "Antigripal",
                CategoriaId = categoria.CategoriaId,
                ProveedorId = proveedor.ProveedorId,
                PrecioCompra = 10m,
                PrecioVenta = 20m
            };
            seedContext.Medicamentos.Add(medicamento);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var medicamentoGuardado = await context.Medicamentos.FirstAsync();
        var servicio = new LotesInventarioService(context);

        var nuevoLote = new LotesInventario
        {
            MedicamentoId = medicamentoGuardado.MedicamentoId,
            NumeroLote = "LT-2026",
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            CantidadActual = 100
        };

        // Act
        var resultado = await servicio.Guardar(nuevoLote);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.LotesInventarios.Count());
        Assert.Equal("LT-2026", context.LotesInventarios.First().NumeroLote);
    }

    [Fact]
    public async Task Eliminar_LoteExistente_DebeRetornarTrue()
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
                PrecioCompra = 1m,
                PrecioVenta = 2m
            };
            seedContext.Medicamentos.Add(medicamento);
            await seedContext.SaveChangesAsync();

            var lote = new LotesInventario
            {
                MedicamentoId = medicamento.MedicamentoId,
                NumeroLote = "001",
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now),
                CantidadActual = 10
            };
            seedContext.LotesInventarios.Add(lote);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new LotesInventarioService(context);
        var loteGuardado = await context.LotesInventarios.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(loteGuardado.LoteId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.LotesInventarios);
    }
}
