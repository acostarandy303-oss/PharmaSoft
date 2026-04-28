using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class Compra
{
    [Key]
    [Column("CompraID")]
    public int CompraId { get; set; }

    [Column("ProveedorID")]
    public int ProveedorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCompra { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalCompra { get; set; }

    [StringLength(20)]
    public string? TipoPago { get; set; }

    [StringLength(50)]
    public string? NumeroFacturaProveedor { get; set; }

    [InverseProperty("Compra")]
    public virtual ICollection<CuentasPorPagar> CuentasPorPagars { get; set; } = new List<CuentasPorPagar>();

    [InverseProperty("Compra")]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [ForeignKey("ProveedorId")]
    [InverseProperty("Compras")]
    public virtual Proveedore Proveedor { get; set; } = null!;
}
