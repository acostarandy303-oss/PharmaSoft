using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class ClienteServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;
    }

    public ClienteServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoCliente_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);
        var servicio = new ClienteService(contexto);

        var nuevoCliente = new Cliente
        {
            Nombre = "Juan Pérez",
            RncCedula = "402-0000000-1",
            Telefono = "809-555-5555",
            LimiteCredito = 5000.00m
        };

        var resultado = await servicio.Guardar(nuevoCliente);

        Assert.True(resultado);
        Assert.Equal(1, contexto.Clientes.Count());
        Assert.Equal("Juan Pérez", contexto.Clientes.First().Nombre);
    }

    [Fact]
    public async Task Eliminar_ClienteExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var clientePrueba = new Cliente { Nombre = "Maria Lopez", RncCedula = "001-0000000-2" };
        contexto.Clientes.Add(clientePrueba);
        await contexto.SaveChangesAsync();

        var servicio = new ClienteService(contexto);

        var resultadoEliminar = await servicio.Eliminar(clientePrueba.ClienteId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Clientes);
    }
}