using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class PagosProveedoreService(PharmaContext contexto) : IService<PagosProveedore, int>
{
    public async Task<bool> Guardar(PagosProveedore pago)
    {
        if (pago.PagoProveedorId == 0)
            return await Insertar(pago);
        else
            return await Modificar(pago);
    }

    private async Task<bool> Insertar(PagosProveedore pago)
    {
        contexto.PagosProveedores.Add(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(PagosProveedore pago)
    {
        contexto.Update(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.PagosProveedores
            .Where(p => p.PagoProveedorId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<PagosProveedore?> Buscar(int id)
    {
        return await contexto.PagosProveedores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PagoProveedorId == id);
    }

    public async Task<List<PagosProveedore>> Listar(Expression<Func<PagosProveedore, bool>> criterio)
    {
        return await contexto.PagosProveedores
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}