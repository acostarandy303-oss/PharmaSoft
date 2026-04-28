using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class PagosCliente
{
    [Key]
    [Column("PagoClienteID")]
    public int PagoClienteId { get; set; }

    [Column("CxCID")]
    public int CxCid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaPago { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal MontoPagado { get; set; }

    [StringLength(50)]
    public string? MetodoPago { get; set; }

    [ForeignKey("CxCid")]
    [InverseProperty("PagosClientes")]
    public virtual CuentasPorCobrar CxC { get; set; } = null!;
}
