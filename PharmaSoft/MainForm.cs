using System.ComponentModel;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using Microsoft.EntityFrameworkCore;

namespace PharmaSoft;

public partial class PharmaSoft : Form
{
    private readonly PharmaContext _context;
    private readonly MedicamentoService _medicamentoService;
    private readonly LotesInventarioService _lotesService;

    public PharmaSoft()
    {
        InitializeComponent();
        _context = new PharmaContext();
        _medicamentoService = new MedicamentoService(_context);
        _lotesService = new LotesInventarioService(_context);
        CargarInventario();
        btnAnadir.Click += btnAnadir_Click;
        btnEditar.Click += btnEditar_Click;
        btnEliminar.Click += btnEliminar_Click;
        txtBuscar.TextChanged += txtBuscar_TextChanged;
    }

    private void btnInventario_Click(object? sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Inventario de Productos";
        CargarInventario();
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Panel de Control";
    }

    private void btnVentas_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Punto de Venta";
    }

    private void btnClientes_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Clientes";
    }

    private void btnCompras_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Compras";
    }

    private void btnRecetas_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Recetas Médicas";
    }

    private void btnReportes_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Reportes";
    }

    private void btnConfiguracion_Click(object sender, EventArgs e)
    {
        lblTituloSeccion.Text = "Configuración";
    }

    private async void CargarInventario()
    {
        var medicamentos = await _medicamentoService.GetList(m => true);
        var lotes = await _lotesService.GetList(l => true);

        var inventario = medicamentos.Select(m => new
        {
            m.MedicamentoId,
            m.CodigoBarras,
            m.Nombre,
            m.Laboratorio,
            Cantidad = lotes.Where(l => l.MedicamentoId == m.MedicamentoId).Sum(l => l.CantidadActual),
            m.PrecioVenta,
            Caducidad = lotes.Where(l => l.MedicamentoId == m.MedicamentoId && l.FechaVencimiento >= DateOnly.FromDateTime(DateTime.Now))
                             .OrderBy(l => l.FechaVencimiento)
                             .Select(l => l.FechaVencimiento)
                             .FirstOrDefault()
        }).ToList();

        dgvInventario.Columns.Clear();
        dgvInventario.DataSource = inventario;
        ConfigurarColumnas();
    }

    private void ConfigurarColumnas()
    {
        if (dgvInventario.Columns["MedicamentoId"] is { } colId) colId.Visible = false;
        if (dgvInventario.Columns["CodigoBarras"] is { } colCod) colCod.HeaderText = "Código";
        if (dgvInventario.Columns["Nombre"] is { } colNom) colNom.HeaderText = "Nombre";
        if (dgvInventario.Columns["Laboratorio"] is { } colLab) colLab.HeaderText = "Laboratorio";
        if (dgvInventario.Columns["Cantidad"] is { } colCant) colCant.HeaderText = "Cantidad";
        if (dgvInventario.Columns["PrecioVenta"] is { } colPrec) colPrec.HeaderText = "Precio Venta";
        if (dgvInventario.Columns["Caducidad"] is { } colCad) colCad.HeaderText = "Caducidad";
    }

    private async void btnAnadir_Click(object? sender, EventArgs e)
    {
        var form = new Forms.MedicamentoForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var medicamento = form.Medicamento;
            if (await _medicamentoService.Guardar(medicamento))
            {
                if (form.Lote != null)
                {
                    form.Lote.MedicamentoId = medicamento.MedicamentoId;
                    await _lotesService.Guardar(form.Lote);
                }
                CargarInventario();
                MessageBox.Show("Medicamento guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private async void btnEditar_Click(object? sender, EventArgs e)
    {
        if (dgvInventario.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un medicamento para editar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var id = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells["MedicamentoId"].Value);
        var medicamento = await _medicamentoService.Buscar(id);

        if (medicamento != null)
        {
            var form = new Forms.MedicamentoForm(medicamento);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (await _medicamentoService.Guardar(form.Medicamento))
                {
                    CargarInventario();
                    MessageBox.Show("Medicamento actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    private async void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (dgvInventario.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un medicamento para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var id = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells["MedicamentoId"].Value);
        var nombre = dgvInventario.SelectedRows[0].Cells["Nombre"].Value?.ToString();

        var resultado = MessageBox.Show($"¿Está seguro de eliminar el medicamento '{nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (resultado == DialogResult.Yes)
        {
            if (await _medicamentoService.Eliminar(id))
            {
                CargarInventario();
                MessageBox.Show("Medicamento eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private async void txtBuscar_TextChanged(object? sender, EventArgs e)
    {
        var criterio = txtBuscar.Text.Trim();
        if (string.IsNullOrEmpty(criterio))
        {
            CargarInventario();
            return;
        }

        var medicamentos = await _medicamentoService.GetList(m =>
            m.Nombre.Contains(criterio) ||
            (m.CodigoBarras != null && m.CodigoBarras.Contains(criterio)) ||
            (m.Laboratorio != null && m.Laboratorio.Contains(criterio)));

        var lotes = await _lotesService.GetList(l => true);

        var inventario = medicamentos.Select(m => new
        {
            m.MedicamentoId,
            m.CodigoBarras,
            m.Nombre,
            m.Laboratorio,
            Cantidad = lotes.Where(l => l.MedicamentoId == m.MedicamentoId).Sum(l => l.CantidadActual),
            m.PrecioVenta,
            Caducidad = lotes.Where(l => l.MedicamentoId == m.MedicamentoId && l.FechaVencimiento >= DateOnly.FromDateTime(DateTime.Now))
                             .OrderBy(l => l.FechaVencimiento)
                             .Select(l => l.FechaVencimiento)
                             .FirstOrDefault()
        }).ToList();

        dgvInventario.DataSource = inventario;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _context.Dispose();
        base.OnFormClosing(e);
    }
}