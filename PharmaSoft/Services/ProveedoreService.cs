using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class ProveedoreService(PharmaContext contexto) : IService<Proveedore, int>
{
    public async Task<bool> Guardar(Proveedore proveedor)
    {
        if (proveedor.ProveedorId == 0)
            return await Insertar(proveedor);
        else
            return await Modificar(proveedor);
    }

    private async Task<bool> Insertar(Proveedore proveedor)
    {
        contexto.Proveedores.Add(proveedor);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Proveedore proveedor)
    {
        contexto.Update(proveedor);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.Proveedores
            .Where(p => p.ProveedorId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<Proveedore?> Buscar(int id)
    {
        return await contexto.Proveedores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProveedorId == id);
    }

    public async Task<List<Proveedore>> Listar(Expression<Func<Proveedore, bool>> criterio)
    {
        return await contexto.Proveedores
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}