using System;
using System.Linq;
using System.Windows.Forms;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft
{
    public class EditarProductoForm : Form
    {
        private int _medicamentoId;
        private TextBox txtNombre;
        private TextBox txtCodigo;
        private NumericUpDown nudPrecioVenta;
        private NumericUpDown nudPrecioCompra;
        private NumericUpDown nudStockMinimo;
        private ComboBox cbCategoria;
        private Button btnGuardar;
        private Button btnCancelar;

        public EditarProductoForm(int medicamentoId)
        {
            _medicamentoId = medicamentoId;
            InitializeComponent();
            LoadCategorias();
            LoadMedicamento();
        }

        private void InitializeComponent()
        {
            Text = "Editar Producto";
            Width = 400;
            Height = 350;
            StartPosition = FormStartPosition.CenterParent;

            var lblNombre = new Label { Text = "Nombre:", Left = 10, Top = 20, Width = 100 };
            txtNombre = new TextBox { Left = 120, Top = 20, Width = 240 };

            var lblCodigo = new Label { Text = "Código:", Left = 10, Top = 60, Width = 100 };
            txtCodigo = new TextBox { Left = 120, Top = 60, Width = 240 };

            var lblCategoria = new Label { Text = "Categoría:", Left = 10, Top = 100, Width = 100 };
            cbCategoria = new ComboBox { Left = 120, Top = 100, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblPrecioCompra = new Label { Text = "Precio Compra:", Left = 10, Top = 140, Width = 100 };
            nudPrecioCompra = new NumericUpDown { Left = 120, Top = 140, Width = 120, DecimalPlaces = 2, Maximum = 1000000 };

            var lblPrecioVenta = new Label { Text = "Precio Venta:", Left = 10, Top = 180, Width = 100 };
            nudPrecioVenta = new NumericUpDown { Left = 120, Top = 180, Width = 120, DecimalPlaces = 2, Maximum = 1000000 };

            var lblStock = new Label { Text = "Stock Min:", Left = 10, Top = 220, Width = 100 };
            nudStockMinimo = new NumericUpDown { Left = 120, Top = 220, Width = 120, Maximum = 100000 };

            btnGuardar = new Button { Text = "Guardar", Left = 120, Top = 260, Width = 100 };
            btnCancelar = new Button { Text = "Cancelar", Left = 240, Top = 260, Width = 100 };

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigo);
            Controls.Add(lblCategoria);
            Controls.Add(cbCategoria);
            Controls.Add(lblPrecioCompra);
            Controls.Add(nudPrecioCompra);
            Controls.Add(lblPrecioVenta);
            Controls.Add(nudPrecioVenta);
            Controls.Add(lblStock);
            Controls.Add(nudStockMinimo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private void LoadCategorias()
        {
            try
            {
                using var db = new PharmaContext();
                var categorias = db.Categorias.Select(c => new { c.CategoriaId, c.Nombre }).ToList();
                cbCategoria.DataSource = categorias;
                cbCategoria.DisplayMember = "Nombre";
                cbCategoria.ValueMember = "CategoriaId";
            }
            catch { }
        }

        private void LoadMedicamento()
        {
            try
            {
                using var db = new PharmaContext();
                var m = db.Medicamentos.Find(_medicamentoId);
                if (m == null) { MessageBox.Show("Producto no encontrado."); DialogResult = DialogResult.Cancel; return; }
                txtNombre.Text = m.Nombre;
                txtCodigo.Text = m.CodigoBarras;
                nudPrecioCompra.Value = m.PrecioCompra;
                nudPrecioVenta.Value = m.PrecioVenta;
                nudStockMinimo.Value = m.StockMinimo ?? 0;
                cbCategoria.SelectedValue = m.CategoriaId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando producto: {ex.Message}");
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { MessageBox.Show("Nombre requerido"); return; }
            try
            {
                using var db = new PharmaContext();
                var m = db.Medicamentos.Find(_medicamentoId);
                if (m == null) { MessageBox.Show("Producto no encontrado."); return; }
                m.Nombre = txtNombre.Text.Trim();
                m.CodigoBarras = string.IsNullOrWhiteSpace(txtCodigo.Text) ? null : txtCodigo.Text.Trim();
                m.PrecioCompra = nudPrecioCompra.Value;
                m.PrecioVenta = nudPrecioVenta.Value;
                m.StockMinimo = (int)nudStockMinimo.Value;
                m.CategoriaId = cbCategoria.SelectedValue is int id ? id : Convert.ToInt32(cbCategoria.SelectedValue);

                db.SaveChanges();
                MessageBox.Show("Producto actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error actualizando producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
