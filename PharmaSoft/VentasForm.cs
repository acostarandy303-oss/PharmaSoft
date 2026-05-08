using System;
using System.Linq;
using System.Windows.Forms;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft
{
    public class VentasForm : Form
    {
        private ComboBox cbProducto;
        private NumericUpDown nudCantidad;
        private Label lblPrecio;
        private Label lblSubtotal;
        private DataGridView dataGridVentasTemp;
        private Button btnAgregarVenta;
        private Button btnFinalizarVenta;
        private Button btnCancelar;
        private decimal totalVentaTemp = 0m;

        public VentasForm()
        {
            InitializeComponent();
            LoadProductos();
            ConfigurarDataGridVentas();
        }

        private void InitializeComponent()
        {
            Text = "Registro de Ventas del Día";
            Width = 700;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;

            var lblTitulo = new Label { Text = "VENTAS DEL DÍA", Left = 20, Top = 10, Width = 300, Font = new Font("Arial", 14, FontStyle.Bold) };

            var lblProducto = new Label { Text = "Producto:", Left = 20, Top = 50, Width = 100 };
            cbProducto = new ComboBox { Left = 130, Top = 50, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblCantidad = new Label { Text = "Cantidad:", Left = 440, Top = 50, Width = 80 };
            nudCantidad = new NumericUpDown { Left = 530, Top = 50, Width = 80, Minimum = 1, Maximum = 10000 };

            var lblPrecioT = new Label { Text = "Precio Unitario:", Left = 20, Top = 90, Width = 100 };
            lblPrecio = new Label { Text = "0", Left = 130, Top = 90, Width = 100, BorderStyle = BorderStyle.FixedSingle };

            var lblSubtotalT = new Label { Text = "Subtotal:", Left = 440, Top = 90, Width = 80 };
            lblSubtotal = new Label { Text = "0", Left = 530, Top = 90, Width = 80, BorderStyle = BorderStyle.FixedSingle };

            btnAgregarVenta = new Button { Text = "➕ Agregar a Venta", Left = 130, Top = 130, Width = 150 };
            btnAgregarVenta.Click += BtnAgregarVenta_Click;

            var lblResumen = new Label { Text = "Resumen de Ventas del Día:", Left = 20, Top = 170, Width = 300, Font = new Font("Arial", 12, FontStyle.Bold) };

            dataGridVentasTemp = new DataGridView { Left = 20, Top = 200, Width = 640, Height = 200, ReadOnly = true, AllowUserToAddRows = false };

            var lblTotalVenta = new Label { Text = "Total de Ventas del Día:", Left = 20, Top = 410, Width = 200, Font = new Font("Arial", 11, FontStyle.Bold) };
            var lblTotalVentaValue = new Label { Text = "0", Left = 230, Top = 410, Width = 100, Font = new Font("Arial", 11, FontStyle.Bold), BackColor = System.Drawing.Color.LightGreen };
            lblTotalVentaValue.Name = "lblTotalVentaValue";

            btnFinalizarVenta = new Button { Text = "✅ Finalizar Venta", Left = 350, Top = 410, Width = 150, BackColor = System.Drawing.Color.LimeGreen };
            btnFinalizarVenta.Click += BtnFinalizarVenta_Click;

            btnCancelar = new Button { Text = "Cancelar", Left = 510, Top = 410, Width = 100 };
            btnCancelar.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(lblTitulo);
            Controls.Add(lblProducto);
            Controls.Add(cbProducto);
            Controls.Add(lblCantidad);
            Controls.Add(nudCantidad);
            Controls.Add(lblPrecioT);
            Controls.Add(lblPrecio);
            Controls.Add(lblSubtotalT);
            Controls.Add(lblSubtotal);
            Controls.Add(btnAgregarVenta);
            Controls.Add(lblResumen);
            Controls.Add(dataGridVentasTemp);
            Controls.Add(lblTotalVenta);
            Controls.Add(lblTotalVentaValue);
            Controls.Add(btnFinalizarVenta);
            Controls.Add(btnCancelar);

            cbProducto.SelectedIndexChanged += CbProducto_SelectedIndexChanged;
            nudCantidad.ValueChanged += NudCantidad_ValueChanged;
        }

        private void ConfigurarDataGridVentas()
        {
            dataGridVentasTemp.Columns.Clear();
            dataGridVentasTemp.AutoGenerateColumns = false;
            dataGridVentasTemp.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", HeaderText = "Producto", Width = 200 });
            dataGridVentasTemp.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", Width = 80 });
            var colPrecio = new DataGridViewTextBoxColumn { Name = "Precio", HeaderText = "Precio Unitario", Width = 120 };
            colPrecio.DefaultCellStyle.Format = "C2";
            dataGridVentasTemp.Columns.Add(colPrecio);
            var colSubtotal = new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", Width = 120 };
            colSubtotal.DefaultCellStyle.Format = "C2";
            dataGridVentasTemp.Columns.Add(colSubtotal);
        }

        private void LoadProductos()
        {
            try
            {
                using var db = new PharmaContext();
                var productos = db.Medicamentos
                    .Where(m => m.LotesInventarios.Sum(l => l.CantidadActual) > 0)
                    .Select(m => new
                    {
                        m.MedicamentoId,
                        m.Nombre,
                        m.PrecioVenta,
                        Stock = m.LotesInventarios.Sum(l => l.CantidadActual)
                    })
                    .ToList();

                cbProducto.DataSource = productos;
                cbProducto.DisplayMember = "Nombre";
                cbProducto.ValueMember = "MedicamentoId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando productos: {ex.Message}");
            }
        }

        private void CbProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbProducto.SelectedItem == null) return;

            // Acceder a propiedades del objeto dinámicamente
            var item = cbProducto.SelectedItem;
            var precioProperty = item.GetType().GetProperty("PrecioVenta");
            if (precioProperty != null)
            {
                var precio = (decimal)precioProperty.GetValue(item);
                lblPrecio.Text = precio.ToString("C2");
            }
            NudCantidad_ValueChanged(this, EventArgs.Empty);
        }

        private void NudCantidad_ValueChanged(object? sender, EventArgs e)
        {
            if (decimal.TryParse(lblPrecio.Text, System.Globalization.NumberStyles.Currency, null, out var precio))
            {
                var subtotal = precio * (int)nudCantidad.Value;
                lblSubtotal.Text = subtotal.ToString("C2");
            }
        }

        private void BtnAgregarVenta_Click(object? sender, EventArgs e)
        {
            if (cbProducto.SelectedValue == null) { MessageBox.Show("Selecciona un producto"); return; }

            var item = cbProducto.SelectedItem;
            var nombreProperty = item.GetType().GetProperty("Nombre");
            var nombre = (string)nombreProperty?.GetValue(item);

            var cantidad = (int)nudCantidad.Value;
            var precio = decimal.Parse(lblPrecio.Text, System.Globalization.NumberStyles.Currency);
            var subtotal = decimal.Parse(lblSubtotal.Text, System.Globalization.NumberStyles.Currency);

            // Agregar fila al grid de resumen
            dataGridVentasTemp.Rows.Add(nombre, cantidad, precio, subtotal);

            // Sumar al total
            totalVentaTemp += subtotal;
            var lblTotal = Controls.Find("lblTotalVentaValue", false).FirstOrDefault() as Label;
            if (lblTotal != null) lblTotal.Text = totalVentaTemp.ToString("C2");

            // Resetear
            nudCantidad.Value = 1;
            MessageBox.Show("Producto agregado a la venta.");
        }

        private void BtnFinalizarVenta_Click(object? sender, EventArgs e)
        {
            if (dataGridVentasTemp.Rows.Count == 0) { MessageBox.Show("Agrega productos a la venta"); return; }

            try
            {
                using var db = new PharmaContext();

                // Crear registro de Venta
                var venta = new Venta
                {
                    FechaVenta = DateTime.Now,
                    Total = totalVentaTemp
                };
                db.Ventas.Add(venta);
                db.SaveChanges();

                // Agregar DetalleVentas y restar stock
                foreach (DataGridViewRow row in dataGridVentasTemp.Rows)
                {
                    var nombreProducto = row.Cells["Producto"].Value.ToString();
                    var cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());
                    var precio = decimal.Parse(row.Cells["Precio"].Value.ToString().Replace("$", "").Replace(",", "."));

                    var medicamento = db.Medicamentos.FirstOrDefault(m => m.Nombre == nombreProducto);
                    if (medicamento == null) continue;

                    // Crear DetalleVenta
                    var detalleVenta = new DetalleVenta
                    {
                        VentaId = venta.VentaId,
                        LoteId = medicamento.LotesInventarios.FirstOrDefault()?.LoteId ?? 0,
                        Cantidad = cantidad,
                        PrecioUnitario = precio
                    };
                    db.DetalleVentas.Add(detalleVenta);

                    // Restar stock del lote
                    var lote = medicamento.LotesInventarios.FirstOrDefault();
                    if (lote != null)
                    {
                        lote.CantidadActual -= cantidad;
                    }
                }

                db.SaveChanges();
                MessageBox.Show($"Venta registrada exitosamente. Total: {totalVentaTemp:C2}", "Éxito");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finalizando venta: {ex.Message}", "Error");
            }
        }
    }
}
