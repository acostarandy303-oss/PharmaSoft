using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class LotesInventarioServiceTests
{
    private DbContextOptions<PharmaContext> CrearOpciones() =>
        new DbContextOptionsBuilder<PharmaContext>()
            .UseSqlServer("Data Source=.\\SqlExpress;Database=PharmaDb_Tests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;")
            .Options;

    public LotesInventarioServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoLote_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        // Dependencias en cascada
        var categoria = new Categoria { Nombre = "Gripales" };
        var proveedor = new Proveedore { NombreEmpresa = "FarmaRD" };
        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var medicamento = new Medicamento { Nombre = "Antigripal", CategoriaId = categoria.CategoriaId, ProveedorId = proveedor.ProveedorId, PrecioCompra = 10, PrecioVenta = 20 };
        contexto.Medicamentos.Add(medicamento);
        await contexto.SaveChangesAsync();

        var servicio = new LotesInventarioService(contexto);
        var nuevoLote = new LotesInventario
        {
            MedicamentoId = medicamento.MedicamentoId,
            NumeroLote = "LT-2026",
            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            CantidadActual = 100
        };

        var resultado = await servicio.Guardar(nuevoLote);

        Assert.True(resultado);
        Assert.Equal(1, contexto.LotesInventarios.Count());
        Assert.Equal("LT-2026", contexto.LotesInventarios.First().NumeroLote);
    }

    [Fact]
    public async Task Eliminar_LoteExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var categoria = new Categoria { Nombre = "Cat" };
        var proveedor = new Proveedore { NombreEmpresa = "Prov" };
        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var medicamento = new Medicamento { Nombre = "Med", CategoriaId = categoria.CategoriaId, ProveedorId = proveedor.ProveedorId, PrecioCompra = 1, PrecioVenta = 2 };
        contexto.Medicamentos.Add(medicamento);
        await contexto.SaveChangesAsync();

        var lote = new LotesInventario { MedicamentoId = medicamento.MedicamentoId, NumeroLote = "001", FechaVencimiento = DateOnly.FromDateTime(DateTime.Now), CantidadActual = 10 };
        contexto.LotesInventarios.Add(lote);
        await contexto.SaveChangesAsync();

        var servicio = new LotesInventarioService(contexto);
        var resultadoEliminar = await servicio.Eliminar(lote.LoteId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.LotesInventarios);
    }
}