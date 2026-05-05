using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaSoft.Test.Infraestructura
{
    public class TestDbContextFactory
    {
        public static string NewDataBaseName() => $"PharmaDb_{Guid.NewGuid()}";

        public static PharmaContext  CreateContext(string DatabaseName)
        {
            var options = new DbContextOptionsBuilder<PharmaContext>()
                .UseInMemoryDatabase(DatabaseName)
                .Options;

            return new InMemoryPharmaContext(options);
        }

        private sealed class InMemoryPharmaContext(DbContextOptions<PharmaContext> options)
            : PharmaContext(options)
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Intentionally empty: tests provide InMemory provider through options.
            }
        }
    }
}
