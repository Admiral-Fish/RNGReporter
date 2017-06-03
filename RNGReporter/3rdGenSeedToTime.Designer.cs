namespace RNGReporter
{
    partial class thirdGenSeedToTime
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGenerate = new RNGReporter.GlassButton();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seedToTimeSeed = new RNGReporter.Controls.MaskedTextBox2();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.SuspendLayout();
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 43;
            this.label20.Text = "16/32-Bit Seed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 126;
            this.label1.Text = "Enter either a 16-bit seed or a 32-bit seed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 13);
            this.label2.TabIndex = 127;
            this.label2.Text = "for the closest 16-bit seed RTC";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonGenerate.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerate.Location = new System.Drawing.Point(101, 22);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 46;
            this.buttonGenerate.Text = "Find";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // dataGridViewValues
            // 
            this.dataGridViewValues.AllowUserToAddRows = false;
            this.dataGridViewValues.AllowUserToDeleteRows = false;
            this.dataGridViewValues.AllowUserToResizeRows = false;
            this.dataGridViewValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Seconds});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewValues.Location = new System.Drawing.Point(12, 105);
            this.dataGridViewValues.MultiSelect = false;
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewValues.RowHeadersVisible = false;
            this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewValues.ShowCellErrors = false;
            this.dataGridViewValues.ShowCellToolTips = false;
            this.dataGridViewValues.ShowEditingIcon = false;
            this.dataGridViewValues.ShowRowErrors = false;
            this.dataGridViewValues.Size = new System.Drawing.Size(199, 271);
            this.dataGridViewValues.TabIndex = 45;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            dataGridViewCellStyle2.Format = "X";
            this.Time.DefaultCellStyle = dataGridViewCellStyle2;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Seconds
            // 
            this.Seconds.DataPropertyName = "Seconds";
            this.Seconds.HeaderText = "Seconds";
            this.Seconds.Name = "Seconds";
            this.Seconds.ReadOnly = true;
            this.Seconds.Width = 95;
            // 
            // seedToTimeSeed
            // 
            this.seedToTimeSeed.Hex = true;
            this.seedToTimeSeed.Location = new System.Drawing.Point(15, 25);
            this.seedToTimeSeed.Mask = "AAAAAAAA";
            this.seedToTimeSeed.Name = "seedToTimeSeed";
            this.seedToTimeSeed.Size = new System.Drawing.Size(59, 20);
            this.seedToTimeSeed.TabIndex = 44;
            // 
            // thirdGenSeedToTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 388);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.dataGridViewValues);
            this.Controls.Add(this.seedToTimeSeed);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "thirdGenSeedToTime";
            this.Text = "16-Bit Seed to Time";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label20;
        private Controls.MaskedTextBox2 seedToTimeSeed;
        private DoubleBufferedDataGridView dataGridViewValues;
        private GlassButton buttonGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seconds;
    }
}