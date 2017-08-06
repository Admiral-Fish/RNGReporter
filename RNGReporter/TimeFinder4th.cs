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
using Microsoft.Win32;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class TimeFinder4th : Form
    {
        private static readonly object threadLock = new object();

        // Column identifiers
        // We use these so we don't have to make string compares
        // for every cell that needs formatting, and edit column
        // indices every time we change columns
        private int CapHPIndex;
        private int CapNatureIndex;
        private int CapSpeedIndex;
        private int cpus;
        private FrameCompare frameCompare;
        private FrameGenerator generator;
        private uint id;

        // Multithreading
        private List<IFrameCapture> iframes;
        private List<IFrameBreeding> iframesEggIVs;
        private List<IFrameEggPID> iframesEggShiny;
        private Thread[] jobs;
        private BindingSource listBindingCap;
        private BindingSource listBindingEgg;
        private BindingSource listBindingShiny;
        private ulong progressFound;
        private ulong progressSearched;
        private ulong progressTotal;
        private bool refreshQueue;
        private uint sid;
        private int tabPage;
        private int targetFrameIndex;
        private EventWaitHandle waitHandle;

        public TimeFinder4th(uint id, uint sid)
        {
            this.id = id;
            this.sid = sid;

            InitializeComponent();

            if (Math.Abs(Settings.Default.DPI - 96) > float.Epsilon)
            {
                float ratio = Settings.Default.DPI/96;

                foreach (DataGridViewColumn column in dataGridViewCapValues.Columns)
                {
                    column.Width = (int) (column.Width*ratio + 1);
                }

                foreach (DataGridViewColumn column in dataGridViewShinyResults.Columns)
                {
                    column.Width = (int) (column.Width*ratio + 1);
                }

                foreach (DataGridViewColumn column in dataGridViewEggIVValues.Columns)
                {
                    column.Width = (int) (column.Width*ratio + 1);
                }
            }
        }

        public int TabPage
        {
            get { return tabPage; }
            set { tabPage = value; }
        }

        private void TimeFinder4th_Load(object sender, EventArgs e)
        {
            // Add smart comboBox items
            // Would be nice if we left these in the Designer file
            // But Visual Studio seems to like deleting them without warning

            comboBoxMethod.Items.AddRange(new object[]
                {
                    new ComboBoxItem("Method 1", FrameType.Method1),
                    new ComboBoxItem("Method J", FrameType.MethodJ),
                    new ComboBoxItem("Method K", FrameType.MethodK),
                    new ComboBoxItem("Wondercard IVs", FrameType.WondercardIVs),
                    new ComboBoxItem("PokeWalker IVs", FrameType.WondercardIVs),
                    new ComboBoxItem("Chained Spreads", FrameType.ChainedShiny)
                });

            var ability = new[]
                {
                    new ComboBoxItem("Any", -1),
                    new ComboBoxItem("Ability 0", 0),
                    new ComboBoxItem("Ability 1", 1)
                };

            var shinyNatureList = new BindingSource {DataSource = Objects.Nature.NatureDropDownCollection()};
            comboBoxShinyNature.DataSource = shinyNatureList;

            comboBoxNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());

            comboBoxAbility.DataSource = ability;
            comboBoxShinyAbility.DataSource = ability;

            comboBoxShinyGender.DataSource = GenderFilter.GenderFilterCollection();

            Settings.Default.PropertyChanged += ChangeLanguage;
            SetLanguage();

            // Obtain the indices of the datagrid columns by name,
            // so we don't have to keep track of them

            CapHPIndex = CapHP.Index;
            CapSpeedIndex = CapSpe.Index;
            CapNatureIndex = Nature.Index;

            comboBoxMethod.SelectedIndex = 0;
            comboBoxNature.SelectedIndex = 0;
            comboBoxAbility.SelectedIndex = 0;
            comboBoxCapGender.SelectedIndex = 0;
            comboBoxCapGenderRatio.SelectedIndex = 0;

            comboBoxEncounterType.SelectedIndex = 0;
            // This is a rather hackish way of making the custom control
            // display the desired text upon loading
            comboBoxEncounterSlot.CheckBoxItems[0].Checked = true;
            comboBoxEncounterSlot.CheckBoxItems[0].Checked = false;

            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;

            comboBoxShinyNature.SelectedIndex = 0;
            comboBoxShinyAbility.SelectedIndex = 0;
            comboBoxShinyGender.SelectedIndex = 0;

            dataGridViewEggIVValues.AutoGenerateColumns = false;
            Seed.DefaultCellStyle.Format = "X8";
            Date.DefaultCellStyle.Format = "MM/dd/yy HH:mm:ss";

            dataGridViewCapValues.AutoGenerateColumns = false;
            CapSeed.DefaultCellStyle.Format = "X8";
            PID.DefaultCellStyle.Format = "X8";

            dataGridViewShinyResults.AutoGenerateColumns = false;
            EggSeed.DefaultCellStyle.Format = "X8";
            EggPID.DefaultCellStyle.Format = "X8";

            maskedTextBoxID.Text = id.ToString("00000");
            maskedTextBoxSID.Text = sid.ToString("00000");

            maskedTextBoxShinyID.Text = id.ToString("00000");
            maskedTextBoxShinySecretID.Text = sid.ToString("00000");

            maskedTextBoxSID.Text = Settings.Default.SID;
            maskedTextBoxID.Text = Settings.Default.ID;

            maskedTextBoxShinySecretID.Text = Settings.Default.SID;
            maskedTextBoxShinyID.Text = Settings.Default.ID;

            tabControl.SelectTab(tabPage);

            //  Load all of our items from the registry
            RegistryKey registrySoftware = Registry.CurrentUser.OpenSubKey("Software", true);
            if (registrySoftware != null)
            {
                RegistryKey registryRngReporter = registrySoftware.OpenSubKey("RNGReporter");

                if (Settings.Default.LastVersion < MainForm.VersionNumber && registryRngReporter != null)
                {
                    maskedTextBoxYear.Text =
                        (string) registryRngReporter.GetValue("4th_year", DateTime.Now.Year.ToString());
                    maskedTextBoxMinOffset.Text = (string) registryRngReporter.GetValue("4th_min_offset", "5");
                    maskedTextBoxMaxOffset.Text = (string) registryRngReporter.GetValue("4th_max_offset", "5");
                    maskedTextBoxMinDelay.Text = (string) registryRngReporter.GetValue("4th_delaymin", "600");
                    maskedTextBoxMaxDelay.Text = (string) registryRngReporter.GetValue("4th_delaymax", "610");

                    maskedTextBoxCapYear.Text =
                        (string) registryRngReporter.GetValue("4th_cap_year", DateTime.Now.Year.ToString());
                    maskedTextBoxCapMaxOffset.Text = (string) registryRngReporter.GetValue("4th_cap_offset", "1000");
                    maskedTextBoxCapMinDelay.Text = (string) registryRngReporter.GetValue("4th_cap_delaymin", "600");
                    maskedTextBoxCapMaxDelay.Text = (string) registryRngReporter.GetValue("4th_cap_delaymax", "610");

                    maskedTextBoxShinyYear.Text =
                        (string) registryRngReporter.GetValue("4th_shiny_year", DateTime.Now.Year.ToString());
                    maskedTextBoxShinyMinDelay.Text = (string) registryRngReporter.GetValue("4th_shiny_delaymin", "600");
                    maskedTextBoxShinyMaxDelay.Text = (string) registryRngReporter.GetValue("4th_shiny_delaymax", "610");

                    cpus =
                        Int16.Parse((string) registryRngReporter.GetValue("cpus", Environment.ProcessorCount.ToString()));
                    if (cpus < 1)
                    {
                        cpus = 1;
                    }

                    if (id == 0 && sid == 0)
                    {
                        maskedTextBoxSID.Text = (string) registryRngReporter.GetValue("sid", "0");
                        maskedTextBoxID.Text = (string) registryRngReporter.GetValue("id", "0");

                        maskedTextBoxShinySecretID.Text = (string) registryRngReporter.GetValue("sid", "0");
                        maskedTextBoxShinyID.Text = (string) registryRngReporter.GetValue("id", "0");
                    }
                }
                    //load from settings
                else
                {
                    maskedTextBoxYear.Text = Settings.Default.fYear;
                    maskedTextBoxMinOffset.Text = Settings.Default.fMinOffset;
                    maskedTextBoxMaxOffset.Text = Settings.Default.fMaxOffset;
                    maskedTextBoxMinDelay.Text = Settings.Default.fMinDelay;
                    maskedTextBoxMaxDelay.Text = Settings.Default.fMaxDelay;

                    maskedTextBoxCapYear.Text = Settings.Default.fCapYear;
                    maskedTextBoxCapMaxOffset.Text = Settings.Default.fCapOffset;
                    maskedTextBoxCapMinDelay.Text = Settings.Default.fCapDelayMin;
                    maskedTextBoxCapMaxDelay.Text = Settings.Default.fCapDelayMax;

                    maskedTextBoxShinyYear.Text = Settings.Default.fShinyYear;
                    maskedTextBoxShinyMinDelay.Text = Settings.Default.fShinyDelayMin;
                    maskedTextBoxShinyMaxDelay.Text = Settings.Default.fShinyDelayMax;

                    cpus = Settings.Default.CPUs;
                    if (cpus < 1)
                    {
                        cpus = 1;
                    }
                }
            }
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
            switch ((Language) Settings.Default.Language)
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

            Nature.DefaultCellStyle = CellStyle;
            HiddenPower.DefaultCellStyle = CellStyle;
            ShinyNature.DefaultCellStyle = CellStyle;
            EncounterSlot.DefaultCellStyle = CellStyle;
            EncounterMod.DefaultCellStyle = CellStyle;

            comboBoxNature.Font = CellStyle.Font;
            comboBoxShinyNature.Font = CellStyle.Font;

            for (int checkBoxIndex = 1; checkBoxIndex < comboBoxNature.Items.Count; checkBoxIndex++)
            {
                comboBoxNature.CheckBoxItems[checkBoxIndex].Text =
                    (comboBoxNature.CheckBoxItems[checkBoxIndex].ComboBoxItem).ToString();
                comboBoxNature.CheckBoxItems[checkBoxIndex].Font = CellStyle.Font;
            }

            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;

            ((BindingSource) comboBoxShinyNature.DataSource).ResetBindings(false);

            dataGridViewCapValues.Refresh();
            dataGridViewShinyResults.Refresh();
        }

        private void PlatinumTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.fYear = maskedTextBoxYear.Text;
            Settings.Default.fMinOffset = maskedTextBoxMinOffset.Text;
            Settings.Default.fMaxOffset = maskedTextBoxMaxOffset.Text;
            Settings.Default.fMinDelay = maskedTextBoxMinDelay.Text;
            Settings.Default.fMaxDelay = maskedTextBoxMaxDelay.Text;

            Settings.Default.fCapYear = maskedTextBoxCapYear.Text;
            Settings.Default.fCapOffset = maskedTextBoxCapMaxOffset.Text;
            Settings.Default.fCapDelayMin = maskedTextBoxCapMinDelay.Text;
            Settings.Default.fCapDelayMax = maskedTextBoxCapMaxDelay.Text;

            Settings.Default.fShinyYear = maskedTextBoxShinyYear.Text;
            Settings.Default.fShinyDelayMin = maskedTextBoxShinyMinDelay.Text;
            Settings.Default.fShinyDelayMax = maskedTextBoxShinyMaxDelay.Text;
            Settings.Default.Save();

            e.Cancel = true;

            buttonCapGenerate.Enabled = true;
            buttonShinyGenerate.Enabled = true;

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

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            if (maskedTextBoxYear.Text != "")
            {
                year = int.Parse(maskedTextBoxYear.Text);

                //  Need to validate the year here
                if (year < 2000)
                {
                    MessageBox.Show("You must enter a year greater than 1999.", "Please Enter a Valid Year",
                                    MessageBoxButtons.OK);
                    return;
                }
            }

            //  We are going to need to make sure that 
            //  this matches the offset that we show
            //  on the main frame screen so that people
            //  will be able to see.

            //  Default to 5, some users might want lower
            //  so we should save this in the registry
            if (maskedTextBoxMinOffset.Text != "")
            {
                maskedTextBoxMaxOffset.Focus();
                maskedTextBoxMinOffset.SelectAll();
            }

            uint minOffset = uint.Parse(maskedTextBoxMinOffset.Text);

            //now with min and max to avoid annoying manual searching
            //default to single value search though 
            if (maskedTextBoxMaxOffset.Text != "")
            {
                maskedTextBoxMaxOffset.Focus();
                maskedTextBoxMinOffset.SelectAll();
            }

            uint maxOffset = uint.Parse(maskedTextBoxMaxOffset.Text);

            if (minOffset > maxOffset)
            {
                maskedTextBoxMinOffset.Focus();
                maskedTextBoxMinOffset.SelectAll();
                return;
            }

            //  Default these to this value, but save to
            //  the registry so we can not have to redo.
            uint highdelay = 610;
            if (maskedTextBoxMaxDelay.Text != "")
                highdelay = uint.Parse(maskedTextBoxMaxDelay.Text);

            uint lowdelay = 600;
            if (maskedTextBoxMinDelay.Text != "")
                lowdelay = uint.Parse(maskedTextBoxMinDelay.Text);

            generator = new FrameGenerator();

            var parentA = new uint[6];
            var parentB = new uint[6];

            if (maskedTextBoxHPA.Text == "")
                maskedTextBoxHPA.Text = "0";

            if (maskedTextBoxAtkA.Text == "")
                maskedTextBoxAtkA.Text = "0";

            if (maskedTextBoxDefA.Text == "")
                maskedTextBoxDefA.Text = "0";

            if (maskedTextBoxSpAA.Text == "")
                maskedTextBoxSpAA.Text = "0";

            if (maskedTextBoxSpDA.Text == "")
                maskedTextBoxSpDA.Text = "0";

            if (maskedTextBoxSpeA.Text == "")
                maskedTextBoxSpeA.Text = "0";

            if (maskedTextBoxHPB.Text == "")
                maskedTextBoxHPB.Text = "0";

            if (maskedTextBoxAtkB.Text == "")
                maskedTextBoxAtkB.Text = "0";

            if (maskedTextBoxDefB.Text == "")
                maskedTextBoxDefB.Text = "0";

            if (maskedTextBoxSpAB.Text == "")
                maskedTextBoxSpAB.Text = "0";

            if (maskedTextBoxSpDB.Text == "")
                maskedTextBoxSpDB.Text = "0";

            if (maskedTextBoxSpeB.Text == "")
                maskedTextBoxSpeB.Text = "0";

            uint.TryParse(maskedTextBoxHPA.Text, out parentA[0]);
            uint.TryParse(maskedTextBoxAtkA.Text, out parentA[1]);
            uint.TryParse(maskedTextBoxDefA.Text, out parentA[2]);
            uint.TryParse(maskedTextBoxSpAA.Text, out parentA[3]);
            uint.TryParse(maskedTextBoxSpDA.Text, out parentA[4]);
            uint.TryParse(maskedTextBoxSpeA.Text, out parentA[5]);

            uint.TryParse(maskedTextBoxHPB.Text, out parentB[0]);
            uint.TryParse(maskedTextBoxAtkB.Text, out parentB[1]);
            uint.TryParse(maskedTextBoxDefB.Text, out parentB[2]);
            uint.TryParse(maskedTextBoxSpAB.Text, out parentB[3]);
            uint.TryParse(maskedTextBoxSpDB.Text, out parentB[4]);
            uint.TryParse(maskedTextBoxSpeB.Text, out parentB[5]);

            frameCompare = new FrameCompare(
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
                ivFiltersEgg.IVFilter,
                null,
                -1,
                false,
                true,
                new NoGenderFilter());

            generator.ParentA = parentA;
            generator.ParentB = parentB;

            //  Toggled based on the users selection
            if (radioButtonEggDPPt.Checked)
            {
                generator.FrameType = FrameType.DPPtBred;
                Flips.HeaderText = "Flip Sequence";
                Flips.DataPropertyName = "Flips";
            }
            else
            {
                generator.FrameType = FrameType.HGSSBred;
                Flips.HeaderText = "Elm Sequence";
                Flips.DataPropertyName = "ElmResponses";
            }

            Date.Visible = false;

            generator.InitialFrame = minOffset;
            generator.MaxResults = ((maxOffset - minOffset) + 1U);

            iframesEggIVs = new List<IFrameBreeding>();
            listBindingEgg = new BindingSource {DataSource = iframesEggIVs};
            dataGridViewEggIVValues.DataSource = listBindingEgg;

            progressSearched = 0;
            progressFound = 0;
            progressTotal = (255 * 24 * (highdelay - lowdelay + 1) * generator.MaxResults);

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(() => Generate4thGenEggIVsJob(lowdelay, highdelay, (uint) year));
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingEgg, dataGridViewEggIVValues, generator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;

            buttonCapGenerate.Enabled = false;
            buttonShinyGenerate.Enabled = false;
            buttonEggGenerate.Enabled = false;
            returnToResultsToolStripMenuItem.Visible = false;

            dataGridViewEggIVValues.Focus();
        }

        private void dataGridViewValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "HP" ||
                dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "Atk" ||
                dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "Def" ||
                dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "SpA" ||
                dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "SpD" ||
                dataGridViewEggIVValues.Columns[e.ColumnIndex].Name == "Spe")
            {
                if ((string) e.Value == "30" || (string) e.Value == "31")
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if ((string) e.Value == "0")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }

                if ((string) e.Value == "A" || (string) e.Value == "B")
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void generateAdjacentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var initialiframe = (IFrameBreeding) dataGridViewEggIVValues.SelectedRows[0].DataBoundItem;

            //  We need to display a dialog that is going to let the user
            //  decide which adjacent spreads that we want to show here.
            var adjacent = new PlatinumTimeAdjacent(initialiframe.Seed, initialiframe.Offset,
                                                    DateTime.Now.Year);

            if (adjacent.ShowDialog() == DialogResult.OK)
            {
                //  Instantiate our Generator and a dummy frame compare record
                //  one time here so we can re-use it for all of our later work
                var adjacentGenerator = new FrameGenerator();

                var parentA = new uint[6];
                var parentB = new uint[6];

                uint.TryParse(maskedTextBoxHPA.Text, out parentA[0]);
                uint.TryParse(maskedTextBoxAtkA.Text, out parentA[1]);
                uint.TryParse(maskedTextBoxDefA.Text, out parentA[2]);
                uint.TryParse(maskedTextBoxSpAA.Text, out parentA[3]);
                uint.TryParse(maskedTextBoxSpDA.Text, out parentA[4]);
                uint.TryParse(maskedTextBoxSpeA.Text, out parentA[5]);

                uint.TryParse(maskedTextBoxHPB.Text, out parentB[0]);
                uint.TryParse(maskedTextBoxAtkB.Text, out parentB[1]);
                uint.TryParse(maskedTextBoxDefB.Text, out parentB[2]);
                uint.TryParse(maskedTextBoxSpAB.Text, out parentB[3]);
                uint.TryParse(maskedTextBoxSpDB.Text, out parentB[4]);
                uint.TryParse(maskedTextBoxSpeB.Text, out parentB[5]);

                adjacentGenerator.ParentA = parentA;
                adjacentGenerator.ParentB = parentB;

                var adjacentFrameCompare = new FrameCompare(
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
                    new NoGenderFilter());

                var adjacentFrames = new List<IFrameBreeding>();

                int matchCount = 0;

                //  Now we need to get the values back from this and run a special generation
                //  with just these variables.  We want it to be sorted by date (with delay)
                //  and for items to be put in offset order.
                foreach (DateTime adjacentTime in adjacent.AdjacentTimeList)
                {
                    for (uint rdelay = adjacent.ReturnMinDelay; rdelay <= adjacent.ReturnMaxDelay; rdelay++)
                    {
                        //  Get the information (year, month, date from the frame) and build
                        //  our initial seed to feed to the Frame Generator so that we can 
                        uint seed =
                            ((((uint) adjacentTime.Month*
                               (uint) adjacentTime.Day +
                               (uint) adjacentTime.Minute +
                               (uint) adjacentTime.Second)&0xFF) << 24) +
                            ((uint) adjacentTime.Hour << 16) +
                            ((uint) adjacentTime.Year - 2000 + rdelay);

                        //  Call a generator with a dummy compare object and go
                        //  up to the max delay and after this iterate through
                        //  the results to include min-max in a new list of frames
                        //  to show on the grid.
                        adjacentGenerator.InitialFrame = adjacent.ReturnMinOffset;
                        adjacentGenerator.MaxResults = adjacent.ReturnMaxOffset - adjacent.ReturnMinOffset + 1;
                        adjacentGenerator.InitialSeed = seed;
                        adjacentGenerator.FrameType = initialiframe.FrameType;

                        List<Frame> frames = adjacentGenerator.Generate(adjacentFrameCompare, 0, 0);

                        foreach (Frame frame in frames)
                        {
                            //  Create our PlatinumIFrame and then add this to the 
                            //  master collection that we are going to display to
                            //  the user.
                            var iframe = new IFrameBreeding
                                {
                                    Seed = seed,
                                    Offset = frame.Number,
                                    Delay = rdelay,
                                    Hp = frame.DisplayHpAlt,
                                    Atk = frame.DisplayAtkAlt,
                                    Def = frame.DisplayDefAlt,
                                    Spa = frame.DisplaySpaAlt,
                                    Spd = frame.DisplaySpdAlt,
                                    Spe = frame.DisplaySpeAlt,
                                    DisplayHpInh = frame.DisplayHp,
                                    DisplayAtkInh = frame.DisplayAtk,
                                    DisplayDefInh = frame.DisplayDef,
                                    DisplaySpaInh = frame.DisplaySpa,
                                    DisplaySpdInh = frame.DisplaySpd,
                                    DisplaySpeInh = frame.DisplaySpe,
                                    SeedTime = adjacentTime,
                                    FrameType = adjacentGenerator.FrameType
                                };

                            if (initialiframe.Seed == iframe.Seed)
                            {
                                if (targetFrameIndex != 0)
                                {
                                    if (initialiframe.Offset == iframe.Offset)
                                        targetFrameIndex = matchCount;
                                }
                                else
                                    targetFrameIndex = matchCount;
                            }

                            adjacentFrames.Add(iframe);

                            matchCount++;
                        }
                    }
                }

                Date.Visible = true;
                dataGridViewEggIVValues.DataSource = adjacentFrames;
                returnToResultsToolStripMenuItem.Visible = true;

                //  Select the row of the initial frame that was
                //  selected when the adjacent command was run

                dataGridViewEggIVValues.FirstDisplayedScrollingRowIndex = targetFrameIndex;
                dataGridViewEggIVValues.Rows[targetFrameIndex].Selected = true;
            }
        }

        private void outputResultsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
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

                List<IFrameBreeding> frames = iframesEggIVs;

                if (iframesEggIVs != null && iframesEggIVs.Count > 0)
                {
                    //  Need to know what sort of display we are doing here.  The
                    //  easiset thing to do is to take the value of the dropdown.
                    var writer = new TXTWriter(dataGridViewEggIVValues);

                    writer.Generate(saveFileDialogTxt.FileName, frames);
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewEggIVValues.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void contextMenuStripEggPid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewShinyResults.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void contextMenuStripCap_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewEggIVValues.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewEggIVValues.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewEggIVValues.ClearSelection();

                        (dataGridViewEggIVValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void setAsTargetFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            targetFrameIndex = dataGridViewEggIVValues.SelectedRows[0].Index;
        }

        private void jumpToTargetFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewEggIVValues.FirstDisplayedScrollingRowIndex = targetFrameIndex;
            dataGridViewEggIVValues.Rows[targetFrameIndex].Selected = true;
        }

        //  Capture code begins here -- This is all of the good stuff for 
        //  captured Pokemon.

        private void buttonCapGenerate_Click(object sender, EventArgs e)
        {
            if (!uint.TryParse(maskedTextBoxID.Text, out id))
                id = 0;

            if (!uint.TryParse(maskedTextBoxSID.Text, out sid))
                sid = 0;

            iframes = new List<IFrameCapture>();
            listBindingCap = new BindingSource {DataSource = iframes};
            dataGridViewCapValues.DataSource = listBindingCap;
            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            //  We want to get our year and offset ranges here so
            //  that we can have some values for our looping.
            //  Default these to this value, but save to
            //  the registry so we can not have to redo.
            uint maxDelay = 2000;
            if (maskedTextBoxCapMaxDelay.Text != "")
                maxDelay = uint.Parse(maskedTextBoxCapMaxDelay.Text);

            uint minDelay = 600;
            if (maskedTextBoxCapMinDelay.Text != "")
                minDelay = uint.Parse(maskedTextBoxCapMinDelay.Text);

            var year = (uint) DateTime.Now.Year;
            if (maskedTextBoxCapYear.Text != "")
            {
                year = uint.Parse(maskedTextBoxCapYear.Text);

                //  Need to validate the year here
                if (year < 2000)
                {
                    MessageBox.Show("You must enter a year greater than 1999.", "Please Enter a Valid Year",
                                    MessageBoxButtons.OK);
                    return;
                }
            }

            uint maxOffset = 1000;
            if (maskedTextBoxCapMaxOffset.Text != "")
                maxOffset = uint.Parse(maskedTextBoxCapMaxOffset.Text);
            else
                maskedTextBoxCapMaxOffset.Text = "1000";

            uint minOffset = 1;
            if (maskedTextBoxCapMinOffset.Text != "")
                minOffset = uint.Parse(maskedTextBoxCapMinOffset.Text);
            else
                maskedTextBoxCapMinOffset.Text = "1";

            if (minOffset > maxOffset)
            {
                maskedTextBoxCapMinOffset.Focus();
                maskedTextBoxCapMinOffset.SelectAll();
                return;
            }

            if (comboBoxMethod.SelectedIndex == 4)
            {
                year = 2000;
                minDelay = 0;
                maxDelay = 100;
            }

            generator = new FrameGenerator
            {
                FrameType = (FrameType)((ComboBoxItem)comboBoxMethod.SelectedItem).Reference,
                EncounterType = EncounterTypeCalc.EncounterString(comboBoxEncounterType.Text),
                EncounterMod = Objects.EncounterMod.Search,
                InitialFrame = minOffset,
                MaxResults = maxOffset
            };

            if (comboBoxEncounterType.SelectedItem.ToString() == "Bug-Catching Contest")
            {
                if (preDexRadioButton.Checked)
                    generator.EncounterType = EncounterType.BugCatchingContestPreDex;
                else if (tuesdayRadioButton.Checked)
                    generator.EncounterType = EncounterType.BugBugCatchingContestTues;
                else if (thursdayRadioButton.Checked)
                    generator.EncounterType = EncounterType.BugCatchingContestThurs;
                else if (saturdayRadioButton.Checked)
                    generator.EncounterType = EncounterType.BugCatchingContestSat;
            }

            // Now that each combo box item is a custom object containing the FrameType reference
            // We can simply retrieve the FrameType from the selected item


            uint minEfgh = (year - 2000) + minDelay;
            uint maxEfgh = (year - 2000) + maxDelay;

            //  Build up a FrameComparer

            IVFilter ivfilter = ivFiltersCapture.IVFilter;

            List<int> encounterSlots = null;
            if (comboBoxEncounterSlot.Text != "Any" && comboBoxEncounterSlot.CheckBoxItems.Count > 0)
            {
                encounterSlots = new List<int>();
                for (int i = 0; i < comboBoxEncounterSlot.CheckBoxItems.Count; i++)
                {
                    if (comboBoxEncounterSlot.CheckBoxItems[i].Checked)
                        // We have to subtract 1 because this custom control contains a hidden item for text display
                        encounterSlots.Add(i - 1);
                }
            }

            List<uint> natures = null;
            if (comboBoxNature.Text != "Any" && comboBoxNature.CheckBoxItems.Count > 0)
            {
                natures = new List<uint>();
                for (int i = 0; i < comboBoxNature.CheckBoxItems.Count; i++)
                {
                    if (comboBoxNature.CheckBoxItems[i].Checked)
                        natures.Add((uint) (((Nature) comboBoxNature.CheckBoxItems[i].ComboBoxItem).Number));
                }
            }

            if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.Method1) ||
                ((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.ChainedShiny))
            {
                Hour.Visible = true;
                EncounterMod.Visible = false;
                EncounterSlot.Visible = false;
                PID.Visible = true;
                Shiny.Visible = true;
                Nature.Visible = true;
                Ability.Visible = true;
                CapHP.Visible = true;
                CapAtk.Visible = true;
                CapDef.Visible = true;
                CapSpA.Visible = true;
                CapSpD.Visible = true;
                CapSpe.Visible = true;
                HiddenPower.Visible = true;
                HiddenPowerPower.Visible = true;
                f25.Visible = true;
                f50.Visible = true;
                f75.Visible = true;
                f125.Visible = true;

                frameCompare = new FrameCompare(
                    ivfilter,
                    natures,
                    (int) ((ComboBoxItem) comboBoxAbility.SelectedItem).Reference,
                    checkBoxShinyOnly.Checked,
                    false,
                    false,
                    null,
                    constructGenderFilter(comboBoxCapGender));
            }

            if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.MethodJ) ||
                ((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.MethodK))
            {
                Hour.Visible = true;
                EncounterMod.Visible = true;
                EncounterSlot.Visible = generator.EncounterType !=
                                        EncounterType.Stationary;
                PID.Visible = true;
                Shiny.Visible = true;
                Nature.Visible = true;
                Ability.Visible = true;
                CapHP.Visible = true;
                CapAtk.Visible = true;
                CapDef.Visible = true;
                CapSpA.Visible = true;
                CapSpD.Visible = true;
                CapSpe.Visible = true;
                HiddenPower.Visible = true;
                HiddenPowerPower.Visible = true;
                f25.Visible = true;
                f50.Visible = true;
                f75.Visible = true;
                f125.Visible = true;

                frameCompare = new FrameCompare(
                    ivFiltersCapture.IVFilter,
                    natures,
                    (int) ((ComboBoxItem) comboBoxAbility.SelectedItem).Reference,
                    checkBoxShinyOnly.Checked,
                    false,
                    false,
                    encounterSlots,
                    constructGenderFilter(comboBoxCapGender));
            }

            if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.WondercardIVs))
            {
                Hour.Visible = true;
                EncounterMod.Visible = false;
                EncounterSlot.Visible = false;
                PID.Visible = false;
                Shiny.Visible = false;
                Nature.Visible = false;
                Ability.Visible = false;
                CapHP.Visible = true;
                CapAtk.Visible = true;
                CapDef.Visible = true;
                CapSpA.Visible = true;
                CapSpD.Visible = true;
                CapSpe.Visible = true;
                HiddenPower.Visible = true;
                HiddenPowerPower.Visible = true;
                f25.Visible = false;
                f50.Visible = false;
                f75.Visible = false;
                f125.Visible = false;

                frameCompare = new FrameCompare(
                    ivfilter,
                    null,
                    -1,
                    false,
                    false,
                    false,
                    null,
                    new NoGenderFilter());
            }

            jobs = new Thread[1];

            var hpList = new List<uint>();
            var atkList = new List<uint>();
            var defList = new List<uint>();
            var spaList = new List<uint>();
            var spdList = new List<uint>();
            var speList = new List<uint>();

            for (uint iv = 0; iv <= 31; iv++)
            {
                if (frameCompare.CompareIV(ivfilter.hpCompare, iv, ivfilter.hpValue))
                    hpList.Add(iv);
                if (frameCompare.CompareIV(ivfilter.atkCompare, iv, ivfilter.atkValue))
                    atkList.Add(iv);
                if (frameCompare.CompareIV(ivfilter.defCompare, iv, ivfilter.defValue))
                    defList.Add(iv);
                if (frameCompare.CompareIV(ivfilter.spaCompare, iv, ivfilter.spaValue))
                    spaList.Add(iv);
                if (frameCompare.CompareIV(ivfilter.spdCompare, iv, ivfilter.spdValue))
                    spdList.Add(iv);
                if (frameCompare.CompareIV(ivfilter.speCompare, iv, ivfilter.speValue))
                    speList.Add(iv);
            }

            if (natures == null)
            {
                natures = new List<uint>();
                for (uint i = 0; i < 25; i++)
                    natures.Add(i);
            }

            jobs[0] = new Thread(() => Generate4thGenCapJob(hpList, atkList, defList, spaList, spdList, speList, natures, minEfgh, maxEfgh));
            jobs[0].Start();

            progressTotal = (ulong)(hpList.Count * atkList.Count * defList.Count * spaList.Count * spdList.Count * speList.Count * natures.Count);
            var progressJob = new Thread(() => ManageProgress(listBindingCap, dataGridViewCapValues, generator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;

            buttonCapGenerate.Enabled = false;

            dataGridViewCapValues.Focus();
        }

        private void Generate4thGenCapJob(List<uint> hpList, List<uint> atkList,
                                          List<uint> defList,
                                          List<uint> spaList, List<uint> spdList,
                                          List<uint> speList,
                                          List<uint> natures, uint minEfgh, uint maxEfgh)
        {
            //if the lists are null we shouldn't be searching so do nothing
            if (hpList == null || atkList == null || defList == null || spaList == null || spdList == null ||
                speList == null)
                return;

            uint incrementFound = 1;
            if (natures != null)
                incrementFound = incrementFound*(uint) natures.Count;

            for (uint i = 0; i < 256; i++)
            {
                uint right = 0x41c64e6d * i + 0x6073;
                ushort val = (ushort)(right >> 16);
                generator.flags[val] = true;
                generator.low8[val] = (byte)i;
                --val;
                generator.flags[val] = true;
                generator.low8[val] = (byte)i;
            }

            foreach (uint hp in hpList)
            {
                foreach (uint atk in atkList)
                {
                    foreach (uint def in defList)
                    {
                        foreach (uint spa in spaList)
                        {
                            foreach (uint spd in spdList)
                            {
                                foreach (uint spe in speList)
                                {
                                    waitHandle.WaitOne();
                                    uint second = spe | (spa << 5) | (spd << 10);
                                    uint first = (hp | (atk << 5) | (def << 10)) << 16;
                                    List<Frame> frames = generator.Generate(frameCompare, hp, atk, def, spa, spd, spe,
                                                                            natures,
                                                                            minEfgh, maxEfgh, id, sid, first, second);
                                    frames.AddRange(generator.Generate(frameCompare, hp, atk, def, spa, spd, spe,
                                                                            natures,
                                                                            minEfgh, maxEfgh, id, sid, first ^ 0x80000000, second));

                                    foreach (Frame frame in frames)
                                    {
                                        frame.DisplayPrep();

                                        var iframe = new IFrameCapture
                                            {
                                                Frame = frame,
                                                Seed = frame.Seed,
                                                Offset = frame.Number
                                            };

                                        lock (threadLock)
                                        {
                                            iframes.Add(iframe);
                                        }

                                        refreshQueue = true;
                                    }

                                    progressSearched += incrementFound;
                                    progressFound += (uint) frames.Count;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Generate4thGenEggPIDJob(uint minEfgh, uint maxEfgh)
        {
            uint searchRange = generator.MaxResults;

            //  Iterate through all AB
            for (uint ab = 0; ab <= 255; ab++)
            {
                waitHandle.WaitOne();
                //  Iterate through all CD
                for (uint cd = 0; cd <= 23; cd++)
                {
                    //  Iterate through delay range + year
                    for (uint efgh = minEfgh; efgh <= maxEfgh; efgh++)
                    {
                        //  First we need to build a seed for this iteration
                        //  based on all of our information.  This should be
                        //  fairly easy since we are not using dates ;)
                        uint seed = (ab << 24) + (cd << 16) + efgh;

                        //  Set this to our seed here
                        generator.InitialSeed = seed;

                        if (iframesEggShiny.Count > 1000000)
                            break;

                        //  This is where we actually go ahead and call our 
                        //  generator for a list of egg PIDs based on parameters
                        //  that have been passed in.
                        List<Frame> frames = generator.Generate(frameCompare, id, sid);

                        progressSearched += searchRange;
                        progressFound += (uint) frames.Count;

                        //  Now we need to iterate through each result here
                        //  and create a collection of the information that
                        //  we are going to place into our grid.
                        foreach (Frame frame in frames)
                        {
                            var iframeEgg = new IFrameEggPID();
                            frame.DisplayPrep();

                            iframeEgg.Offset = frame.Number;
                            iframeEgg.Seed = seed;
                            iframeEgg.Pid = frame.Pid;
                            iframeEgg.Shiny = frame.Shiny;

                            lock (threadLock)
                            {
                                iframesEggShiny.Add(iframeEgg);
                            }
                            refreshQueue = true;
                        }
                    }
                }
            }
        }

        private void Generate4thGenEggIVsJob(uint minDelay, uint maxDelay, uint year)
        {
            uint searchRange = generator.MaxResults;

            //  Iterate through all AB
            for (uint ab = 0; ab <= 255; ab++)
            {
                waitHandle.WaitOne();
                //  Iterate through all CD
                for (uint cd = 0; cd <= 23; cd++)
                {
                    //  Iterate through delay range + year
                    for (uint delay = minDelay; delay <= maxDelay; delay++)
                    {
                        //  First we need to build a seed for this iteration
                        //  based on all of our information.  This should be
                        //  fairly easy since we are not using dates ;)
                        uint seed = (ab << 24) | (cd << 16) | (delay + year - 2000);

                        //  Set this to our seed here
                        generator.InitialSeed = seed;

                        if (iframesEggIVs.Count > 1000000)
                            break;

                        //  This is where we actually go ahead and call our 
                        //  generator for a list of egg PIDs based on parameters
                        //  that have been passed in.
                        List<Frame> frames = generator.Generate(frameCompare, id, sid);

                        progressSearched += searchRange;
                        progressFound += (uint) frames.Count;

                        //  Now we need to iterate through each result here
                        //  and create a collection of the information that
                        //  we are going to place into our grid.
                        foreach (Frame frame in frames)
                        {
                            var iframeEgg = new IFrameBreeding
                                {
                                    Offset = frame.Number,
                                    Seed = seed,
                                    FrameType = generator.FrameType,
                                    Delay = delay,
                                    Hp = frame.DisplayHpAlt,
                                    Atk = frame.DisplayAtkAlt,
                                    Def = frame.DisplayDefAlt,
                                    Spa = frame.DisplaySpaAlt,
                                    Spd = frame.DisplaySpdAlt,
                                    Spe = frame.DisplaySpeAlt,
                                    DisplayHpInh = frame.DisplayHp,
                                    DisplayAtkInh = frame.DisplayAtk,
                                    DisplayDefInh = frame.DisplayDef,
                                    DisplaySpaInh = frame.DisplaySpa,
                                    DisplaySpdInh = frame.DisplaySpd,
                                    DisplaySpeInh = frame.DisplaySpe
                                };


                            lock (threadLock)
                            {
                                iframesEggIVs.Add(iframeEgg);
                            }
                            refreshQueue = true;
                        }
                    }
                }
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
            var updateParams = new object[] {bindingSource};
            ResortGridDelegate gridSorter = ResortGrid;
            var sortParams = new object[] {bindingSource, grid, frameType};
            ThreadDelegate enableGenerateButton = EnableCapGenerate;

            try
            {
                bool alive = true;
                while (alive)
                {
                    progress.ShowProgress(progressSearched/(float) progressTotal, progressSearched, progressFound);
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
                    foreach (Thread t in jobs)
                    {
                        if (t != null)
                        {
                            t.Abort();
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
                case FrameType.MethodJ:
                case FrameType.MethodK:
                    var iframeCaptureComparer = new IFrameCaptureComparer {CompareType = "Seed"};
                    ((List<IFrameCapture>) bindingSource.DataSource).Sort(iframeCaptureComparer);
                    CapSeed.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
            }

            dataGrid.DataSource = bindingSource;
            bindingSource.ResetBindings(false);
        }

        private void EnableCapGenerate()
        {
            buttonCapGenerate.Enabled = true;
            buttonShinyGenerate.Enabled = true;
            buttonEggGenerate.Enabled = true;
        }

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] encounterMenu;
            string previousEncounter = "Wild Pokémon";
            if (comboBoxEncounterType.SelectedItem != null)
                previousEncounter = comboBoxEncounterType.SelectedItem.ToString();

            if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.MethodK))
            {
                encounterMenu = new[]
                    {
                        "Wild Pokémon",
                        "Wild Pokémon (Surfing)",
                        "Wild Pokémon (Old Rod)",
                        "Wild Pokémon (Good Rod)",
                        "Wild Pokémon (Super Rod)",
                        "Stationary Pokémon",
                        "Bug-Catching Contest",
                        "Safari Zone",
                        "Headbutt"
                    };

                comboBoxEncounterType.Enabled = true;
                comboBoxAbility.Enabled = true;
                comboBoxNature.Enabled = true;
            }
            else if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.MethodJ))
            {
                encounterMenu = new[]
                    {
                        "Wild Pokémon",
                        "Wild Pokémon (Surfing)",
                        "Wild Pokémon (Old Rod)",
                        "Wild Pokémon (Good Rod)",
                        "Wild Pokémon (Super Rod)",
                        "Stationary Pokémon"
                    };

                comboBoxEncounterType.Enabled = true;
                comboBoxAbility.Enabled = true;
                comboBoxNature.Enabled = true;
            }
            else if (((ComboBoxItem) comboBoxMethod.SelectedItem).Reference.Equals(FrameType.WondercardIVs))
            {
                encounterMenu = new[]
                    {
                        "Stationary\\Gift Pokémon"
                    };

                comboBoxEncounterType.Enabled = false;
                comboBoxAbility.Enabled = false;
                comboBoxNature.Enabled = false;
            }
            else
            {
                encounterMenu = new[]
                    {
                        "Stationary\\Gift Pokémon"
                    };

                comboBoxEncounterType.Enabled = false;
                comboBoxAbility.Enabled = true;
                comboBoxNature.Enabled = true;
            }

            comboBoxEncounterType.DataSource = encounterMenu;

            // makes sure new selected option matches previous, if applicable
            for (int i = 0; i < encounterMenu.Length; i++)
            {
                if (encounterMenu[i] == previousEncounter)
                {
                    comboBoxEncounterType.SelectedIndex = i;
                    break;
                }
            }
        }

        private void buttonAnyNature_Click(object sender, EventArgs e)
        {
            comboBoxNature.ClearSelection();
        }

        private void dataGridViewCapValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //  Make all of the junk natures show up in a lighter color
            if (e.ColumnIndex == CapNatureIndex)
            {
                var nature = (string) e.Value;

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

            if (e.ColumnIndex >= CapHPIndex && e.ColumnIndex <= CapSpeedIndex)
            {
                var number = (uint) e.Value;

                if (number >= 30)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if (number == 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void generateTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows[0] != null)
            {
                var frame = (IFrameCapture) dataGridViewCapValues.SelectedRows[0].DataBoundItem;

                // This is a bit of a strange hack, because this window
                //  needs to be hidden before we load the seed to time
                //  form or it wont be able to be focused. 
                bool showMap = HgSsRoamerSW.Window.Map.Visible;
                HgSsRoamerSW.Window.Hide();

                var seedToTime = new SeedToTime();

                //  Get the currently selected frame here so we can
                //  pull out some of the values that we are going to
                //  need to use.

                if (frame.Frame.FrameType == FrameType.Method1 ||
                    frame.Frame.FrameType == FrameType.MethodJ)
                    seedToTime.setDPPt();
                else
                    seedToTime.setHGSS();

                seedToTime.AutoGenerate = true;
                seedToTime.ShowMap = showMap;
                seedToTime.Seed = (uint) frame.Seed;

                //  Grab this from what the user had searched on
                seedToTime.Year = (uint) DateTime.Now.Year;
                if (maskedTextBoxCapYear.Text != "")
                    seedToTime.Year = uint.Parse(maskedTextBoxCapYear.Text);

                seedToTime.Show();
            }
        }

        private void outputCapResultsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
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
                List<IFrameCapture> frames = iframes;

                if (frames.Count > 0)
                {
                    var writer = new TXTWriter(dataGridViewCapValues);
                    writer.Generate(saveFileDialogTxt.FileName, frames);
                }
            }
        }

        private void dataGridViewCapValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewCapValues.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewCapValues.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewCapValues.ClearSelection();

                        (dataGridViewCapValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var searchFlips = new SearchFlips();

            if (searchFlips.ShowDialog() == DialogResult.OK)
            {
                //  Going to pull out the collection and then iterate looking
                //  for the first instance of a match.

                if (iframesEggIVs != null)
                {
                    int cnt = 0;

                    foreach (IFrameBreeding frame in iframesEggIVs)
                    {
                        if (frame.Flips == searchFlips.ReturnFlips)
                        {
                            dataGridViewEggIVValues.FirstDisplayedScrollingRowIndex = cnt;
                            dataGridViewEggIVValues.Rows[cnt].Selected = true;

                            break;
                        }

                        cnt++;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------
        //ShinyFinder code added here-----------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------

        private void buttonShinyClearNature_Click(object sender, EventArgs e)
        {
            comboBoxShinyNature.SelectedIndex = 0;
        }

        //--------------------------------------------------------------------------------------------------

        private void buttonShinyClearAbility_Click(object sender, EventArgs e)
        {
            comboBoxShinyAbility.SelectedIndex = 0;
        }

        //--------------------------------------------------------------------------------------------------

        //todo:make a more through validation
        private void validateShinyInput()
            //get rid of any junk characters in the shiny text boxes
            //and restrict the inputs to their appropriate levels
            //prevents the program confusing the user by throwing nasty exceptions
        {
            //check the date is valid
            maskedTextBoxShinyYear.Text = Functions.NumericFilter(maskedTextBoxShinyYear.Text);
            //this will crash if the year is bad but won't do anything else
            //int year = Convert.ToInt32(maskedTextBoxShinyYear.Text);

            //check the min delay is less than or equal to the max delay
            int minDelay = Convert.ToInt32(Functions.NumericFilter(maskedTextBoxShinyMinDelay.Text));
            int maxDelay = Convert.ToInt32(Functions.NumericFilter(maskedTextBoxShinyMaxDelay.Text));

            if (minDelay > maxDelay)
            {
                maskedTextBoxShinyMinDelay.Text = maxDelay.ToString();
                maskedTextBoxShinyMaxDelay.Text = minDelay.ToString();
            }
            else
            {
                maskedTextBoxShinyMinDelay.Text = minDelay.ToString();
                maskedTextBoxShinyMaxDelay.Text = maxDelay.ToString();
            }

            //check the IDs are 16-bit numbers
            maskedTextBoxShinyID.Text =
                (Functions.Clip(Convert.ToInt32(Functions.NumericFilter(maskedTextBoxShinyID.Text)), 0, 65535)).ToString
                    ("00000");
            maskedTextBoxShinySecretID.Text =
                (Functions.Clip(Convert.ToInt32(Functions.NumericFilter(maskedTextBoxShinySecretID.Text)), 0, 65535)).
                    ToString("00000");
        }

        //--------------------------------------------------------------------------------------------------

        private void buttonShinyGenerate_Click(object sender, EventArgs e)
        {
            //the main function for the shiny Time finder

            //check to make sure the user hasn't filled the text
            //boxes with exception-throwing garbage
            validateShinyInput();

            //ShinyFinder stores TimeData structs in a list in order to pass them to
            //its adjacents window, so one is used to store all the variables in this function

            //read the user input from the form
            int year = Convert.ToInt32(maskedTextBoxShinyYear.Text);

            uint minDelay = Convert.ToUInt32(maskedTextBoxShinyMinDelay.Text);
            uint maxDelay = Convert.ToUInt32(maskedTextBoxShinyMaxDelay.Text);

            var minEfgh = (uint) (minDelay + year - 2000);
            var maxEfgh = (uint) (maxDelay + year - 2000);

            if (!uint.TryParse(maskedTextBoxShinyID.Text, out id))
                id = 0;

            if (!uint.TryParse(maskedTextBoxShinySecretID.Text, out sid))
                sid = 0;

            List<uint> nature = null;
            if (comboBoxShinyNature.SelectedIndex != 0)
                nature = new List<uint> {(uint) ((Nature) comboBoxShinyNature.SelectedItem).Number};

            //List<IFrameEggPID> iframesEgg = new List<IFrameEggPID>();

            generator = new FrameGenerator();
            iframesEggShiny = new List<IFrameEggPID>();
            frameCompare = new FrameCompare(0, CompareType.None,
                                            0, CompareType.None,
                                            0, CompareType.None,
                                            0, CompareType.None,
                                            0, CompareType.None,
                                            0, CompareType.None,
                                            nature,
                                            (int) ((ComboBoxItem) comboBoxShinyAbility.SelectedItem).Reference,
                                            checkBoxShinyShinyOnly.Checked,
                                            false,
                                            false,
                                            null,
                                            (GenderFilter) (comboBoxShinyGender.SelectedItem));


            listBindingShiny = new BindingSource {DataSource = iframesEggShiny};
            dataGridViewShinyResults.DataSource = listBindingShiny;

            uint maxTaps = Convert.ToUInt32(maskedTextBoxMaxTaps.Text);

            if (radioButtonDPPt.Checked && !checkBoxIntlParents.Checked)
            {
                generator.FrameType = FrameType.Gen4Normal;
                if (checkBoxNoHappiness.Checked)
                {
                    generator.InitialFrame = 11;
                    generator.MaxResults = 11;
                }
                else
                {
                    generator.InitialFrame = 23;
                    generator.MaxResults = 11 + maxTaps*12;
                }
            }
            else if (radioButtonDPPt.Checked && checkBoxIntlParents.Checked)
            {
                generator.FrameType = FrameType.Gen4International;
                if (checkBoxNoHappiness.Checked)
                {
                    generator.InitialFrame = 11;
                    generator.MaxResults = 11;
                }
                else
                {
                    generator.InitialFrame = 23;
                    generator.MaxResults = 11 + maxTaps*12;
                }
            }
            else if (radioButtonHGSS.Checked && !checkBoxIntlParents.Checked)
            {
                generator.FrameType = FrameType.Gen4Normal;
                generator.InitialFrame = 1;
                generator.MaxResults = 14;
            }
            else if (radioButtonHGSS.Checked && checkBoxIntlParents.Checked)
            {
                generator.FrameType = FrameType.Gen4International;
                generator.InitialFrame = 1;
                generator.MaxResults = 14;
            }

            progressSearched = 0;
            progressFound = 0;
            progressTotal = (255 * 24 * (maxEfgh - minEfgh + 1) * generator.MaxResults);

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[1];
            jobs[0] = new Thread(() => Generate4thGenEggPIDJob(minEfgh, maxEfgh));
            jobs[0].Start();

            Thread.Sleep(200);

            var progressJob = new Thread(() => ManageProgress(listBindingShiny, dataGridViewShinyResults, generator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;

            buttonCapGenerate.Enabled = false;
            buttonShinyGenerate.Enabled = false;
            buttonEggGenerate.Enabled = false;

            //  Here is where we should have a collection that we can
            //  bind to the grid and display information to the user

            if (radioButtonHGSS.Checked)
            {
                EggSeed.DefaultCellStyle.Format = "X8";
                ShinyTaps.Visible = false;
                ShinyFlipSequence.Visible = false;
                ShinyFlips.Visible = false;
                ShinyOffset.Visible = true;
            }
            else if (radioButtonDPPt.Checked)
            {
                EggSeed.DefaultCellStyle.Format = "X8";
                ShinyTaps.Visible = !checkBoxNoHappiness.Checked;
                ShinyFlipSequence.Visible = true;
                ShinyFlips.Visible = true;
                ShinyOffset.Visible = false;
            }

            dataGridViewShinyResults.Focus();
        }

        private void dataGridViewShinyResults_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewShinyResults.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewShinyResults.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewShinyResults.ClearSelection();

                        (dataGridViewShinyResults.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void generateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewShinyResults.SelectedRows[0] != null)
            {
                var frame = (IFrameEggPID) dataGridViewShinyResults.SelectedRows[0].DataBoundItem;

                // This is a bit of a strange hack, because this window
                //  needs to be hidden before we load the seed to time
                //  form or it wont be able to be focused. 
                bool showMap = HgSsRoamerSW.Window.Map.Visible;
                HgSsRoamerSW.Window.Hide();

                var seedToTime = new SeedToTime();

                //  Get the currently selected frame here so we can
                //  pull out some of the values that we are going to
                //  need to use.

                seedToTime.setDPPt();
                seedToTime.AutoGenerate = true;
                seedToTime.ShowMap = showMap;
                seedToTime.Seed = frame.Seed;

                //  Grab this from what the user had searched on
                seedToTime.Year = uint.Parse(maskedTextBoxShinyYear.Text);

                seedToTime.Show();
            }
        }

        private void outputResultsToTXTToolStripMenuItem1_Click(object sender, EventArgs e)
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

                // Throws an exception if the wrong object type
                // And goes to the other one

                var writer = new TXTWriter(dataGridViewShinyResults);
                try
                {
                    var frames = (List<IFrameEggPID>) listBindingShiny.DataSource;

                    if (frames.Count > 0)
                    {
                        writer.Generate(saveFileDialogTxt.FileName, frames);
                    }
                }
                catch
                {
                    var frames = (List<IFrameCapture>) listBindingShiny.DataSource;

                    if (frames.Count > 0)
                    {
                        writer.Generate(saveFileDialogTxt.FileName, frames);
                    }
                }
            }
        }

        private void copySeedToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewEggIVValues.SelectedRows[0] != null)
            {
                var frame = (IFrameBreeding) dataGridViewEggIVValues.SelectedRows[0].DataBoundItem;

                Clipboard.SetText(frame.Seed.ToString("X8"));
            }
        }

        private void copySeedToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows[0] != null)
            {
                var frame = (IFrameCapture) dataGridViewCapValues.SelectedRows[0].DataBoundItem;

                Clipboard.SetText(frame.Seed.ToString("X8"));
            }
        }

        private void copySeedToClipboardToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridViewShinyResults.SelectedRows[0] != null)
            {
                try
                {
                    var frame = (IFrameEggPID) dataGridViewShinyResults.SelectedRows[0].DataBoundItem;
                    Clipboard.SetText(frame.Seed.ToString("X8"));
                }
                catch
                {
                    var frame = (IFrameCapture) dataGridViewShinyResults.SelectedRows[0].DataBoundItem;
                    Clipboard.SetText(frame.Seed.ToString("X16"));
                }
            }
        }

        private void comboBoxShinyVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButtonDPPt.Checked)
            {
                labelDPPtWarning.Visible = true;

                labelHappinessWarning.Visible = !checkBoxNoHappiness.Checked;
                label8.Visible = true;
                maskedTextBoxMaxTaps.Visible = true;
                checkBoxNoHappiness.Visible = true;
            }
            else
            {
                labelDPPtWarning.Visible = false;
                labelHappinessWarning.Visible = false;
                label8.Visible = false;
                maskedTextBoxMaxTaps.Visible = false;
                checkBoxNoHappiness.Visible = false;
            }
        }

        private void dataGridViewShinyResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //  Make all of the junk natures show up in a lighter color
            if (dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinyNature")
            {
                var nature = (string) e.Value;

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

            if (dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinyHP" ||
                dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinyAtk" ||
                dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinyDef" ||
                dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinySpA" ||
                dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinySpD" ||
                dataGridViewShinyResults.Columns[e.ColumnIndex].Name == "ShinySpe")
            {
                if ((string) e.Value == "30" || (string) e.Value == "31")
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if ((string) e.Value == "0")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }

                if ((string) e.Value == "A" || (string) e.Value == "B")
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        // Sorts the grid 
        // Can't use SortCompare method because this grid is data-bound
        private void dataGridViewCapValues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewCapValues.DataSource != null && iframes != null && listBindingCap != null)
            {
                DataGridViewColumn selectedColumn = dataGridViewCapValues.Columns[e.ColumnIndex];

                var iframeCaptureComparer = new IFrameCaptureComparer
                    {CompareType = selectedColumn.DataPropertyName};

                if (selectedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    iframeCaptureComparer.sortOrder = SortOrder.Descending;

                iframes.Sort(iframeCaptureComparer);

                listBindingCap.ResetBindings(false);
                selectedColumn.HeaderCell.SortGlyphDirection = iframeCaptureComparer.sortOrder;
            }
        }

        private void buttonAnySlot_Click(object sender, EventArgs e)
        {
            comboBoxEncounterSlot.ClearSelection();
        }

        private void dataGridViewCapValues_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && e.Control)
            {
                DataObject clipboardContent = dataGridViewCapValues.GetClipboardContent();
                if (clipboardContent != null)
                {
                    var test = (string) clipboardContent.GetData(DataFormats.UnicodeText);
                    test = test.Replace('\t', ' ');
                    Clipboard.SetText(test);
                }
            }
        }

        private void checkBoxNoHappiness_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNoHappiness.Checked)
            {
                maskedTextBoxMaxTaps.Enabled = false;
                labelHappinessWarning.Visible = false;
            }
            else
            {
                maskedTextBoxMaxTaps.Enabled = true;
                labelHappinessWarning.Visible = true;
            }
        }

        private void returnToResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewEggIVValues.DataSource = listBindingEgg;
            returnToResultsToolStripMenuItem.Visible = false;
        }

        private void checkBoxShowInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowInheritance.Checked)
            {
                HP.DataPropertyName = "DisplayHPInh";
                Atk.DataPropertyName = "DisplayAtkInh";
                Def.DataPropertyName = "DisplayDefInh";
                SpA.DataPropertyName = "DisplaySpaInh";
                SpD.DataPropertyName = "DisplaySpdInh";
                Spe.DataPropertyName = "DisplaySpeInh";
            }
            else
            {
                HP.DataPropertyName = "DisplayHP";
                Atk.DataPropertyName = "DisplayAtk";
                Def.DataPropertyName = "DisplayDef";
                SpA.DataPropertyName = "DisplaySpa";
                SpD.DataPropertyName = "DisplaySpd";
                Spe.DataPropertyName = "DisplaySpe";
            }
        }

        private GenderFilter constructGenderFilter(ComboBox genderComboBox)
        {
            var criteria = (GenderCriteria) genderComboBox.SelectedIndex;
            uint ratio = 0;

            switch (comboBoxCapGenderRatio.SelectedIndex)
            {
                case 0:
                    ratio = 255;
                    break;
                case 1:
                    ratio = 127;
                    break;
                case 2:
                    ratio = 191;
                    break;
                case 3:
                    ratio = 63;
                    break;
                case 4:
                    ratio = 31;
                    break;
                case 5:
                    ratio = 0;
                    break;
            }

            var filter = new GenderFilter("", ratio, criteria);

            return filter;
        }

        private void comboBoxCapGenderRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCapGenderRatio.SelectedIndex == 0 || comboBoxCapGenderRatio.SelectedIndex == 5 ||
                !comboBoxCapGenderRatio.Enabled)
            {
                comboBoxCapGender.Enabled = false;
                comboBoxCapGender.SelectedIndex = 0;
            }
            else
                comboBoxCapGender.Enabled = true;
        }

        private void comboBoxCapGenderRatio_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var lookup = new GenderRatioLookup();
                if (lookup.ShowDialog() == DialogResult.OK)
                {
                    switch (lookup.GenderRatio)
                    {
                        case 127:
                            comboBoxCapGenderRatio.SelectedIndex = 1;
                            break;
                        case 191:
                            comboBoxCapGenderRatio.SelectedIndex = 2;
                            break;
                        case 63:
                            comboBoxCapGenderRatio.SelectedIndex = 3;
                            break;
                        case 31:
                            comboBoxCapGenderRatio.SelectedIndex = 4;
                            break;
                        default:
                            comboBoxCapGenderRatio.SelectedIndex = 5;
                            break;
                    }
                }
            }
        }

