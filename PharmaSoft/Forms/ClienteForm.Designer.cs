namespace PharmaSoft.Forms
{
    partial class ClienteForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelScroll;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblRncCedula;
        private System.Windows.Forms.TextBox txtRncCedula;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblLimiteCredito;
        private System.Windows.Forms.NumericUpDown nudLimiteCredito;
        
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCancelar;

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
            this.panelScroll = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblRncCedula = new System.Windows.Forms.Label();
            this.txtRncCedula = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblLimiteCredito = new System.Windows.Forms.Label();
            this.nudLimiteCredito = new System.Windows.Forms.NumericUpDown();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)this.nudLimiteCredito).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvClientes).BeginInit();
            this.SuspendLayout();

            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.AutoScroll = true;
            this.panelScroll.BackColor = System.Drawing.Color.White;
            this.panelScroll.Controls.Add(this.lblTitulo);
            this.panelScroll.Controls.Add(this.lblNombre);
            this.panelScroll.Controls.Add(this.txtNombre);
            this.panelScroll.Controls.Add(this.lblRncCedula);
            this.panelScroll.Controls.Add(this.txtRncCedula);
            this.panelScroll.Controls.Add(this.lblTelefono);
            this.panelScroll.Controls.Add(this.txtTelefono);
            this.panelScroll.Controls.Add(this.lblDireccion);
            this.panelScroll.Controls.Add(this.txtDireccion);
            this.panelScroll.Controls.Add(this.lblLimiteCredito);
            this.panelScroll.Controls.Add(this.nudLimiteCredito);
            this.panelScroll.Controls.Add(this.btnNuevo);
            this.panelScroll.Controls.Add(this.btnGuardar);
            this.panelScroll.Controls.Add(this.btnEliminar);
            this.panelScroll.Controls.Add(this.btnCancelar);
            this.panelScroll.Controls.Add(this.lblBuscar);
            this.panelScroll.Controls.Add(this.txtBuscar);
            this.panelScroll.Controls.Add(this.dgvClientes);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(200, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Clientes";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNombre.Location = new System.Drawing.Point(20, 80);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(58, 19);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(20, 102);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(350, 23);
            this.txtNombre.TabIndex = 2;
            // 
            // lblRncCedula
            // 
            this.lblRncCedula.AutoSize = true;
            this.lblRncCedula.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRncCedula.Location = new System.Drawing.Point(400, 80);
            this.lblRncCedula.Name = "lblRncCedula";
            this.lblRncCedula.Size = new System.Drawing.Size(85, 19);
            this.lblRncCedula.TabIndex = 3;
            this.lblRncCedula.Text = "RNC/Cédula:";
            // 
            // txtRncCedula
            // 
            this.txtRncCedula.Location = new System.Drawing.Point(400, 102);
            this.txtRncCedula.MaxLength = 20;
            this.txtRncCedula.Name = "txtRncCedula";
            this.txtRncCedula.Size = new System.Drawing.Size(200, 23);
            this.txtRncCedula.TabIndex = 4;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelefono.Location = new System.Drawing.Point(20, 145);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(62, 19);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(20, 167);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(200, 23);
            this.txtTelefono.TabIndex = 6;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDireccion.Location = new System.Drawing.Point(250, 145);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(69, 19);
            this.lblDireccion.TabIndex = 7;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(250, 167);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(350, 60);
            this.txtDireccion.TabIndex = 8;
            // 
            // lblLimiteCredito
            // 
            this.lblLimiteCredito.AutoSize = true;
            this.lblLimiteCredito.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLimiteCredito.Location = new System.Drawing.Point(630, 80);
            this.lblLimiteCredito.Name = "lblLimiteCredito";
            this.lblLimiteCredito.Size = new System.Drawing.Size(100, 19);
            this.lblLimiteCredito.TabIndex = 9;
            this.lblLimiteCredito.Text = "Límite Crédito:";
            // 
            // nudLimiteCredito
            // 
            this.nudLimiteCredito.DecimalPlaces = 2;
            this.nudLimiteCredito.Location = new System.Drawing.Point(630, 102);
            this.nudLimiteCredito.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudLimiteCredito.Name = "nudLimiteCredito";
            this.nudLimiteCredito.Size = new System.Drawing.Size(150, 23);
            this.nudLimiteCredito.TabIndex = 10;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(23, 184, 186);
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(20, 250);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 35);
            this.btnNuevo.TabIndex = 11;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(130, 250);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 35);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(240, 250);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 35);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(350, 250);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBuscar.Location = new System.Drawing.Point(20, 310);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(58, 19);
            this.lblBuscar.TabIndex = 15;
            this.lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(20, 332);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(430, 23);
            this.txtBuscar.TabIndex = 16;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(20, 370);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(760, 250);
            this.dgvClientes.TabIndex = 17;
            this.dgvClientes.SelectionChanged += new System.EventHandler(this.dgvClientes_SelectionChanged);
            // 
            // ClienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.panelScroll);
            this.Name = "ClienteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.ClienteForm_Load);
            ((System.ComponentModel.ISupportInitialize)this.nudLimiteCredito).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvClientes).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}