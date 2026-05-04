using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class DetalleVentaServiceTests
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
    public DetalleVentaServiceTests()
    {
        using var contexto = new PharmaContext(CrearOpciones());
        contexto.Database.EnsureDeleted();
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoDetalle_DebeInsertarCorrectamente()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var categoria = new Categoria { Nombre = "Cat" };
        var proveedor = new Proveedore { NombreEmpresa = "Prov" };
        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var medicamento = new Medicamento { Nombre = "Med", CategoriaId = categoria.CategoriaId, ProveedorId = proveedor.ProveedorId, PrecioCompra = 5, PrecioVenta = 10 };
        var venta = new Venta { Total = 20m };
        contexto.Medicamentos.Add(medicamento);
        contexto.Ventas.Add(venta);
        await contexto.SaveChangesAsync();

        var lote = new LotesInventario { MedicamentoId = medicamento.MedicamentoId, NumeroLote = "L1", CantidadActual = 50, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.LotesInventarios.Add(lote);
        await contexto.SaveChangesAsync();

        var servicio = new DetalleVentaService(contexto);
        var detalle = new DetalleVenta
        {
            VentaId = venta.VentaId,
            LoteId = lote.LoteId,
            Cantidad = 2,
            PrecioUnitario = 10m,
            Subtotal = 20m
        };

        var resultado = await servicio.Guardar(detalle);

        Assert.True(resultado);
        Assert.Equal(1, contexto.DetalleVentas.Count());
        Assert.Equal(20m, contexto.DetalleVentas.First().Subtotal);
    }

    [Fact]
    public async Task Eliminar_DetalleExistente_DebeRetornarTrue()
    {
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        var categoria = new Categoria { Nombre = "C" };
        var proveedor = new Proveedore { NombreEmpresa = "P" };
        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var medicamento = new Medicamento { Nombre = "M", CategoriaId = categoria.CategoriaId, ProveedorId = proveedor.ProveedorId, PrecioCompra = 1, PrecioVenta = 2 };
        var venta = new Venta { Total = 2 };
        contexto.Medicamentos.Add(medicamento);
        contexto.Ventas.Add(venta);
        await contexto.SaveChangesAsync();

        var lote = new LotesInventario { MedicamentoId = medicamento.MedicamentoId, NumeroLote = "L", CantidadActual = 10, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now) };
        contexto.LotesInventarios.Add(lote);
        await contexto.SaveChangesAsync();

        var detalle = new DetalleVenta { VentaId = venta.VentaId, LoteId = lote.LoteId, Cantidad = 1, PrecioUnitario = 2, Subtotal = 2 };
        contexto.DetalleVentas.Add(detalle);
        await contexto.SaveChangesAsync();

        var servicio = new DetalleVentaService(contexto);
        var resultadoEliminar = await servicio.Eliminar(detalle.DetalleId);

        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.DetalleVentas);
    }
}