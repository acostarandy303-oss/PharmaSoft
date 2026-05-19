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
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Button btnCuentasCobrar;
        private System.Windows.Forms.Button btnCuentasPagar;

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
            btnCuentasPagar = new Button();
            btnCuentasCobrar = new Button();
            btnCompras = new Button();
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
            lbTotalCxCobrar = new Label();
            lbCuentasXCobrar = new Label();
            lbCatidadVentas = new Label();
            lbVentasDiarias = new Label();
            lbCantidadStock = new Label();
            lbStock = new Label();
            lbTitulo = new Label();
            lbStockDisp = new Label();
            panelLateral.SuspendLayout();
            panelCabecera.SuspendLayout();
            panelEstado.SuspendLayout();
            panelContenido.SuspendLayout();
            SuspendLayout();
            // 
            // panelLateral
            // 
            panelLateral.BackColor = Color.FromArgb(248, 249, 250);
            panelLateral.Controls.Add(btnConfiguracion);
            panelLateral.Controls.Add(btnCuentasPagar);
            panelLateral.Controls.Add(btnCuentasCobrar);
            panelLateral.Controls.Add(btnReportes);
            panelLateral.Controls.Add(btnCompras);
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
            btnConfiguracion.Location = new Point(0, 385);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Padding = new Padding(20, 0, 0, 0);
            btnConfiguracion.Size = new Size(220, 45);
            btnConfiguracion.TabIndex = 0;
            btnConfiguracion.Text = "  Configuración";
            btnConfiguracion.TextAlign = ContentAlignment.MiddleLeft;
            btnConfiguracion.UseVisualStyleBackColor = true;
            btnConfiguracion.Click += btnConfiguracion_Click;
            // 
            // btnReportes
            // 
            btnReportes.Dock = DockStyle.Top;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 10F);
            btnReportes.Location = new Point(0, 340);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(20, 0, 0, 0);
            btnReportes.Size = new Size(220, 45);
            btnReportes.TabIndex = 1;
            btnReportes.Text = "  Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnCuentasPagar
            // 
            btnCuentasPagar.Dock = DockStyle.Top;
            btnCuentasPagar.FlatAppearance.BorderSize = 0;
            btnCuentasPagar.FlatStyle = FlatStyle.Flat;
            btnCuentasPagar.Font = new Font("Segoe UI", 10F);
            btnCuentasPagar.Location = new Point(0, 385);
            btnCuentasPagar.Name = "btnCuentasPagar";
            btnCuentasPagar.Padding = new Padding(20, 0, 0, 0);
            btnCuentasPagar.Size = new Size(220, 45);
            btnCuentasPagar.TabIndex = 17;
            btnCuentasPagar.Text = "  Cuentas por Pagar";
            btnCuentasPagar.TextAlign = ContentAlignment.MiddleLeft;
            btnCuentasPagar.UseVisualStyleBackColor = true;
            btnCuentasPagar.Click += btnCuentasPagar_Click;
            // 
            // btnCuentasCobrar
            // 
            btnCuentasCobrar.Dock = DockStyle.Top;
            btnCuentasCobrar.FlatAppearance.BorderSize = 0;
            btnCuentasCobrar.FlatStyle = FlatStyle.Flat;
            btnCuentasCobrar.Font = new Font("Segoe UI", 10F);
            btnCuentasCobrar.Location = new Point(0, 430);
            btnCuentasCobrar.Name = "btnCuentasCobrar";
            btnCuentasCobrar.Padding = new Padding(20, 0, 0, 0);
            btnCuentasCobrar.Size = new Size(220, 45);
            btnCuentasCobrar.TabIndex = 18;
            btnCuentasCobrar.Text = "  Cuentas por Cobrar";
            btnCuentasCobrar.TextAlign = ContentAlignment.MiddleLeft;
            btnCuentasCobrar.UseVisualStyleBackColor = true;
            btnCuentasCobrar.Click += btnCuentasCobrar_Click;
            // 
            // btnCompras
            // 
            btnCompras.Dock = DockStyle.Top;
            btnCompras.FlatAppearance.BorderSize = 0;
            btnCompras.FlatStyle = FlatStyle.Flat;
            btnCompras.Font = new Font("Segoe UI", 10F);
            btnCompras.Location = new Point(0, 295);
            btnCompras.Name = "btnCompras";
            btnCompras.Padding = new Padding(20, 0, 0, 0);
            btnCompras.Size = new Size(220, 45);
            btnCompras.TabIndex = 2;
            btnCompras.Text = "  Compras";
            btnCompras.TextAlign = ContentAlignment.MiddleLeft;
            btnCompras.UseVisualStyleBackColor = true;
            btnCompras.Click += btnCompras_Click;
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
            btnClientes.Click += btnClientes_Click;
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
            panelContenido.Controls.Add(lbTotalCxCobrar);
            panelContenido.Controls.Add(lbCuentasXCobrar);
            panelContenido.Controls.Add(lbCatidadVentas);
            panelContenido.Controls.Add(lbVentasDiarias);
            panelContenido.Controls.Add(lbCantidadStock);
            panelContenido.Controls.Add(lbStock);
            panelContenido.Controls.Add(lbTitulo);
            panelContenido.Controls.Add(lbStockDisp);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(20);
            panelContenido.Size = new Size(1050, 630);
            panelContenido.TabIndex = 0;
            // 
            // lbTotalCxCobrar
            // 
            lbTotalCxCobrar.AutoSize = true;
            lbTotalCxCobrar.Location = new Point(757, 190);
            lbTotalCxCobrar.Name = "lbTotalCxCobrar";
            lbTotalCxCobrar.Size = new Size(13, 15);
            lbTotalCxCobrar.TabIndex = 7;
            lbTotalCxCobrar.Text = "0";
            // 
            // lbCuentasXCobrar
            // 
            lbCuentasXCobrar.AutoSize = true;
            lbCuentasXCobrar.Location = new Point(707, 160);
            lbCuentasXCobrar.Name = "lbCuentasXCobrar";
            lbCuentasXCobrar.Size = new Size(110, 15);
            lbCuentasXCobrar.TabIndex = 6;
            lbCuentasXCobrar.Text = "Cuentas por Cobrar";
            // 
            // lbCatidadVentas
            // 
            lbCatidadVentas.AutoSize = true;
            lbCatidadVentas.Location = new Point(541, 190);
            lbCatidadVentas.Name = "lbCatidadVentas";
            lbCatidadVentas.Size = new Size(13, 15);
            lbCatidadVentas.TabIndex = 5;
            lbCatidadVentas.Text = "0";
            // 
            // lbVentasDiarias
            // 
            lbVentasDiarias.AutoSize = true;
            lbVentasDiarias.Location = new Point(514, 160);
            lbVentasDiarias.Name = "lbVentasDiarias";
            lbVentasDiarias.Size = new Size(69, 15);
            lbVentasDiarias.TabIndex = 4;
            lbVentasDiarias.Text = "Total Ventas";
            // 
            // lbCantidadStock
            // 
            lbCantidadStock.AutoSize = true;
            lbCantidadStock.Location = new Point(305, 190);
            lbCantidadStock.Name = "lbCantidadStock";
            lbCantidadStock.Size = new Size(13, 15);
            lbCantidadStock.TabIndex = 3;
            lbCantidadStock.Text = "0";
            // 
            // lbStock
            // 
            lbStock.AutoSize = true;
            lbStock.Location = new Point(279, 160);
            lbStock.Name = "lbStock";
            lbStock.Size = new Size(73, 15);
            lbStock.TabIndex = 2;
            lbStock.Text = "Stock Actual";
            // 
            // lbTitulo
            // 
            lbTitulo.AutoSize = true;
            lbTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitulo.ForeColor = Color.MidnightBlue;
            lbTitulo.Location = new Point(240, 86);
            lbTitulo.Name = "lbTitulo";
            lbTitulo.Size = new Size(198, 45);
            lbTitulo.TabIndex = 1;
            lbTitulo.Text = "PharmaSoft";
            // 
            // lbStockDisp
            // 
            lbStockDisp.AutoSize = true;
            lbStockDisp.Location = new Point(64, 116);
            lbStockDisp.Name = "lbStockDisp";
            lbStockDisp.Size = new Size(94, 15);
            lbStockDisp.TabIndex = 0;
            lbStockDisp.Text = "Stock disponible";
            // 
            // PharmaSoft
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 630);
            Controls.Add(panelCabecera);
            Controls.Add(panelLateral);
            Controls.Add(panelEstado);
            Controls.Add(panelContenido);
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
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private Label lbStockDisp;
        private Label lbTitulo;
        private Label lbCantidadStock;
        private Label lbStock;
        private Label lbVentasDiarias;
        private Label lbCatidadVentas;
        private Label lbTotalCxCobrar;
        private Label lbCuentasXCobrar;
    }
}