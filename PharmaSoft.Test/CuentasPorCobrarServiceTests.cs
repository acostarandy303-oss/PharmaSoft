using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class CuentasPorCobrarServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteCxC_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(CreateCxc(id: 1, ventaId: 1, clienteId: 1, saldo: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.CxCid);
            Assert.Equal(50m, result.SaldoPendiente);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteCxC_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new CuentasPorCobrarService(context);

            // Act
            var result = await service.Buscar(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_CuandoSeFiltraPorSaldo_RetornaCoincidencias()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.AddRange(
                    CreateCxc(id: 1, ventaId: 1, clienteId: 1, saldo: 0m),
                    CreateCxc(id: 2, ventaId: 1, clienteId: 1, saldo: 50m),
                    CreateCxc(id: 3, ventaId: 1, clienteId: 1, saldo: 100m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);

            // Act
            var result = await service.GetList(c => c.SaldoPendiente > 0m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CxCid == 2);
            Assert.Contains(result, c => c.CxCid == 3);
        }

        [Fact]
        public async Task Guardar_CuandoCxCNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);
            var nuevaCxC = CreateCxc(id: 0, ventaId: 1, clienteId: 1, saldo: 50m);

            // Act
            var result = await service.Guardar(nuevaCxC);

            // Assert
            Assert.True(result);

            var saved = await context.CuentasPorCobrars.FirstOrDefaultAsync(c => c.CxCid == nuevaCxC.CxCid);
            Assert.NotNull(saved);
            Assert.Equal(50m, saved!.SaldoPendiente);
        }

        [Fact]
        public async Task Guardar_CuandoCxCExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(CreateCxc(id: 20, ventaId: 1, clienteId: 1, saldo: 150m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);

            var updated = CreateCxc(id: 20, ventaId: 1, clienteId: 1, saldo: 75m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.CuentasPorCobrars.FirstOrDefaultAsync(c => c.CxCid == 20);
            Assert.NotNull(saved);
            Assert.Equal(75m, saved!.SaldoPendiente);
        }

        [Fact]
        public async Task Existe_CuandoCxCExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(CreateCxc(id: 50, ventaId: 1, clienteId: 1, saldo: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteCxC_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(CreateCxc(id: 80, ventaId: 1, clienteId: 1, saldo: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorCobrarService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.CuentasPorCobrars.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static CuentasPorCobrar CreateCxc(int id, int ventaId, int clienteId, decimal saldo)
        {
            return new CuentasPorCobrar
            {
                CxCid = id,
                VentaId = ventaId,
                ClienteId = clienteId,
                MontoInicial = saldo + 50m,
                SaldoPendiente = saldo,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                Estado = "Pendiente"
            };
        }
    }
}

