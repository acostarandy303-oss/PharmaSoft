using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class CategoriaServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        // GitHub Actions automáticamente establece esta variable en "true"
        bool enGitHub = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true";

        // Si estamos en GitHub usa LocalDB, si estamos en tu PC usa SqlExpress
        string servidor = enGitHub ? "(localdb)\\MSSQLLocalDB" : ".\\SqlExpress";

        string connectionString = $"Data Source={servidor};Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer(connectionString)
            .Options;
    }

    public CategoriaServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevaCategoria_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);
        var servicio = new CategoriaService(contexto);

        var nuevaCategoria = new Categoria
        {
            Nombre = "Vitaminas y Suplementos",
            Descripcion = "Complejos vitamínicos diarios"
        };

        var resultado = await servicio.Guardar(nuevaCategoria);

        Assert.True(resultado);
        Assert.Equal(1, contexto.Categorias.Count());
        Assert.Equal("Vitaminas y Suplementos", contexto.Categorias.First().Nombre);
    }

    [Fact]
    public async Task Eliminar_CategoriaExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var categoriaPrueba = new Categoria { Nombre = "Cuidado Personal" };
        contexto.Categorias.Add(categoriaPrueba);
        await contexto.SaveChangesAsync();

        var servicio = new CategoriaService(contexto);

        var resultadoEliminar = await servicio.Eliminar(categoriaPrueba.CategoriaId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Categorias);
    }
}