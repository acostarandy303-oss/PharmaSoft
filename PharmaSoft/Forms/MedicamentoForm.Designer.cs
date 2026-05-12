namespace PharmaSoft.Forms
{
    partial class MedicamentoForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCodigoBarras;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrincipioActivo;
        private System.Windows.Forms.TextBox txtPrincipioActivo;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.TextBox txtPresentacion;
        private System.Windows.Forms.Label lblLaboratorio;
        private System.Windows.Forms.TextBox txtLaboratorio;
        private System.Windows.Forms.Label lblDosis;
        private System.Windows.Forms.TextBox txtDosis;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label lblPrecioCompra;
        private System.Windows.Forms.NumericUpDown nudPrecioCompra;
        private System.Windows.Forms.Label lblPrecioVenta;
        private System.Windows.Forms.NumericUpDown nudPrecioVenta;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.NumericUpDown nudStockMinimo;
        private System.Windows.Forms.CheckBox chkRequiereReceta;

        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.Label lblNumeroLote;
        private System.Windows.Forms.TextBox txtNumeroLote;
        private System.Windows.Forms.Label lblCantidadLote;
        private System.Windows.Forms.NumericUpDown nudCantidadLote;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;

        private System.Windows.Forms.Button btnGuardar;
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrincipioActivo = new System.Windows.Forms.Label();
            this.txtPrincipioActivo = new System.Windows.Forms.TextBox();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.txtPresentacion = new System.Windows.Forms.TextBox();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.txtLaboratorio = new System.Windows.Forms.TextBox();
            this.lblDosis = new System.Windows.Forms.Label();
            this.txtDosis = new System.Windows.Forms.TextBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.lblPrecioCompra = new System.Windows.Forms.Label();
            this.nudPrecioCompra = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.nudPrecioVenta = new System.Windows.Forms.NumericUpDown();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.nudStockMinimo = new System.Windows.Forms.NumericUpDown();
            this.chkRequiereReceta = new System.Windows.Forms.CheckBox();
            this.lblLote = new System.Windows.Forms.Label();
            this.lblNumeroLote = new System.Windows.Forms.Label();
            this.txtNumeroLote = new System.Windows.Forms.TextBox();
            this.lblCantidadLote = new System.Windows.Forms.Label();
            this.nudCantidadLote = new System.Windows.Forms.NumericUpDown();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCompra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadLote)).BeginInit();
            this.SuspendLayout();

            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(360, 25);
            this.lblTitulo.Text = "Medicamento";
            this.Controls.Add(this.lblTitulo);

            int y = 50;
            this.lblCodigoBarras.Location = new System.Drawing.Point(20, y);
            this.lblCodigoBarras.Size = new System.Drawing.Size(100, 20);
            this.lblCodigoBarras.Text = "Código de Barras:";
            this.Controls.Add(this.lblCodigoBarras);

            this.txtCodigoBarras.Location = new System.Drawing.Point(140, y);
            this.txtCodigoBarras.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtCodigoBarras);

            y += 30;
            this.lblNombre.Location = new System.Drawing.Point(20, y);
            this.lblNombre.Size = new System.Drawing.Size(100, 20);
            this.lblNombre.Text = "Nombre:*";
            this.Controls.Add(this.lblNombre);

            this.txtNombre.Location = new System.Drawing.Point(140, y);
            this.txtNombre.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtNombre);

            y += 30;
            this.lblPrincipioActivo.Location = new System.Drawing.Point(20, y);
            this.lblPrincipioActivo.Size = new System.Drawing.Size(100, 20);
            this.lblPrincipioActivo.Text = "Principio Activo:";
            this.Controls.Add(this.lblPrincipioActivo);

            this.txtPrincipioActivo.Location = new System.Drawing.Point(140, y);
            this.txtPrincipioActivo.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtPrincipioActivo);

            y += 30;
            this.lblPresentacion.Location = new System.Drawing.Point(20, y);
            this.lblPresentacion.Size = new System.Drawing.Size(100, 20);
            this.lblPresentacion.Text = "Presentación:";
            this.Controls.Add(this.lblPresentacion);

            this.txtPresentacion.Location = new System.Drawing.Point(140, y);
            this.txtPresentacion.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtPresentacion);

            y += 30;
            this.lblLaboratorio.Location = new System.Drawing.Point(20, y);
            this.lblLaboratorio.Size = new System.Drawing.Size(100, 20);
            this.lblLaboratorio.Text = "Laboratorio:";
            this.Controls.Add(this.lblLaboratorio);

            this.txtLaboratorio.Location = new System.Drawing.Point(140, y);
            this.txtLaboratorio.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtLaboratorio);

            y += 30;
            this.lblDosis.Location = new System.Drawing.Point(20, y);
            this.lblDosis.Size = new System.Drawing.Size(100, 20);
            this.lblDosis.Text = "Dosis:";
            this.Controls.Add(this.lblDosis);

            this.txtDosis.Location = new System.Drawing.Point(140, y);
            this.txtDosis.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtDosis);

            y += 30;
            this.lblCategoria.Location = new System.Drawing.Point(20, y);
            this.lblCategoria.Size = new System.Drawing.Size(100, 20);
            this.lblCategoria.Text = "Categoría:";
            this.Controls.Add(this.lblCategoria);

            this.cmbCategoria.Location = new System.Drawing.Point(140, y);
            this.cmbCategoria.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.cmbCategoria);

            y += 30;
            this.lblProveedor.Location = new System.Drawing.Point(20, y);
            this.lblProveedor.Size = new System.Drawing.Size(100, 20);
            this.lblProveedor.Text = "Proveedor:";
            this.Controls.Add(this.lblProveedor);

            this.cmbProveedor.Location = new System.Drawing.Point(140, y);
            this.cmbProveedor.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.cmbProveedor);

            y += 30;
            this.lblPrecioCompra.Location = new System.Drawing.Point(20, y);
            this.lblPrecioCompra.Size = new System.Drawing.Size(100, 20);
            this.lblPrecioCompra.Text = "Precio Compra:";
            this.Controls.Add(this.lblPrecioCompra);

            this.nudPrecioCompra.Location = new System.Drawing.Point(140, y);
            this.nudPrecioCompra.Size = new System.Drawing.Size(120, 23);
            this.nudPrecioCompra.DecimalPlaces = 2;
            this.nudPrecioCompra.Minimum = 0;
            this.nudPrecioCompra.Maximum = 999999;
            this.Controls.Add(this.nudPrecioCompra);

            y += 30;
            this.lblPrecioVenta.Location = new System.Drawing.Point(20, y);
            this.lblPrecioVenta.Size = new System.Drawing.Size(100, 20);
            this.lblPrecioVenta.Text = "Precio Venta:";
            this.Controls.Add(this.lblPrecioVenta);

            this.nudPrecioVenta.Location = new System.Drawing.Point(140, y);
            this.nudPrecioVenta.Size = new System.Drawing.Size(120, 23);
            this.nudPrecioVenta.DecimalPlaces = 2;
            this.nudPrecioVenta.Minimum = 0;
            this.nudPrecioVenta.Maximum = 999999;
            this.Controls.Add(this.nudPrecioVenta);

            y += 30;
            this.lblStockMinimo.Location = new System.Drawing.Point(20, y);
            this.lblStockMinimo.Size = new System.Drawing.Size(100, 20);
            this.lblStockMinimo.Text = "Stock Mínimo:";
            this.Controls.Add(this.lblStockMinimo);

            this.nudStockMinimo.Location = new System.Drawing.Point(140, y);
            this.nudStockMinimo.Size = new System.Drawing.Size(80, 23);
            this.nudStockMinimo.Minimum = 0;
            this.nudStockMinimo.Maximum = 9999;
            this.nudStockMinimo.Value = 10;
            this.Controls.Add(this.nudStockMinimo);

            y += 30;
            this.chkRequiereReceta.Location = new System.Drawing.Point(140, y);
            this.chkRequiereReceta.Size = new System.Drawing.Size(200, 20);
            this.chkRequiereReceta.Text = "Requiere Receta";
            this.Controls.Add(this.chkRequiereReceta);

            y += 40;
            this.lblLote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLote.Location = new System.Drawing.Point(20, y);
            this.lblLote.Size = new System.Drawing.Size(200, 20);
            this.lblLote.Text = "Inventario Inicial";
            this.Controls.Add(this.lblLote);

            y += 30;
            this.lblNumeroLote.Location = new System.Drawing.Point(20, y);
            this.lblNumeroLote.Size = new System.Drawing.Size(100, 20);
            this.lblNumeroLote.Text = "Número Lote:";
            this.Controls.Add(this.lblNumeroLote);

            this.txtNumeroLote.Location = new System.Drawing.Point(140, y);
            this.txtNumeroLote.Size = new System.Drawing.Size(240, 23);
            this.Controls.Add(this.txtNumeroLote);

            y += 30;
            this.lblCantidadLote.Location = new System.Drawing.Point(20, y);
            this.lblCantidadLote.Size = new System.Drawing.Size(100, 20);
            this.lblCantidadLote.Text = "Cantidad:";
            this.Controls.Add(this.lblCantidadLote);

            this.nudCantidadLote.Location = new System.Drawing.Point(140, y);
            this.nudCantidadLote.Size = new System.Drawing.Size(80, 23);
            this.nudCantidadLote.Minimum = 0;
            this.nudCantidadLote.Maximum = 99999;
            this.Controls.Add(this.nudCantidadLote);

            y += 30;
            this.lblFechaVencimiento.Location = new System.Drawing.Point(20, y);
            this.lblFechaVencimiento.Size = new System.Drawing.Size(110, 20);
            this.lblFechaVencimiento.Text = "Fecha Vencimiento:";
            this.Controls.Add(this.lblFechaVencimiento);

            this.dtpFechaVencimiento.Location = new System.Drawing.Point(140, y);
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(150, 23);
            this.dtpFechaVencimiento.MinDate = DateTime.Now;
            this.dtpFechaVencimiento.Value = DateTime.Now.AddYears(1);
            this.Controls.Add(this.dtpFechaVencimiento);

            y += 50;
            this.btnGuardar.Location = new System.Drawing.Point(140, y);
            this.btnGuardar.Size = new System.Drawing.Size(100, 35);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Click += btnGuardar_Click;
            this.Controls.Add(this.btnGuardar);

            this.btnCancelar.Location = new System.Drawing.Point(250, y);
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Click += btnCancelar_Click;
            this.Controls.Add(this.btnCancelar);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, y + 50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Medicamento";

            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCompra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadLote)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}