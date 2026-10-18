namespace Ecuaciones_Diferenciales
{
    partial class Form1
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
            groupBox1 = new GroupBox();
            btnCompararH = new Button();
            btnSimular = new Button();
            cmbModelo = new ComboBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            a = new Label();
            txtH = new TextBox();
            txtTf = new TextBox();
            txtT0 = new TextBox();
            txtVc0 = new TextBox();
            txtVt = new TextBox();
            txtN = new TextBox();
            txtIs = new TextBox();
            txtC = new TextBox();
            txtR = new TextBox();
            txtVs = new TextBox();
            dgvEuler = new TabControl();
            tabPage1 = new TabPage();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            tabPage2 = new TabPage();
            dgvEule = new DataGridView();
            tabPage3 = new TabPage();
            splitContainer1 = new SplitContainer();
            dgvExperimental = new DataGridView();
            picFoto = new PictureBox();
            btnCargarFoto = new Button();
            groupBox1.SuspendLayout();
            dgvEuler.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEule).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExperimental).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picFoto).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCompararH);
            groupBox1.Controls.Add(btnSimular);
            groupBox1.Controls.Add(cmbModelo);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(a);
            groupBox1.Controls.Add(txtH);
            groupBox1.Controls.Add(txtTf);
            groupBox1.Controls.Add(txtT0);
            groupBox1.Controls.Add(txtVc0);
            groupBox1.Controls.Add(txtVt);
            groupBox1.Controls.Add(txtN);
            groupBox1.Controls.Add(txtIs);
            groupBox1.Controls.Add(txtC);
            groupBox1.Controls.Add(txtR);
            groupBox1.Controls.Add(txtVs);
            groupBox1.Location = new Point(37, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(290, 403);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parametros";
            // 
            // btnCompararH
            // 
            btnCompararH.Location = new Point(153, 356);
            btnCompararH.Name = "btnCompararH";
            btnCompararH.Size = new Size(75, 23);
            btnCompararH.TabIndex = 20;
            btnCompararH.Text = "Comparar";
            btnCompararH.UseVisualStyleBackColor = true;
            btnCompararH.Click += btnCompararH_Click;
            // 
            // btnSimular
            // 
            btnSimular.AutoSize = true;
            btnSimular.Location = new Point(17, 356);
            btnSimular.Name = "btnSimular";
            btnSimular.Size = new Size(75, 25);
            btnSimular.TabIndex = 1;
            btnSimular.Text = "Simular";
            btnSimular.UseVisualStyleBackColor = true;
            btnSimular.Click += btnSimular_Click;
            // 
            // cmbModelo
            // 
            cmbModelo.FormattingEnabled = true;
            cmbModelo.Items.AddRange(new object[] { "Ecuación del Documento", "Circuito Serie Real" });
            cmbModelo.Location = new Point(107, 312);
            cmbModelo.Name = "cmbModelo";
            cmbModelo.Size = new Size(121, 23);
            cmbModelo.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(17, 291);
            label10.Name = "label10";
            label10.Size = new Size(104, 15);
            label10.TabIndex = 19;
            label10.Text = "Tamaño de paso h";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(17, 262);
            label9.Name = "label9";
            label9.Size = new Size(87, 15);
            label9.TabIndex = 18;
            label9.Text = "Tiempo final Tf";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 233);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 17;
            label8.Text = "Tiempo inicial To";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 204);
            label7.Name = "label7";
            label7.Size = new Size(91, 15);
            label7.TabIndex = 16;
            label7.Text = "Voltaje inicial Vc";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 175);
            label6.Name = "label6";
            label6.Size = new Size(100, 15);
            label6.TabIndex = 15;
            label6.Text = "Voltaje térmico Vt";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 146);
            label5.Name = "label5";
            label5.Size = new Size(101, 15);
            label5.TabIndex = 14;
            label5.Text = "Factor idealidad n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 117);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 13;
            label4.Text = "Corriente Is";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 88);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 12;
            label3.Text = "Capacitancia C";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 59);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 11;
            label2.Text = "Resistencia  R";
            // 
            // a
            // 
            a.AutoSize = true;
            a.Location = new Point(17, 30);
            a.Name = "a";
            a.Size = new Size(94, 15);
            a.TabIndex = 1;
            a.Text = "Voltaje fuente Vs";
            // 
            // txtH
            // 
            txtH.Location = new Point(128, 283);
            txtH.Name = "txtH";
            txtH.Size = new Size(100, 23);
            txtH.TabIndex = 10;
            // 
            // txtTf
            // 
            txtTf.Location = new Point(128, 254);
            txtTf.Name = "txtTf";
            txtTf.Size = new Size(100, 23);
            txtTf.TabIndex = 9;
            // 
            // txtT0
            // 
            txtT0.Location = new Point(128, 225);
            txtT0.Name = "txtT0";
            txtT0.Size = new Size(100, 23);
            txtT0.TabIndex = 8;
            // 
            // txtVc0
            // 
            txtVc0.Location = new Point(128, 196);
            txtVc0.Name = "txtVc0";
            txtVc0.Size = new Size(100, 23);
            txtVc0.TabIndex = 7;
            // 
            // txtVt
            // 
            txtVt.Location = new Point(128, 167);
            txtVt.Name = "txtVt";
            txtVt.Size = new Size(100, 23);
            txtVt.TabIndex = 6;
            // 
            // txtN
            // 
            txtN.Location = new Point(128, 138);
            txtN.Name = "txtN";
            txtN.Size = new Size(100, 23);
            txtN.TabIndex = 5;
            // 
            // txtIs
            // 
            txtIs.Location = new Point(128, 109);
            txtIs.Name = "txtIs";
            txtIs.Size = new Size(100, 23);
            txtIs.TabIndex = 4;
            // 
            // txtC
            // 
            txtC.Location = new Point(128, 80);
            txtC.Name = "txtC";
            txtC.Size = new Size(100, 23);
            txtC.TabIndex = 3;
            // 
            // txtR
            // 
            txtR.Location = new Point(128, 51);
            txtR.Name = "txtR";
            txtR.Size = new Size(100, 23);
            txtR.TabIndex = 2;
            // 
            // txtVs
            // 
            txtVs.Location = new Point(128, 22);
            txtVs.Name = "txtVs";
            txtVs.Size = new Size(100, 23);
            txtVs.TabIndex = 1;
            // 
            // dgvEuler
            // 
            dgvEuler.Controls.Add(tabPage1);
            dgvEuler.Controls.Add(tabPage2);
            dgvEuler.Controls.Add(tabPage3);
            dgvEuler.Location = new Point(333, 21);
            dgvEuler.Name = "dgvEuler";
            dgvEuler.SelectedIndex = 0;
            dgvEuler.Size = new Size(442, 394);
            dgvEuler.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(formsPlot1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(434, 366);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Gráfica";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // formsPlot1
            // 
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(3, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(428, 360);
            formsPlot1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvEule);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(434, 366);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Tabla de Iteraciones";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvEule
            // 
            dgvEule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEule.Dock = DockStyle.Fill;
            dgvEule.Location = new Point(3, 3);
            dgvEule.Name = "dgvEule";
            dgvEule.Size = new Size(428, 360);
            dgvEule.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(splitContainer1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(434, 366);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Datos Experimentales";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvExperimental);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(picFoto);
            splitContainer1.Panel2.Controls.Add(btnCargarFoto);
            splitContainer1.Size = new Size(434, 366);
            splitContainer1.SplitterDistance = 217;
            splitContainer1.TabIndex = 0;
            // 
            // dgvExperimental
            // 
            dgvExperimental.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExperimental.Dock = DockStyle.Left;
            dgvExperimental.Location = new Point(0, 0);
            dgvExperimental.Name = "dgvExperimental";
            dgvExperimental.Size = new Size(215, 366);
            dgvExperimental.TabIndex = 0;
            // 
            // picFoto
            // 
            picFoto.Dock = DockStyle.Fill;
            picFoto.Image = Properties.Resources.WhatsApp_Image_2026_10_16_at_12_25_04;
            picFoto.Location = new Point(0, 23);
            picFoto.Name = "picFoto";
            picFoto.Size = new Size(213, 343);
            picFoto.SizeMode = PictureBoxSizeMode.Zoom;
            picFoto.TabIndex = 1;
            picFoto.TabStop = false;
            // 
            // btnCargarFoto
            // 
            btnCargarFoto.Dock = DockStyle.Top;
            btnCargarFoto.Location = new Point(0, 0);
            btnCargarFoto.Name = "btnCargarFoto";
            btnCargarFoto.Size = new Size(213, 23);
            btnCargarFoto.TabIndex = 0;
            btnCargarFoto.Text = "Cargar foto";
            btnCargarFoto.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvEuler);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            dgvEuler.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEule).EndInit();
            tabPage3.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExperimental).EndInit();
            ((System.ComponentModel.ISupportInitialize)picFoto).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtN;
        private TextBox txtIs;
        private TextBox txtC;
        private TextBox txtR;
        private TextBox txtVs;
        private TextBox txtH;
        private TextBox txtTf;
        private TextBox txtT0;
        private TextBox txtVc0;
        private TextBox txtVt;
        private Button btnCompararH;
        private Button btnSimular;
        private ComboBox cmbModelo;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label a;
        private TabControl dgvEuler;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private SplitContainer splitContainer1;
        private DataGridView dgvExperimental;
        private PictureBox picFoto;
        private Button btnCargarFoto;
        private DataGridView dgvEule;
    }
}
