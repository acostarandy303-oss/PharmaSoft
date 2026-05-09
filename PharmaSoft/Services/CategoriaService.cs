using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using System.Linq.Expressions;


namespace PharmaSoft.Services;

public class CategoriaService(PharmaContext contexto) : IService<Categoria, int>
{
    public async Task<bool> Guardar(Categoria categoria)
    {
        
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

        var categoria = await contexto.Categorias.FindAsync(id);
        if (categoria == null)
            return false;

        contexto.Categorias.Remove(categoria);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;

        
    }

    public async Task<Categoria?> Buscar(int id)
    {
        return await contexto.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoriaId == id);
    }

    public async Task<List<Categoria>> GetList(Expression<Func<Categoria, bool>> criterio)
    {
        return await contexto.Categorias
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync(); 
    }
}

