using System;
using System.Linq;
using System.Windows.Forms;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft
{
    public class IngresarProductoForm : Form
    {
        private TextBox txtNombre;
        private TextBox txtCodigo;
        private NumericUpDown nudPrecioVenta;
        private NumericUpDown nudPrecioCompra;
        private NumericUpDown nudStockMinimo;
        private ComboBox cbCategoria;
        private Button btnGuardar;
        private Button btnCancelar;

        public IngresarProductoForm()
        {
            InitializeComponent();
            LoadCategorias();
        }

        private void InitializeComponent()
        {
            Text = "Ingresar Producto";
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
                var categorias = db.Categorias
                    .Select(c => new { c.CategoriaId, c.Nombre })
                    .ToList();

                cbCategoria.DataSource = categorias;
                cbCategoria.DisplayMember = "Nombre";
                cbCategoria.ValueMember = "CategoriaId";
            }
            catch
            {
                // ignorar si hay error
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Nombre es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new PharmaContext();

                // Determinar CategoriaId: usar la seleccion, o crear/usar una categoria por defecto si no hay ninguna
                int categoriaId;
                if (cbCategoria.SelectedValue != null && int.TryParse(cbCategoria.SelectedValue.ToString(), out var parsedCatId))
                {
                    categoriaId = parsedCatId;
                }
                else
                {
                    var existingCat = db.Categorias.FirstOrDefault();
                    if (existingCat == null)
                    {
                        existingCat = new Categoria { Nombre = "Sin categoría", Descripcion = "Creada automáticamente" };
                        db.Categorias.Add(existingCat);
                        db.SaveChanges();
                    }
                    categoriaId = existingCat.CategoriaId;
                }

                // Determinar ProveedorId: asegurarse que exista al menos un proveedor, crear uno si no hay
                var proveedor = db.Proveedores.FirstOrDefault();
                if (proveedor == null)
                {
                    proveedor = new Proveedore { NombreEmpresa = "Proveedor por defecto", Contacto = "Auto" };
                    db.Proveedores.Add(proveedor);
                    db.SaveChanges();
                }

                var medicamento = new Medicamento
                {
                    Nombre = txtNombre.Text.Trim(),
                    CodigoBarras = string.IsNullOrWhiteSpace(txtCodigo.Text) ? null : txtCodigo.Text.Trim(),
                    CategoriaId = categoriaId,
                    PrecioCompra = nudPrecioCompra.Value,
                    PrecioVenta = nudPrecioVenta.Value,
                    StockMinimo = (int)nudStockMinimo.Value,
                    ProveedorId = proveedor.ProveedorId
                };

                db.Medicamentos.Add(medicamento);
                db.SaveChanges();

                MessageBox.Show($"Producto guardado con ID {medicamento.MedicamentoId}", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                MessageBox.Show($"Error de base de datos al guardar producto: {inner}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
