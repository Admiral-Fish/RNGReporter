using RNGReporter.Controls;

namespace RNGReporter
{
    partial class ChainToSID
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
            this.buttonCalculate = new RNGReporter.GlassButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxNature = new RNGReporter.GlassComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.comboBoxAbility = new RNGReporter.GlassComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxGender = new RNGReporter.GlassComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClear = new RNGReporter.GlassButton();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new RNGReporter.GlassButton();
            this.label4 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.ivs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PokemonNature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PokemonAbility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PokemonGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCancel = new RNGReporter.GlassButton();
            this.labelSid = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBoxID = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxHP = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxAtk = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxDef = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpA = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpD = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxSpe = new RNGReporter.Controls.MaskedTextBox2();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCalculate.ForeColor = System.Drawing.Color.Black;
            this.buttonCalculate.Location = new System.Drawing.Point(330, 120);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCalculate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCalculate.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculate.TabIndex = 23;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Nature";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(327, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Spe";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(289, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "SpD";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(247, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "SpA";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(205, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Def";
            // 
            // comboBoxNature
            // 
            this.comboBoxNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNature.ForeColor = System.Drawing.Color.Black;
            this.comboBoxNature.FormattingEnabled = true;
            this.comboBoxNature.Location = new System.Drawing.Point(12, 22);
            this.comboBoxNature.Name = "comboBoxNature";
            this.comboBoxNature.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxNature.Size = new System.Drawing.Size(105, 21);
            this.comboBoxNature.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(163, 6);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 13);
            this.label19.TabIndex = 4;
            this.label19.Text = "Atk";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(122, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(22, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "HP";
            // 
            // comboBoxAbility
            // 
            this.comboBoxAbility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAbility.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAbility.FormattingEnabled = true;
            this.comboBoxAbility.Items.AddRange(new object[] {
            "Single Ability",
            "Ability 0",
            "Ability 1"});
            this.comboBoxAbility.Location = new System.Drawing.Point(12, 61);
            this.comboBoxAbility.Name = "comboBoxAbility";
            this.comboBoxAbility.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxAbility.Size = new System.Drawing.Size(105, 21);
            this.comboBoxAbility.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Ability";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.ForeColor = System.Drawing.Color.Black;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new System.Drawing.Point(125, 61);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxGender.Size = new System.Drawing.Size(240, 21);
            this.comboBoxGender.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Gender / Gender Ratio";
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonClear.ForeColor = System.Drawing.Color.Black;
            this.buttonClear.Location = new System.Drawing.Point(250, 120);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonClear.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 22;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Possibilities:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ForeColor = System.Drawing.Color.Black;
            this.buttonOk.Location = new System.Drawing.Point(250, 348);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonOk.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 27;
            this.buttonOk.Text = "OK";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Trainer ID";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(82, 130);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(31, 13);
            this.labelInfo.TabIndex = 21;
            this.labelInfo.Text = "8192";
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
            this.ivs,
            this.PokemonNature,
            this.PokemonAbility,
            this.PokemonGender});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewValues.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewValues.Location = new System.Drawing.Point(12, 153);
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
            this.dataGridViewValues.Size = new System.Drawing.Size(395, 185);
            this.dataGridViewValues.TabIndex = 24;
            // 
            // ivs
            // 
            this.ivs.DataPropertyName = "Ivs";
            dataGridViewCellStyle2.NullValue = null;
            this.ivs.DefaultCellStyle = dataGridViewCellStyle2;
            this.ivs.HeaderText = "Pokemon IVs";
            this.ivs.Name = "ivs";
            this.ivs.ReadOnly = true;
            this.ivs.Width = 140;
            // 
            // PokemonNature
            // 
            this.PokemonNature.DataPropertyName = "Nature";
            this.PokemonNature.HeaderText = "Nature";
            this.PokemonNature.Name = "PokemonNature";
            this.PokemonNature.ReadOnly = true;
            this.PokemonNature.Width = 70;
            // 
            // PokemonAbility
            // 
            this.PokemonAbility.DataPropertyName = "Ability";
            this.PokemonAbility.HeaderText = "Ability";
            this.PokemonAbility.Name = "PokemonAbility";
            this.PokemonAbility.ReadOnly = true;
            this.PokemonAbility.Width = 70;
            // 
            // PokemonGender
            // 
            this.PokemonGender.DataPropertyName = "Gender";
            this.PokemonGender.HeaderText = "Gender";
            this.PokemonGender.Name = "PokemonGender";
            this.PokemonGender.ReadOnly = true;
            this.PokemonGender.Width = 70;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(330, 348);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 28;
            this.buttonCancel.Text = "Cancel";
            // 
            // labelSid
            // 
            this.labelSid.AutoSize = true;
            this.labelSid.Location = new System.Drawing.Point(67, 353);
            this.labelSid.Name = "labelSid";
            this.labelSid.Size = new System.Drawing.Size(0, 13);
            this.labelSid.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "SID:";
            // 
            // maskedTextBoxID
            // 
            this.maskedTextBoxID.Hex = false;
            this.maskedTextBoxID.Location = new System.Drawing.Point(12, 101);
            this.maskedTextBoxID.Mask = "00000";
            this.maskedTextBoxID.Name = "maskedTextBoxID";
            this.maskedTextBoxID.Size = new System.Drawing.Size(105, 20);
            this.maskedTextBoxID.TabIndex = 19;
            this.maskedTextBoxID.ValidatingType = typeof(int);
            // 
            // maskedTextBoxHP
            // 
            this.maskedTextBoxHP.Hex = false;
            this.maskedTextBoxHP.Location = new System.Drawing.Point(125, 22);
            this.maskedTextBoxHP.Mask = "00";
            this.maskedTextBoxHP.Name = "maskedTextBoxHP";
            this.maskedTextBoxHP.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxHP.TabIndex = 3;
            this.maskedTextBoxHP.Tag = "ivs";
            // 
            // maskedTextBoxAtk
            // 
            this.maskedTextBoxAtk.Hex = false;
            this.maskedTextBoxAtk.Location = new System.Drawing.Point(166, 22);
            this.maskedTextBoxAtk.Mask = "00";
            this.maskedTextBoxAtk.Name = "maskedTextBoxAtk";
            this.maskedTextBoxAtk.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxAtk.TabIndex = 5;
            this.maskedTextBoxAtk.Tag = "ivs";
            // 
            // maskedTextBoxDef
            // 
            this.maskedTextBoxDef.Hex = false;
            this.maskedTextBoxDef.Location = new System.Drawing.Point(207, 22);
            this.maskedTextBoxDef.Mask = "00";
            this.maskedTextBoxDef.Name = "maskedTextBoxDef";
            this.maskedTextBoxDef.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxDef.TabIndex = 7;
            this.maskedTextBoxDef.Tag = "ivs";
            // 
            // maskedTextBoxSpA
            // 
            this.maskedTextBoxSpA.Hex = false;
            this.maskedTextBoxSpA.Location = new System.Drawing.Point(248, 22);
            this.maskedTextBoxSpA.Mask = "00";
            this.maskedTextBoxSpA.Name = "maskedTextBoxSpA";
            this.maskedTextBoxSpA.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpA.TabIndex = 9;
            this.maskedTextBoxSpA.Tag = "ivs";
            // 
            // maskedTextBoxSpD
            // 
            this.maskedTextBoxSpD.Hex = false;
            this.maskedTextBoxSpD.Location = new System.Drawing.Point(289, 22);
            this.maskedTextBoxSpD.Mask = "00";
            this.maskedTextBoxSpD.Name = "maskedTextBoxSpD";
            this.maskedTextBoxSpD.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpD.TabIndex = 11;
            this.maskedTextBoxSpD.Tag = "ivs";
            // 
            // maskedTextBoxSpe
            // 
            this.maskedTextBoxSpe.Hex = false;
            this.maskedTextBoxSpe.Location = new System.Drawing.Point(330, 22);
            this.maskedTextBoxSpe.Mask = "00";
            this.maskedTextBoxSpe.Name = "maskedTextBoxSpe";
            this.maskedTextBoxSpe.Size = new System.Drawing.Size(35, 20);
            this.maskedTextBoxSpe.TabIndex = 13;
            this.maskedTextBoxSpe.Tag = "ivs";
            // 
            // ChainToSID
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(424, 383);
            this.ControlBox = false;
            this.Controls.Add(this.labelSid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewValues);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.maskedTextBoxID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxGender);
            this.Controls.Add(this.comboBoxAbility);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedTextBoxHP);
            this.Controls.Add(this.buttonCalculate);
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
            this.Name = "ChainToSID";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chain To SID";
            this.Load += new System.EventHandler(this.ChainToSID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaskedTextBox2 maskedTextBoxHP;
        private RNGReporter.GlassButton buttonCalculate;
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
        private RNGReporter.GlassComboBox comboBoxNature;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private RNGReporter.GlassComboBox comboBoxAbility;
        private System.Windows.Forms.Label label1;
        private RNGReporter.GlassComboBox comboBoxGender;
        private System.Windows.Forms.Label label2;
        private RNGReporter.GlassButton buttonClear;
        private System.Windows.Forms.Label label3;
        private RNGReporter.GlassButton buttonOk;
        private System.Windows.Forms.Label label4;
        private MaskedTextBox2 maskedTextBoxID;
        private System.Windows.Forms.Label labelInfo;
        private RNGReporter.DoubleBufferedDataGridView dataGridViewValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn ivs;
        private System.Windows.Forms.DataGridViewTextBoxColumn PokemonNature;
        private System.Windows.Forms.DataGridViewTextBoxColumn PokemonAbility;
        private System.Windows.Forms.DataGridViewTextBoxColumn PokemonGender;
        private RNGReporter.GlassButton buttonCancel;
        private System.Windows.Forms.Label labelSid;
        private System.Windows.Forms.Label label5;
    }
}