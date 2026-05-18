using PharmaSoft.Data.Context;
using PharmaSoft.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PharmaSoft
{
    public partial class InventarioForm : Form
    {
        private readonly PharmaContext _context;
        private readonly MedicamentoService _medicamentoService;
        private readonly LotesInventarioService _lotesService;
        public InventarioForm()
        {
            InitializeComponent();
            _context = new PharmaContext();
            _medicamentoService = new MedicamentoService(_context);
            _lotesService = new LotesInventarioService(_context);
            CargarInventario();
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            var criterio = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarInventario();
                return;
            }

            var medicamentos = await _medicamentoService.GetList(m =>
                m.Activo && (m.Nombre.Contains(criterio) ||
                (m.CodigoBarras != null && m.CodigoBarras.Contains(criterio)) ||
                (m.Laboratorio != null && m.Laboratorio.Contains(criterio))));

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

        private async void CargarInventario()
        {
            var medicamentos = await _medicamentoService.GetList(m => m.Activo);
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
            if (dgvInventario.Columns["PrecioCompra"] is { } colPrecC) colPrecC.HeaderText = "Precio Compra";
            if (dgvInventario.Columns["Caducidad"] is { } colCad) colCad.HeaderText = "Caducidad";
        }

        private async void button3_Click(object sender, EventArgs e)
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

        private async void button2_Click(object sender, EventArgs e)
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
                        if (form.Lote != null)
                        {
                            form.Lote.MedicamentoId = form.Medicamento.MedicamentoId;
                            await _lotesService.Guardar(form.Lote);
                        }
                        CargarInventario();
                        MessageBox.Show("Medicamento actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
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
    }
}