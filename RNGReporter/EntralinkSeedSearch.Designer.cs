using RNGReporter.Controls;
using RNGReporter.Objects;

namespace RNGReporter
{
    partial class EntralinkSeedSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntralinkSeedSearch));
            RNGReporter.Controls.CheckBoxProperties checkBoxProperties1 = new RNGReporter.Controls.CheckBoxProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStripCap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySeedToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.generateTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAdjacentSeedsFrame1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAdjacentSeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.outputCapResultsToTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogTxt = new System.Windows.Forms.SaveFileDialog();
            this.labelMaxFrame = new System.Windows.Forms.Label();
            this.labelMinFrame = new System.Windows.Forms.Label();
            this.textBoxChatot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.maskedTextBoxDelayCalibration = new RNGReporter.Controls.MaskedTextBox2();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxGenderless = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBoxGroupSize = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxCGearFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.comboBoxNature = new RNGReporter.Controls.CheckBoxComboBox();
            this.buttonAnyNature = new RNGReporter.GlassButton();
            this.maskedTextBoxYear = new RNGReporter.Controls.MaskedTextBox2();
            this.textBoxSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.dataGridViewCapValues = new RNGReporter.DoubleBufferedDataGridView();
            this.CapSeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timer0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSeedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapHP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapAtk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapSpA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapSpD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapSpe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keypresses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSeedGenerate = new RNGReporter.GlassButton();
            this.maskedTextBoxCapMinOffset = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxCapMaxOffset = new RNGReporter.Controls.MaskedTextBox2();
            this.groupBoxConfiguration = new System.Windows.Forms.GroupBox();
            this.labelProfileInformation = new System.Windows.Forms.Label();
            this.comboBoxProfiles = new RNGReporter.GlassComboBox();
            this.buttonEditProfile = new RNGReporter.GlassButton();
            this.contextMenuStripCap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCapValues)).BeginInit();
            this.groupBoxConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripCap
            // 
            this.contextMenuStripCap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySeedToClipboardToolStripMenuItem1,
            this.toolStripMenuItem6,
            this.generateTimesToolStripMenuItem,
            this.generateAdjacentSeedsFrame1ToolStripMenuItem,
            this.generateAdjacentSeedsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.outputCapResultsToTXTToolStripMenuItem});
            this.contextMenuStripCap.Name = "contextMenuStripCap";
            this.contextMenuStripCap.Size = new System.Drawing.Size(292, 126);
            this.contextMenuStripCap.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripCap_Opening);
            // 
            // copySeedToClipboardToolStripMenuItem1
            // 
            this.copySeedToClipboardToolStripMenuItem1.Name = "copySeedToClipboardToolStripMenuItem1";
            this.copySeedToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(291, 22);
            this.copySeedToClipboardToolStripMenuItem1.Text = "Copy Seed to Clipboard";
            this.copySeedToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.copySeedToClipboardToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(288, 6);
            // 
            // generateTimesToolStripMenuItem
            // 
            this.generateTimesToolStripMenuItem.Name = "generateTimesToolStripMenuItem";
            this.generateTimesToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.generateTimesToolStripMenuItem.Text = "Generate More Times ...";
            // 
            // generateAdjacentSeedsFrame1ToolStripMenuItem
            // 
            this.generateAdjacentSeedsFrame1ToolStripMenuItem.Name = "generateAdjacentSeedsFrame1ToolStripMenuItem";
            this.generateAdjacentSeedsFrame1ToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.generateAdjacentSeedsFrame1ToolStripMenuItem.Text = "Generate Adjacent Seeds (Frame 1)";
            this.generateAdjacentSeedsFrame1ToolStripMenuItem.Visible = false;
            // 
            // generateAdjacentSeedsToolStripMenuItem
            // 
            this.generateAdjacentSeedsToolStripMenuItem.Name = "generateAdjacentSeedsToolStripMenuItem";
            this.generateAdjacentSeedsToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.generateAdjacentSeedsToolStripMenuItem.Text = "Generate Adjacent Seeds (Current Frame)";
            this.generateAdjacentSeedsToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(288, 6);
            // 
            // outputCapResultsToTXTToolStripMenuItem
            // 
            this.outputCapResultsToTXTToolStripMenuItem.Name = "outputCapResultsToTXTToolStripMenuItem";
            this.outputCapResultsToTXTToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.outputCapResultsToTXTToolStripMenuItem.Text = "Output Results to TXT ...";
            this.outputCapResultsToTXTToolStripMenuItem.Click += new System.EventHandler(this.outputCapResultsToTXTToolStripMenuItem_Click);
            // 
            // labelMaxFrame
            // 
            this.labelMaxFrame.Location = new System.Drawing.Point(431, 94);
            this.labelMaxFrame.Name = "labelMaxFrame";
            this.labelMaxFrame.Size = new System.Drawing.Size(78, 13);
            this.labelMaxFrame.TabIndex = 107;
            this.labelMaxFrame.Text = "Max Advances";
            this.labelMaxFrame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMinFrame
            // 
            this.labelMinFrame.Location = new System.Drawing.Point(431, 68);
            this.labelMinFrame.Name = "labelMinFrame";
            this.labelMinFrame.Size = new System.Drawing.Size(78, 13);
            this.labelMinFrame.TabIndex = 125;
            this.labelMinFrame.Text = "Min Advances";
            this.labelMinFrame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxChatot
            // 
            this.textBoxChatot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChatot.Location = new System.Drawing.Point(96, 451);
            this.textBoxChatot.Name = "textBoxChatot";
            this.textBoxChatot.ReadOnly = true;
            this.textBoxChatot.Size = new System.Drawing.Size(702, 20);
            this.textBoxChatot.TabIndex = 13;
            this.toolTip1.SetToolTip(this.textBoxChatot, resources.GetString("textBoxChatot.ToolTip"));
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 171;
            this.label3.Text = "Chatot Pitches";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(335, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 333;
            this.label20.Text = "Year";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 104);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new System.Drawing.Size(256, 75);
            this.textBoxDescription.TabIndex = 175;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = resources.GetString("textBoxDescription.Text");
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 7000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // maskedTextBoxDelayCalibration
            // 
            this.maskedTextBoxDelayCalibration.Hex = false;
            this.maskedTextBoxDelayCalibration.Location = new System.Drawing.Point(665, 89);
            this.maskedTextBoxDelayCalibration.Mask = "00000";
            this.maskedTextBoxDelayCalibration.Name = "maskedTextBoxDelayCalibration";
            this.maskedTextBoxDelayCalibration.Size = new System.Drawing.Size(39, 21);
            this.maskedTextBoxDelayCalibration.TabIndex = 10;
            this.maskedTextBoxDelayCalibration.Text = "0";
            this.toolTip1.SetToolTip(this.maskedTextBoxDelayCalibration, "This is the amount to offset the time difference between the Standard Seed and th" +
        "e CGear Seed.");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 176;
            this.label12.Text = "C-Gear Seed (Hex)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(283, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 329;
            this.label13.Text = "Nature";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 332;
            this.label1.Text = "Delay";
            // 
            // checkBoxGenderless
            // 
            this.checkBoxGenderless.AutoSize = true;
            this.checkBoxGenderless.Location = new System.Drawing.Point(283, 150);
            this.checkBoxGenderless.Name = "checkBoxGenderless";
            this.checkBoxGenderless.Size = new System.Drawing.Size(113, 17);
            this.checkBoxGenderless.TabIndex = 4;
            this.checkBoxGenderless.Text = "Genderless Target";
            this.checkBoxGenderless.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 336;
            this.label2.Text = "C-Gear Seed Frame";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(580, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 338;
            this.label4.Text = "Min Cluster Size";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(580, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 339;
            this.label5.Text = "Delay Calibration";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maskedTextBoxGroupSize
            // 
            this.maskedTextBoxGroupSize.Hex = false;
            this.maskedTextBoxGroupSize.Location = new System.Drawing.Point(665, 65);
            this.maskedTextBoxGroupSize.Mask = "00000";
            this.maskedTextBoxGroupSize.Name = "maskedTextBoxGroupSize";
            this.maskedTextBoxGroupSize.Size = new System.Drawing.Size(39, 21);
            this.maskedTextBoxGroupSize.TabIndex = 9;
            this.maskedTextBoxGroupSize.Text = "3";
            // 
            // maskedTextBoxCGearFrame
            // 
            this.maskedTextBoxCGearFrame.Hex = false;
            this.maskedTextBoxCGearFrame.Location = new System.Drawing.Point(368, 65);
            this.maskedTextBoxCGearFrame.Mask = "00000";
            this.maskedTextBoxCGearFrame.Name = "maskedTextBoxCGearFrame";
            this.maskedTextBoxCGearFrame.Size = new System.Drawing.Size(41, 21);
            this.maskedTextBoxCGearFrame.TabIndex = 5;
            this.maskedTextBoxCGearFrame.Text = "1";
            // 
            // maskedTextBoxDelay
            // 
            this.maskedTextBoxDelay.Enabled = false;
            this.maskedTextBoxDelay.Hex = false;
            this.maskedTextBoxDelay.Location = new System.Drawing.Point(174, 78);
            this.maskedTextBoxDelay.Mask = "00000";
            this.maskedTextBoxDelay.Name = "maskedTextBoxDelay";
            this.maskedTextBoxDelay.Size = new System.Drawing.Size(42, 20);
            this.maskedTextBoxDelay.TabIndex = 1;
            // 
            // comboBoxNature
            // 
            this.comboBoxNature.BlankText = "Any";
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxNature.CheckBoxProperties = checkBoxProperties1;
            this.comboBoxNature.DisplayMemberSingleItem = "";
            this.comboBoxNature.DropDownHeight = 300;
            this.comboBoxNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNature.FormattingEnabled = true;
            this.comboBoxNature.Location = new System.Drawing.Point(288, 123);
            this.comboBoxNature.Name = "comboBoxNature";
            this.comboBoxNature.Size = new System.Drawing.Size(146, 21);
            this.comboBoxNature.TabIndex = 2;
            this.comboBoxNature.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FocusControl);
            // 
            // buttonAnyNature
            // 
            this.buttonAnyNature.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonAnyNature.ForeColor = System.Drawing.Color.Black;
            this.buttonAnyNature.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnyNature.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAnyNature.Location = new System.Drawing.Point(444, 122);
            this.buttonAnyNature.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAnyNature.Name = "buttonAnyNature";
            this.buttonAnyNature.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonAnyNature.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonAnyNature.Size = new System.Drawing.Size(43, 23);
            this.buttonAnyNature.TabIndex = 3;
            this.buttonAnyNature.Text = "Any";
            this.buttonAnyNature.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnyNature.Click += new System.EventHandler(this.buttonAnyNature_Click);
            // 
            // maskedTextBoxYear
            // 
            this.maskedTextBoxYear.Hex = false;
            this.maskedTextBoxYear.Location = new System.Drawing.Point(368, 90);
            this.maskedTextBoxYear.Mask = "2\\000";
            this.maskedTextBoxYear.Name = "maskedTextBoxYear";
            this.maskedTextBoxYear.Size = new System.Drawing.Size(41, 20);
            this.maskedTextBoxYear.TabIndex = 6;
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Hex = false;
            this.textBoxSeed.Location = new System.Drawing.Point(17, 78);
            this.textBoxSeed.Mask = "AAAAAAAA";
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(146, 20);
            this.textBoxSeed.TabIndex = 0;
            // 
            // dataGridViewCapValues
            // 
            this.dataGridViewCapValues.AllowUserToAddRows = false;
            this.dataGridViewCapValues.AllowUserToDeleteRows = false;
            this.dataGridViewCapValues.AllowUserToResizeRows = false;
            this.dataGridViewCapValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCapValues.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCapValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCapValues.ColumnHeadersHeight = 20;
            this.dataGridViewCapValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CapSeed,
            this.Timer0,
            this.SeedTime,
            this.CSeedTime,
            this.CapOffset,
            this.Nature,
            this.CapHP,
            this.CapAtk,
            this.CapDef,
            this.CapSpA,
            this.CapSpD,
            this.CapSpe,
            this.Grey,
            this.Keypresses});
            this.dataGridViewCapValues.ContextMenuStrip = this.contextMenuStripCap;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCapValues.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCapValues.Location = new System.Drawing.Point(12, 185);
            this.dataGridViewCapValues.MultiSelect = false;
            this.dataGridViewCapValues.Name = "dataGridViewCapValues";
            this.dataGridViewCapValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewCapValues.RowHeadersVisible = false;
            this.dataGridViewCapValues.RowTemplate.Height = 20;
            this.dataGridViewCapValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCapValues.ShowCellErrors = false;
            this.dataGridViewCapValues.ShowCellToolTips = false;
            this.dataGridViewCapValues.ShowEditingIcon = false;
            this.dataGridViewCapValues.ShowRowErrors = false;
            this.dataGridViewCapValues.Size = new System.Drawing.Size(786, 260);
            this.dataGridViewCapValues.TabIndex = 12;
            this.dataGridViewCapValues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewCapValues_CellFormatting);
            this.dataGridViewCapValues.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCapValues_ColumnHeaderMouseClick);
            this.dataGridViewCapValues.SelectionChanged += new System.EventHandler(this.dataGridViewCapValues_SelectionChanged);
            this.dataGridViewCapValues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCapValues_KeyDown);
            this.dataGridViewCapValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewCapValues_MouseDown);
            // 
            // CapSeed
            // 
            this.CapSeed.DataPropertyName = "Seed";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8.75F);
            this.CapSeed.DefaultCellStyle = dataGridViewCellStyle2;
            this.CapSeed.FillWeight = 110F;
            this.CapSeed.HeaderText = "Seed";
            this.CapSeed.Name = "CapSeed";
            this.CapSeed.ReadOnly = true;
            this.CapSeed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.CapSeed.Width = 120;
            // 
            // Timer0
            // 
            this.Timer0.DataPropertyName = "Timer0";
            this.Timer0.HeaderText = "Timer0";
            this.Timer0.Name = "Timer0";
            this.Timer0.ReadOnly = true;
            this.Timer0.Width = 50;
            // 
            // SeedTime
            // 
            this.SeedTime.DataPropertyName = "TimeDate";
            this.SeedTime.HeaderText = "Date\\Time";
            this.SeedTime.Name = "SeedTime";
            this.SeedTime.ReadOnly = true;
            // 
            // CSeedTime
            // 
            this.CSeedTime.DataPropertyName = "CSeedTime";
            this.CSeedTime.HeaderText = "C-Gear Date\\Time";
            this.CSeedTime.Name = "CSeedTime";
            // 
            // CapOffset
            // 
            this.CapOffset.DataPropertyName = "Offset";
            this.CapOffset.HeaderText = "Frame";
            this.CapOffset.Name = "CapOffset";
            this.CapOffset.ReadOnly = true;
            this.CapOffset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.CapOffset.Width = 45;
            // 
            // Nature
            // 
            this.Nature.DataPropertyName = "Nature";
            this.Nature.HeaderText = "Nature";
            this.Nature.Name = "Nature";
            this.Nature.ReadOnly = true;
            this.Nature.Width = 65;
            // 
            // CapHP
            // 
            this.CapHP.DataPropertyName = "Hp";
            this.CapHP.HeaderText = "HP";
            this.CapHP.Name = "CapHP";
            this.CapHP.ReadOnly = true;
            this.CapHP.Width = 30;
            // 
            // CapAtk
            // 
            this.CapAtk.DataPropertyName = "Atk";
            this.CapAtk.HeaderText = "Atk";
            this.CapAtk.Name = "CapAtk";
            this.CapAtk.ReadOnly = true;
            this.CapAtk.Width = 30;
            // 
            // CapDef
            // 
            this.CapDef.DataPropertyName = "Def";
            this.CapDef.HeaderText = "Def";
            this.CapDef.Name = "CapDef";
            this.CapDef.ReadOnly = true;
            this.CapDef.Width = 30;
            // 
            // CapSpA
            // 
            this.CapSpA.DataPropertyName = "SpA";
            this.CapSpA.HeaderText = "SpA";
            this.CapSpA.Name = "CapSpA";
            this.CapSpA.ReadOnly = true;
            this.CapSpA.Width = 30;
            // 
            // CapSpD
            // 
            this.CapSpD.DataPropertyName = "SpD";
            this.CapSpD.HeaderText = "SpD";
            this.CapSpD.Name = "CapSpD";
            this.CapSpD.ReadOnly = true;
            this.CapSpD.Width = 30;
            // 
            // CapSpe
            // 
            this.CapSpe.DataPropertyName = "Spe";
            this.CapSpe.HeaderText = "Spe";
            this.CapSpe.Name = "CapSpe";
            this.CapSpe.ReadOnly = true;
            this.CapSpe.Width = 30;
            // 
            // Grey
            // 
            this.Grey.DataPropertyName = "Synchable";
            this.Grey.HeaderText = "Grey";
            this.Grey.Name = "Grey";
            this.Grey.ReadOnly = true;
            this.Grey.Visible = false;
            // 
            // Keypresses
            // 
            this.Keypresses.DataPropertyName = "Keypress";
            this.Keypresses.HeaderText = "Keypresses";
            this.Keypresses.Name = "Keypresses";
            this.Keypresses.Width = 120;
            // 
            // buttonSeedGenerate
            // 
            this.buttonSeedGenerate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonSeedGenerate.ForeColor = System.Drawing.Color.Black;
            this.buttonSeedGenerate.Location = new System.Drawing.Point(716, 144);
            this.buttonSeedGenerate.Name = "buttonSeedGenerate";
            this.buttonSeedGenerate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonSeedGenerate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonSeedGenerate.Size = new System.Drawing.Size(76, 23);
            this.buttonSeedGenerate.TabIndex = 11;
            this.buttonSeedGenerate.Text = "Search";
            this.buttonSeedGenerate.Click += new System.EventHandler(this.buttonSeedGenerate_Click);
            // 
            // maskedTextBoxCapMinOffset
            // 
            this.maskedTextBoxCapMinOffset.Hex = false;
            this.maskedTextBoxCapMinOffset.Location = new System.Drawing.Point(515, 65);
            this.maskedTextBoxCapMinOffset.Mask = "00000";
            this.maskedTextBoxCapMinOffset.Name = "maskedTextBoxCapMinOffset";
            this.maskedTextBoxCapMinOffset.Size = new System.Drawing.Size(39, 21);
            this.maskedTextBoxCapMinOffset.TabIndex = 7;
            this.maskedTextBoxCapMinOffset.Text = "40";
            // 
            // maskedTextBoxCapMaxOffset
            // 
            this.maskedTextBoxCapMaxOffset.Hex = false;
            this.maskedTextBoxCapMaxOffset.Location = new System.Drawing.Point(515, 90);
            this.maskedTextBoxCapMaxOffset.Mask = "00000";
            this.maskedTextBoxCapMaxOffset.Name = "maskedTextBoxCapMaxOffset";
            this.maskedTextBoxCapMaxOffset.Size = new System.Drawing.Size(39, 21);
            this.maskedTextBoxCapMaxOffset.TabIndex = 8;
            this.maskedTextBoxCapMaxOffset.Text = "100";
            // 
            // groupBoxConfiguration
            // 
            this.groupBoxConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConfiguration.Controls.Add(this.labelProfileInformation);
            this.groupBoxConfiguration.Controls.Add(this.comboBoxProfiles);
            this.groupBoxConfiguration.Controls.Add(this.buttonEditProfile);
            this.groupBoxConfiguration.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConfiguration.Name = "groupBoxConfiguration";
            this.groupBoxConfiguration.Size = new System.Drawing.Size(786, 48);
            this.groupBoxConfiguration.TabIndex = 340;
            this.groupBoxConfiguration.TabStop = false;
            this.groupBoxConfiguration.Text = "Configuration";
            // 
            // labelProfileInformation
            // 
            this.labelProfileInformation.AutoSize = true;
            this.labelProfileInformation.Location = new System.Drawing.Point(131, 22);
            this.labelProfileInformation.Name = "labelProfileInformation";
            this.labelProfileInformation.Size = new System.Drawing.Size(0, 13);
            this.labelProfileInformation.TabIndex = 320;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.ForeColor = System.Drawing.Color.Black;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(6, 19);
            this.comboBoxProfiles.MaxDropDownItems = 3;
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxProfiles.Size = new System.Drawing.Size(119, 21);
            this.comboBoxProfiles.TabIndex = 33;
            this.comboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfiles_SelectedIndexChanged);
            // 
            // buttonEditProfile
            // 
            this.buttonEditProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditProfile.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonEditProfile.ForeColor = System.Drawing.Color.Black;
            this.buttonEditProfile.Location = new System.Drawing.Point(741, 17);
            this.buttonEditProfile.Name = "buttonEditProfile";
            this.buttonEditProfile.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonEditProfile.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonEditProfile.Size = new System.Drawing.Size(39, 23);
            this.buttonEditProfile.TabIndex = 11;
            this.buttonEditProfile.Text = "Edit";
            this.buttonEditProfile.Click += new System.EventHandler(this.buttonEditProfile_Click);
            // 
            // EntralinkSeedSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 479);
            this.Controls.Add(this.groupBoxConfiguration);
            this.Controls.Add(this.maskedTextBoxDelayCalibration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.checkBoxGenderless);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maskedTextBoxGroupSize);
            this.Controls.Add(this.maskedTextBoxCGearFrame);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.maskedTextBoxYear);
            this.Controls.Add(this.textBoxChatot);
            this.Controls.Add(this.dataGridViewCapValues);
            this.Controls.Add(this.maskedTextBoxCapMinOffset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedTextBoxDelay);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.buttonSeedGenerate);
            this.Controls.Add(this.maskedTextBoxCapMaxOffset);
            this.Controls.Add(this.comboBoxNature);
            this.Controls.Add(this.buttonAnyNature);
            this.Controls.Add(this.textBoxSeed);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.labelMaxFrame);
            this.Controls.Add(this.labelMinFrame);
            this.MinimumSize = new System.Drawing.Size(677, 378);
            this.Name = "EntralinkSeedSearch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Entralink Seed Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlatinumTime_FormClosing);
            this.Load += new System.EventHandler(this.PlatinumTime_Load);
            this.contextMenuStripCap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCapValues)).EndInit();
            this.groupBoxConfiguration.ResumeLayout(false);
            this.groupBoxConfiguration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialogTxt;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCap;
        private System.Windows.Forms.ToolStripMenuItem generateTimesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem outputCapResultsToTXTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySeedToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem generateAdjacentSeedsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateAdjacentSeedsFrame1ToolStripMenuItem;
        private RNGReporter.GlassButton buttonSeedGenerate;
        private DoubleBufferedDataGridView dataGridViewCapValues;
        private System.Windows.Forms.Label labelMaxFrame;
        private MaskedTextBox2 maskedTextBoxCapMaxOffset;
        private MaskedTextBox2 maskedTextBoxCapMinOffset;
        private System.Windows.Forms.Label labelMinFrame;
        private System.Windows.Forms.TextBox textBoxChatot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolTip toolTip1;
        private MaskedTextBox2 textBoxSeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private CheckBoxComboBox comboBoxNature;
        private RNGReporter.GlassButton buttonAnyNature;
        private MaskedTextBox2 maskedTextBoxDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label20;
        private MaskedTextBox2 maskedTextBoxYear;
        private System.Windows.Forms.CheckBox checkBoxGenderless;
        private System.Windows.Forms.Label label2;
        private MaskedTextBox2 maskedTextBoxCGearFrame;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapSeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timer0;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSeedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nature;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapHP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapAtk;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapDef;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapSpA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapSpD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapSpe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grey;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keypresses;
        private MaskedTextBox2 maskedTextBoxGroupSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private MaskedTextBox2 maskedTextBoxDelayCalibration;
        private System.Windows.Forms.GroupBox groupBoxConfiguration;
        private System.Windows.Forms.Label labelProfileInformation;
        private GlassComboBox comboBoxProfiles;
        private GlassButton buttonEditProfile;
    }
}