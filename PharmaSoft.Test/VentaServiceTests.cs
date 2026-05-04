using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class VentaServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;
    }

    public VentaServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevaVenta_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);
        var servicio = new VentaService(contexto);

        var nuevaVenta = new Venta
        {
            FechaVenta = DateTime.Now,
            Total = 1250.75m,
            MetodoPago = "Efectivo"
        };

        var resultado = await servicio.Guardar(nuevaVenta);

        Assert.True(resultado);
        Assert.Equal(1, contexto.Ventas.Count());
        Assert.Equal(1250.75m, contexto.Ventas.First().Total);
    }

    [Fact]
    public async Task Eliminar_VentaExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var ventaPrueba = new Venta { Total = 300m, MetodoPago = "Tarjeta" };
        contexto.Ventas.Add(ventaPrueba);
        await contexto.SaveChangesAsync();

        var servicio = new VentaService(contexto);

        var resultadoEliminar = await servicio.Eliminar(ventaPrueba.VentaId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Ventas);
    }
}