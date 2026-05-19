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

public class CuentasPorPagarService(PharmaContext contexto) : IService<CuentasPorPagar, int>
{
    public async Task<bool> Guardar(CuentasPorPagar cxp)
    {
        if (cxp.CxPid == 0)
            return await Insertar(cxp);
        else
            return await Modificar(cxp);
    }

    private async Task<bool> Insertar(CuentasPorPagar cxp)
    {
        contexto.CuentasPorPagars.Add(cxp);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(CuentasPorPagar cxp)
    {
        contexto.Update(cxp);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var cuentas = await contexto.CuentasPorPagars.FindAsync(id);
        if (cuentas == null)
            return false;

        contexto.CuentasPorPagars.Remove(cuentas);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await contexto.CuentasPorPagars.AnyAsync(c => c.CxPid == id);
    }

    public async Task<CuentasPorPagar?> Buscar(int id)
    {
        return await contexto.CuentasPorPagars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CxPid == id);
    }

    public async Task<List<CuentasPorPagar>> GetList(Expression<Func<CuentasPorPagar, bool>> criterio)
    {
        return await contexto.CuentasPorPagars
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}