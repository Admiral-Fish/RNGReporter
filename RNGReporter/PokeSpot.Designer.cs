namespace RNGReporter.Objects
{
    partial class PokeSpot
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
            this.anyAbility = new System.Windows.Forms.Button();
            this.anyGender = new System.Windows.Forms.Button();
            this.anyNature = new System.Windows.Forms.Button();
            this.L_mezapa = new System.Windows.Forms.Label();
            this.natureType = new System.Windows.Forms.ComboBox();
            this.genderType = new System.Windows.Forms.ComboBox();
            this.L_sex = new System.Windows.Forms.Label();
            this.L_ability = new System.Windows.Forms.Label();
            this.abilityType = new System.Windows.Forms.ComboBox();
            this.sid = new System.Windows.Forms.MaskedTextBox();
            this.id = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Shiny_Check = new System.Windows.Forms.CheckBox();
            this.k_dataGridView = new System.Windows.Forms.DataGridView();
            this.anyPokeSpot = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pokeSpotType = new System.Windows.Forms.ComboBox();
            this.Seed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shiny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eighth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quarter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Half = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Three_Fourths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.maxFrame = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSeed = new RNGReporter.Controls.MaskedTextBox2();
            ((System.ComponentModel.ISupportInitialize)(this.k_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // anyAbility
            // 
            this.anyAbility.Location = new System.Drawing.Point(734, 210);
            this.anyAbility.Name = "anyAbility";
            this.anyAbility.Size = new System.Drawing.Size(36, 21);
            this.anyAbility.TabIndex = 286;
            this.anyAbility.Text = "Any";
            this.anyAbility.UseVisualStyleBackColor = true;
            this.anyAbility.Click += new System.EventHandler(this.anyAbility_Click);
            // 
            // anyGender
            // 
            this.anyGender.Location = new System.Drawing.Point(734, 177);
            this.anyGender.Name = "anyGender";
            this.anyGender.Size = new System.Drawing.Size(36, 21);
            this.anyGender.TabIndex = 285;
            this.anyGender.Text = "Any";
            this.anyGender.UseVisualStyleBackColor = true;
            this.anyGender.Click += new System.EventHandler(this.anyGender_Click);
            // 
            // anyNature
            // 
            this.anyNature.Location = new System.Drawing.Point(734, 141);
            this.anyNature.Name = "anyNature";
            this.anyNature.Size = new System.Drawing.Size(36, 21);
            this.anyNature.TabIndex = 284;
            this.anyNature.Text = "Any";
            this.anyNature.UseVisualStyleBackColor = true;
            this.anyNature.Click += new System.EventHandler(this.anyNature_Click);
            // 
            // L_mezapa
            // 
            this.L_mezapa.Location = new System.Drawing.Point(481, 141);
            this.L_mezapa.Name = "L_mezapa";
            this.L_mezapa.Size = new System.Drawing.Size(57, 20);
            this.L_mezapa.TabIndex = 283;
            this.L_mezapa.Text = "Nature";
            this.L_mezapa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // natureType
            // 
            this.natureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.natureType.FormattingEnabled = true;
            this.natureType.Items.AddRange(new object[] {
            "Any",
            "Adamant",
            "Brave",
            "Bold",
            "Calm",
            "Careful",
            "Hasty",
            "Impish",
            "Jolly",
            "Lonely",
            "Mild",
            "Modest",
            "Naive",
            "Naughty",
            "Quiet",
            "Rash",
            "Relaxed",
            "Sassy",
            "Timid",
            "Gentle",
            "Lax",
            "Bashful",
            "Docile",
            "Hardy",
            "Quirky",
            "Serious"});
            this.natureType.Location = new System.Drawing.Point(540, 141);
            this.natureType.Name = "natureType";
            this.natureType.Size = new System.Drawing.Size(188, 21);
            this.natureType.TabIndex = 282;
            // 
            // genderType
            // 
            this.genderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderType.FormattingEnabled = true;
            this.genderType.Items.AddRange(new object[] {
            "Don\'t Care / Genderless",
            "Male (50% Male / 50% Female)",
            "Female (50% Male / 50% Female)",
            "Male (25% Male / 75% Female)",
            "Female (25% Male / 75% Female)",
            "Male (75% Male / 25% Female)",
            "Female (75% Male / 25% Female)",
            "Male (87.5% Male / 12.5% Female)",
            "Female (87.5% Male / 12.5% Female)",
            "Male (100% Male)",
            "Female (100% Female)"});
            this.genderType.Location = new System.Drawing.Point(540, 175);
            this.genderType.Name = "genderType";
            this.genderType.Size = new System.Drawing.Size(188, 21);
            this.genderType.TabIndex = 279;
            // 
            // L_sex
            // 
            this.L_sex.Location = new System.Drawing.Point(481, 175);
            this.L_sex.Name = "L_sex";
            this.L_sex.Size = new System.Drawing.Size(57, 20);
            this.L_sex.TabIndex = 281;
            this.L_sex.Text = "Gender";
            this.L_sex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_ability
            // 
            this.L_ability.Location = new System.Drawing.Point(481, 211);
            this.L_ability.Name = "L_ability";
            this.L_ability.Size = new System.Drawing.Size(57, 20);
            this.L_ability.TabIndex = 280;
            this.L_ability.Text = "Ability";
            this.L_ability.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // abilityType
            // 
            this.abilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.abilityType.FormattingEnabled = true;
            this.abilityType.Items.AddRange(new object[] {
            "Any",
            "Ability 0",
            "Ability 1"});
            this.abilityType.Location = new System.Drawing.Point(540, 210);
            this.abilityType.Name = "abilityType";
            this.abilityType.Size = new System.Drawing.Size(188, 21);
            this.abilityType.TabIndex = 278;
            // 
            // sid
            // 
            this.sid.Location = new System.Drawing.Point(630, 257);
            this.sid.Mask = "00000";
            this.sid.Name = "sid";
            this.sid.Size = new System.Drawing.Size(39, 20);
            this.sid.TabIndex = 291;
            this.sid.Text = "0";
            this.sid.ValidatingType = typeof(int);
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(540, 257);
            this.id.Mask = "00000";
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(39, 20);
            this.id.TabIndex = 290;
            this.id.Text = "0";
            this.id.ValidatingType = typeof(int);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(585, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 289;
            this.label2.Text = "SID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(514, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 288;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Shiny_Check
            // 
            this.Shiny_Check.AutoSize = true;
            this.Shiny_Check.Location = new System.Drawing.Point(528, 282);
            this.Shiny_Check.Name = "Shiny_Check";
            this.Shiny_Check.Size = new System.Drawing.Size(76, 17);
            this.Shiny_Check.TabIndex = 287;
            this.Shiny_Check.Text = "Shiny Only";
            this.Shiny_Check.UseVisualStyleBackColor = true;
            // 
            // k_dataGridView
            // 
            this.k_dataGridView.AllowUserToAddRows = false;
            this.k_dataGridView.AllowUserToResizeColumns = false;
            this.k_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.k_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.k_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.k_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.k_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seed,
            this.Frame,
            this.PID,
            this.Shiny,
            this.Type,
            this.Nature,
            this.Ability,
            this.Eighth,
            this.Quarter,
            this.Half,
            this.Three_Fourths});
            this.k_dataGridView.Location = new System.Drawing.Point(12, 385);
            this.k_dataGridView.Name = "k_dataGridView";
            this.k_dataGridView.RowTemplate.Height = 21;
            this.k_dataGridView.Size = new System.Drawing.Size(780, 187);
            this.k_dataGridView.TabIndex = 292;
            // 
            // anyPokeSpot
            // 
            this.anyPokeSpot.Location = new System.Drawing.Point(294, 142);
            this.anyPokeSpot.Name = "anyPokeSpot";
            this.anyPokeSpot.Size = new System.Drawing.Size(36, 21);
            this.anyPokeSpot.TabIndex = 295;
            this.anyPokeSpot.Text = "Any";
            this.anyPokeSpot.UseVisualStyleBackColor = true;
            this.anyPokeSpot.Click += new System.EventHandler(this.anyPokeSpot_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 294;
            this.label3.Text = "PokeSpot Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pokeSpotType
            // 
            this.pokeSpotType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pokeSpotType.FormattingEnabled = true;
            this.pokeSpotType.Items.AddRange(new object[] {
            "Any",
            "Common",
            "Uncommon",
            "Rare",
            "Munchlax Only"});
            this.pokeSpotType.Location = new System.Drawing.Point(100, 142);
            this.pokeSpotType.Name = "pokeSpotType";
            this.pokeSpotType.Size = new System.Drawing.Size(188, 21);
            this.pokeSpotType.TabIndex = 293;
            // 
            // Seed
            // 
            this.Seed.DataPropertyName = "Seed";
            this.Seed.HeaderText = "Seed";
            this.Seed.Name = "Seed";
            this.Seed.ReadOnly = true;
            this.Seed.Width = 57;
            // 
            // Frame
            // 
            this.Frame.DataPropertyName = "Frame";
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
            this.Frame.Width = 61;
            // 
            // PID
            // 
            this.PID.DataPropertyName = "PID";
            this.PID.HeaderText = "PID";
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Width = 50;
            // 
            // Shiny
            // 
            this.Shiny.DataPropertyName = "Shiny";
            this.Shiny.HeaderText = "!!!";
            this.Shiny.Name = "Shiny";
            this.Shiny.ReadOnly = true;
            this.Shiny.Width = 41;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 56;
            // 
            // Nature
            // 
            this.Nature.DataPropertyName = "Nature";
            this.Nature.HeaderText = "Nature";
            this.Nature.Name = "Nature";
            this.Nature.ReadOnly = true;
            this.Nature.Width = 64;
            // 
            // Ability
            // 
            this.Ability.DataPropertyName = "Ability";
            this.Ability.HeaderText = "Ability";
            this.Ability.Name = "Ability";
            this.Ability.ReadOnly = true;
            this.Ability.Width = 59;
            // 
            // Eighth
            // 
            this.Eighth.DataPropertyName = "Eighth";
            this.Eighth.HeaderText = "12.5%F";
            this.Eighth.Name = "Eighth";
            this.Eighth.ReadOnly = true;
            this.Eighth.Width = 67;
            // 
            // Quarter
            // 
            this.Quarter.DataPropertyName = "Quarter";
            this.Quarter.HeaderText = "25% F";
            this.Quarter.Name = "Quarter";
            this.Quarter.ReadOnly = true;
            this.Quarter.Width = 61;
            // 
            // Half
            // 
            this.Half.DataPropertyName = "Half";
            this.Half.HeaderText = "50% F";
            this.Half.Name = "Half";
            this.Half.ReadOnly = true;
            this.Half.Width = 61;
            // 
            // Three_Fourths
            // 
            this.Three_Fourths.DataPropertyName = "Three_Fourths";
            this.Three_Fourths.HeaderText = "75% F";
            this.Three_Fourths.Name = "Three_Fourths";
            this.Three_Fourths.ReadOnly = true;
            this.Three_Fourths.Width = 61;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 20);
            this.label4.TabIndex = 296;
            this.label4.Text = "Credits to amab, Zari and Kaphotics for this";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(717, 354);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 25);
            this.cancel.TabIndex = 298;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(636, 354);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 25);
            this.search.TabIndex = 297;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(9, 577);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(279, 20);
            this.status.TabIndex = 299;
            this.status.Text = "Awaiting command";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxFrame
            // 
            this.maxFrame.Location = new System.Drawing.Point(100, 256);
            this.maxFrame.Mask = "0000000000";
            this.maxFrame.Name = "maxFrame";
            this.maxFrame.Size = new System.Drawing.Size(64, 20);
            this.maxFrame.TabIndex = 301;
            this.maxFrame.Text = "3000000";
            this.maxFrame.ValidatingType = typeof(int);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 300;
            this.label5.Text = "Max Frame";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(40, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 20);
            this.label6.TabIndex = 302;
            this.label6.Text = "Seed";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Hex = true;
            this.textBoxSeed.Location = new System.Drawing.Point(100, 230);
            this.textBoxSeed.Mask = "AAAAAAAA";
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(64, 20);
            this.textBoxSeed.TabIndex = 303;
            // 
            // PokeSpot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 606);
            this.Controls.Add(this.textBoxSeed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maxFrame);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.status);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.search);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.anyPokeSpot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pokeSpotType);
            this.Controls.Add(this.k_dataGridView);
            this.Controls.Add(this.sid);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Shiny_Check);
            this.Controls.Add(this.anyAbility);
            this.Controls.Add(this.anyGender);
            this.Controls.Add(this.anyNature);
            this.Controls.Add(this.L_mezapa);
            this.Controls.Add(this.natureType);
            this.Controls.Add(this.genderType);
            this.Controls.Add(this.L_sex);
            this.Controls.Add(this.L_ability);
            this.Controls.Add(this.abilityType);
            this.MaximizeBox = false;
            this.Name = "PokeSpot";
            this.Text = "PokeSpot";
            ((System.ComponentModel.ISupportInitialize)(this.k_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button anyAbility;
        private System.Windows.Forms.Button anyGender;
        private System.Windows.Forms.Button anyNature;
        private System.Windows.Forms.Label L_mezapa;
        private System.Windows.Forms.ComboBox natureType;
        private System.Windows.Forms.ComboBox genderType;
        private System.Windows.Forms.Label L_sex;
        private System.Windows.Forms.Label L_ability;
        private System.Windows.Forms.ComboBox abilityType;
        private System.Windows.Forms.MaskedTextBox sid;
        private System.Windows.Forms.MaskedTextBox id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Shiny_Check;
        private System.Windows.Forms.DataGridView k_dataGridView;
        private System.Windows.Forms.Button anyPokeSpot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox pokeSpotType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shiny;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ability;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eighth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quarter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Half;
        private System.Windows.Forms.DataGridViewTextBoxColumn Three_Fourths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.MaskedTextBox maxFrame;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Controls.MaskedTextBox2 textBoxSeed;
    }
}