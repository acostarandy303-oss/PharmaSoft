using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

[Table("CuentasPorCobrar")]
public partial class CuentasPorCobrar
{
    [Key]
    [Column("CxCID")]
    public int CxCid { get; set; }

    [Column("VentaID")]
    public int VentaId { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal MontoInicial { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal SaldoPendiente { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("CuentasPorCobrars")]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("CxC")]
    public virtual ICollection<PagosCliente> PagosClientes { get; set; } = new List<PagosCliente>();

    [ForeignKey("VentaId")]
    [InverseProperty("CuentasPorCobrars")]
    public virtual Venta Venta { get; set; } = null!;
}
