namespace PharmaSoft
{
    partial class InventarioForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelContenido = new Panel();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dgvInventario = new DataGridView();
            CodigoBarras = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Laboratorio = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            PrecioVenta = new DataGridViewTextBoxColumn();
            Caducidad = new DataGridViewTextBoxColumn();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAnadir = new Button();
            txtBuscar = new TextBox();
            lblTituloSeccion = new Label();
            panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).BeginInit();
            SuspendLayout();
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.White;
            panelContenido.Controls.Add(button1);
            panelContenido.Controls.Add(button2);
            panelContenido.Controls.Add(button3);
            panelContenido.Controls.Add(dgvInventario);
            panelContenido.Controls.Add(btnEliminar);
            panelContenido.Controls.Add(btnEditar);
            panelContenido.Controls.Add(btnAnadir);
            panelContenido.Controls.Add(txtBuscar);
            panelContenido.Controls.Add(lblTituloSeccion);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(20);
            panelContenido.Size = new Size(1050, 630);
            panelContenido.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(220, 53, 69);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(937, 67);
            button1.Name = "button1";
            button1.Size = new Size(90, 30);
            button1.TabIndex = 6;
            button1.Text = "Eliminar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackColor = Color.FromArgb(40, 167, 69);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(841, 67);
            button2.Name = "button2";
            button2.Size = new Size(90, 30);
            button2.TabIndex = 7;
            button2.Text = "Editar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.BackColor = Color.FromArgb(25, 118, 210);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Location = new Point(745, 67);
            button3.Name = "button3";
            button3.Size = new Size(90, 30);
            button3.TabIndex = 8;
            button3.Text = "Añadir";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // dgvInventario
            // 
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventario.BackgroundColor = Color.White;
            dgvInventario.BorderStyle = BorderStyle.Fixed3D;
            dgvInventario.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInventario.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvInventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvInventario.ColumnHeadersHeight = 35;
            dgvInventario.Columns.AddRange(new DataGridViewColumn[] { CodigoBarras, Nombre, Laboratorio, Cantidad, PrecioVenta, Caducidad });
            dgvInventario.EnableHeadersVisualStyles = false;
            dgvInventario.Location = new Point(0, 121);
            dgvInventario.Name = "dgvInventario";
            dgvInventario.ReadOnly = true;
            dgvInventario.RowHeadersVisible = false;
            dgvInventario.RowTemplate.Height = 30;
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.Size = new Size(1050, 509);
            dgvInventario.TabIndex = 0;
            // 
            // CodigoBarras
            // 
            CodigoBarras.HeaderText = "Código";
            CodigoBarras.Name = "CodigoBarras";
            CodigoBarras.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // Laboratorio
            // 
            Laboratorio.HeaderText = "Laboratorio";
            Laboratorio.Name = "Laboratorio";
            Laboratorio.ReadOnly = true;
            // 
            // Cantidad
            // 
            Cantidad.HeaderText = "Cantidad";
            Cantidad.Name = "Cantidad";
            Cantidad.ReadOnly = true;
            // 
            // PrecioVenta
            // 
            PrecioVenta.HeaderText = "Precio Venta";
            PrecioVenta.Name = "PrecioVenta";
            PrecioVenta.ReadOnly = true;
            // 
            // Caducidad
            // 
            Caducidad.HeaderText = "Caducidad";
            Caducidad.Name = "Caducidad";
            Caducidad.ReadOnly = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.BackColor = Color.FromArgb(220, 53, 69);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(1547, 85);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(90, 30);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.BackColor = Color.FromArgb(40, 167, 69);
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(1451, 85);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(90, 30);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnAnadir
            // 
            btnAnadir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnadir.BackColor = Color.FromArgb(25, 118, 210);
            btnAnadir.FlatAppearance.BorderSize = 0;
            btnAnadir.FlatStyle = FlatStyle.Flat;
            btnAnadir.ForeColor = Color.White;
            btnAnadir.Location = new Point(1355, 85);
            btnAnadir.Name = "btnAnadir";
            btnAnadir.Size = new Size(90, 30);
            btnAnadir.TabIndex = 3;
            btnAnadir.Text = "Añadir";
            btnAnadir.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.Location = new Point(23, 67);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = " Buscar...";
            txtBuscar.Size = new Size(300, 27);
            txtBuscar.TabIndex = 4;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblTituloSeccion
            // 
            lblTituloSeccion.AutoSize = true;
            lblTituloSeccion.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTituloSeccion.Location = new Point(38, 40);
            lblTituloSeccion.Name = "lblTituloSeccion";
            lblTituloSeccion.Size = new Size(228, 25);
            lblTituloSeccion.TabIndex = 5;
            lblTituloSeccion.Text = "Inventario de Productos";
            // 
            // InventarioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 630);
            Controls.Add(panelContenido);
            Name = "InventarioForm";
            Text = "MainForm";
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContenido;
        private DataGridView dgvInventario;
        private DataGridViewTextBoxColumn CodigoBarras;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Laboratorio;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn PrecioVenta;
        private DataGridViewTextBoxColumn Caducidad;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnAnadir;
        private TextBox txtBuscar;
        private Label lblTituloSeccion;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}