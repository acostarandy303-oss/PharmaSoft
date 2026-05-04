using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class PagosClienteService(PharmaContext contexto) : IService<PagosCliente, int>
{
    public async Task<bool> Guardar(PagosCliente pago)
    {
        if (pago.PagoClienteId == 0)
            return await Insertar(pago);
        else
            return await Modificar(pago);
    }

    private async Task<bool> Insertar(PagosCliente pago)
    {
        contexto.PagosClientes.Add(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(PagosCliente pago)
    {
        contexto.Update(pago);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.PagosClientes
            .Where(p => p.PagoClienteId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<PagosCliente?> Buscar(int id)
    {
        return await contexto.PagosClientes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PagoClienteId == id);
    }

    public async Task<List<PagosCliente>> Listar(Expression<Func<PagosCliente, bool>> criterio)
    {
        return await contexto.PagosClientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}