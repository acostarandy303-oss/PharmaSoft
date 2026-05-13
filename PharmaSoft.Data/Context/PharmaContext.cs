using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Data.Context;

public partial class PharmaContext : DbContext
{
    public PharmaContext()
    {
    }

    public PharmaContext(DbContextOptions<PharmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<CuentasPorCobrar> CuentasPorCobrars { get; set; }

    public virtual DbSet<CuentasPorPagar> CuentasPorPagars { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<LotesInventario> LotesInventarios { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<PagosCliente> PagosClientes { get; set; }

    public virtual DbSet<PagosProveedore> PagosProveedores { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetConnectionString("PharmaDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C5E037566A");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A7DAC6EF43");

            entity.Property(e => e.LimiteCredito).HasDefaultValue(0.00m);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.CompraId).HasName("PK__Compras__067DA725CB505B59");

            entity.Property(e => e.FechaCompra).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedores");
        });

        modelBuilder.Entity<CuentasPorCobrar>(entity =>
        {
            entity.HasKey(e => e.CxCid).HasName("PK__CuentasP__1884E33C3B157190");

            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");

            entity.HasOne(d => d.Cliente).WithMany(p => p.CuentasPorCobrars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CxC_Clientes");

            entity.HasOne(d => d.Venta).WithMany(p => p.CuentasPorCobrars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CxC_Ventas");
        });

        modelBuilder.Entity<CuentasPorPagar>(entity =>
        {
            entity.HasKey(e => e.CxPid).HasName("PK__CuentasP__2B8D7CE83D0A6A21");

            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");

            entity.HasOne(d => d.Compra).WithMany(p => p.CuentasPorPagars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CxP_Compras");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.CuentasPorPagars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CxP_Proveedores");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.DetalleCompraId).HasName("PK__DetalleC__F7FC189A4B103DD5");

            entity.HasOne(d => d.Compra).WithMany(p => p.DetalleCompras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleC_Compras");

            entity.HasOne(d => d.Medicamento).WithMany(p => p.DetalleCompras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleC_Medicamentos");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleV__6E19D6FAEBC23E91");

            entity.HasOne(d => d.Lote).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Lotes");

            entity.HasOne(d => d.Venta).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detalle_Ventas");
        });

        modelBuilder.Entity<LotesInventario>(entity =>
        {
            entity.HasKey(e => e.LoteId).HasName("PK__LotesInv__E6EAE6F88F0AB7ED");

            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Medicamento).WithMany(p => p.LotesInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lotes_Medicamentos");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.MedicamentoId).HasName("PK__Medicame__003D65F308DB0ECC");

            entity.Property(e => e.RequiereReceta).HasDefaultValue(false);
            entity.Property(e => e.StockMinimo).HasDefaultValue(10);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Medicamentos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicamentos_Categorias");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Medicamentos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicamentos_Proveedores");
        });

        modelBuilder.Entity<PagosCliente>(entity =>
        {
            entity.HasKey(e => e.PagoClienteId).HasName("PK__PagosCli__7A06804746EB57F7");

            entity.Property(e => e.FechaPago).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CxC).WithMany(p => p.PagosClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_CxC");
        });

        modelBuilder.Entity<PagosProveedore>(entity =>
        {
            entity.HasKey(e => e.PagoProveedorId).HasName("PK__PagosPro__D09663A5632D04E3");

            entity.Property(e => e.FechaPago).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CxP).WithMany(p => p.PagosProveedores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_CxP");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266BB9F87D2A0F");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("PK__Ventas__5B41514C6BD66B9A");

            entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
