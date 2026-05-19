using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using System.Data;

namespace PharmaSoft.Forms;

public partial class CuentasCobrarForm : Form
{
    private readonly PharmaContext _context;
    private readonly CuentasPorCobrarService _cuentasCobrarService;
    private readonly ClienteService _clienteService;
    private readonly PagosClienteService _pagosService;

    public CuentasCobrarForm(
        PharmaContext context,
        CuentasPorCobrarService cuentasCobrarService,
        ClienteService clienteService,
        PagosClienteService pagosService)
    {
        InitializeComponent();
        _context = context;
        _cuentasCobrarService = cuentasCobrarService;
        _clienteService = clienteService;
        _pagosService = pagosService;
    }

    private async void CuentasCobrarForm_Load(object sender, EventArgs e)
    {
        await CargarCuentas();
    }

    private async Task CargarCuentas()
    {
        try
        {
            var cuentas = await _cuentasCobrarService.GetList(c => c.Estado == "Pendiente");
            var clientes = await _clienteService.GetList(c => true);

            var data = cuentas.Select(c => new
            {
                c.CxCid,
                c.VentaId,
                Cliente = clientes.FirstOrDefault(x => x.ClienteId == c.ClienteId)?.Nombre ?? "Sin cliente",
                c.MontoInicial,
                c.SaldoPendiente,
                FechaVencimiento = c.FechaVencimiento.ToString("dd/MM/yyyy"),
                c.Estado
            }).ToList();

            dgvCuentas.DataSource = data;
            dgvCuentas.Columns["CxCid"].Visible = false;
            dgvCuentas.Columns["VentaId"].HeaderText = "Venta ID";
            dgvCuentas.Columns["Cliente"].HeaderText = "Cliente";
            dgvCuentas.Columns["MontoInicial"].HeaderText = "Monto Inicial";
            dgvCuentas.Columns["SaldoPendiente"].HeaderText = "Saldo Pendiente";
            dgvCuentas.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
            dgvCuentas.Columns["Estado"].HeaderText = "Estado";

            decimal totalPendiente = cuentas.Sum(c => c.SaldoPendiente);
            lblTotal.Text = $"Total Pendiente: RD$ {totalPendiente:N2}";
            lblCantidad.Text = $"Cuentas: {cuentas.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnRegistrarPago_Click(object sender, EventArgs e)
    {
        if (dgvCuentas.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione una cuenta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int cxcId = Convert.ToInt32(dgvCuentas.SelectedRows[0].Cells["CxCid"].Value);
        var cuenta = await _cuentasCobrarService.Buscar(cxcId);
        if (cuenta == null) return;

        using var formPago = new FormInput("Registrar Pago", "Monto a pagar:");
        if (formPago.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(formPago.Valor))
            return;

        if (!decimal.TryParse(formPago.Valor, out decimal monto) || monto <= 0)
        {
            MessageBox.Show("Monto inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (monto > cuenta.SaldoPendiente)
        {
            MessageBox.Show("El monto no puede ser mayor al saldo pendiente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            var pago = new PagosCliente
            {
                CxCid = cxcId,
                FechaPago = DateTime.Now,
                MontoPagado = monto
            };

            await _pagosService.Guardar(pago);

            cuenta.SaldoPendiente -= monto;
            if (cuenta.SaldoPendiente <= 0)
            {
                cuenta.Estado = "Pagado";
            }

            await _cuentasCobrarService.Guardar(cuenta);
            MessageBox.Show("Pago registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await CargarCuentas();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        string criterio = txtBuscar.Text.Trim();
        if (string.IsNullOrEmpty(criterio))
        {
            await CargarCuentas();
            return;
        }

        var cuentas = await _cuentasCobrarService.GetList(c => c.Estado == "Pendiente");
        var clientes = await _clienteService.GetList(c => c.Nombre.Contains(criterio));
        var clienteIds = clientes.Select(c => c.ClienteId).ToList();

        var filtrado = cuentas.Where(c => clienteIds.Contains(c.ClienteId)).ToList();

        var data = filtrado.Select(c => new
        {
            c.CxCid,
            c.VentaId,
            Cliente = clientes.FirstOrDefault(x => x.ClienteId == c.ClienteId)?.Nombre ?? "Sin cliente",
            c.MontoInicial,
            c.SaldoPendiente,
            FechaVencimiento = c.FechaVencimiento.ToString("dd/MM/yyyy"),
            c.Estado
        }).ToList();

        dgvCuentas.DataSource = data;
    }

    private void btnCerrar_Click(object sender, EventArgs e)
    {
        Close();
    }
}

public class FormInput : Form
{
    private TextBox txtValor;
    public string Valor => txtValor.Text;

    public FormInput(string titulo, string etiqueta)
    {
        Text = titulo;
        Width = 350;
        Height = 150;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        var lbl = new Label { Text = etiqueta, Left = 20, Top = 20, Width = 300 };
        txtValor = new TextBox { Left = 20, Top = 45, Width = 300 };

        var btnAceptar = new Button { Text = "Aceptar", Left = 170, Top = 80, Width = 80, DialogResult = DialogResult.OK };
        var btnCancelar = new Button { Text = "Cancelar", Left = 250, Top = 80, Width = 80, DialogResult = DialogResult.Cancel };

        Controls.Add(lbl);
        Controls.Add(txtValor);
        Controls.Add(btnAceptar);
        Controls.Add(btnCancelar);
    }
}