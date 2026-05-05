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

public class DetalleCompraService(PharmaContext contexto) : IService<DetalleCompra, int>
{
    public async Task<bool> Guardar(DetalleCompra detalle)
    {
        if (detalle.DetalleCompraId == 0)
            return await Insertar(detalle);
        else
            return await Modificar(detalle);
    }

    private async Task<bool> Insertar(DetalleCompra detalle)
    {
        contexto.DetalleCompras.Add(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(DetalleCompra detalle)
    {
        contexto.Update(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var detalleCompra = await contexto.DetalleCompras.FindAsync(id);
        if (detalleCompra == null)
            return false;

        contexto.DetalleCompras.Remove(detalleCompra);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<DetalleCompra?> Buscar(int id)
    {
        return await contexto.DetalleCompras
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleCompraId == id);
    }

    public async Task<List<DetalleCompra>> GetList(Expression<Func<DetalleCompra, bool>> criterio)
    {
        return await contexto.DetalleCompras
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}