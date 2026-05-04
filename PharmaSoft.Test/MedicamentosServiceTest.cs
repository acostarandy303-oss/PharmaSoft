using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Tests;

public class MedicamentoServiceTests
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

    // Constructor de la clase de pruebas
    public MedicamentoServiceTests()
    {
        // Esto se ejecuta automáticamente antes de cada [Fact].
        using var contexto = new PharmaContext(CrearOpciones());

        // ¡Atención! EnsureDeleted borra la base de datos de pruebas entera para empezar limpio.
        // Asegúrate de que la cadena de conexión apunte a PharmaDb_Tests y NO a la real.
        contexto.Database.EnsureDeleted();

        // EnsureCreated crea la base de datos y todas sus tablas basándose en tus modelos.
        contexto.Database.EnsureCreated();
    }

    [Fact]
    public async Task Guardar_NuevoMedicamento_DebeInsertarCorrectamente()
    {
        // 1. Arrange (Preparar)
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        // Primero necesitamos crear la Categoria y el Proveedor para que no falle la relación
        var categoria = new Categoria { Nombre = "Analgésicos" };
        var proveedor = new Proveedore { NombreEmpresa = "FarmaCorp" };

        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var servicio = new MedicamentoService(contexto);
        var nuevoMedicamento = new Medicamento
        {
            Nombre = "Ibuprofeno 400mg",
            PrincipioActivo = "Ibuprofeno",
            CategoriaId = categoria.CategoriaId, // Ahora tomará el ID real de SQL Server
            ProveedorId = proveedor.ProveedorId, // Ahora tomará el ID real de SQL Server
            PrecioCompra = 50.00m,
            PrecioVenta = 75.00m,
            RequiereReceta = false,
            StockMinimo = 20
        };

        // 2. Act (Actuar)
        var resultado = await servicio.Guardar(nuevoMedicamento);

        // 3. Assert (Afirmar)
        Assert.True(resultado);
        Assert.Equal(1, contexto.Medicamentos.Count());

        var medicamentoGuardado = contexto.Medicamentos.First();
        Assert.Equal("Ibuprofeno 400mg", medicamentoGuardado.Nombre);
        Assert.Equal(75.00m, medicamentoGuardado.PrecioVenta);
    }

    [Fact]
    public async Task Eliminar_MedicamentoExistente_DebeRetornarTrue()
    {
        // 1. Arrange
        var opciones = CrearOpciones();
        using var contexto = new PharmaContext(opciones);

        // Como la BD se borra y se crea en el constructor, debemos crear las dependencias de nuevo aquí
        var categoria = new Categoria { Nombre = "Antibióticos" };
        var proveedor = new Proveedore { NombreEmpresa = "PharmaGlobal" };
        contexto.Categorias.Add(categoria);
        contexto.Proveedores.Add(proveedor);
        await contexto.SaveChangesAsync();

        var medicamentoPrueba = new Medicamento
        {
            Nombre = "Amoxicilina",
            CategoriaId = categoria.CategoriaId,
            ProveedorId = proveedor.ProveedorId,
            PrecioCompra = 100m,
            PrecioVenta = 150m
        };
        contexto.Medicamentos.Add(medicamentoPrueba);
        await contexto.SaveChangesAsync();

        var servicio = new MedicamentoService(contexto);

        // 2. Act
        // Usamos el ID real generado por SQL Server
        var resultadoEliminar = await servicio.Eliminar(medicamentoPrueba.MedicamentoId);

        // 3. Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(contexto.Medicamentos);
    }
}