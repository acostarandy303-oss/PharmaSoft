using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft
{
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

            btnAnadir.Click += BtnAnadir_Click;

            // Suscripción a eventos
            this.Load += PharmaSoft_Load;
            btnInventario.Click += BtnInventario_Click;
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
        }

        private async void BtnAnadir_Click(object? sender, EventArgs e)
        {
            // Instanciamos la ventana secundaria pasándole tu contexto de base de datos
            
        }

        // Solución a la nulabilidad: se agregó '?' después de 'object'
        private async void PharmaSoft_Load(object? sender, EventArgs e)
        {
            await CargarInventario();
        }

        private async void BtnInventario_Click(object? sender, EventArgs e)
        {
            await CargarInventario();
        }

        private async void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            await CargarInventario(txtBuscar.Text);
        }

        private async Task CargarInventario(string filtroBuscador = "")
        {
            try
            {
                dgvInventario.Rows.Clear();

                var medicamentos = string.IsNullOrWhiteSpace(filtroBuscador)
                    ? await _medicamentoService.GetList(m => true)
                    : await _medicamentoService.GetList(m => m.Nombre.Contains(filtroBuscador) || m.CodigoBarras == filtroBuscador);

                var lotes = await _lotesService.GetList(l => true);

                foreach (var med in medicamentos)
                {
                    var lotesDelMedicamento = lotes.Where(l => l.MedicamentoId == med.MedicamentoId).ToList();

                    int cantidadTotal = lotesDelMedicamento.Sum(l => l.CantidadActual);

                    string fechaCaducidad = "Sin lote";
                    if (lotesDelMedicamento.Any())
                    {
                        var loteMasProximo = lotesDelMedicamento.Min(l => l.FechaVencimiento);
                        fechaCaducidad = loteMasProximo.ToString("dd/MM/yyyy");
                    }

                    string laboratorio = med.Laboratorio ?? "Desconocido";
                    string codigo = string.IsNullOrEmpty(med.CodigoBarras) ? med.MedicamentoId.ToString() : med.CodigoBarras;

                    dgvInventario.Rows.Add(
                        codigo,
                        med.Nombre,
                        laboratorio,
                        cantidadTotal,
                        med.PrecioVenta.ToString("C2"),
                        fechaCaducidad
                    );
                }

                lblTotalProductos.Text = $"Total Productos: {medicamentos.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar el inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}