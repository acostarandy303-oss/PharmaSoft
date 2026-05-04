using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class VentaService(PharmaContext contexto) : IService<Venta, int>
{
    public async Task<bool> Guardar(Venta venta)
    {
        if (venta.VentaId == 0)
            return await Insertar(venta);
        else
            return await Modificar(venta);
    }

    private async Task<bool> Insertar(Venta venta)
    {
        contexto.Ventas.Add(venta);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Venta venta)
    {
        contexto.Update(venta);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.Ventas
            .Where(v => v.VentaId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<Venta?> Buscar(int id)
    {
        return await contexto.Ventas
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VentaId == id);
    }

    public async Task<List<Venta>> Listar(Expression<Func<Venta, bool>> criterio)
    {
        return await contexto.Ventas
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}