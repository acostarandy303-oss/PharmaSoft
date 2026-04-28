using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class Proveedore
{
    [Key]
    [Column("ProveedorID")]
    public int ProveedorId { get; set; }

    [StringLength(150)]
    public string NombreEmpresa { get; set; } = null!;

    [StringLength(100)]
    public string? Contacto { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [InverseProperty("Proveedor")]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    [InverseProperty("Proveedor")]
    public virtual ICollection<CuentasPorPagar> CuentasPorPagars { get; set; } = new List<CuentasPorPagar>();

    [InverseProperty("Proveedor")]
    public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
