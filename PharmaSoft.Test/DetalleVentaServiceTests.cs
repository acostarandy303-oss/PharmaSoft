using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class DetalleVentaServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteDetalleVenta_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                seedContext.DetalleVentas.Add(CreateDetalleVenta(id: 1, ventaId: 1, loteId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.DetalleId);
            Assert.Equal(5, result.Cantidad);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteDetalleVenta_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new DetalleVentaService(context);

            // Act
            var result = await service.Buscar(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_CuandoSeFiltraPorCantidad_RetornaCoincidencias()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                seedContext.DetalleVentas.AddRange(
                    CreateDetalleVenta(id: 1, ventaId: 1, loteId: 1, cantidad: 2),
                    CreateDetalleVenta(id: 2, ventaId: 1, loteId: 1, cantidad: 5),
                    CreateDetalleVenta(id: 3, ventaId: 1, loteId: 1, cantidad: 10)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);

            // Act
            var result = await service.GetList(d => d.Cantidad >= 5);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, d => d.DetalleId == 2);
            Assert.Contains(result, d => d.DetalleId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoDetalleVentaNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);
            var nuevo = CreateDetalleVenta(id: 0, ventaId: 1, loteId: 1, cantidad: 20);

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.DetalleVentas.FirstOrDefaultAsync(d => d.DetalleId == nuevo.DetalleId);
            Assert.NotNull(saved);
            Assert.Equal(20, saved!.Cantidad);
        }

        [Fact]
        public async Task Guardar_CuandoDetalleVentaExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                seedContext.DetalleVentas.Add(CreateDetalleVenta(id: 20, ventaId: 1, loteId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);

            var updated = CreateDetalleVenta(id: 20, ventaId: 1, loteId: 1, cantidad: 15);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.DetalleVentas.FirstOrDefaultAsync(d => d.DetalleId == 20);
            Assert.NotNull(saved);
            Assert.Equal(15, saved!.Cantidad);
        }

        [Fact]
        public async Task Existe_CuandoDetalleVentaExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                seedContext.DetalleVentas.Add(CreateDetalleVenta(id: 50, ventaId: 1, loteId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteDetalleVenta_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(new Cliente { ClienteId = 1, Nombre = "C1" });
                seedContext.Ventas.Add(new Venta { VentaId = 1, Total = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(new LotesInventario { LoteId = 1, MedicamentoId = 1, NumeroLote = "L1", CantidadActual = 100, FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) });
                seedContext.DetalleVentas.Add(CreateDetalleVenta(id: 80, ventaId: 1, loteId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleVentaService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.DetalleVentas.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static DetalleVenta CreateDetalleVenta(int id, int ventaId, int loteId, int cantidad)
        {
            return new DetalleVenta
            {
                DetalleId = id,
                VentaId = ventaId,
                LoteId = loteId,
                Cantidad = cantidad,
                PrecioUnitario = 15m,
                Subtotal = 15m * cantidad
            };
        }
    }
}
