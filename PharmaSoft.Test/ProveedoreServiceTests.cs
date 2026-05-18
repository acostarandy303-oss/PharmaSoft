using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class ProveedoreServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteProveedor_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(CreateProveedor(id: 1, nombreEmpresa: "FarmaCorp"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.ProveedorId);
            Assert.Equal("FarmaCorp", result.NombreEmpresa);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteProveedor_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new ProveedoreService(context);

            // Act
            var result = await service.Buscar(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_CuandoSeFiltraPorNombre_RetornaCoincidencias()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.AddRange(
                    CreateProveedor(id: 1, nombreEmpresa: "FarmaCorp"),
                    CreateProveedor(id: 2, nombreEmpresa: "PharmaGlobal"),
                    CreateProveedor(id: 3, nombreEmpresa: "BioSalud")
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);

            // Act
            var result = await service.GetList(p => p.NombreEmpresa.StartsWith("Pharma"));

            // Assert
            Assert.Single(result);
            Assert.Contains(result, p => p.ProveedorId == 2);
        }

        [Fact]
        public async Task Guardar_CuandoProveedorNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);
            var nuevo = CreateProveedor(id: 0, nombreEmpresa: "Nuevo Proveedor");

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.Proveedores.FirstOrDefaultAsync(p => p.ProveedorId == nuevo.ProveedorId);
            Assert.NotNull(saved);
            Assert.Equal("Nuevo Proveedor", saved!.NombreEmpresa);
        }

        [Fact]
        public async Task Guardar_CuandoProveedorExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(CreateProveedor(id: 20, nombreEmpresa: "Original"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);

            var updated = CreateProveedor(id: 20, nombreEmpresa: "Modificado");

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Proveedores.FirstOrDefaultAsync(p => p.ProveedorId == 20);
            Assert.NotNull(saved);
            Assert.Equal("Modificado", saved!.NombreEmpresa);
        }

        [Fact]
        public async Task Existe_CuandoProveedorExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(CreateProveedor(id: 50, nombreEmpresa: "Prueba"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteProveedor_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(CreateProveedor(id: 80, nombreEmpresa: "Para Borrar"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ProveedoreService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Proveedores.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static Proveedore CreateProveedor(int id, string nombreEmpresa)
        {
            return new Proveedore
            {
                ProveedorId = id,
                NombreEmpresa = nombreEmpresa,
                Contacto = "Contacto",
                Telefono = "8091112222",
                Email = "contacto@proveedor.com"
            };
        }
    }
}

