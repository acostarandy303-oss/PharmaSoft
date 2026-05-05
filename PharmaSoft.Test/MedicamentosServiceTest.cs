using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class MedicamentoServiceTests
{
    [Fact]
    public async Task Guardar_NuevoMedicamento_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var categoria = new Categoria { Nombre = "Analgésicos" };
            var proveedor = new Proveedore { NombreEmpresa = "FarmaCorp" };
            seedContext.Categorias.Add(categoria);
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var categoriaGuardada = await context.Categorias.FirstAsync();
        var proveedorGuardado = await context.Proveedores.FirstAsync();
        var servicio = new MedicamentoService(context);

        var nuevoMedicamento = new Medicamento
        {
            Nombre = "Ibuprofeno 400mg",
            PrincipioActivo = "Ibuprofeno",
            CategoriaId = categoriaGuardada.CategoriaId,
            ProveedorId = proveedorGuardado.ProveedorId,
            PrecioCompra = 50.00m,
            PrecioVenta = 75.00m,
            RequiereReceta = false,
            StockMinimo = 20
        };

        // Act
        var resultado = await servicio.Guardar(nuevoMedicamento);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.Medicamentos.Count());

        var medicamentoGuardado = await context.Medicamentos.FirstAsync();
        Assert.Equal("Ibuprofeno 400mg", medicamentoGuardado.Nombre);
        Assert.Equal(75.00m, medicamentoGuardado.PrecioVenta);
    }

    [Fact]
    public async Task Eliminar_MedicamentoExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var categoria = new Categoria { Nombre = "Antibióticos" };
            var proveedor = new Proveedore { NombreEmpresa = "PharmaGlobal" };
            seedContext.Categorias.Add(categoria);
            seedContext.Proveedores.Add(proveedor);
            await seedContext.SaveChangesAsync();

            var medicamentoPrueba = new Medicamento
            {
                Nombre = "Amoxicilina",
                CategoriaId = categoria.CategoriaId,
                ProveedorId = proveedor.ProveedorId,
                PrecioCompra = 100m,
                PrecioVenta = 150m
            };
            seedContext.Medicamentos.Add(medicamentoPrueba);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new MedicamentoService(context);
        var medicamentoGuardado = await context.Medicamentos.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(medicamentoGuardado.MedicamentoId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.Medicamentos);
    }
}
