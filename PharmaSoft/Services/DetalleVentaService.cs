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

public class DetalleVentaService(PharmaContext contexto) : IService<DetalleVenta, int>
{
    public async Task<bool> Guardar(DetalleVenta detalle)
    {
        if (detalle.DetalleId == 0)
            return await Insertar(detalle);
        else
            return await Modificar(detalle);
    }

    private async Task<bool> Insertar(DetalleVenta detalle)
    {
        contexto.DetalleVentas.Add(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(DetalleVenta detalle)
    {
        contexto.Update(detalle);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var detalleVentas = await contexto.DetalleVentas.FindAsync(id);
        if (detalleVentas == null)
            return false;

        contexto.DetalleVentas.Remove(detalleVentas );
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<DetalleVenta?> Buscar(int id)
    {
        return await contexto.DetalleVentas
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DetalleId == id);
    }

    public async Task<List<DetalleVenta>> GetList(Expression<Func<DetalleVenta, bool>> criterio)
    {
        return await contexto.DetalleVentas
           .AsNoTracking()
           .Where(criterio)
           .ToListAsync();
    }
}