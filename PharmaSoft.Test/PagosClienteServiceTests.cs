using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class PagosClienteServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExistePago_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosClientes.Add(CreatePago(id: 1, cxcId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.PagoClienteId);
            Assert.Equal(50m, result.MontoPagado);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExistePago_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new PagosClienteService(context);

            // Act
            var result = await service.Buscar(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_CuandoSeFiltraPorMonto_RetornaCoincidencias()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosClientes.AddRange(
                    CreatePago(id: 1, cxcId: 1, monto: 10m),
                    CreatePago(id: 2, cxcId: 1, monto: 20m),
                    CreatePago(id: 3, cxcId: 1, monto: 30m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);

            // Act
            var result = await service.GetList(p => p.MontoPagado >= 20m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.PagoClienteId == 2);
            Assert.Contains(result, p => p.PagoClienteId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoPagoNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);
            var nuevo = CreatePago(id: 0, cxcId: 1, monto: 25m);

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.PagosClientes.FirstOrDefaultAsync(p => p.PagoClienteId == nuevo.PagoClienteId);
            Assert.NotNull(saved);
            Assert.Equal(25m, saved!.MontoPagado);
        }

        [Fact]
        public async Task Guardar_CuandoPagoExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosClientes.Add(CreatePago(id: 20, cxcId: 1, monto: 15m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);

            var updated = CreatePago(id: 20, cxcId: 1, monto: 35m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.PagosClientes.FirstOrDefaultAsync(p => p.PagoClienteId == 20);
            Assert.NotNull(saved);
            Assert.Equal(35m, saved!.MontoPagado);
        }

        [Fact]
        public async Task Existe_CuandoPagoExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosClientes.Add(CreatePago(id: 50, cxcId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExistePago_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.CuentasPorCobrars.Add(new CuentasPorCobrar { CxCid = 1, VentaId = 1, ClienteId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosClientes.Add(CreatePago(id: 80, cxcId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosClienteService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.PagosClientes.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static PagosCliente CreatePago(int id, int cxcId, decimal monto)
        {
            return new PagosCliente
            {
                PagoClienteId = id,
                CxCid = cxcId,
                MontoPagado = monto,
                FechaPago = DateTime.Now,
                MetodoPago = "Efectivo"
            };
        }
    }
}

