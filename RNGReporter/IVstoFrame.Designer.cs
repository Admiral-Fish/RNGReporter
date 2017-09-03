namespace RNGReporter
{
    partial class IVstoFrame
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new RNGReporter._DataGridView(this.components);
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.glassButton1 = new RNGReporter.GlassButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new RNGReporter._ComboBox(this.components);
            this.maskedTextBox1 = new RNGReporter._MaskedTextBox(this.components);
            this.maskedTextBox4 = new RNGReporter._MaskedTextBox(this.components);
            this.maskedTextBox3 = new RNGReporter._MaskedTextBox(this.components);
            this.maskedTextBox2 = new RNGReporter._MaskedTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this._MaskedTextBox1 = new RNGReporter._MaskedTextBox(this.components);
            this.glassButton2_3 = new RNGReporter.GlassButton();
            this.glassButton2_2 = new RNGReporter.GlassButton();
            this.glassButton2_1 = new RNGReporter.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.setIVs5 = new RNGReporter.SetIVs();
            this.setIVs4 = new RNGReporter.SetIVs();
            this.setIVs3 = new RNGReporter.SetIVs();
            this.setIVs2 = new RNGReporter.SetIVs();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.setIVs1 = new RNGReporter.SetIVs();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.glassButton1);
            this.groupBox1.Location = new System.Drawing.Point(322, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 356);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Frame Finder";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Frame});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(7, 23);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(103, 287);
            this.dataGridView1.TabIndex = 128;
            this.dataGridView1.TabStop = false;
            // 
            // Frame
            // 
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
            this.Frame.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton1.ForeColor = System.Drawing.Color.Black;
            this.glassButton1.Location = new System.Drawing.Point(30, 321);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton1.Size = new System.Drawing.Size(56, 23);
            this.glassButton1.TabIndex = 0;
            this.glassButton1.Tag = "";
            this.glassButton1.Text = "Search";
            this.glassButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.maskedTextBox1);
            this.groupBox2.Controls.Add(this.maskedTextBox4);
            this.groupBox2.Controls.Add(this.maskedTextBox3);
            this.groupBox2.Controls.Add(this.maskedTextBox2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this._MaskedTextBox1);
            this.groupBox2.Controls.Add(this.glassButton2_3);
            this.groupBox2.Controls.Add(this.glassButton2_2);
            this.groupBox2.Controls.Add(this.glassButton2_1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.setIVs5);
            this.groupBox2.Controls.Add(this.setIVs4);
            this.groupBox2.Controls.Add(this.setIVs3);
            this.groupBox2.Controls.Add(this.setIVs2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.setIVs1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 356);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Config";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Method 1",
            "Method 2",
            "Method 4",
            "Colosseum\\XD"});
            this.comboBox1.Location = new System.Drawing.Point(104, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(104, 50);
            this.maskedTextBox1.Mask = "AAAAAAAA";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(182, 20);
            this.maskedTextBox1.TabIndex = 2;
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.Location = new System.Drawing.Point(104, 131);
            this.maskedTextBox4.Mask = "9999999999";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(182, 20);
            this.maskedTextBox4.TabIndex = 5;
            this.maskedTextBox4.Tag = "frame";
            this.maskedTextBox4.Text = "100000";
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Location = new System.Drawing.Point(104, 104);
            this.maskedTextBox3.Mask = "9999999999";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(182, 20);
            this.maskedTextBox3.TabIndex = 4;
            this.maskedTextBox3.Tag = "frame";
            this.maskedTextBox3.Text = "1";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(104, 77);
            this.maskedTextBox2.Mask = "AAAAAAAA";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(182, 20);
            this.maskedTextBox2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 146;
            this.label3.Text = "PID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 145;
            this.label9.Text = "Initial seed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 143;
            this.label10.Text = "Starting Frame";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Max Frame";
            // 
            // _MaskedTextBox1
            // 
            this._MaskedTextBox1.Location = new System.Drawing.Point(104, 323);
            this._MaskedTextBox1.Mask = "99";
            this._MaskedTextBox1.Name = "_MaskedTextBox1";
            this._MaskedTextBox1.Size = new System.Drawing.Size(26, 20);
            this._MaskedTextBox1.TabIndex = 11;
            this._MaskedTextBox1.Tag = "ivs";
            this._MaskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this._MaskedTextBox1_KeyDown);
            // 
            // glassButton2_3
            // 
            this.glassButton2_3.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton2_3.ForeColor = System.Drawing.Color.Black;
            this.glassButton2_3.Location = new System.Drawing.Point(240, 323);
            this.glassButton2_3.Name = "glassButton2_3";
            this.glassButton2_3.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2_3.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton2_3.Size = new System.Drawing.Size(42, 21);
            this.glassButton2_3.TabIndex = 138;
            this.glassButton2_3.TabStop = false;
            this.glassButton2_3.Text = "Clear";
            this.glassButton2_3.Click += new System.EventHandler(this.button1_3_Click);
            // 
            // glassButton2_2
            // 
            this.glassButton2_2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton2_2.ForeColor = System.Drawing.Color.Black;
            this.glassButton2_2.Location = new System.Drawing.Point(188, 323);
            this.glassButton2_2.Name = "glassButton2_2";
            this.glassButton2_2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2_2.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton2_2.Size = new System.Drawing.Size(42, 21);
            this.glassButton2_2.TabIndex = 137;
            this.glassButton2_2.TabStop = false;
            this.glassButton2_2.Text = "==30";
            this.glassButton2_2.Click += new System.EventHandler(this.button1_2_Click);
            // 
            // glassButton2_1
            // 
            this.glassButton2_1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton2_1.ForeColor = System.Drawing.Color.Black;
            this.glassButton2_1.Location = new System.Drawing.Point(136, 323);
            this.glassButton2_1.Name = "glassButton2_1";
            this.glassButton2_1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton2_1.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton2_1.Size = new System.Drawing.Size(42, 21);
            this.glassButton2_1.TabIndex = 136;
            this.glassButton2_1.TabStop = false;
            this.glassButton2_1.Text = "==31";
            this.glassButton2_1.Click += new System.EventHandler(this.button1_1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Method";
            // 
            // setIVs5
            // 
            this.setIVs5.Location = new System.Drawing.Point(99, 289);
            this.setIVs5.Name = "setIVs5";
            this.setIVs5.Size = new System.Drawing.Size(189, 28);
            this.setIVs5.TabIndex = 10;
            // 
            // setIVs4
            // 
            this.setIVs4.Location = new System.Drawing.Point(99, 260);
            this.setIVs4.Name = "setIVs4";
            this.setIVs4.Size = new System.Drawing.Size(189, 28);
            this.setIVs4.TabIndex = 9;
            // 
            // setIVs3
            // 
            this.setIVs3.Location = new System.Drawing.Point(99, 231);
            this.setIVs3.Name = "setIVs3";
            this.setIVs3.Size = new System.Drawing.Size(189, 28);
            this.setIVs3.TabIndex = 8;
            // 
            // setIVs2
            // 
            this.setIVs2.Location = new System.Drawing.Point(99, 202);
            this.setIVs2.Name = "setIVs2";
            this.setIVs2.Size = new System.Drawing.Size(189, 28);
            this.setIVs2.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 125;
            this.label8.Text = "Spe";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 297);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 124;
            this.label7.Text = "Spd";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 123;
            this.label6.Text = "Spa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "Def";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 127;
            this.label4.Text = "Atk";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 126;
            this.label12.Text = "Hp";
            // 
            // setIVs1
            // 
            this.setIVs1.Location = new System.Drawing.Point(99, 174);
            this.setIVs1.Name = "setIVs1";
            this.setIVs1.Size = new System.Drawing.Size(189, 28);
            this.setIVs1.TabIndex = 6;
            // 
            // IVstoFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 376);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "IVstoFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRLG/RSE IVs to Frame";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private SetIVs setIVs1;
        private GlassButton glassButton1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private SetIVs setIVs5;
        private SetIVs setIVs4;
        private SetIVs setIVs3;
        public SetIVs setIVs2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private _DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        public _MaskedTextBox _MaskedTextBox1;
        private GlassButton glassButton2_3;
        private GlassButton glassButton2_2;
        private GlassButton glassButton2_1;
        public _MaskedTextBox maskedTextBox1;
        public _MaskedTextBox maskedTextBox4;
        public _MaskedTextBox maskedTextBox3;
        public _MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private _ComboBox comboBox1;
    }
}
