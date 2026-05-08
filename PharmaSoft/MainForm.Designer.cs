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
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            btnInventario = new Button();
            label2 = new Label();
            label3 = new Label();
            panel3 = new Panel();
            label15 = new Label();
            dataGridView1 = new DataGridView();
            label5 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            panel4 = new Panel();
            button11 = new Button();
            button10 = new Button();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            comboBox1 = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 17);
            label1.Name = "label1";
            label1.Size = new Size(255, 65);
            label1.TabIndex = 0;
            label1.Text = "🏪PharmaSoft";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btnInventario);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(22, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 654);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 192, 192);
            panel2.Controls.Add(button9);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button6);
            panel2.Location = new Point(11, 306);
            panel2.Name = "panel2";
            panel2.Size = new Size(300, 317);
            panel2.TabIndex = 6;
            // 
            // button9
            // 
            button9.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button9.Location = new Point(26, 164);
            button9.Name = "button9";
            button9.Size = new Size(177, 34);
            button9.TabIndex = 3;
            button9.Text = "💰Finanzas";
            button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.Location = new Point(26, 112);
            button8.Name = "button8";
            button8.Size = new Size(177, 34);
            button8.TabIndex = 2;
            button8.Text = "🚛Proveedores";
            button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(26, 61);
            button7.Name = "button7";
            button7.Size = new Size(177, 34);
            button7.TabIndex = 1;
            button7.Text = "\U0001f6d2Compras";
            button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(26, 15);
            button6.Name = "button6";
            button6.Size = new Size(177, 34);
            button6.TabIndex = 0;
            button6.Text = "\U0001f9d1‍\U0001f91d‍\U0001f9d1Clientes";
            button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ControlLightLight;
            button5.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(28, 265);
            button5.Name = "button5";
            button5.Size = new Size(186, 38);
            button5.TabIndex = 5;
            button5.Text = "📝Ventas";
            button5.TextAlign = ContentAlignment.TopLeft;
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlLightLight;
            button4.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(28, 225);
            button4.Name = "button4";
            button4.Size = new Size(186, 34);
            button4.TabIndex = 4;
            button4.Text = "\U0001f9f3Lotes";
            button4.TextAlign = ContentAlignment.TopLeft;
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlLightLight;
            button3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(28, 180);
            button3.Name = "button3";
            button3.Size = new Size(186, 39);
            button3.TabIndex = 3;
            button3.Text = "🏷️Categorias";
            button3.TextAlign = ContentAlignment.TopLeft;
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlLightLight;
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(28, 140);
            button2.Name = "button2";
            button2.Size = new Size(186, 34);
            button2.TabIndex = 2;
            button2.Text = "💊 Medicamentos";
            button2.TextAlign = ContentAlignment.TopLeft;
            button2.UseVisualStyleBackColor = false;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = SystemColors.ControlLightLight;
            btnInventario.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnInventario.Location = new Point(28, 100);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(186, 34);
            btnInventario.TabIndex = 1;
            btnInventario.Text = "📦Inventario";
            btnInventario.TextAlign = ContentAlignment.TopLeft;
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnclickInventario;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(388, 56);
            label2.Name = "label2";
            label2.Size = new Size(190, 30);
            label2.TabIndex = 2;
            label2.Text = "Gestion Farmacia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(419, 155);
            label3.Name = "label3";
            label3.Size = new Size(94, 32);
            label3.TabIndex = 3;
            label3.Text = "Ventas";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLightLight;
            panel3.Controls.Add(label15);
            panel3.Controls.Add(dataGridView1);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(textBox1);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(388, 190);
            panel3.Name = "panel3";
            panel3.Size = new Size(552, 458);
            panel3.TabIndex = 4;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = SystemColors.ActiveCaption;
            label15.Location = new Point(373, 63);
            label15.Name = "label15";
            label15.Size = new Size(37, 25);
            label15.TabIndex = 4;
            label15.Text = "🔍";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(13, 144);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(523, 299);
            dataGridView1.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(31, 113);
            label5.Name = "label5";
            label5.Size = new Size(133, 28);
            label5.TabIndex = 2;
            label5.Text = "Venta Actual";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(13, 60);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(354, 31);
            textBox1.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(13, 14);
            label4.Name = "label4";
            label4.Size = new Size(189, 32);
            label4.TabIndex = 0;
            label4.Text = "Punto de Venta";
            label4.Click += label4_Click;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ControlLightLight;
            panel4.Controls.Add(button11);
            panel4.Controls.Add(button10);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(textBox2);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(comboBox1);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(label6);
            panel4.Location = new Point(986, 190);
            panel4.Name = "panel4";
            panel4.Size = new Size(362, 443);
            panel4.TabIndex = 5;
            // 
            // button11
            // 
            button11.BackColor = Color.Red;
            button11.Location = new Point(108, 394);
            button11.Name = "button11";
            button11.Size = new Size(167, 46);
            button11.TabIndex = 12;
            button11.Text = "Cancelar Venta";
            button11.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            button10.BackColor = Color.Lime;
            button10.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.Location = new Point(108, 324);
            button10.Name = "button10";
            button10.Size = new Size(167, 53);
            button10.TabIndex = 11;
            button10.Text = "Finalizar Venta";
            button10.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(29, 293);
            label14.Name = "label14";
            label14.Size = new Size(80, 28);
            label14.TabIndex = 10;
            label14.Text = "Cambio";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(29, 256);
            label13.Name = "label13";
            label13.Size = new Size(153, 28);
            label13.TabIndex = 9;
            label13.Text = "Monto Recibido";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(207, 218);
            label12.Name = "label12";
            label12.Size = new Size(146, 28);
            label12.TabIndex = 8;
            label12.Text = "Efectivo/Tarjeta";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(29, 218);
            label11.Name = "label11";
            label11.Size = new Size(159, 28);
            label11.TabIndex = 7;
            label11.Text = "Metodo de Pago";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Lime;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(29, 173);
            label10.Name = "label10";
            label10.Size = new Size(124, 28);
            label10.TabIndex = 6;
            label10.Text = "TOTAL NETO:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(21, 138);
            label9.Name = "label9";
            label9.Size = new Size(169, 28);
            label9.TabIndex = 5;
            label9.Text = "Lista de Articulos: ";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(108, 95);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(181, 31);
            textBox2.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(29, 95);
            label8.Name = "label8";
            label8.Size = new Size(43, 25);
            label8.TabIndex = 3;
            label8.Text = "NIF:";
            label8.Click += label8_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(108, 51);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(195, 33);
            comboBox1.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(21, 51);
            label7.Name = "label7";
            label7.Size = new Size(81, 28);
            label7.TabIndex = 1;
            label7.Text = "Cliente: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(21, 8);
            label6.Name = "label6";
            label6.Size = new Size(187, 28);
            label6.TabIndex = 0;
            label6.Text = "Resumen de Venta";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 70);
            // 
            // ingresarProductoToolStripMenuItem
            // 
            ingresarProductoToolStripMenuItem = new ToolStripMenuItem();
            listaProductosToolStripMenuItem = new ToolStripMenuItem();
            ingresarProductoToolStripMenuItem.Name = "ingresarProductoToolStripMenuItem";
            ingresarProductoToolStripMenuItem.Size = new Size(180, 22);
            ingresarProductoToolStripMenuItem.Text = "Ingresar producto";
            ingresarProductoToolStripMenuItem.Click += ingresarProductoToolStripMenuItem_Click;
            listaProductosToolStripMenuItem.Name = "listaProductosToolStripMenuItem";
            listaProductosToolStripMenuItem.Size = new Size(180, 22);
            listaProductosToolStripMenuItem.Text = "Lista productos";
            listaProductosToolStripMenuItem.Click += listaProductosToolStripMenuItem_Click;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
                ingresarProductoToolStripMenuItem,
                listaProductosToolStripMenuItem
            });
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1448, 750);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button btnInventario;
        private Panel panel2;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Label label2;
        private Label label3;
        private Panel panel3;
        private TextBox textBox1;
        private Label label4;
        private DataGridView dataGridView1;
        private Label label5;
        private Panel panel4;
        private Label label6;
        private Label label8;
        private ComboBox comboBox1;
        private Label label7;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ingresarProductoToolStripMenuItem;
        private ToolStripMenuItem listaProductosToolStripMenuItem;
        private Label label9;
        private TextBox textBox2;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label14;
        private Label label13;
        private Button button11;
        private Button button10;
        private Label label15;
    }
}
