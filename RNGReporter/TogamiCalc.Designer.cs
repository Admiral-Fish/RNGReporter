namespace RNGReporter
{
    partial class TogamiCalc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.components = new System.ComponentModel.Container();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.searchText = new System.Windows.Forms.Label();
            this.cancel = new RNGReporter.GlassButton();
            this.buttonSearch = new RNGReporter.GlassButton();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.copySeed = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.minFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.targetSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.initialSeed = new RNGReporter.Controls.MaskedTextBox2();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(310, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "This is a RTC calculator intended for use with Gamecube games";
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySeed});
            this.contextMenuStripGrid.Name = "contextMenuStripGrid";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(203, 48);
            this.contextMenuStripGrid.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripGrid_Opening);
            // 
            // copySeed
            // 
            this.copySeed.Name = "copySeed";
            this.copySeed.Size = new System.Drawing.Size(202, 22);
            this.copySeed.Text = "Copy Seed to Clipboard";
            this.copySeed.Click += new System.EventHandler(this.copySeedToClipboard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Seed at 1/1/2000 at 00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(12, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Target seed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(208, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Min frame";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(208, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Max frame";
            // 
            // searchText
            // 
            this.searchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchText.AutoSize = true;
            this.searchText.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.searchText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.searchText.Location = new System.Drawing.Point(12, 337);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(96, 13);
            this.searchText.TabIndex = 49;
            this.searchText.Text = "Awaiting command";
            this.searchText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cancel
            // 
            this.cancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.cancel.ForeColor = System.Drawing.Color.Black;
            this.cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cancel.Location = new System.Drawing.Point(355, 138);
            this.cancel.Name = "cancel";
            this.cancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.cancel.ShineColor = System.Drawing.SystemColors.Window;
            this.cancel.Size = new System.Drawing.Size(52, 23);
            this.cancel.TabIndex = 50;
            this.cancel.Text = "Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonSearch.ForeColor = System.Drawing.Color.Black;
            this.buttonSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSearch.Location = new System.Drawing.Point(297, 138);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonSearch.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonSearch.Size = new System.Drawing.Size(52, 23);
            this.buttonSearch.TabIndex = 48;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // dataGridViewValues
            // 
            this.dataGridViewValues.AllowUserToAddRows = false;
            this.dataGridViewValues.AllowUserToDeleteRows = false;
            this.dataGridViewValues.AllowUserToResizeRows = false;
            this.dataGridViewValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Frame,
            this.Seed});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewValues.Location = new System.Drawing.Point(15, 167);
            this.dataGridViewValues.MultiSelect = false;
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.ContextMenuStrip = this.contextMenuStripGrid;
            this.dataGridViewValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewValues.RowHeadersVisible = false;
            this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewValues.ShowCellErrors = false;
            this.dataGridViewValues.ShowCellToolTips = false;
            this.dataGridViewValues.ShowEditingIcon = false;
            this.dataGridViewValues.ShowRowErrors = false;
            this.dataGridViewValues.Size = new System.Drawing.Size(392, 167);
            this.dataGridViewValues.TabIndex = 46;
            this.dataGridViewValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewValues_MouseDown);
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            dataGridViewCellStyle6.Format = "X";
            this.Time.DefaultCellStyle = dataGridViewCellStyle6;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Frame
            // 
            this.Frame.DataPropertyName = "Frame";
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
            // 
            // Seed
            // 
            this.Seed.DataPropertyName = "Seed";
            this.Seed.HeaderText = "Seed";
            this.Seed.Name = "Seed";
            this.Seed.ReadOnly = true;
            // 
            // maxFrame
            // 
            this.maxFrame.Hex = false;
            this.maxFrame.Location = new System.Drawing.Point(210, 108);
            this.maxFrame.Mask = "0000000";
            this.maxFrame.Name = "maxFrame";
            this.maxFrame.Size = new System.Drawing.Size(51, 20);
            this.maxFrame.TabIndex = 25;
            this.maxFrame.Text = "1000000";
            this.maxFrame.ValidatingType = typeof(int);
            // 
            // minFrame
            // 
            this.minFrame.Hex = false;
            this.minFrame.Location = new System.Drawing.Point(210, 69);
            this.minFrame.Mask = "0000000";
            this.minFrame.Name = "minFrame";
            this.minFrame.Size = new System.Drawing.Size(51, 20);
            this.minFrame.TabIndex = 24;
            this.minFrame.Text = "500000";
            this.minFrame.ValidatingType = typeof(int);
            // 
            // targetSeed
            // 
            this.targetSeed.Hex = true;
            this.targetSeed.Location = new System.Drawing.Point(15, 108);
            this.targetSeed.Mask = "AAAAAAAA";
            this.targetSeed.Name = "targetSeed";
            this.targetSeed.Size = new System.Drawing.Size(96, 20);
            this.targetSeed.TabIndex = 9;
            // 
            // initialSeed
            // 
            this.initialSeed.Hex = true;
            this.initialSeed.Location = new System.Drawing.Point(15, 69);
            this.initialSeed.Mask = "AAAAAAAA";
            this.initialSeed.Name = "initialSeed";
            this.initialSeed.Size = new System.Drawing.Size(96, 20);
            this.initialSeed.TabIndex = 8;
            // 
            // TogamiCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 359);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridViewValues);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.maxFrame);
            this.Controls.Add(this.minFrame);
            this.Controls.Add(this.targetSeed);
            this.Controls.Add(this.initialSeed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "TogamiCalc";
            this.Text = "TogamiCalc";
            this.contextMenuStripGrid.ResumeLayout(false);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TogamiCalc_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MaskedTextBox2 initialSeed;
        private Controls.MaskedTextBox2 targetSeed;
        private Controls.MaskedTextBox2 minFrame;
        private Controls.MaskedTextBox2 maxFrame;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem copySeed;
        private DoubleBufferedDataGridView dataGridViewValues;
        private GlassButton buttonSearch;
        private System.Windows.Forms.Label searchText;
        private GlassButton cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seed;
    }
}