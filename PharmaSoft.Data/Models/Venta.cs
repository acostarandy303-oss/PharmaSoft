using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class Venta
{
    [Key]
    [Column("VentaID")]
    public int VentaId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaVenta { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [StringLength(50)]
    public string? MetodoPago { get; set; }

    [InverseProperty("Venta")]
    public virtual ICollection<CuentasPorCobrar> CuentasPorCobrars { get; set; } = new List<CuentasPorCobrar>();

    [InverseProperty("Venta")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
}
