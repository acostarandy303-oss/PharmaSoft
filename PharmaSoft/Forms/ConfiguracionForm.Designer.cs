namespace PharmaSoft.Forms
{
    partial class ConfiguracionForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDatosNegocio;
        private System.Windows.Forms.TabPage tabCategorias;
        private System.Windows.Forms.TabPage tabProveedores;
        private System.Windows.Forms.Label lblNombreNegocio;
        private System.Windows.Forms.TextBox txtNombreNegocio;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblRNC;
        private System.Windows.Forms.TextBox txtRNC;
        private System.Windows.Forms.Label lblNuevaCategoria;
        private System.Windows.Forms.TextBox txtNuevaCategoria;
        private System.Windows.Forms.Label lblDescripcionCategoria;
        private System.Windows.Forms.TextBox txtDescripcionCategoria;
        private System.Windows.Forms.Button btnGuardarCategoria;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.Label lblRncProveedor;
        private System.Windows.Forms.TextBox txtRncProveedor;
        private System.Windows.Forms.Label lblTelefonoProveedor;
        private System.Windows.Forms.TextBox txtTelefonoProveedor;
        private System.Windows.Forms.Label lblCorreoProveedor;
        private System.Windows.Forms.TextBox txtCorreoProveedor;
        private System.Windows.Forms.Label lblDireccionProveedor;
        private System.Windows.Forms.TextBox txtDireccionProveedor;
        private System.Windows.Forms.Button btnGuardarProveedor;
        private System.Windows.Forms.Button btnEliminarProveedor;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Button btnCerrar;

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
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDatosNegocio = new System.Windows.Forms.TabPage();
            this.lblNombreNegocio = new System.Windows.Forms.Label();
            this.txtNombreNegocio = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblRNC = new System.Windows.Forms.Label();
            this.txtRNC = new System.Windows.Forms.TextBox();
            this.tabCategorias = new System.Windows.Forms.TabPage();
            this.lblNuevaCategoria = new System.Windows.Forms.Label();
            this.txtNuevaCategoria = new System.Windows.Forms.TextBox();
            this.lblDescripcionCategoria = new System.Windows.Forms.Label();
            this.txtDescripcionCategoria = new System.Windows.Forms.TextBox();
            this.btnGuardarCategoria = new System.Windows.Forms.Button();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.tabProveedores = new System.Windows.Forms.TabPage();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.lblRncProveedor = new System.Windows.Forms.Label();
            this.txtRncProveedor = new System.Windows.Forms.TextBox();
            this.lblTelefonoProveedor = new System.Windows.Forms.Label();
            this.txtTelefonoProveedor = new System.Windows.Forms.TextBox();
            this.lblCorreoProveedor = new System.Windows.Forms.Label();
            this.txtCorreoProveedor = new System.Windows.Forms.TextBox();
            this.lblDireccionProveedor = new System.Windows.Forms.Label();
            this.txtDireccionProveedor = new System.Windows.Forms.TextBox();
            this.btnGuardarProveedor = new System.Windows.Forms.Button();
            this.btnEliminarProveedor = new System.Windows.Forms.Button();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelTitulo.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDatosNegocio.SuspendLayout();
            this.tabCategorias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvCategorias).BeginInit();
            this.tabProveedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvProveedores).BeginInit();
            this.SuspendLayout();

            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(700, 60);
            this.panelTitulo.TabIndex = 0;

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(143, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configuración";

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDatosNegocio);
            this.tabControl.Controls.Add(this.tabCategorias);
            this.tabControl.Controls.Add(this.tabProveedores);
            this.tabControl.Location = new System.Drawing.Point(20, 80);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(660, 420);
            this.tabControl.TabIndex = 1;

            // 
            // tabDatosNegocio
            // 
            this.tabDatosNegocio.Controls.Add(this.lblNombreNegocio);
            this.tabDatosNegocio.Controls.Add(this.txtNombreNegocio);
            this.tabDatosNegocio.Controls.Add(this.lblDireccion);
            this.tabDatosNegocio.Controls.Add(this.txtDireccion);
            this.tabDatosNegocio.Controls.Add(this.lblTelefono);
            this.tabDatosNegocio.Controls.Add(this.txtTelefono);
            this.tabDatosNegocio.Controls.Add(this.lblRNC);
            this.tabDatosNegocio.Controls.Add(this.txtRNC);
            this.tabDatosNegocio.Location = new System.Drawing.Point(4, 24);
            this.tabDatosNegocio.Name = "tabDatosNegocio";
            this.tabDatosNegocio.Padding = new System.Windows.Forms.Padding(10);
            this.tabDatosNegocio.Size = new System.Drawing.Size(652, 392);
            this.tabDatosNegocio.TabIndex = 0;
            this.tabDatosNegocio.Text = "Datos del Negocio";
            this.tabDatosNegocio.UseVisualStyleBackColor = true;

            // 
            // lblNombreNegocio
            // 
            this.lblNombreNegocio.AutoSize = true;
            this.lblNombreNegocio.Location = new System.Drawing.Point(30, 40);
            this.lblNombreNegocio.Name = "lblNombreNegocio";
            this.lblNombreNegocio.Size = new System.Drawing.Size(107, 15);
            this.lblNombreNegocio.TabIndex = 0;
            this.lblNombreNegocio.Text = "Nombre del Negocio:";

            // 
            // txtNombreNegocio
            // 
            this.txtNombreNegocio.Location = new System.Drawing.Point(30, 58);
            this.txtNombreNegocio.Name = "txtNombreNegocio";
            this.txtNombreNegocio.Size = new System.Drawing.Size(300, 23);
            this.txtNombreNegocio.TabIndex = 1;

            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(30, 100);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(67, 15);
            this.lblDireccion.TabIndex = 2;
            this.lblDireccion.Text = "Dirección:";

            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(30, 118);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(300, 60);
            this.txtDireccion.TabIndex = 3;

            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(30, 200);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(63, 15);
            this.lblTelefono.TabIndex = 4;
            this.lblTelefono.Text = "Teléfono:";

            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(30, 218);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(150, 23);
            this.txtTelefono.TabIndex = 5;

            // 
            // lblRNC
            // 
            this.lblRNC.AutoSize = true;
            this.lblRNC.Location = new System.Drawing.Point(30, 260);
            this.lblRNC.Name = "lblRNC";
            this.lblRNC.Size = new System.Drawing.Size(36, 15);
            this.lblRNC.TabIndex = 6;
            this.lblRNC.Text = "RNC:";

            // 
            // txtRNC
            // 
            this.txtRNC.Location = new System.Drawing.Point(30, 278);
            this.txtRNC.Name = "txtRNC";
            this.txtRNC.Size = new System.Drawing.Size(150, 23);
            this.txtRNC.TabIndex = 7;

            // 
            // tabCategorias
            // 
            this.tabCategorias.Controls.Add(this.lblNuevaCategoria);
            this.tabCategorias.Controls.Add(this.txtNuevaCategoria);
            this.tabCategorias.Controls.Add(this.lblDescripcionCategoria);
            this.tabCategorias.Controls.Add(this.txtDescripcionCategoria);
            this.tabCategorias.Controls.Add(this.btnGuardarCategoria);
            this.tabCategorias.Controls.Add(this.dgvCategorias);
            this.tabCategorias.Location = new System.Drawing.Point(4, 24);
            this.tabCategorias.Name = "tabCategorias";
            this.tabCategorias.Padding = new System.Windows.Forms.Padding(10);
            this.tabCategorias.Size = new System.Drawing.Size(652, 392);
            this.tabCategorias.TabIndex = 1;
            this.tabCategorias.Text = "Categorías";
            this.tabCategorias.UseVisualStyleBackColor = true;

            // 
            // lblNuevaCategoria
            // 
            this.lblNuevaCategoria.AutoSize = true;
            this.lblNuevaCategoria.Location = new System.Drawing.Point(30, 30);
            this.lblNuevaCategoria.Name = "lblNuevaCategoria";
            this.lblNuevaCategoria.Size = new System.Drawing.Size(51, 15);
            this.lblNuevaCategoria.TabIndex = 0;
            this.lblNuevaCategoria.Text = "Nombre:";

            // 
            // txtNuevaCategoria
            // 
            this.txtNuevaCategoria.Location = new System.Drawing.Point(30, 48);
            this.txtNuevaCategoria.Name = "txtNuevaCategoria";
            this.txtNuevaCategoria.Size = new System.Drawing.Size(250, 23);
            this.txtNuevaCategoria.TabIndex = 1;

            // 
            // lblDescripcionCategoria
            // 
            this.lblDescripcionCategoria.AutoSize = true;
            this.lblDescripcionCategoria.Location = new System.Drawing.Point(300, 30);
            this.lblDescripcionCategoria.Name = "lblDescripcionCategoria";
            this.lblDescripcionCategoria.Size = new System.Drawing.Size(80, 15);
            this.lblDescripcionCategoria.TabIndex = 2;
            this.lblDescripcionCategoria.Text = "Descripción:";

            // 
            // txtDescripcionCategoria
            // 
            this.txtDescripcionCategoria.Location = new System.Drawing.Point(300, 48);
            this.txtDescripcionCategoria.Name = "txtDescripcionCategoria";
            this.txtDescripcionCategoria.Size = new System.Drawing.Size(250, 23);
            this.txtDescripcionCategoria.TabIndex = 3;

            // 
            // btnGuardarCategoria
            // 
            this.btnGuardarCategoria.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnGuardarCategoria.FlatAppearance.BorderSize = 0;
            this.btnGuardarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCategoria.ForeColor = System.Drawing.Color.White;
            this.btnGuardarCategoria.Location = new System.Drawing.Point(560, 46);
            this.btnGuardarCategoria.Name = "btnGuardarCategoria";
            this.btnGuardarCategoria.Size = new System.Drawing.Size(80, 28);
            this.btnGuardarCategoria.TabIndex = 4;
            this.btnGuardarCategoria.Text = "Guardar";
            this.btnGuardarCategoria.UseVisualStyleBackColor = false;
            this.btnGuardarCategoria.Click += new System.EventHandler(this.btnGuardarCategoria_Click);

            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategorias.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategorias.Location = new System.Drawing.Point(30, 90);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategorias.Size = new System.Drawing.Size(610, 280);
            this.dgvCategorias.TabIndex = 5;

            // 
            // tabProveedores
            // 
            this.tabProveedores.Controls.Add(this.lblNombreProveedor);
            this.tabProveedores.Controls.Add(this.txtNombreProveedor);
            this.tabProveedores.Controls.Add(this.lblRncProveedor);
            this.tabProveedores.Controls.Add(this.txtRncProveedor);
            this.tabProveedores.Controls.Add(this.lblTelefonoProveedor);
            this.tabProveedores.Controls.Add(this.txtTelefonoProveedor);
            this.tabProveedores.Controls.Add(this.lblCorreoProveedor);
            this.tabProveedores.Controls.Add(this.txtCorreoProveedor);
            this.tabProveedores.Controls.Add(this.lblDireccionProveedor);
            this.tabProveedores.Controls.Add(this.txtDireccionProveedor);
            this.tabProveedores.Controls.Add(this.btnGuardarProveedor);
            this.tabProveedores.Controls.Add(this.btnEliminarProveedor);
            this.tabProveedores.Controls.Add(this.dgvProveedores);
            this.tabProveedores.Location = new System.Drawing.Point(4, 24);
            this.tabProveedores.Name = "tabProveedores";
            this.tabProveedores.Padding = new System.Windows.Forms.Padding(10);
            this.tabProveedores.Size = new System.Drawing.Size(652, 392);
            this.tabProveedores.TabIndex = 2;
            this.tabProveedores.Text = "Proveedores";
            this.tabProveedores.UseVisualStyleBackColor = true;

            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.AutoSize = true;
            this.lblNombreProveedor.Location = new System.Drawing.Point(20, 20);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(51, 15);
            this.lblNombreProveedor.TabIndex = 0;
            this.lblNombreProveedor.Text = "Nombre:";

            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.Location = new System.Drawing.Point(20, 38);
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.Size = new System.Drawing.Size(200, 23);
            this.txtNombreProveedor.TabIndex = 1;

            // 
            // lblRncProveedor
            // 
            this.lblRncProveedor.AutoSize = true;
            this.lblRncProveedor.Location = new System.Drawing.Point(240, 20);
            this.lblRncProveedor.Name = "lblRncProveedor";
            this.lblRncProveedor.Size = new System.Drawing.Size(36, 15);
            this.lblRncProveedor.TabIndex = 2;
            this.lblRncProveedor.Text = "RNC:";

            // 
            // txtRncProveedor
            // 
            this.txtRncProveedor.Location = new System.Drawing.Point(240, 38);
            this.txtRncProveedor.Name = "txtRncProveedor";
            this.txtRncProveedor.Size = new System.Drawing.Size(120, 23);
            this.txtRncProveedor.TabIndex = 3;

            // 
            // lblTelefonoProveedor
            // 
            this.lblTelefonoProveedor.AutoSize = true;
            this.lblTelefonoProveedor.Location = new System.Drawing.Point(380, 20);
            this.lblTelefonoProveedor.Name = "lblTelefonoProveedor";
            this.lblTelefonoProveedor.Size = new System.Drawing.Size(63, 15);
            this.lblTelefonoProveedor.TabIndex = 4;
            this.lblTelefonoProveedor.Text = "Teléfono:";

            // 
            // txtTelefonoProveedor
            // 
            this.txtTelefonoProveedor.Location = new System.Drawing.Point(380, 38);
            this.txtTelefonoProveedor.Name = "txtTelefonoProveedor";
            this.txtTelefonoProveedor.Size = new System.Drawing.Size(120, 23);
            this.txtTelefonoProveedor.TabIndex = 5;

            // 
            // lblCorreoProveedor
            // 
            this.lblCorreoProveedor.AutoSize = true;
            this.lblCorreoProveedor.Location = new System.Drawing.Point(20, 75);
            this.lblCorreoProveedor.Name = "lblCorreoProveedor";
            this.lblCorreoProveedor.Size = new System.Drawing.Size(49, 15);
            this.lblCorreoProveedor.TabIndex = 6;
            this.lblCorreoProveedor.Text = "Correo:";

            // 
            // txtCorreoProveedor
            // 
            this.txtCorreoProveedor.Location = new System.Drawing.Point(20, 93);
            this.txtCorreoProveedor.Name = "txtCorreoProveedor";
            this.txtCorreoProveedor.Size = new System.Drawing.Size(200, 23);
            this.txtCorreoProveedor.TabIndex = 7;

            // 
            // lblDireccionProveedor
            // 
            this.lblDireccionProveedor.AutoSize = true;
            this.lblDireccionProveedor.Location = new System.Drawing.Point(240, 75);
            this.lblDireccionProveedor.Name = "lblDireccionProveedor";
            this.lblDireccionProveedor.Size = new System.Drawing.Size(67, 15);
            this.lblDireccionProveedor.TabIndex = 8;
            this.lblDireccionProveedor.Text = "Dirección:";

            // 
            // txtDireccionProveedor
            // 
            this.txtDireccionProveedor.Location = new System.Drawing.Point(240, 93);
            this.txtDireccionProveedor.Name = "txtDireccionProveedor";
            this.txtDireccionProveedor.Size = new System.Drawing.Size(260, 23);
            this.txtDireccionProveedor.TabIndex = 9;

            // 
            // btnGuardarProveedor
            // 
            this.btnGuardarProveedor.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnGuardarProveedor.FlatAppearance.BorderSize = 0;
            this.btnGuardarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnGuardarProveedor.Location = new System.Drawing.Point(520, 38);
            this.btnGuardarProveedor.Name = "btnGuardarProveedor";
            this.btnGuardarProveedor.Size = new System.Drawing.Size(120, 28);
            this.btnGuardarProveedor.TabIndex = 10;
            this.btnGuardarProveedor.Text = "Guardar";
            this.btnGuardarProveedor.UseVisualStyleBackColor = false;
            this.btnGuardarProveedor.Click += new System.EventHandler(this.btnGuardarProveedor_Click);

            // 
            // btnEliminarProveedor
            // 
            this.btnEliminarProveedor.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnEliminarProveedor.FlatAppearance.BorderSize = 0;
            this.btnEliminarProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarProveedor.ForeColor = System.Drawing.Color.White;
            this.btnEliminarProveedor.Location = new System.Drawing.Point(520, 93);
            this.btnEliminarProveedor.Name = "btnEliminarProveedor";
            this.btnEliminarProveedor.Size = new System.Drawing.Size(120, 28);
            this.btnEliminarProveedor.TabIndex = 11;
            this.btnEliminarProveedor.Text = "Eliminar";
            this.btnEliminarProveedor.UseVisualStyleBackColor = false;
            this.btnEliminarProveedor.Click += new System.EventHandler(this.btnEliminarProveedor_Click);

            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AllowUserToDeleteRows = false;
            this.dgvProveedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProveedores.BackgroundColor = System.Drawing.Color.White;
            this.dgvProveedores.Location = new System.Drawing.Point(20, 140);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.ReadOnly = true;
            this.dgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProveedores.Size = new System.Drawing.Size(620, 235);
            this.dgvProveedores.TabIndex = 12;
            this.dgvProveedores.SelectionChanged += new System.EventHandler(this.dgvProveedores_SelectionChanged);

            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(580, 510);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 35);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // 
            // ConfiguracionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 560);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración - PharmaSoft";
            this.Load += new System.EventHandler(this.ConfiguracionForm_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDatosNegocio.ResumeLayout(false);
            this.tabDatosNegocio.PerformLayout();
            this.tabCategorias.ResumeLayout(false);
            this.tabCategorias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvCategorias).EndInit();
            this.tabProveedores.ResumeLayout(false);
            this.tabProveedores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvProveedores).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}