using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class CategoriaServiceTests
{
    [Fact]
    public async Task Buscar_CuandoExisteCategoria_RetornaEntidad()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            seedContext.Categorias.Add(CreateCategoria(id: 1, nombre: "Analgésicos"));
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var service = new CategoriaService(context);

        // Act
        var result = await service.Buscar(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result!.CategoriaId);
        Assert.Equal("Analgésicos", result.Nombre);
        Assert.Empty(context.ChangeTracker.Entries());
    }

    [Fact]
    public async Task Buscar_CuandoNoExisteCategoria_RetornaNull()
    {
        // Arrange
        await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
        var service = new CategoriaService(context);

        // Act
        var result = await service.Buscar(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Listar_CuandoSeFiltraPorNombre_RetornaCoincidencias()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            seedContext.Categorias.AddRange(
                CreateCategoria(id: 1, nombre: "Analgésicos"),
                CreateCategoria(id: 2, nombre: "Antiinflamatorios"),
                CreateCategoria(id: 3, nombre: "Antibióticos")
            );
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var service = new CategoriaService(context);

        // Act
        var result = await service.GetList(c => c.Nombre.StartsWith("Anti"));

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.CategoriaId == 2);
        Assert.Contains(result, c => c.CategoriaId == 3);
    }

    [Fact]
    public async Task Guardar_CuandoCategoriaNoExiste_InsertaYRetornaTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using var context = TestDbContextFactory.CreateContext(dbName);
        var service = new CategoriaService(context);
        var nuevaCategoria = CreateCategoria(id: 0, nombre: "Vitaminas"); // ID 0 para insertar

        // Act
        var result = await service.Guardar(nuevaCategoria);

        // Assert
        Assert.True(result);
        var saved = await context.Categorias.FirstOrDefaultAsync(c => c.Nombre == "Vitaminas");
        Assert.NotNull(saved);
        Assert.Equal("Vitaminas", saved!.Nombre);
    }

    [Fact]
    public async Task Guardar_CuandoCategoriaExiste_ModificaYRetornaTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            seedContext.Categorias.Add(CreateCategoria(id: 20, nombre: "Original"));
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var service = new CategoriaService(context);
        var updated = CreateCategoria(id: 20, nombre: "Modificado");

        // Act
        var result = await service.Guardar(updated);

        // Assert
        Assert.True(result);
        var saved = await context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == 20);
        Assert.NotNull(saved);
        Assert.Equal("Modificado", saved!.Nombre);
    }

    [Fact]
    public async Task Eliminar_CuandoExisteCategoria_LoBorraYRetornaTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            seedContext.Categorias.Add(CreateCategoria(id: 30, nombre: "ParaBorrar"));
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var service = new CategoriaService(context);

        // Act
        var result = await service.Eliminar(30);

        // Assert
        Assert.True(result);
        var eliminado = await context.Categorias.FindAsync(30);
        Assert.Null(eliminado);
    }

    private static Categoria CreateCategoria(int id, string nombre)
    {
        return new Categoria
        {
            CategoriaId = id,
            Nombre = nombre,
            Descripcion = "Descripcion de prueba"
        };
    }
}