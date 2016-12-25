using RNGReporter.Controls;

namespace RNGReporter
{
    partial class SearchRoamers
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
            this.buttonCancel = new RNGReporter.GlassButton();
            this.buttonOk = new RNGReporter.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBoxRRoute = new MaskedTextBox2();
            this.maskedTextBoxLRoute = new MaskedTextBox2();
            this.maskedTextBoxERoute = new MaskedTextBox2();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(197, 157);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 26;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(116, 157);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 27;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 101;
            this.label3.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 100;
            this.label2.Text = "E";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 99;
            this.label1.Text = "L";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 48);
            this.label4.TabIndex = 102;
            this.label4.Text = "Please enter the routes of all roaming Pokemon, If any roaming Pokemon is not cur" +
                "rently released leave that field blank.";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 38);
            this.label5.TabIndex = 103;
            this.label5.Text = "Please note that this search method will not function reliably unless all three r" +
                "oamers are released.";
            // 
            // maskedTextBoxRRoute
            // 
            this.maskedTextBoxRRoute.Location = new System.Drawing.Point(36, 113);
            this.maskedTextBoxRRoute.Mask = "00";
            this.maskedTextBoxRRoute.Name = "maskedTextBoxRRoute";
            this.maskedTextBoxRRoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxRRoute.TabIndex = 96;
            this.maskedTextBoxRRoute.ValidatingType = typeof(int);
            // 
            // maskedTextBoxLRoute
            // 
            this.maskedTextBoxLRoute.Location = new System.Drawing.Point(133, 113);
            this.maskedTextBoxLRoute.Mask = "00";
            this.maskedTextBoxLRoute.Name = "maskedTextBoxLRoute";
            this.maskedTextBoxLRoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxLRoute.TabIndex = 98;
            this.maskedTextBoxLRoute.ValidatingType = typeof(int);
            // 
            // maskedTextBoxERoute
            // 
            this.maskedTextBoxERoute.Location = new System.Drawing.Point(84, 113);
            this.maskedTextBoxERoute.Mask = "00";
            this.maskedTextBoxERoute.Name = "maskedTextBoxERoute";
            this.maskedTextBoxERoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxERoute.TabIndex = 97;
            this.maskedTextBoxERoute.ValidatingType = typeof(int);
            // 
            // SearchRoamers
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(284, 192);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maskedTextBoxRRoute);
            this.Controls.Add(this.maskedTextBoxLRoute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maskedTextBoxERoute);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SearchRoamers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Roamers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RNGReporter.GlassButton buttonCancel;
        private RNGReporter.GlassButton buttonOk;
        private MaskedTextBox2 maskedTextBoxRRoute;
        private MaskedTextBox2 maskedTextBoxLRoute;
        private System.Windows.Forms.Label label3;
        private MaskedTextBox2 maskedTextBoxERoute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}