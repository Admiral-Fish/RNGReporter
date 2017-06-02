namespace RNGReporter
{
    partial class SetIVs
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glassButton1 = new RNGReporter.GlassButton();
            this.glassButton12 = new RNGReporter.GlassButton();
            this.glassButton18 = new RNGReporter.GlassButton();
            this._MaskedTextBox1 = new RNGReporter._MaskedTextBox(this.components);
            this.SuspendLayout();
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton1.ForeColor = System.Drawing.Color.Black;
            this.glassButton1.Location = new System.Drawing.Point(37, 5);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton1.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton1.Size = new System.Drawing.Size(42, 21);
            this.glassButton1.TabIndex = 122;
            this.glassButton1.TabStop = false;
            this.glassButton1.Text = "==31";
            this.glassButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // glassButton12
            // 
            this.glassButton12.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton12.ForeColor = System.Drawing.Color.Black;
            this.glassButton12.Location = new System.Drawing.Point(89, 5);
            this.glassButton12.Name = "glassButton12";
            this.glassButton12.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton12.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton12.Size = new System.Drawing.Size(42, 21);
            this.glassButton12.TabIndex = 128;
            this.glassButton12.TabStop = false;
            this.glassButton12.Text = "==30";
            this.glassButton12.Click += new System.EventHandler(this.button1_2_Click);
            // 
            // glassButton18
            // 
            this.glassButton18.BackColor = System.Drawing.Color.AntiqueWhite;
            this.glassButton18.ForeColor = System.Drawing.Color.Black;
            this.glassButton18.Location = new System.Drawing.Point(141, 5);
            this.glassButton18.Name = "glassButton18";
            this.glassButton18.OuterBorderColor = System.Drawing.Color.Transparent;
            this.glassButton18.ShineColor = System.Drawing.SystemColors.Window;
            this.glassButton18.Size = new System.Drawing.Size(42, 21);
            this.glassButton18.TabIndex = 134;
            this.glassButton18.TabStop = false;
            this.glassButton18.Text = "Clear";
            this.glassButton18.Click += new System.EventHandler(this.button1_3_Click);
            // 
            // _MaskedTextBox1
            // 
            this._MaskedTextBox1.Location = new System.Drawing.Point(5, 5);
            this._MaskedTextBox1.Mask = "99";
            this._MaskedTextBox1.Name = "_MaskedTextBox1";
            this._MaskedTextBox1.Size = new System.Drawing.Size(26, 20);
            this._MaskedTextBox1.TabIndex = 0;
            this._MaskedTextBox1.Tag = "ivs";
            // 
            // SetIVs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._MaskedTextBox1);
            this.Controls.Add(this.glassButton18);
            this.Controls.Add(this.glassButton12);
            this.Controls.Add(this.glassButton1);
            this.Name = "SetIVs";
            this.Size = new System.Drawing.Size(188, 30);
            this.ResumeLayout(false);
            this.PerformLayout();           
        }

        #endregion
        private GlassButton glassButton1;
        private GlassButton glassButton12;
        private GlassButton glassButton18;
        public _MaskedTextBox _MaskedTextBox1;
    }
}
