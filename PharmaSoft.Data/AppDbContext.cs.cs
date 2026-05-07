using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Models;

public class AppDbContext : DbContext
{
    // Aquí defines tus tablas basadas en tus modelos
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }
    // Agrega los demás modelos que mencionaste aquí...

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Esta es la cadena de conexión a tu SQL Server local
        optionsBuilder.UseSqlServer("Server=.;Database=PharmaSoftDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
