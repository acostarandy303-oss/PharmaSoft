using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class DetalleCompraServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteDetalleCompra_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.DetalleCompras.Add(CreateDetalleCompra(id: 1, compraId: 1, medicamentoId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.DetalleCompraId);
            Assert.Equal(5, result.Cantidad);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteDetalleCompra_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new DetalleCompraService(context);

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
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.DetalleCompras.AddRange(
                    CreateDetalleCompra(id: 1, compraId: 1, medicamentoId: 1, cantidad: 2),
                    CreateDetalleCompra(id: 2, compraId: 1, medicamentoId: 1, cantidad: 5),
                    CreateDetalleCompra(id: 3, compraId: 1, medicamentoId: 1, cantidad: 10)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);

            // Act
            var result = await service.GetList(d => d.Cantidad >= 5);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, d => d.DetalleCompraId == 2);
            Assert.Contains(result, d => d.DetalleCompraId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoDetalleCompraNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);
            var nuevo = CreateDetalleCompra(id: 0, compraId: 1, medicamentoId: 1, cantidad: 20);

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.DetalleCompras.FirstOrDefaultAsync(d => d.DetalleCompraId == nuevo.DetalleCompraId);
            Assert.NotNull(saved);
            Assert.Equal(20, saved!.Cantidad);
        }

        [Fact]
        public async Task Guardar_CuandoDetalleCompraExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.DetalleCompras.Add(CreateDetalleCompra(id: 20, compraId: 1, medicamentoId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);

            var updated = CreateDetalleCompra(id: 20, compraId: 1, medicamentoId: 1, cantidad: 15);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.DetalleCompras.FirstOrDefaultAsync(d => d.DetalleCompraId == 20);
            Assert.NotNull(saved);
            Assert.Equal(15, saved!.Cantidad);
        }

        [Fact]
        public async Task Existe_CuandoDetalleCompraExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.DetalleCompras.Add(CreateDetalleCompra(id: 50, compraId: 1, medicamentoId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteDetalleCompra_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Compras.Add(new Compra { CompraId = 1, ProveedorId = 1, TotalCompra = 100m });
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.DetalleCompras.Add(CreateDetalleCompra(id: 80, compraId: 1, medicamentoId: 1, cantidad: 5));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new DetalleCompraService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.DetalleCompras.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static DetalleCompra CreateDetalleCompra(int id, int compraId, int medicamentoId, int cantidad)
        {
            return new DetalleCompra
            {
                DetalleCompraId = id,
                CompraId = compraId,
                MedicamentoId = medicamentoId,
                Cantidad = cantidad,
                PrecioCostoUnitario = 10m
            };
        }
    }
}

