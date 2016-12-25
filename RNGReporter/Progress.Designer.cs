namespace RNGReporter
{
    partial class Progress
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSearched = new System.Windows.Forms.Label();
            this.labelFound = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new RNGReporter.DualProgressBar();
            this.plainBackgroundPainter1 = new RNGReporter.PlainBackgroundPainter();
            this.plainBorderPainter1 = new RNGReporter.PlainBorderPainter();
            this.plainProgressPainter2 = new RNGReporter.PlainProgressPainter();
            this.gradientGlossPainter1 = new RNGReporter.GradientGlossPainter();
            this.plainProgressPainter1 = new RNGReporter.PlainProgressPainter();
            this.roundGlossPainter1 = new RNGReporter.RoundGlossPainter();
            this.buttonCancel = new RNGReporter.GlassButton();
            this.plainBackgroundPainter2 = new RNGReporter.PlainBackgroundPainter();
            this.roundGlossPainter2 = new RNGReporter.RoundGlossPainter();
            this.buttonPause = new RNGReporter.GlassButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Found";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Searched";
            // 
            // labelSearched
            // 
            this.labelSearched.AutoSize = true;
            this.labelSearched.Location = new System.Drawing.Point(78, 13);
            this.labelSearched.Name = "labelSearched";
            this.labelSearched.Size = new System.Drawing.Size(0, 13);
            this.labelSearched.TabIndex = 4;
            // 
            // labelFound
            // 
            this.labelFound.AutoSize = true;
            this.labelFound.Location = new System.Drawing.Point(78, 33);
            this.labelFound.Name = "labelFound";
            this.labelFound.Size = new System.Drawing.Size(0, 13);
            this.labelFound.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // progressBar
            // 
            this.progressBar.BackgroundPainter = this.plainBackgroundPainter1;
            this.progressBar.BorderPainter = this.plainBorderPainter1;
            this.progressBar.Location = new System.Drawing.Point(22, 56);
            this.progressBar.MarqueePercentage = 25;
            this.progressBar.MarqueeSpeed = 1;
            this.progressBar.MarqueeStep = 5;
            this.progressBar.MasterMaximum = 100;
            this.progressBar.MasterPainter = this.plainProgressPainter2;
            this.progressBar.MasterProgressPadding = 0;
            this.progressBar.MasterValue = 0;
            this.progressBar.Maximum = 100;
            this.progressBar.Minimum = 0;
            this.progressBar.Name = "progressBar";
            this.progressBar.PaintMasterFirst = true;
            this.progressBar.ProgressPadding = 2;
            this.progressBar.ProgressPainter = this.plainProgressPainter1;
            this.progressBar.ProgressType = RNGReporter.ProgressType.Smooth;
            this.progressBar.ShowPercentage = false;
            this.progressBar.Size = new System.Drawing.Size(258, 16);
            this.progressBar.TabIndex = 6;
            this.progressBar.Value = 0;
            // 
            // plainBackgroundPainter1
            // 
            this.plainBackgroundPainter1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.plainBackgroundPainter1.GlossPainter = null;
            // 
            // plainBorderPainter1
            // 
            this.plainBorderPainter1.Color = System.Drawing.Color.Black;
            this.plainBorderPainter1.RoundedCorners = true;
            this.plainBorderPainter1.Style = RNGReporter.PlainBorderPainter.PlainBorderStyle.Flat;
            // 
            // plainProgressPainter2
            // 
            this.plainProgressPainter2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.plainProgressPainter2.GlossPainter = this.gradientGlossPainter1;
            this.plainProgressPainter2.LeadingEdge = System.Drawing.Color.Transparent;
            this.plainProgressPainter2.ProgressBorderPainter = null;
            // 
            // gradientGlossPainter1
            // 
            this.gradientGlossPainter1.AlphaHigh = 240;
            this.gradientGlossPainter1.AlphaLow = 0;
            this.gradientGlossPainter1.Angle = 90F;
            this.gradientGlossPainter1.Color = System.Drawing.Color.DimGray;
            this.gradientGlossPainter1.PercentageCovered = 50;
            this.gradientGlossPainter1.Style = RNGReporter.GlossStyle.Top;
            this.gradientGlossPainter1.Successor = null;
            // 
            // plainProgressPainter1
            // 
            this.plainProgressPainter1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.plainProgressPainter1.GlossPainter = this.roundGlossPainter1;
            this.plainProgressPainter1.LeadingEdge = System.Drawing.Color.Transparent;
            this.plainProgressPainter1.ProgressBorderPainter = this.plainBorderPainter1;
            // 
            // roundGlossPainter1
            // 
            this.roundGlossPainter1.AlphaHigh = 240;
            this.roundGlossPainter1.AlphaLow = 0;
            this.roundGlossPainter1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.roundGlossPainter1.Style = RNGReporter.GlossStyle.Both;
            this.roundGlossPainter1.Successor = null;
            this.roundGlossPainter1.TaperHeight = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(205, 78);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // plainBackgroundPainter2
            // 
            this.plainBackgroundPainter2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.plainBackgroundPainter2.GlossPainter = this.gradientGlossPainter1;
            // 
            // roundGlossPainter2
            // 
            this.roundGlossPainter2.AlphaHigh = 150;
            this.roundGlossPainter2.AlphaLow = 0;
            this.roundGlossPainter2.Color = System.Drawing.Color.Maroon;
            this.roundGlossPainter2.Style = RNGReporter.GlossStyle.Bottom;
            this.roundGlossPainter2.Successor = this.gradientGlossPainter1;
            this.roundGlossPainter2.TaperHeight = 6;
            // 
            // buttonPause
            // 
            this.buttonPause.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonPause.ForeColor = System.Drawing.Color.Black;
            this.buttonPause.Location = new System.Drawing.Point(124, 78);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonPause.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 7;
            this.buttonPause.Text = "Pause";
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 112);
            this.ControlBox = false;
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelFound);
            this.Controls.Add(this.labelSearched);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Time Finder Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RNGReporter.GlassButton buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSearched;
        private System.Windows.Forms.Label labelFound;
        private PlainBackgroundPainter plainBackgroundPainter1;
        private System.Windows.Forms.Timer timer1;
        private PlainBackgroundPainter plainBackgroundPainter2;
        private GradientGlossPainter gradientGlossPainter1;
        private PlainBorderPainter plainBorderPainter1;
        private PlainProgressPainter plainProgressPainter2;
        private PlainProgressPainter plainProgressPainter1;
        private RoundGlossPainter roundGlossPainter1;
        private RoundGlossPainter roundGlossPainter2;
        private DualProgressBar progressBar;
        private GlassButton buttonPause;
    }
}