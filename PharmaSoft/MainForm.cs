using System.ComponentModel;
using PharmaSoft.Data.Context;
using PharmaSoft.Data.Models;
using PharmaSoft.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaSoft.Forms;

namespace PharmaSoft;

public partial class PharmaSoft : Form
{
    public PharmaSoft()
    {
        InitializeComponent();
    }



    private void PharmaSoft_Load(object sender, EventArgs e)
    {
        foreach (Control control in panelLateral.Controls)
        {
            if (control is Button btn)
            {
                btn.Click += (s, args) => ActivarBoton(s);
            }
        }
        ActivarBoton(btnInicio);
    }

    private void ActivarBoton(object senderBoton)
    {
        if (senderBoton != null)
        {
            Button botonActivo = (Button)senderBoton;
            foreach (Control previousBtn in panelLateral.Controls)
            {
                if (previousBtn is Button btn)
                {
                    btn.BackColor = Color.FromArgb(248, 249, 250);
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }
            botonActivo.BackColor = Color.FromArgb(25, 118, 210);
            botonActivo.ForeColor = Color.White;
            botonActivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
    }

    private void btnInventario_Click(object sender, EventArgs e)
    {
        var inventarioForm = Program.ServiceProvider.GetRequiredService<InventarioForm>();
        inventarioForm.Show();
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
        var inicioForm = Program.ServiceProvider.GetRequiredService<PharmaSoft>();
        inicioForm.Show();
    }

    private void btnVentas_Click(object sender, EventArgs e)
    {
        var ventasForm = Program.ServiceProvider.GetRequiredService<VentaForm>();
        ventasForm.Show();
    }
}