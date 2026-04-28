using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

[Table("CuentasPorPagar")]
public partial class CuentasPorPagar
{
    [Key]
    [Column("CxPID")]
    public int CxPid { get; set; }

    [Column("CompraID")]
    public int CompraId { get; set; }

    [Column("ProveedorID")]
    public int ProveedorId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal MontoInicial { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal SaldoPendiente { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("CompraId")]
    [InverseProperty("CuentasPorPagars")]
    public virtual Compra Compra { get; set; } = null!;

    [InverseProperty("CxP")]
    public virtual ICollection<PagosProveedore> PagosProveedores { get; set; } = new List<PagosProveedore>();

    [ForeignKey("ProveedorId")]
    [InverseProperty("CuentasPorPagars")]
    public virtual Proveedore Proveedor { get; set; } = null!;
}
