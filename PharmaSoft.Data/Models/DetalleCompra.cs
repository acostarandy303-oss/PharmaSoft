using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class DetalleCompra
{
    [Key]
    [Column("DetalleCompraID")]
    public int DetalleCompraId { get; set; }

    [Column("CompraID")]
    public int CompraId { get; set; }

    [Column("MedicamentoID")]
    public int MedicamentoId { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioCostoUnitario { get; set; }

    [ForeignKey("CompraId")]
    [InverseProperty("DetalleCompras")]
    public virtual Compra Compra { get; set; } = null!;

    [ForeignKey("MedicamentoId")]
    [InverseProperty("DetalleCompras")]
    public virtual Medicamento Medicamento { get; set; } = null!;
}
