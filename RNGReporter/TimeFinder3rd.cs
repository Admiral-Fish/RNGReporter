/*
 * This file is part of RNG Reporter
 * Copyright (C) 2012 by Bill Young, Mike Suleski, and Andrew Ringer
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Objects.Searchers;
using RNGReporter.Properties;
using System.Linq;
using System.Globalization;

namespace RNGReporter
{
    public partial class TimeFinder3rd : Form
    {
        private static readonly object threadLock = new object();
        private FrameCompare frameCompare;
        private ulong gameTick;
        private ulong gameTickAlt;
        private DateTime gameTime;
        private ushort id;
        private List<IFrameEEggPID> iframesEEgg;
        private List<IFrameEEggPID> iframesEEggIV;
        private List<IFrameRSEggPID> iframesRSEgg;
        private FrameGenerator ivGenerator;
        private Thread[] jobs;
        private BindingSource listBindingEggEIV;
        private BindingSource listBindingEggEPID;
        private BindingSource listBindingEggRS;
        private FrameGenerator lowerGenerator;
        private ulong progressFound;
        private ulong progressSearched;
        private ulong progressTotal;
        private bool refreshQueue;
        private ushort sid;
        private FrameCompare subFrameCompare;
        private int tabPage;
        private EventWaitHandle waitHandle;

        private List<WildSlots> wildSlots;
        private bool isSearching;
        private List<uint> natureList;
        private List<uint> slotsList;
        private EncounterType encounterType;
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<uint> slist = new List<uint>();
        private List<uint> rlist = new List<uint>();
        private uint shinyval, genderFilter, abilityFilter;
        private static List<uint> hiddenPowerList;
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private readonly String[] hiddenPowers = { "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark" };


        public TimeFinder3rd(ushort id, ushort sid)
        {
            this.id = id;
            this.sid = sid;
            InitializeComponent();

            normalSpreadRadioButton.Checked = true;
        }

        public int TabPage
        {
            get { return tabPage; }
            set { tabPage = value; }
        }

        //private bool paused = false;

        public void setPause()
        {
            //paused = true;
        }

        public void setUnpause()
        {
            //paused = false;
        }

        private void PlatinumTime_Load(object sender, EventArgs e)
        {
            // Add smart comboBox items
            // Would be nice if we left these in the Designer file
            // But Visual Studio seems to like deleting them without warning

            var ability = new[]
                {
                    new ComboBoxItem("Any", -1),
                    new ComboBoxItem("Ability 0", 0),
                    new ComboBoxItem("Ability 1", 1)
                };

            cbMethod.Items.AddRange(new object[]
                {
                    new ComboBoxItem("Method 1", FrameType.Method1),
                    new ComboBoxItem("Method 2", FrameType.Method2),
                    new ComboBoxItem("Method 4", FrameType.Method4),
                    new ComboBoxItem("Method H-1", FrameType.MethodH1),
                    new ComboBoxItem("Method H-2", FrameType.MethodH2),
                    new ComboBoxItem("Method H-4", FrameType.MethodH4),
                });

            cbEncounterType.Items.AddRange(new object[]
                {
                    new ComboBoxItem("Wild Pokémon", EncounterType.Wild),
                    new ComboBoxItem("Wild Pokémon (Surfing)",
                                     EncounterType.WildSurfing),
                    new ComboBoxItem("Wild Pokémon (Old Rod)",
                                     EncounterType.WildOldRod),
                    new ComboBoxItem("Wild Pokémon (Good Rod)",
                                     EncounterType.WildGoodRod),
                    new ComboBoxItem("Wild Pokémon (Super Rod)",
                                     EncounterType.WildSuperRod),
                    new ComboBoxItem("Stationary Pokémon", EncounterType.Stationary),
                    new ComboBoxItem("Safari Zone", EncounterType.SafariZone)
                });

            comboBoxType.Items.AddRange(new object[]
                {
                    new ComboBoxItem("Wild Pokémon", EncounterType.Wild),
                    new ComboBoxItem("Wild Pokémon (Surfing)",
                                     EncounterType.WildSurfing),
                    new ComboBoxItem("Wild Pokémon (Old Rod)",
                                     EncounterType.WildOldRod),
                    new ComboBoxItem("Wild Pokémon (Good Rod)",
                                     EncounterType.WildGoodRod),
                    new ComboBoxItem("Wild Pokémon (Super Rod)",
                                     EncounterType.WildSuperRod)
                });

            comboBoxShiny3rdNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());
            comboBoxShiny3rdAbility.DataSource = ability;
            comboBoxShiny3rdGender.DataSource = GenderFilter.GenderFilterCollection();

            comboEPIDNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());
            comboEPIDAbility.DataSource = ability;
            comboEPIDGender.DataSource = GenderFilter.GenderFilterCollection();

            cbNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());
            cbAbility.DataSource = ability;
            comboBoxGenderXD.DataSource = GenderFilter.GenderFilterCollection();

            comboBoxNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());
            comboBoxHiddenPower.Items.AddRange(addHP());
            setComboBox();

            checkBoxNatureFRLG.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());
            checkBoxNatureFRLG.CheckBoxItems[0].Checked = true;
            checkBoxNatureFRLG.CheckBoxItems[0].Checked = false;
            glassComboBoxGenderFRLG.DataSource = GenderFilter.GenderFilterCollection();
            glassComboBoxAbilityFRLG.DataSource = ability;
            glassComboBoxAbilityFRLG.SelectedIndex = 0;
            compatibilityFRLG.SelectedIndex = 0;
            glassComboBoxGenderFRLG.SelectedIndex = 0;

            var everstoneList = new BindingSource { DataSource = Objects.Nature.NatureDropDownCollectionSynch() };
            comboEPIDEverstone.DataSource = everstoneList;
            cbSynchNature.DataSource = Objects.Nature.NatureDropDownCollectionSynch();

            cbCapGender.DataSource = GenderFilter.GenderFilterCollection();

            Settings.Default.PropertyChanged += ChangeLanguage;
            SetLanguage();

            comboBoxShiny3rdNature.SelectedIndex = 0;
            comboBoxShiny3rdAbility.SelectedIndex = 0;
            comboEPIDGender.SelectedIndex = 0;
            comboEPIDEverstone.SelectedIndex = 0;
            comboEPIDNature.SelectedIndex = 0;
            comboEPIDCompatibility.SelectedIndex = 0;
            comboEPIDAbility.SelectedIndex = 0;

            comboBoxShiny3rdGender.SelectedIndex = 0;
            comboBoxParentCompatibility.SelectedIndex = 0;

            cbMethod.SelectedIndex = 0;
            cbEncounterType.SelectedIndex = 0;
            cbEncounterSlot.SelectedIndex = 0;
            cbSynchNature.SelectedIndex = 0;
            cbNature.SelectedIndex = 0;

            dataGridViewShinyRSResults.AutoGenerateColumns = false;
            shiny3rdPID.DefaultCellStyle.Format = "X8";

            dataGridFRLG.AutoGenerateColumns = false;
            pidFRLG.DefaultCellStyle.Format = "X8";

            dataGridViewEIVs.AutoGenerateColumns = false;
            dataGridViewEPIDs.AutoGenerateColumns = false;
            EPIDPID.DefaultCellStyle.Format = "X8";

            dataGridViewXDCalibration.AutoGenerateColumns = false;
            XDSeed.DefaultCellStyle.Format = "X8";
            XDPID.DefaultCellStyle.Format = "X8";
            XDTime.DefaultCellStyle.Format = "0.000";
            comboBoxNatureXD.SelectedIndex = 0;
            comboBoxGenderXD.SelectedIndex = 8;

            maskedTextBoxShiny3rdID.Text = id.ToString("00000");
            maskedTextBoxShiny3rdSID.Text = sid.ToString("00000");

            tabControl.SelectTab(tabPage);

            cbEncounterSlot.CheckBoxItems[0].Checked = true;
            cbEncounterSlot.CheckBoxItems[0].Checked = false;

            maskedTextBoxShiny3rdSID.Text = Settings.Default.SID;
            maskedTextBoxShiny3rdID.Text = Settings.Default.ID;
            textEPIDSID.Text = Settings.Default.SID;
            textEPIDID.Text = Settings.Default.ID;
            txtSID.Text = Settings.Default.SID;
            txtID.Text = Settings.Default.ID;
            wildTID.Text = Settings.Default.ID;
            wildSID.Text = Settings.Default.SID;
        }

        public void ChangeLanguage(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language")
            {
                SetLanguage();
            }
        }

        public void SetLanguage()
        {
            var CellStyle = new DataGridViewCellStyle();
            switch ((Language)Settings.Default.Language)
            {
                case (Language.Japanese):
                    CellStyle.Font = new Font("Meiryo", 7.25F);
                    if (CellStyle.Font.Name != "Meiryo")
                    {
                        CellStyle.Font = new Font("Arial Unicode MS", 8.25F);
                        if (CellStyle.Font.Name != "Arial Unicode MS")
                        {
                            CellStyle.Font = new Font("MS Mincho", 8.25F);
                        }
                    }
                    break;
                case (Language.Korean):
                    CellStyle.Font = new Font("Malgun Gothic", 8.25F);
                    if (CellStyle.Font.Name != "Malgun Gothic")
                    {
                        CellStyle.Font = new Font("Gulim", 9.25F);
                        if (CellStyle.Font.Name != "Gulim")
                        {
                            CellStyle.Font = new Font("Arial Unicode MS", 8.25F);
                        }
                    }
                    break;
                default:
                    CellStyle.Font = DefaultFont;
                    break;
            }

            shiny3rdNature.DefaultCellStyle = CellStyle;
            EPIDNature.DefaultCellStyle = CellStyle;
            comboBoxShiny3rdNature.Font = CellStyle.Font;
            comboEPIDNature.Font = CellStyle.Font;

            for (int checkBoxIndex = 1; checkBoxIndex < comboBoxShiny3rdNature.Items.Count; checkBoxIndex++)
            {
                comboBoxShiny3rdNature.CheckBoxItems[checkBoxIndex].Text =
                    (comboBoxShiny3rdNature.CheckBoxItems[checkBoxIndex].ComboBoxItem).ToString();
                comboBoxShiny3rdNature.CheckBoxItems[checkBoxIndex].Font = CellStyle.Font;

                comboEPIDNature.CheckBoxItems[checkBoxIndex].Text =
                    (comboEPIDNature.CheckBoxItems[checkBoxIndex].ComboBoxItem).ToString();
                comboEPIDNature.CheckBoxItems[checkBoxIndex].Font = CellStyle.Font;

                cbNature.CheckBoxItems[checkBoxIndex].Text =
                    (cbNature.CheckBoxItems[checkBoxIndex].ComboBoxItem).ToString();
                cbNature.CheckBoxItems[checkBoxIndex].Font = CellStyle.Font;
            }

            comboBoxShiny3rdNature.CheckBoxItems[0].Checked = true;
            comboBoxShiny3rdNature.CheckBoxItems[0].Checked = false;

            comboEPIDNature.CheckBoxItems[0].Checked = true;
            comboEPIDNature.CheckBoxItems[0].Checked = false;

            cbNature.CheckBoxItems[0].Checked = true;
            cbNature.CheckBoxItems[0].Checked = false;

            dataGridViewShinyRSResults.Refresh();
        }

        private void PlatinumTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            if (jobs != null)
            {
                for (int i = 0; i < jobs.Length; i++)
                {
                    if (jobs[i] != null)
                    {
                        jobs[i].Abort();
                    }
                }
            }
            Hide();
        }

        private void contextMenuStripEggPid3rd_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewShinyRSResults.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        //Ruby/Sapphire generation
        private void Generate3rdGenRSJob()
        {
            uint searchRange = ivGenerator.MaxResults;

            //  This is where we actually go ahead and call our 
            //  generator for a list of egg PIDs based on parameters
            //  that have been passed in.
            List<Frame> frames = lowerGenerator.Generate(frameCompare, id, sid);
            progressTotal += (ulong)frames.Count * searchRange;

            //  Now we need to iterate through each result heref
            //  and create a collection of the information that
            //  we are going to place into our grid.
            foreach (Frame frame in frames)
            {
                waitHandle.WaitOne();
                ivGenerator.StaticPID = frame.Pid;
                List<Frame> shinyFrames = ivGenerator.Generate(subFrameCompare, id, sid);

                progressSearched += searchRange;
                progressFound += (uint)shinyFrames.Count;

                foreach (Frame shinyFrame in shinyFrames)
                {
                    var iframe = new IFrameRSEggPID
                    {
                        FrameLowerPID = frame.Number,
                        FrameUpperPID = shinyFrame.Number,
                        Pid = shinyFrame.Pid,
                        Shiny = shinyFrame.Shiny,
                        DisplayHp = shinyFrame.DisplayHpAlt,
                        DisplayAtk = shinyFrame.DisplayAtkAlt,
                        DisplayDef = shinyFrame.DisplayDefAlt,
                        DisplaySpa = shinyFrame.DisplaySpaAlt,
                        DisplaySpd = shinyFrame.DisplaySpdAlt,
                        DisplaySpe = shinyFrame.DisplaySpeAlt,
                        DisplayHpInh = shinyFrame.DisplayHp,
                        DisplayAtkInh = shinyFrame.DisplayAtk,
                        DisplayDefInh = shinyFrame.DisplayDef,
                        DisplaySpaInh = shinyFrame.DisplaySpa,
                        DisplaySpdInh = shinyFrame.DisplaySpd,
                        DisplaySpeInh = shinyFrame.DisplaySpe
                    };

                    lock (threadLock)
                    {
                        if (iframe.FrameUpperPID > iframe.FrameLowerPID)
                        {
                            iframesRSEgg.Add(iframe);
                        }
                    }
                    refreshQueue = true;
                }
            }
        }

        //FRLG
        private void Generate3rdGenFRLGJob()
        {
            uint searchRange = ivGenerator.MaxResults;

            //  This is where we actually go ahead and call our 
            //  generator for a list of egg PIDs based on parameters
            //  that have been passed in.
            List<Frame> frames = lowerGenerator.Generate(frameCompare, id, sid);
            progressTotal += (ulong)frames.Count * searchRange;

            //  Now we need to iterate through each result heref
            //  and create a collection of the information that
            //  we are going to place into our grid.
            foreach (Frame frame in frames)
            {
                waitHandle.WaitOne();
                ivGenerator.StaticPID = frame.Pid;
                List<Frame> shinyFrames = ivGenerator.Generate(subFrameCompare, id, sid);

                progressSearched += searchRange;
                progressFound += (uint)shinyFrames.Count;

                foreach (Frame shinyFrame in shinyFrames)
                {
                    var iframe = new IFrameRSEggPID
                    {
                        FrameLowerPID = frame.Number,
                        FrameUpperPID = shinyFrame.Number,
                        Pid = shinyFrame.Pid,
                        Shiny = shinyFrame.Shiny,
                        DisplayHp = shinyFrame.DisplayHpAlt,
                        DisplayAtk = shinyFrame.DisplayAtkAlt,
                        DisplayDef = shinyFrame.DisplayDefAlt,
                        DisplaySpa = shinyFrame.DisplaySpaAlt,
                        DisplaySpd = shinyFrame.DisplaySpdAlt,
                        DisplaySpe = shinyFrame.DisplaySpeAlt,
                        DisplayHpInh = shinyFrame.DisplayHp,
                        DisplayAtkInh = shinyFrame.DisplayAtk,
                        DisplayDefInh = shinyFrame.DisplayDef,
                        DisplaySpaInh = shinyFrame.DisplaySpa,
                        DisplaySpdInh = shinyFrame.DisplaySpd,
                        DisplaySpeInh = shinyFrame.DisplaySpe
                    };

                    lock (threadLock)
                    {
                        if (iframe.FrameUpperPID > iframe.FrameLowerPID)
                        {
                            iframesRSEgg.Add(iframe);
                        }
                    }
                    refreshQueue = true;
                }
            }
        }

        //Emerald
        private void Generate3rdGenEPIDJob(uint calibration, uint minRedraws, uint maxRedraws)
        {
            uint searchRange = lowerGenerator.MaxResults;

            for (uint redraws = minRedraws; redraws <= maxRedraws; ++redraws)
            {
                // note: this is inefficent and should be done in a much faster way
                // will require a restructure of FrameGenerator
                uint offset = calibration + 3 * redraws;
                lowerGenerator.Calibration = offset;
                List<Frame> frames = lowerGenerator.Generate(frameCompare, id, sid);
                progressTotal = (ulong)frames.Count * searchRange * (maxRedraws - minRedraws);

                foreach (Frame frame in frames)
                {
                    waitHandle.WaitOne();
                    progressSearched += searchRange;
                    progressFound += 1;
                    var iframe = new IFrameEEggPID
                    {
                        Advances = frame.Advances,
                        FrameLowerPID = frame.Number - offset - 18,
                        Pid = frame.Pid,
                        Shiny = frame.Shiny,
                        Redraws = redraws
                    };

                    lock (threadLock)
                    {
                        iframesEEgg.Add(iframe);
                    }
                    refreshQueue = true;
                }
            }
        }

        private void Generate3rdGenEIVJob()
        {
            uint searchRange = ivGenerator.MaxResults;

            //generate the iv frames
            List<Frame> ivFrames = ivGenerator.Generate(subFrameCompare, id, sid);
            progressTotal = (ulong)ivFrames.Count * searchRange;

            foreach (Frame frame in ivFrames)
            {
                waitHandle.WaitOne();
                //ivGenerator.StaticPID = frame.Pid;
                progressSearched += searchRange;
                progressFound += (uint)ivFrames.Count;

                var iframe = new IFrameEEggPID
                {
                    FrameNumber = frame.Name,
                    FrameUpperPID = frame.Number,
                    Pid = frame.Pid,
                    Shiny = frame.Shiny,
                    DisplayHp = frame.DisplayHpAlt,
                    DisplayAtk = frame.DisplayAtkAlt,
                    DisplayDef = frame.DisplayDefAlt,
                    DisplaySpa = frame.DisplaySpaAlt,
                    DisplaySpd = frame.DisplaySpdAlt,
                    DisplaySpe = frame.DisplaySpeAlt,
                    DisplayHpInh = frame.DisplayHp,
                    DisplayAtkInh = frame.DisplayAtk,
                    DisplayDefInh = frame.DisplayDef,
                    DisplaySpaInh = frame.DisplaySpa,
                    DisplaySpdInh = frame.DisplaySpd,
                    DisplaySpeInh = frame.DisplaySpe,
                };

                lock (threadLock)
                {
                    iframesEEggIV.Add(iframe);
                }
                refreshQueue = true;
            }
        }

        private void ManageProgress(BindingSource bindingSource, DoubleBufferedDataGridView grid, FrameType frameType,
                                    int sleepTimer)
        {
            var progress = new Progress();
            progress.SetupAndShow(this, 0, 0, false, true, waitHandle);

            progressSearched = 0;
            progressFound = 0;

            UpdateGridDelegate gridUpdater = UpdateGrid;
            var updateParams = new object[] { bindingSource };
            ResortGridDelegate gridSorter = ResortGrid;
            var sortParams = new object[] { bindingSource, grid, frameType };
            ThreadDelegate enableGenerateButton = EnableCapGenerate;

            try
            {
                bool alive = true;
                while (alive)
                {
                    progress.ShowProgress(progressSearched / (float)progressTotal, progressSearched, progressFound);
                    if (refreshQueue)
                    {
                        Invoke(gridUpdater, updateParams);
                        refreshQueue = false;
                    }
                    if (jobs != null)
                    {
                        foreach (Thread job in jobs)
                        {
                            if (job != null && job.IsAlive)
                            {
                                alive = true;
                                break;
                            }
                            alive = false;
                        }
                    }
                    if (sleepTimer > 0)
                        Thread.Sleep(sleepTimer);
                }
            }
            catch (ObjectDisposedException)
            {
                // This keeps the program from crashing when the Time Finder progress box
                // is closed from the Windows taskbar.
            }
            catch (Exception exception)
            {
                if (exception.Message != "Operation Cancelled")
                {
                    throw;
                }
            }
            finally
            {
                progress.Finish();

                if (jobs != null)
                {
                    for (int i = 0; i < jobs.Length; i++)
                    {
                        if (jobs[i] != null)
                        {
                            jobs[i].Abort();
                        }
                    }
                }

                Invoke(enableGenerateButton);
                Invoke(gridSorter, sortParams);
            }
        }

        // Methods we'll use when we roll up the above functions

        private void UpdateGrid(BindingSource bindingSource)
        {
            bindingSource.ResetBindings(false);
        }

        private void ResortGrid(BindingSource bindingSource, DoubleBufferedDataGridView dataGrid, FrameType frameType)
        {
            switch (frameType)
            {
                case FrameType.EBredPID:
                    var iframeComparer = new IFrameEEggPIDComparer { CompareType = "Frame" };
                    ((List<IFrameEEggPID>)bindingSource.DataSource).Sort(iframeComparer);
                    EPIDFrame.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
            }
            dataGrid.DataSource = bindingSource;
            bindingSource.ResetBindings(false);
        }

        private void EnableCapGenerate()
        {
            buttonShiny3rdGenerate.Enabled = true;
        }

        private void dataGridViewShiny3rdResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //  Make all of the junk natures show up in a lighter color
            if (dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdNature")
            {
                var nature = (string)e.Value;

                if (nature == Functions.NatureStrings(18) ||
                    nature == Functions.NatureStrings(6) ||
                    nature == Functions.NatureStrings(0) ||
                    nature == Functions.NatureStrings(24) ||
                    nature == Functions.NatureStrings(12) ||
                    nature == Functions.NatureStrings(9) ||
                    nature == Functions.NatureStrings(21))
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }

            if (dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdHP" ||
                dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdAtk" ||
                dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdDef" ||
                dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdSpA" ||
                dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdSpD" ||
                dataGridViewShinyRSResults.Columns[e.ColumnIndex].Name == "shiny3rdSpe")
            {
                if ((string)e.Value == "30" || (string)e.Value == "31")
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if ((string)e.Value == "0")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }

                if ((string)e.Value == "A" || (string)e.Value == "B")
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void outputShiny3rdResultsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Going to need to present the user with a File Dialog and 
            //  then interate through the Grid, outputting columns that
            //  are visible.

            saveFileDialogTxt.AddExtension = true;
            saveFileDialogTxt.Title = "Save Output to TXT";
            saveFileDialogTxt.Filter = "TXT Files|*.txt";
            saveFileDialogTxt.FileName = "rngreporter.txt";
            if (saveFileDialogTxt.ShowDialog() == DialogResult.OK)
            {
                //  Get the name of the file and then go ahead 
                //  and create and save the thing to the hard
                //  drive.   
                List<IFrameRSEggPID> frames = iframesRSEgg;

                if (frames != null)
                {
                    if (frames.Count > 0)
                    {
                        var writer = new TXTWriter(dataGridViewShinyRSResults);
                        writer.Generate(saveFileDialogTxt.FileName, frames);
                    }
                }
            }
        }

        private void dataGridViewShiny3rdResults_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewShinyRSResults.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewShinyRSResults.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewShinyRSResults.ClearSelection();

                        (dataGridViewShinyRSResults.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void buttonShiny3rdGenerate_Click(object sender, EventArgs e)
        {
            uint seed;

            if (normalSpreadRadioButton.Checked == true)
            {
                // seed used by all Ruby\Sapphire cartridges when the internal battery is dead
                seed = 0x05A0;
            }
            else
            {
                seed = uint.Parse(maskedTextBox21.Text, NumberStyles.HexNumber);
            }

            if (maskedTextBoxShiny3rdID.Text != "")
                id = ushort.Parse(maskedTextBoxShiny3rdID.Text);

            if (maskedTextBoxShiny3rdSID.Text != "")
                sid = ushort.Parse(maskedTextBoxShiny3rdSID.Text);

            var parentA = new uint[6];
            var parentB = new uint[6];

            uint.TryParse(maskedTextBoxShiny3rdParentA_HP.Text, out parentA[0]);
            uint.TryParse(maskedTextBoxShiny3rdParentA_Atk.Text, out parentA[1]);
            uint.TryParse(maskedTextBoxShiny3rdParentA_Def.Text, out parentA[2]);
            uint.TryParse(maskedTextBoxShiny3rdParentA_SpA.Text, out parentA[3]);
            uint.TryParse(maskedTextBoxShiny3rdParentA_SpD.Text, out parentA[4]);
            uint.TryParse(maskedTextBoxShiny3rdParentA_Spe.Text, out parentA[5]);

            uint.TryParse(maskedTextBoxShiny3rdParentB_HP.Text, out parentB[0]);
            uint.TryParse(maskedTextBoxShiny3rdParentB_Atk.Text, out parentB[1]);
            uint.TryParse(maskedTextBoxShiny3rdParentB_Def.Text, out parentB[2]);
            uint.TryParse(maskedTextBoxShiny3rdParentB_SpA.Text, out parentB[3]);
            uint.TryParse(maskedTextBoxShiny3rdParentB_SpD.Text, out parentB[4]);
            uint.TryParse(maskedTextBoxShiny3rdParentB_Spe.Text, out parentB[5]);

            uint maxHeldFrame;
            uint maxPickupFrame;
            uint minHeldFrame;
            uint minPickupFrame;

            if (!uint.TryParse(maskedTextBox3rdHeldMinFrame.Text, out minHeldFrame))
            {
                maskedTextBox3rdHeldMinFrame.Focus();
                maskedTextBox3rdHeldMinFrame.SelectAll();
                return;
            }

            if (!uint.TryParse(maskedTextBox3rdPickupMinFrame.Text, out minPickupFrame))
            {
                maskedTextBox3rdPickupMinFrame.Focus();
                maskedTextBox3rdPickupMinFrame.SelectAll();
                return;
            }

            if (!uint.TryParse(maskedTextBox3rdHeldMaxFrame.Text, out maxHeldFrame))
            {
                maskedTextBox3rdHeldMaxFrame.Focus();
                maskedTextBox3rdHeldMaxFrame.SelectAll();
                return;
            }

            if (!uint.TryParse(maskedTextBox3rdPickupMaxFrame.Text, out maxPickupFrame))
            {
                maskedTextBox3rdPickupMaxFrame.Focus();
                maskedTextBox3rdPickupMaxFrame.SelectAll();
                return;
            }

            if (minHeldFrame > maxHeldFrame)
            {
                maskedTextBox3rdHeldMinFrame.Focus();
                maskedTextBox3rdHeldMinFrame.SelectAll();
                return;
            }

            if (minPickupFrame > maxPickupFrame)
            {
                maskedTextBox3rdPickupMinFrame.Focus();
                maskedTextBox3rdPickupMinFrame.SelectAll();
                return;
            }

            lowerGenerator = new FrameGenerator();
            ivGenerator = new FrameGenerator();

            if (comboBoxParentCompatibility.SelectedIndex == 1)
                lowerGenerator.Compatibility = 50;
            else if (comboBoxParentCompatibility.SelectedIndex == 2)
                lowerGenerator.Compatibility = 70;
            else
                lowerGenerator.Compatibility = 20;

            lowerGenerator.FrameType = FrameType.RSBredLower;
            ivGenerator.FrameType = FrameType.RSBredUpper;

            lowerGenerator.InitialFrame = minHeldFrame;
            ivGenerator.InitialFrame = minPickupFrame;

            lowerGenerator.MaxResults = maxHeldFrame - minHeldFrame + 1;
            ivGenerator.MaxResults = maxPickupFrame - minPickupFrame + 1;

            lowerGenerator.InitialSeed = seed;
            ivGenerator.InitialSeed = seed;

            ivGenerator.ParentA = parentA;
            ivGenerator.ParentB = parentB;

            List<uint> natures = null;
            if (comboBoxShiny3rdNature.Text != "Any" && comboBoxShiny3rdNature.CheckBoxItems.Count > 0)
            {
                natures = new List<uint>();
                for (int i = 0; i < comboBoxShiny3rdNature.CheckBoxItems.Count; i++)
                    if (comboBoxShiny3rdNature.CheckBoxItems[i].Checked)
                        natures.Add((uint)((Nature)comboBoxShiny3rdNature.CheckBoxItems[i].ComboBoxItem).Number);
            }

            frameCompare = new FrameCompare(
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                null,
                -1,
                false,
                false,
                false,
                null,
                (GenderFilter)(comboBoxShiny3rdGender.SelectedItem));

            subFrameCompare = new FrameCompare(
                parentA[0],
                parentA[1],
                parentA[2],
                parentA[3],
                parentA[4],
                parentA[5],
                parentB[0],
                parentB[1],
                parentB[2],
                parentB[3],
                parentB[4],
                parentB[5],
                ivFiltersRSEgg.IVFilter,
                natures,
                (int)((ComboBoxItem)comboBoxShiny3rdAbility.SelectedItem).Reference,
                checkBoxShiny3rdShinyOnly.Checked,
                true,
                new NoGenderFilter());

            // Here we check the parent IVs
            // To make sure they even have a chance of producing the desired spread
            int parentPassCount = 0;
            for (int i = 0; i < 6; i++)
            {
                if (subFrameCompare.CompareIV(i, parentA[i]) || subFrameCompare.CompareIV(i, parentB[i]))
                    parentPassCount++;
            }

            if (parentPassCount < 3)
            {
                MessageBox.Show("The parent IVs you have listed cannot produce your desired search results.");
                return;
            }

            iframesRSEgg = new List<IFrameRSEggPID>();
            listBindingEggRS = new BindingSource { DataSource = iframesRSEgg };

            dataGridViewShinyRSResults.DataSource = listBindingEggRS;

            progressSearched = 0;
            progressFound = 0;
            progressTotal = 0;

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(Generate3rdGenRSJob);
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingEggRS, dataGridViewShinyRSResults, lowerGenerator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;
            buttonShiny3rdGenerate.Enabled = false;
        }

        private void checkBoxShiny3rdShowInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxShiny3rdShowInheritance.Checked)
            {
                shiny3rdHP.DataPropertyName = "DisplayHp";
                shiny3rdAtk.DataPropertyName = "DisplayAtk";
                shiny3rdDef.DataPropertyName = "DisplayDef";
                shiny3rdSpA.DataPropertyName = "DisplaySpa";
                shiny3rdSpD.DataPropertyName = "DisplaySpd";
                shiny3rdSpe.DataPropertyName = "DisplaySpe";
            }
            else
            {
                shiny3rdHP.DataPropertyName = "DisplayHpInh";
                shiny3rdAtk.DataPropertyName = "DisplayAtkInh";
                shiny3rdDef.DataPropertyName = "DisplayDefInh";
                shiny3rdSpA.DataPropertyName = "DisplaySpaInh";
                shiny3rdSpD.DataPropertyName = "DisplaySpdInh";
                shiny3rdSpe.DataPropertyName = "DisplaySpeInh";
            }
        }

        private void buttonGenerateXD_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxMinHP.Text == "")
            {
                maskedTextBoxMinHP.Focus();
                maskedTextBoxMinHP.SelectAll();
                return;
            }

            if (maskedTextBoxMinAtk.Text == "")
            {
                maskedTextBoxMinAtk.Focus();
                maskedTextBoxMinAtk.SelectAll();
                return;
            }

            if (maskedTextBoxMinDef.Text == "")
            {
                maskedTextBoxMinDef.Focus();
                maskedTextBoxMinDef.SelectAll();
                return;
            }

            if (maskedTextBoxMinSpA.Text == "")
            {
                maskedTextBoxMinSpA.Focus();
                maskedTextBoxMinSpA.SelectAll();
                return;
            }

            if (maskedTextBoxMinSpD.Text == "")
            {
                maskedTextBoxMinSpD.Focus();
                maskedTextBoxMinSpD.SelectAll();
                return;
            }

            if (maskedTextBoxMinSpe.Text == "")
            {
                maskedTextBoxMinSpe.Focus();
                maskedTextBoxMinSpe.SelectAll();
                return;
            }

            if (maskedTextBoxMaxHP.Text == "")
            {
                maskedTextBoxMaxHP.Focus();
                maskedTextBoxMaxHP.SelectAll();
                return;
            }

            if (maskedTextBoxMaxAtk.Text == "")
            {
                maskedTextBoxMaxAtk.Focus();
                maskedTextBoxMaxAtk.SelectAll();
                return;
            }

            if (maskedTextBoxMaxDef.Text == "")
            {
                maskedTextBoxMaxDef.Focus();
                maskedTextBoxMaxDef.SelectAll();
                return;
            }

            if (maskedTextBoxMaxSpA.Text == "")
            {
                maskedTextBoxMaxSpA.Focus();
                maskedTextBoxMaxSpA.SelectAll();
                return;
            }

            if (maskedTextBoxMaxSpD.Text == "")
            {
                maskedTextBoxMaxSpD.Focus();
                maskedTextBoxMaxSpD.SelectAll();
                return;
            }

            if (maskedTextBoxMaxSpe.Text == "")
            {
                maskedTextBoxMaxSpe.Focus();
                maskedTextBoxMaxSpe.SelectAll();
                return;
            }

            uint minhp = uint.Parse(maskedTextBoxMinHP.Text);
            uint minatk = uint.Parse(maskedTextBoxMinAtk.Text);
            uint mindef = uint.Parse(maskedTextBoxMinDef.Text);
            uint minspa = uint.Parse(maskedTextBoxMinSpA.Text);
            uint minspd = uint.Parse(maskedTextBoxMinSpD.Text);
            uint minspe = uint.Parse(maskedTextBoxMinSpe.Text);

            uint maxhp = uint.Parse(maskedTextBoxMaxHP.Text);
            uint maxatk = uint.Parse(maskedTextBoxMaxAtk.Text);
            uint maxdef = uint.Parse(maskedTextBoxMaxDef.Text);
            uint maxspa = uint.Parse(maskedTextBoxMaxSpA.Text);
            uint maxspd = uint.Parse(maskedTextBoxMaxSpD.Text);
            uint maxspe = uint.Parse(maskedTextBoxMaxSpe.Text);

            var nature = (uint)comboBoxNatureXD.SelectedIndex;

            var XDGenerator = new FrameGenerator();

            List<Frame> frames = XDGenerator.Generate(minhp, maxhp,
                                                      minatk, maxatk,
                                                      mindef, maxdef,
                                                      minspa, maxspa,
                                                      minspd, maxspd,
                                                      minspe, maxspe,
                                                      nature);

            var iframes = new List<IFrameCaptureXD>();

            foreach (Frame frame in frames)
            {
                var iframe = new IFrameCaptureXD { Frame = frame };

                // We're calibrating only with shadow Pokémon that are generated first in the party.
                // There are 375451 frames between the initial seed generation and Pokémon generation,
                // so we need to reverse the RNG that many frames
                var reverseRNG = new XdRngR(frame.Seed);


                for (int i = 0; i < 375450; i++)
                {
                    reverseRNG.GetNext32BitNumber();
                }

                iframe.Seed = reverseRNG.GetNext32BitNumber();
                //iframe.Seed = frame.Seed;
                iframes.Add(iframe);
            }

            dataGridViewXDCalibration.DataSource = iframes;
        }

        private void btnSetGCTick_Click(object sender, EventArgs e)
        {
            gameTime = DateTime.Now;
            buttonGenerateXD.Enabled = true;
            btnGetCurrentTick.Enabled = true;
        }

        private void btnGetCurrentTick_Click(object sender, EventArgs e)
        {
            var tickDifference = (ulong)(DateTime.Now.Ticks - gameTime.Ticks);
            ulong currentTick = (gameTick + tickDifference / 10 * 6) & 0xFFFFFFFF;
            ulong currentTickAlt = (gameTickAlt + tickDifference / 10 * 6) & 0xFFFFFFFF;

            lblCurrentTick.Text = "Tick: " + currentTick.ToString("X8");
            lblCurrentTickAlt.Text = "Tick 2: " + currentTickAlt.ToString("X8");
        }

        private void buttonXDTickReset_Click(object sender, EventArgs e)
        {
            btnGetCurrentTick.Enabled = false;
            buttonGenerateXD.Enabled = false;

            maskedTextBoxXDHp.Text = "";
            maskedTextBoxXDAtk.Text = "";
            maskedTextBoxXDDef.Text = "";
            maskedTextBoxXDSpa.Text = "";
            maskedTextBoxXDSpd.Text = "";
            maskedTextBoxXDSpe.Text = "";
            textBoxXDNature.Text = "";
        }

        private void buttonXDSetStats_Click(object sender, EventArgs e)
        {
            if (dataGridViewXDCalibration.SelectedRows[0] != null)
            {
                var frame = (IFrameCaptureXD)dataGridViewXDCalibration.SelectedRows[0].DataBoundItem;
                gameTick = frame.Seed;

                var oneFrameBack = new XdRngR((uint)frame.Seed);
                gameTickAlt = oneFrameBack.GetNext32BitNumber();

                maskedTextBoxXDHp.Text = frame.DisplayHp;
                maskedTextBoxXDAtk.Text = frame.DisplayAtk;
                maskedTextBoxXDDef.Text = frame.DisplayDef;
                maskedTextBoxXDSpa.Text = frame.DisplaySpa;
                maskedTextBoxXDSpd.Text = frame.DisplaySpd;
                maskedTextBoxXDSpe.Text = frame.DisplaySpe;
                textBoxXDNature.Text = frame.Nature;
            }
        }

        private void buttonSwapParents_Click(object sender, EventArgs e)
        {
            string tempHP = maskedTextBoxShiny3rdParentB_HP.Text;
            string tempAtk = maskedTextBoxShiny3rdParentB_Atk.Text;
            string tempDef = maskedTextBoxShiny3rdParentB_Def.Text;
            string tempSpA = maskedTextBoxShiny3rdParentB_SpA.Text;
            string tempSpD = maskedTextBoxShiny3rdParentB_SpD.Text;
            string tempSpe = maskedTextBoxShiny3rdParentB_Spe.Text;

            maskedTextBoxShiny3rdParentB_HP.Text = maskedTextBoxShiny3rdParentA_HP.Text;
            maskedTextBoxShiny3rdParentB_Atk.Text = maskedTextBoxShiny3rdParentA_Atk.Text;
            maskedTextBoxShiny3rdParentB_Def.Text = maskedTextBoxShiny3rdParentA_Def.Text;
            maskedTextBoxShiny3rdParentB_SpA.Text = maskedTextBoxShiny3rdParentA_SpA.Text;
            maskedTextBoxShiny3rdParentB_SpD.Text = maskedTextBoxShiny3rdParentA_SpD.Text;
            maskedTextBoxShiny3rdParentB_Spe.Text = maskedTextBoxShiny3rdParentA_Spe.Text;

            maskedTextBoxShiny3rdParentA_HP.Text = tempHP;
            maskedTextBoxShiny3rdParentA_Atk.Text = tempAtk;
            maskedTextBoxShiny3rdParentA_Def.Text = tempDef;
            maskedTextBoxShiny3rdParentA_SpA.Text = tempSpA;
            maskedTextBoxShiny3rdParentA_SpD.Text = tempSpD;
            maskedTextBoxShiny3rdParentA_Spe.Text = tempSpe;
        }

        private void FocusControl(object sender, MouseEventArgs e)
        {
            ((Control)sender).Focus();
        }

        private void buttonGenerateEPIDs_Click(object sender, EventArgs e)
        {
            const uint seed = 0x0;

            if (textEPIDID.Text != "")
            {
                ParseInputD(textEPIDID, out id);
            }

            if (textEPIDSID.Text != "")
            {
                ParseInputD(textEPIDSID, out sid);
            }

            uint maxHeldFrame;
            uint minHeldFrame;
            uint minRedraw;
            uint maxRedraw;
            uint calibration;

            if (!ParseInputD(textEPIDMinFrame, out minHeldFrame) ||
                !ParseInputD(textEPIDMinFrame, out minHeldFrame) ||
                !ParseInputD(textEPIDMaxFrame, out maxHeldFrame) ||
                !ParseInputD(textEPIDMinRedraws, out minRedraw) ||
                !ParseInputD(textEPIDMaxRedraws, out maxRedraw) ||
                !ParseInputD(textEPIDCalibration, out calibration)) return;

            if (minHeldFrame > maxHeldFrame)
            {
                maskedTextBox3rdHeldMinFrame.Focus();
                maskedTextBox3rdHeldMinFrame.SelectAll();
                return;
            }

            lowerGenerator = new FrameGenerator();

            switch (comboEPIDCompatibility.SelectedIndex)
            {
                case 1:
                    lowerGenerator.Compatibility = 50;
                    break;
                case 2:
                    lowerGenerator.Compatibility = 70;
                    break;
                default:
                    lowerGenerator.Compatibility = 20;
                    break;
            }

            lowerGenerator.FrameType = FrameType.EBredPID;

            lowerGenerator.InitialFrame = minHeldFrame;

            lowerGenerator.MaxResults = maxHeldFrame - minHeldFrame + 1 + 3 * (maxRedraw - minRedraw);

            lowerGenerator.InitialSeed = seed;

            List<uint> natures = null;
            if (comboEPIDNature.Text != "Any" && comboEPIDNature.CheckBoxItems.Count > 0)
            {
                natures = new List<uint>();
                for (int i = 0; i < comboEPIDNature.CheckBoxItems.Count; i++)
                {
                    if (comboEPIDNature.CheckBoxItems[i].Checked)
                        natures.Add((uint)((Nature)comboEPIDNature.CheckBoxItems[i].ComboBoxItem).Number);
                }
            }

            if (comboEPIDEverstone.SelectedIndex != 0)
            {
                lowerGenerator.Everstone = true;
                lowerGenerator.SynchNature = ((Nature)comboEPIDEverstone.SelectedItem).Number;
                Advances.Visible = true;
            }
            else
                Advances.Visible = false;

            frameCompare = new FrameCompare(
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                natures,
                (int)((ComboBoxItem)comboEPIDAbility.SelectedItem).Reference,
                checkEPIDShiny.Checked,
                false,
                false,
                null,
                (GenderFilter)(comboEPIDGender.SelectedItem));

            // Here we check the parent IVs
            // To make sure they even have a chance of producing the desired spread
            iframesEEgg = new List<IFrameEEggPID>();
            listBindingEggEPID = new BindingSource { DataSource = iframesEEgg };
            dataGridViewEPIDs.DataSource = listBindingEggEPID;

            progressSearched = 0;
            progressFound = 0;
            progressTotal = 0;

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(() => Generate3rdGenEPIDJob(calibration, minRedraw, maxRedraw));
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingEggEPID, dataGridViewEPIDs, lowerGenerator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;
            buttonShiny3rdGenerate.Enabled = false;
        }

        private void buttonGenerateEIVs_Click(object sender, EventArgs e)
        {
            const uint seed = 0x0;

            var parentA = new uint[6];
            var parentB = new uint[6];

            uint.TryParse(textEIVParentA_HP.Text, out parentA[0]);
            uint.TryParse(textEIVParentA_Atk.Text, out parentA[1]);
            uint.TryParse(textEIVParentA_Def.Text, out parentA[2]);
            uint.TryParse(textEIVParentA_SpA.Text, out parentA[3]);
            uint.TryParse(textEIVParentA_SpD.Text, out parentA[4]);
            uint.TryParse(textEIVParentA_Spe.Text, out parentA[5]);

            uint.TryParse(textEIVParentB_HP.Text, out parentB[0]);
            uint.TryParse(textEIVParentB_Atk.Text, out parentB[1]);
            uint.TryParse(textEIVParentB_Def.Text, out parentB[2]);
            uint.TryParse(textEIVParentB_SpA.Text, out parentB[3]);
            uint.TryParse(textEIVParentB_SpD.Text, out parentB[4]);
            uint.TryParse(textEIVParentB_Spe.Text, out parentB[5]);

            uint maxPickupFrame;
            uint minPickupFrame;

            if (!ParseInputD(textEIVMinFrame, out minPickupFrame) || !ParseInputD(textEIVMaxFrame, out maxPickupFrame))
                return;

            if (minPickupFrame > maxPickupFrame)
            {
                textEIVMinFrame.Focus();
                textEIVMinFrame.SelectAll();
                return;
            }

            ivGenerator = new FrameGenerator();

            if (radioButtonEIVSplit.Checked)
                ivGenerator.FrameType = FrameType.BredSplit;
            else if (radioButtonEIVAlternate.Checked)
                ivGenerator.FrameType = FrameType.BredAlternate;
            else
                ivGenerator.FrameType = FrameType.Bred;

            ivGenerator.InitialFrame = minPickupFrame;

            ivGenerator.MaxResults = maxPickupFrame - minPickupFrame + 1;

            ivGenerator.InitialSeed = seed;

            ivGenerator.ParentA = parentA;
            ivGenerator.ParentB = parentB;

            subFrameCompare = new FrameCompare(
                parentA[0],
                parentA[1],
                parentA[2],
                parentA[3],
                parentA[4],
                parentA[5],
                parentB[0],
                parentB[1],
                parentB[2],
                parentB[3],
                parentB[4],
                parentB[5],
                ivFiltersEEgg.IVFilter,
                null,
                -1,
                false,
                true,
                new NoGenderFilter());

            // Here we check the parent IVs
            // To make sure they even have a chance of producing the desired spread
            int parentPassCount = 0;
            for (int i = 0; i < 6; i++)
            {
                if (subFrameCompare.CompareIV(i, parentA[i]) ||
                    subFrameCompare.CompareIV(i, parentB[i]))
                {
                    parentPassCount++;
                }
            }

            if (parentPassCount < 3)
            {
                MessageBox.Show("The parent IVs you have listed cannot produce your desired search results.");
                return;
            }

            iframesEEggIV = new List<IFrameEEggPID>();
            listBindingEggEIV = new BindingSource { DataSource = iframesEEggIV };

            dataGridViewEIVs.DataSource = listBindingEggEIV;

            progressSearched = 0;
            progressFound = 0;
            progressTotal = 0;

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(Generate3rdGenEIVJob);
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingEggEIV, dataGridViewEIVs, ivGenerator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;
            buttonShiny3rdGenerate.Enabled = false;
        }

        private static bool ParseInputD(TextBoxBase control, out uint value)
        {
            if (!uint.TryParse(control.Text, out value))
            {
                control.Focus();
                control.SelectAll();
                return false;
            }
            return true;
        }

        private static bool ParseInputD(TextBoxBase control, out ushort value)
        {
            if (!ushort.TryParse(control.Text, out value))
            {
                control.Focus();
                control.SelectAll();
                return false;
            }
            return true;
        }

        private void buttonAnyNature_Click(object sender, EventArgs e)
        {
            comboBoxShiny3rdNature.ClearSelection();
        }

        private void buttonAnyAbility_Click(object sender, EventArgs e)
        {
            comboBoxShiny3rdAbility.SelectedIndex = 0;
        }

        private void checkEIVInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEIVInheritance.Checked)
            {
                EIVHP.DataPropertyName = "DisplayHp";
                EIVAtk.DataPropertyName = "DisplayAtk";
                EIVDef.DataPropertyName = "DisplayDef";
                EIVSpA.DataPropertyName = "DisplaySpa";
                EIVSpD.DataPropertyName = "DisplaySpd";
                EIVSpe.DataPropertyName = "DisplaySpe";
            }
            else
            {
                EIVHP.DataPropertyName = "DisplayHpInh";
                EIVAtk.DataPropertyName = "DisplayAtkInh";
                EIVDef.DataPropertyName = "DisplayDefInh";
                EIVSpA.DataPropertyName = "DisplaySpaInh";
                EIVSpD.DataPropertyName = "DisplaySpdInh";
                EIVSpe.DataPropertyName = "DisplaySpeInh";
            }
        }

        private void buttonEIVSwapParents_Click(object sender, EventArgs e)
        {
            string tempHP = textEIVParentB_HP.Text;
            string tempAtk = textEIVParentB_Atk.Text;
            string tempDef = textEIVParentB_Def.Text;
            string tempSpA = textEIVParentB_SpA.Text;
            string tempSpD = textEIVParentB_SpD.Text;
            string tempSpe = textEIVParentB_Spe.Text;

            textEIVParentB_HP.Text = textEIVParentA_HP.Text;
            textEIVParentB_Atk.Text = textEIVParentA_Atk.Text;
            textEIVParentB_Def.Text = textEIVParentA_Def.Text;
            textEIVParentB_SpA.Text = textEIVParentA_SpA.Text;
            textEIVParentB_SpD.Text = textEIVParentA_SpD.Text;
            textEIVParentB_Spe.Text = textEIVParentA_Spe.Text;

            textEIVParentA_HP.Text = tempHP;
            textEIVParentA_Atk.Text = tempAtk;
            textEIVParentA_Def.Text = tempDef;
            textEIVParentA_SpA.Text = tempSpA;
            textEIVParentA_SpD.Text = tempSpD;
            textEIVParentA_Spe.Text = tempSpe;
        }

        private void buttonEPIDNature_Click(object sender, EventArgs e)
        {
            comboEPIDNature.ClearSelection();
        }

        private void buttonEPIDAbility_Click(object sender, EventArgs e)
        {
            comboEPIDAbility.SelectedIndex = 0;
        }

        private void cbDeadBattery_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDeadBattery.Checked)
            {
                dtSeed.Enabled = false;
                txtMinHour.Enabled = false;
                txtMaxHour.Enabled = false;
                txtMinMinute.Enabled = false;
                txtMaxMinute.Enabled = false;
                //set the values for a dead battery
                dtSeed.Value = new DateTime(2000, 1, 1);
                txtMinHour.Text = "0";
                txtMaxHour.Text = "0";
                txtMinMinute.Text = "0";
                txtMaxMinute.Text = "0";
            }
            else
            {
                dtSeed.Enabled = true;
                txtMinHour.Enabled = true;
                txtMaxHour.Enabled = true;
                txtMinMinute.Enabled = true;
                txtMaxMinute.Enabled = true;
            }
        }

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((FrameType)((ComboBoxItem)cbMethod.SelectedItem).Reference == FrameType.MethodH1 ||
                (FrameType)((ComboBoxItem)cbMethod.SelectedItem).Reference == FrameType.MethodH2 ||
                (FrameType)((ComboBoxItem)cbMethod.SelectedItem).Reference == FrameType.MethodH4)
            {
                cbEncounterSlot.Enabled = true;
            }
            else
            {
                cbEncounterSlot.Enabled = false;
            }
        }

        private void btnAnySlot_Click(object sender, EventArgs e)
        {
            cbEncounterSlot.ClearSelection();
        }

        private void btnClearNatures_Click(object sender, EventArgs e)
        {
            cbNature.ClearSelection();
        }

        private void buttonCapGenerate_Click(object sender, EventArgs e)
        {
            var searchParams = new Gen3SearchParams
            {
                ability = cbAbility,
                capButton = btnCapGenerate,
                dataGridView = dgvCapValues,
                date = dtSeed,
                encounterSlot = cbEncounterSlot,
                encounterType = cbEncounterType,
                frameType = cbMethod,
                gender = cbCapGender,
                id = txtID,
                isShiny = chkShinyOnly,
                isSynch = chkSynchOnly,
                ivfilters = ivFiltersCapture,
                maxFrame = txtCapMaxFrame,
                maxHour = txtMaxHour,
                maxMinute = txtMaxMinute,
                minFrame = txtCapMinFrame,
                minHour = txtMinHour,
                minMinute = txtMinMinute,
                nature = cbNature,
                sid = txtSID,
                synchNature = cbSynchNature
            };
            Searcher searcher = new Gen3Searcher(searchParams, threadLock, this);
            if (!searcher.ParseInput()) return;
            searcher.RunSearch();
        }

        private void rbRS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRS.Checked)
            {
                cbSynchNature.Enabled = false;
                cbSynchNature.SelectedIndex = 0;
                cbDeadBattery.Enabled = true;
                cbDeadBattery_CheckedChanged(sender, e);
            }
            else
            {
                cbSynchNature.Enabled = true;
                dtSeed.Value = new DateTime(1900, 12, 31);
                dtSeed.Enabled = false;
                cbDeadBattery.Enabled = false;
                txtMinHour.Enabled = false;
                txtMaxHour.Enabled = false;
                txtMinMinute.Enabled = false;
                txtMaxMinute.Enabled = false;
            }
        }

        private void swapParentsFRLG_Click(object sender, EventArgs e)
        {
            string tempHP = parentBFRLG_HP.Text;
            string tempAtk = parentBFRLG_Atk.Text;
            string tempDef = parentBFRLG_Def.Text;
            string tempSpA = parentBFRLG_SpA.Text;
            string tempSpD = parentBFRLG_SpD.Text;
            string tempSpe = parentBFRLG_Spe.Text;

            parentBFRLG_HP.Text = parentAFRLG_HP.Text;
            parentBFRLG_Atk.Text = parentAFRLG_Atk.Text;
            parentBFRLG_Def.Text = parentAFRLG_Def.Text;
            parentBFRLG_SpA.Text = parentAFRLG_SpA.Text;
            parentBFRLG_SpD.Text = parentAFRLG_SpD.Text;
            parentBFRLG_Spe.Text = parentAFRLG_Spe.Text;

            parentAFRLG_HP.Text = tempHP;
            parentAFRLG_Atk.Text = tempAtk;
            parentAFRLG_Def.Text = tempDef;
            parentAFRLG_SpA.Text = tempSpA;
            parentAFRLG_SpD.Text = tempSpD;
            parentAFRLG_Spe.Text = tempSpe;
        }

        private void anyAbilityFRLG_Click(object sender, EventArgs e)
        {
            glassComboBoxAbilityFRLG.SelectedIndex = 0;
        }

        private void anyNatureFRLG_Click(object sender, EventArgs e)
        {
            checkBoxNatureFRLG.ClearSelection();
        }

        private void generateFRLGEggShiny_Click(object sender, EventArgs e)
        {
            uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint seed); ;

            if (tidFRLG.Text != "")
                id = ushort.Parse(tidFRLG.Text);

            if (sidFRLG.Text != "")
                sid = ushort.Parse(sidFRLG.Text);

            var parentA = new uint[6];
            var parentB = new uint[6];

            uint.TryParse(parentAFRLG_HP.Text, out parentA[0]);
            uint.TryParse(parentAFRLG_Atk.Text, out parentA[1]);
            uint.TryParse(parentAFRLG_Def.Text, out parentA[2]);
            uint.TryParse(parentAFRLG_SpA.Text, out parentA[3]);
            uint.TryParse(parentAFRLG_SpD.Text, out parentA[4]);
            uint.TryParse(parentAFRLG_Spe.Text, out parentA[5]);

            uint.TryParse(parentBFRLG_HP.Text, out parentB[0]);
            uint.TryParse(parentBFRLG_Atk.Text, out parentB[1]);
            uint.TryParse(parentBFRLG_Def.Text, out parentB[2]);
            uint.TryParse(parentBFRLG_SpA.Text, out parentB[3]);
            uint.TryParse(parentBFRLG_SpD.Text, out parentB[4]);
            uint.TryParse(parentBFRLG_Spe.Text, out parentB[5]);

            uint maxHeldFrame;
            uint maxPickupFrame;
            uint minHeldFrame;
            uint minPickupFrame;

            if (!uint.TryParse(minHeldFrameFRLG.Text, out minHeldFrame))
            {
                minHeldFrameFRLG.Focus();
                minHeldFrameFRLG.SelectAll();
                return;
            }

            if (!uint.TryParse(minPickFrameFRLG.Text, out minPickupFrame))
            {
                minPickFrameFRLG.Focus();
                minPickFrameFRLG.SelectAll();
                return;
            }

            if (!uint.TryParse(maxHeldFrameFRLG.Text, out maxHeldFrame))
            {
                maxHeldFrameFRLG.Focus();
                maxHeldFrameFRLG.SelectAll();
                return;
            }

            if (!uint.TryParse(maxPickFrameFRLG.Text, out maxPickupFrame))
            {
                maxPickFrameFRLG.Focus();
                maxPickFrameFRLG.SelectAll();
                return;
            }

            if (minHeldFrame > maxHeldFrame)
            {
                minHeldFrameFRLG.Focus();
                minHeldFrameFRLG.SelectAll();
                return;
            }

            if (minPickupFrame > maxPickupFrame)
            {
                minPickFrameFRLG.Focus();
                minPickFrameFRLG.SelectAll();
                return;
            }

            lowerGenerator = new FrameGenerator();
            ivGenerator = new FrameGenerator();

            if (compatibilityFRLG.SelectedIndex == 1)
                lowerGenerator.Compatibility = 50;
            else if (compatibilityFRLG.SelectedIndex == 2)
                lowerGenerator.Compatibility = 70;
            else
                lowerGenerator.Compatibility = 20;

            lowerGenerator.FrameType = FrameType.FRLGBredLower;
            ivGenerator.FrameType = FrameType.FRLGBredUpper;

            lowerGenerator.InitialFrame = minHeldFrame;
            ivGenerator.InitialFrame = minPickupFrame;

            lowerGenerator.MaxResults = maxHeldFrame - minHeldFrame + 1;
            ivGenerator.MaxResults = maxPickupFrame - minPickupFrame + 1;

            lowerGenerator.InitialSeed = seed;
            ivGenerator.InitialSeed = seed;

            ivGenerator.ParentA = parentA;
            ivGenerator.ParentB = parentB;

            List<uint> natures = null;
            if (checkBoxNatureFRLG.Text != "Any" && checkBoxNatureFRLG.CheckBoxItems.Count > 0)
            {
                natures = new List<uint>();
                for (int i = 0; i < checkBoxNatureFRLG.CheckBoxItems.Count; i++)
                    if (checkBoxNatureFRLG.CheckBoxItems[i].Checked)
                        natures.Add((uint)((Nature)checkBoxNatureFRLG.CheckBoxItems[i].ComboBoxItem).Number);
            }

            frameCompare = new FrameCompare(
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                null,
                -1,
                false,
                false,
                false,
                null,
                (GenderFilter)(glassComboBoxGenderFRLG.SelectedItem));

            subFrameCompare = new FrameCompare(
                parentA[0],
                parentA[1],
                parentA[2],
                parentA[3],
                parentA[4],
                parentA[5],
                parentB[0],
                parentB[1],
                parentB[2],
                parentB[3],
                parentB[4],
                parentB[5],
                ivFiltersFRLG.IVFilter,
                natures,
                (int)((ComboBoxItem)glassComboBoxAbilityFRLG.SelectedItem).Reference,
                shinyOnlyFRLG.Checked,
                true,
                new NoGenderFilter());

            // Here we check the parent IVs
            // To make sure they even have a chance of producing the desired spread
            int parentPassCount = 0;
            for (int i = 0; i < 6; i++)
            {
                if (subFrameCompare.CompareIV(i, parentA[i]) || subFrameCompare.CompareIV(i, parentB[i]))
                    parentPassCount++;
            }

            if (parentPassCount < 3)
            {
                MessageBox.Show("The parent IVs you have listed cannot produce your desired search results.");
                return;
            }

            iframesRSEgg = new List<IFrameRSEggPID>();
            listBindingEggRS = new BindingSource { DataSource = iframesRSEgg };

            dataGridViewShinyRSResults.DataSource = listBindingEggRS;

            progressSearched = 0;
            progressFound = 0;
            progressTotal = 0;

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(Generate3rdGenFRLGJob);
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingEggRS, dataGridFRLG, lowerGenerator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;
            buttonShiny3rdGenerate.Enabled = false;
        }

        #region Nested type: ResortGridDelegate

        private delegate void ResortGridDelegate(
            BindingSource bindingSource, DoubleBufferedDataGridView dataGrid, FrameType frameType);

        #endregion

        #region Nested type: ThreadDelegate

        private delegate void ThreadDelegate();

        #endregion

        #region Nested type: UpdateGridDelegate

        private delegate void UpdateGridDelegate(BindingSource bindingSource);

        #endregion

        #region Wild Encounter Slots Time Finder
        #region Search Settings
        private void searchWild_Click(object sender, EventArgs e)
        {
            uint[] ivsLower, ivsUpper;
            getIVs(out ivsLower, out ivsUpper);

            if (ivsLower[0] > ivsUpper[0])
                MessageBox.Show("HP: Lower limit > Upper limit");
            else if (ivsLower[1] > ivsUpper[1])
                MessageBox.Show("Atk: Lower limit > Upper limit");
            else if (ivsLower[2] > ivsUpper[2])
                MessageBox.Show("Def: Lower limit > Upper limit");
            else if (ivsLower[3] > ivsUpper[3])
                MessageBox.Show("SpA: Lower limit > Upper limit");
            else if (ivsLower[4] > ivsUpper[4])
                MessageBox.Show("SpD: Lower limit > Upper limit");
            else if (ivsLower[5] > ivsUpper[5])
                MessageBox.Show("Spe: Lower limit > Upper limit");
            else
            {
                if (isSearching)
                    return;

                isSearching = true;
                status.Text = "Searching";
                int methodNum = comboBoxMethod.SelectedIndex;
                encounterType = getEncounterType(comboBoxType.SelectedIndex);
                wildSlots = new List<WildSlots>();
                rlist.Clear();
                slist.Clear();
                binding = new BindingSource { DataSource = wildSlots };
                dataGridViewResult.DataSource = binding;
                shinyval = ((uint.Parse(wildTID.Text)) ^ (uint.Parse(wildSID.Text))) >> 3;

                natureList = null;
                if (comboBoxNature.Text != "Any" && comboBoxNature.CheckBoxItems.Count > 0)
                    natureList = (from t in comboBoxNature.CheckBoxItems where t.Checked select (uint)((Nature)t.ComboBoxItem).Number).ToList();

                slotsList = null;
                if (comboBoxSlots.Text != "Any" && comboBoxSlots.CheckBoxItems.Count > 0)
                {
                    slotsList = new List<uint>();
                    for (uint i = 0; i < comboBoxSlots.CheckBoxItems.Count; i++)
                    {
                        if (comboBoxSlots.CheckBoxItems[(int)i].Checked)
                            // We have to subtract 1 because this custom control contains a hidden item for text display
                            slotsList.Add(i - 1);
                    }
                }

                hiddenPowerList = null;
                List<uint> temp = new List<uint>();
                if (comboBoxHiddenPower.Text != "Any" && comboBoxHiddenPower.CheckBoxItems.Count > 0)
                    for (int x = 1; x <= 16; x++)
                        if (comboBoxHiddenPower.CheckBoxItems[x].Checked)
                            temp.Add((uint)(x - 1));

                if (temp.Count != 0)
                    hiddenPowerList = temp;

                if (methodNum == 0 || methodNum == 1 || methodNum == 2)
                    dataGridViewResult.Columns[1].Visible = false;
                else
                    dataGridViewResult.Columns[1].Visible = true;

                searchThread = new Thread(() => getSearch(ivsLower, ivsUpper, methodNum));
                searchThread.Start();

                var update = new Thread(updateGUI);
                update.Start();
            }
        }

        private void getSearch(uint[] ivsLower, uint[] ivsUpper, int num)
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 76871)
            {
                if (num == 0 || num == 1 || num == 2)
                    method124Frame(ivsLower, ivsUpper, num);
                else
                    methodH124Frame(ivsLower, ivsUpper, num);
            }
            else
            {
                if (num == 0 || num == 3)
                    methodH1IV(ivsLower, ivsUpper, num);
                else if (num == 1 || num == 4)
                    methodH2IV(ivsLower, ivsUpper, num);
                else
                    methodH4IV(ivsLower, ivsUpper, num);
            }
        }
        #endregion

        #region Method 1 IV
        private void methodH1IV(uint[] ivsLower, uint[] ivsUpper, int method)
        {
            isSearching = true;
            abilityFilter = getAbility();
            genderFilter = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeed1(a, b, c, d, e, f, method);
                            }

            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeed1(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int method)
        {
            uint x4 = hp | (atk << 5) | (def << 10);
            uint x4_2 = x4 ^ 0x8000;
            uint ex4 = spe | (spa << 5) | (spd << 10);
            uint ex4_2 = ex4 ^ 0x8000;
            uint ivs_1b = x4 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b + cnt;
                uint ivs_2 = forward(seedb) >> 16;
                if (ivs_2 == ex4 || ivs_2 == ex4_2)
                {
                    uint pid2 = reverse(seedb);
                    uint pid1 = reverse(pid2);
                    uint seed = reverse(pid1);
                    uint pid = (pid2 & 0xFFFF0000) | (pid1 >> 16);
                    uint nature = pid - 25 * (pid / 25);

                    uint xorSeed = seed ^ 0x80000000;
                    uint xorPID = pid ^ 0x80008000;
                    uint xorNature = xorPID - 25 * (xorPID / 25);

                    if (method == 0)
                    {
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, 0);
                        if (natureList == null || natureList.Contains(xorNature))
                            filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, 0);
                    }
                    else
                    {
                        if (natureList == null || natureList.Contains(nature))
                        {
                            getEncounterSlot(seed, pid, out int slot, out seed);
                            if (validatePID(seed) == pid)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, slot);
                            }
                        }
                        if (natureList == null || natureList.Contains(xorNature))
                        {
                            getEncounterSlot(xorSeed, xorPID, out int slot, out xorSeed);
                            if (validatePID(xorSeed) == xorPID)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, slot);
                            }
                        }
                    }

                }

            }
        }
        #endregion

        #region Method H124 Frame
        private void methodH124Frame(uint[] ivsLower, uint[] ivsUpper, int method)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            abilityFilter = getAbility();
            genderFilter = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange + 1000);
                    for (uint n = 0; n < srange; n++)
                    {
                        for (uint sisterSeed = 0; sisterSeed < 2; sisterSeed++)
                        {
                            int slot = EncounterSlotCalc.encounterSlot(sisterSeed == 0 ? slist[(int)n + 1] : slist[(int)n + 1] ^ 0x80000000, FrameType.MethodH1, encounterType);
                            if (slotsList == null || slotsList.Contains((uint)slot))
                            {
                                uint pid = 0;
                                uint nature = (sisterSeed == 0 ? slist[(int)n + 3] : slist[(int)n + 3] ^ 0x80000000) >> 16;
                                nature = nature - 25 * (nature / 25);
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    int count = 3;
                                    bool flag = true;
                                    uint pid1, pid2;
                                    while (flag)
                                    {
                                        pid1 = sisterSeed == 0 ? rlist[(int)(n + 1 + count)] : rlist[(int)(n + 1 + count)] ^ 0x8000;
                                        pid2 = sisterSeed == 0 ? rlist[(int)(n + 2 + count)] : rlist[(int)(n + 2 + count)] ^ 0x8000;
                                        pid = (pid2 << 16) | pid1;
                                        if ((pid - 25 * (pid / 25)) == nature)
                                            flag = false;
                                        else
                                            count += 2;
                                    }
                                    uint[] ivs;
                                    if (method == 3)
                                        ivs = createIVs(sisterSeed == 0 ? rlist[(int)(n + 3 + count)] : rlist[(int)(n + 3 + count)] ^ 0x8000, sisterSeed == 0 ? rlist[(int)(n + 4 + count)] : rlist[(int)(n + 4 + count)] ^ 0x8000, ivsLower, ivsUpper);
                                    else if (method == 4)
                                        ivs = createIVs(sisterSeed == 0 ? rlist[(int)(n + 4 + count)] : rlist[(int)(n + 4 + count)] ^ 0x8000, sisterSeed == 0 ? rlist[(int)(n + 5 + count)] : rlist[(int)(n + 5 + count)] ^ 0x8000, ivsLower, ivsUpper);
                                    else
                                        ivs = createIVs(sisterSeed == 0 ? rlist[(int)(n + 3 + count)] : rlist[(int)(n + 3 + count)] ^ 0x8000, sisterSeed == 0 ? rlist[(int)(n + 5 + count)] : rlist[(int)(n + 4 + count)] ^ 0x8000, ivsLower, ivsUpper);
                                    if (ivs != null)
                                    {
                                        filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, sisterSeed == 0 ? slist[(int)n] : slist[(int)n] ^ 0x80000000, method, slot);
                                    }
                                }
                            }
                        }
                    }
                    refresh = true;
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion

        #region Method 2 IV
        private void methodH2IV(uint[] ivsLower, uint[] ivsUpper, int method)
        {
            isSearching = true;
            abilityFilter = getAbility();
            genderFilter = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeed2(a, b, c, d, e, f, method);
                            }

            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeed2(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int method)
        {
            uint x4 = hp | (atk << 5) | (def << 10);
            uint ex4 = spe | (spa << 5) | (spd << 10);
            uint ex4_2 = ex4 ^ 0x8000;
            uint ivs_1b = x4 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b + cnt;
                uint ivs_2 = forward(seedb) >> 16;
                if (ivs_2 == ex4 || ivs_2 == ex4_2)
                {
                    uint pid2 = reverse(reverse(seedb));
                    uint pid1 = reverse(pid2);
                    uint seed = reverse(pid1);
                    uint pid = (pid2 & 0xFFFF0000) | (pid1 >> 16);
                    uint nature = pid - 25 * (pid / 25);

                    uint xorSeed = seed ^ 0x80000000;
                    uint xorPID = pid ^ 0x80008000;
                    uint xorNature = xorPID - 25 * (xorPID / 25);

                    if (method == 1)
                    {
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, 0);
                        if (natureList == null || natureList.Contains(xorNature))
                            filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, 0);
                    }
                    else
                    {
                        if (natureList == null || natureList.Contains(nature))
                        {
                            getEncounterSlot(seed, pid, out int slot, out seed);
                            if (validatePID(seed) == pid)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, slot);
                            }
                        }
                        if (natureList == null || natureList.Contains(xorNature))
                        {
                            getEncounterSlot(xorSeed, xorPID, out int slot, out xorSeed);
                            if (validatePID(xorSeed) == xorPID)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, slot);
                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region Method 4 IV
        private void methodH4IV(uint[] ivsLower, uint[] ivsUpper, int method)
        {
            isSearching = true;
            abilityFilter = getAbility();
            genderFilter = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeed4(a, b, c, d, e, f, method);
                            }

            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeed4(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int method)
        {
            uint x4 = hp | (atk << 5) | (def << 10);
            uint ex4 = spe | (spa << 5) | (spd << 10);
            uint ex4_2 = ex4 ^ 0x8000;
            uint ivs_1b = x4 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b + cnt;
                uint ivs_2 = forward(forward(seedb)) >> 16;
                if (ivs_2 == ex4 || ivs_2 == ex4_2)
                {
                    uint pid2 = reverse(seedb);
                    uint pid1 = reverse(pid2);
                    uint seed = reverse(pid1);
                    uint pid = (pid2 & 0xFFFF0000) | (pid1 >> 16);
                    uint nature = pid - 25 * (pid / 25);

                    uint xorSeed = seed ^ 0x80000000;
                    uint xorPID = pid ^ 0x80008000;
                    uint xorNature = xorPID - 25 * (xorPID / 25);

                    if (method == 2)
                    {
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, 0);
                        if (natureList == null || natureList.Contains(xorNature))
                            filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, 0);
                    }
                    else
                    {
                        if (natureList == null || natureList.Contains(nature))
                        {
                            getEncounterSlot(seed, pid, out int slot, out seed);
                            if (validatePID(seed) == pid)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed, method, slot);
                            }
                        }
                        if (natureList == null || natureList.Contains(xorNature))
                        {
                            getEncounterSlot(xorSeed, xorPID, out int slot, out xorSeed);
                            if (validatePID(xorSeed) == xorPID)
                            {
                                if (slotsList == null || slotsList.Contains((uint)slot))
                                    filterSeed(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, method, slot);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Method 124 Frame
        private void method124Frame(uint[] ivsLower, uint[] ivsUpper, int method)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            abilityFilter = getAbility();
            genderFilter = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs;
                        if (method == 0)
                            ivs = createIVs(rlist[(int)(n + 3)], rlist[(int)(n + 4)], ivsLower, ivsUpper);
                        else if (method == 1)
                            ivs = createIVs(rlist[(int)(n + 4)], rlist[(int)(n + 5)], ivsLower, ivsUpper);
                        else
                            ivs = createIVs(rlist[(int)(n + 3)], rlist[(int)(n + 5)], ivsLower, ivsUpper);
                        if (ivs != null)
                        {
                            uint pid = pidChk(n);
                            uint nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, slist[(int)(n)], method, 0);

                            pid ^= 0x80008000;
                            nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, slist[(int)(n)] ^ 0x80000000, method, 0);
                        }
                    }
                    refresh = true;
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        private uint[] createIVs(uint iv1, uint ivs2, uint[] ivsLower, uint[] ivsUpper)
        {
            uint[] ivs = new uint[6];

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                if (iv >= ivsLower[x] && iv <= ivsUpper[x])
                    ivs[x] = iv;
                else
                    return null;
            }

            uint iV = (ivs2 >> 5) & 31;
            if (iV >= ivsLower[3] && iV <= ivsUpper[3])
                ivs[3] = iV;
            else
                return null;

            iV = (ivs2 >> 10) & 31;
            if (iV >= ivsLower[4] && iV <= ivsUpper[4])
                ivs[4] = iV;
            else
                return null;

            iV = ivs2 & 31;
            if (iV >= ivsLower[5] && iV <= ivsUpper[5])
                ivs[5] = iV;
            else
                return null;

            return ivs;
        }

        private uint pidChk(uint frame)
        {
            return (rlist[(int)(frame + 2)] << 16) | rlist[(int)(frame + 1)];
        }
        #endregion

        private void filterSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint pid, uint nature, uint seed, int method, int slot)
        {
            String shiny = "";
            if (checkBoxShiny.Checked)
            {
                if (!isShiny(pid))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null && (!hiddenPowerList.Contains(actualHP)))
                return;

            uint ability = pid & 1;
            if (abilityFilter != 0)
                if (ability != (abilityFilter - 1))
                    return;

            uint gender = pid & 255;
            switch (gender)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, slot);
        }

        private void addSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint pid, String shiny, uint seed, int slot)
        {
            if (shiny == "")
                if (isShiny(pid))
                    shiny = "!!!";

            wildSlots.Add(new WildSlots
            {
                Seed = seed.ToString("X"),
                Slot = slot,
                PID = pid.ToString("X"),
                Shiny = shiny,
                Nature = Natures[nature],
                Ability = ability,
                Hp = hp,
                Atk = atk,
                Def = def,
                SpA = spa,
                SpD = spd,
                Spe = spe,
                Hidden = hiddenPowers[hP],
                Power = calcHPPower(hp, atk, def, spa, spd, spe),
                Eighth = gender < 31 ? 'F' : 'M',
                Quarter = gender < 64 ? 'F' : 'M',
                Half = gender < 126 ? 'F' : 'M',
                Three_Fourths = gender < 191 ? 'F' : 'M'
            });
        }

        #region Helper Methods

        private void comboBoxMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = comboBoxMethod.SelectedIndex;
            if (num == 0 || num == 1 || num == 2)
            {
                comboBoxType.Visible = false;
                label15.Visible = false;
                comboBoxSlots.Visible = false;
                label5.Visible = false;
                anySlots.Visible = false;
            }
            else
            {
                comboBoxType.Visible = true;
                label15.Visible = true;
                comboBoxSlots.Visible = true;
                label5.Visible = true;
                anySlots.Visible = true;
            }
        }

        private void getEncounterSlot(uint initialSeed, uint pid, out int slot, out uint seed)
        {
            uint nature = pid - 25 * (pid / 25);
            var rng = new PokeRngR(initialSeed);
            rng.GetNext32BitNumber();
            uint searchNature = rng.GetNext16BitNumber() % 25;

            while (searchNature != nature)
            {
                rng.GetNext32BitNumber();
                searchNature = rng.GetNext16BitNumber() % 25;
            }

            rng.GetNext32BitNumber(2);
            slot = EncounterSlotCalc.encounterSlot(rng.Seed, FrameType.MethodH1, encounterType);
            seed = rng.GetNext32BitNumber();
        }

        private uint validatePID(uint seed)
        {
            var rng = new PokeRng(seed);
            rng.GetNext32BitNumber(2);
            uint nature = rng.GetNext16BitNumber() % 25;
            uint pid = rng.GetNext16BitNumber() | (rng.GetNext32BitNumber() & 0xFFFF0000);

            while (pid % 25 != nature)
                pid = rng.GetNext16BitNumber() | (rng.GetNext32BitNumber() & 0xFFFF0000);

            return pid;
        }

        private EncounterType getEncounterType(int num)
        {
            switch (num)
            {
                case 0:
                    return EncounterType.Wild;
                case 1:
                    return EncounterType.WildSurfing;
                case 2:
                    return EncounterType.WildOldRod;
                case 3:
                    return EncounterType.WildGoodRod;
                default:
                    return EncounterType.WildSuperRod;
            }
        }

        private void anyHiddenPower_Click(object sender, EventArgs e)
        {
            comboBoxHiddenPower.ClearSelection();
        }

        private void hp31Quick_Click(object sender, EventArgs e)
        {
            hpValue.Text = "31";
            hpLogic.SelectedIndex = 0;
        }

        private void hp30Quick_Click(object sender, EventArgs e)
        {
            hpValue.Text = "30";
            hpLogic.SelectedIndex = 0;
        }

        private void hp30Above_Click(object sender, EventArgs e)
        {
            hpValue.Text = "30";
            hpLogic.SelectedIndex = 1;
        }

        private void atk31Quick_Click(object sender, EventArgs e)
        {
            atkValue.Text = "31";
            atkLogic.SelectedIndex = 0;
        }

        private void atk30Quick_Click(object sender, EventArgs e)
        {
            atkValue.Text = "30";
            atkLogic.SelectedIndex = 0;
        }

        private void atk30Above_Click(object sender, EventArgs e)
        {
            atkValue.Text = "30";
            atkLogic.SelectedIndex = 1;
        }

        private void def31Quick_Click(object sender, EventArgs e)
        {
            defValue.Text = "31";
            defLogic.SelectedIndex = 0;
        }

        private void def30Quick_Click(object sender, EventArgs e)
        {
            defValue.Text = "30";
            defLogic.SelectedIndex = 0;
        }

        private void def30Above_Click(object sender, EventArgs e)
        {
            defValue.Text = "30";
            defLogic.SelectedIndex = 1;
        }

        private void spa31Quick_Click(object sender, EventArgs e)
        {
            spaValue.Text = "31";
            spaLogic.SelectedIndex = 0;
        }

        private void spa30Quick_Click(object sender, EventArgs e)
        {
            spaValue.Text = "30";
            spaLogic.SelectedIndex = 0;
        }

        private void spa30Above_Click(object sender, EventArgs e)
        {
            spaValue.Text = "30";
            spaLogic.SelectedIndex = 1;
        }

        private void spd31Quick_Click(object sender, EventArgs e)
        {
            spdValue.Text = "31";
            spdLogic.SelectedIndex = 0;
        }

        private void spd30Quick_Click(object sender, EventArgs e)
        {
            spdValue.Text = "30";
            spdLogic.SelectedIndex = 0;
        }

        private void spd30Above_Click(object sender, EventArgs e)
        {
            spdValue.Text = "30";
            spdLogic.SelectedIndex = 1;
        }

        private void spe31Quick_Click(object sender, EventArgs e)
        {
            speValue.Text = "31";
            speLogic.SelectedIndex = 0;
        }

        private void spe30Quick_Click(object sender, EventArgs e)
        {
            speValue.Text = "30";
            speLogic.SelectedIndex = 0;
        }

        private void spe30Above_Click(object sender, EventArgs e)
        {
            speValue.Text = "30";
            speLogic.SelectedIndex = 1;
        }

        private void hpClear_Click(object sender, EventArgs e)
        {
            hpValue.Text = "0";
            hpLogic.SelectedIndex = 1;
        }

        private void atkClear_Click(object sender, EventArgs e)
        {
            atkValue.Text = "0";
            atkLogic.SelectedIndex = 1;
        }

        private void defClear_Click(object sender, EventArgs e)
        {
            defValue.Text = "0";
            defLogic.SelectedIndex = 1;
        }

        private void spaClear_Click(object sender, EventArgs e)
        {
            spaValue.Text = "0";
            spaLogic.SelectedIndex = 1;
        }

        private void spdClear_Click(object sender, EventArgs e)
        {
            spdValue.Text = "0";
            spdLogic.SelectedIndex = 1;
        }

        private void speClear_Click(object sender, EventArgs e)
        {
            speValue.Text = "0";
            speLogic.SelectedIndex = 1;
        }

        private String[] addHP()
        {
            String[] temp = new String[]
                {
                    "Fighting",
                    "Flying",
                    "Poison",
                    "Ground",
                    "Rock",
                    "Bug",
                    "Ghost",
                    "Steel",
                    "Fire",
                    "Water",
                    "Grass",
                    "Electric",
                    "Psychic",
                    "Ice",
                    "Dragon",
                    "Dark"
                };
            return temp;
        }

        private void setComboBox()
        {
            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = true;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = false;
            comboBoxSlots.CheckBoxItems[0].Checked = true;
            comboBoxSlots.CheckBoxItems[0].Checked = false;
            comboBoxMethod.SelectedIndex = 0;
            comboBoxGender.SelectedIndex = 0;
            comboBoxAbility.SelectedIndex = 0;
            hpLogic.SelectedIndex = 0;
            atkLogic.SelectedIndex = 0;
            defLogic.SelectedIndex = 0;
            spaLogic.SelectedIndex = 0;
            spdLogic.SelectedIndex = 0;
            speLogic.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            comboBoxNature.ClearSelection();
        }

        private void anyHiddenPower_Click_1(object sender, EventArgs e)
        {
            comboBoxHiddenPower.ClearSelection();
        }

        private void anySlots_Click(object sender, EventArgs e)
        {
            comboBoxSlots.ClearSelection();
        }

        private void getIVs(out uint[] IVsLower, out uint[] IVsUpper)
        {
            IVsLower = new uint[6];
            IVsUpper = new uint[6];

            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            uint.TryParse(hpValue.Text, out hp);
            uint.TryParse(atkValue.Text, out atk);
            uint.TryParse(defValue.Text, out def);
            uint.TryParse(spaValue.Text, out spa);
            uint.TryParse(spdValue.Text, out spd);
            uint.TryParse(speValue.Text, out spe);

            uint[] ivs = { hp, atk, def, spa, spd, spe };
            int[] ivsLogic = { hpLogic.SelectedIndex, atkLogic.SelectedIndex, defLogic.SelectedIndex, spaLogic.SelectedIndex, spdLogic.SelectedIndex, speLogic.SelectedIndex };

            for (int x = 0; x < 6; x++)
            {
                if (ivsLogic[x] == 0)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = ivs[x];
                }
                else if (ivsLogic[x] == 1)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = 31;
                }
                else
                {
                    IVsLower[x] = 0;
                    IVsUpper[x] = ivs[x];
                }
            }
        }

        private uint getAbility()
        {
            if (comboBoxAbility.InvokeRequired)
                return (uint)comboBoxAbility.Invoke(new Func<uint>(getAbility));
            else
                return (uint)comboBoxAbility.SelectedIndex;
        }

        private uint getGender()
        {
            if (comboBoxGender.InvokeRequired)
                return (uint)comboBoxGender.Invoke(new Func<uint>(getGender));
            else
                return (uint)comboBoxGender.SelectedIndex;
        }

        private uint forward(uint seed)
        {
            return ((seed * 0x41c64e6d + 0x6073) & 0xFFFFFFFF);
        }

        private uint reverse(uint seed)
        {
            return ((seed * 0xeeb9eb65 + 0xa3561a1) & 0xFFFFFFFF);
        }

        private uint populateRNGR(uint seed)
        {
            seed = forward(seed);
            slist.Add(seed);
            rlist.Add((seed >> 16));
            return seed;
        }

        private void populate(uint seed, uint srange)
        {
            uint s = seed;
            for (uint x = 0; x < (srange + 10); x++)
                s = populateRNGR(s);
        }

        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = dataGridViewResult.AutoResizeColumns;
            try
            {
                bool alive = true;
                while (alive)
                {
                    if (refresh)
                    {
                        if (dataGridViewResult != null)
                        {
                            Invoke(gridUpdate);
                            refresh = false;
                        }
                    }
                    if (searchThread == null || !searchThread.IsAlive)
                    {
                        alive = false;
                    }

                    Thread.Sleep(500);
                }
            }
            finally
            {
                if (dataGridViewResult != null)
                {
                    Invoke(gridUpdate);
                    Invoke(resizeGrid);
                }
            }
        }

        private void dataGridUpdate()
        {
            binding.ResetBindings(false);
        }

        private bool isShiny(uint PID)
        {
            return (((PID >> 16) ^ (PID & 0xffff)) >> 3) == shinyval;
        }

        private int calcHPPower(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return (int)(30 + ((((hp >> 1) & 1) + 2 * ((atk >> 1) & 1) + 4 * ((def >> 1) & 1) + 8 * ((spe >> 1) & 1) + 16 * ((spa >> 1) & 1) + 32 * ((spd >> 1) & 1)) * 40 / 63));
        }

        private void normalSpreadRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (normalSpreadRadioButton.Checked == true)
            {
                maskedTextBox21.ReadOnly = true;
                maskedTextBox21.TabStop = false;
            }
            else
            {
                maskedTextBox21.ReadOnly = false;
                maskedTextBox21.TabStop = true;
            }

        }
        
        private void maskedTextBox21_Enter(object sender, EventArgs e)
        {
            if (maskedTextBox21.ReadOnly == true)
            {
                label99.Focus();
            }
        }

        private uint calcHP(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return ((((hp & 1) + 2 * (atk & 1) + 4 * (def & 1) + 8 * (spe & 1) + 16 * (spa & 1) + 32 * (spd & 1)) * 15) / 63);
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                status.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }
        #endregion
        #endregion
    }
}
