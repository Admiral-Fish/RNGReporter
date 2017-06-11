namespace RNGReporter
{
    partial class PIDToIVs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySeed = new System.Windows.Forms.ToolStripMenuItem();
            this.moveResultToMainForm = new System.Windows.Forms.ToolStripMenuItem();
            this.moveIVsToMainForm = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonGenerate = new RNGReporter.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.contextMenuStripGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewValues
            // 
            this.dataGridViewValues.AllowUserToAddRows = false;
            this.dataGridViewValues.AllowUserToDeleteRows = false;
            this.dataGridViewValues.AllowUserToResizeRows = false;
            this.dataGridViewValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewValues.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewValues.ColumnHeadersHeight = 20;
            this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewValues.ContextMenuStrip = this.contextMenuStripGrid;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewValues.Location = new System.Drawing.Point(12, 110);
            this.dataGridViewValues.MultiSelect = false;
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewValues.RowHeadersVisible = false;
            this.dataGridViewValues.RowTemplate.Height = 20;
            this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewValues.ShowCellErrors = false;
            this.dataGridViewValues.ShowCellToolTips = false;
            this.dataGridViewValues.ShowEditingIcon = false;
            this.dataGridViewValues.ShowRowErrors = false;
            this.dataGridViewValues.Size = new System.Drawing.Size(439, 213);
            this.dataGridViewValues.TabIndex = 11;
            this.dataGridViewValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewValues_MouseDown);
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySeed,
            this.moveResultToMainForm,
            this.moveIVsToMainForm});
            this.contextMenuStripGrid.Name = "contextMenuStripGrid";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(226, 70);
            this.contextMenuStripGrid.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripGrid_Opening);
            // 
            // copySeed
            // 
            this.copySeed.Name = "copySeed";
            this.copySeed.Size = new System.Drawing.Size(225, 22);
            this.copySeed.Text = "Copy Seed to Clipboard";
            this.copySeed.Click += new System.EventHandler(this.copySeedToClipboard_Click);
            // 
            // moveResultToMainForm
            // 
            this.moveResultToMainForm.Name = "moveResultToMainForm";
            this.moveResultToMainForm.Size = new System.Drawing.Size(225, 22);
            this.moveResultToMainForm.Text = "Move result to main window";
            this.moveResultToMainForm.Click += new System.EventHandler(this.moveResultToMainForm_Click);
            // 
            // moveIVsToMainForm
            // 
            this.moveIVsToMainForm.Name = "moveIVsToMainForm";
            this.moveIVsToMainForm.Size = new System.Drawing.Size(225, 22);
            this.moveIVsToMainForm.Text = "Move IVs to main window";
            this.moveIVsToMainForm.Click += new System.EventHandler(this.moveIVsToMainForm_Click);
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Hex = true;
            this.textBoxSeed.Location = new System.Drawing.Point(12, 25);
            this.textBoxSeed.Mask = "AAAAAAAA";
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(60, 20);
            this.textBoxSeed.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "PID (Hex)";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonGenerate.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonGenerate.Location = new System.Drawing.Point(377, 81);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonGenerate.Size = new System.Drawing.Size(74, 23);
            this.buttonGenerate.TabIndex = 14;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // PIDToIVs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 335);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxSeed);
            this.Controls.Add(this.dataGridViewValues);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PIDToIVs";
            this.Text = "PID To IVs";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.contextMenuStripGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedDataGridView dataGridViewValues;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.ToolStripMenuItem moveResultToMainForm;
        private System.Windows.Forms.ToolStripMenuItem moveIVsToMainForm;
        private System.Windows.Forms.ToolStripMenuItem copySeed;
        private Controls.MaskedTextBox2 textBoxSeed;
        private System.Windows.Forms.Label label12;
        private GlassButton buttonGenerate;
    }
}