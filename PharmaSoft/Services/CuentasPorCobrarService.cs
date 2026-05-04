using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class CuentasPorCobrarService(PharmaContext contexto) : IService<CuentasPorCobrar, int>
{
    public async Task<bool> Guardar(CuentasPorCobrar cxc)
    {
        if (cxc.CxCid == 0)
            return await Insertar(cxc);
        else
            return await Modificar(cxc);
    }

    private async Task<bool> Insertar(CuentasPorCobrar cxc)
    {
        contexto.CuentasPorCobrars.Add(cxc);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(CuentasPorCobrar cxc)
    {
        contexto.Update(cxc);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.CuentasPorCobrars
            .Where(c => c.CxCid == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<CuentasPorCobrar?> Buscar(int id)
    {
        return await contexto.CuentasPorCobrars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CxCid == id);
    }

    public async Task<List<CuentasPorCobrar>> Listar(Expression<Func<CuentasPorCobrar, bool>> criterio)
    {
        return await contexto.CuentasPorCobrars
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}