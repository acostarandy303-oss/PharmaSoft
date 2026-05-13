using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

[Index("CodigoBarras", Name = "UQ__Medicame__F61589C8B5D33220", IsUnique = true)]
public partial class Medicamento
{
    [Key]
    [Column("MedicamentoID")]
    public int MedicamentoId { get; set; }

    [StringLength(50)]
    public string? CodigoBarras { get; set; }

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [StringLength(500)]
    [NotMapped]
    public string? Descripcion { get; set; }

    [StringLength(150)]
    [NotMapped]
    public string? Presentacion { get; set; }

    [StringLength(150)]
    [NotMapped]
    public string? Laboratorio { get; set; }

    [StringLength(100)]
    [NotMapped]
    public string? Dosis { get; set; }

    [Column("CategoriaID")]
    public int CategoriaId { get; set; }

    [Column("ProveedorID")]
    public int ProveedorId { get; set; }

    public bool? RequiereReceta { get; set; }

    [NotMapped]
    public DateOnly? FechaVencimiento { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioCompra { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioVenta { get; set; }

    public int? StockMinimo { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Medicamentos")]
    public virtual Categoria Categoria { get; set; } = null!;

    [InverseProperty("Medicamento")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [InverseProperty("Medicamento")]
    public virtual ICollection<LotesInventario> LotesInventarios { get; set; } = new List<LotesInventario>();

    [ForeignKey("ProveedorId")]
    [InverseProperty("Medicamentos")]
    public virtual Proveedore Proveedor { get; set; } = null!;
}
