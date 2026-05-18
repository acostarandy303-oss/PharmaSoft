using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class CuentasPorPagarServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteCxP_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(CreateCxp(id: 1, compraId: 1, proveedorId: 1, saldo: 50m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.CxPid);
            Assert.Equal(50m, result.SaldoPendiente);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteCxP_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new CuentasPorPagarService(context);

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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.AddRange(
                    CreateCxp(id: 1, compraId: 1, proveedorId: 1, saldo: 0m),
                    CreateCxp(id: 2, compraId: 1, proveedorId: 1, saldo: 50m),
                    CreateCxp(id: 3, compraId: 1, proveedorId: 1, saldo: 100m)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);

            // Act
            var result = await service.GetList(c => c.SaldoPendiente > 0m);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CxPid == 2);
            Assert.Contains(result, c => c.CxPid == 3);
        }

        [Fact]
        public async Task Guardar_CuandoCxPNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);
            var nuevaCxP = CreateCxp(id: 0, compraId: 1, proveedorId: 1, saldo: 200m);

            // Act
            var result = await service.Guardar(nuevaCxP);

            // Assert
            Assert.True(result);

            var saved = await context.CuentasPorPagars.FirstOrDefaultAsync(c => c.CxPid == nuevaCxP.CxPid);
            Assert.NotNull(saved);
            Assert.Equal(200m, saved!.SaldoPendiente);
        }

        [Fact]
        public async Task Guardar_CuandoCxPExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(CreateCxp(id: 20, compraId: 1, proveedorId: 1, saldo: 150m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);

            var updated = CreateCxp(id: 20, compraId: 1, proveedorId: 1, saldo: 75m);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.CuentasPorPagars.FirstOrDefaultAsync(c => c.CxPid == 20);
            Assert.NotNull(saved);
            Assert.Equal(75m, saved!.SaldoPendiente);
        }

        [Fact]
        public async Task Existe_CuandoCxPExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(CreateCxp(id: 50, compraId: 1, proveedorId: 1, saldo: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteCxP_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.CuentasPorPagars.Add(CreateCxp(id: 80, compraId: 1, proveedorId: 1, saldo: 100m));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CuentasPorPagarService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.CuentasPorPagars.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static CuentasPorPagar CreateCxp(int id, int compraId, int proveedorId, decimal saldo)
        {
            return new CuentasPorPagar
            {
                CxPid = id,
                CompraId = compraId,
                ProveedorId = proveedorId,
                MontoInicial = saldo + 50m,
                SaldoPendiente = saldo,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                Estado = "Pendiente"
            };
        }
    }
}

