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

public class CompraService(PharmaContext contexto) : IService<Compra, int>
{
    public async Task<bool> Guardar(Compra compra)
    {
        if (compra.CompraId == 0)
            return await Insertar(compra);
        else
            return await Modificar(compra);
    }

    private async Task<bool> Insertar(Compra compra)
    {
        contexto.Compras.Add(compra);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Compra compra)
    {
        contexto.Update(compra);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var compra = await contexto.Compras.FindAsync(id);
        if (compra == null)
            return false;

        contexto.Compras.Remove(compra);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<bool> Existe(int id)
    {
        return await contexto.Compras.AnyAsync(c => c.CompraId == id);
    }

    public async Task<Compra?> Buscar(int id)
    {
        // En un escenario real, tal vez quieras usar .Include(c => c.DetalleCompras) aquí
        return await contexto.Compras
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompraId == id);
    }

   

    public async Task<List<Compra>> GetList(Expression<Func<Compra, bool>> criterio)
    {
        return await contexto.Compras
           .AsNoTracking()
           .Where(criterio)
           .ToListAsync();
    }
}