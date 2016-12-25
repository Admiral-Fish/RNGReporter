namespace RNGReporter
{
    partial class SearchFlips
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
            this.btnTails = new RNGReporter.GlassButton();
            this.buttonCancel = new RNGReporter.GlassButton();
            this.buttonOk = new RNGReporter.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHeads = new RNGReporter.GlassButton();
            this.labelResults = new System.Windows.Forms.Label();
            this.txtFlips = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTails
            // 
            this.btnTails.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnTails.ForeColor = System.Drawing.Color.Black;
            this.btnTails.Image = global::RNGReporter.Properties.Resources.tails;
            this.btnTails.Location = new System.Drawing.Point(93, 12);
            this.btnTails.Name = "btnTails";
            this.btnTails.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnTails.ShineColor = System.Drawing.SystemColors.Window;
            this.btnTails.Size = new System.Drawing.Size(80, 80);
            this.btnTails.TabIndex = 1;
            this.btnTails.Text = "T";
            this.btnTails.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnTails.Click += new System.EventHandler(this.btnTails_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(410, 160);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 48;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ForeColor = System.Drawing.Color.Black;
            this.buttonOk.Location = new System.Drawing.Point(329, 160);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonOk.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 47;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Click the buttons to match your coin flip pattern.";
            // 
            // btnHeads
            // 
            this.btnHeads.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnHeads.ForeColor = System.Drawing.Color.Black;
            this.btnHeads.Image = global::RNGReporter.Properties.Resources.heads;
            this.btnHeads.Location = new System.Drawing.Point(12, 12);
            this.btnHeads.Name = "btnHeads";
            this.btnHeads.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnHeads.ShineColor = System.Drawing.SystemColors.Window;
            this.btnHeads.Size = new System.Drawing.Size(80, 80);
            this.btnHeads.TabIndex = 0;
            this.btnHeads.Text = "H";
            this.btnHeads.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnHeads.Click += new System.EventHandler(this.btnHeads_Click);
            // 
            // labelResults
            // 
            this.labelResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(12, 145);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(133, 13);
            this.labelResults.TabIndex = 57;
            this.labelResults.Text = "Number of possible results:";
            // 
            // txtFlips
            // 
            this.txtFlips.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlips.Location = new System.Drawing.Point(12, 111);
            this.txtFlips.Name = "txtFlips";
            this.txtFlips.Size = new System.Drawing.Size(548, 31);
            this.txtFlips.TabIndex = 56;
            this.txtFlips.TextChanged += new System.EventHandler(this.txtFlips_TextChanged);
            // 
            // SearchFlips
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(565, 195);
            this.ControlBox = false;
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.txtFlips);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.btnTails);
            this.Controls.Add(this.btnHeads);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SearchFlips";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Coin Flips";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RNGReporter.GlassButton btnHeads;
        private RNGReporter.GlassButton btnTails;
        private RNGReporter.GlassButton buttonCancel;
        private RNGReporter.GlassButton buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.TextBox txtFlips;
    }
}