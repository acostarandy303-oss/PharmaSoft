using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class DetalleVenta
{
    [Key]
    [Column("DetalleID")]
    public int DetalleId { get; set; }

    [Column("VentaID")]
    public int VentaId { get; set; }

    [Column("LoteID")]
    public int LoteId { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Subtotal { get; set; }

    [ForeignKey("LoteId")]
    [InverseProperty("DetalleVenta")]
    public virtual LotesInventario Lote { get; set; } = null!;

    [ForeignKey("VentaId")]
    [InverseProperty("DetalleVenta")]
    public virtual Venta Venta { get; set; } = null!;
}
