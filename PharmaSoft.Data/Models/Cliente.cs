using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

[Index("RncCedula", Name = "UQ__Clientes__554ADC6A51A518E9", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("RNC_Cedula")]
    [StringLength(20)]
    public string? RncCedula { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(255)]
    public string? Direccion { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? LimiteCredito { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<CuentasPorCobrar> CuentasPorCobrars { get; set; } = new List<CuentasPorCobrar>();
}
