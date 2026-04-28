using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class PagosProveedore
{
    [Key]
    [Column("PagoProveedorID")]
    public int PagoProveedorId { get; set; }

    [Column("CxPID")]
    public int CxPid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaPago { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal MontoPagado { get; set; }

    [StringLength(50)]
    public string? MetodoPago { get; set; }

    [ForeignKey("CxPid")]
    [InverseProperty("PagosProveedores")]
    public virtual CuentasPorPagar CxP { get; set; } = null!;
}
