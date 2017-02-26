using RNGReporter.Controls;
using RNGReporter.Objects;

namespace RNGReporter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            RNGReporter.Controls.CheckBoxProperties checkBoxProperties1 = new RNGReporter.Controls.CheckBoxProperties();
            RNGReporter.Controls.CheckBoxProperties checkBoxProperties2 = new RNGReporter.Controls.CheckBoxProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxShinyOnly = new System.Windows.Forms.CheckBox();
            this.contextMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lockFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.centerTo1SecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTo2SecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTo3SecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTo5SecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTo10SecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTp1MinuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeCenteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.calculatePoketechTapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.searchCoinFlipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchNaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchElmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayParentsInSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetParentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.outputResultsToTXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.saveFileDialogTxt = new System.Windows.Forms.SaveFileDialog();
            this.label14 = new System.Windows.Forms.Label();
            this.labelTargetFrame = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.labelFlipsForSeed = new System.Windows.Forms.Label();
            this.labelElmForSeed = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBoxRPresent = new System.Windows.Forms.CheckBox();
            this.checkBoxEPresent = new System.Windows.Forms.CheckBox();
            this.checkBoxLPresent = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.labelRoamerRoutes = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.checkBoxDreamWorld = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pokedexIVCheckerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rdGenToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitSeedToTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jirachiGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pIDToIVsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.togamiCalcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pokespotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rubyEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sapphireEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emeraldEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fireRedEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leafGreenEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thGenToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iVsToPIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSIDFromChainedShiniesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seedToTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findSeedByIVsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSeedByStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSeedByIVRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleSeedGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.diamondEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pearlEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.platinumEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heartGoldEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soulSilverEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thGenToolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.findDSParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unovaLinkParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seedToTimeCGearSeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleSeedGeneratorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.adjacentSeedToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entralinkSeedToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.blackEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteEncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.black2EncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.white2EncounterTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenGrottoEncounterTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberOfCPUCoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxCPUCount = new System.Windows.Forms.ToolStripComboBox();
            this.pIDDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolTipsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.researcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日本語ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deutschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.españolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.françaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.한국어ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxDittoParent = new System.Windows.Forms.CheckBox();
            this.checkBoxSynchOnly = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.labelCalcWarning = new System.Windows.Forms.Label();
            this.checkBoxRoamerReleased = new System.Windows.Forms.CheckBox();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.label41 = new System.Windows.Forms.Label();
            this.contextMenuStripTimeFinder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FifthGenerationTimeFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FourthGenerationTimeFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThirdGenerationTimeFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameCubeTimeFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipDataGrid = new System.Windows.Forms.ToolTip(this.components);
            this.buttonLead = new System.Windows.Forms.Button();
            this.checkBoxBW2 = new System.Windows.Forms.CheckBox();
            this.cbNidoBeat = new System.Windows.Forms.CheckBox();
            this.cbShinyCharm = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxSID = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxID = new RNGReporter.Controls.MaskedTextBox2();
            this.ivFilters = new RNGReporter.Controls.IVFilters();
            this.buttonAnySlot = new RNGReporter.GlassButton();
            this.buttonRoamerMap = new RNGReporter.GlassButton();
            this.comboBoxGender = new RNGReporter.GlassComboBox();
            this.buttonCalcInitialFrame = new RNGReporter.GlassButton();
            this.buttonDSParameters = new RNGReporter.GlassButton();
            this.comboBoxEncounterType = new RNGReporter.GlassComboBox();
            this.maskedTextBoxERoute = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxRRoute = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxLRoute = new RNGReporter.Controls.MaskedTextBox2();
            this.comboBoxSynchNatures = new RNGReporter.GlassComboBox();
            this.comboBoxEncounterSlot = new RNGReporter.Controls.CheckBoxComboBox();
            this.comboBoxAbility = new RNGReporter.GlassComboBox();
            this.textBoxSeed = new RNGReporter.Controls.MaskedTextBox2();
            this.buttonFindTime = new RNGReporter.GlassButton();
            this.comboBoxNature = new RNGReporter.Controls.CheckBoxComboBox();
            this.buttonAnyNature = new RNGReporter.GlassButton();
            this.buttonGenerate = new RNGReporter.GlassButton();
            this.dataGridViewValues = new RNGReporter.DoubleBufferedDataGridView();
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Elm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chatot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EncounterSlot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaveSpot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shiny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dream = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Coin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Atk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Def = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HiddenPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HiddenPowerPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Characteristic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaleOnlySpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f125 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f75 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PossibleShakingSpot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Synchable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maskedTextBoxStartingFrame = new RNGReporter.Controls.MaskedTextBox2();
            this.maskedTextBoxMaxFrames = new RNGReporter.Controls.MaskedTextBox2();
            this.comboBoxMethod = new RNGReporter.GlassComboBox();
            this.checkBoxMemoryLink = new System.Windows.Forms.CheckBox();
            this.contextMenuStripGrid.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripTimeFinder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(356, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "SID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(277, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(52, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Method";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBoxShinyOnly
            // 
            this.checkBoxShinyOnly.AutoSize = true;
            this.checkBoxShinyOnly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShinyOnly.Location = new System.Drawing.Point(299, 220);
            this.checkBoxShinyOnly.Name = "checkBoxShinyOnly";
            this.checkBoxShinyOnly.Size = new System.Drawing.Size(76, 17);
            this.checkBoxShinyOnly.TabIndex = 24;
            this.checkBoxShinyOnly.Text = "Shiny Only";
            this.checkBoxShinyOnly.UseVisualStyleBackColor = true;
            // 
            // contextMenuStripGrid
            // 
            this.contextMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockFrameToolStripMenuItem,
            this.jumpFrameToolStripMenuItem,
            this.toolStripMenuItem1,
            this.centerTo1SecondToolStripMenuItem,
            this.centerTo2SecondsToolStripMenuItem,
            this.centerTo3SecondsToolStripMenuItem,
            this.centerTo5SecondsToolStripMenuItem,
            this.centerTo10SecondsToolStripMenuItem,
            this.centerTp1MinuteToolStripMenuItem,
            this.removeCenteringToolStripMenuItem,
            this.toolStripMenuItem2,
            this.calculatePoketechTapsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.searchCoinFlipsToolStripMenuItem,
            this.searchNaturesToolStripMenuItem,
            this.searchElmToolStripMenuItem,
            this.displayParentsInSearchToolStripMenuItem,
            this.resetParentsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.outputResultsToTXTToolStripMenuItem});
            this.contextMenuStripGrid.Name = "contextMenuStripGrid";
            this.contextMenuStripGrid.Size = new System.Drawing.Size(335, 380);
            this.contextMenuStripGrid.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripGrid_Opening);
            // 
            // lockFrameToolStripMenuItem
            // 
            this.lockFrameToolStripMenuItem.Name = "lockFrameToolStripMenuItem";
            this.lockFrameToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.lockFrameToolStripMenuItem.Text = "Set as Target Frame";
            this.lockFrameToolStripMenuItem.Click += new System.EventHandler(this.lockFrameToolStripMenuItem_Click);
            // 
            // jumpFrameToolStripMenuItem
            // 
            this.jumpFrameToolStripMenuItem.Name = "jumpFrameToolStripMenuItem";
            this.jumpFrameToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.jumpFrameToolStripMenuItem.Text = "Jump to Target Frame";
            this.jumpFrameToolStripMenuItem.Click += new System.EventHandler(this.jumpFrameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(331, 6);
            // 
            // centerTo1SecondToolStripMenuItem
            // 
            this.centerTo1SecondToolStripMenuItem.Name = "centerTo1SecondToolStripMenuItem";
            this.centerTo1SecondToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTo1SecondToolStripMenuItem.Text = "Center to +/- 1 Second and Set as Target Frame";
            this.centerTo1SecondToolStripMenuItem.Click += new System.EventHandler(this.centerTo1SecondToolStripMenuItem_Click);
            // 
            // centerTo2SecondsToolStripMenuItem
            // 
            this.centerTo2SecondsToolStripMenuItem.Name = "centerTo2SecondsToolStripMenuItem";
            this.centerTo2SecondsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTo2SecondsToolStripMenuItem.Text = "Center to +/- 2 Seconds and Set as Target Frame";
            this.centerTo2SecondsToolStripMenuItem.Click += new System.EventHandler(this.centerTo2SecondsToolStripMenuItem_Click);
            // 
            // centerTo3SecondsToolStripMenuItem
            // 
            this.centerTo3SecondsToolStripMenuItem.Name = "centerTo3SecondsToolStripMenuItem";
            this.centerTo3SecondsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTo3SecondsToolStripMenuItem.Text = "Center to +/- 3 Seconds and Set as Target Frame";
            this.centerTo3SecondsToolStripMenuItem.Click += new System.EventHandler(this.centerTo3SecondsToolStripMenuItem_Click);
            // 
            // centerTo5SecondsToolStripMenuItem
            // 
            this.centerTo5SecondsToolStripMenuItem.Name = "centerTo5SecondsToolStripMenuItem";
            this.centerTo5SecondsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTo5SecondsToolStripMenuItem.Text = "Center to +/- 5 Seconds and Set as Target Frame";
            this.centerTo5SecondsToolStripMenuItem.Click += new System.EventHandler(this.centerTo5SecondsToolStripMenuItem_Click);
            // 
            // centerTo10SecondsToolStripMenuItem
            // 
            this.centerTo10SecondsToolStripMenuItem.Name = "centerTo10SecondsToolStripMenuItem";
            this.centerTo10SecondsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTo10SecondsToolStripMenuItem.Text = "Center to +/- 10 Seconds and Set as Target Frame";
            this.centerTo10SecondsToolStripMenuItem.Click += new System.EventHandler(this.centerTo10SecondsToolStripMenuItem_Click);
            // 
            // centerTp1MinuteToolStripMenuItem
            // 
            this.centerTp1MinuteToolStripMenuItem.Name = "centerTp1MinuteToolStripMenuItem";
            this.centerTp1MinuteToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.centerTp1MinuteToolStripMenuItem.Text = "Center to +/- 1 Minute and Set as Target Frame";
            this.centerTp1MinuteToolStripMenuItem.Click += new System.EventHandler(this.centerTp1MinuteToolStripMenuItem_Click);
            // 
            // removeCenteringToolStripMenuItem
            // 
            this.removeCenteringToolStripMenuItem.Name = "removeCenteringToolStripMenuItem";
            this.removeCenteringToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.removeCenteringToolStripMenuItem.Text = "Remove Centering";
            this.removeCenteringToolStripMenuItem.Click += new System.EventHandler(this.removeCenteringToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(331, 6);
            // 
            // calculatePoketechTapsToolStripMenuItem
            // 
            this.calculatePoketechTapsToolStripMenuItem.Name = "calculatePoketechTapsToolStripMenuItem";
            this.calculatePoketechTapsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.calculatePoketechTapsToolStripMenuItem.Text = "Calculate Poketech Taps ...";
            this.calculatePoketechTapsToolStripMenuItem.Click += new System.EventHandler(this.calculatePoketechTapsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(331, 6);
            // 
            // searchCoinFlipsToolStripMenuItem
            // 
            this.searchCoinFlipsToolStripMenuItem.Name = "searchCoinFlipsToolStripMenuItem";
            this.searchCoinFlipsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.searchCoinFlipsToolStripMenuItem.Text = "Search Coin Flips ...";
            this.searchCoinFlipsToolStripMenuItem.Click += new System.EventHandler(this.searchCoinFlipsToolStripMenuItem_Click);
            // 
            // searchNaturesToolStripMenuItem
            // 
            this.searchNaturesToolStripMenuItem.Name = "searchNaturesToolStripMenuItem";
            this.searchNaturesToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.searchNaturesToolStripMenuItem.Text = "Search Natures ...";
            this.searchNaturesToolStripMenuItem.Click += new System.EventHandler(this.searchNaturesToolStripMenuItem_Click);
            // 
            // searchElmToolStripMenuItem
            // 
            this.searchElmToolStripMenuItem.Name = "searchElmToolStripMenuItem";
            this.searchElmToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.searchElmToolStripMenuItem.Text = "Search Elm Responses ...";
            this.searchElmToolStripMenuItem.Click += new System.EventHandler(this.searchElmToolStripMenuItem_Click);
            // 
            // displayParentsInSearchToolStripMenuItem
            // 
            this.displayParentsInSearchToolStripMenuItem.Name = "displayParentsInSearchToolStripMenuItem";
            this.displayParentsInSearchToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.displayParentsInSearchToolStripMenuItem.Text = "Display Parents in Search...";
            this.displayParentsInSearchToolStripMenuItem.Click += new System.EventHandler(this.displayParentsInSearchToolStripMenuItem_Click);
            // 
            // resetParentsToolStripMenuItem
            // 
            this.resetParentsToolStripMenuItem.Name = "resetParentsToolStripMenuItem";
            this.resetParentsToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.resetParentsToolStripMenuItem.Text = "Reset Parents";
            this.resetParentsToolStripMenuItem.Visible = false;
            this.resetParentsToolStripMenuItem.Click += new System.EventHandler(this.resetParentsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(331, 6);
            // 
            // outputResultsToTXTToolStripMenuItem
            // 
            this.outputResultsToTXTToolStripMenuItem.Name = "outputResultsToTXTToolStripMenuItem";
            this.outputResultsToTXTToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.outputResultsToTXTToolStripMenuItem.Text = "Output Results to TXT ...";
            this.outputResultsToTXTToolStripMenuItem.Click += new System.EventHandler(this.outputResultsToTXTToolStripMenuItem_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(30, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Max Results";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(20, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Starting Frame";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(35, 168);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Seed (Hex)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(619, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 46;
            this.label13.Text = "Nature";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(12, 467);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Target Frame:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelTargetFrame
            // 
            this.labelTargetFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTargetFrame.AutoSize = true;
            this.labelTargetFrame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTargetFrame.Location = new System.Drawing.Point(89, 467);
            this.labelTargetFrame.Name = "labelTargetFrame";
            this.labelTargetFrame.Size = new System.Drawing.Size(33, 13);
            this.labelTargetFrame.TabIndex = 37;
            this.labelTargetFrame.Text = "None";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(624, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 49;
            this.label16.Text = "Ability";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(130, 467);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 13);
            this.label17.TabIndex = 74;
            this.label17.Text = "Coin Flips for Seed:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelFlipsForSeed
            // 
            this.labelFlipsForSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFlipsForSeed.AutoSize = true;
            this.labelFlipsForSeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFlipsForSeed.Location = new System.Drawing.Point(227, 467);
            this.labelFlipsForSeed.Name = "labelFlipsForSeed";
            this.labelFlipsForSeed.Size = new System.Drawing.Size(0, 13);
            this.labelFlipsForSeed.TabIndex = 75;
            this.labelFlipsForSeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelElmForSeed
            // 
            this.labelElmForSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelElmForSeed.AutoSize = true;
            this.labelElmForSeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelElmForSeed.Location = new System.Drawing.Point(524, 467);
            this.labelElmForSeed.Name = "labelElmForSeed";
            this.labelElmForSeed.Size = new System.Drawing.Size(0, 13);
            this.labelElmForSeed.TabIndex = 77;
            this.labelElmForSeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(398, 467);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(126, 13);
            this.label19.TabIndex = 76;
            this.label19.Text = "Elm Responses for Seed:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBoxRPresent
            // 
            this.checkBoxRPresent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxRPresent.AutoSize = true;
            this.checkBoxRPresent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxRPresent.Location = new System.Drawing.Point(184, 491);
            this.checkBoxRPresent.Name = "checkBoxRPresent";
            this.checkBoxRPresent.Size = new System.Drawing.Size(34, 17);
            this.checkBoxRPresent.TabIndex = 39;
            this.checkBoxRPresent.Text = "R";
            this.checkBoxRPresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxEPresent
            // 
            this.checkBoxEPresent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEPresent.AutoSize = true;
            this.checkBoxEPresent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxEPresent.Location = new System.Drawing.Point(249, 491);
            this.checkBoxEPresent.Name = "checkBoxEPresent";
            this.checkBoxEPresent.Size = new System.Drawing.Size(33, 17);
            this.checkBoxEPresent.TabIndex = 41;
            this.checkBoxEPresent.Text = "E";
            this.checkBoxEPresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxLPresent
            // 
            this.checkBoxLPresent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxLPresent.AutoSize = true;
            this.checkBoxLPresent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxLPresent.Location = new System.Drawing.Point(314, 491);
            this.checkBoxLPresent.Name = "checkBoxLPresent";
            this.checkBoxLPresent.Size = new System.Drawing.Size(32, 17);
            this.checkBoxLPresent.TabIndex = 43;
            this.checkBoxLPresent.Text = "L";
            this.checkBoxLPresent.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(375, 492);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(149, 13);
            this.label18.TabIndex = 85;
            this.label18.Text = "Roaming Pokemon Locations:";
            // 
            // labelRoamerRoutes
            // 
            this.labelRoamerRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRoamerRoutes.AutoSize = true;
            this.labelRoamerRoutes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRoamerRoutes.Location = new System.Drawing.Point(524, 492);
            this.labelRoamerRoutes.Name = "labelRoamerRoutes";
            this.labelRoamerRoutes.Size = new System.Drawing.Size(0, 13);
            this.labelRoamerRoutes.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(581, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 13);
            this.label20.TabIndex = 57;
            this.label20.Text = "Encounter Slot";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxDreamWorld
            // 
            this.checkBoxDreamWorld.AutoSize = true;
            this.checkBoxDreamWorld.Enabled = false;
            this.checkBoxDreamWorld.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDreamWorld.Location = new System.Drawing.Point(422, 236);
            this.checkBoxDreamWorld.Name = "checkBoxDreamWorld";
            this.checkBoxDreamWorld.Size = new System.Drawing.Size(134, 17);
            this.checkBoxDreamWorld.TabIndex = 27;
            this.checkBoxDreamWorld.Text = "Dream World Egg Only";
            this.checkBoxDreamWorld.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pokedexIVCheckerToolStripMenuItem,
            this.rdGenToolsToolStripMenuItem,
            this.thGenToolsToolStripMenuItem,
            this.thGenToolsToolStripMenuItem1,
            this.donationToolStripMenuItem,
            this.performanceOptionsToolStripMenuItem,
            this.researcherToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.profilesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(916, 29);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pokedexIVCheckerToolStripMenuItem
            // 
            this.pokedexIVCheckerToolStripMenuItem.Name = "pokedexIVCheckerToolStripMenuItem";
            this.pokedexIVCheckerToolStripMenuItem.Size = new System.Drawing.Size(124, 25);
            this.pokedexIVCheckerToolStripMenuItem.Text = "Pokédex-IV Checker";
            this.pokedexIVCheckerToolStripMenuItem.Click += new System.EventHandler(this.pokedexIVCheckerToolStripMenuItem_Click);
            // 
            // rdGenToolsToolStripMenuItem
            // 
            this.rdGenToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2,
            this.bitSeedToTimeToolStripMenuItem,
            this.jirachiGenerationToolStripMenuItem,
            this.pIDToIVsToolStripMenuItem,
            this.togamiCalcToolStripMenuItem,
            this.pokespotToolStripMenuItem,
            this.toolStripSeparator4,
            this.rubyEncounterTableToolStripMenuItem,
            this.sapphireEncounterTableToolStripMenuItem,
            this.emeraldEncounterTableToolStripMenuItem,
            this.fireRedEncounterTableToolStripMenuItem,
            this.leafGreenEncounterTableToolStripMenuItem});
            this.rdGenToolsToolStripMenuItem.Name = "rdGenToolsToolStripMenuItem";
            this.rdGenToolsToolStripMenuItem.Size = new System.Drawing.Size(91, 25);
            this.rdGenToolsToolStripMenuItem.Text = "3rd Gen Tools";
            // 
            // tIDSIDManipulationPandorasBoxToolStripMenuItem2
            // 
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2.Name = "tIDSIDManipulationPandorasBoxToolStripMenuItem2";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2.Size = new System.Drawing.Size(283, 22);
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2.Text = "TID\\SID Manipulation (\"Pandora\'s Box\")";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem2.Click += new System.EventHandler(this.tIDSIDManipulationPandorasBoxToolStripMenuItem2_Click);
            // 
            // bitSeedToTimeToolStripMenuItem
            // 
            this.bitSeedToTimeToolStripMenuItem.Name = "bitSeedToTimeToolStripMenuItem";
            this.bitSeedToTimeToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.bitSeedToTimeToolStripMenuItem.Text = "16-Bit Seed to Time";
            this.bitSeedToTimeToolStripMenuItem.Click += new System.EventHandler(this.bitSeedToTimeToolStripMenuItem_Click);
            // 
            // jirachiGenerationToolStripMenuItem
            // 
            this.jirachiGenerationToolStripMenuItem.Name = "jirachiGenerationToolStripMenuItem";
            this.jirachiGenerationToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.jirachiGenerationToolStripMenuItem.Text = "Jirachi Generation";
            this.jirachiGenerationToolStripMenuItem.Click += new System.EventHandler(this.jirachiGenerationToolStripMenuItem_Click);
            // 
            // pIDToIVsToolStripMenuItem
            // 
            this.pIDToIVsToolStripMenuItem.Name = "pIDToIVsToolStripMenuItem";
            this.pIDToIVsToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.pIDToIVsToolStripMenuItem.Text = "PID to IVs";
            this.pIDToIVsToolStripMenuItem.Click += new System.EventHandler(this.pIDToIVsToolStripMenuItem_Click);
            // 
            // togamiCalcToolStripMenuItem
            // 
            this.togamiCalcToolStripMenuItem.Name = "togamiCalcToolStripMenuItem";
            this.togamiCalcToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.togamiCalcToolStripMenuItem.Text = "TogamiCalc";
            this.togamiCalcToolStripMenuItem.Click += new System.EventHandler(this.togamiCalcToolStripMenuItem_Click);
            // 
            // pokespotToolStripMenuItem
            // 
            this.pokespotToolStripMenuItem.Name = "pokespotToolStripMenuItem";
            this.pokespotToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.pokespotToolStripMenuItem.Text = "Pokespot";
            this.pokespotToolStripMenuItem.Click += new System.EventHandler(this.pokespotToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(280, 6);
            // 
            // rubyEncounterTableToolStripMenuItem
            // 
            this.rubyEncounterTableToolStripMenuItem.Name = "rubyEncounterTableToolStripMenuItem";
            this.rubyEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.rubyEncounterTableToolStripMenuItem.Text = "Ruby Encounter Table";
            this.rubyEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.rubyEncounterTableToolStripMenuItem_Click);
            // 
            // sapphireEncounterTableToolStripMenuItem
            // 
            this.sapphireEncounterTableToolStripMenuItem.Name = "sapphireEncounterTableToolStripMenuItem";
            this.sapphireEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.sapphireEncounterTableToolStripMenuItem.Text = "Sapphire Encounter Table";
            this.sapphireEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.sapphireEncounterTableToolStripMenuItem_Click);
            // 
            // emeraldEncounterTableToolStripMenuItem
            // 
            this.emeraldEncounterTableToolStripMenuItem.Name = "emeraldEncounterTableToolStripMenuItem";
            this.emeraldEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.emeraldEncounterTableToolStripMenuItem.Text = "Emerald Encounter Table";
            this.emeraldEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.emeraldEncounterTableToolStripMenuItem_Click);
            // 
            // fireRedEncounterTableToolStripMenuItem
            // 
            this.fireRedEncounterTableToolStripMenuItem.Name = "fireRedEncounterTableToolStripMenuItem";
            this.fireRedEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.fireRedEncounterTableToolStripMenuItem.Text = "Fire Red Encounter Table";
            this.fireRedEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.fireRedEncounterTableToolStripMenuItem_Click);
            // 
            // leafGreenEncounterTableToolStripMenuItem
            // 
            this.leafGreenEncounterTableToolStripMenuItem.Name = "leafGreenEncounterTableToolStripMenuItem";
            this.leafGreenEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.leafGreenEncounterTableToolStripMenuItem.Text = "Leaf Green Encounter Table";
            this.leafGreenEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.leafGreenEncounterTableToolStripMenuItem_Click);
            // 
            // thGenToolsToolStripMenuItem
            // 
            this.thGenToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iVsToPIDToolStripMenuItem,
            this.findSIDFromChainedShiniesToolStripMenuItem,
            this.seedToTimeToolStripMenuItem,
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem,
            this.toolStripSeparator1,
            this.findSeedByIVsToolStripMenuItem,
            this.findSeedByStatsToolStripMenuItem,
            this.findSeedByIVRangeToolStripMenuItem,
            this.simpleSeedGeneratorToolStripMenuItem,
            this.toolStripSeparator3,
            this.diamondEncounterTableToolStripMenuItem,
            this.pearlEncounterTableToolStripMenuItem,
            this.platinumEncounterTableToolStripMenuItem,
            this.heartGoldEncounterTableToolStripMenuItem,
            this.soulSilverEncounterTableToolStripMenuItem});
            this.thGenToolsToolStripMenuItem.Name = "thGenToolsToolStripMenuItem";
            this.thGenToolsToolStripMenuItem.Size = new System.Drawing.Size(91, 25);
            this.thGenToolsToolStripMenuItem.Text = "4th Gen Tools";
            // 
            // iVsToPIDToolStripMenuItem
            // 
            this.iVsToPIDToolStripMenuItem.Name = "iVsToPIDToolStripMenuItem";
            this.iVsToPIDToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.iVsToPIDToolStripMenuItem.Text = "Calculate PID from IVs";
            this.iVsToPIDToolStripMenuItem.Click += new System.EventHandler(this.iVsToPIDToolStripMenuItem_Click);
            // 
            // findSIDFromChainedShiniesToolStripMenuItem
            // 
            this.findSIDFromChainedShiniesToolStripMenuItem.Name = "findSIDFromChainedShiniesToolStripMenuItem";
            this.findSIDFromChainedShiniesToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.findSIDFromChainedShiniesToolStripMenuItem.Text = "Find SID from Chained Shinies";
            this.findSIDFromChainedShiniesToolStripMenuItem.Click += new System.EventHandler(this.findSIDFromChainedShiniesToolStripMenuItem_Click);
            // 
            // seedToTimeToolStripMenuItem
            // 
            this.seedToTimeToolStripMenuItem.Name = "seedToTimeToolStripMenuItem";
            this.seedToTimeToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.seedToTimeToolStripMenuItem.Text = "Seed to Time";
            this.seedToTimeToolStripMenuItem.Click += new System.EventHandler(this.seedToTimeToolStripMenuItem_Click);
            // 
            // tIDSIDManipulationPandorasBoxToolStripMenuItem
            // 
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem.Name = "tIDSIDManipulationPandorasBoxToolStripMenuItem";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem.Text = "TID\\SID Manipulation (\"Pandora\'s Box\")";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem.Click += new System.EventHandler(this.tIDSIDManipulationPandorasBoxToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(280, 6);
            // 
            // findSeedByIVsToolStripMenuItem
            // 
            this.findSeedByIVsToolStripMenuItem.Name = "findSeedByIVsToolStripMenuItem";
            this.findSeedByIVsToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.findSeedByIVsToolStripMenuItem.Text = "Find Seed by IVs";
            this.findSeedByIVsToolStripMenuItem.Click += new System.EventHandler(this.findSeedByIVsToolStripMenuItem_Click);
            // 
            // findSeedByStatsToolStripMenuItem
            // 
            this.findSeedByStatsToolStripMenuItem.Name = "findSeedByStatsToolStripMenuItem";
            this.findSeedByStatsToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.findSeedByStatsToolStripMenuItem.Text = "Find Seed by Stats";
            this.findSeedByStatsToolStripMenuItem.Click += new System.EventHandler(this.findSeedByStatsToolStripMenuItem_Click);
            // 
            // findSeedByIVRangeToolStripMenuItem
            // 
            this.findSeedByIVRangeToolStripMenuItem.Name = "findSeedByIVRangeToolStripMenuItem";
            this.findSeedByIVRangeToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.findSeedByIVRangeToolStripMenuItem.Text = "Find Seed by IV Range";
            this.findSeedByIVRangeToolStripMenuItem.Click += new System.EventHandler(this.findSeedByIVRangeToolStripMenuItem_Click);
            // 
            // simpleSeedGeneratorToolStripMenuItem
            // 
            this.simpleSeedGeneratorToolStripMenuItem.Name = "simpleSeedGeneratorToolStripMenuItem";
            this.simpleSeedGeneratorToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.simpleSeedGeneratorToolStripMenuItem.Text = "Simple Seed Generator";
            this.simpleSeedGeneratorToolStripMenuItem.Click += new System.EventHandler(this.simpleSeedGeneratorToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(280, 6);
            // 
            // diamondEncounterTableToolStripMenuItem
            // 
            this.diamondEncounterTableToolStripMenuItem.Name = "diamondEncounterTableToolStripMenuItem";
            this.diamondEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.diamondEncounterTableToolStripMenuItem.Text = "Diamond Encounter Table";
            this.diamondEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.diamondEncounterTableToolStripMenuItem_Click);
            // 
            // pearlEncounterTableToolStripMenuItem
            // 
            this.pearlEncounterTableToolStripMenuItem.Name = "pearlEncounterTableToolStripMenuItem";
            this.pearlEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.pearlEncounterTableToolStripMenuItem.Text = "Pearl Encounter Table";
            this.pearlEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.pearlEncounterTableToolStripMenuItem_Click);
            // 
            // platinumEncounterTableToolStripMenuItem
            // 
            this.platinumEncounterTableToolStripMenuItem.Name = "platinumEncounterTableToolStripMenuItem";
            this.platinumEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.platinumEncounterTableToolStripMenuItem.Text = "Platinum Encounter Table";
            this.platinumEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.platinumEncounterTableToolStripMenuItem_Click);
            // 
            // heartGoldEncounterTableToolStripMenuItem
            // 
            this.heartGoldEncounterTableToolStripMenuItem.Name = "heartGoldEncounterTableToolStripMenuItem";
            this.heartGoldEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.heartGoldEncounterTableToolStripMenuItem.Text = "Heart Gold Encounter Table";
            this.heartGoldEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.heartGoldEncounterTableToolStripMenuItem_Click);
            // 
            // soulSilverEncounterTableToolStripMenuItem
            // 
            this.soulSilverEncounterTableToolStripMenuItem.Name = "soulSilverEncounterTableToolStripMenuItem";
            this.soulSilverEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.soulSilverEncounterTableToolStripMenuItem.Text = "Soul Silver Encounter Table";
            this.soulSilverEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.soulSilverEncounterTableToolStripMenuItem_Click);
            // 
            // thGenToolsToolStripMenuItem1
            // 
            this.thGenToolsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findDSParametersToolStripMenuItem,
            this.unovaLinkParametersToolStripMenuItem,
            this.seedToTimeCGearSeedsToolStripMenuItem,
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1,
            this.simpleSeedGeneratorToolStripMenuItem1,
            this.adjacentSeedToolToolStripMenuItem,
            this.entralinkSeedToolToolStripMenuItem,
            this.toolStripSeparator2,
            this.blackEncounterTableToolStripMenuItem,
            this.whiteEncounterTableToolStripMenuItem,
            this.black2EncounterTableToolStripMenuItem,
            this.white2EncounterTableToolStripMenuItem,
            this.hiddenGrottoEncounterTablesToolStripMenuItem});
            this.thGenToolsToolStripMenuItem1.Name = "thGenToolsToolStripMenuItem1";
            this.thGenToolsToolStripMenuItem1.Size = new System.Drawing.Size(91, 25);
            this.thGenToolsToolStripMenuItem1.Text = "5th Gen Tools";
            // 
            // findDSParametersToolStripMenuItem
            // 
            this.findDSParametersToolStripMenuItem.Name = "findDSParametersToolStripMenuItem";
            this.findDSParametersToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.findDSParametersToolStripMenuItem.Text = "Find DS Parameters (Standard Seeds)";
            this.findDSParametersToolStripMenuItem.Click += new System.EventHandler(this.findDSParametersToolStripMenuItem_Click);
            // 
            // unovaLinkParametersToolStripMenuItem
            // 
            this.unovaLinkParametersToolStripMenuItem.Name = "unovaLinkParametersToolStripMenuItem";
            this.unovaLinkParametersToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.unovaLinkParametersToolStripMenuItem.Text = "Unova Link Parameters";
            this.unovaLinkParametersToolStripMenuItem.Click += new System.EventHandler(this.unovaLinkParametersToolStripMenuItem_Click);
            // 
            // seedToTimeCGearSeedsToolStripMenuItem
            // 
            this.seedToTimeCGearSeedsToolStripMenuItem.Name = "seedToTimeCGearSeedsToolStripMenuItem";
            this.seedToTimeCGearSeedsToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.seedToTimeCGearSeedsToolStripMenuItem.Text = "Seed to Time (C-Gear Seeds)";
            this.seedToTimeCGearSeedsToolStripMenuItem.Click += new System.EventHandler(this.seedToTimeCGearSeedsToolStripMenuItem_Click);
            // 
            // tIDSIDManipulationPandorasBoxToolStripMenuItem1
            // 
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1.Name = "tIDSIDManipulationPandorasBoxToolStripMenuItem1";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1.Size = new System.Drawing.Size(283, 22);
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1.Text = "TID\\SID Manipulation (\"Pandora\'s Box\")";
            this.tIDSIDManipulationPandorasBoxToolStripMenuItem1.Click += new System.EventHandler(this.tIDSIDManipulationPandorasBoxToolStripMenuItem1_Click);
            // 
            // simpleSeedGeneratorToolStripMenuItem1
            // 
            this.simpleSeedGeneratorToolStripMenuItem1.Name = "simpleSeedGeneratorToolStripMenuItem1";
            this.simpleSeedGeneratorToolStripMenuItem1.Size = new System.Drawing.Size(283, 22);
            this.simpleSeedGeneratorToolStripMenuItem1.Text = "Simple Seed Generator";
            this.simpleSeedGeneratorToolStripMenuItem1.Click += new System.EventHandler(this.simpleSeedGeneratorToolStripMenuItem1_Click);
            // 
            // adjacentSeedToolToolStripMenuItem
            // 
            this.adjacentSeedToolToolStripMenuItem.Name = "adjacentSeedToolToolStripMenuItem";
            this.adjacentSeedToolToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.adjacentSeedToolToolStripMenuItem.Text = "Adjacent Seed Tool";
            this.adjacentSeedToolToolStripMenuItem.Click += new System.EventHandler(this.adjacentSeedToolToolStripMenuItem_Click);
            // 
            // entralinkSeedToolToolStripMenuItem
            // 
            this.entralinkSeedToolToolStripMenuItem.Name = "entralinkSeedToolToolStripMenuItem";
            this.entralinkSeedToolToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.entralinkSeedToolToolStripMenuItem.Text = "Entralink Seed Search";
            this.entralinkSeedToolToolStripMenuItem.Click += new System.EventHandler(this.entralinkSeedSearchToolToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(280, 6);
            this.toolStripSeparator2.Click += new System.EventHandler(this.toolStripSeparator2_Click);
            // 
            // blackEncounterTableToolStripMenuItem
            // 
            this.blackEncounterTableToolStripMenuItem.Name = "blackEncounterTableToolStripMenuItem";
            this.blackEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.blackEncounterTableToolStripMenuItem.Text = "Black Encounter Table";
            this.blackEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.blackEncounterTableToolStripMenuItem_Click);
            // 
            // whiteEncounterTableToolStripMenuItem
            // 
            this.whiteEncounterTableToolStripMenuItem.Name = "whiteEncounterTableToolStripMenuItem";
            this.whiteEncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.whiteEncounterTableToolStripMenuItem.Text = "White Encounter Table";
            this.whiteEncounterTableToolStripMenuItem.Click += new System.EventHandler(this.whiteEncounterTableToolStripMenuItem_Click);
            // 
            // black2EncounterTableToolStripMenuItem
            // 
            this.black2EncounterTableToolStripMenuItem.Name = "black2EncounterTableToolStripMenuItem";
            this.black2EncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.black2EncounterTableToolStripMenuItem.Text = "Black 2 Encounter Table";
            this.black2EncounterTableToolStripMenuItem.Click += new System.EventHandler(this.black2EncounterTableToolStripMenuItem_Click);
            // 
            // white2EncounterTableToolStripMenuItem
            // 
            this.white2EncounterTableToolStripMenuItem.Name = "white2EncounterTableToolStripMenuItem";
            this.white2EncounterTableToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.white2EncounterTableToolStripMenuItem.Text = "White 2 Encounter Table";
            this.white2EncounterTableToolStripMenuItem.Click += new System.EventHandler(this.white2EncounterTableToolStripMenuItem_Click);
            // 
            // hiddenGrottoEncounterTablesToolStripMenuItem
            // 
            this.hiddenGrottoEncounterTablesToolStripMenuItem.Name = "hiddenGrottoEncounterTablesToolStripMenuItem";
            this.hiddenGrottoEncounterTablesToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.hiddenGrottoEncounterTablesToolStripMenuItem.Text = "Hidden Grotto Encounter Tables";
            this.hiddenGrottoEncounterTablesToolStripMenuItem.Click += new System.EventHandler(this.hiddenGrottoEncounterTablesToolStripMenuItem_Click);
            // 
            // donationToolStripMenuItem
            // 
            this.donationToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.donationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("donationToolStripMenuItem.Image")));
            this.donationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.donationToolStripMenuItem.Name = "donationToolStripMenuItem";
            this.donationToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.donationToolStripMenuItem.Size = new System.Drawing.Size(78, 25);
            this.donationToolStripMenuItem.Click += new System.EventHandler(this.donationToolStripMenuItem_Click);
            // 
            // performanceOptionsToolStripMenuItem
            // 
            this.performanceOptionsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.performanceOptionsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.performanceOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numberOfCPUCoresToolStripMenuItem,
            this.pIDDisplayToolStripMenuItem,
            this.showToolTipsToolStripMenuItem});
            this.performanceOptionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.performanceOptionsToolStripMenuItem.Name = "performanceOptionsToolStripMenuItem";
            this.performanceOptionsToolStripMenuItem.Size = new System.Drawing.Size(61, 25);
            this.performanceOptionsToolStripMenuItem.Text = "Options";
            // 
            // numberOfCPUCoresToolStripMenuItem
            // 
            this.numberOfCPUCoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboBoxCPUCount});
            this.numberOfCPUCoresToolStripMenuItem.Name = "numberOfCPUCoresToolStripMenuItem";
            this.numberOfCPUCoresToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.numberOfCPUCoresToolStripMenuItem.Text = "Number of CPU Cores";
            // 
            // comboBoxCPUCount
            // 
            this.comboBoxCPUCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCPUCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxCPUCount.Name = "comboBoxCPUCount";
            this.comboBoxCPUCount.Size = new System.Drawing.Size(121, 23);
            this.comboBoxCPUCount.SelectedIndexChanged += new System.EventHandler(this.comboBoxCPUCount_SelectedIndexChanged);
            // 
            // pIDDisplayToolStripMenuItem
            // 
            this.pIDDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hexToolStripMenuItem,
            this.decimalToolStripMenuItem});
            this.pIDDisplayToolStripMenuItem.Name = "pIDDisplayToolStripMenuItem";
            this.pIDDisplayToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.pIDDisplayToolStripMenuItem.Text = "PID Display";
            // 
            // hexToolStripMenuItem
            // 
            this.hexToolStripMenuItem.Checked = true;
            this.hexToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hexToolStripMenuItem.Name = "hexToolStripMenuItem";
            this.hexToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.hexToolStripMenuItem.Text = "Hex";
            this.hexToolStripMenuItem.Click += new System.EventHandler(this.hexToolStripMenuItem_Click);
            // 
            // decimalToolStripMenuItem
            // 
            this.decimalToolStripMenuItem.Name = "decimalToolStripMenuItem";
            this.decimalToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.decimalToolStripMenuItem.Text = "Decimal";
            this.decimalToolStripMenuItem.Click += new System.EventHandler(this.decimalToolStripMenuItem_Click);
            // 
            // showToolTipsToolStripMenuItem
            // 
            this.showToolTipsToolStripMenuItem.CheckOnClick = true;
            this.showToolTipsToolStripMenuItem.Name = "showToolTipsToolStripMenuItem";
            this.showToolTipsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.showToolTipsToolStripMenuItem.Text = "Show ToolTips";
            this.showToolTipsToolStripMenuItem.Click += new System.EventHandler(this.showToolTipsToolStripMenuItem_Click);
            // 
            // researcherToolStripMenuItem
            // 
            this.researcherToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.researcherToolStripMenuItem.Name = "researcherToolStripMenuItem";
            this.researcherToolStripMenuItem.Size = new System.Drawing.Size(76, 25);
            this.researcherToolStripMenuItem.Text = "Researcher";
            this.researcherToolStripMenuItem.Click += new System.EventHandler(this.researcherToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.日本語ToolStripMenuItem,
            this.deutschToolStripMenuItem,
            this.españolToolStripMenuItem,
            this.françaisToolStripMenuItem,
            this.italianoToolStripMenuItem,
            this.한국어ToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 25);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckOnClick = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // 日本語ToolStripMenuItem
            // 
            this.日本語ToolStripMenuItem.CheckOnClick = true;
            this.日本語ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F);
            this.日本語ToolStripMenuItem.Name = "日本語ToolStripMenuItem";
            this.日本語ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.日本語ToolStripMenuItem.Text = "日本語";
            this.日本語ToolStripMenuItem.Click += new System.EventHandler(this.日本語ToolStripMenuItem_Click);
            // 
            // deutschToolStripMenuItem
            // 
            this.deutschToolStripMenuItem.CheckOnClick = true;
            this.deutschToolStripMenuItem.Name = "deutschToolStripMenuItem";
            this.deutschToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deutschToolStripMenuItem.Text = "Deutsch";
            this.deutschToolStripMenuItem.Click += new System.EventHandler(this.deutschToolStripMenuItem_Click);
            // 
            // españolToolStripMenuItem
            // 
            this.españolToolStripMenuItem.CheckOnClick = true;
            this.españolToolStripMenuItem.Name = "españolToolStripMenuItem";
            this.españolToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.españolToolStripMenuItem.Text = "Español";
            this.españolToolStripMenuItem.Click += new System.EventHandler(this.españolToolStripMenuItem_Click);
            // 
            // françaisToolStripMenuItem
            // 
            this.françaisToolStripMenuItem.CheckOnClick = true;
            this.françaisToolStripMenuItem.Name = "françaisToolStripMenuItem";
            this.françaisToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.françaisToolStripMenuItem.Text = "Français";
            this.françaisToolStripMenuItem.Click += new System.EventHandler(this.françaisToolStripMenuItem_Click);
            // 
            // italianoToolStripMenuItem
            // 
            this.italianoToolStripMenuItem.CheckOnClick = true;
            this.italianoToolStripMenuItem.Name = "italianoToolStripMenuItem";
            this.italianoToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.italianoToolStripMenuItem.Text = "Italiano";
            this.italianoToolStripMenuItem.Click += new System.EventHandler(this.italianoToolStripMenuItem_Click);
            // 
            // 한국어ToolStripMenuItem
            // 
            this.한국어ToolStripMenuItem.CheckOnClick = true;
            this.한국어ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 8.25F);
            this.한국어ToolStripMenuItem.Name = "한국어ToolStripMenuItem";
            this.한국어ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.한국어ToolStripMenuItem.Text = "한국어";
            this.한국어ToolStripMenuItem.Click += new System.EventHandler(this.한국어ToolStripMenuItem_Click);
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(58, 25);
            this.profilesToolStripMenuItem.Text = "Profiles";
            this.profilesToolStripMenuItem.Click += new System.EventHandler(this.profilesToolStripMenuItem_Click);
            // 
            // checkBoxDittoParent
            // 
            this.checkBoxDittoParent.AutoSize = true;
            this.checkBoxDittoParent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDittoParent.Location = new System.Drawing.Point(422, 220);
            this.checkBoxDittoParent.Name = "checkBoxDittoParent";
            this.checkBoxDittoParent.Size = new System.Drawing.Size(82, 17);
            this.checkBoxDittoParent.TabIndex = 26;
            this.checkBoxDittoParent.Text = "Ditto Parent";
            this.checkBoxDittoParent.UseVisualStyleBackColor = true;
            // 
            // checkBoxSynchOnly
            // 
            this.checkBoxSynchOnly.AutoSize = true;
            this.checkBoxSynchOnly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSynchOnly.Location = new System.Drawing.Point(299, 236);
            this.checkBoxSynchOnly.Name = "checkBoxSynchOnly";
            this.checkBoxSynchOnly.Size = new System.Drawing.Size(117, 17);
            this.checkBoxSynchOnly.TabIndex = 25;
            this.checkBoxSynchOnly.Text = "Synch Frames Only";
            this.checkBoxSynchOnly.UseVisualStyleBackColor = true;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label52.Location = new System.Drawing.Point(12, 64);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(83, 13);
            this.label52.TabIndex = 105;
            this.label52.Text = "Encounter Type";
            this.label52.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(662, 239);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(170, 12);
            this.label21.TabIndex = 108;
            this.label21.Text = "required for Black\\White Standard Seeds";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCalcWarning
            // 
            this.labelCalcWarning.AutoSize = true;
            this.labelCalcWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.labelCalcWarning.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCalcWarning.Location = new System.Drawing.Point(666, 188);
            this.labelCalcWarning.Name = "labelCalcWarning";
            this.labelCalcWarning.Size = new System.Drawing.Size(157, 24);
            this.labelCalcWarning.TabIndex = 110;
            this.labelCalcWarning.Text = "Note: frame prediction will not be\naccurate if there are NPCs in the area";
            this.labelCalcWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxRoamerReleased
            // 
            this.checkBoxRoamerReleased.AutoSize = true;
            this.checkBoxRoamerReleased.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxRoamerReleased.Location = new System.Drawing.Point(666, 170);
            this.checkBoxRoamerReleased.Name = "checkBoxRoamerReleased";
            this.checkBoxRoamerReleased.Size = new System.Drawing.Size(111, 17);
            this.checkBoxRoamerReleased.TabIndex = 35;
            this.checkBoxRoamerReleased.Text = "Roamer Released";
            this.checkBoxRoamerReleased.UseVisualStyleBackColor = true;
            this.checkBoxRoamerReleased.Visible = false;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem5.Text = "4";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label41.Location = new System.Drawing.Point(616, 119);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(42, 13);
            this.label41.TabIndex = 317;
            this.label41.Text = "Gender";
            // 
            // contextMenuStripTimeFinder
            // 
            this.contextMenuStripTimeFinder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FifthGenerationTimeFinderToolStripMenuItem,
            this.FourthGenerationTimeFinderToolStripMenuItem,
            this.ThirdGenerationTimeFinderToolStripMenuItem,
            this.GameCubeTimeFinderToolStripMenuItem});
            this.contextMenuStripTimeFinder.Name = "contextMenuStripTimeFinder";
            this.contextMenuStripTimeFinder.Size = new System.Drawing.Size(219, 92);
            // 
            // FifthGenerationTimeFinderToolStripMenuItem
            // 
            this.FifthGenerationTimeFinderToolStripMenuItem.Name = "FifthGenerationTimeFinderToolStripMenuItem";
            this.FifthGenerationTimeFinderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.FifthGenerationTimeFinderToolStripMenuItem.Text = "5th Generation Time Finder";
            this.FifthGenerationTimeFinderToolStripMenuItem.Click += new System.EventHandler(this.buttonFindTime5thGen_Click);
            // 
            // FourthGenerationTimeFinderToolStripMenuItem
            // 
            this.FourthGenerationTimeFinderToolStripMenuItem.Name = "FourthGenerationTimeFinderToolStripMenuItem";
            this.FourthGenerationTimeFinderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.FourthGenerationTimeFinderToolStripMenuItem.Text = "4th Generation Time Finder";
            this.FourthGenerationTimeFinderToolStripMenuItem.Click += new System.EventHandler(this.buttonFindTime4thGen_Click);
            // 
            // ThirdGenerationTimeFinderToolStripMenuItem
            // 
            this.ThirdGenerationTimeFinderToolStripMenuItem.Name = "ThirdGenerationTimeFinderToolStripMenuItem";
            this.ThirdGenerationTimeFinderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.ThirdGenerationTimeFinderToolStripMenuItem.Text = "3rd Generation Time Finder";
            this.ThirdGenerationTimeFinderToolStripMenuItem.Click += new System.EventHandler(this.buttonFindTime3rdGen_Click);
            // 
            // GameCubeTimeFinderToolStripMenuItem
            // 
            this.GameCubeTimeFinderToolStripMenuItem.Name = "GameCubeTimeFinderToolStripMenuItem";
            this.GameCubeTimeFinderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.GameCubeTimeFinderToolStripMenuItem.Text = "GameCube Time Finder";
            this.GameCubeTimeFinderToolStripMenuItem.Click += new System.EventHandler(this.buttonFindTimeGameCube_Click);
            // 
            // toolTipDataGrid
            // 
            this.toolTipDataGrid.AutoPopDelay = 6000;
            this.toolTipDataGrid.InitialDelay = 300;
            this.toolTipDataGrid.ReshowDelay = 100;
            this.toolTipDataGrid.ToolTipTitle = "Chatot Pitch";
            // 
            // buttonLead
            // 
            this.buttonLead.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLead.FlatAppearance.BorderSize = 0;
            this.buttonLead.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonLead.ForeColor = System.Drawing.Color.Black;
            this.buttonLead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLead.Location = new System.Drawing.Point(7, 86);
            this.buttonLead.Name = "buttonLead";
            this.buttonLead.Size = new System.Drawing.Size(87, 23);
            this.buttonLead.TabIndex = 3;
            this.buttonLead.UseVisualStyleBackColor = false;
            this.buttonLead.Click += new System.EventHandler(this.buttonLead_Click);
            // 
            // checkBoxBW2
            // 
            this.checkBoxBW2.AutoSize = true;
            this.checkBoxBW2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBW2.Location = new System.Drawing.Point(783, 170);
            this.checkBoxBW2.Name = "checkBoxBW2";
            this.checkBoxBW2.Size = new System.Drawing.Size(99, 17);
            this.checkBoxBW2.TabIndex = 319;
            this.checkBoxBW2.Text = "Black White 2?";
            this.checkBoxBW2.UseVisualStyleBackColor = true;
            this.checkBoxBW2.Visible = false;
            // 
            // cbNidoBeat
            // 
            this.cbNidoBeat.AutoSize = true;
            this.cbNidoBeat.Location = new System.Drawing.Point(510, 220);
            this.cbNidoBeat.Name = "cbNidoBeat";
            this.cbNidoBeat.Size = new System.Drawing.Size(104, 17);
            this.cbNidoBeat.TabIndex = 330;
            this.cbNidoBeat.Text = "Nidoran/Volbeat";
            this.cbNidoBeat.UseVisualStyleBackColor = true;
            // 
            // cbShinyCharm
            // 
            this.cbShinyCharm.AutoSize = true;
            this.cbShinyCharm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbShinyCharm.Location = new System.Drawing.Point(562, 236);
            this.cbShinyCharm.Name = "cbShinyCharm";
            this.cbShinyCharm.Size = new System.Drawing.Size(85, 17);
            this.cbShinyCharm.TabIndex = 331;
            this.cbShinyCharm.Text = "Shiny Charm";
            this.cbShinyCharm.UseVisualStyleBackColor = true;
            // 
            // maskedTextBoxSID
            // 
            this.maskedTextBoxSID.Hex = false;
            this.maskedTextBoxSID.Location = new System.Drawing.Point(384, 194);
            this.maskedTextBoxSID.Mask = "00000";
            this.maskedTextBoxSID.Name = "maskedTextBoxSID";
            this.maskedTextBoxSID.Size = new System.Drawing.Size(51, 20);
            this.maskedTextBoxSID.TabIndex = 23;
            this.maskedTextBoxSID.ValidatingType = typeof(int);
            // 
            // maskedTextBoxID
            // 
            this.maskedTextBoxID.Hex = false;
            this.maskedTextBoxID.Location = new System.Drawing.Point(299, 194);
            this.maskedTextBoxID.Mask = "00000";
            this.maskedTextBoxID.Name = "maskedTextBoxID";
            this.maskedTextBoxID.Size = new System.Drawing.Size(51, 20);
            this.maskedTextBoxID.TabIndex = 22;
            this.maskedTextBoxID.ValidatingType = typeof(int);
            // 
            // ivFilters
            // 
            this.ivFilters.Location = new System.Drawing.Point(260, 32);
            this.ivFilters.Name = "ivFilters";
            this.ivFilters.Size = new System.Drawing.Size(315, 166);
            this.ivFilters.TabIndex = 318;
            // 
            // buttonAnySlot
            // 
            this.buttonAnySlot.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonAnySlot.ForeColor = System.Drawing.Color.Black;
            this.buttonAnySlot.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnySlot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAnySlot.Location = new System.Drawing.Point(846, 86);
            this.buttonAnySlot.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAnySlot.Name = "buttonAnySlot";
            this.buttonAnySlot.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonAnySlot.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonAnySlot.Size = new System.Drawing.Size(48, 23);
            this.buttonAnySlot.TabIndex = 32;
            this.buttonAnySlot.Text = "Any";
            this.buttonAnySlot.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnySlot.Click += new System.EventHandler(this.buttonAnySlot_Click);
            // 
            // buttonRoamerMap
            // 
            this.buttonRoamerMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRoamerMap.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonRoamerMap.ForeColor = System.Drawing.Color.Black;
            this.buttonRoamerMap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRoamerMap.Location = new System.Drawing.Point(133, 487);
            this.buttonRoamerMap.Name = "buttonRoamerMap";
            this.buttonRoamerMap.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonRoamerMap.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonRoamerMap.Size = new System.Drawing.Size(45, 23);
            this.buttonRoamerMap.TabIndex = 38;
            this.buttonRoamerMap.Text = "Map";
            this.buttonRoamerMap.Click += new System.EventHandler(this.buttonRoamerMap_Click);
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.ForeColor = System.Drawing.Color.Black;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new System.Drawing.Point(662, 115);
            this.comboBoxGender.MaxDropDownItems = 3;
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxGender.Size = new System.Drawing.Size(170, 21);
            this.comboBoxGender.TabIndex = 33;
            // 
            // buttonCalcInitialFrame
            // 
            this.buttonCalcInitialFrame.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonCalcInitialFrame.ForeColor = System.Drawing.Color.Black;
            this.buttonCalcInitialFrame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCalcInitialFrame.Location = new System.Drawing.Point(662, 143);
            this.buttonCalcInitialFrame.Name = "buttonCalcInitialFrame";
            this.buttonCalcInitialFrame.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonCalcInitialFrame.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonCalcInitialFrame.Size = new System.Drawing.Size(169, 23);
            this.buttonCalcInitialFrame.TabIndex = 34;
            this.buttonCalcInitialFrame.Text = "Calculate Initial PIDRNG Frame";
            this.buttonCalcInitialFrame.Click += new System.EventHandler(this.buttonCalcInitialFrame_Click);
            // 
            // buttonDSParameters
            // 
            this.buttonDSParameters.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonDSParameters.ForeColor = System.Drawing.Color.Black;
            this.buttonDSParameters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDSParameters.Location = new System.Drawing.Point(662, 216);
            this.buttonDSParameters.Name = "buttonDSParameters";
            this.buttonDSParameters.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonDSParameters.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonDSParameters.Size = new System.Drawing.Size(169, 23);
            this.buttonDSParameters.TabIndex = 36;
            this.buttonDSParameters.Text = "DS Parameters Search";
            this.buttonDSParameters.Click += new System.EventHandler(this.findDSParametersToolStripMenuItem_Click);
            // 
            // comboBoxEncounterType
            // 
            this.comboBoxEncounterType.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBoxEncounterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncounterType.Enabled = false;
            this.comboBoxEncounterType.ForeColor = System.Drawing.Color.Black;
            this.comboBoxEncounterType.FormattingEnabled = true;
            this.comboBoxEncounterType.Items.AddRange(new object[] {
            "Wild Pokémon",
            "Wild Pokémon (Surfing)",
            "Wild Pokémon (Fishing)",
            "Wild Pokémon (Bubble Spot)",
            "Wild Pokémon (Shaking Grass)",
            "Wild Pokémon (Cave Spot)",
            "Stationary Pokémon",
            "Roaming Pokémon",
            "Gift Pokémon (Non-Mystery Gift)",
            "Safari Zone",
            "Bug-Catching Contest"});
            this.comboBoxEncounterType.Location = new System.Drawing.Point(101, 60);
            this.comboBoxEncounterType.Name = "comboBoxEncounterType";
            this.comboBoxEncounterType.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxEncounterType.Size = new System.Drawing.Size(153, 21);
            this.comboBoxEncounterType.TabIndex = 2;
            // 
            // maskedTextBoxERoute
            // 
            this.maskedTextBoxERoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxERoute.Hex = false;
            this.maskedTextBoxERoute.Location = new System.Drawing.Point(282, 489);
            this.maskedTextBoxERoute.Mask = "00";
            this.maskedTextBoxERoute.Name = "maskedTextBoxERoute";
            this.maskedTextBoxERoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxERoute.TabIndex = 42;
            this.maskedTextBoxERoute.ValidatingType = typeof(int);
            // 
            // maskedTextBoxRRoute
            // 
            this.maskedTextBoxRRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxRRoute.Hex = false;
            this.maskedTextBoxRRoute.Location = new System.Drawing.Point(217, 489);
            this.maskedTextBoxRRoute.Mask = "00";
            this.maskedTextBoxRRoute.Name = "maskedTextBoxRRoute";
            this.maskedTextBoxRRoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxRRoute.TabIndex = 40;
            this.maskedTextBoxRRoute.ValidatingType = typeof(int);
            // 
            // maskedTextBoxLRoute
            // 
            this.maskedTextBoxLRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxLRoute.Hex = false;
            this.maskedTextBoxLRoute.Location = new System.Drawing.Point(347, 489);
            this.maskedTextBoxLRoute.Mask = "00";
            this.maskedTextBoxLRoute.Name = "maskedTextBoxLRoute";
            this.maskedTextBoxLRoute.Size = new System.Drawing.Size(20, 20);
            this.maskedTextBoxLRoute.TabIndex = 44;
            this.maskedTextBoxLRoute.ValidatingType = typeof(int);
            // 
            // comboBoxSynchNatures
            // 
            this.comboBoxSynchNatures.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBoxSynchNatures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSynchNatures.Enabled = false;
            this.comboBoxSynchNatures.ForeColor = System.Drawing.Color.Black;
            this.comboBoxSynchNatures.FormattingEnabled = true;
            this.comboBoxSynchNatures.Location = new System.Drawing.Point(101, 86);
            this.comboBoxSynchNatures.Name = "comboBoxSynchNatures";
            this.comboBoxSynchNatures.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxSynchNatures.Size = new System.Drawing.Size(153, 21);
            this.comboBoxSynchNatures.TabIndex = 4;
            // 
            // comboBoxEncounterSlot
            // 
            this.comboBoxEncounterSlot.BlankText = "Any";
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxEncounterSlot.CheckBoxProperties = checkBoxProperties1;
            this.comboBoxEncounterSlot.DisplayMemberSingleItem = "";
            this.comboBoxEncounterSlot.DropDownHeight = 300;
            this.comboBoxEncounterSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncounterSlot.FormattingEnabled = true;
            this.comboBoxEncounterSlot.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "Swarm"});
            this.comboBoxEncounterSlot.Location = new System.Drawing.Point(662, 88);
            this.comboBoxEncounterSlot.Name = "comboBoxEncounterSlot";
            this.comboBoxEncounterSlot.Size = new System.Drawing.Size(170, 21);
            this.comboBoxEncounterSlot.TabIndex = 31;
            this.comboBoxEncounterSlot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FocusControl);
            // 
            // comboBoxAbility
            // 
            this.comboBoxAbility.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBoxAbility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAbility.ForeColor = System.Drawing.Color.Black;
            this.comboBoxAbility.FormattingEnabled = true;
            this.comboBoxAbility.Items.AddRange(new object[] {
            "Any",
            "Ability 0",
            "Ability 1"});
            this.comboBoxAbility.Location = new System.Drawing.Point(662, 61);
            this.comboBoxAbility.Name = "comboBoxAbility";
            this.comboBoxAbility.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxAbility.Size = new System.Drawing.Size(79, 21);
            this.comboBoxAbility.TabIndex = 30;
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Hex = true;
            this.textBoxSeed.Location = new System.Drawing.Point(101, 164);
            this.textBoxSeed.Mask = "AAAAAAAAAAAAAAAA";
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(153, 20);
            this.textBoxSeed.TabIndex = 7;
            // 
            // buttonFindTime
            // 
            this.buttonFindTime.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonFindTime.ForeColor = System.Drawing.Color.Black;
            this.buttonFindTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonFindTime.Location = new System.Drawing.Point(101, 188);
            this.buttonFindTime.Name = "buttonFindTime";
            this.buttonFindTime.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonFindTime.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonFindTime.Size = new System.Drawing.Size(153, 23);
            this.buttonFindTime.TabIndex = 8;
            this.buttonFindTime.Text = "Time Finder";
            this.buttonFindTime.Click += new System.EventHandler(this.buttonFindTime_Click);
            // 
            // comboBoxNature
            // 
            this.comboBoxNature.BlankText = "Any";
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxNature.CheckBoxProperties = checkBoxProperties2;
            this.comboBoxNature.DisplayMemberSingleItem = "";
            this.comboBoxNature.DropDownHeight = 300;
            this.comboBoxNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNature.FormattingEnabled = true;
            this.comboBoxNature.Location = new System.Drawing.Point(662, 34);
            this.comboBoxNature.Name = "comboBoxNature";
            this.comboBoxNature.Size = new System.Drawing.Size(170, 21);
            this.comboBoxNature.TabIndex = 28;
            this.comboBoxNature.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FocusControl);
            // 
            // buttonAnyNature
            // 
            this.buttonAnyNature.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonAnyNature.ForeColor = System.Drawing.Color.Black;
            this.buttonAnyNature.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnyNature.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAnyNature.Location = new System.Drawing.Point(846, 32);
            this.buttonAnyNature.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAnyNature.Name = "buttonAnyNature";
            this.buttonAnyNature.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonAnyNature.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonAnyNature.Size = new System.Drawing.Size(48, 23);
            this.buttonAnyNature.TabIndex = 29;
            this.buttonAnyNature.Text = "Any";
            this.buttonAnyNature.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAnyNature.Click += new System.EventHandler(this.buttonAnyNature_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.BackColor = System.Drawing.Color.AntiqueWhite;
            this.buttonGenerate.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonGenerate.Location = new System.Drawing.Point(101, 232);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.OuterBorderColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ShineColor = System.Drawing.SystemColors.Window;
            this.buttonGenerate.Size = new System.Drawing.Size(153, 23);
            this.buttonGenerate.TabIndex = 9;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // dataGridViewValues
            // 
            this.dataGridViewValues.AllowUserToAddRows = false;
            this.dataGridViewValues.AllowUserToDeleteRows = false;
            this.dataGridViewValues.AllowUserToResizeRows = false;
            this.dataGridViewValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewValues.ColumnHeadersHeight = 20;
            this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Frame,
            this.Offset,
            this.Time,
            this.Elm,
            this.Chatot,
            this.EncounterSlot,
            this.ItemCalc,
            this.PID,
            this.CaveSpot,
            this.Shiny,
            this.Nature,
            this.Ability,
            this.Dream,
            this.Coin,
            this.HP,
            this.Atk,
            this.Def,
            this.SpA,
            this.SpD,
            this.Spe,
            this.HiddenPower,
            this.HiddenPowerPower,
            this.Characteristic,
            this.MaleOnlySpecies,
            this.f50,
            this.f125,
            this.f25,
            this.f75,
            this.PossibleShakingSpot,
            this.Synchable});
            this.dataGridViewValues.ContextMenuStrip = this.contextMenuStripGrid;
            this.dataGridViewValues.Location = new System.Drawing.Point(11, 261);
            this.dataGridViewValues.MultiSelect = false;
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewValues.RowHeadersVisible = false;
            this.dataGridViewValues.RowHeadersWidth = 21;
            this.dataGridViewValues.RowTemplate.Height = 20;
            this.dataGridViewValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewValues.ShowCellErrors = false;
            this.dataGridViewValues.ShowCellToolTips = false;
            this.dataGridViewValues.ShowEditingIcon = false;
            this.dataGridViewValues.ShowRowErrors = false;
            this.dataGridViewValues.Size = new System.Drawing.Size(892, 193);
            this.dataGridViewValues.TabIndex = 9;
            this.dataGridViewValues.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewValues_CellFormatting);
            this.dataGridViewValues.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewValues_CellMouseEnter);
            this.dataGridViewValues.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewValues_CellMouseLeave);
            this.dataGridViewValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewValues_MouseDown);
            // 
            // Frame
            // 
            this.Frame.DataPropertyName = "Number";
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
            this.Frame.Width = 55;
            // 
            // Offset
            // 
            this.Offset.DataPropertyName = "Offset";
            this.Offset.HeaderText = "Occidentary";
            this.Offset.Name = "Offset";
            this.Offset.ReadOnly = true;
            this.Offset.Width = 75;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 55;
            // 
            // Elm
            // 
            this.Elm.DataPropertyName = "Elm";
            this.Elm.HeaderText = "Elm";
            this.Elm.Name = "Elm";
            this.Elm.ReadOnly = true;
            this.Elm.Width = 35;
            // 
            // Chatot
            // 
            this.Chatot.DataPropertyName = "Chatot";
            this.Chatot.HeaderText = "Chatot Pitch";
            this.Chatot.Name = "Chatot";
            this.Chatot.ReadOnly = true;
            this.Chatot.Width = 85;
            // 
            // EncounterSlot
            // 
            this.EncounterSlot.DataPropertyName = "EncounterString";
            this.EncounterSlot.HeaderText = "Encounter Slot";
            this.EncounterSlot.Name = "EncounterSlot";
            this.EncounterSlot.ReadOnly = true;
            this.EncounterSlot.Width = 90;
            // 
            // ItemCalc
            // 
            this.ItemCalc.DataPropertyName = "ItemCalc";
            this.ItemCalc.HeaderText = "Item Percent";
            this.ItemCalc.Name = "ItemCalc";
            this.ItemCalc.ReadOnly = true;
            this.ItemCalc.Visible = false;
            this.ItemCalc.Width = 105;
            // 
            // PID
            // 
            this.PID.DataPropertyName = "Pid";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.75F);
            this.PID.DefaultCellStyle = dataGridViewCellStyle1;
            this.PID.HeaderText = "PID";
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Width = 75;
            // 
            // CaveSpot
            // 
            this.CaveSpot.DataPropertyName = "CaveSpotting";
            this.CaveSpot.HeaderText = "Cave Spot";
            this.CaveSpot.Name = "CaveSpot";
            this.CaveSpot.ReadOnly = true;
            this.CaveSpot.Visible = false;
            this.CaveSpot.Width = 87;
            // 
            // Shiny
            // 
            this.Shiny.DataPropertyName = "ShinyDisplay";
            this.Shiny.HeaderText = "!!!";
            this.Shiny.Name = "Shiny";
            this.Shiny.ReadOnly = true;
            this.Shiny.Width = 25;
            // 
            // Nature
            // 
            this.Nature.DataPropertyName = "NatureDisplay";
            this.Nature.HeaderText = "Nature";
            this.Nature.Name = "Nature";
            this.Nature.ReadOnly = true;
            this.Nature.Width = 75;
            // 
            // Ability
            // 
            this.Ability.DataPropertyName = "Ability";
            this.Ability.HeaderText = "Ability";
            this.Ability.Name = "Ability";
            this.Ability.ReadOnly = true;
            this.Ability.Width = 45;
            // 
            // Dream
            // 
            this.Dream.DataPropertyName = "DreamAbility";
            this.Dream.HeaderText = "Dream World";
            this.Dream.Name = "Dream";
            this.Dream.ReadOnly = true;
            this.Dream.Visible = false;
            this.Dream.Width = 80;
            // 
            // Coin
            // 
            this.Coin.DataPropertyName = "Coin";
            this.Coin.HeaderText = "Coin";
            this.Coin.Name = "Coin";
            this.Coin.ReadOnly = true;
            this.Coin.Width = 55;
            // 
            // HP
            // 
            this.HP.DataPropertyName = "DisplayHp";
            this.HP.HeaderText = "HP";
            this.HP.Name = "HP";
            this.HP.ReadOnly = true;
            this.HP.Width = 35;
            // 
            // Atk
            // 
            this.Atk.DataPropertyName = "DisplayAtk";
            this.Atk.HeaderText = "Atk";
            this.Atk.Name = "Atk";
            this.Atk.ReadOnly = true;
            this.Atk.Width = 35;
            // 
            // Def
            // 
            this.Def.DataPropertyName = "DisplayDef";
            this.Def.HeaderText = "Def";
            this.Def.Name = "Def";
            this.Def.ReadOnly = true;
            this.Def.Width = 35;
            // 
            // SpA
            // 
            this.SpA.DataPropertyName = "DisplaySpa";
            this.SpA.HeaderText = "SpA";
            this.SpA.Name = "SpA";
            this.SpA.ReadOnly = true;
            this.SpA.Width = 35;
            // 
            // SpD
            // 
            this.SpD.DataPropertyName = "DisplaySpd";
            this.SpD.HeaderText = "SpD";
            this.SpD.Name = "SpD";
            this.SpD.ReadOnly = true;
            this.SpD.Width = 35;
            // 
            // Spe
            // 
            this.Spe.DataPropertyName = "DisplaySpe";
            this.Spe.HeaderText = "Spe";
            this.Spe.Name = "Spe";
            this.Spe.ReadOnly = true;
            this.Spe.Width = 35;
            // 
            // HiddenPower
            // 
            this.HiddenPower.DataPropertyName = "HiddenPowerType";
            this.HiddenPower.HeaderText = "Hidden";
            this.HiddenPower.Name = "HiddenPower";
            this.HiddenPower.ReadOnly = true;
            this.HiddenPower.Width = 50;
            // 
            // HiddenPowerPower
            // 
            this.HiddenPowerPower.DataPropertyName = "HiddenPowerPower";
            this.HiddenPowerPower.HeaderText = "Power";
            this.HiddenPowerPower.Name = "HiddenPowerPower";
            this.HiddenPowerPower.ReadOnly = true;
            this.HiddenPowerPower.Width = 50;
            // 
            // Characteristic
            // 
            this.Characteristic.DataPropertyName = "Characteristic";
            this.Characteristic.HeaderText = "Characteristic";
            this.Characteristic.Name = "Characteristic";
            this.Characteristic.ReadOnly = true;
            this.Characteristic.Visible = false;
            this.Characteristic.Width = 155;
            // 
            // MaleOnlySpecies
            // 
            this.MaleOnlySpecies.DataPropertyName = "MaleOnly";
            this.MaleOnlySpecies.HeaderText = "Species";
            this.MaleOnlySpecies.Name = "MaleOnlySpecies";
            this.MaleOnlySpecies.ReadOnly = true;
            this.MaleOnlySpecies.Width = 105;
            // 
            // f50
            // 
            this.f50.DataPropertyName = "Female50";
            this.f50.HeaderText = "50% F";
            this.f50.Name = "f50";
            this.f50.ReadOnly = true;
            this.f50.Width = 50;
            // 
            // f125
            // 
            this.f125.DataPropertyName = "Female125";
            this.f125.HeaderText = "12.5%F";
            this.f125.Name = "f125";
            this.f125.ReadOnly = true;
            this.f125.Width = 50;
            // 
            // f25
            // 
            this.f25.DataPropertyName = "Female25";
            this.f25.HeaderText = "25% F";
            this.f25.Name = "f25";
            this.f25.ReadOnly = true;
            this.f25.Width = 50;
            // 
            // f75
            // 
            this.f75.DataPropertyName = "Female75";
            this.f75.HeaderText = "75% F";
            this.f75.Name = "f75";
            this.f75.ReadOnly = true;
            this.f75.Width = 50;
            // 
            // PossibleShakingSpot
            // 
            this.PossibleShakingSpot.DataPropertyName = "ShakingSpotPossible";
            this.PossibleShakingSpot.HeaderText = "Possible Shaking Spot";
            this.PossibleShakingSpot.Name = "PossibleShakingSpot";
            this.PossibleShakingSpot.ReadOnly = true;
            this.PossibleShakingSpot.Visible = false;
            this.PossibleShakingSpot.Width = 105;
            // 
            // Synchable
            // 
            this.Synchable.DataPropertyName = "Synchable";
            this.Synchable.HeaderText = "Synchable";
            this.Synchable.Name = "Synchable";
            this.Synchable.ReadOnly = true;
            this.Synchable.Visible = false;
            this.Synchable.Width = 105;
            // 
            // maskedTextBoxStartingFrame
            // 
            this.maskedTextBoxStartingFrame.Hex = false;
            this.maskedTextBoxStartingFrame.Location = new System.Drawing.Point(101, 138);
            this.maskedTextBoxStartingFrame.Mask = "0000000000";
            this.maskedTextBoxStartingFrame.Name = "maskedTextBoxStartingFrame";
            this.maskedTextBoxStartingFrame.Size = new System.Drawing.Size(153, 20);
            this.maskedTextBoxStartingFrame.TabIndex = 6;
            this.maskedTextBoxStartingFrame.Text = "1";
            this.maskedTextBoxStartingFrame.ValidatingType = typeof(int);
            // 
            // maskedTextBoxMaxFrames
            // 
            this.maskedTextBoxMaxFrames.Hex = false;
            this.maskedTextBoxMaxFrames.Location = new System.Drawing.Point(101, 112);
            this.maskedTextBoxMaxFrames.Mask = "0000000000";
            this.maskedTextBoxMaxFrames.Name = "maskedTextBoxMaxFrames";
            this.maskedTextBoxMaxFrames.Size = new System.Drawing.Size(153, 20);
            this.maskedTextBoxMaxFrames.TabIndex = 5;
            this.maskedTextBoxMaxFrames.Text = "100000";
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.ForeColor = System.Drawing.Color.Black;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Location = new System.Drawing.Point(101, 34);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.ShineColor = System.Drawing.SystemColors.Window;
            this.comboBoxMethod.Size = new System.Drawing.Size(153, 21);
            this.comboBoxMethod.TabIndex = 1;
            this.comboBoxMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethod_SelectedIndexChanged);
            // 
            // checkBoxMemoryLink
            // 
            this.checkBoxMemoryLink.AutoSize = true;
            this.checkBoxMemoryLink.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxMemoryLink.Location = new System.Drawing.Point(817, 188);
            this.checkBoxMemoryLink.Name = "checkBoxMemoryLink";
            this.checkBoxMemoryLink.Size = new System.Drawing.Size(92, 17);
            this.checkBoxMemoryLink.TabIndex = 332;
            this.checkBoxMemoryLink.Text = "Memory Link?";
            this.checkBoxMemoryLink.UseVisualStyleBackColor = true;
            this.checkBoxMemoryLink.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(916, 516);
            this.Controls.Add(this.checkBoxMemoryLink);
            this.Controls.Add(this.cbShinyCharm);
            this.Controls.Add(this.cbNidoBeat);
            this.Controls.Add(this.maskedTextBoxSID);
            this.Controls.Add(this.maskedTextBoxID);
            this.Controls.Add(this.checkBoxBW2);
            this.Controls.Add(this.ivFilters);
            this.Controls.Add(this.buttonAnySlot);
            this.Controls.Add(this.buttonRoamerMap);
            this.Controls.Add(this.checkBoxRoamerReleased);
            this.Controls.Add(this.buttonLead);
            this.Controls.Add(this.comboBoxGender);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.labelRoamerRoutes);
            this.Controls.Add(this.buttonCalcInitialFrame);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.labelCalcWarning);
            this.Controls.Add(this.buttonDSParameters);
            this.Controls.Add(this.labelElmForSeed);
            this.Controls.Add(this.comboBoxEncounterType);
            this.Controls.Add(this.labelFlipsForSeed);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.checkBoxEPresent);
            this.Controls.Add(this.maskedTextBoxERoute);
            this.Controls.Add(this.maskedTextBoxRRoute);
            this.Controls.Add(this.maskedTextBoxLRoute);
            this.Controls.Add(this.checkBoxRPresent);
            this.Controls.Add(this.checkBoxLPresent);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.checkBoxSynchOnly);
            this.Controls.Add(this.checkBoxDittoParent);
            this.Controls.Add(this.comboBoxSynchNatures);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.checkBoxDreamWorld);
            this.Controls.Add(this.labelTargetFrame);
            this.Controls.Add(this.comboBoxEncounterSlot);
            this.Controls.Add(this.comboBoxAbility);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxSeed);
            this.Controls.Add(this.buttonFindTime);
            this.Controls.Add(this.comboBoxNature);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.buttonAnyNature);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dataGridViewValues);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.maskedTextBoxStartingFrame);
            this.Controls.Add(this.maskedTextBoxMaxFrames);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.checkBoxShinyOnly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = " RNG Reporter 10.1.5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStripGrid.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripTimeFinder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RNGReporter.GlassComboBox comboBoxMethod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxShinyOnly;
        private RNGReporter.DoubleBufferedDataGridView dataGridViewValues;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private RNGReporter.GlassButton buttonGenerate;
        private CheckBoxComboBox comboBoxNature;
        private System.Windows.Forms.Label label13;
        private RNGReporter.GlassButton buttonAnyNature;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGrid;
        private System.Windows.Forms.ToolStripMenuItem lockFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem centerTo1SecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTo5SecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTo10SecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTp1MinuteToolStripMenuItem;
        private MaskedTextBox2 textBoxSeed;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem calculatePoketechTapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTo2SecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerTo3SecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeCenteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem outputResultsToTXTToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTxt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelTargetFrame;
        private RNGReporter.GlassButton buttonFindTime;
        private RNGReporter.GlassComboBox comboBoxSynchNatures;
        private MaskedTextBox2 maskedTextBoxSID;
        private MaskedTextBox2 maskedTextBoxID;
        private MaskedTextBox2 maskedTextBoxMaxFrames;
        private MaskedTextBox2 maskedTextBoxStartingFrame;
        private RNGReporter.GlassComboBox comboBoxAbility;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labelFlipsForSeed;
        private System.Windows.Forms.Label labelElmForSeed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkBoxRPresent;
        private System.Windows.Forms.CheckBox checkBoxEPresent;
        private System.Windows.Forms.CheckBox checkBoxLPresent;
        private MaskedTextBox2 maskedTextBoxRRoute;
        private MaskedTextBox2 maskedTextBoxERoute;
        private MaskedTextBox2 maskedTextBoxLRoute;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelRoamerRoutes;
        private System.Windows.Forms.Label label20;
        private CheckBoxComboBox comboBoxEncounterSlot;
        private RNGReporter.GlassButton buttonRoamerMap;
        private System.Windows.Forms.ToolStripMenuItem searchElmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchCoinFlipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem searchNaturesToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxDreamWorld;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem performanceOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberOfCPUCoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboBoxCPUCount;
        private System.Windows.Forms.CheckBox checkBoxDittoParent;
        private System.Windows.Forms.CheckBox checkBoxSynchOnly;
        private RNGReporter.GlassComboBox comboBoxEncounterType;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ToolStripMenuItem pokedexIVCheckerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thGenToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iVsToPIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSIDFromChainedShiniesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem researcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seedToTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thGenToolsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem findDSParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seedToTimeCGearSeedsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tIDSIDManipulationPandorasBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tIDSIDManipulationPandorasBoxToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem diamondEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pearlEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem platinumEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heartGoldEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soulSilverEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem blackEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSeedByIVsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSeedByStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSeedByIVRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleSeedGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem simpleSeedGeneratorToolStripMenuItem1;
        private RNGReporter.GlassButton buttonDSParameters;
        private System.Windows.Forms.Label label21;
        private RNGReporter.GlassButton buttonCalcInitialFrame;
        private System.Windows.Forms.Label labelCalcWarning;
        private System.Windows.Forms.CheckBox checkBoxRoamerReleased;
        private System.Windows.Forms.ToolStripMenuItem displayParentsInSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetParentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pIDDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem donationToolStripMenuItem;
        private RNGReporter.GlassComboBox comboBoxGender;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ToolStripMenuItem rdGenToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rubyEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sapphireEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emeraldEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fireRedEncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leafGreenEncounterTableToolStripMenuItem;
        private System.Windows.Forms.Button buttonLead;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日本語ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deutschToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem españolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem françaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italianoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 한국어ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTimeFinder;
        private System.Windows.Forms.ToolStripMenuItem FifthGenerationTimeFinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FourthGenerationTimeFinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ThirdGenerationTimeFinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameCubeTimeFinderToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipDataGrid;
        private System.Windows.Forms.ToolStripMenuItem showToolTipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjacentSeedToolToolStripMenuItem;
        private RNGReporter.GlassButton buttonAnySlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Elm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chatot;
        private System.Windows.Forms.DataGridViewTextBoxColumn EncounterSlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaveSpot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shiny;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ability;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dream;
        private System.Windows.Forms.DataGridViewTextBoxColumn Coin;
        private System.Windows.Forms.DataGridViewTextBoxColumn HP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Atk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Def;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpA;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spe;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiddenPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiddenPowerPower;
        private System.Windows.Forms.DataGridViewTextBoxColumn Characteristic;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaleOnlySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn f50;
        private System.Windows.Forms.DataGridViewTextBoxColumn f125;
        private System.Windows.Forms.DataGridViewTextBoxColumn f25;
        private System.Windows.Forms.DataGridViewTextBoxColumn f75;
        private System.Windows.Forms.DataGridViewTextBoxColumn PossibleShakingSpot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Synchable;
        private System.Windows.Forms.ToolStripMenuItem entralinkSeedToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tIDSIDManipulationPandorasBoxToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private IVFilters ivFilters;
        private System.Windows.Forms.ToolStripMenuItem black2EncounterTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem white2EncounterTableToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxBW2;
        private System.Windows.Forms.CheckBox cbNidoBeat;
        private System.Windows.Forms.CheckBox cbShinyCharm;
        private System.Windows.Forms.CheckBox checkBoxMemoryLink;
        private System.Windows.Forms.ToolStripMenuItem unovaLinkParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiddenGrottoEncounterTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitSeedToTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem togamiCalcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jirachiGenerationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pIDToIVsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pokespotToolStripMenuItem;
    }
}