/*
        private void DisplayGenderColumns()
        {
            switch (comboBoxCapGenderRatio.SelectedIndex)
            {
                case 0:
                    f25.Visible = true;
                    f50.Visible = true;
                    f75.Visible = true;
                    f125.Visible = true;
                    break;
                case 1:
                    f25.Visible = false;
                    f50.Visible = true;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
                case 2:
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = true;
                    f125.Visible = false;
                    break;
                case 3:
                    f25.Visible = true;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
                case 4:
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = true;
                    break;
                case 5:
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
            }
        }
*/

        private void dataGridViewCapValues_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Settings.Default.ShowToolTips)
            {
                Rectangle cellRect = dataGridViewCapValues.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                if (dataGridViewCapValues.Columns[e.ColumnIndex].Name == "EncounterMod")
                {
                    if (e.RowIndex >= 0)
                    {
                        switch (
                            ((IFrameCapture) dataGridViewCapValues.Rows[e.RowIndex].DataBoundItem).Frame.EncounterMod)
                        {
                            case Objects.EncounterMod.Synchronize:
                                toolTipDataGrid.ToolTipTitle = "Synchronize";

                                toolTipDataGrid.Show(
                                    "When encountering the desired Pokémon, the lead Pokémon in your party\r\n" +
                                    "must have the ability Synchronize, and have a nature that matches your\r\n" +
                                    "desired nature.  This will cause the target Pokémon to have your desired nature.",
                                    this,
                                    dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                    dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                    5000);
                                break;
                            case Objects.EncounterMod.CuteCharm50F:
                            case Objects.EncounterMod.CuteCharm125F:
                            case Objects.EncounterMod.CuteCharm25F:
                            case Objects.EncounterMod.CuteCharm75F:
                            case Objects.EncounterMod.CuteCharm50M:
                            case Objects.EncounterMod.CuteCharm875M:
                            case Objects.EncounterMod.CuteCharm75M:
                            case Objects.EncounterMod.CuteCharm25M:
                            case Objects.EncounterMod.CuteCharmFemale:
                                toolTipDataGrid.ToolTipTitle = "Cute Charm";

                                toolTipDataGrid.Show(
                                    "When encountering the target Pokémon, the lead Pokémon in your party\r\n" +
                                    "must have the ability Cute Charm, and be the opposite gender of the listed target.\r\n" +
                                    "The listed gender ratio must also match that of the target Pokémon.\r\n\r\n" +
                                    "For example: Cute Charm (75% M) indicates that the target Pokémon must be\r\n" +
                                    "male (requiring a female Cute Charm lead), and must be of a species that has a\r\n" +
                                    "75% male gender ratio, such as Alakazam.  However, Cute Charm (Female), requires a\r\n" +
                                    "male lead and will work for all female Pokémon (except female-only Pokémon such as Jynx).\r\n\r\n" +
                                    "Cute Charm does not work for species with only one gender, such as Tauros.",
                                    this,
                                    dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                    dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                    15000);
                                break;
                            case Objects.EncounterMod.SuctionCups:
                                toolTipDataGrid.ToolTipTitle = "Suction Cups";

                                toolTipDataGrid.Show(
                                    "When fishing for the target Pokémon, the lead Pokémon in your party\r\n" +
                                    "must have the ability Suction Cups.\r\n\r\n" +
                                    "Otherwise, fishing will fail with \"Not even a nibble.\"",
                                    this,
                                    dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                    dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                    15000);
                                break;
                            default:
                                toolTipDataGrid.Hide(this);
                                break;
                        }
                    }
                }
                else if (dataGridViewCapValues.Columns[e.ColumnIndex].Name == "Nature")
                {
                    toolTipDataGrid.ToolTipTitle = "Nature";

                    toolTipDataGrid.Show("Greyed-out natures are natures with no competitive value.",
                                         this,
                                         dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                         dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                         5000);
                }
                else if (dataGridViewCapValues.Columns[e.ColumnIndex].Name == "Shiny")
                {
                    toolTipDataGrid.ToolTipTitle = "!!!";

                    toolTipDataGrid.Show("A !!! in this column indicates the frame will be shiny.",
                                         this,
                                         dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                         dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                         5000);
                }
                else if (dataGridViewCapValues.Columns[e.ColumnIndex].Name == "EncounterSlot")
                {
                    toolTipDataGrid.ToolTipTitle = "Encounter Slot";

                    toolTipDataGrid.Show("Encounter slots are used to determine what Pokémon appears for\r\n" +
                                         "a wild battle.  Use the encounter tables under the main menus to look up\r\n" +
                                         "which Pokémon appears for each slot in each area.\r\n",
                                         this,
                                         dataGridViewCapValues.Location.X + cellRect.X + cellRect.Size.Width,
                                         dataGridViewCapValues.Location.Y + cellRect.Y + cellRect.Size.Height,
                                         5000);
                }
                else if (e.ColumnIndex >= 0)
                {
                    toolTipDataGrid.Hide(this);
                }
            }
        }

        private void dataGridViewCapValues_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTipDataGrid.Hide(this);
        }

        private void FocusControl(object sender, MouseEventArgs e)
        {
            ((Control) sender).Focus();
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

        private void comboBoxEncounterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEncounterType.SelectedItem.ToString() == "Bug-Catching Contest")
                preDexRadioButton.Visible = saturdayRadioButton.Visible = thursdayRadioButton.Visible = tuesdayRadioButton.Visible = true;
            else
                preDexRadioButton.Visible = saturdayRadioButton.Visible = thursdayRadioButton.Visible = tuesdayRadioButton.Visible = false;
        }
    }
}