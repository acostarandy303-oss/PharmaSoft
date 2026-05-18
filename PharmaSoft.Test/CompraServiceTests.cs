using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class CompraServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteCompra_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(CreateCompra(id: 1, proveedorId: 1, total: 500m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.CompraId);
            Assert.Equal(500m, result.TotalCompra);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteCompra_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new CompraService(context);

            // Act
            var result = await service.Buscar(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_CuandoSeFiltraPorTotal_RetornaCoincidencias()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.AddRange(
                    CreateCompra(id: 1, proveedorId: 1, total: 100m),
                    CreateCompra(id: 2, proveedorId: 1, total: 200m),
                    CreateCompra(id: 3, proveedorId: 1, total: 300m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);

            // Act
            var result = await service.GetList(c => c.TotalCompra >= 200m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CompraId == 2);
            Assert.Contains(result, c => c.CompraId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoCompraNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);
            var nuevaCompra = CreateCompra(id: 0, proveedorId: 1, total: 300m);

            // Act
            var result = await service.Guardar(nuevaCompra);

            // Assert
            Assert.True(result);

            var saved = await context.Compras.FirstOrDefaultAsync(c => c.CompraId == nuevaCompra.CompraId);
            Assert.NotNull(saved);
            Assert.Equal(300m, saved!.TotalCompra);
        }

        [Fact]
        public async Task Guardar_CuandoCompraExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(CreateCompra(id: 20, proveedorId: 1, total: 1000m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);

            var updated = CreateCompra(id: 20, proveedorId: 1, total: 2000m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Compras.FirstOrDefaultAsync(c => c.CompraId == 20);
            Assert.NotNull(saved);
            Assert.Equal(2000m, saved!.TotalCompra);
        }

        [Fact]
        public async Task Existe_CuandoCompraExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(CreateCompra(id: 50, proveedorId: 1, total: 500m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteCompra_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(CreateCompra(id: 80, proveedorId: 1, total: 500m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CompraService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Compras.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static Compra CreateCompra(int id, int proveedorId, decimal total)
        {
            return new Compra
            {
                CompraId = id,
                ProveedorId = proveedorId,
                TotalCompra = total,
                FechaCompra = DateTime.Now,
                TipoPago = "Efectivo",
                NumeroFacturaProveedor = "FAC-" + id
            };
        }
    }
}

