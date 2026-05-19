using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class CategoriaServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteCategoria_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(CreateCategoria(id: 1, nombre: "AnalgÃ©sicos"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CategoriaService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.CategoriaId);
            Assert.Equal("AnalgÃ©sicos", result.Nombre);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteCategoria_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new CategoriaService(context);

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
                seedContext.Categorias.AddRange(
                    CreateCategoria(id: 1, nombre: "AnalgÃ©sicos"),
                    CreateCategoria(id: 2, nombre: "Antiinflamatorios"),
                    CreateCategoria(id: 3, nombre: "AntibiÃ³ticos")
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CategoriaService(context);

            // Act
            var result = await service.GetList(c => c.Nombre.StartsWith("Anti"));

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoriaId == 2);
            Assert.Contains(result, c => c.CategoriaId == 3);
        }

        [Fact]
        public async Task Guardar_CuandoCategoriaNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new CategoriaService(context);
            var nuevaCategoria = CreateCategoria(id: 0, nombre: "Nueva Categoría");

            // Act
            var result = await service.Guardar(nuevaCategoria);

            // Assert
            Assert.True(result);

            var saved = await context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == nuevaCategoria.CategoriaId);
            Assert.NotNull(saved);
            Assert.Equal("Nueva Categoría", saved!.Nombre);
        }

        [Fact]
        public async Task Guardar_CuandoCategoriaExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();

            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(CreateCategoria(id: 20, nombre: "Original"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CategoriaService(context);

            var updated = CreateCategoria(id: 20, nombre: "Modificado");

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == 20);
            Assert.NotNull(saved);
            Assert.Equal("Modificado", saved!.Nombre);
        }

        [Fact]
        public async Task Existe_CuandoCategoriaExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(CreateCategoria(id: 50, nombre: "PruebaExiste"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CategoriaService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteCategoria_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Categorias.Add(CreateCategoria(id: 80, nombre: "ParaBorrar"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new CategoriaService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Categorias.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static Categoria CreateCategoria(int id, string nombre, string? descripcion = "DescripciÃ³n genÃ©rica")
        {
            return new Categoria
            {
                CategoriaId = id,
                Nombre = nombre,
                Descripcion = descripcion
            };
        }
    }
}
