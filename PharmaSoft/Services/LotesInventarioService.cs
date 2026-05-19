using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using Aplicada1.Core;   

namespace PharmaSoft.Services;

public class LotesInventarioService(PharmaContext contexto) : IService<LotesInventario, int>
{
    public async Task<bool> Guardar(LotesInventario lote)
    {
        if (lote.LoteId == 0)
            return await Insertar(lote);
        else
            return await Modificar(lote);
    }

    private async Task<bool> Insertar(LotesInventario lote)
    {
        contexto.LotesInventarios.Add(lote);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(LotesInventario lote)
    {
        var tracked = contexto.LotesInventarios.Local.FirstOrDefault(l => l.LoteId == lote.LoteId);
        if (tracked != null)
        {
            contexto.Entry(tracked).CurrentValues.SetValues(lote);
        }
        else
        {
            contexto.LotesInventarios.Update(lote);
        }
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var loteInventario = await contexto.LotesInventarios.FindAsync(id);
        if (loteInventario == null)
            return false;

        contexto.LotesInventarios.Remove(loteInventario);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await contexto.LotesInventarios.AnyAsync(l => l.LoteId == id);
    }

    public async Task<LotesInventario?> Buscar(int id)
    {
        return await contexto.LotesInventarios
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LoteId == id);
    }

    public async Task<List<LotesInventario>> GetList(Expression<Func<LotesInventario, bool>> criterio)
    {
      return await contexto.LotesInventarios
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}