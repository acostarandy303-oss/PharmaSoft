using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test;

public class ProveedoreServiceTests
{
    [Fact]
    public async Task Guardar_NuevoProveedor_DebeInsertarCorrectamente()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            // seed nothing; service will insert
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new ProveedoreService(context);

        var nuevoProveedor = new Proveedore
        {
            NombreEmpresa = "Distribuidora Nacional",
            Contacto = "Carlos Gómez",
            Telefono = "809-123-4567"
        };

        // Act
        var resultado = await servicio.Guardar(nuevoProveedor);

        // Assert
        Assert.True(resultado);
        Assert.Equal(1, context.Proveedores.Count());
        Assert.Equal("Distribuidora Nacional", context.Proveedores.First().NombreEmpresa);
    }

    [Fact]
    public async Task Eliminar_ProveedorExistente_DebeRetornarTrue()
    {
        // Arrange
        var dbName = TestDbContextFactory.NewDataBaseName();
        await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
        {
            var proveedorPrueba = new Proveedore { NombreEmpresa = "Laboratorios Z" };
            seedContext.Proveedores.Add(proveedorPrueba);
            await seedContext.SaveChangesAsync();
        }

        await using var context = TestDbContextFactory.CreateContext(dbName);
        var servicio = new ProveedoreService(context);
        var proveedorGuardado = await context.Proveedores.FirstAsync();

        // Act
        var resultadoEliminar = await servicio.Eliminar(proveedorGuardado.ProveedorId);

        // Assert
        Assert.True(resultadoEliminar);
        Assert.Empty(context.Proveedores);
    }
}
