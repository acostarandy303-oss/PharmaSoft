using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft.Data.Models;

public partial class Categoria
{
    [Key]
    [Column("CategoriaID")]
    public int CategoriaId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [InverseProperty("Categoria")]
    public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
