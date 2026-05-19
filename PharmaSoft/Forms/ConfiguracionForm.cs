using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Forms;

public partial class ConfiguracionForm : Form
{
    private readonly PharmaContext _context;
    private readonly CategoriaService _categoriaService;
    private readonly ProveedoreService _proveedorService;

    public ConfiguracionForm(
        PharmaContext context,
        CategoriaService categoriaService,
        ProveedoreService proveedorService)
    {
        InitializeComponent();
        _context = context;
        _categoriaService = categoriaService;
        _proveedorService = proveedorService;
    }

    private async void ConfiguracionForm_Load(object sender, EventArgs e)
    {
        await CargarCategorias();
        await CargarProveedores();
        CargarDatosNegocio();
    }

    private async Task CargarCategorias()
    {
        var categorias = await _categoriaService.GetList(c => true);
        dgvCategorias.DataSource = categorias;
        dgvCategorias.Columns["CategoriaId"].Visible = false;
        dgvCategorias.Columns["CategoriaId"].HeaderText = "ID";
        dgvCategorias.Columns["Nombre"].HeaderText = "Nombre";
        dgvCategorias.Columns["Descripcion"].HeaderText = "Descripción";
    }

    private async Task CargarProveedores()
    {
        var proveedores = await _proveedorService.GetList(p => true);
        dgvProveedores.DataSource = proveedores;
        dgvProveedores.Columns["ProveedorId"].Visible = false;
        dgvProveedores.Columns["ProveedorId"].HeaderText = "ID";
        dgvProveedores.Columns["NombreEmpresa"].HeaderText = "Nombre";
        dgvProveedores.Columns["Contacto"].HeaderText = "Contacto";
        dgvProveedores.Columns["Telefono"].HeaderText = "Teléfono";
        dgvProveedores.Columns["Email"].HeaderText = "Correo";
    }

    private void CargarDatosNegocio()
    {
        txtNombreNegocio.Text = "PharmaSoft";
        txtDireccion.Text = "Dirección de la farmacia";
        txtTelefono.Text = "809-000-0000";
        txtRNC.Text = "000000000";
    }

    private async void btnGuardarCategoria_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNuevaCategoria.Text))
        {
            MessageBox.Show("Ingrese el nombre de la categoría", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var categoria = new Categoria
            {
                Nombre = txtNuevaCategoria.Text.Trim(),
                Descripcion = txtDescripcionCategoria.Text.Trim()
            };

            var resultado = await _categoriaService.Guardar(categoria);
            if (resultado)
            {
                MessageBox.Show("Categoría guardada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear();
                txtDescripcionCategoria.Clear();
                await CargarCategorias();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnGuardarProveedor_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
        {
            MessageBox.Show("Ingrese el nombre del proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var proveedor = new Proveedore
            {
                NombreEmpresa = txtNombreProveedor.Text.Trim(),
                Contacto = txtRncProveedor.Text.Trim(),
                Telefono = txtTelefonoProveedor.Text.Trim(),
                Email = txtCorreoProveedor.Text.Trim()
            };

            var resultado = await _proveedorService.Guardar(proveedor);
            if (resultado)
            {
                MessageBox.Show("Proveedor guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposProveedor();
                await CargarProveedores();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvProveedores.SelectedRows.Count > 0)
        {
            var proveedor = dgvProveedores.SelectedRows[0].DataBoundItem as Proveedore;
            if (proveedor != null)
            {
                txtNombreProveedor.Text = proveedor.NombreEmpresa;
                txtRncProveedor.Text = proveedor.Contacto ?? "";
                txtTelefonoProveedor.Text = proveedor.Telefono ?? "";
                txtCorreoProveedor.Text = proveedor.Email ?? "";
                txtDireccionProveedor.Text = "";
            }
        }
    }

    private async void btnEliminarProveedor_Click(object sender, EventArgs e)
    {
        if (dgvProveedores.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione un proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var proveedor = dgvProveedores.SelectedRows[0].DataBoundItem as Proveedore;
        if (proveedor == null) return;

        var confirm = MessageBox.Show($"¿Eliminar proveedor '{proveedor.NombreEmpresa}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes) return;

        try
        {
            var resultado = await _proveedorService.Eliminar(proveedor.ProveedorId);
            if (resultado)
            {
                MessageBox.Show("Proveedor eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarProveedores();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LimpiarCamposProveedor()
    {
        txtNombreProveedor.Clear();
        txtRncProveedor.Clear();
        txtTelefonoProveedor.Clear();
        txtCorreoProveedor.Clear();
        txtDireccionProveedor.Clear();
        dgvProveedores.ClearSelection();
    }

    private void btnCerrar_Click(object sender, EventArgs e)
    {
        Close();
    }
}