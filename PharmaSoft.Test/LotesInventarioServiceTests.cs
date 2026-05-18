using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class LotesInventarioServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteLote_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(CreateLote(id: 1, medicamentoId: 1, numeroLote: "LOTE1", cantidad: 50));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.LoteId);
            Assert.Equal("LOTE1", result.NumeroLote);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteLote_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new LotesInventarioService(context);

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
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.AddRange(
                    CreateLote(id: 1, medicamentoId: 1, numeroLote: "L1", cantidad: 0),
                    CreateLote(id: 2, medicamentoId: 1, numeroLote: "L2", cantidad: 10),
                    CreateLote(id: 3, medicamentoId: 1, numeroLote: "L3", cantidad: 20)
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);

            // Act
            var result = await service.GetList(l => l.CantidadActual > 0);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, l => l.LoteId == 2);
            Assert.Contains(result, l => l.LoteId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoLoteNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);
            var nuevo = CreateLote(id: 0, medicamentoId: 1, numeroLote: "L-NUEVO", cantidad: 100);

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.LotesInventarios.FirstOrDefaultAsync(l => l.LoteId == nuevo.LoteId);
            Assert.NotNull(saved);
            Assert.Equal("L-NUEVO", saved!.NumeroLote);
        }

        [Fact]
        public async Task Guardar_CuandoLoteExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(CreateLote(id: 20, medicamentoId: 1, numeroLote: "L-20", cantidad: 50));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);

            var updated = CreateLote(id: 20, medicamentoId: 1, numeroLote: "L-20", cantidad: 25);

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.LotesInventarios.FirstOrDefaultAsync(l => l.LoteId == 20);
            Assert.NotNull(saved);
            Assert.Equal(25, saved!.CantidadActual);
        }

        [Fact]
        public async Task Existe_CuandoLoteExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(CreateLote(id: 50, medicamentoId: 1, numeroLote: "L-50", cantidad: 50));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteLote_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(new Medicamento { MedicamentoId = 1, CategoriaId = 1, ProveedorId = 1, Nombre = "Med1", PrecioCompra = 10m, PrecioVenta = 15m });
                seedContext.LotesInventarios.Add(CreateLote(id: 80, medicamentoId: 1, numeroLote: "L-80", cantidad: 50));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new LotesInventarioService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.LotesInventarios.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static LotesInventario CreateLote(int id, int medicamentoId, string numeroLote, int cantidad)
        {
            return new LotesInventario
            {
                LoteId = id,
                MedicamentoId = medicamentoId,
                NumeroLote = numeroLote,
                CantidadActual = cantidad,
                FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                FechaIngreso = DateTime.Now
            };
        }
    }
}

