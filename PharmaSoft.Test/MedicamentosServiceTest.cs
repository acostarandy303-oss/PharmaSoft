using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class MedicamentosServiceTest
    {
        [Fact]
        public async Task Buscar_CuandoExisteMedicamento_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(CreateMedicamento(id: 1, categoriaId: 1, proveedorId: 1, nombre: "Med1"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.MedicamentoId);
            Assert.Equal("Med1", result.Nombre);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteMedicamento_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new MedicamentoService(context);

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
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.AddRange(
                    CreateMedicamento(id: 1, categoriaId: 1, proveedorId: 1, nombre: "Aspirina"),
                    CreateMedicamento(id: 2, categoriaId: 1, proveedorId: 1, nombre: "Paracetamol"),
                    CreateMedicamento(id: 3, categoriaId: 1, proveedorId: 1, nombre: "Acetaminofen")
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);

            // Act
            var result = await service.GetList(m => m.Nombre.StartsWith("A"));

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, m => m.MedicamentoId == 1);
            Assert.Contains(result, m => m.MedicamentoId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoMedicamentoNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);
            var nuevo = CreateMedicamento(id: 0, categoriaId: 1, proveedorId: 1, nombre: "Nuevo Med");

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == nuevo.MedicamentoId);
            Assert.NotNull(saved);
            Assert.Equal("Nuevo Med", saved!.Nombre);
        }

        [Fact]
        public async Task Guardar_CuandoMedicamentoExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(CreateMedicamento(id: 20, categoriaId: 1, proveedorId: 1, nombre: "Original Med"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);

            var updated = CreateMedicamento(id: 20, categoriaId: 1, proveedorId: 1, nombre: "Med Modificado");

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Medicamentos.FirstOrDefaultAsync(m => m.MedicamentoId == 20);
            Assert.NotNull(saved);
            Assert.Equal("Med Modificado", saved!.Nombre);
        }

        [Fact]
        public async Task Existe_CuandoMedicamentoExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(CreateMedicamento(id: 50, categoriaId: 1, proveedorId: 1, nombre: "Prueba"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteMedicamento_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(new Categoria { CategoriaId = 1, Nombre = "Cat1" });
                seedContext.Proveedores.Add(new Proveedore { ProveedorId = 1, NombreEmpresa = "Prov1" });
                seedContext.Medicamentos.Add(CreateMedicamento(id: 80, categoriaId: 1, proveedorId: 1, nombre: "Para Borrar"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new MedicamentoService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Medicamentos.FindAsync(80);
            // Para Medicamento, el Eliminar es lÃ³gico (Activo = false) en su implementaciÃ³n actual.
            Assert.NotNull(eliminado);
            Assert.False(eliminado!.Activo);
        }

        private static Medicamento CreateMedicamento(int id, int categoriaId, int proveedorId, string nombre)
        {
            return new Medicamento
            {
                MedicamentoId = id,
                CategoriaId = categoriaId,
                ProveedorId = proveedorId,
                Nombre = nombre,
                CodigoBarras = "CB" + id,
                PrecioCompra = 10m,
                PrecioVenta = 15m,
                StockMinimo = 5,
                Activo = true
            };
        }
    }
}

