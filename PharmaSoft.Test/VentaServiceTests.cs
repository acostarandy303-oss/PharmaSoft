using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class VentaServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteVenta_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Ventas.Add(CreateVenta(id: 1, total: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.VentaId);
            Assert.Equal(100m, result.Total);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteVenta_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new VentaService(context);

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
                seedContext.Ventas.AddRange(
                    CreateVenta(id: 1, total: 50m),
                    CreateVenta(id: 2, total: 150m),
                    CreateVenta(id: 3, total: 200m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);

            // Act
            var result = await service.GetList(v => v.Total >= 100m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, v => v.VentaId == 2);
            Assert.Contains(result, v => v.VentaId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoVentaNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);
            var nuevaVenta = CreateVenta(id: 0, total: 300m);

            // Act
            var result = await service.Guardar(nuevaVenta);

            // Assert
            Assert.True(result);

            var saved = await context.Ventas.FirstOrDefaultAsync(v => v.VentaId == nuevaVenta.VentaId);
            Assert.NotNull(saved);
            Assert.Equal(300m, saved!.Total);
        }

        [Fact]
        public async Task Guardar_CuandoVentaExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Ventas.Add(CreateVenta(id: 20, total: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);

            var updated = CreateVenta(id: 20, total: 250m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Ventas.FirstOrDefaultAsync(v => v.VentaId == 20);
            Assert.NotNull(saved);
            Assert.Equal(250m, saved!.Total);
        }

        [Fact]
        public async Task Existe_CuandoVentaExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Ventas.Add(CreateVenta(id: 50, total: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteVenta_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Ventas.Add(CreateVenta(id: 80, total: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new VentaService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Ventas.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static Venta CreateVenta(int id, decimal total)
        {
            return new Venta
            {
                VentaId = id,
                Total = total,
                FechaVenta = DateTime.Now,
                MetodoPago = "Efectivo"
            };
        }
    }
}

