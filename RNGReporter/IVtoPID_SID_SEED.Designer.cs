using RNGReporter.Controls;

namespace RNGReporter
{
    partial class IVtoPID_SID_SEED
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IVtoPID_SID_SEED));
            this.buttonGenerate = new RNGReporter.GlassButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxNature = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.MonsterSeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F125 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setSeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSeed = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonCancel = new RNGReporter.GlassButton();
            this.buttonOk = new RNGReporter.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSid = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBoxID = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxHP = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxAtk = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxDef = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpA = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpD = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpe = new RNGReporter.Controls.MaskedTextBox2();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonGenerate.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerate.Location = new System.Drawing.Point(436, 21);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 39;
            this.buttonGenerate.Text = "Find";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Nature";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(322, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "Spe";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(284, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "SpD";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(242, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "SpA";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(200, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "Def";
            // 
            // comboBoxNature
            // 
            this.comboBoxNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNature.FormattingEnabled = true;
            this.comboBoxNature.Location = new System.Drawing.Point(7, 24);
            this.comboBoxNature.Name = "comboBoxNature";
            this.comboBoxNature.Size = new System.Drawing.Size(105, 21);
            this.comboBoxNature.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(158, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 13);
            this.label19.TabIndex = 29;
            this.label19.Text = "Atk";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(117, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(22, 13);
            this.label20.TabIndex = 27;
            this.label20.Text = "HP";
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
            this.MonsterSeed,
            this.Pid,
            this.Method,
            this.Ability,
            this.F50,
            this.F125,
            this.F25,
            this.F75,
            this.SidColumn});
            this.dataGridViewValues.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewValues.Location = new System.Drawing.Point(7, 197);
            this.dataGridViewValues.MultiSelect = false;
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewValues.RowHeadersVisible = false;
            this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewValues.ShowCellErrors = false;
            this.dataGridViewValues.ShowCellToolTips = false;
            this.dataGridViewValues.ShowEditingIcon = false;
            this.dataGridViewValues.ShowRowErrors = false;
            this.dataGridViewValues.Size = new System.Drawing.Size(509, 158);
            this.dataGridViewValues.TabIndex = 40;
            this.dataGridViewValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewValues_MouseDown);
            // 
            // MonsterSeed
            // 
            this.MonsterSeed.DataPropertyName = "MonsterSeed";
            dataGridViewCellStyle2.Format = "X";
            this.MonsterSeed.DefaultCellStyle = dataGridViewCellStyle2;
            this.MonsterSeed.HeaderText = "Seed";
            this.MonsterSeed.Name = "MonsterSeed";
            this.MonsterSeed.ReadOnly = true;
            this.MonsterSeed.Width = 70;
            // 
            // Pid
            // 
            this.Pid.DataPropertyName = "Pid";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8.75F);
            dataGridViewCellStyle3.Format = "X";
            dataGridViewCellStyle3.NullValue = null;
            this.Pid.DefaultCellStyle = dataGridViewCellStyle3;
            this.Pid.HeaderText = "PID";
            this.Pid.Name = "Pid";
            this.Pid.ReadOnly = true;
            this.Pid.Width = 70;
            // 
            // Method
            // 
            this.Method.DataPropertyName = "Method";
            this.Method.HeaderText = "Method";
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            this.Method.Width = 105;
            // 
            // Ability
            // 
            this.Ability.DataPropertyName = "Ability";
            this.Ability.HeaderText = "Ability";
            this.Ability.Name = "Ability";
            this.Ability.ReadOnly = true;
            this.Ability.Width = 40;
            // 
            // F50
            // 
            this.F50.DataPropertyName = "Female50";
            this.F50.HeaderText = "50%";
            this.F50.Name = "F50";
            this.F50.ReadOnly = true;
            this.F50.Width = 40;
            // 
            // F125
            // 
            this.F125.DataPropertyName = "Female125";
            this.F125.HeaderText = "12.5%";
            this.F125.Name = "F125";
            this.F125.ReadOnly = true;
            this.F125.Width = 40;
            // 
            // F25
            // 
            this.F25.DataPropertyName = "Female25";
            this.F25.HeaderText = "25%";
            this.F25.Name = "F25";
            this.F25.ReadOnly = true;
            this.F25.Width = 40;
            // 
            // F75
            // 
            this.F75.DataPropertyName = "Female75";
            this.F75.HeaderText = "75%";
            this.F75.Name = "F75";
            this.F75.ReadOnly = true;
            this.F75.Width = 40;
            // 
            // SidColumn
            // 
            this.SidColumn.DataPropertyName = "Sid";
            this.SidColumn.HeaderText = "SID";
            this.SidColumn.Name = "SidColumn";
            this.SidColumn.ReadOnly = true;
            this.SidColumn.Width = 60;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSeedToolStripMenuItem,
            this.setSIDToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(119, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // setSeedToolStripMenuItem
            // 
            this.setSeedToolStripMenuItem.Name = "setSeedToolStripMenuItem";
            this.setSeedToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.setSeedToolStripMenuItem.Text = "Set Seed";
            this.setSeedToolStripMenuItem.Click += new System.EventHandler(this.setSeedToolStripMenuItem_Click);
            // 
            // setSIDToolStripMenuItem
            // 
            this.setSIDToolStripMenuItem.Name = "setSIDToolStripMenuItem";
            this.setSIDToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.setSIDToolStripMenuItem.Text = "Set SID";
            this.setSIDToolStripMenuItem.Click += new System.EventHandler(this.setSIDToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 68);
            this.label1.TabIndex = 41;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(396, 46);
            this.label2.TabIndex = 42;
            this.label2.Text = "This screen also shows a compatible Secret ID that will make the listed Pokemon S" +
    "hiny based on the Trainer ID entered in the main screen.  This calculation is do" +
    "ne with the Trainer ID shown below.";
            // 
            // labelSeed
            // 
            this.labelSeed.AutoSize = true;
            this.labelSeed.Location = new System.Drawing.Point(129, 367);
            this.labelSeed.Name = "labelSeed";
            this.labelSeed.Size = new System.Drawing.Size(0, 13);
            this.labelSeed.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(87, 367);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Seed:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(443, 362);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 46;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ForeColor = System.Drawing.Color.Black;
            this.buttonOk.Location = new System.Drawing.Point(362, 362);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonOk.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 45;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSid
            // 
            this.labelSid.AutoSize = true;
            this.labelSid.Location = new System.Drawing.Point(259, 367);
            this.labelSid.Name = "labelSid";
            this.labelSid.Size = new System.Drawing.Size(0, 13);
            this.labelSid.TabIndex = 56;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 367);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "SID:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(430, 25);
            this.label4.TabIndex = 57;
            this.label4.Text = "This tool cannot be used for 5th Generation Pokémon.";
            // 
            // maskedTextBoxID
            // 
            this.maskedTextBoxID.Hex = false;
            this.maskedTextBoxID.Location = new System.Drawing.Point(287, 171);
            this.maskedTextBoxID.Mask = "00000";
            this.maskedTextBoxID.Name = "maskedTextBoxID";
            this.maskedTextBoxID.Size = new System.Drawing.Size(74, 20);
            this.maskedTextBoxID.TabIndex = 53;
            this.maskedTextBoxID.ValidatingType = typeof(int);
            // 
            // maskedTextBoxHP
            // 
            this.maskedTextBoxHP.Hex = false;
            this.maskedTextBoxHP.Location = new System.Drawing.Point(120, 24);
            this.maskedTextBoxHP.Mask = "00";
            this.maskedTextBoxHP.Name = "maskedTextBoxHP";
            this.maskedTextBoxHP.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxHP.TabIndex = 28;
            this.maskedTextBoxHP.Tag = "ivs";
            // 
            // maskedTextBoxAtk
            // 
            this.maskedTextBoxAtk.Hex = false;
            this.maskedTextBoxAtk.Location = new System.Drawing.Point(161, 24);
            this.maskedTextBoxAtk.Mask = "00";
            this.maskedTextBoxAtk.Name = "maskedTextBoxAtk";
            this.maskedTextBoxAtk.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxAtk.TabIndex = 30;
            this.maskedTextBoxAtk.Tag = "ivs";
            // 
            // maskedTextBoxDef
            // 
            this.maskedTextBoxDef.Hex = false;
            this.maskedTextBoxDef.Location = new System.Drawing.Point(202, 24);
            this.maskedTextBoxDef.Mask = "00";
            this.maskedTextBoxDef.Name = "maskedTextBoxDef";
            this.maskedTextBoxDef.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxDef.TabIndex = 32;
            this.maskedTextBoxDef.Tag = "ivs";
            // 
            // maskedTextBoxSpA
            // 
            this.maskedTextBoxSpA.Hex = false;
            this.maskedTextBoxSpA.Location = new System.Drawing.Point(243, 24);
            this.maskedTextBoxSpA.Mask = "00";
            this.maskedTextBoxSpA.Name = "maskedTextBoxSpA";
            this.maskedTextBoxSpA.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpA.TabIndex = 34;
            this.maskedTextBoxSpA.Tag = "ivs";
            // 
            // maskedTextBoxSpD
            // 
            this.maskedTextBoxSpD.Hex = false;
            this.maskedTextBoxSpD.Location = new System.Drawing.Point(284, 24);
            this.maskedTextBoxSpD.Mask = "00";
            this.maskedTextBoxSpD.Name = "maskedTextBoxSpD";
            this.maskedTextBoxSpD.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpD.TabIndex = 36;
            this.maskedTextBoxSpD.Tag = "ivs";
            // 
            // maskedTextBoxSpe
            // 
            this.maskedTextBoxSpe.Hex = false;
            this.maskedTextBoxSpe.Location = new System.Drawing.Point(325, 24);
            this.maskedTextBoxSpe.Mask = "00";
            this.maskedTextBoxSpe.Name = "maskedTextBoxSpe";
            this.maskedTextBoxSpe.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpe.TabIndex = 38;
            this.maskedTextBoxSpe.Tag = "ivs";
            // 
            // IVtoPID_SID_SEED
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(523, 390);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maskedTextBoxID);
            this.Controls.Add(this.labelSeed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewValues);
            this.Controls.Add(this.maskedTextBoxHP);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.maskedTextBoxAtk);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.maskedTextBoxDef);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.maskedTextBoxSpA);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.maskedTextBoxSpD);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.maskedTextBoxSpe);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.comboBoxNature);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "IVtoPID_SID_SEED";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IVs to PID / SEED";
            this.Load += new System.EventHandler(this.IVtoPID_SID_SEED_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaskedTextBox2 maskedTextBoxHP;
        private RNGReporter.GlassButton buttonGenerate;
        private MaskedTextBox2 maskedTextBoxAtk;
        private System.Windows.Forms.Label label10;
        private MaskedTextBox2 maskedTextBoxDef;
        private System.Windows.Forms.Label label15;
        private MaskedTextBox2 maskedTextBoxSpA;
        private System.Windows.Forms.Label label16;
        private MaskedTextBox2 maskedTextBoxSpD;
        private System.Windows.Forms.Label label17;
        private MaskedTextBox2 maskedTextBoxSpe;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBoxNature;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private RNGReporter.DoubleBufferedDataGridView dataGridViewValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSeed;
        private System.Windows.Forms.Label label9;
        private RNGReporter.GlassButton buttonCancel;
        private RNGReporter.GlassButton buttonOk;
        private MaskedTextBox2 maskedTextBoxID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setSeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSIDToolStripMenuItem;
        private System.Windows.Forms.Label labelSid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonsterSeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ability;
        private System.Windows.Forms.DataGridViewTextBoxColumn F50;
        private System.Windows.Forms.DataGridViewTextBoxColumn F125;
        private System.Windows.Forms.DataGridViewTextBoxColumn F25;
        private System.Windows.Forms.DataGridViewTextBoxColumn F75;
        private System.Windows.Forms.DataGridViewTextBoxColumn SidColumn;
    }
}