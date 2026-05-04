using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft.Services;

public class ClienteService(PharmaContext contexto) : IService<Cliente, int>
{
    public async Task<bool> Guardar(Cliente cliente)
    {
        if (cliente.ClienteId == 0)
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    private async Task<bool> Insertar(Cliente cliente)
    {
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cliente cliente)
    {
        contexto.Update(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var eliminados = await contexto.Clientes
            .Where(c => c.ClienteId == id)
            .ExecuteDeleteAsync();

        return eliminados > 0;
    }

    public async Task<Cliente?> Buscar(int id)
    {
        return await contexto.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio)
    {
        return await contexto.Clientes
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}