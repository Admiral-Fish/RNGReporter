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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.searchText = new System.Windows.Forms.Label();
            this.cancel = new RNGReporter.GlassButton();
            this.buttonSearch = new RNGReporter.GlassButton();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(241, 337);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Credits to CollectorTogami for this";
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
            this.Frame});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewValues.Location = new System.Drawing.Point(15, 167);
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
            this.dataGridViewValues.Size = new System.Drawing.Size(392, 167);
            this.dataGridViewValues.TabIndex = 46;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            dataGridViewCellStyle2.Format = "X";
            this.Time.DefaultCellStyle = dataGridViewCellStyle2;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 123;
            // 
            // Frame
            // 
            this.Frame.DataPropertyName = "Frame";
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
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
            this.Controls.Add(this.label5);
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
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DoubleBufferedDataGridView dataGridViewValues;
        private System.Windows.Forms.Label label5;
        private GlassButton buttonSearch;
        private System.Windows.Forms.Label searchText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        private GlassButton cancel;
    }
}