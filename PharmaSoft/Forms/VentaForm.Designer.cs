namespace PharmaSoft.Forms
{
    partial class VentaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowToolbar;
        private System.Windows.Forms.Panel pnlTotales;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnNuevaVenta;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblCarrito;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Label lblPagoCon;
        private System.Windows.Forms.NumericUpDown nudPagoCon;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label lblMontoCambio;
        private System.Windows.Forms.Button btnFinalizarVenta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPreFactura;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnAddCuentas;

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
            this.flowToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTotales = new System.Windows.Forms.Panel();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblCarrito = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.lblPagoCon = new System.Windows.Forms.Label();
            this.nudPagoCon = new System.Windows.Forms.NumericUpDown();
            this.lblCambio = new System.Windows.Forms.Label();
            this.lblMontoCambio = new System.Windows.Forms.Label();
            this.btnFinalizarVenta = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPreFactura = new System.Windows.Forms.Button();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnAddCuentas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPagoCon)).BeginInit();
            this.SuspendLayout();

            this.flowToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.flowToolbar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowToolbar.Location = new System.Drawing.Point(0, 0);
            this.flowToolbar.Name = "flowToolbar";
            this.flowToolbar.Size = new System.Drawing.Size(900, 120);
            this.flowToolbar.TabIndex = 0;

            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(300, 30);
            this.lblTitulo.Text = "Nueva Venta";
            this.flowToolbar.Controls.Add(this.lblTitulo);

            this.lblBuscar.Location = new System.Drawing.Point(20, 60);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(80, 20);
            this.lblBuscar.Text = "Buscar:*";
            this.flowToolbar.Controls.Add(this.lblBuscar);

            this.txtBuscar.Location = new System.Drawing.Point(100, 58);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(250, 23);
            this.flowToolbar.Controls.Add(this.txtBuscar);

            this.btnNuevaVenta.Location = new System.Drawing.Point(370, 58);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(120, 28);
            this.btnNuevaVenta.Text = "Nueva Venta";
            this.btnNuevaVenta.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnNuevaVenta.ForeColor = System.Drawing.Color.White;
            this.btnNuevaVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flowToolbar.Controls.Add(this.btnNuevaVenta);

            this.pnlTotales.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.pnlTotales.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotales.Location = new System.Drawing.Point(0, 120);
            this.pnlTotales.Name = "pnlTotales";
            this.pnlTotales.Size = new System.Drawing.Size(900, 40);
            this.pnlTotales.TabIndex = 1;

            this.lblSubtotal.Location = new System.Drawing.Point(30, 10);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(80, 20);
            this.lblSubtotal.Text = "Subtotal:";
            this.lblSubtotal.ForeColor = System.Drawing.Color.White;
            this.pnlTotales.Controls.Add(this.lblSubtotal);

            this.lblSubtotalValue.Location = new System.Drawing.Point(110, 10);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(100, 20);
            this.lblSubtotalValue.Text = "RD$ 0.00";
            this.lblSubtotalValue.ForeColor = System.Drawing.Color.White;
            this.lblSubtotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlTotales.Controls.Add(this.lblSubtotalValue);

            this.lblTotal.Location = new System.Drawing.Point(250, 10);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(60, 20);
            this.lblTotal.Text = "Total:";
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlTotales.Controls.Add(this.lblTotal);

            this.lblMontoTotal.Location = new System.Drawing.Point(310, 10);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(150, 20);
            this.lblMontoTotal.Text = "RD$ 0.00";
            this.lblMontoTotal.ForeColor = System.Drawing.Color.White;
            this.lblMontoTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.pnlTotales.Controls.Add(this.lblMontoTotal);

            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAcciones.Location = new System.Drawing.Point(650, 160);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(250, 460);
            this.pnlAcciones.TabIndex = 3;

            this.btnPreFactura.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnPreFactura.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPreFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreFactura.ForeColor = System.Drawing.Color.White;
            this.btnPreFactura.Location = new System.Drawing.Point(0, 0);
            this.btnPreFactura.Name = "btnPreFactura";
            this.btnPreFactura.Size = new System.Drawing.Size(250, 50);
            this.btnPreFactura.TabIndex = 0;
            this.btnPreFactura.Text = "Pre-Factura";
            this.pnlAcciones.Controls.Add(this.btnPreFactura);

            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCobrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.ForeColor = System.Drawing.Color.White;
            this.btnCobrar.Location = new System.Drawing.Point(0, 50);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(250, 50);
            this.btnCobrar.TabIndex = 1;
            this.btnCobrar.Text = "Cobrar";
            this.pnlAcciones.Controls.Add(this.btnCobrar);

            this.btnAddCuentas.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnAddCuentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddCuentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCuentas.ForeColor = System.Drawing.Color.White;
            this.btnAddCuentas.Location = new System.Drawing.Point(0, 100);
            this.btnAddCuentas.Name = "btnAddCuentas";
            this.btnAddCuentas.Size = new System.Drawing.Size(250, 50);
            this.btnAddCuentas.TabIndex = 2;
            this.btnAddCuentas.Text = "Add Cuentas";
            this.pnlAcciones.Controls.Add(this.btnAddCuentas);

            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(0, 160);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Size = new System.Drawing.Size(650, 460);
            this.dgvProductos.TabIndex = 2;

            this.lblCarrito.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCarrito.Location = new System.Drawing.Point(20, 170);
            this.lblCarrito.Name = "lblCarrito";
            this.lblCarrito.Size = new System.Drawing.Size(150, 20);
            this.lblCarrito.Text = "Carrito de Compras";

            this.dgvCarrito.AllowUserToAddRows = false;
            this.dgvCarrito.AllowUserToDeleteRows = false;
            this.dgvCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrito.Location = new System.Drawing.Point(20, 200);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.ReadOnly = true;
            this.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrito.MultiSelect = false;
            this.dgvCarrito.Size = new System.Drawing.Size(350, 150);

            this.btnAgregar.Location = new System.Drawing.Point(150, 470);
            this.btnAgregar.Size = new System.Drawing.Size(100, 30);
            this.btnAgregar.Text = "Agregar →";
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnQuitar.Location = new System.Drawing.Point(260, 470);
            this.btnQuitar.Size = new System.Drawing.Size(100, 30);
            this.btnQuitar.Text = "← Quitar";
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnQuitar.ForeColor = System.Drawing.Color.White;
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.lblMetodoPago.Location = new System.Drawing.Point(20, 530);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(100, 20);
            this.lblMetodoPago.Text = "Método Pago:*";

            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.Location = new System.Drawing.Point(130, 530);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(150, 23);

            this.lblPagoCon.Location = new System.Drawing.Point(20, 560);
            this.lblPagoCon.Name = "lblPagoCon";
            this.lblPagoCon.Size = new System.Drawing.Size(100, 20);
            this.lblPagoCon.Text = "Pago con:*";

            this.nudPagoCon.DecimalPlaces = 2;
            this.nudPagoCon.Location = new System.Drawing.Point(130, 560);
            this.nudPagoCon.Maximum = 999999;
            this.nudPagoCon.Minimum = 0;
            this.nudPagoCon.Name = "nudPagoCon";
            this.nudPagoCon.Size = new System.Drawing.Size(120, 23);

            this.lblCambio.Location = new System.Drawing.Point(20, 590);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(100, 20);
            this.lblCambio.Text = "Cambio:";

            this.lblMontoCambio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMontoCambio.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.lblMontoCambio.Location = new System.Drawing.Point(130, 590);
            this.lblMontoCambio.Name = "lblMontoCambio";
            this.lblMontoCambio.Size = new System.Drawing.Size(120, 20);
            this.lblMontoCambio.Text = "RD$ 0.00";

            this.btnFinalizarVenta.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnFinalizarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarVenta.ForeColor = System.Drawing.Color.White;
            this.btnFinalizarVenta.Location = new System.Drawing.Point(380, 530);
            this.btnFinalizarVenta.Name = "btnFinalizarVenta";
            this.btnFinalizarVenta.Size = new System.Drawing.Size(160, 35);
            this.btnFinalizarVenta.Text = "Finalizar Venta";

            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(380, 570);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.Text = "Cancelar";

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva Venta";
            this.Load += VentaForm_Load;
            this.txtBuscar.TextChanged += txtBuscar_TextChanged;

            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.pnlTotales);
            this.Controls.Add(this.flowToolbar);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.lblCarrito);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblMetodoPago);
            this.Controls.Add(this.cmbMetodoPago);
            this.Controls.Add(this.lblPagoCon);
            this.Controls.Add(this.nudPagoCon);
            this.Controls.Add(this.lblCambio);
            this.Controls.Add(this.lblMontoCambio);
            this.Controls.Add(this.btnFinalizarVenta);
            this.Controls.Add(this.btnCancelar);

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPagoCon)).EndInit();
            this.ResumeLayout(false);
        }
    }
}