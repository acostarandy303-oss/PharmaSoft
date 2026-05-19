using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Threading.Tasks;
using PharmaSoft.Test.Infraestructura;
using Xunit;

namespace PharmaSoft.Test
{
    public class ClienteServiceTests
    {
        [Fact]
        public async Task Buscar_CuandoExisteCliente_RetornaEntidad()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(CreateCliente(id: 1, nombre: "Juan PÃ©rez"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ClienteService(context);

            // Act
            var result = await service.Buscar(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.ClienteId);
            Assert.Equal("Juan PÃ©rez", result.Nombre);
            Assert.Empty(context.ChangeTracker.Entries());
        }

        [Fact]
        public async Task Buscar_CuandoNoExisteCliente_RetornaNull()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new ClienteService(context);

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
                seedContext.Clientes.AddRange(
                    CreateCliente(id: 1, nombre: "Ana"),
                    CreateCliente(id: 2, nombre: "Antonio"),
                    CreateCliente(id: 3, nombre: "Beatriz")
                );
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ClienteService(context);

            // Act
            var result = await service.GetList(c => c.Nombre.StartsWith("An"));

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.ClienteId == 1);
            Assert.Contains(result, c => c.ClienteId == 2);
        }

        [Fact]
        public async Task Guardar_CuandoClienteNoExiste_InsertaYRetornaTrue()
        {
            // Arrange
            await using var context = TestDbContextFactory.CreateContext(TestDbContextFactory.NewDataBaseName());
            var service = new ClienteService(context);
            var nuevo = CreateCliente(id: 0, nombre: "Carlos");

            // Act
            var result = await service.Guardar(nuevo);

            // Assert
            Assert.True(result);

            var saved = await context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == nuevo.ClienteId);
            Assert.NotNull(saved);
            Assert.Equal("Carlos", saved!.Nombre);
        }

        [Fact]
        public async Task Guardar_CuandoClienteExiste_ModificaYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();

            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(CreateCliente(id: 20, nombre: "Original"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ClienteService(context);

            var updated = CreateCliente(id: 20, nombre: "Modificado");

            // Act
            var result = await service.Guardar(updated);

            // Assert
            Assert.True(result);

            var saved = await context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == 20);
            Assert.NotNull(saved);
            Assert.Equal("Modificado", saved!.Nombre);
        }

        [Fact]
        public async Task Existe_CuandoClienteExiste_RetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(CreateCliente(id: 50, nombre: "PruebaExiste"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ClienteService(context);

            // Act
            var result = await service.Existe(50);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Eliminar_CuandoExisteCliente_LoBorraYRetornaTrue()
        {
            // Arrange
            var dbName = TestDbContextFactory.NewDataBaseName();
            await using (var seedContext = TestDbContextFactory.CreateContext(dbName))
            {
                seedContext.Clientes.Add(CreateCliente(id: 80, nombre: "ParaBorrar"));
                await seedContext.SaveChangesAsync();
            }

            await using var context = TestDbContextFactory.CreateContext(dbName);
            var service = new ClienteService(context);

            // Act
            var result = await service.Eliminar(80);

            // Assert
            Assert.True(result);
            var eliminado = await context.Clientes.FindAsync(80);
            Assert.Null(eliminado);
        }

        private static Cliente CreateCliente(int id, string nombre)
        {
            return new Cliente
            {
                ClienteId = id,
                Nombre = nombre,
                RncCedula = "00000000000",
                Telefono = "8090000000",
                Direccion = "Desconocida",
                LimiteCredito = 1000m
            };
        }
    }
}

