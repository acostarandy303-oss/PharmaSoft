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
            // Mostrar el menú contextual definido en el diseñador debajo del botón
            try
            {
                var location = new System.Drawing.Point(0, btnInventario.Height);
                contextMenuStrip1.Show(btnInventario, location);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mostrando opciones de inventario: {ex.Message}", "Error",
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
    }
}
