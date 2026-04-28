using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

[Table("LotesInventario")]
public partial class LotesInventario
{
    [Key]
    [Column("LoteID")]
    public int LoteId { get; set; }

    [Column("MedicamentoID")]
    public int MedicamentoId { get; set; }

    [StringLength(50)]
    public string NumeroLote { get; set; } = null!;

    public DateOnly FechaVencimiento { get; set; }

    public int CantidadActual { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaIngreso { get; set; }

    [InverseProperty("Lote")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [ForeignKey("MedicamentoId")]
    [InverseProperty("LotesInventarios")]
    public virtual Medicamento Medicamento { get; set; } = null!;
}
