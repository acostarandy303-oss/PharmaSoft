using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class CompraServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones()
    {
        return new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;
    }

    public CompraServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevaCompra_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        // 1. Crear dependencia (Proveedor)
        var proveedor = new Proveedore { NombreEmpresa = "Distribuidora Salud" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var servicio = new CompraService(contexto);
        var nuevaCompra = new Compra
        {
            ProveedorId = proveedor.ProveedorId,
            FechaCompra = DateTime.Now,
            TotalCompra = 15000.50m,
            TipoPago = "Crédito",
            NumeroFacturaProveedor = "FACT-001"
        };

        var resultado = await servicio.Guardar(nuevaCompra);

        Assert.True(resultado);
        Assert.Equal(1, contexto.Compras.Count());
        Assert.Equal(15000.50m, contexto.Compras.First().TotalCompra);
    }

    [Fact]
    public async Task Eliminar_CompraExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var proveedor = new Proveedore { NombreEmpresa = "Laboratorios X" };
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var compraPrueba = new Compra
        {
            ProveedorId = proveedor.ProveedorId,
            TotalCompra = 5000m
        };
        contexto.Compras.Add(compraPrueba);
        await contexto.SaveChangesAsync();

        var servicio = new CompraService(contexto);

        var resultadoEliminar = await servicio.Eliminar(compraPrueba.CompraId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Compras);
    }
}