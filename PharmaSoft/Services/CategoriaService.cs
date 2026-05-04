using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

// Usando inyección de dependencias a través del constructor primario (C# 12+)
public class CategoriaService(PharmaContext contexto) : IService<Categoria, int>
{
    public async Task<bool> Guardar(Categoria categoria)
    {
        // Si el ID es 0, es un registro nuevo. Si es mayor, se está modificando.
        if (categoria.CategoriaId == 0)
            return await Insertar(categoria);
        else
            return await Modificar(categoria);
    }

    private async Task<bool> Insertar(Categoria categoria)
    {
        contexto.Categorias.Add(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Categoria categoria)
    {
        contexto.Update(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        // ExecuteDeleteAsync es la forma más optimizada de borrar en EF Core 7+
        var eliminados = await contexto.Categorias
            .Where(c => c.CategoriaId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<Categoria?> Buscar(int id)
    {
        return await contexto.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoriaId == id);
    }

    public async Task<List<Categoria>> Listar(Expression<Func<Categoria, bool>> criterio)
    {
        return await contexto.Categorias
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}

public interface IService<T1, T2>
{
}