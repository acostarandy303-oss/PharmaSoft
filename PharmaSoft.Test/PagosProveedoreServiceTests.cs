using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class PagosProveedoreServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExistePago_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosProveedores.Add(CreatePago(id: 1, cxpId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.PagoProveedorId);
            Assert.Equal(50m, result.MontoPagado);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExistePago_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new PagosProveedoreService(context);

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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosProveedores.AddRange(
                    CreatePago(id: 1, cxpId: 1, monto: 10m),
                    CreatePago(id: 2, cxpId: 1, monto: 20m),
                    CreatePago(id: 3, cxpId: 1, monto: 30m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);

            // Act
            var result = await service.GetList(p => p.MontoPagado >= 20m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.PagoProveedorId == 2);
            Assert.Contains(result, p => p.PagoProveedorId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoPagoNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);
            var nuevo = CreatePago(id: 0, cxpId: 1, monto: 25m);

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.PagosProveedores.FirstOrDefaultAsync(p => p.PagoProveedorId == nuevo.PagoProveedorId);
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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosProveedores.Add(CreatePago(id: 20, cxpId: 1, monto: 15m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);

            var updated = CreatePago(id: 20, cxpId: 1, monto: 35m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.PagosProveedores.FirstOrDefaultAsync(p => p.PagoProveedorId == 20);
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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosProveedores.Add(CreatePago(id: 50, cxpId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);

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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "P1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(new CuentasPorPagar { CxPid = 1, CompraId = 1, ProveedorId = 1, MontoInicial = 100m, SaldoPendiente = 50m });
                seedContext.PagosProveedores.Add(CreatePago(id: 80, cxpId: 1, monto: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new PagosProveedoreService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.PagosProveedores.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static PagosProveedore CreatePago(int id, int cxpId, decimal monto)
        {
            return new PagosProveedore
            {
                PagoProveedorId = id,
                CxPid = cxpId,
                MontoPagado = monto,
                FechaPago = DateTime.Now,
                MetodoPago = "Efectivo"
            };
        }
    }
}

