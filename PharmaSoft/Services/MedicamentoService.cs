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

public class MedicamentoService(PharmaContext contexto) : IService<Medicamento, int>
{
    public async Task<bool> Guardar(Medicamento medicamento)
    {
        if (medicamento.MedicamentoId == 0)
            return await Insertar(medicamento);
        else
            return await Modificar(medicamento);
    }

    private async Task<bool> Insertar(Medicamento medicamento)
    {
        contexto.Medicamentos.Add(medicamento);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Medicamento medicamento)
    {
        contexto.Update(medicamento);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        var medicamento = await contexto.Medicamentos.FindAsync(id);
        if (medicamento == null)
            return false;

        contexto.Medicamentos.Remove(medicamento);
        var eliminados = await contexto.SaveChangesAsync();
        return eliminados > 0;
    }

    public async Task<Medicamento?> Buscar(int id)
    {
        return await contexto.Medicamentos
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MedicamentoId == id);
    }

   

    public async Task<List<Medicamento>> GetList(Expression<Func<Medicamento, bool>> criterio)
    {
        return await contexto.Medicamentos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}