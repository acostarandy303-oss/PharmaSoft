using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Forms;

public partial class ClienteForm : Form
{
    private readonly PharmaContext _context;
    private readonly ClienteService _clienteService;
    private Cliente? _clienteSeleccionado;

    public ClienteForm(
        PharmaContext context,
        ClienteService clienteService)
    {
        InitializeComponent();
        _context = context;
        _clienteService = clienteService;
    }

    private async void ClienteForm_Load(object sender, EventArgs e)
    {
        await CargarClientes();
    }

    private async Task CargarClientes()
    {
        var clientes = await _clienteService.GetList(c => true);
        dgvClientes.DataSource = clientes;
        dgvClientes.Columns["ClienteId"].Visible = false;
        dgvClientes.Columns["CuentasPorCobrars"].Visible = false;
    }

    private async void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        var criterio = txtBuscar.Text.Trim();
        if (string.IsNullOrEmpty(criterio))
        {
            await CargarClientes();
            return;
        }

        var clientes = await _clienteService.GetList(c =>
            c.Nombre.Contains(criterio) ||
            (c.RncCedula != null && c.RncCedula.Contains(criterio)) ||
            (c.Telefono != null && c.Telefono.Contains(criterio)));

        dgvClientes.DataSource = clientes;
    }

    private void dgvClientes_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvClientes.SelectedRows.Count > 0)
        {
            _clienteSeleccionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;
            if (_clienteSeleccionado != null)
            {
                txtNombre.Text = _clienteSeleccionado.Nombre;
                txtRncCedula.Text = _clienteSeleccionado.RncCedula ?? "";
                txtTelefono.Text = _clienteSeleccionado.Telefono ?? "";
                txtDireccion.Text = _clienteSeleccionado.Direccion ?? "";
                nudLimiteCredito.Value = _clienteSeleccionado.LimiteCredito ?? 0;
            }
        }
    }

    private async void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarCampos();
        _clienteSeleccionado = null;
        txtNombre.Focus();
    }

    private async void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            var cliente = _clienteSeleccionado ?? new Cliente();
            cliente.Nombre = txtNombre.Text.Trim();
            cliente.RncCedula = string.IsNullOrWhiteSpace(txtRncCedula.Text) ? null : txtRncCedula.Text.Trim();
            cliente.Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim();
            cliente.Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim();
            cliente.LimiteCredito = nudLimiteCredito.Value;

            var resultado = await _clienteService.Guardar(cliente);
            if (resultado)
            {
                MessageBox.Show("Cliente guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarClientes();
                LimpiarCampos();
                _clienteSeleccionado = null;
            }
            else
            {
                MessageBox.Show("Error al guardar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnEliminar_Click(object sender, EventArgs e)
    {
        if (_clienteSeleccionado == null || _clienteSeleccionado.ClienteId == 0)
        {
            MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmResult = MessageBox.Show($"¿Está seguro de eliminar el cliente '{_clienteSeleccionado.Nombre}'?",
            "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmResult != DialogResult.Yes) return;

        try
        {
            var resultado = await _clienteService.Eliminar(_clienteSeleccionado.ClienteId);
            if (resultado)
            {
                MessageBox.Show("Cliente eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarClientes();
                LimpiarCampos();
                _clienteSeleccionado = null;
            }
            else
            {
                MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void LimpiarCampos()
    {
        txtNombre.Clear();
        txtRncCedula.Clear();
        txtTelefono.Clear();
        txtDireccion.Clear();
        nudLimiteCredito.Value = 0;
        dgvClientes.ClearSelection();
    }

    public Cliente? ObtenerCliente()
    {
        return _clienteSeleccionado;
    }
}