namespace PharmaSoft
{
    partial class PharmaSoft
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Panel panelEstado;
        private System.Windows.Forms.Panel panelContenido;

        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnRecetas;
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnConfiguracion;

        private System.Windows.Forms.Label lblTituloCabecera;

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblTotalProductos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelLateral = new Panel();
            btnConfiguracion = new Button();
            btnReportes = new Button();
            btnCompras = new Button();
            btnRecetas = new Button();
            btnClientes = new Button();
            btnVentas = new Button();
            btnInventario = new Button();
            btnInicio = new Button();
            lblLogo = new Label();
            panelCabecera = new Panel();
            lblTituloCabecera = new Label();
            panelEstado = new Panel();
            lblTotalProductos = new Label();
            lblUsuario = new Label();
            panelContenido = new Panel();
            panelLateral.SuspendLayout();
            panelCabecera.SuspendLayout();
            panelEstado.SuspendLayout();
            SuspendLayout();
            // 
            // panelLateral
            // 
            panelLateral.BackColor = Color.FromArgb(248, 249, 250);
            panelLateral.Controls.Add(btnConfiguracion);
            panelLateral.Controls.Add(btnReportes);
            panelLateral.Controls.Add(btnCompras);
            panelLateral.Controls.Add(btnRecetas);
            panelLateral.Controls.Add(btnClientes);
            panelLateral.Controls.Add(btnVentas);
            panelLateral.Controls.Add(btnInventario);
            panelLateral.Controls.Add(btnInicio);
            panelLateral.Controls.Add(lblLogo);
            panelLateral.Dock = DockStyle.Left;
            panelLateral.Location = new Point(0, 0);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(220, 600);
            panelLateral.TabIndex = 0;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.Dock = DockStyle.Top;
            btnConfiguracion.FlatAppearance.BorderSize = 0;
            btnConfiguracion.FlatStyle = FlatStyle.Flat;
            btnConfiguracion.Font = new Font("Segoe UI", 10F);
            btnConfiguracion.Location = new Point(0, 430);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Padding = new Padding(20, 0, 0, 0);
            btnConfiguracion.Size = new Size(220, 45);
            btnConfiguracion.TabIndex = 0;
            btnConfiguracion.Text = "  Configuración";
            btnConfiguracion.TextAlign = ContentAlignment.MiddleLeft;
            btnConfiguracion.UseVisualStyleBackColor = true;
            // 
            // btnReportes
            // 
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 10F);
            btnReportes.Location = new Point(0, 385);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(20, 0, 0, 0);
            btnReportes.Size = new Size(220, 45);
            btnReportes.TabIndex = 1;
            btnReportes.Text = "  Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = true;
            // 
            // btnCompras
            // 
            btnCompras.Dock = DockStyle.Top;
            btnCompras.FlatAppearance.BorderSize = 0;
            btnCompras.FlatStyle = FlatStyle.Flat;
            btnCompras.Font = new Font("Segoe UI", 10F);
            btnCompras.Location = new Point(0, 340);
            btnCompras.Name = "btnCompras";
            btnCompras.Padding = new Padding(20, 0, 0, 0);
            btnCompras.Size = new Size(220, 45);
            btnCompras.TabIndex = 2;
            btnCompras.Text = "  Compras";
            btnCompras.TextAlign = ContentAlignment.MiddleLeft;
            btnCompras.UseVisualStyleBackColor = true;
            // 
            // btnRecetas
            // 
            btnRecetas.Dock = DockStyle.Top;
            btnRecetas.FlatAppearance.BorderSize = 0;
            btnRecetas.FlatStyle = FlatStyle.Flat;
            btnRecetas.Font = new Font("Segoe UI", 10F);
            btnRecetas.Location = new Point(0, 295);
            btnRecetas.Name = "btnRecetas";
            btnRecetas.Padding = new Padding(20, 0, 0, 0);
            btnRecetas.Size = new Size(220, 45);
            btnRecetas.TabIndex = 3;
            btnRecetas.Text = "  Recetas";
            btnRecetas.TextAlign = ContentAlignment.MiddleLeft;
            btnRecetas.UseVisualStyleBackColor = true;
            // 
            // btnClientes
            // 
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 10F);
            btnClientes.Location = new Point(0, 250);
            btnClientes.Name = "btnClientes";
            btnClientes.Padding = new Padding(20, 0, 0, 0);
            btnClientes.Size = new Size(220, 45);
            btnClientes.TabIndex = 4;
            btnClientes.Text = "  Clientes";
            btnClientes.TextAlign = ContentAlignment.MiddleLeft;
            btnClientes.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            btnVentas.Dock = DockStyle.Top;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Segoe UI", 10F);
            btnVentas.Location = new Point(0, 205);
            btnVentas.Name = "btnVentas";
            btnVentas.Padding = new Padding(20, 0, 0, 0);
            btnVentas.Size = new Size(220, 45);
            btnVentas.TabIndex = 5;
            btnVentas.Text = "  Ventas (Point of Sale)";
            btnVentas.TextAlign = ContentAlignment.MiddleLeft;
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.FromArgb(25, 118, 210);
            btnInventario.Dock = DockStyle.Top;
            btnInventario.FlatAppearance.BorderSize = 0;
            btnInventario.FlatStyle = FlatStyle.Flat;
            btnInventario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnInventario.ForeColor = Color.White;
            btnInventario.Location = new Point(0, 160);
            btnInventario.Name = "btnInventario";
            btnInventario.Padding = new Padding(20, 0, 0, 0);
            btnInventario.Size = new Size(220, 45);
            btnInventario.TabIndex = 6;
            btnInventario.Text = "  Inventario";
            btnInventario.TextAlign = ContentAlignment.MiddleLeft;
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnInicio
            // 
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Font = new Font("Segoe UI", 10F);
            btnInicio.Location = new Point(0, 115);
            btnInicio.Name = "btnInicio";
            btnInicio.Padding = new Padding(20, 0, 0, 0);
            btnInicio.Size = new Size(220, 45);
            btnInicio.TabIndex = 7;
            btnInicio.Text = "  Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleLeft;
            btnInicio.UseVisualStyleBackColor = true;
            btnInicio.Click += btnInicio_Click;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(11, 51, 100);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(220, 115);
            lblLogo.TabIndex = 8;
            lblLogo.Text = "+ PharmaSoft";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelCabecera
            // 
            panelCabecera.BackColor = Color.FromArgb(25, 118, 210);
            panelCabecera.Controls.Add(lblTituloCabecera);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(220, 0);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(830, 60);
            panelCabecera.TabIndex = 1;
            // 
            // lblTituloCabecera
            // 
            lblTituloCabecera.AutoSize = true;
            lblTituloCabecera.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTituloCabecera.ForeColor = Color.White;
            lblTituloCabecera.Location = new Point(20, 15);
            lblTituloCabecera.Name = "lblTituloCabecera";
            lblTituloCabecera.Size = new Size(184, 30);
            lblTituloCabecera.TabIndex = 0;
            lblTituloCabecera.Text = "Panel de Control";
            // 
            // panelEstado
            // 
            panelEstado.BackColor = Color.FromArgb(25, 118, 210);
            panelEstado.Controls.Add(lblTotalProductos);
            panelEstado.Controls.Add(lblUsuario);
            panelEstado.Dock = DockStyle.Bottom;
            panelEstado.Location = new Point(0, 600);
            panelEstado.Name = "panelEstado";
            panelEstado.Size = new Size(1050, 30);
            panelEstado.TabIndex = 2;
            // 
            // lblTotalProductos
            // 
            lblTotalProductos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalProductos.AutoSize = true;
            lblTotalProductos.ForeColor = Color.White;
            lblTotalProductos.Location = new Point(880, 8);
            lblTotalProductos.Name = "lblTotalProductos";
            lblTotalProductos.Size = new Size(119, 15);
            lblTotalProductos.TabIndex = 0;
            lblTotalProductos.Text = "Total Productos: 1450";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Location = new Point(10, 8);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(189, 15);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario: Admin (Farmacia Central)";
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.White;
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(220, 60);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(20);
            panelContenido.Size = new Size(830, 540);
            panelContenido.TabIndex = 0;
            // 
            // PharmaSoft
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 630);
            Controls.Add(panelContenido);
            Controls.Add(panelCabecera);
            Controls.Add(panelLateral);
            Controls.Add(panelEstado);
            MinimumSize = new Size(800, 500);
            Name = "PharmaSoft";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PharmaSoft - Gestión de Farmacia [v1.0]";
            Load += PharmaSoft_Load;
            panelLateral.ResumeLayout(false);
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            panelEstado.ResumeLayout(false);
            panelEstado.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
    }
}