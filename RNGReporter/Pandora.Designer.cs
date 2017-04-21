using RNGReporter.Controls;

namespace RNGReporter
{
    partial class Pandora : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if ((disposing && components != null))
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpShiny = new System.Windows.Forms.GroupBox();
            this.cbxShinyInf = new System.Windows.Forms.CheckBox();
            this.textBoxShinyTID = new RNGReporter.Controls.MaskedTextBox2();
            this.lblShinyTrainerID = new System.Windows.Forms.Label();
            this.cbxSearchID = new System.Windows.Forms.CheckBox();
            this.btnShinyCancel = new RNGReporter.GlassButton();
            this.btnShinyGo = new RNGReporter.GlassButton();
            this.txtShinyMaxDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.lblShinyMaxDelay = new System.Windows.Forms.Label();
            this.lblShinyMinDelay = new System.Windows.Forms.Label();
            this.txtShinyMinDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.textBoxShinyYear = new RNGReporter.Controls.MaskedTextBox2();
            this.lblShinyYr = new System.Windows.Forms.Label();
            this.lblShinyPID = new System.Windows.Forms.Label();
            this.textBoxShinyPID = new RNGReporter.Controls.MaskedTextBox2();
            this.grpID = new System.Windows.Forms.GroupBox();
            this.cbxIDInf = new System.Windows.Forms.CheckBox();
            this.btnIDCancel = new RNGReporter.GlassButton();
            this.btnIDGo = new RNGReporter.GlassButton();
            this.textBoxIDMaxDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.textBoxIDMinDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.lblIDMaxDelay = new System.Windows.Forms.Label();
            this.lblIDMinDelay = new System.Windows.Forms.Label();
            this.textBoxIDYear = new RNGReporter.Controls.MaskedTextBox2();
            this.lblIDYr = new System.Windows.Forms.Label();
            this.textBoxDesiredSID = new RNGReporter.Controls.MaskedTextBox2();
            this.lblSecretID = new System.Windows.Forms.Label();
            this.cbxSearchSID = new System.Windows.Forms.CheckBox();
            this.textBoxDesiredTID = new RNGReporter.Controls.MaskedTextBox2();
            this.lblTrainerID = new System.Windows.Forms.Label();
            this.grpSeed = new System.Windows.Forms.GroupBox();
            this.lblSeedMaxDelay = new System.Windows.Forms.Label();
            this.lblSeedMinDelay = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblSeedYr = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.txtSeedMaxDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.btnSeedGo = new RNGReporter.GlassButton();
            this.txtSeedMinDelay = new RNGReporter.Controls.MaskedTextBox2();
            this.txtMinute = new RNGReporter.Controls.MaskedTextBox2();
            this.txtHour = new RNGReporter.Controls.MaskedTextBox2();
            this.txtSeedYr = new RNGReporter.Controls.MaskedTextBox2();
            this.txtDay = new RNGReporter.Controls.MaskedTextBox2();
            this.lblMonth = new System.Windows.Forms.Label();
            this.txtMonth = new RNGReporter.Controls.MaskedTextBox2();
            this.txtIDObtained = new RNGReporter.Controls.MaskedTextBox2();
            this.lblIDObtained = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.lblAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwShiny = new System.ComponentModel.BackgroundWorker();
            this.bgwID = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySeedToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.generateTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwIDInf = new System.ComponentModel.BackgroundWorker();
            this.lblSimple = new System.Windows.Forms.Label();
            this.lblSeed = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSimpleGo = new RNGReporter.GlassButton();
            this.textBoxSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.bgwShinyInf = new System.ComponentModel.BackgroundWorker();
            this.tabGenSelect = new System.Windows.Forms.TabControl();
            this.tabXDColo = new System.Windows.Forms.TabPage();
            this.labelXDColo = new System.Windows.Forms.Label();
            this.genCancelXDColo = new RNGReporter.GlassButton();
            this.searchGenXDColo = new RNGReporter.GlassButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.XDColoMaxFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label16 = new System.Windows.Forms.Label();
            this.XDColoMinFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.XDColoPID = new RNGReporter.Controls.MaskedTextBox2();
            this.XDColoPRNG = new RNGReporter.Controls.MaskedTextBox2();
            this.tabGen3FRLGE = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.genCancelFRLGE = new RNGReporter.GlassButton();
            this.genSearchFRLGE = new RNGReporter.GlassButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.genFRLGEMaxFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label12 = new System.Windows.Forms.Label();
            this.genFRLGEMinFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.genFRLGEPID = new RNGReporter.Controls.MaskedTextBox2();
            this.genFRLGETID = new RNGReporter.Controls.MaskedTextBox2();
            this.tabGen3RS = new System.Windows.Forms.TabPage();
            this.buttonIIICancel = new RNGReporter.GlassButton();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkIIIClock = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textIIIMaxFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textIIIMinFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.label7 = new System.Windows.Forms.Label();
            this.textIIIMinute = new RNGReporter.Controls.MaskedTextBox2();
            this.dateIII = new System.Windows.Forms.DateTimePicker();
            this.textIIIHour = new RNGReporter.Controls.MaskedTextBox2();
            this.buttonIIIFindFrames = new RNGReporter.GlassButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textIIISID = new RNGReporter.Controls.MaskedTextBox2();
            this.checkIIISID = new System.Windows.Forms.CheckBox();
            this.checkIIIPID = new System.Windows.Forms.CheckBox();
            this.textIIIPID = new RNGReporter.Controls.MaskedTextBox2();
            this.textIIITID = new RNGReporter.Controls.MaskedTextBox2();
            this.checkIIITID = new System.Windows.Forms.CheckBox();
            this.tabGen4 = new System.Windows.Forms.TabPage();
            this.labelBy = new System.Windows.Forms.Label();
            this.btnCredits = new RNGReporter.GlassButton();
            this.tabGen5 = new System.Windows.Forms.TabPage();
            this.buttonVFindSeedHit = new RNGReporter.GlassButton();
            this.groupVSeedFinder = new System.Windows.Forms.GroupBox();
            this.textVMinute = new RNGReporter.Controls.MaskedTextBox2();
            this.textVHour = new RNGReporter.Controls.MaskedTextBox2();
            this.labelTIDReceived = new System.Windows.Forms.Label();
            this.labelVMinute = new System.Windows.Forms.Label();
            this.textVTIDReceived = new RNGReporter.Controls.MaskedTextBox2();
            this.textVMaxSec = new RNGReporter.Controls.MaskedTextBox2();
            this.labelVHour = new System.Windows.Forms.Label();
            this.textVMinSec = new RNGReporter.Controls.MaskedTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.textVMaxFrameHit = new RNGReporter.Controls.MaskedTextBox2();
            this.dateTimeSeedSearch = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.textVMinFrameHit = new RNGReporter.Controls.MaskedTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.groupVDSParams = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProfileInformation = new System.Windows.Forms.Label();
            this.comboBoxProfiles = new RNGReporter.GlassComboBox();
            this.buttonEditProfile = new RNGReporter.GlassButton();
            this.groupVSearchParams = new System.Windows.Forms.GroupBox();
            this.checkBoxSaveExists = new System.Windows.Forms.CheckBox();
            this.checkBoxMinFrameCalc = new System.Windows.Forms.CheckBox();
            this.textVMaxFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.labelVMaxFrame = new System.Windows.Forms.Label();
            this.textVMinFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.labelVMinFrame = new System.Windows.Forms.Label();
            this.checkVMonth = new System.Windows.Forms.CheckBox();
            this.dateTimeSearch = new System.Windows.Forms.DateTimePicker();
            this.labelVDate = new System.Windows.Forms.Label();
            this.groupVPID = new System.Windows.Forms.GroupBox();
            this.textVSID = new RNGReporter.Controls.MaskedTextBox2();
            this.checkVSID = new System.Windows.Forms.CheckBox();
            this.checkVPID = new System.Windows.Forms.CheckBox();
            this.textVPID = new RNGReporter.Controls.MaskedTextBox2();
            this.labelVFrame = new System.Windows.Forms.Label();
            this.textVFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.textVTID = new RNGReporter.Controls.MaskedTextBox2();
            this.checkVSeed = new System.Windows.Forms.CheckBox();
            this.textVSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.checkVTID = new System.Windows.Forms.CheckBox();
            this.buttonVFindSeeds = new RNGReporter.GlassButton();
            this.buttonVCancel = new RNGReporter.GlassButton();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.dgvResults = new RNGReporter.DoubleBufferedDataGridView();
            this.clmSeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFrame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInitialFrame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSeconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStarter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmButton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpShiny.SuspendLayout();
            this.grpID.SuspendLayout();
            this.grpSeed.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.tabGenSelect.SuspendLayout();
            this.tabXDColo.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabGen3FRLGE.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabGen3RS.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabGen4.SuspendLayout();
            this.tabGen5.SuspendLayout();
            this.groupVSeedFinder.SuspendLayout();
            this.groupVDSParams.SuspendLayout();
            this.groupVSearchParams.SuspendLayout();
            this.groupVPID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // grpShiny
            // 
            this.grpShiny.Controls.Add(this.cbxShinyInf);
            this.grpShiny.Controls.Add(this.textBoxShinyTID);
            this.grpShiny.Controls.Add(this.lblShinyTrainerID);
            this.grpShiny.Controls.Add(this.cbxSearchID);
            this.grpShiny.Controls.Add(this.btnShinyCancel);
            this.grpShiny.Controls.Add(this.btnShinyGo);
            this.grpShiny.Controls.Add(this.txtShinyMaxDelay);
            this.grpShiny.Controls.Add(this.lblShinyMaxDelay);
            this.grpShiny.Controls.Add(this.lblShinyMinDelay);
            this.grpShiny.Controls.Add(this.txtShinyMinDelay);
            this.grpShiny.Controls.Add(this.textBoxShinyYear);
            this.grpShiny.Controls.Add(this.lblShinyYr);
            this.grpShiny.Controls.Add(this.lblShinyPID);
            this.grpShiny.Controls.Add(this.textBoxShinyPID);
            this.grpShiny.Location = new System.Drawing.Point(6, 41);
            this.grpShiny.Name = "grpShiny";
            this.grpShiny.Size = new System.Drawing.Size(183, 310);
            this.grpShiny.TabIndex = 1;
            this.grpShiny.TabStop = false;
            this.grpShiny.Text = "Shiny PID";
            // 
            // cbxShinyInf
            // 
            this.cbxShinyInf.AutoSize = true;
            this.cbxShinyInf.Location = new System.Drawing.Point(9, 184);
            this.cbxShinyInf.Name = "cbxShinyInf";
            this.cbxShinyInf.Size = new System.Drawing.Size(94, 17);
            this.cbxShinyInf.TabIndex = 7;
            this.cbxShinyInf.Text = "Infinite Search";
            this.cbxShinyInf.UseVisualStyleBackColor = true;
            this.cbxShinyInf.CheckedChanged += new System.EventHandler(this.cbxShinyInf_CheckedChanged);
            // 
            // textBoxShinyTID
            // 
            this.textBoxShinyTID.Enabled = false;
            this.textBoxShinyTID.Hex = false;
            this.textBoxShinyTID.Location = new System.Drawing.Point(104, 77);
            this.textBoxShinyTID.Mask = "00000";
            this.textBoxShinyTID.Name = "textBoxShinyTID";
            this.textBoxShinyTID.Size = new System.Drawing.Size(37, 20);
            this.textBoxShinyTID.TabIndex = 3;
            // 
            // lblShinyTrainerID
            // 
            this.lblShinyTrainerID.AutoSize = true;
            this.lblShinyTrainerID.Enabled = false;
            this.lblShinyTrainerID.Location = new System.Drawing.Point(6, 80);
            this.lblShinyTrainerID.Name = "lblShinyTrainerID";
            this.lblShinyTrainerID.Size = new System.Drawing.Size(96, 13);
            this.lblShinyTrainerID.TabIndex = 10;
            this.lblShinyTrainerID.Text = "Desired Trainer ID:";
            this.lblShinyTrainerID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxSearchID
            // 
            this.cbxSearchID.AutoSize = true;
            this.cbxSearchID.Location = new System.Drawing.Point(9, 54);
            this.cbxSearchID.Name = "cbxSearchID";
            this.cbxSearchID.Size = new System.Drawing.Size(125, 17);
            this.cbxSearchID.TabIndex = 2;
            this.cbxSearchID.Text = "Search for Trainer ID";
            this.cbxSearchID.UseVisualStyleBackColor = true;
            this.cbxSearchID.CheckedChanged += new System.EventHandler(this.cbxSearchID_CheckedChanged);
            // 
            // btnShinyCancel
            // 
            this.btnShinyCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnShinyCancel.Enabled = false;
            this.btnShinyCancel.ForeColor = System.Drawing.Color.Black;
            this.btnShinyCancel.Location = new System.Drawing.Point(11, 269);
            this.btnShinyCancel.Name = "btnShinyCancel";
            this.btnShinyCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnShinyCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.btnShinyCancel.Size = new System.Drawing.Size(159, 29);
            this.btnShinyCancel.TabIndex = 9;
            this.btnShinyCancel.Text = "Cancel";
            this.btnShinyCancel.Click += new System.EventHandler(this.btnShinyCancel_Click);
            // 
            // btnShinyGo
            // 
            this.btnShinyGo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnShinyGo.ForeColor = System.Drawing.Color.Black;
            this.btnShinyGo.Location = new System.Drawing.Point(10, 234);
            this.btnShinyGo.Name = "btnShinyGo";
            this.btnShinyGo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnShinyGo.ShineColor = System.Drawing.SystemColors.Window;
            this.btnShinyGo.Size = new System.Drawing.Size(159, 29);
            this.btnShinyGo.TabIndex = 8;
            this.btnShinyGo.Text = "Find Compatible Seeds";
            this.btnShinyGo.Click += new System.EventHandler(this.btnShinyGo_Click);
            // 
            // txtShinyMaxDelay
            // 
            this.txtShinyMaxDelay.Hex = false;
            this.txtShinyMaxDelay.Location = new System.Drawing.Point(104, 156);
            this.txtShinyMaxDelay.Mask = "00000000";
            this.txtShinyMaxDelay.Name = "txtShinyMaxDelay";
            this.txtShinyMaxDelay.Size = new System.Drawing.Size(65, 20);
            this.txtShinyMaxDelay.TabIndex = 6;
            // 
            // lblShinyMaxDelay
            // 
            this.lblShinyMaxDelay.AutoSize = true;
            this.lblShinyMaxDelay.Location = new System.Drawing.Point(14, 159);
            this.lblShinyMaxDelay.Name = "lblShinyMaxDelay";
            this.lblShinyMaxDelay.Size = new System.Drawing.Size(84, 13);
            this.lblShinyMaxDelay.TabIndex = 6;
            this.lblShinyMaxDelay.Text = "Maximum Delay:";
            this.lblShinyMaxDelay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblShinyMinDelay
            // 
            this.lblShinyMinDelay.AutoSize = true;
            this.lblShinyMinDelay.Location = new System.Drawing.Point(20, 132);
            this.lblShinyMinDelay.Name = "lblShinyMinDelay";
            this.lblShinyMinDelay.Size = new System.Drawing.Size(81, 13);
            this.lblShinyMinDelay.TabIndex = 5;
            this.lblShinyMinDelay.Text = "Minimum Delay:";
            this.lblShinyMinDelay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtShinyMinDelay
            // 
            this.txtShinyMinDelay.Hex = false;
            this.txtShinyMinDelay.Location = new System.Drawing.Point(104, 129);
            this.txtShinyMinDelay.Mask = "00000000";
            this.txtShinyMinDelay.Name = "txtShinyMinDelay";
            this.txtShinyMinDelay.Size = new System.Drawing.Size(65, 20);
            this.txtShinyMinDelay.TabIndex = 5;
            // 
            // textBoxShinyYear
            // 
            this.textBoxShinyYear.Hex = false;
            this.textBoxShinyYear.Location = new System.Drawing.Point(104, 103);
            this.textBoxShinyYear.Mask = "2\\000";
            this.textBoxShinyYear.Name = "textBoxShinyYear";
            this.textBoxShinyYear.Size = new System.Drawing.Size(35, 20);
            this.textBoxShinyYear.TabIndex = 4;
            // 
            // lblShinyYr
            // 
            this.lblShinyYr.AutoSize = true;
            this.lblShinyYr.Location = new System.Drawing.Point(69, 106);
            this.lblShinyYr.Name = "lblShinyYr";
            this.lblShinyYr.Size = new System.Drawing.Size(32, 13);
            this.lblShinyYr.TabIndex = 2;
            this.lblShinyYr.Text = "Year:";
            this.lblShinyYr.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblShinyPID
            // 
            this.lblShinyPID.AutoSize = true;
            this.lblShinyPID.Location = new System.Drawing.Point(6, 28);
            this.lblShinyPID.Name = "lblShinyPID";
            this.lblShinyPID.Size = new System.Drawing.Size(95, 13);
            this.lblShinyPID.TabIndex = 1;
            this.lblShinyPID.Text = "Desired PID (Hex):";
            this.lblShinyPID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxShinyPID
            // 
            this.textBoxShinyPID.Hex = true;
            this.textBoxShinyPID.Location = new System.Drawing.Point(104, 25);
            this.textBoxShinyPID.Mask = "AAAAAAAA";
            this.textBoxShinyPID.Name = "textBoxShinyPID";
            this.textBoxShinyPID.Size = new System.Drawing.Size(65, 20);
            this.textBoxShinyPID.TabIndex = 1;
            // 
            // grpID
            // 
            this.grpID.Controls.Add(this.cbxIDInf);
            this.grpID.Controls.Add(this.btnIDCancel);
            this.grpID.Controls.Add(this.btnIDGo);
            this.grpID.Controls.Add(this.textBoxIDMaxDelay);
            this.grpID.Controls.Add(this.textBoxIDMinDelay);
            this.grpID.Controls.Add(this.lblIDMaxDelay);
            this.grpID.Controls.Add(this.lblIDMinDelay);
            this.grpID.Controls.Add(this.textBoxIDYear);
            this.grpID.Controls.Add(this.lblIDYr);
            this.grpID.Controls.Add(this.textBoxDesiredSID);
            this.grpID.Controls.Add(this.lblSecretID);
            this.grpID.Controls.Add(this.cbxSearchSID);
            this.grpID.Controls.Add(this.textBoxDesiredTID);
            this.grpID.Controls.Add(this.lblTrainerID);
            this.grpID.Location = new System.Drawing.Point(195, 41);
            this.grpID.Name = "grpID";
            this.grpID.Size = new System.Drawing.Size(183, 310);
            this.grpID.TabIndex = 2;
            this.grpID.TabStop = false;
            this.grpID.Text = "Trainer ID";
            // 
            // cbxIDInf
            // 
            this.cbxIDInf.AutoSize = true;
            this.cbxIDInf.Location = new System.Drawing.Point(9, 182);
            this.cbxIDInf.Name = "cbxIDInf";
            this.cbxIDInf.Size = new System.Drawing.Size(94, 17);
            this.cbxIDInf.TabIndex = 16;
            this.cbxIDInf.Text = "Infinite Search";
            this.cbxIDInf.UseVisualStyleBackColor = true;
            this.cbxIDInf.CheckedChanged += new System.EventHandler(this.cbxIDInf_CheckedChanged);
            // 
            // btnIDCancel
            // 
            this.btnIDCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnIDCancel.Enabled = false;
            this.btnIDCancel.ForeColor = System.Drawing.Color.Black;
            this.btnIDCancel.Location = new System.Drawing.Point(9, 269);
            this.btnIDCancel.Name = "btnIDCancel";
            this.btnIDCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnIDCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.btnIDCancel.Size = new System.Drawing.Size(159, 29);
            this.btnIDCancel.TabIndex = 18;
            this.btnIDCancel.Text = "Cancel";
            this.btnIDCancel.Click += new System.EventHandler(this.btnIDCancel_Click);
            // 
            // btnIDGo
            // 
            this.btnIDGo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnIDGo.ForeColor = System.Drawing.Color.Black;
            this.btnIDGo.Location = new System.Drawing.Point(9, 234);
            this.btnIDGo.Name = "btnIDGo";
            this.btnIDGo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnIDGo.ShineColor = System.Drawing.SystemColors.Window;
            this.btnIDGo.Size = new System.Drawing.Size(159, 29);
            this.btnIDGo.TabIndex = 17;
            this.btnIDGo.Text = "Find Compatible Seeds";
            this.btnIDGo.Click += new System.EventHandler(this.btnIDGo_Click);
            // 
            // textBoxIDMaxDelay
            // 
            this.textBoxIDMaxDelay.Hex = false;
            this.textBoxIDMaxDelay.Location = new System.Drawing.Point(106, 156);
            this.textBoxIDMaxDelay.Mask = "00000000";
            this.textBoxIDMaxDelay.Name = "textBoxIDMaxDelay";
            this.textBoxIDMaxDelay.Size = new System.Drawing.Size(65, 20);
            this.textBoxIDMaxDelay.TabIndex = 15;
            // 
            // textBoxIDMinDelay
            // 
            this.textBoxIDMinDelay.Hex = false;
            this.textBoxIDMinDelay.Location = new System.Drawing.Point(106, 129);
            this.textBoxIDMinDelay.Mask = "00000000";
            this.textBoxIDMinDelay.Name = "textBoxIDMinDelay";
            this.textBoxIDMinDelay.Size = new System.Drawing.Size(65, 20);
            this.textBoxIDMinDelay.TabIndex = 14;
            // 
            // lblIDMaxDelay
            // 
            this.lblIDMaxDelay.AutoSize = true;
            this.lblIDMaxDelay.Location = new System.Drawing.Point(16, 159);
            this.lblIDMaxDelay.Name = "lblIDMaxDelay";
            this.lblIDMaxDelay.Size = new System.Drawing.Size(84, 13);
            this.lblIDMaxDelay.TabIndex = 13;
            this.lblIDMaxDelay.Text = "Maximum Delay:";
            this.lblIDMaxDelay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIDMinDelay
            // 
            this.lblIDMinDelay.AutoSize = true;
            this.lblIDMinDelay.Location = new System.Drawing.Point(19, 132);
            this.lblIDMinDelay.Name = "lblIDMinDelay";
            this.lblIDMinDelay.Size = new System.Drawing.Size(81, 13);
            this.lblIDMinDelay.TabIndex = 12;
            this.lblIDMinDelay.Text = "Minimum Delay:";
            this.lblIDMinDelay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxIDYear
            // 
            this.textBoxIDYear.Hex = false;
            this.textBoxIDYear.Location = new System.Drawing.Point(106, 103);
            this.textBoxIDYear.Mask = "2\\000";
            this.textBoxIDYear.Name = "textBoxIDYear";
            this.textBoxIDYear.Size = new System.Drawing.Size(49, 20);
            this.textBoxIDYear.TabIndex = 13;
            // 
            // lblIDYr
            // 
            this.lblIDYr.AutoSize = true;
            this.lblIDYr.Location = new System.Drawing.Point(68, 106);
            this.lblIDYr.Name = "lblIDYr";
            this.lblIDYr.Size = new System.Drawing.Size(32, 13);
            this.lblIDYr.TabIndex = 10;
            this.lblIDYr.Text = "Year:";
            this.lblIDYr.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxDesiredSID
            // 
            this.textBoxDesiredSID.Enabled = false;
            this.textBoxDesiredSID.Hex = false;
            this.textBoxDesiredSID.Location = new System.Drawing.Point(106, 77);
            this.textBoxDesiredSID.Mask = "00000";
            this.textBoxDesiredSID.Name = "textBoxDesiredSID";
            this.textBoxDesiredSID.Size = new System.Drawing.Size(49, 20);
            this.textBoxDesiredSID.TabIndex = 12;
            // 
            // lblSecretID
            // 
            this.lblSecretID.AutoSize = true;
            this.lblSecretID.Enabled = false;
            this.lblSecretID.Location = new System.Drawing.Point(6, 80);
            this.lblSecretID.Name = "lblSecretID";
            this.lblSecretID.Size = new System.Drawing.Size(94, 13);
            this.lblSecretID.TabIndex = 6;
            this.lblSecretID.Text = "Desired Secret ID:";
            this.lblSecretID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbxSearchSID
            // 
            this.cbxSearchSID.AutoSize = true;
            this.cbxSearchSID.Location = new System.Drawing.Point(9, 53);
            this.cbxSearchSID.Name = "cbxSearchSID";
            this.cbxSearchSID.Size = new System.Drawing.Size(96, 17);
            this.cbxSearchSID.TabIndex = 11;
            this.cbxSearchSID.Text = "Search for SID";
            this.cbxSearchSID.UseVisualStyleBackColor = true;
            this.cbxSearchSID.CheckedChanged += new System.EventHandler(this.cbxSearchSID_CheckedChanged);
            // 
            // textBoxDesiredTID
            // 
            this.textBoxDesiredTID.Hex = false;
            this.textBoxDesiredTID.Location = new System.Drawing.Point(108, 25);
            this.textBoxDesiredTID.Mask = "00000";
            this.textBoxDesiredTID.Name = "textBoxDesiredTID";
            this.textBoxDesiredTID.Size = new System.Drawing.Size(47, 20);
            this.textBoxDesiredTID.TabIndex = 10;
            // 
            // lblTrainerID
            // 
            this.lblTrainerID.AutoSize = true;
            this.lblTrainerID.Location = new System.Drawing.Point(6, 28);
            this.lblTrainerID.Name = "lblTrainerID";
            this.lblTrainerID.Size = new System.Drawing.Size(96, 13);
            this.lblTrainerID.TabIndex = 2;
            this.lblTrainerID.Text = "Desired Trainer ID:";
            this.lblTrainerID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpSeed
            // 
            this.grpSeed.Controls.Add(this.lblSeedMaxDelay);
            this.grpSeed.Controls.Add(this.lblSeedMinDelay);
            this.grpSeed.Controls.Add(this.lblMinute);
            this.grpSeed.Controls.Add(this.lblHour);
            this.grpSeed.Controls.Add(this.lblSeedYr);
            this.grpSeed.Controls.Add(this.lblDay);
            this.grpSeed.Controls.Add(this.txtSeedMaxDelay);
            this.grpSeed.Controls.Add(this.btnSeedGo);
            this.grpSeed.Controls.Add(this.txtSeedMinDelay);
            this.grpSeed.Controls.Add(this.txtMinute);
            this.grpSeed.Controls.Add(this.txtHour);
            this.grpSeed.Controls.Add(this.txtSeedYr);
            this.grpSeed.Controls.Add(this.txtDay);
            this.grpSeed.Controls.Add(this.lblMonth);
            this.grpSeed.Controls.Add(this.txtMonth);
            this.grpSeed.Controls.Add(this.txtIDObtained);
            this.grpSeed.Controls.Add(this.lblIDObtained);
            this.grpSeed.Location = new System.Drawing.Point(389, 41);
            this.grpSeed.Name = "grpSeed";
            this.grpSeed.Size = new System.Drawing.Size(183, 310);
            this.grpSeed.TabIndex = 3;
            this.grpSeed.TabStop = false;
            this.grpSeed.Text = "Seed Finder";
            // 
            // lblSeedMaxDelay
            // 
            this.lblSeedMaxDelay.AutoSize = true;
            this.lblSeedMaxDelay.Location = new System.Drawing.Point(24, 211);
            this.lblSeedMaxDelay.Name = "lblSeedMaxDelay";
            this.lblSeedMaxDelay.Size = new System.Drawing.Size(84, 13);
            this.lblSeedMaxDelay.TabIndex = 30;
            this.lblSeedMaxDelay.Text = "Maximum Delay:";
            // 
            // lblSeedMinDelay
            // 
            this.lblSeedMinDelay.AutoSize = true;
            this.lblSeedMinDelay.Location = new System.Drawing.Point(25, 185);
            this.lblSeedMinDelay.Name = "lblSeedMinDelay";
            this.lblSeedMinDelay.Size = new System.Drawing.Size(81, 13);
            this.lblSeedMinDelay.TabIndex = 29;
            this.lblSeedMinDelay.Text = "Minimum Delay:";
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(64, 159);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(42, 13);
            this.lblMinute.TabIndex = 28;
            this.lblMinute.Text = "Minute:";
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(73, 132);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(33, 13);
            this.lblHour.TabIndex = 27;
            this.lblHour.Text = "Hour:";
            // 
            // lblSeedYr
            // 
            this.lblSeedYr.AutoSize = true;
            this.lblSeedYr.Location = new System.Drawing.Point(74, 106);
            this.lblSeedYr.Name = "lblSeedYr";
            this.lblSeedYr.Size = new System.Drawing.Size(32, 13);
            this.lblSeedYr.TabIndex = 26;
            this.lblSeedYr.Text = "Year:";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Location = new System.Drawing.Point(77, 80);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(29, 13);
            this.lblDay.TabIndex = 25;
            this.lblDay.Text = "Day:";
            // 
            // txtSeedMaxDelay
            // 
            this.txtSeedMaxDelay.Hex = false;
            this.txtSeedMaxDelay.Location = new System.Drawing.Point(112, 208);
            this.txtSeedMaxDelay.Mask = "00000000";
            this.txtSeedMaxDelay.Name = "txtSeedMaxDelay";
            this.txtSeedMaxDelay.Size = new System.Drawing.Size(65, 20);
            this.txtSeedMaxDelay.TabIndex = 26;
            // 
            // btnSeedGo
            // 
            this.btnSeedGo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnSeedGo.ForeColor = System.Drawing.Color.Black;
            this.btnSeedGo.Location = new System.Drawing.Point(12, 234);
            this.btnSeedGo.Name = "btnSeedGo";
            this.btnSeedGo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnSeedGo.ShineColor = System.Drawing.SystemColors.Window;
            this.btnSeedGo.Size = new System.Drawing.Size(159, 29);
            this.btnSeedGo.TabIndex = 27;
            this.btnSeedGo.Text = "Find Compatible Seeds";
            this.btnSeedGo.Click += new System.EventHandler(this.btnSeedGo_Click);
            // 
            // txtSeedMinDelay
            // 
            this.txtSeedMinDelay.Hex = false;
            this.txtSeedMinDelay.Location = new System.Drawing.Point(112, 182);
            this.txtSeedMinDelay.Mask = "00000000";
            this.txtSeedMinDelay.Name = "txtSeedMinDelay";
            this.txtSeedMinDelay.Size = new System.Drawing.Size(65, 20);
            this.txtSeedMinDelay.TabIndex = 25;
            // 
            // txtMinute
            // 
            this.txtMinute.Hex = false;
            this.txtMinute.Location = new System.Drawing.Point(112, 156);
            this.txtMinute.Mask = "00";
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(24, 20);
            this.txtMinute.TabIndex = 24;
            // 
            // txtHour
            // 
            this.txtHour.Hex = false;
            this.txtHour.Location = new System.Drawing.Point(112, 129);
            this.txtHour.Mask = "00";
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(24, 20);
            this.txtHour.TabIndex = 23;
            // 
            // txtSeedYr
            // 
            this.txtSeedYr.Hex = false;
            this.txtSeedYr.Location = new System.Drawing.Point(112, 103);
            this.txtSeedYr.Mask = "2\\000";
            this.txtSeedYr.Name = "txtSeedYr";
            this.txtSeedYr.Size = new System.Drawing.Size(35, 20);
            this.txtSeedYr.TabIndex = 22;
            // 
            // txtDay
            // 
            this.txtDay.Hex = false;
            this.txtDay.Location = new System.Drawing.Point(112, 77);
            this.txtDay.Mask = "00";
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(24, 20);
            this.txtDay.TabIndex = 21;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(66, 54);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(40, 13);
            this.lblMonth.TabIndex = 17;
            this.lblMonth.Text = "Month:";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMonth
            // 
            this.txtMonth.Hex = false;
            this.txtMonth.Location = new System.Drawing.Point(112, 50);
            this.txtMonth.Mask = "00";
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(24, 20);
            this.txtMonth.TabIndex = 20;
            // 
            // txtIDObtained
            // 
            this.txtIDObtained.Hex = false;
            this.txtIDObtained.Location = new System.Drawing.Point(112, 25);
            this.txtIDObtained.Mask = "00000";
            this.txtIDObtained.Name = "txtIDObtained";
            this.txtIDObtained.Size = new System.Drawing.Size(49, 20);
            this.txtIDObtained.TabIndex = 19;
            // 
            // lblIDObtained
            // 
            this.lblIDObtained.AutoSize = true;
            this.lblIDObtained.Location = new System.Drawing.Point(3, 28);
            this.lblIDObtained.Name = "lblIDObtained";
            this.lblIDObtained.Size = new System.Drawing.Size(103, 13);
            this.lblIDObtained.TabIndex = 0;
            this.lblIDObtained.Text = "Trainer ID Obtained:";
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAction});
            this.StatusBar.Location = new System.Drawing.Point(0, 577);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(618, 22);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 5;
            // 
            // lblAction
            // 
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(114, 17);
            this.lblAction.Text = "Awaiting Command";
            // 
            // bgwShiny
            // 
            this.bgwShiny.WorkerReportsProgress = true;
            this.bgwShiny.WorkerSupportsCancellation = true;
            this.bgwShiny.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwShiny_DoWork);
            this.bgwShiny.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwShiny_RunWorkerCompleted);
            // 
            // bgwID
            // 
            this.bgwID.WorkerReportsProgress = true;
            this.bgwID.WorkerSupportsCancellation = true;
            this.bgwID.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwID_DoWork);
            this.bgwID.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwID_RunWorkerCompleted);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySeedToClipboardToolStripMenuItem1,
            this.toolStripMenuItem6,
            this.generateTimesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStripCap";
            this.contextMenuStrip.Size = new System.Drawing.Size(200, 54);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // copySeedToClipboardToolStripMenuItem1
            // 
            this.copySeedToClipboardToolStripMenuItem1.Name = "copySeedToClipboardToolStripMenuItem1";
            this.copySeedToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(199, 22);
            this.copySeedToClipboardToolStripMenuItem1.Text = "Copy Seed to Clipboard";
            this.copySeedToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.copySeedToClipboardToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(196, 6);
            // 
            // generateTimesToolStripMenuItem
            // 
            this.generateTimesToolStripMenuItem.Name = "generateTimesToolStripMenuItem";
            this.generateTimesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.generateTimesToolStripMenuItem.Text = "Generate More Times ...";
            this.generateTimesToolStripMenuItem.Click += new System.EventHandler(this.generateTimesToolStripMenuItem_Click);
            // 
            // bgwIDInf
            // 
            this.bgwIDInf.WorkerReportsProgress = true;
            this.bgwIDInf.WorkerSupportsCancellation = true;
            this.bgwIDInf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwIDInf_DoWork);
            this.bgwIDInf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwIDInf_RunWorkerCompleted);
            // 
            // lblSimple
            // 
            this.lblSimple.AutoSize = true;
            this.lblSimple.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(213)))));
            this.lblSimple.Location = new System.Drawing.Point(6, 13);
            this.lblSimple.Name = "lblSimple";
            this.lblSimple.Size = new System.Drawing.Size(165, 13);
            this.lblSimple.TabIndex = 26;
            this.lblSimple.Text = "Simple Seed to ID/SID Calculator";
            // 
            // lblSeed
            // 
            this.lblSeed.AutoSize = true;
            this.lblSeed.Location = new System.Drawing.Point(191, 13);
            this.lblSeed.Name = "lblSeed";
            this.lblSeed.Size = new System.Drawing.Size(63, 13);
            this.lblSeed.TabIndex = 27;
            this.lblSeed.Text = "Seed (Hex):";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.btnSimpleGo);
            this.GroupBox1.Controls.Add(this.textBoxSeed);
            this.GroupBox1.Controls.Add(this.lblSeed);
            this.GroupBox1.Controls.Add(this.lblSimple);
            this.GroupBox1.Location = new System.Drawing.Point(195, 1);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(376, 34);
            this.GroupBox1.TabIndex = 30;
            this.GroupBox1.TabStop = false;
            // 
            // btnSimpleGo
            // 
            this.btnSimpleGo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnSimpleGo.ForeColor = System.Drawing.Color.Black;
            this.btnSimpleGo.Location = new System.Drawing.Point(327, 7);
            this.btnSimpleGo.Name = "btnSimpleGo";
            this.btnSimpleGo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnSimpleGo.ShineColor = System.Drawing.SystemColors.Window;
            this.btnSimpleGo.Size = new System.Drawing.Size(38, 24);
            this.btnSimpleGo.TabIndex = 30;
            this.btnSimpleGo.Text = "Go";
            this.btnSimpleGo.Click += new System.EventHandler(this.btnSimpleGo_Click);
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Hex = true;
            this.textBoxSeed.Location = new System.Drawing.Point(256, 10);
            this.textBoxSeed.Mask = "AAAAAAAA";
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(65, 20);
            this.textBoxSeed.TabIndex = 29;
            // 
            // bgwShinyInf
            // 
            this.bgwShinyInf.WorkerReportsProgress = true;
            this.bgwShinyInf.WorkerSupportsCancellation = true;
            this.bgwShinyInf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwShinyInf_DoWork);
            this.bgwShinyInf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwShinyInf_RunWorkerCompleted);
            // 
            // tabGenSelect
            // 
            this.tabGenSelect.Controls.Add(this.tabXDColo);
            this.tabGenSelect.Controls.Add(this.tabGen3FRLGE);
            this.tabGenSelect.Controls.Add(this.tabGen3RS);
            this.tabGenSelect.Controls.Add(this.tabGen4);
            this.tabGenSelect.Controls.Add(this.tabGen5);
            this.tabGenSelect.Location = new System.Drawing.Point(12, 5);
            this.tabGenSelect.Name = "tabGenSelect";
            this.tabGenSelect.SelectedIndex = 0;
            this.tabGenSelect.Size = new System.Drawing.Size(596, 385);
            this.tabGenSelect.TabIndex = 31;
            this.tabGenSelect.SelectedIndexChanged += new System.EventHandler(this.tabGenSelect_SelectedIndexChanged);
            // 
            // tabXDColo
            // 
            this.tabXDColo.Controls.Add(this.labelXDColo);
            this.tabXDColo.Controls.Add(this.genCancelXDColo);
            this.tabXDColo.Controls.Add(this.searchGenXDColo);
            this.tabXDColo.Controls.Add(this.groupBox7);
            this.tabXDColo.Controls.Add(this.groupBox6);
            this.tabXDColo.Location = new System.Drawing.Point(4, 22);
            this.tabXDColo.Name = "tabXDColo";
            this.tabXDColo.Size = new System.Drawing.Size(588, 359);
            this.tabXDColo.TabIndex = 4;
            this.tabXDColo.Text = "XD/Colo";
            this.tabXDColo.UseVisualStyleBackColor = true;
            // 
            // labelXDColo
            // 
            this.labelXDColo.Location = new System.Drawing.Point(6, 262);
            this.labelXDColo.Name = "labelXDColo";
            this.labelXDColo.Size = new System.Drawing.Size(269, 30);
            this.labelXDColo.TabIndex = 123;
            this.labelXDColo.Text = "This feature will only work for Gales of Darkness and Colosseum.";
            // 
            // genCancelXDColo
            // 
            this.genCancelXDColo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.genCancelXDColo.ForeColor = System.Drawing.Color.Black;
            this.genCancelXDColo.Location = new System.Drawing.Point(204, 234);
            this.genCancelXDColo.Name = "genCancelXDColo";
            this.genCancelXDColo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.genCancelXDColo.ShineColor = System.Drawing.SystemColors.Window;
            this.genCancelXDColo.Size = new System.Drawing.Size(177, 25);
            this.genCancelXDColo.TabIndex = 122;
            this.genCancelXDColo.Text = "Cancel";
            this.genCancelXDColo.Click += new System.EventHandler(this.genCancelXDColo_Click);
            // 
            // searchGenXDColo
            // 
            this.searchGenXDColo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.searchGenXDColo.ForeColor = System.Drawing.Color.Black;
            this.searchGenXDColo.Location = new System.Drawing.Point(3, 234);
            this.searchGenXDColo.Name = "searchGenXDColo";
            this.searchGenXDColo.OuterBorderColor = System.Drawing.Color.Transparent;
            this.searchGenXDColo.ShineColor = System.Drawing.SystemColors.Window;
            this.searchGenXDColo.Size = new System.Drawing.Size(195, 25);
            this.searchGenXDColo.TabIndex = 32;
            this.searchGenXDColo.Text = "Find ID Frames";
            this.searchGenXDColo.Click += new System.EventHandler(this.searchGenXDColo_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.XDColoMaxFrame);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.XDColoMinFrame);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Location = new System.Drawing.Point(204, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(177, 77);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Search Parameters";
            // 
            // XDColoMaxFrame
            // 
            this.XDColoMaxFrame.Hex = false;
            this.XDColoMaxFrame.Location = new System.Drawing.Point(72, 50);
            this.XDColoMaxFrame.Mask = "000000";
            this.XDColoMaxFrame.Name = "XDColoMaxFrame";
            this.XDColoMaxFrame.Size = new System.Drawing.Size(90, 20);
            this.XDColoMaxFrame.TabIndex = 7;
            this.XDColoMaxFrame.Text = "10000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Min Frame:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // XDColoMinFrame
            // 
            this.XDColoMinFrame.Hex = false;
            this.XDColoMinFrame.Location = new System.Drawing.Point(72, 24);
            this.XDColoMinFrame.Mask = "000000";
            this.XDColoMinFrame.Name = "XDColoMinFrame";
            this.XDColoMinFrame.Size = new System.Drawing.Size(90, 20);
            this.XDColoMinFrame.TabIndex = 6;
            this.XDColoMinFrame.Text = "5000";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Max Frame:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.XDColoPID);
            this.groupBox6.Controls.Add(this.XDColoPRNG);
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(195, 77);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Search By";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "PRNG";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "PID";
            // 
            // XDColoPID
            // 
            this.XDColoPID.Hex = true;
            this.XDColoPID.Location = new System.Drawing.Point(62, 17);
            this.XDColoPID.Mask = "AAAAAAAA";
            this.XDColoPID.Name = "XDColoPID";
            this.XDColoPID.Size = new System.Drawing.Size(119, 20);
            this.XDColoPID.TabIndex = 7;
            this.toolTips.SetToolTip(this.XDColoPID, "Enter the PID you want shiny.");
            // 
            // XDColoPRNG
            // 
            this.XDColoPRNG.Hex = true;
            this.XDColoPRNG.Location = new System.Drawing.Point(62, 43);
            this.XDColoPRNG.Mask = "AAAAAAAA";
            this.XDColoPRNG.Name = "XDColoPRNG";
            this.XDColoPRNG.Size = new System.Drawing.Size(119, 20);
            this.XDColoPRNG.TabIndex = 3;
            this.toolTips.SetToolTip(this.XDColoPRNG, "Enter the current PRNG state.");
            // 
            // tabGen3FRLGE
            // 
            this.tabGen3FRLGE.Controls.Add(this.label11);
            this.tabGen3FRLGE.Controls.Add(this.genCancelFRLGE);
            this.tabGen3FRLGE.Controls.Add(this.genSearchFRLGE);
            this.tabGen3FRLGE.Controls.Add(this.groupBox5);
            this.tabGen3FRLGE.Controls.Add(this.groupBox2);
            this.tabGen3FRLGE.Location = new System.Drawing.Point(4, 22);
            this.tabGen3FRLGE.Name = "tabGen3FRLGE";
            this.tabGen3FRLGE.Size = new System.Drawing.Size(588, 359);
            this.tabGen3FRLGE.TabIndex = 3;
            this.tabGen3FRLGE.Text = "Gen III FRLGE";
            this.tabGen3FRLGE.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 248);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(269, 30);
            this.label11.TabIndex = 122;
            this.label11.Text = "This feature will only work for Fire Red, Leaf Green, and Emerald.";
            // 
            // genCancelFRLGE
            // 
            this.genCancelFRLGE.BackColor = System.Drawing.Color.AntiqueWhite;
            this.genCancelFRLGE.ForeColor = System.Drawing.Color.Black;
            this.genCancelFRLGE.Location = new System.Drawing.Point(204, 220);
            this.genCancelFRLGE.Name = "genCancelFRLGE";
            this.genCancelFRLGE.OuterBorderColor = System.Drawing.Color.Transparent;
            this.genCancelFRLGE.ShineColor = System.Drawing.SystemColors.Window;
            this.genCancelFRLGE.Size = new System.Drawing.Size(177, 25);
            this.genCancelFRLGE.TabIndex = 121;
            this.genCancelFRLGE.Text = "Cancel";
            this.genCancelFRLGE.Click += new System.EventHandler(this.genCancelFRLGE_Click);
            // 
            // genSearchFRLGE
            // 
            this.genSearchFRLGE.BackColor = System.Drawing.Color.AntiqueWhite;
            this.genSearchFRLGE.ForeColor = System.Drawing.Color.Black;
            this.genSearchFRLGE.Location = new System.Drawing.Point(3, 220);
            this.genSearchFRLGE.Name = "genSearchFRLGE";
            this.genSearchFRLGE.OuterBorderColor = System.Drawing.Color.Transparent;
            this.genSearchFRLGE.ShineColor = System.Drawing.SystemColors.Window;
            this.genSearchFRLGE.Size = new System.Drawing.Size(195, 25);
            this.genSearchFRLGE.TabIndex = 31;
            this.genSearchFRLGE.Text = "Find ID Frames";
            this.genSearchFRLGE.Click += new System.EventHandler(this.genSearchFRLGE_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.genFRLGEMaxFrame);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.genFRLGEMinFrame);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(204, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(177, 77);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search Parameters";
            // 
            // genFRLGEMaxFrame
            // 
            this.genFRLGEMaxFrame.Hex = false;
            this.genFRLGEMaxFrame.Location = new System.Drawing.Point(72, 50);
            this.genFRLGEMaxFrame.Mask = "000000";
            this.genFRLGEMaxFrame.Name = "genFRLGEMaxFrame";
            this.genFRLGEMaxFrame.Size = new System.Drawing.Size(90, 20);
            this.genFRLGEMaxFrame.TabIndex = 7;
            this.genFRLGEMaxFrame.Text = "10000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Min Frame:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // genFRLGEMinFrame
            // 
            this.genFRLGEMinFrame.Hex = false;
            this.genFRLGEMinFrame.Location = new System.Drawing.Point(72, 24);
            this.genFRLGEMinFrame.Mask = "000000";
            this.genFRLGEMinFrame.Name = "genFRLGEMinFrame";
            this.genFRLGEMinFrame.Size = new System.Drawing.Size(90, 20);
            this.genFRLGEMinFrame.TabIndex = 6;
            this.genFRLGEMinFrame.Text = "5000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Max Frame:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.genFRLGEPID);
            this.groupBox2.Controls.Add(this.genFRLGETID);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 77);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search By";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "TID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "PID";
            // 
            // genFRLGEPID
            // 
            this.genFRLGEPID.Hex = true;
            this.genFRLGEPID.Location = new System.Drawing.Point(62, 17);
            this.genFRLGEPID.Mask = "AAAAAAAA";
            this.genFRLGEPID.Name = "genFRLGEPID";
            this.genFRLGEPID.Size = new System.Drawing.Size(119, 20);
            this.genFRLGEPID.TabIndex = 7;
            this.toolTips.SetToolTip(this.genFRLGEPID, "Enter the PID you want shiny.");
            // 
            // genFRLGETID
            // 
            this.genFRLGETID.Hex = false;
            this.genFRLGETID.Location = new System.Drawing.Point(62, 43);
            this.genFRLGETID.Mask = "00000";
            this.genFRLGETID.Name = "genFRLGETID";
            this.genFRLGETID.Size = new System.Drawing.Size(119, 20);
            this.genFRLGETID.TabIndex = 3;
            this.toolTips.SetToolTip(this.genFRLGETID, "Enter the TID you obtained.");
            // 
            // tabGen3RS
            // 
            this.tabGen3RS.Controls.Add(this.buttonIIICancel);
            this.tabGen3RS.Controls.Add(this.label32);
            this.tabGen3RS.Controls.Add(this.groupBox4);
            this.tabGen3RS.Controls.Add(this.buttonIIIFindFrames);
            this.tabGen3RS.Controls.Add(this.groupBox3);
            this.tabGen3RS.Location = new System.Drawing.Point(4, 22);
            this.tabGen3RS.Name = "tabGen3RS";
            this.tabGen3RS.Padding = new System.Windows.Forms.Padding(3);
            this.tabGen3RS.Size = new System.Drawing.Size(588, 359);
            this.tabGen3RS.TabIndex = 2;
            this.tabGen3RS.Text = "Gen III RS";
            this.tabGen3RS.UseVisualStyleBackColor = true;
            // 
            // buttonIIICancel
            // 
            this.buttonIIICancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonIIICancel.ForeColor = System.Drawing.Color.Black;
            this.buttonIIICancel.Location = new System.Drawing.Point(207, 185);
            this.buttonIIICancel.Name = "buttonIIICancel";
            this.buttonIIICancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonIIICancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonIIICancel.Size = new System.Drawing.Size(177, 25);
            this.buttonIIICancel.TabIndex = 120;
            this.buttonIIICancel.Text = "Cancel";
            this.buttonIIICancel.Click += new System.EventHandler(this.buttonIIICancel_Click);
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(3, 213);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(269, 30);
            this.label32.TabIndex = 119;
            this.label32.Text = "This feature will only work for a copy of Ruby\\Sapphire that displays the \"intern" +
    "al battery has run dry\" error.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkIIIClock);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textIIIMaxFrame);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textIIIMinFrame);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textIIIMinute);
            this.groupBox4.Controls.Add(this.dateIII);
            this.groupBox4.Controls.Add(this.textIIIHour);
            this.groupBox4.Location = new System.Drawing.Point(207, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 173);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search Parameters";
            // 
            // checkIIIClock
            // 
            this.checkIIIClock.AutoSize = true;
            this.checkIIIClock.Checked = true;
            this.checkIIIClock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIIIClock.Location = new System.Drawing.Point(6, 19);
            this.checkIIIClock.Name = "checkIIIClock";
            this.checkIIIClock.Size = new System.Drawing.Size(88, 17);
            this.checkIIIClock.TabIndex = 33;
            this.checkIIIClock.Text = "Dead Battery";
            this.checkIIIClock.UseVisualStyleBackColor = true;
            this.checkIIIClock.CheckedChanged += new System.EventHandler(this.checkIIIClock_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Minute:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textIIIMaxFrame
            // 
            this.textIIIMaxFrame.Hex = false;
            this.textIIIMaxFrame.Location = new System.Drawing.Point(72, 146);
            this.textIIIMaxFrame.Mask = "000000";
            this.textIIIMaxFrame.Name = "textIIIMaxFrame";
            this.textIIIMaxFrame.Size = new System.Drawing.Size(90, 20);
            this.textIIIMaxFrame.TabIndex = 7;
            this.textIIIMaxFrame.Text = "10000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Min Frame:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Hour:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textIIIMinFrame
            // 
            this.textIIIMinFrame.Hex = false;
            this.textIIIMinFrame.Location = new System.Drawing.Point(72, 120);
            this.textIIIMinFrame.Mask = "000000";
            this.textIIIMinFrame.Name = "textIIIMinFrame";
            this.textIIIMinFrame.Size = new System.Drawing.Size(90, 20);
            this.textIIIMinFrame.TabIndex = 6;
            this.textIIIMinFrame.Text = "5000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Max Frame:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textIIIMinute
            // 
            this.textIIIMinute.Enabled = false;
            this.textIIIMinute.Hex = false;
            this.textIIIMinute.Location = new System.Drawing.Point(72, 94);
            this.textIIIMinute.Mask = "00";
            this.textIIIMinute.Name = "textIIIMinute";
            this.textIIIMinute.Size = new System.Drawing.Size(24, 20);
            this.textIIIMinute.TabIndex = 30;
            this.textIIIMinute.Text = "0";
            // 
            // dateIII
            // 
            this.dateIII.CustomFormat = "";
            this.dateIII.Enabled = false;
            this.dateIII.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateIII.Location = new System.Drawing.Point(72, 42);
            this.dateIII.MaxDate = new System.DateTime(2038, 1, 19, 0, 0, 0, 0);
            this.dateIII.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateIII.Name = "dateIII";
            this.dateIII.Size = new System.Drawing.Size(99, 20);
            this.dateIII.TabIndex = 10;
            this.toolTips.SetToolTip(this.dateIII, "Select the date to search.");
            this.dateIII.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // textIIIHour
            // 
            this.textIIIHour.Enabled = false;
            this.textIIIHour.Hex = false;
            this.textIIIHour.Location = new System.Drawing.Point(72, 66);
            this.textIIIHour.Mask = "00";
            this.textIIIHour.Name = "textIIIHour";
            this.textIIIHour.Size = new System.Drawing.Size(24, 20);
            this.textIIIHour.TabIndex = 29;
            this.textIIIHour.Text = "0";
            // 
            // buttonIIIFindFrames
            // 
            this.buttonIIIFindFrames.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonIIIFindFrames.ForeColor = System.Drawing.Color.Black;
            this.buttonIIIFindFrames.Location = new System.Drawing.Point(6, 185);
            this.buttonIIIFindFrames.Name = "buttonIIIFindFrames";
            this.buttonIIIFindFrames.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonIIIFindFrames.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonIIIFindFrames.Size = new System.Drawing.Size(195, 25);
            this.buttonIIIFindFrames.TabIndex = 26;
            this.buttonIIIFindFrames.Text = "Find ID Frames";
            this.buttonIIIFindFrames.Click += new System.EventHandler(this.buttonIIIFindFrames_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textIIISID);
            this.groupBox3.Controls.Add(this.checkIIISID);
            this.groupBox3.Controls.Add(this.checkIIIPID);
            this.groupBox3.Controls.Add(this.textIIIPID);
            this.groupBox3.Controls.Add(this.textIIITID);
            this.groupBox3.Controls.Add(this.checkIIITID);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 97);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search By";
            // 
            // textIIISID
            // 
            this.textIIISID.Enabled = false;
            this.textIIISID.Hex = false;
            this.textIIISID.Location = new System.Drawing.Point(62, 67);
            this.textIIISID.Mask = "00000";
            this.textIIISID.Name = "textIIISID";
            this.textIIISID.Size = new System.Drawing.Size(119, 20);
            this.textIIISID.TabIndex = 9;
            this.toolTips.SetToolTip(this.textIIISID, "Enter TID to search for here.");
            // 
            // checkIIISID
            // 
            this.checkIIISID.AutoSize = true;
            this.checkIIISID.Location = new System.Drawing.Point(6, 69);
            this.checkIIISID.Name = "checkIIISID";
            this.checkIIISID.Size = new System.Drawing.Size(50, 17);
            this.checkIIISID.TabIndex = 8;
            this.checkIIISID.Text = "SID: ";
            this.checkIIISID.UseVisualStyleBackColor = true;
            this.checkIIISID.CheckedChanged += new System.EventHandler(this.checkIIISID_CheckedChanged);
            // 
            // checkIIIPID
            // 
            this.checkIIIPID.AutoSize = true;
            this.checkIIIPID.Location = new System.Drawing.Point(6, 19);
            this.checkIIIPID.Name = "checkIIIPID";
            this.checkIIIPID.Size = new System.Drawing.Size(47, 17);
            this.checkIIIPID.TabIndex = 6;
            this.checkIIIPID.Text = "PID:";
            this.checkIIIPID.UseVisualStyleBackColor = true;
            this.checkIIIPID.CheckedChanged += new System.EventHandler(this.checkIIIPID_CheckedChanged);
            // 
            // textIIIPID
            // 
            this.textIIIPID.Enabled = false;
            this.textIIIPID.Hex = true;
            this.textIIIPID.Location = new System.Drawing.Point(62, 17);
            this.textIIIPID.Mask = "AAAAAAAA";
            this.textIIIPID.Name = "textIIIPID";
            this.textIIIPID.Size = new System.Drawing.Size(119, 20);
            this.textIIIPID.TabIndex = 7;
            this.toolTips.SetToolTip(this.textIIIPID, "Enter the full seed given by RNG Reporter.");
            // 
            // textIIITID
            // 
            this.textIIITID.Enabled = false;
            this.textIIITID.Hex = false;
            this.textIIITID.Location = new System.Drawing.Point(62, 43);
            this.textIIITID.Mask = "00000";
            this.textIIITID.Name = "textIIITID";
            this.textIIITID.Size = new System.Drawing.Size(119, 20);
            this.textIIITID.TabIndex = 3;
            this.toolTips.SetToolTip(this.textIIITID, "Enter TID to search for here.");
            // 
            // checkIIITID
            // 
            this.checkIIITID.AutoSize = true;
            this.checkIIITID.Location = new System.Drawing.Point(6, 45);
            this.checkIIITID.Name = "checkIIITID";
            this.checkIIITID.Size = new System.Drawing.Size(50, 17);
            this.checkIIITID.TabIndex = 2;
            this.checkIIITID.Text = "TID: ";
            this.checkIIITID.UseVisualStyleBackColor = true;
            this.checkIIITID.CheckedChanged += new System.EventHandler(this.checkIIITID_CheckedChanged);
            // 
            // tabGen4
            // 
            this.tabGen4.Controls.Add(this.labelBy);
            this.tabGen4.Controls.Add(this.GroupBox1);
            this.tabGen4.Controls.Add(this.grpShiny);
            this.tabGen4.Controls.Add(this.grpID);
            this.tabGen4.Controls.Add(this.grpSeed);
            this.tabGen4.Controls.Add(this.btnCredits);
            this.tabGen4.Location = new System.Drawing.Point(4, 22);
            this.tabGen4.Name = "tabGen4";
            this.tabGen4.Padding = new System.Windows.Forms.Padding(3);
            this.tabGen4.Size = new System.Drawing.Size(588, 359);
            this.tabGen4.TabIndex = 0;
            this.tabGen4.Text = "Gen IV";
            this.tabGen4.UseVisualStyleBackColor = true;
            // 
            // labelBy
            // 
            this.labelBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBy.AutoSize = true;
            this.labelBy.Location = new System.Drawing.Point(14, 14);
            this.labelBy.Name = "labelBy";
            this.labelBy.Size = new System.Drawing.Size(62, 13);
            this.labelBy.TabIndex = 32;
            this.labelBy.Text = "By WildEep";
            // 
            // btnCredits
            // 
            this.btnCredits.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btnCredits.ForeColor = System.Drawing.Color.Black;
            this.btnCredits.Location = new System.Drawing.Point(99, 6);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnCredits.ShineColor = System.Drawing.SystemColors.Window;
            this.btnCredits.Size = new System.Drawing.Size(90, 29);
            this.btnCredits.TabIndex = 0;
            this.btnCredits.Text = "Credits";
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            // 
            // tabGen5
            // 
            this.tabGen5.Controls.Add(this.buttonVFindSeedHit);
            this.tabGen5.Controls.Add(this.groupVSeedFinder);
            this.tabGen5.Controls.Add(this.groupVDSParams);
            this.tabGen5.Controls.Add(this.groupVSearchParams);
            this.tabGen5.Controls.Add(this.groupVPID);
            this.tabGen5.Controls.Add(this.buttonVFindSeeds);
            this.tabGen5.Controls.Add(this.buttonVCancel);
            this.tabGen5.Location = new System.Drawing.Point(4, 22);
            this.tabGen5.Name = "tabGen5";
            this.tabGen5.Padding = new System.Windows.Forms.Padding(3);
            this.tabGen5.Size = new System.Drawing.Size(588, 359);
            this.tabGen5.TabIndex = 1;
            this.tabGen5.Text = "Gen V";
            this.tabGen5.UseVisualStyleBackColor = true;
            // 
            // buttonVFindSeedHit
            // 
            this.buttonVFindSeedHit.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonVFindSeedHit.ForeColor = System.Drawing.Color.Black;
            this.buttonVFindSeedHit.Location = new System.Drawing.Point(208, 328);
            this.buttonVFindSeedHit.Name = "buttonVFindSeedHit";
            this.buttonVFindSeedHit.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonVFindSeedHit.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonVFindSeedHit.Size = new System.Drawing.Size(190, 25);
            this.buttonVFindSeedHit.TabIndex = 15;
            this.buttonVFindSeedHit.Text = "Find Seed Hit";
            this.buttonVFindSeedHit.Click += new System.EventHandler(this.buttonVFindSeedHit_Click);
            // 
            // groupVSeedFinder
            // 
            this.groupVSeedFinder.Controls.Add(this.textVMinute);
            this.groupVSeedFinder.Controls.Add(this.textVHour);
            this.groupVSeedFinder.Controls.Add(this.labelTIDReceived);
            this.groupVSeedFinder.Controls.Add(this.labelVMinute);
            this.groupVSeedFinder.Controls.Add(this.textVTIDReceived);
            this.groupVSeedFinder.Controls.Add(this.textVMaxSec);
            this.groupVSeedFinder.Controls.Add(this.labelVHour);
            this.groupVSeedFinder.Controls.Add(this.textVMinSec);
            this.groupVSeedFinder.Controls.Add(this.label1);
            this.groupVSeedFinder.Controls.Add(this.textVMaxFrameHit);
            this.groupVSeedFinder.Controls.Add(this.dateTimeSeedSearch);
            this.groupVSeedFinder.Controls.Add(this.label3);
            this.groupVSeedFinder.Controls.Add(this.textVMinFrameHit);
            this.groupVSeedFinder.Controls.Add(this.label2);
            this.groupVSeedFinder.Location = new System.Drawing.Point(208, 174);
            this.groupVSeedFinder.Name = "groupVSeedFinder";
            this.groupVSeedFinder.Size = new System.Drawing.Size(190, 148);
            this.groupVSeedFinder.TabIndex = 24;
            this.groupVSeedFinder.TabStop = false;
            this.groupVSeedFinder.Text = "Seed Finder";
            // 
            // textVMinute
            // 
            this.textVMinute.Hex = false;
            this.textVMinute.Location = new System.Drawing.Point(139, 67);
            this.textVMinute.Name = "textVMinute";
            this.textVMinute.Size = new System.Drawing.Size(41, 20);
            this.textVMinute.TabIndex = 10;
            this.textVMinute.Text = "0";
            this.toolTips.SetToolTip(this.textVMinute, "Enter the minute you started your game/");
            // 
            // textVHour
            // 
            this.textVHour.Hex = false;
            this.textVHour.Location = new System.Drawing.Point(42, 67);
            this.textVHour.Name = "textVHour";
            this.textVHour.Size = new System.Drawing.Size(44, 20);
            this.textVHour.TabIndex = 11;
            this.textVHour.Text = "0";
            this.toolTips.SetToolTip(this.textVHour, "Enter the hour that you started the game.");
            // 
            // labelTIDReceived
            // 
            this.labelTIDReceived.AutoSize = true;
            this.labelTIDReceived.Location = new System.Drawing.Point(6, 18);
            this.labelTIDReceived.Name = "labelTIDReceived";
            this.labelTIDReceived.Size = new System.Drawing.Size(67, 13);
            this.labelTIDReceived.TabIndex = 22;
            this.labelTIDReceived.Text = "ID Received";
            // 
            // labelVMinute
            // 
            this.labelVMinute.AutoSize = true;
            this.labelVMinute.Location = new System.Drawing.Point(95, 70);
            this.labelVMinute.Name = "labelVMinute";
            this.labelVMinute.Size = new System.Drawing.Size(42, 13);
            this.labelVMinute.TabIndex = 25;
            this.labelVMinute.Text = "Minute:";
            // 
            // textVTIDReceived
            // 
            this.textVTIDReceived.Hex = false;
            this.textVTIDReceived.Location = new System.Drawing.Point(84, 15);
            this.textVTIDReceived.Mask = "00000";
            this.textVTIDReceived.Name = "textVTIDReceived";
            this.textVTIDReceived.Size = new System.Drawing.Size(100, 20);
            this.textVTIDReceived.TabIndex = 8;
            this.toolTips.SetToolTip(this.textVTIDReceived, "Enter the ID you received here to see what seed you hit.");
            // 
            // textVMaxSec
            // 
            this.textVMaxSec.Hex = false;
            this.textVMaxSec.Location = new System.Drawing.Point(139, 94);
            this.textVMaxSec.Name = "textVMaxSec";
            this.textVMaxSec.Size = new System.Drawing.Size(44, 20);
            this.textVMaxSec.TabIndex = 13;
            this.textVMaxSec.Text = "59";
            // 
            // labelVHour
            // 
            this.labelVHour.AutoSize = true;
            this.labelVHour.Location = new System.Drawing.Point(6, 70);
            this.labelVHour.Name = "labelVHour";
            this.labelVHour.Size = new System.Drawing.Size(33, 13);
            this.labelVHour.TabIndex = 23;
            this.labelVHour.Text = "Hour:";
            // 
            // textVMinSec
            // 
            this.textVMinSec.Hex = false;
            this.textVMinSec.Location = new System.Drawing.Point(89, 94);
            this.textVMinSec.Name = "textVMinSec";
            this.textVMinSec.Size = new System.Drawing.Size(44, 20);
            this.textVMinSec.TabIndex = 12;
            this.textVMinSec.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Min/Max Sec:";
            // 
            // textVMaxFrameHit
            // 
            this.textVMaxFrameHit.Hex = false;
            this.textVMaxFrameHit.Location = new System.Drawing.Point(139, 120);
            this.textVMaxFrameHit.Name = "textVMaxFrameHit";
            this.textVMaxFrameHit.Size = new System.Drawing.Size(44, 20);
            this.textVMaxFrameHit.TabIndex = 15;
            this.textVMaxFrameHit.Text = "40";
            // 
            // dateTimeSeedSearch
            // 
            this.dateTimeSeedSearch.CustomFormat = "";
            this.dateTimeSeedSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeSeedSearch.Location = new System.Drawing.Point(84, 41);
            this.dateTimeSeedSearch.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dateTimeSeedSearch.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimeSeedSearch.Name = "dateTimeSeedSearch";
            this.dateTimeSeedSearch.Size = new System.Drawing.Size(99, 20);
            this.dateTimeSeedSearch.TabIndex = 9;
            this.dateTimeSeedSearch.Value = new System.DateTime(2011, 3, 28, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Date:";
            // 
            // textVMinFrameHit
            // 
            this.textVMinFrameHit.Hex = false;
            this.textVMinFrameHit.Location = new System.Drawing.Point(89, 120);
            this.textVMinFrameHit.Name = "textVMinFrameHit";
            this.textVMinFrameHit.Size = new System.Drawing.Size(44, 20);
            this.textVMinFrameHit.TabIndex = 14;
            this.textVMinFrameHit.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Min/Max Frame:";
            // 
            // groupVDSParams
            // 
            this.groupVDSParams.Controls.Add(this.label4);
            this.groupVDSParams.Controls.Add(this.labelProfileInformation);
            this.groupVDSParams.Controls.Add(this.comboBoxProfiles);
            this.groupVDSParams.Controls.Add(this.buttonEditProfile);
            this.groupVDSParams.Location = new System.Drawing.Point(7, 7);
            this.groupVDSParams.Name = "groupVDSParams";
            this.groupVDSParams.Size = new System.Drawing.Size(574, 143);
            this.groupVDSParams.TabIndex = 23;
            this.groupVDSParams.TabStop = false;
            this.groupVDSParams.Text = "DS Parameters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 13);
            this.label4.TabIndex = 324;
            this.label4.Text = "Please note: only the min Timer0 is used.";
            // 
            // labelProfileInformation
            // 
            this.labelProfileInformation.AutoSize = true;
            this.labelProfileInformation.Location = new System.Drawing.Point(3, 47);
            this.labelProfileInformation.Name = "labelProfileInformation";
            this.labelProfileInformation.Size = new System.Drawing.Size(0, 13);
            this.labelProfileInformation.TabIndex = 323;
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
            this.comboBoxProfiles.TabIndex = 322;
            this.comboBoxProfiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxProfiles_SelectedIndexChanged);
            // 
            // buttonEditProfile
            // 
            this.buttonEditProfile.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonEditProfile.ForeColor = System.Drawing.Color.Black;
            this.buttonEditProfile.Location = new System.Drawing.Point(131, 17);
            this.buttonEditProfile.Name = "buttonEditProfile";
            this.buttonEditProfile.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonEditProfile.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonEditProfile.Size = new System.Drawing.Size(39, 23);
            this.buttonEditProfile.TabIndex = 321;
            this.buttonEditProfile.Text = "Edit";
            this.buttonEditProfile.Click += new System.EventHandler(this.buttonEditProfile_Click);
            // 
            // groupVSearchParams
            // 
            this.groupVSearchParams.Controls.Add(this.checkBoxSaveExists);
            this.groupVSearchParams.Controls.Add(this.checkBoxMinFrameCalc);
            this.groupVSearchParams.Controls.Add(this.textVMaxFrame);
            this.groupVSearchParams.Controls.Add(this.labelVMaxFrame);
            this.groupVSearchParams.Controls.Add(this.textVMinFrame);
            this.groupVSearchParams.Controls.Add(this.labelVMinFrame);
            this.groupVSearchParams.Controls.Add(this.checkVMonth);
            this.groupVSearchParams.Controls.Add(this.dateTimeSearch);
            this.groupVSearchParams.Controls.Add(this.labelVDate);
            this.groupVSearchParams.Location = new System.Drawing.Point(404, 157);
            this.groupVSearchParams.Name = "groupVSearchParams";
            this.groupVSearchParams.Size = new System.Drawing.Size(177, 165);
            this.groupVSearchParams.TabIndex = 8;
            this.groupVSearchParams.TabStop = false;
            this.groupVSearchParams.Text = "Search Parameters";
            // 
            // checkBoxSaveExists
            // 
            this.checkBoxSaveExists.AutoSize = true;
            this.checkBoxSaveExists.Enabled = false;
            this.checkBoxSaveExists.Location = new System.Drawing.Point(17, 112);
            this.checkBoxSaveExists.Name = "checkBoxSaveExists";
            this.checkBoxSaveExists.Size = new System.Drawing.Size(109, 17);
            this.checkBoxSaveExists.TabIndex = 11;
            this.checkBoxSaveExists.Text = "Existing Save File";
            this.toolTips.SetToolTip(this.checkBoxSaveExists, "Choose any day during the month you want to search.");
            this.checkBoxSaveExists.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinFrameCalc
            // 
            this.checkBoxMinFrameCalc.AutoSize = true;
            this.checkBoxMinFrameCalc.Location = new System.Drawing.Point(17, 93);
            this.checkBoxMinFrameCalc.Name = "checkBoxMinFrameCalc";
            this.checkBoxMinFrameCalc.Size = new System.Drawing.Size(122, 17);
            this.checkBoxMinFrameCalc.TabIndex = 10;
            this.checkBoxMinFrameCalc.Text = "Calculate Min Frame";
            this.toolTips.SetToolTip(this.checkBoxMinFrameCalc, "Choose any day during the month you want to search.");
            this.checkBoxMinFrameCalc.UseVisualStyleBackColor = true;
            this.checkBoxMinFrameCalc.CheckedChanged += new System.EventHandler(this.checkBoxMinFrameCalc_CheckedChanged);
            // 
            // textVMaxFrame
            // 
            this.textVMaxFrame.Hex = false;
            this.textVMaxFrame.Location = new System.Drawing.Point(79, 135);
            this.textVMaxFrame.Mask = "00";
            this.textVMaxFrame.Name = "textVMaxFrame";
            this.textVMaxFrame.Size = new System.Drawing.Size(90, 20);
            this.textVMaxFrame.TabIndex = 7;
            this.textVMaxFrame.Text = "40";
            // 
            // labelVMaxFrame
            // 
            this.labelVMaxFrame.AutoSize = true;
            this.labelVMaxFrame.Location = new System.Drawing.Point(14, 138);
            this.labelVMaxFrame.Name = "labelVMaxFrame";
            this.labelVMaxFrame.Size = new System.Drawing.Size(62, 13);
            this.labelVMaxFrame.TabIndex = 9;
            this.labelVMaxFrame.Text = "Max Frame:";
            // 
            // textVMinFrame
            // 
            this.textVMinFrame.Hex = false;
            this.textVMinFrame.Location = new System.Drawing.Point(79, 68);
            this.textVMinFrame.Mask = "00";
            this.textVMinFrame.Name = "textVMinFrame";
            this.textVMinFrame.Size = new System.Drawing.Size(90, 20);
            this.textVMinFrame.TabIndex = 6;
            this.textVMinFrame.Text = "28";
            // 
            // labelVMinFrame
            // 
            this.labelVMinFrame.AutoSize = true;
            this.labelVMinFrame.Location = new System.Drawing.Point(14, 71);
            this.labelVMinFrame.Name = "labelVMinFrame";
            this.labelVMinFrame.Size = new System.Drawing.Size(59, 13);
            this.labelVMinFrame.TabIndex = 7;
            this.labelVMinFrame.Text = "Min Frame:";
            // 
            // checkVMonth
            // 
            this.checkVMonth.AutoSize = true;
            this.checkVMonth.Location = new System.Drawing.Point(17, 45);
            this.checkVMonth.Name = "checkVMonth";
            this.checkVMonth.Size = new System.Drawing.Size(123, 17);
            this.checkVMonth.TabIndex = 5;
            this.checkVMonth.Text = "Search Entire Month";
            this.toolTips.SetToolTip(this.checkVMonth, "Choose any day during the month you want to search.");
            this.checkVMonth.UseVisualStyleBackColor = true;
            // 
            // dateTimeSearch
            // 
            this.dateTimeSearch.CustomFormat = "";
            this.dateTimeSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeSearch.Location = new System.Drawing.Point(70, 19);
            this.dateTimeSearch.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dateTimeSearch.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimeSearch.Name = "dateTimeSearch";
            this.dateTimeSearch.Size = new System.Drawing.Size(99, 20);
            this.dateTimeSearch.TabIndex = 4;
            this.toolTips.SetToolTip(this.dateTimeSearch, "Select the date to search.");
            this.dateTimeSearch.Value = new System.DateTime(2011, 3, 28, 0, 0, 0, 0);
            // 
            // labelVDate
            // 
            this.labelVDate.AutoSize = true;
            this.labelVDate.Location = new System.Drawing.Point(14, 22);
            this.labelVDate.Name = "labelVDate";
            this.labelVDate.Size = new System.Drawing.Size(33, 13);
            this.labelVDate.TabIndex = 6;
            this.labelVDate.Text = "Date:";
            // 
            // groupVPID
            // 
            this.groupVPID.Controls.Add(this.textVSID);
            this.groupVPID.Controls.Add(this.checkVSID);
            this.groupVPID.Controls.Add(this.checkVPID);
            this.groupVPID.Controls.Add(this.textVPID);
            this.groupVPID.Controls.Add(this.labelVFrame);
            this.groupVPID.Controls.Add(this.textVFrame);
            this.groupVPID.Controls.Add(this.textVTID);
            this.groupVPID.Controls.Add(this.checkVSeed);
            this.groupVPID.Controls.Add(this.textVSeed);
            this.groupVPID.Controls.Add(this.checkVTID);
            this.groupVPID.Location = new System.Drawing.Point(6, 174);
            this.groupVPID.Name = "groupVPID";
            this.groupVPID.Size = new System.Drawing.Size(195, 148);
            this.groupVPID.TabIndex = 0;
            this.groupVPID.TabStop = false;
            this.groupVPID.Text = "Search By";
            // 
            // textVSID
            // 
            this.textVSID.Enabled = false;
            this.textVSID.Hex = false;
            this.textVSID.Location = new System.Drawing.Point(67, 120);
            this.textVSID.Mask = "00000";
            this.textVSID.Name = "textVSID";
            this.textVSID.Size = new System.Drawing.Size(119, 20);
            this.textVSID.TabIndex = 9;
            this.toolTips.SetToolTip(this.textVSID, "Enter TID to search for here.");
            // 
            // checkVSID
            // 
            this.checkVSID.AutoSize = true;
            this.checkVSID.Location = new System.Drawing.Point(11, 122);
            this.checkVSID.Name = "checkVSID";
            this.checkVSID.Size = new System.Drawing.Size(50, 17);
            this.checkVSID.TabIndex = 8;
            this.checkVSID.Text = "SID: ";
            this.checkVSID.UseVisualStyleBackColor = true;
            this.checkVSID.CheckedChanged += new System.EventHandler(this.checkVSID_CheckedChanged);
            // 
            // checkVPID
            // 
            this.checkVPID.AutoSize = true;
            this.checkVPID.Location = new System.Drawing.Point(11, 72);
            this.checkVPID.Name = "checkVPID";
            this.checkVPID.Size = new System.Drawing.Size(47, 17);
            this.checkVPID.TabIndex = 6;
            this.checkVPID.Text = "PID:";
            this.checkVPID.UseVisualStyleBackColor = true;
            this.checkVPID.CheckedChanged += new System.EventHandler(this.checkVPID_CheckedChanged);
            // 
            // textVPID
            // 
            this.textVPID.Enabled = false;
            this.textVPID.Hex = true;
            this.textVPID.Location = new System.Drawing.Point(67, 70);
            this.textVPID.Mask = "AAAAAAAA";
            this.textVPID.Name = "textVPID";
            this.textVPID.Size = new System.Drawing.Size(119, 20);
            this.textVPID.TabIndex = 7;
            this.toolTips.SetToolTip(this.textVPID, "Enter the full seed given by RNG Reporter.");
            // 
            // labelVFrame
            // 
            this.labelVFrame.AutoSize = true;
            this.labelVFrame.Location = new System.Drawing.Point(26, 49);
            this.labelVFrame.Name = "labelVFrame";
            this.labelVFrame.Size = new System.Drawing.Size(39, 13);
            this.labelVFrame.TabIndex = 5;
            this.labelVFrame.Text = "Frame:";
            // 
            // textVFrame
            // 
            this.textVFrame.Enabled = false;
            this.textVFrame.Hex = false;
            this.textVFrame.Location = new System.Drawing.Point(67, 46);
            this.textVFrame.Mask = "0000";
            this.textVFrame.Name = "textVFrame";
            this.textVFrame.Size = new System.Drawing.Size(119, 20);
            this.textVFrame.TabIndex = 4;
            this.toolTips.SetToolTip(this.textVFrame, "Enter the frame number for the PID.");
            // 
            // textVTID
            // 
            this.textVTID.Enabled = false;
            this.textVTID.Hex = false;
            this.textVTID.Location = new System.Drawing.Point(67, 96);
            this.textVTID.Mask = "00000";
            this.textVTID.Name = "textVTID";
            this.textVTID.Size = new System.Drawing.Size(119, 20);
            this.textVTID.TabIndex = 3;
            this.toolTips.SetToolTip(this.textVTID, "Enter TID to search for here.");
            // 
            // checkVSeed
            // 
            this.checkVSeed.AutoSize = true;
            this.checkVSeed.Location = new System.Drawing.Point(11, 21);
            this.checkVSeed.Name = "checkVSeed";
            this.checkVSeed.Size = new System.Drawing.Size(54, 17);
            this.checkVSeed.TabIndex = 0;
            this.checkVSeed.Text = "Seed:";
            this.checkVSeed.UseVisualStyleBackColor = true;
            this.checkVSeed.CheckedChanged += new System.EventHandler(this.textVSeed_TextChanged);
            // 
            // textVSeed
            // 
            this.textVSeed.Enabled = false;
            this.textVSeed.Hex = true;
            this.textVSeed.Location = new System.Drawing.Point(67, 19);
            this.textVSeed.Mask = "AAAAAAAAAAAAAAAA";
            this.textVSeed.Name = "textVSeed";
            this.textVSeed.Size = new System.Drawing.Size(119, 20);
            this.textVSeed.TabIndex = 1;
            this.toolTips.SetToolTip(this.textVSeed, "Enter the full seed given by RNG Reporter.");
            // 
            // checkVTID
            // 
            this.checkVTID.AutoSize = true;
            this.checkVTID.Location = new System.Drawing.Point(11, 98);
            this.checkVTID.Name = "checkVTID";
            this.checkVTID.Size = new System.Drawing.Size(50, 17);
            this.checkVTID.TabIndex = 2;
            this.checkVTID.Text = "TID: ";
            this.checkVTID.UseVisualStyleBackColor = true;
            this.checkVTID.CheckedChanged += new System.EventHandler(this.checkTID_CheckedChanged);
            // 
            // buttonVFindSeeds
            // 
            this.buttonVFindSeeds.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonVFindSeeds.ForeColor = System.Drawing.Color.Black;
            this.buttonVFindSeeds.Location = new System.Drawing.Point(7, 328);
            this.buttonVFindSeeds.Name = "buttonVFindSeeds";
            this.buttonVFindSeeds.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonVFindSeeds.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonVFindSeeds.Size = new System.Drawing.Size(195, 25);
            this.buttonVFindSeeds.TabIndex = 8;
            this.buttonVFindSeeds.Text = "Find ID Seeds";
            this.buttonVFindSeeds.Click += new System.EventHandler(this.buttonVFindSeeds_Click);
            // 
            // buttonVCancel
            // 
            this.buttonVCancel.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonVCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonVCancel.Location = new System.Drawing.Point(404, 328);
            this.buttonVCancel.Name = "buttonVCancel";
            this.buttonVCancel.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonVCancel.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonVCancel.Size = new System.Drawing.Size(177, 25);
            this.buttonVCancel.TabIndex = 22;
            this.buttonVCancel.Text = "Cancel";
            this.buttonVCancel.Click += new System.EventHandler(this.buttonVCancel_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSeed,
            this.clmFrame,
            this.clmInitialFrame,
            this.clmID,
            this.clmSID,
            this.clmDelay,
            this.clmSeconds,
            this.clmStarter,
            this.clmDate,
            this.clmTime,
            this.clmButton});
            this.dgvResults.ContextMenuStrip = this.contextMenuStrip;
            this.dgvResults.Location = new System.Drawing.Point(10, 396);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 20;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(596, 175);
            this.dgvResults.TabIndex = 25;
            this.dgvResults.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCapValues_ColumnHeaderMouseClick);
            this.dgvResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvResults_MouseDown);
            // 
            // clmSeed
            // 
            this.clmSeed.DataPropertyName = "Seed";
            this.clmSeed.HeaderText = "Seed";
            this.clmSeed.Name = "clmSeed";
            this.clmSeed.ReadOnly = true;
            // 
            // clmFrame
            // 
            this.clmFrame.DataPropertyName = "Frame";
            this.clmFrame.HeaderText = "Frame";
            this.clmFrame.Name = "clmFrame";
            this.clmFrame.ReadOnly = true;
            this.clmFrame.Visible = false;
            // 
            // clmInitialFrame
            // 
            this.clmInitialFrame.DataPropertyName = "InitialFrame";
            this.clmInitialFrame.HeaderText = "Initial Frame";
            this.clmInitialFrame.Name = "clmInitialFrame";
            this.clmInitialFrame.ReadOnly = true;
            this.clmInitialFrame.Visible = false;
            // 
            // clmID
            // 
            this.clmID.DataPropertyName = "ID";
            this.clmID.HeaderText = "ID";
            this.clmID.Name = "clmID";
            this.clmID.ReadOnly = true;
            // 
            // clmSID
            // 
            this.clmSID.DataPropertyName = "SID";
            this.clmSID.HeaderText = "SID";
            this.clmSID.Name = "clmSID";
            this.clmSID.ReadOnly = true;
            // 
            // clmDelay
            // 
            this.clmDelay.DataPropertyName = "Delay";
            this.clmDelay.HeaderText = "Delay";
            this.clmDelay.Name = "clmDelay";
            this.clmDelay.ReadOnly = true;
            this.clmDelay.Visible = false;
            // 
            // clmSeconds
            // 
            this.clmSeconds.DataPropertyName = "Seconds";
            this.clmSeconds.HeaderText = "Seconds";
            this.clmSeconds.Name = "clmSeconds";
            this.clmSeconds.ReadOnly = true;
            this.clmSeconds.Visible = false;
            // 
            // clmStarter
            // 
            this.clmStarter.DataPropertyName = "Starter";
            this.clmStarter.HeaderText = "Starter";
            this.clmStarter.Name = "clmStarter";
            this.clmStarter.ReadOnly = true;
            this.clmStarter.Visible = false;
            // 
            // clmDate
            // 
            this.clmDate.DataPropertyName = "Date";
            this.clmDate.HeaderText = "Date";
            this.clmDate.Name = "clmDate";
            this.clmDate.ReadOnly = true;
            this.clmDate.Visible = false;
            // 
            // clmTime
            // 
            this.clmTime.DataPropertyName = "Time";
            this.clmTime.HeaderText = "Time";
            this.clmTime.Name = "clmTime";
            this.clmTime.ReadOnly = true;
            this.clmTime.Visible = false;
            // 
            // clmButton
            // 
            this.clmButton.DataPropertyName = "Button";
            this.clmButton.HeaderText = "Buttons";
            this.clmButton.Name = "clmButton";
            this.clmButton.ReadOnly = true;
            this.clmButton.Visible = false;
            // 
            // Pandora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 599);
            this.Controls.Add(this.tabGenSelect);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.StatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Pandora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "\"Pandora\'s Box\" - ID\\SID Manipulation Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpShiny.ResumeLayout(false);
            this.grpShiny.PerformLayout();
            this.grpID.ResumeLayout(false);
            this.grpID.PerformLayout();
            this.grpSeed.ResumeLayout(false);
            this.grpSeed.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.tabGenSelect.ResumeLayout(false);
            this.tabXDColo.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabGen3FRLGE.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabGen3RS.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabGen4.ResumeLayout(false);
            this.tabGen4.PerformLayout();
            this.tabGen5.ResumeLayout(false);
            this.groupVSeedFinder.ResumeLayout(false);
            this.groupVSeedFinder.PerformLayout();
            this.groupVDSParams.ResumeLayout(false);
            this.groupVDSParams.PerformLayout();
            this.groupVSearchParams.ResumeLayout(false);
            this.groupVSearchParams.PerformLayout();
            this.groupVPID.ResumeLayout(false);
            this.groupVPID.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TabControl tabGenSelect;
        private System.Windows.Forms.TabPage tabGen4;
        private System.Windows.Forms.TabPage tabGen5;
        private System.Windows.Forms.GroupBox groupVPID;
        private System.Windows.Forms.Label labelBy;
        private System.Windows.Forms.DateTimePicker dateTimeSearch;
        private MaskedTextBox2 textVSeed;
        private System.Windows.Forms.CheckBox checkVMonth;
        private System.Windows.Forms.Label labelVDate;
        private System.Windows.Forms.CheckBox checkVTID;
        private System.Windows.Forms.GroupBox groupVSearchParams;
        private System.Windows.Forms.CheckBox checkVSeed;
        private MaskedTextBox2 textVTID;
        private System.Windows.Forms.Label labelVMaxFrame;
        private MaskedTextBox2 textVMinFrame;
        private System.Windows.Forms.Label labelVMinFrame;
        private System.Windows.Forms.ToolTip toolTips;
        private MaskedTextBox2 textVMaxFrame;
        private RNGReporter.GlassButton buttonVCancel;
        private RNGReporter.GlassButton buttonVFindSeeds;
        private System.Windows.Forms.GroupBox groupVDSParams;
        private System.Windows.Forms.GroupBox groupVSeedFinder;
        private RNGReporter.GlassButton buttonVFindSeedHit;
        private MaskedTextBox2 textVMinute;
        private System.Windows.Forms.Label labelVMinute;
        private MaskedTextBox2 textVHour;
        private System.Windows.Forms.Label labelVHour;
        private System.Windows.Forms.Label labelTIDReceived;
        private MaskedTextBox2 textVTIDReceived;
        private MaskedTextBox2 textVMaxSec;
        private MaskedTextBox2 textVMinSec;
        private System.Windows.Forms.Label label1;
        private MaskedTextBox2 textVMaxFrameHit;
        private System.Windows.Forms.DateTimePicker dateTimeSeedSearch;
        private System.Windows.Forms.Label label3;
        private MaskedTextBox2 textVMinFrameHit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelVFrame;
        private MaskedTextBox2 textVFrame;
        private System.Windows.Forms.CheckBox checkBoxMinFrameCalc;
        private System.Windows.Forms.CheckBox checkBoxSaveExists;
        private System.Windows.Forms.CheckBox checkVPID;
        private MaskedTextBox2 textVPID;
        private System.Windows.Forms.GroupBox grpShiny;
        private System.Windows.Forms.GroupBox grpID;
        private System.Windows.Forms.GroupBox grpSeed;
        private MaskedTextBox2 textBoxShinyPID;
        private System.Windows.Forms.Label lblShinyPID;
        private MaskedTextBox2 textBoxShinyYear;
        private System.Windows.Forms.Label lblShinyYr;
        private MaskedTextBox2 txtShinyMinDelay;
        private System.Windows.Forms.Label lblShinyMinDelay;
        private MaskedTextBox2 txtShinyMaxDelay;
        private System.Windows.Forms.Label lblShinyMaxDelay;
        private RNGReporter.GlassButton btnShinyGo;
        private System.Windows.Forms.Label lblTrainerID;
        private MaskedTextBox2 textBoxDesiredSID;
        private System.Windows.Forms.Label lblSecretID;
        private System.Windows.Forms.CheckBox cbxSearchSID;
        private MaskedTextBox2 textBoxDesiredTID;
        private MaskedTextBox2 textBoxIDYear;
        private System.Windows.Forms.Label lblIDYr;
        private MaskedTextBox2 textBoxIDMaxDelay;
        private MaskedTextBox2 textBoxIDMinDelay;
        private System.Windows.Forms.Label lblIDMaxDelay;
        private System.Windows.Forms.Label lblIDMinDelay;
        private RNGReporter.GlassButton btnIDGo;
        private MaskedTextBox2 txtMonth;
        private MaskedTextBox2 txtIDObtained;
        private System.Windows.Forms.Label lblIDObtained;
        private MaskedTextBox2 txtDay;
        private System.Windows.Forms.Label lblMonth;
        private RNGReporter.GlassButton btnCredits;
        private MaskedTextBox2 txtSeedMinDelay;
        private MaskedTextBox2 txtMinute;
        private MaskedTextBox2 txtHour;
        private MaskedTextBox2 txtSeedYr;
        private MaskedTextBox2 txtSeedMaxDelay;
        private RNGReporter.GlassButton btnSeedGo;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblSeedMinDelay;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblSeedYr;
        private System.Windows.Forms.Label lblSeedMaxDelay;
        private DoubleBufferedDataGridView dgvResults;
        private RNGReporter.GlassButton btnShinyCancel;
        private RNGReporter.GlassButton btnIDCancel;
        private System.Windows.Forms.CheckBox cbxIDInf;
        private System.Windows.Forms.CheckBox cbxSearchID;
        private System.Windows.Forms.Label lblShinyTrainerID;
        private System.Windows.Forms.CheckBox cbxShinyInf;
        private MaskedTextBox2 textBoxShinyTID;
        private System.Windows.Forms.Label lblSimple;
        private System.Windows.Forms.Label lblSeed;
        private RNGReporter.GlassButton btnSimpleGo;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.ComponentModel.BackgroundWorker bgwShiny;
        private System.ComponentModel.BackgroundWorker bgwID;
        private System.ComponentModel.BackgroundWorker bgwIDInf;
        private System.ComponentModel.BackgroundWorker bgwShinyInf;
        private System.Windows.Forms.ToolStripStatusLabel lblAction;
        private MaskedTextBox2 textBoxSeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFrame;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInitialFrame;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSeconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStarter;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copySeedToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem generateTimesToolStripMenuItem;
        private MaskedTextBox2 textVSID;
        private System.Windows.Forms.CheckBox checkVSID;
        private System.Windows.Forms.Label labelProfileInformation;
        private GlassComboBox comboBoxProfiles;
        private GlassButton buttonEditProfile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabGen3RS;
        private GlassButton buttonIIIFindFrames;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaskedTextBox2 textIIISID;
        private System.Windows.Forms.CheckBox checkIIISID;
        private System.Windows.Forms.CheckBox checkIIIPID;
        private MaskedTextBox2 textIIIPID;
        private MaskedTextBox2 textIIITID;
        private System.Windows.Forms.CheckBox checkIIITID;
        private System.Windows.Forms.GroupBox groupBox4;
        private MaskedTextBox2 textIIIMaxFrame;
        private System.Windows.Forms.Label label6;
        private MaskedTextBox2 textIIIMinFrame;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label32;
        private GlassButton buttonIIICancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private MaskedTextBox2 textIIIMinute;
        private System.Windows.Forms.DateTimePicker dateIII;
        private MaskedTextBox2 textIIIHour;
        private System.Windows.Forms.CheckBox checkIIIClock;
        private System.Windows.Forms.TabPage tabGen3FRLGE;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaskedTextBox2 genFRLGEPID;
        private MaskedTextBox2 genFRLGETID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private MaskedTextBox2 genFRLGEMaxFrame;
        private System.Windows.Forms.Label label12;
        private MaskedTextBox2 genFRLGEMinFrame;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private GlassButton genCancelFRLGE;
        private GlassButton genSearchFRLGE;
        private System.Windows.Forms.TabPage tabXDColo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private MaskedTextBox2 XDColoPID;
        private MaskedTextBox2 XDColoPRNG;
        private System.Windows.Forms.GroupBox groupBox7;
        private MaskedTextBox2 XDColoMaxFrame;
        private System.Windows.Forms.Label label16;
        private MaskedTextBox2 XDColoMinFrame;
        private System.Windows.Forms.Label label17;
        private GlassButton genCancelXDColo;
        private GlassButton searchGenXDColo;
        private System.Windows.Forms.Label labelXDColo;
    }

}
