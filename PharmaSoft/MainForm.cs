using System;
using System.Windows.Forms;
using System.Linq;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;

namespace PharmaSoft
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarProductoToolStripMenuItem_Click(sender, e);
        }

        // Botón Medicamentos: abrir pantalla de gestión
        private void button2_Click(object sender, EventArgs e)
        {
            // Mostrar información introductoria y abrir la lista de productos
            MessageBox.Show("Aquí administras toda la información médica/farmacéutica.", "Medicamentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Mostrar columnas completas en el DataGridView
            try
            {
                using var db = new PharmaContext();
                var list = db.Medicamentos
                    .Select(m => new
                    {
                        m.MedicamentoId,
                        Codigo = m.CodigoBarras,
                        Medicamento = m.Nombre,
                        Descripcion = m.Descripcion,
                        Presentacion = m.Presentacion,
                        Laboratorio = m.Laboratorio,
                        Dosis = m.Dosis,
                        PrecioCompra = m.PrecioCompra,
                        PrecioVenta = m.PrecioVenta,
                        FechaVencimiento = m.LotesInventarios.Min(l => (DateTime?)l.FechaVencimiento.ToDateTime(new TimeOnly(0, 0)))
                    })
                    .ToList();

                BindMedicamentos(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando medicamentos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindMedicamentos(object list)
        {
            // Configura columnas y botóns Edit/Delete
            try
            {
                dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            }
            catch { }

            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            // Id (oculto)
            var colId = new DataGridViewTextBoxColumn()
            {
                Name = "MedicamentoId",
                DataPropertyName = "MedicamentoId",
                Visible = false
            };
            dataGridView1.Columns.Add(colId);

            // Columnas visibles
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", DataPropertyName = "Codigo", HeaderText = "Código", Width = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Medicamento", DataPropertyName = "Medicamento", HeaderText = "Medicamento", Width = 180 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descripcion", DataPropertyName = "Descripcion", HeaderText = "Descripción", Width = 200 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Presentacion", DataPropertyName = "Presentacion", HeaderText = "Presentación", Width = 120 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Laboratorio", DataPropertyName = "Laboratorio", HeaderText = "Laboratorio", Width = 120 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Dosis", DataPropertyName = "Dosis", HeaderText = "Dosis", Width = 100 });

            var colPrecioCompra = new DataGridViewTextBoxColumn { Name = "PrecioCompra", DataPropertyName = "PrecioCompra", HeaderText = "Precio compra", Width = 100 };
            colPrecioCompra.DefaultCellStyle.Format = "C2";
            dataGridView1.Columns.Add(colPrecioCompra);

            var colPrecioVenta = new DataGridViewTextBoxColumn { Name = "PrecioVenta", DataPropertyName = "PrecioVenta", HeaderText = "Precio venta", Width = 100 };
            colPrecioVenta.DefaultCellStyle.Format = "C2";
            dataGridView1.Columns.Add(colPrecioVenta);

            var colVenc = new DataGridViewTextBoxColumn { Name = "FechaVencimiento", DataPropertyName = "FechaVencimiento", HeaderText = "Vencimiento", Width = 120 };
            colVenc.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colVenc);

            // Botones
            var editCol = new DataGridViewButtonColumn { Name = "Edit", HeaderText = "", Text = "Editar", UseColumnTextForButtonValue = true, Width = 70 };
            var delCol = new DataGridViewButtonColumn { Name = "Delete", HeaderText = "", Text = "Eliminar", UseColumnTextForButtonValue = true, Width = 80 };
            dataGridView1.Columns.Add(editCol);
            dataGridView1.Columns.Add(delCol);

            dataGridView1.DataSource = list;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = dataGridView1;
            var col = grid.Columns[e.ColumnIndex];
            if (col == null) return;

            if (col.Name == "Edit")
            {
                var idObj = grid.Rows[e.RowIndex].Cells["MedicamentoId"].Value;
                if (idObj == null) return;
                if (!int.TryParse(idObj.ToString(), out var id)) return;
                using var form = new EditarProductoForm(id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // recargar la vista de medicamentos
                    button2_Click(this, EventArgs.Empty);
                }
            }
            else if (col.Name == "Delete")
            {
                var idObj = grid.Rows[e.RowIndex].Cells["MedicamentoId"].Value;
                if (idObj == null) return;
                if (!int.TryParse(idObj.ToString(), out var id)) return;
                var confirm = MessageBox.Show("¿Eliminar producto seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
                try
                {
                    using var db = new PharmaContext();
                    var m = db.Medicamentos.Find(id);
                    if (m != null)
                    {
                        db.Medicamentos.Remove(m);
                        db.SaveChanges();
                    }
                    button2_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error eliminando producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Manejadores adicionales (básicos) para nuevas opciones
        private void verInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar todos los campos recomendados en la tabla
            try
            {
                using var db = new PharmaContext();
                // Mostrar un listado simple: Código, Medicamento, Stock, Precio, Vence
                var list = db.Medicamentos
                    .Select(m => new
                    {
                        MedicamentoId = m.MedicamentoId,
                        Codigo = m.CodigoBarras,
                        Medicamento = m.Nombre,
                        Stock = m.LotesInventarios.Sum(l => l.CantidadActual),
                        Precio = m.PrecioVenta,
                        Vence = m.LotesInventarios.Min(l => (DateTime?)l.FechaVencimiento.ToDateTime(new TimeOnly(0, 0)))
                    })
                    .ToList();

                // Bind y formato simple
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Codigo", DataPropertyName = "Codigo", HeaderText = "Código", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Medicamento", DataPropertyName = "Medicamento", HeaderText = "Medicamento", Width = 220 });
                var colStock = new DataGridViewTextBoxColumn { Name = "Stock", DataPropertyName = "Stock", HeaderText = "Stock", Width = 80 };
                dataGridView1.Columns.Add(colStock);
                var colPrecio = new DataGridViewTextBoxColumn { Name = "Precio", DataPropertyName = "Precio", HeaderText = "Precio", Width = 100 };
                colPrecio.DefaultCellStyle.Format = "C2";
                dataGridView1.Columns.Add(colPrecio);
                var colVence = new DataGridViewTextBoxColumn { Name = "Vence", DataPropertyName = "Vence", HeaderText = "Vence", Width = 120 };
                colVence.DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns.Add(colVence);

                // Botones principales: Agregar, Editar, Eliminar (botones fuera del grid)
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reutilizar búsqueda desde la barra
            var input = textBoxBuscar?.Text;
            if (string.IsNullOrWhiteSpace(input)) return;
            try
            {
                using var db = new PharmaContext();
                var list = db.Medicamentos
                    .Where(m => m.Nombre.Contains(input) || (m.CodigoBarras != null && m.CodigoBarras.Contains(input)))
                    .Select(m => new { m.MedicamentoId, Codigo = m.CodigoBarras, Medicamento = m.Nombre, Stock = m.LotesInventarios.Sum(l => l.CantidadActual), Precio = m.PrecioVenta, Vence = m.LotesInventarios.Min(l => (DateTime?)l.FechaVencimiento.ToDateTime(new TimeOnly(0, 0))) })
                    .ToList();

                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error buscando producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void agregarMedicamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ingresarProductoToolStripMenuItem_Click(sender, e);
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) { MessageBox.Show("Seleccione un producto en la tabla."); return; }
            var idObj = dataGridView1.CurrentRow.Cells["MedicamentoId"].Value ?? dataGridView1.CurrentRow.Cells[0].Value;
            if (!int.TryParse(idObj?.ToString(), out var id)) { MessageBox.Show("ID de producto no válido."); return; }

            using var form = new EditarProductoForm(id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProductos();
            }
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) { MessageBox.Show("Seleccione un producto en la tabla."); return; }
            var idObj = dataGridView1.CurrentRow.Cells["MedicamentoId"].Value ?? dataGridView1.CurrentRow.Cells[0].Value;
            if (!int.TryParse(idObj?.ToString(), out var id)) { MessageBox.Show("ID de producto no válido."); return; }

            var confirm = MessageBox.Show("¿Eliminar producto seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new PharmaContext();
                var m = db.Medicamentos.Find(id);
                if (m == null) { MessageBox.Show("Producto no encontrado."); return; }
                db.Medicamentos.Remove(m);
                db.SaveChanges();
                LoadProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error eliminando producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ajusteStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ajuste de stock: implementar formulario de ajuste.");
        }

        private void entradaMercanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Entrada de mercancía: implementar registro de lote/entrada.");
        }

        private void salidaMercanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Salida de mercancía: implementar reducción de stock por venta/ajuste.");
        }

        private void productosAgotadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new PharmaContext();
                var list = db.Medicamentos
                    .Where(m => m.LotesInventarios.Sum(l => l.CantidadActual) <= 0)
                    .Select(m => new { m.MedicamentoId, m.CodigoBarras, m.Nombre })
                    .ToList();
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error consultando productos agotados: {ex.Message}");
            }
        }

        private void proximosAVencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var db = new PharmaContext();
                var threshold = DateOnly.FromDateTime(DateTime.Today.AddDays(30));
                var list = db.LotesInventarios
                    .Where(l => l.FechaVencimiento <= threshold)
                    .Select(l => new { l.LoteId, l.MedicamentoId, l.NumeroLote, FechaVencimiento = l.FechaVencimiento, l.CantidadActual })
                    .ToList();
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error consultando proximos a vencer: {ex.Message}");
            }
        }

        // Manejadores para opciones del menú de Inventario
        private void ingresarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir formulario modal para ingresar producto
            using var form = new IngresarProductoForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Si el producto se guardó, actualizar la lista de productos si está visible
                LoadProductos();
            }
        }

        private void listaProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar la lista de productos en el DataGridView
            LoadProductos();
        }

        private void LoadProductos()
        {
            try
            {
                using var db = new PharmaContext();
                var list = db.Medicamentos
                    .Select(m => new
                    {
                        m.MedicamentoId,
                        m.CodigoBarras,
                        m.Nombre,
                        m.PrecioVenta,
                        m.PrecioCompra,
                        m.StockMinimo
                    })
                    .ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategorias();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        // Al hacer clic en Inventario mostrar opciones: Ingresar producto / Lista productos
        private void btnclickInventario(object sender, EventArgs e)
        {
            // Mostrar la vista de Inventario: barra de búsqueda, tabla simple y botones
            try
            {
                // Asegurar que la barra de búsqueda y los botones estén visibles
                if (textBoxBuscar != null) textBoxBuscar.Visible = true;
                if (btnBuscar != null) btnBuscar.Visible = true;
                if (btnAgregar != null) btnAgregar.Visible = true;
                if (btnEditar != null) btnEditar.Visible = true;
                if (btnEliminar != null) btnEliminar.Visible = true;

                // Cargar y mostrar el inventario simple
                verInventarioToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mostrando inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategorias()
        {
            try
            {
                using var db = new PharmaContext();
                // Seleccionar campos relevantes para evitar problemas con entidades proxy
                var list = db.Categorias
                    .Select(c => new { c.CategoriaId, c.Nombre, c.Descripcion })
                    .ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                // Mostrar error opcionalmente, pero no interrumpir la carga del formulario
                Console.Error.WriteLine($"Error cargando categorías: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Botón Ventas (button5)
        public void button5_Click(object sender, EventArgs e)
        {
            // Abrir formulario de ventas
            using var form = new VentasForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Cargar resumen de ventas del día
                CargarResumenVentasDelDia();
            }
        }

        private void CargarResumenVentasDelDia()
        {
            try
            {
                using var db = new PharmaContext();
                var hoy = DateTime.Today;

                // Obtener ventas del día con detalles
                var ventasDelDia = db.Ventas
                    .Where(v => v.FechaVenta.HasValue && v.FechaVenta.Value.Date == hoy)
                    .SelectMany(v => v.DetalleVenta.Select(dv => new
                    {
                        Producto = dv.Lote.Medicamento.Nombre,
                        Cantidad = dv.Cantidad,
                        PrecioUnitario = dv.PrecioUnitario,
                        Subtotal = dv.Cantidad * dv.PrecioUnitario,
                        FechaVenta = v.FechaVenta
                    }))
                    .ToList();

                // Configurar DataGridView para resumen
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", DataPropertyName = "Producto", HeaderText = "Producto", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", HeaderText = "Cantidad", Width = 80 });
                var colPrecio = new DataGridViewTextBoxColumn { Name = "PrecioUnitario", DataPropertyName = "PrecioUnitario", HeaderText = "Precio Unitario", Width = 120 };
                colPrecio.DefaultCellStyle.Format = "C2";
                dataGridView1.Columns.Add(colPrecio);
                var colSubtotal = new DataGridViewTextBoxColumn { Name = "Subtotal", DataPropertyName = "Subtotal", HeaderText = "Subtotal", Width = 120 };
                colSubtotal.DefaultCellStyle.Format = "C2";
                dataGridView1.Columns.Add(colSubtotal);

                dataGridView1.DataSource = ventasDelDia;

                // Calcular total del día
                var totalDelDia = ventasDelDia.Sum(v => v.Subtotal);
                label3.Text = $"Ventas del Día - Total: {totalDelDia:C2}";

                // Mostrar en panel3
                label4.Text = "Resumen de Ventas del Día";
                label5.Text = $"Total: {totalDelDia:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando resumen de ventas: {ex.Message}", "Error");
            }
        }
    }
}
