using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;

namespace PharmaSoft.Forms;

public partial class CuentasPagarForm : Form
{
    private readonly CuentasPorPagarService _cuentasPagarService;
    private readonly ProveedoreService _proveedorService;
    private readonly PagosProveedoreService _pagosService;

    public CuentasPagarForm(
        PharmaContext context,
        CuentasPorPagarService cuentasPagarService,
        ProveedoreService proveedorService,
        PagosProveedoreService pagosService)
    {
        InitializeComponent();
        _cuentasPagarService = cuentasPagarService;
        _proveedorService = proveedorService;
        _pagosService = pagosService;
    }

    private async void CuentasPagarForm_Load(object sender, EventArgs e)
    {
        await CargarCuentas();
    }

    private async Task CargarCuentas()
    {
        try
        {
            var cuentas = await _cuentasPagarService.GetList(c => c.Estado == "Pendiente");
            var proveedores = await _proveedorService.GetList(p => true);

            var data = cuentas.Select(c => new
            {
                c.CxPid,
                c.CompraId,
                Proveedor = proveedores.FirstOrDefault(x => x.ProveedorId == c.ProveedorId)?.NombreEmpresa ?? "Sin proveedor",
                c.MontoInicial,
                c.SaldoPendiente,
                FechaVencimiento = c.FechaVencimiento.ToString("dd/MM/yyyy"),
                c.Estado
            }).ToList();

            dgvCuentas.DataSource = data;
            dgvCuentas.Columns["CxPid"].Visible = false;
            dgvCuentas.Columns["CompraId"].HeaderText = "Compra ID";
            dgvCuentas.Columns["Proveedor"].HeaderText = "Proveedor";
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

        int cxpId = Convert.ToInt32(dgvCuentas.SelectedRows[0].Cells["CxPid"].Value);
        var cuenta = await _cuentasPagarService.Buscar(cxpId);
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
            var pago = new PagosProveedore
            {
                CxPid = cxpId,
                FechaPago = DateTime.Now,
                MontoPagado = monto
            };

            await _pagosService.Guardar(pago);

            var cuentaActualizada = await _cuentasPagarService.Buscar(cxpId);
            if (cuentaActualizada == null)
            {
                MessageBox.Show("Error al cargar la cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cuentaActualizada.SaldoPendiente -= monto;
            if (cuentaActualizada.SaldoPendiente <= 0)
            {
                cuentaActualizada.Estado = "Pagado";
            }

            await _cuentasPagarService.Guardar(cuentaActualizada);
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

        var cuentas = await _cuentasPagarService.GetList(c => c.Estado == "Pendiente");
        var proveedores = await _proveedorService.GetList(p => p.NombreEmpresa.Contains(criterio));
        var proveedorIds = proveedores.Select(p => p.ProveedorId).ToList();

        var filtrado = cuentas.Where(c => proveedorIds.Contains(c.ProveedorId)).ToList();

        var data = filtrado.Select(c => new
        {
            c.CxPid,
            c.CompraId,
            Proveedor = proveedores.FirstOrDefault(x => x.ProveedorId == c.ProveedorId)?.NombreEmpresa ?? "Sin proveedor",
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