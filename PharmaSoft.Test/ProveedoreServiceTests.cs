using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class ProveedoreServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;
    }

    public ProveedoreServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoProveedor_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);
        var servicio = new ProveedoreService(contexto);

        var nuevoProveedor = new Proveedore
        {
            NombreEmpresa = "Distribuidora Nacional",
            Contacto = "Carlos Gómez",
            Telefono = "809-123-4567"
        };

        var resultado = await servicio.Guardar(nuevoProveedor);

        Assert.True(resultado);
        Assert.Equal(1, contexto.Proveedores.Count());
        Assert.Equal("Distribuidora Nacional", contexto.Proveedores.First().NombreEmpresa);
    }

    [Fact]
    public async Task Eliminar_ProveedorExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedorPrueba = new Proveedore { NombreEmpresa = "Laboratorios Z" };
        contexto.Proveedores.Add(proveedorPrueba);
        await contexto.SaveChangesAsync();

        var servicio = new ProveedoreService(contexto);

        var resultadoEliminar = await servicio.Eliminar(proveedorPrueba.ProveedorId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Proveedores);
    }
}