namespace PharmaSoft.Forms
{
    partial class VentaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private System.Windows.Forms.Panel pnlTotales;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblCarrito;

        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Label lblPagoCon;
        private System.Windows.Forms.NumericUpDown nudPagoCon;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label lblMontoCambio;
        private System.Windows.Forms.Button btnFinalizarVenta;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            flowToolbar = new FlowLayoutPanel();
            lblTitulo = new Label();
            lblBuscar = new Label();
            pnlTotales = new Panel();
            lblSubtotal = new Label();
            lblSubtotalValue = new Label();
            lblTotal = new Label();
            lblMontoTotal = new Label();
            pnlAcciones = new Panel();
            btnCobrar = new Button();
            btnAddCuenta = new Button();
            btnPreCompra = new Button();
            panel1 = new Panel();
            cmbComprobante = new ComboBox();
            lbCliente = new Label();
            cmbTFactura = new ComboBox();
            lbTFactura = new Label();
            cmbCliente = new ComboBox();
            lbTipoComprobantes = new Label();
            lblCarrito = new Label();
            btnQuitar = new Button();
            lblMetodoPago = new Label();
            cmbMetodoPago = new ComboBox();
            lblPagoCon = new Label();
            nudPagoCon = new NumericUpDown();
            lblCambio = new Label();
            lblMontoCambio = new Label();
            btnFinalizarVenta = new Button();
            btnCancelar = new Button();
            dgvCarrito = new DataGridView();
            lbDinero = new Label();
            numDowDinero = new NumericUpDown();
            txtBuscar = new ComboBox();
            flowToolbar.SuspendLayout();
            pnlTotales.SuspendLayout();
            pnlAcciones.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPagoCon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDowDinero).BeginInit();
            SuspendLayout();
            // 
            // flowToolbar
            // 
            flowToolbar.BackColor = SystemColors.Control;
            flowToolbar.Controls.Add(lblTitulo);
            flowToolbar.Controls.Add(lblBuscar);
            flowToolbar.Controls.Add(txtBuscar);
            flowToolbar.Dock = DockStyle.Top;
            flowToolbar.FlowDirection = FlowDirection.TopDown;
            flowToolbar.Location = new Point(0, 0);
            flowToolbar.Name = "flowToolbar";
            flowToolbar.Size = new Size(650, 94);
            flowToolbar.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(3, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(300, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Nueva Venta";
            // 
            // lblBuscar
            // 
            lblBuscar.Location = new Point(3, 30);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(80, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // pnlTotales
            // 
            pnlTotales.BackColor = Color.FromArgb(220, 53, 69);
            pnlTotales.Controls.Add(lblSubtotal);
            pnlTotales.Controls.Add(lblSubtotalValue);
            pnlTotales.Controls.Add(lblTotal);
            pnlTotales.Controls.Add(lblMontoTotal);
            pnlTotales.Dock = DockStyle.Top;
            pnlTotales.Location = new Point(0, 94);
            pnlTotales.Name = "pnlTotales";
            pnlTotales.Size = new Size(650, 40);
            pnlTotales.TabIndex = 1;
            // 
            // lblSubtotal
            // 
            lblSubtotal.ForeColor = Color.White;
            lblSubtotal.Location = new Point(30, 10);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(80, 20);
            lblSubtotal.TabIndex = 0;
            lblSubtotal.Text = "Subtotal:";
            // 
            // lblSubtotalValue
            // 
            lblSubtotalValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubtotalValue.ForeColor = Color.White;
            lblSubtotalValue.Location = new Point(110, 10);
            lblSubtotalValue.Name = "lblSubtotalValue";
            lblSubtotalValue.Size = new Size(100, 20);
            lblSubtotalValue.TabIndex = 1;
            lblSubtotalValue.Text = "RD$ 0.00";
            // 
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotal.ForeColor = Color.White;
            lblTotal.Location = new Point(250, 10);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(60, 20);
            lblTotal.TabIndex = 2;
            lblTotal.Text = "Total:";
            // 
            // lblMontoTotal
            // 
            lblMontoTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblMontoTotal.ForeColor = Color.White;
            lblMontoTotal.Location = new Point(310, 10);
            lblMontoTotal.Name = "lblMontoTotal";
            lblMontoTotal.Size = new Size(150, 20);
            lblMontoTotal.TabIndex = 3;
            lblMontoTotal.Text = "RD$ 0.00";
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(224, 224, 224);
            pnlAcciones.Controls.Add(btnCobrar);
            pnlAcciones.Controls.Add(btnAddCuenta);
            pnlAcciones.Controls.Add(btnPreCompra);
            pnlAcciones.Controls.Add(panel1);
            pnlAcciones.Dock = DockStyle.Right;
            pnlAcciones.Location = new Point(650, 0);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(250, 620);
            pnlAcciones.TabIndex = 3;
            pnlAcciones.Paint += pnlAcciones_Paint;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.LimeGreen;
            btnCobrar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCobrar.Location = new Point(16, 499);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(222, 60);
            btnCobrar.TabIndex = 9;
            btnCobrar.Text = "Cobrar";
            btnCobrar.UseVisualStyleBackColor = false;
            btnCobrar.Click += btnCobrar_Click;
            // 
            // btnAddCuenta
            // 
            btnAddCuenta.BackColor = SystemColors.HotTrack;
            btnAddCuenta.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddCuenta.Location = new Point(16, 420);
            btnAddCuenta.Name = "btnAddCuenta";
            btnAddCuenta.Size = new Size(222, 60);
            btnAddCuenta.TabIndex = 8;
            btnAddCuenta.Text = "Add Cuenta";
            btnAddCuenta.UseVisualStyleBackColor = false;
            btnAddCuenta.Click += btnAddCuenta_Click;
            // 
            // btnPreCompra
            // 
            btnPreCompra.BackColor = Color.Gold;
            btnPreCompra.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPreCompra.ForeColor = Color.Black;
            btnPreCompra.Location = new Point(16, 341);
            btnPreCompra.Name = "btnPreCompra";
            btnPreCompra.Size = new Size(222, 60);
            btnPreCompra.TabIndex = 7;
            btnPreCompra.Text = "Pre-Factura";
            btnPreCompra.UseVisualStyleBackColor = false;
            btnPreCompra.Click += btnPreCompra_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(cmbComprobante);
            panel1.Controls.Add(lbCliente);
            panel1.Controls.Add(cmbTFactura);
            panel1.Controls.Add(lbTFactura);
            panel1.Controls.Add(cmbCliente);
            panel1.Controls.Add(lbTipoComprobantes);
            panel1.Location = new Point(7, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 221);
            panel1.TabIndex = 6;
            // 
            // cmbComprobante
            // 
            cmbComprobante.FormattingEnabled = true;
            cmbComprobante.Location = new Point(9, 175);
            cmbComprobante.Name = "cmbComprobante";
            cmbComprobante.Size = new Size(222, 23);
            cmbComprobante.TabIndex = 5;
            // 
            // lbCliente
            // 
            lbCliente.AutoSize = true;
            lbCliente.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCliente.Location = new Point(88, 21);
            lbCliente.Name = "lbCliente";
            lbCliente.Size = new Size(64, 21);
            lbCliente.TabIndex = 0;
            lbCliente.Text = "Cliente";
            // 
            // cmbTFactura
            // 
            cmbTFactura.FormattingEnabled = true;
            cmbTFactura.Location = new Point(9, 108);
            cmbTFactura.Name = "cmbTFactura";
            cmbTFactura.Size = new Size(222, 23);
            cmbTFactura.TabIndex = 4;
            // 
            // lbTFactura
            // 
            lbTFactura.AutoSize = true;
            lbTFactura.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTFactura.Location = new Point(55, 80);
            lbTFactura.Name = "lbTFactura";
            lbTFactura.Size = new Size(127, 21);
            lbTFactura.TabIndex = 1;
            lbTFactura.Text = "Tipo de Factura";
            // 
            // cmbCliente
            // 
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(9, 45);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(222, 23);
            cmbCliente.TabIndex = 3;
            // 
            // lbTipoComprobantes
            // 
            lbTipoComprobantes.AutoSize = true;
            lbTipoComprobantes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTipoComprobantes.Location = new Point(38, 151);
            lbTipoComprobantes.Name = "lbTipoComprobantes";
            lbTipoComprobantes.Size = new Size(160, 21);
            lbTipoComprobantes.TabIndex = 2;
            lbTipoComprobantes.Text = "Tipo Comprobantes";
            // 
            // lblCarrito
            // 
            lblCarrito.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCarrito.Location = new Point(0, 147);
            lblCarrito.Name = "lblCarrito";
            lblCarrito.Size = new Size(150, 20);
            lblCarrito.TabIndex = 4;
            lblCarrito.Text = "Productos en Carrito";
            // 
            // btnQuitar
            // 
            btnQuitar.BackColor = Color.FromArgb(220, 53, 69);
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.ForeColor = Color.White;
            btnQuitar.Location = new Point(3, 459);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(100, 30);
            btnQuitar.TabIndex = 7;
            btnQuitar.Text = "← Quitar";
            btnQuitar.UseVisualStyleBackColor = false;
            btnQuitar.Click += btnQuitar_Click;
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.Location = new Point(3, 351);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(100, 20);
            lblMetodoPago.TabIndex = 8;
            lblMetodoPago.Text = "Método Pago:*";
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodoPago.Location = new Point(113, 351);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(150, 23);
            cmbMetodoPago.TabIndex = 9;
            // 
            // lblPagoCon
            // 
            lblPagoCon.Location = new Point(3, 381);
            lblPagoCon.Name = "lblPagoCon";
            lblPagoCon.Size = new Size(100, 20);
            lblPagoCon.TabIndex = 10;
            lblPagoCon.Text = "Pago con:*";
            // 
            // nudPagoCon
            // 
            nudPagoCon.DecimalPlaces = 2;
            nudPagoCon.Location = new Point(113, 381);
            nudPagoCon.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudPagoCon.Name = "nudPagoCon";
            nudPagoCon.Size = new Size(120, 23);
            nudPagoCon.TabIndex = 11;
            // 
            // lblCambio
            // 
            lblCambio.Location = new Point(3, 411);
            lblCambio.Name = "lblCambio";
            lblCambio.Size = new Size(100, 20);
            lblCambio.TabIndex = 12;
            lblCambio.Text = "Cambio:";
            // 
            // lblMontoCambio
            // 
            lblMontoCambio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMontoCambio.ForeColor = Color.FromArgb(40, 167, 69);
            lblMontoCambio.Location = new Point(113, 411);
            lblMontoCambio.Name = "lblMontoCambio";
            lblMontoCambio.Size = new Size(120, 20);
            lblMontoCambio.TabIndex = 13;
            lblMontoCambio.Text = "RD$ 0.00";
            // 
            // btnFinalizarVenta
            // 
            btnFinalizarVenta.Location = new Point(0, 0);
            btnFinalizarVenta.Name = "btnFinalizarVenta";
            btnFinalizarVenta.Size = new Size(75, 23);
            btnFinalizarVenta.TabIndex = 0;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(108, 117, 125);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(113, 459);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 15;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dgvCarrito
            // 
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.AllowUserToDeleteRows = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.Location = new Point(3, 170);
            dgvCarrito.MultiSelect = false;
            dgvCarrito.Name = "dgvCarrito";
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.Size = new Size(640, 280);
            dgvCarrito.TabIndex = 5;
            dgvCarrito.SelectionChanged += dgvCarrito_SelectionChanged;
            // 
            // lbDinero
            // 
            lbDinero.AutoSize = true;
            lbDinero.Location = new Point(250, 467);
            lbDinero.Name = "lbDinero";
            lbDinero.Size = new Size(87, 15);
            lbDinero.TabIndex = 16;
            lbDinero.Text = "Total Recibido: ";
            // 
            // numDowDinero
            // 
            numDowDinero.Location = new Point(340, 466);
            numDowDinero.Name = "numDowDinero";
            numDowDinero.Size = new Size(120, 23);
            numDowDinero.TabIndex = 17;
            // 
            // txtBuscar
            // 
            txtBuscar.FormattingEnabled = true;
            txtBuscar.Location = new Point(3, 53);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(640, 23);
            txtBuscar.TabIndex = 2;
            // 
            // VentaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 620);
            Controls.Add(numDowDinero);
            Controls.Add(lbDinero);
            Controls.Add(pnlTotales);
            Controls.Add(flowToolbar);
            Controls.Add(pnlAcciones);
            Controls.Add(lblCarrito);
            Controls.Add(dgvCarrito);
            Controls.Add(btnQuitar);
            Controls.Add(lblMetodoPago);
            Controls.Add(cmbMetodoPago);
            Controls.Add(lblPagoCon);
            Controls.Add(nudPagoCon);
            Controls.Add(lblCambio);
            Controls.Add(lblMontoCambio);
            Controls.Add(btnCancelar);
            Name = "VentaForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nueva Venta";
            Load += VentaForm_Load;
            flowToolbar.ResumeLayout(false);
            pnlTotales.ResumeLayout(false);
            pnlAcciones.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPagoCon).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDowDinero).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lbTFactura;
        private Label lbCliente;
        private Label lbTipoComprobantes;
        private ComboBox cmbCliente;
        private Panel panel1;
        private ComboBox cmbComprobante;
        private ComboBox cmbTFactura;
        private Button btnAddCuenta;
        private Button btnPreCompra;
        private Button btnCobrar;

        private DataGridView dgvCarrito;
        private Label lbDinero;
        private NumericUpDown numDowDinero;
        private ComboBox txtBuscar;
    }
}


