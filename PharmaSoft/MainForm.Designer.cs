namespace PharmaSoft
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            verInventarioToolStripMenuItem = new ToolStripMenuItem();
            buscarProductoToolStripMenuItem = new ToolStripMenuItem();
            agregarMedicamentoToolStripMenuItem = new ToolStripMenuItem();
            editarProductoToolStripMenuItem = new ToolStripMenuItem();
            eliminarProductoToolStripMenuItem = new ToolStripMenuItem();
            ajusteStockToolStripMenuItem = new ToolStripMenuItem();
            entradaMercanciaToolStripMenuItem = new ToolStripMenuItem();
            salidaMercanciaToolStripMenuItem = new ToolStripMenuItem();
            productosAgotadosToolStripMenuItem = new ToolStripMenuItem();
            proximosAVencerToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { verInventarioToolStripMenuItem, buscarProductoToolStripMenuItem, agregarMedicamentoToolStripMenuItem, editarProductoToolStripMenuItem, eliminarProductoToolStripMenuItem, ajusteStockToolStripMenuItem, entradaMercanciaToolStripMenuItem, salidaMercanciaToolStripMenuItem, productosAgotadosToolStripMenuItem, proximosAVencerToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(229, 224);
            // 
            // verInventarioToolStripMenuItem
            // 
            verInventarioToolStripMenuItem.Name = "verInventarioToolStripMenuItem";
            verInventarioToolStripMenuItem.Size = new Size(228, 22);
            verInventarioToolStripMenuItem.Text = "Ver inventario general";
            verInventarioToolStripMenuItem.Click += verInventarioToolStripMenuItem_Click;
            // 
            // buscarProductoToolStripMenuItem
            // 
            buscarProductoToolStripMenuItem.Name = "buscarProductoToolStripMenuItem";
            buscarProductoToolStripMenuItem.Size = new Size(228, 22);
            buscarProductoToolStripMenuItem.Text = "Buscar producto";
            buscarProductoToolStripMenuItem.Click += buscarProductoToolStripMenuItem_Click;
            // 
            // agregarMedicamentoToolStripMenuItem
            // 
            agregarMedicamentoToolStripMenuItem.Name = "agregarMedicamentoToolStripMenuItem";
            agregarMedicamentoToolStripMenuItem.Size = new Size(228, 22);
            agregarMedicamentoToolStripMenuItem.Text = "Agregar medicamento";
            agregarMedicamentoToolStripMenuItem.Click += agregarMedicamentoToolStripMenuItem_Click;
            // 
            // editarProductoToolStripMenuItem
            // 
            editarProductoToolStripMenuItem.Name = "editarProductoToolStripMenuItem";
            editarProductoToolStripMenuItem.Size = new Size(228, 22);
            editarProductoToolStripMenuItem.Text = "Editar producto";
            editarProductoToolStripMenuItem.Click += editarProductoToolStripMenuItem_Click;
            // 
            // eliminarProductoToolStripMenuItem
            // 
            eliminarProductoToolStripMenuItem.Name = "eliminarProductoToolStripMenuItem";
            eliminarProductoToolStripMenuItem.Size = new Size(228, 22);
            eliminarProductoToolStripMenuItem.Text = "Eliminar producto";
            eliminarProductoToolStripMenuItem.Click += eliminarProductoToolStripMenuItem_Click;
            // 
            // ajusteStockToolStripMenuItem
            // 
            ajusteStockToolStripMenuItem.Name = "ajusteStockToolStripMenuItem";
            ajusteStockToolStripMenuItem.Size = new Size(228, 22);
            ajusteStockToolStripMenuItem.Text = "Ajuste de stock";
            ajusteStockToolStripMenuItem.Click += ajusteStockToolStripMenuItem_Click;
            // 
            // entradaMercanciaToolStripMenuItem
            // 
            entradaMercanciaToolStripMenuItem.Name = "entradaMercanciaToolStripMenuItem";
            entradaMercanciaToolStripMenuItem.Size = new Size(228, 22);
            entradaMercanciaToolStripMenuItem.Text = "Entrada de mercancía";
            entradaMercanciaToolStripMenuItem.Click += entradaMercanciaToolStripMenuItem_Click;
            // 
            // salidaMercanciaToolStripMenuItem
            // 
            salidaMercanciaToolStripMenuItem.Name = "salidaMercanciaToolStripMenuItem";
            salidaMercanciaToolStripMenuItem.Size = new Size(228, 22);
            salidaMercanciaToolStripMenuItem.Text = "Salida de mercancía";
            salidaMercanciaToolStripMenuItem.Click += salidaMercanciaToolStripMenuItem_Click;
            // 
            // productosAgotadosToolStripMenuItem
            // 
            productosAgotadosToolStripMenuItem.Name = "productosAgotadosToolStripMenuItem";
            productosAgotadosToolStripMenuItem.Size = new Size(228, 22);
            productosAgotadosToolStripMenuItem.Text = "Productos agotados";
            productosAgotadosToolStripMenuItem.Click += productosAgotadosToolStripMenuItem_Click;
            // 
            // proximosAVencerToolStripMenuItem
            // 
            proximosAVencerToolStripMenuItem.Name = "proximosAVencerToolStripMenuItem";
            proximosAVencerToolStripMenuItem.Size = new Size(228, 22);
            proximosAVencerToolStripMenuItem.Text = "Productos próximos a vencer";
            proximosAVencerToolStripMenuItem.Click += proximosAVencerToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(959, 449);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem verInventarioToolStripMenuItem;
        private ToolStripMenuItem buscarProductoToolStripMenuItem;
        private ToolStripMenuItem agregarMedicamentoToolStripMenuItem;
        private ToolStripMenuItem editarProductoToolStripMenuItem;
        private ToolStripMenuItem eliminarProductoToolStripMenuItem;
        private ToolStripMenuItem ajusteStockToolStripMenuItem;
        private ToolStripMenuItem entradaMercanciaToolStripMenuItem;
        private ToolStripMenuItem salidaMercanciaToolStripMenuItem;
        private ToolStripMenuItem productosAgotadosToolStripMenuItem;
        private ToolStripMenuItem proximosAVencerToolStripMenuItem;
        private ToolStripMenuItem ingresarProductoToolStripMenuItem;
        private ToolStripMenuItem listaProductosToolStripMenuItem;
    }
}
