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
using System.Windows.Forms;
using Microsoft.Win32;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class Adjacents : Form
    {
        private readonly EncounterType encounterType;
        private readonly FrameType frameType;
        private readonly bool loadSearchResult;
        private readonly int profile;

        // Multithreading

        // Column identifiers
        // We use these so we don't have to make string compares
        // for every cell that needs formatting, and edit column
        // indices every time we change columns
        private int CapHPIndex;
        private int CapNatureIndex;
        private int CapSpeedIndex;
        private FrameCompare frameCompare;
        private FrameGenerator generator;
        private List<IFrameCapture> iframes;
        private BindingSource listBindingCap;
        private BindingSource profilesSource;
        private ulong seedMatch;

        public Adjacents()
        {
            InitializeComponent();
            loadSearchResult = false;
        }

        public Adjacents(DateTime dateTime,
                         int profile, IEnumerable<ButtonComboType> buttons,
                         FrameType frameType, EncounterType encounterType,
                         uint Advances)
        {
            InitializeComponent();
            datePicker.Value = dateTime;

            maskedTextBoxCapMinOffset.Text = Advances.ToString();
            maskedTextBoxCapMaxOffset.Text = Advances.ToString();

            this.frameType = frameType;
            this.encounterType = encounterType;

            foreach (ButtonComboType button in buttons)
            {
                comboBoxKeypresses.CheckBoxItems[(int) button].Checked = true;
            }
            this.profile = profile;
            loadSearchResult = true;
        }

        private void PlatinumTime_Load(object sender, EventArgs e)
        {
            if (Profiles.List != null && Profiles.List.Count > 0)
            {
                profilesSource = new BindingSource {DataSource = Profiles.List};
                comboBoxProfiles.DataSource = profilesSource;
            }
            comboBoxProfiles.SelectedIndex = profile;

            // Add smart comboBox items
            // Would be nice if we left these in the Designer file
            // But Visual Studio seems to like deleting them without warning

            comboBoxMethod.Items.AddRange(new object[]
                {
                    new ComboBoxItem("IVs (Standard Seed)", FrameType.Method5Standard),
                    new ComboBoxItem("PIDRNG", FrameType.Method5Natures),
                    new ComboBoxItem("Eggs", FrameType.BWBred),
                    new ComboBoxItem("Wondercard", FrameType.Wondercard5thGen),
                    new ComboBoxItem("GLAN Wondercard", FrameType.Wondercard5thGenFixed)
                });

            comboBoxEncounterType.Items.AddRange(new object[]
                {
                    new ComboBoxItem("Wild Pokémon", EncounterType.Wild),
                    new ComboBoxItem("Wild Pokémon (Swarm)",
                                     EncounterType.WildSwarm),
                    new ComboBoxItem("Wild Pokémon (Surfing)",
                                     EncounterType.WildSurfing),
                    new ComboBoxItem("Wild Pokémon (Fishing)",
                                     EncounterType.WildSuperRod),
                    new ComboBoxItem("Wild Pokémon (Shaking Grass)",
                                     EncounterType.WildShakerGrass),
                    new ComboBoxItem("Wild Pokémon (Bubble Spot)",
                                     EncounterType.WildWaterSpot),
                    new ComboBoxItem("Wild Pokémon (Cave Spot)",
                                     EncounterType.WildCaveSpot),
                    new ComboBoxItem("Stationary Pokémon", EncounterType.Stationary)
                    ,
                    new ComboBoxItem("Roaming Pokémon", EncounterType.Roamer),
                    new ComboBoxItem("Gift Pokémon", EncounterType.Gift),
                    new ComboBoxItem("Larvesta Egg", EncounterType.LarvestaEgg)
                });

            comboBoxLead.Items.AddRange(new object[]
                {
                    new ComboBoxItem("None", EncounterMod.None),
                    new ComboBoxItem("Synchronize", EncounterMod.Synchronize),
                    new ComboBoxItem("Cute Charm", EncounterMod.CuteCharm),
                    new ComboBoxItem("Suction Cups", EncounterMod.SuctionCups)
                });

            Settings.Default.PropertyChanged += ChangeLanguage;
            SetLanguage();

            // Obtain the indices of the datagrid columns by name,
            // so we don't have to keep track of them

            CapHPIndex = CapHP.Index;
            CapSpeedIndex = CapSpe.Index;
            CapNatureIndex = Nature.Index;

            dataGridViewCapValues.AutoGenerateColumns = false;
            CapSeed.DefaultCellStyle.Format = "X16";
            PID.DefaultCellStyle.Format = "X8";

            Timer0.DefaultCellStyle.Format = "X";
            SeedTime.DefaultCellStyle.Format = "MM/dd/yy HH:mm:ss";

            comboBoxLead.SelectedIndex = 0;

            if (loadSearchResult)
            {
                switch (frameType)
                {
                    case FrameType.Method5Standard:
                        comboBoxMethod.SelectedIndex = 0;
                        break;
                    case FrameType.Method5Natures:
                        comboBoxMethod.SelectedIndex = 1;
                        break;
                    case FrameType.BWBred:
                    case FrameType.BWBredInternational:
                        comboBoxMethod.SelectedIndex = 2;
                        break;
                    case FrameType.Wondercard5thGen:
                        comboBoxMethod.SelectedIndex = 3;
                        break;
                    case FrameType.Wondercard5thGenFixed:
                        comboBoxMethod.SelectedIndex = 4;
                        break;
                }

                switch (encounterType)
                {
                    case EncounterType.Wild:
                        comboBoxEncounterType.SelectedIndex = 0;
                        break;
                    case EncounterType.WildSwarm:
                        comboBoxEncounterType.SelectedIndex = 1;
                        break;
                    case EncounterType.WildSurfing:
                        comboBoxEncounterType.SelectedIndex = 2;
                        break;
                    case EncounterType.WildSuperRod:
                        comboBoxEncounterType.SelectedIndex = 3;
                        break;
                    case EncounterType.WildShakerGrass:
                        comboBoxEncounterType.SelectedIndex = 4;
                        break;
                    case EncounterType.WildWaterSpot:
                        comboBoxEncounterType.SelectedIndex = 5;
                        break;
                    case EncounterType.WildCaveSpot:
                        comboBoxEncounterType.SelectedIndex = 6;
                        break;
                    case EncounterType.Stationary:
                        comboBoxEncounterType.SelectedIndex = 7;
                        break;
                    case EncounterType.Roamer:
                        comboBoxEncounterType.SelectedIndex = 8;
                        break;
                    case EncounterType.Gift:
                        comboBoxEncounterType.SelectedIndex = 9;
                        break;
                    case EncounterType.LarvestaEgg:
                        comboBoxEncounterType.SelectedIndex = 10;
                        break;
                }

                Generate();
            }
            else
            {
                comboBoxMethod.SelectedIndex = 0;
                comboBoxEncounterType.SelectedIndex = 0;

                // This is a rather hackish way of making the custom control
                // display the desired text upon loading
                comboBoxKeypresses.CheckBoxItems[0].Checked = true;
                comboBoxKeypresses.CheckBoxItems[0].Checked = false;

                //  Load all of our items from the registry
                RegistryKey registrySoftware = Registry.CurrentUser.OpenSubKey("Software", true);
                if (registrySoftware != null)
                {
                    RegistryKey registryRngReporter = registrySoftware.OpenSubKey("RNGReporter");

                    if (Settings.Default.LastVersion < MainForm.VersionNumber && registryRngReporter != null)
                    {
                        maskedTextBoxCapMaxOffset.Text = (string) registryRngReporter.GetValue("pt_cap_offset", "1000");

                        if (maskedTextBoxCapMaxOffset.Text == "0")
                            maskedTextBoxCapMaxOffset.Text = "1";
                    }
                        //load from settings
                    else
                    {
                        maskedTextBoxCapMaxOffset.Text = Settings.Default.CapOffset;

                        if (maskedTextBoxCapMaxOffset.Text == "0")
                            maskedTextBoxCapMaxOffset.Text = "1";
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
            EncounterSlot.DefaultCellStyle = CellStyle;

            dataGridViewCapValues.Refresh();
        }

        private void PlatinumTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void contextMenuStripCap_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void buttonSeedGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void Generate()
        {
            var profile = (Profile) comboBoxProfiles.SelectedItem;
            uint minFrame = uint.Parse(maskedTextBoxCapMinOffset.Text);
            uint maxFrame = uint.Parse(maskedTextBoxCapMaxOffset.Text);

            DateTime seedTime = datePicker.Value;
            iframes = new List<IFrameCapture>();

            generator = new FrameGenerator
                {
                    FrameType = (FrameType) ((ComboBoxItem) comboBoxMethod.SelectedItem).Reference,
                    EncounterType =
                        (EncounterType) ((ComboBoxItem) comboBoxEncounterType.SelectedItem).Reference,
                    EncounterMod = (EncounterMod) ((ComboBoxItem) comboBoxLead.SelectedItem).Reference,
                    SynchNature = -2,
                    InitialFrame = minFrame + (profile.IsBW2() ? 2u : 0),
                    MaxResults = maxFrame - minFrame + 1
                };

            // Now that each combo box item is a custom object containing the FrameType reference
            // We can simply retrieve the FrameType from the selected item


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
                new NoGenderFilter());

            switch (generator.FrameType)
            {
                case FrameType.Method5Standard:
                    CapSeed.DefaultCellStyle.Format = "X16";
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
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
                case FrameType.Method5Natures:

                    CapSeed.DefaultCellStyle.Format = "X16";
                    if (generator.EncounterType != EncounterType.Stationary &&
                        generator.EncounterType != EncounterType.Gift &&
                        generator.EncounterType != EncounterType.Roamer &&
                        generator.EncounterType != EncounterType.LarvestaEgg)
                        EncounterSlot.Visible = true;
                    else
                        EncounterSlot.Visible = false;
                    PID.Visible = true;
                    Shiny.Visible = true;
                    Nature.Visible = true;
                    Ability.Visible = true;
                    CapHP.Visible = false;
                    CapAtk.Visible = false;
                    CapDef.Visible = false;
                    CapSpA.Visible = false;
                    CapSpD.Visible = false;
                    CapSpe.Visible = false;
                    f25.Visible = true;
                    f50.Visible = true;
                    f75.Visible = true;
                    f125.Visible = true;
                    break;
                case FrameType.BWBred:
                    generator.FrameType = FrameType.Method5Standard;
                    generator.InitialFrame = 14;
                    generator.MaxResults = 7;

                    CapSeed.DefaultCellStyle.Format = "X16";
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
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
                case FrameType.Wondercard5thGen:
                case FrameType.Wondercard5thGenFixed:
                    CapSeed.DefaultCellStyle.Format = "X16";
                    EncounterSlot.Visible = false;
                    PID.Visible = false;
                    Shiny.Visible = false;
                    Nature.Visible = true;
                    Ability.Visible = false;
                    CapHP.Visible = true;
                    CapAtk.Visible = true;
                    CapDef.Visible = true;
                    CapSpA.Visible = true;
                    CapSpD.Visible = true;
                    CapSpe.Visible = true;
                    f25.Visible = false;
                    f50.Visible = false;
                    f75.Visible = false;
                    f125.Visible = false;
                    break;
            }

            for (int seconds = (int) numericUpDownSeconds.Value*-1; seconds <= numericUpDownSeconds.Value; seconds++)
            {
                for (uint timer0 = profile.Timer0Min - 1; timer0 <= profile.Timer0Max + 1; timer0++)
                {
                    ulong seed = Functions.EncryptSeed(seedTime.AddSeconds(seconds), profile.MAC_Address,
                                                       profile.Version, profile.Language,
                                                       profile.DSType, profile.SoftReset, profile.VCount, timer0,
                                                       profile.GxStat,
                                                       profile.VFrame, buttonValue());

                    if (seconds == 0 && timer0 == profile.Timer0Min)
                    {
                        seedMatch = seed;
                    }

                    switch (generator.FrameType)
                    {
                        case FrameType.Method5Standard:
                            generator.InitialSeed = seed >> 32;
                            break;
                        case FrameType.Method5Natures:
                        case FrameType.Wondercard5thGen:
                        case FrameType.Wondercard5thGenFixed:
                            generator.InitialSeed = seed;
                            generator.InitialFrame = Functions.initialPIDRNG(seed, profile) + minFrame;
                            break;
                    }

                    List<Frame> frames = generator.Generate(frameCompare, profile.ID, profile.SID);

                    foreach (Frame frame in frames)
                    {
                        var iframe = new IFrameCapture();

                        frame.DisplayPrep();
                        iframe.Offset = frame.Number;
                        iframe.Seed = seed;
                        iframe.Frame = frame;

                        iframe.TimeDate = seedTime.AddSeconds(seconds);
                        iframe.Timer0 = timer0;

                        iframes.Add(iframe);
                    }
                }
            }

            listBindingCap = new BindingSource {DataSource = iframes};
            dataGridViewCapValues.DataSource = listBindingCap;


            foreach (DataGridViewRow row in dataGridViewCapValues.Rows)
            {
                if ((ulong) row.Cells[0].Value == seedMatch)
                {
                    dataGridViewCapValues.CurrentCell = row.Cells[0];
                    dataGridViewCapValues.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }

            dataGridViewCapValues.Focus();
        }

        private uint buttonValue()
        {
            var customKeypress = new List<ButtonComboType>();

            for (int button = 0; button < 13; button++)
            {
                if (comboBoxKeypresses.CheckBoxItems[button].Checked)
                    customKeypress.Add((ButtonComboType) button);
            }

            return Functions.buttonMashed(customKeypress);
        }

        private void dataGridViewCapValues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //  Make all of the junk natures show up in a lighter color
            if (e.ColumnIndex == CapNatureIndex)
            {
                var nature = (string) e.Value;

                if ((bool) dataGridViewCapValues.Rows[e.RowIndex].Cells["Synchable"].Value)
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (nature == Functions.NatureStrings(18) ||
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

            if (dataGridViewCapValues.Columns[e.ColumnIndex].Name == "CapSeed")
            {
                if (seedMatch != (ulong) e.Value)
                    dataGridViewCapValues.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
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

        private void copySeedToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows[0] != null)
            {
                var frame = (IFrameCapture) dataGridViewCapValues.SelectedRows[0].DataBoundItem;

                Clipboard.SetText(frame.Seed.ToString("X8"));
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

        // Ctrl+D for IRC-friendly copy+pasting
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

        private void dataGridViewCapValues_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCapValues.SelectedRows.Count > 0)
            {
                var profile = (Profile) comboBoxProfiles.SelectedItem;
                textBoxChatot.Text =
                    Responses.ChatotResponses64(
                        ((IFrameCapture) dataGridViewCapValues.SelectedRows[0].DataBoundItem).Seed, profile);
            }
        }

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (FrameType) ((ComboBoxItem) comboBoxMethod.SelectedItem).Reference;

            switch (type)
            {
                case FrameType.Method5Standard:
                    comboBoxEncounterType.Enabled = true;
                    comboBoxLead.Enabled = false;
                    maskedTextBoxCapMaxOffset.Enabled = true;
                    maskedTextBoxCapMinOffset.Enabled = true;
                    labelMinFrame.Text = "Min Frame";
                    labelMaxFrame.Text = "Max Frame";

                    if (maskedTextBoxCapMinOffset.Text == "0")
                        maskedTextBoxCapMinOffset.Text = "1";

                    if (maskedTextBoxCapMaxOffset.Text == "0")
                        maskedTextBoxCapMaxOffset.Text = "1";

                    textBoxDescription.Text = "This RNG handles IVs only.";
                    break;
                case FrameType.Method5Natures:
                    comboBoxEncounterType.Enabled = true;
                    comboBoxLead.Enabled = true;
                    maskedTextBoxCapMaxOffset.Enabled = true;
                    maskedTextBoxCapMinOffset.Enabled = true;
                    labelMinFrame.Text = "Min Advances";
                    labelMaxFrame.Text = "Max Advances";

                    if (maskedTextBoxCapMinOffset.Text == "1")
                        maskedTextBoxCapMinOffset.Text = "0";

                    if (maskedTextBoxCapMaxOffset.Text == "1")
                        maskedTextBoxCapMaxOffset.Text = "0";

                    textBoxDescription.Text = "This RNG handles natures, gender, encounter slot, and shininess only.";
                    break;
                case FrameType.BWBred:
                    comboBoxEncounterType.Enabled = false;
                    comboBoxLead.Enabled = false;
                    maskedTextBoxCapMaxOffset.Enabled = false;
                    maskedTextBoxCapMinOffset.Enabled = false;
                    labelMinFrame.Text = "Min Advances";
                    labelMaxFrame.Text = "Max Advances";
                    textBoxDescription.Text =
                        "After obtaining an egg from the Day-Care Man, you must immediately capture a Pokémon to confirm your seed.  " +
                        "The IVs of the captured Pokémon will appear on frames 14-20.";
                    break;
                case FrameType.Wondercard5thGen:
                    comboBoxEncounterType.Enabled = false;
                    comboBoxLead.Enabled = false;
                    maskedTextBoxCapMaxOffset.Enabled = true;
                    maskedTextBoxCapMinOffset.Enabled = true;
                    labelMinFrame.Text = "Min Advances";
                    labelMaxFrame.Text = "Max Advances";

                    if (maskedTextBoxCapMinOffset.Text == "1")
                        maskedTextBoxCapMinOffset.Text = "0";

                    if (maskedTextBoxCapMaxOffset.Text == "1")
                        maskedTextBoxCapMaxOffset.Text = "0";

                    textBoxDescription.Text =
                        "Most Mystery Gifts use this method.  However, Mystery Gifts that can have any nature but are locked into a single gender " +
                        "must use the GLAN (Gender Locked, Any Nature) Wondercard method.";
                    break;
                case FrameType.Wondercard5thGenFixed:
                    comboBoxEncounterType.Enabled = false;
                    comboBoxLead.Enabled = false;
                    maskedTextBoxCapMaxOffset.Enabled = true;
                    maskedTextBoxCapMinOffset.Enabled = true;
                    labelMinFrame.Text = "Min Advances";
                    labelMaxFrame.Text = "Max Advances";

                    if (maskedTextBoxCapMinOffset.Text == "1")
                        maskedTextBoxCapMinOffset.Text = "0";

                    if (maskedTextBoxCapMaxOffset.Text == "1")
                        maskedTextBoxCapMaxOffset.Text = "0";

                    textBoxDescription.Text =
                        "Mystery Gifts that can have any nature but are locked into a single gender " +
                        "must use the GLAN (Gender Locked, Any Nature) Wondercard method.";
                    break;
            }

            textBoxDescription.Text += Environment.NewLine +
                                       "Frames listed with a grey background are from seeds adjacent to the target.";
        }

        private void FocusControl(object sender, MouseEventArgs e)
        {
            ((Control) sender).Focus();
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelProfileInformation.Text = ((Profile) comboBoxProfiles.SelectedItem).ProfileInformationShort();
        }

        private void buttonEditProfile_Click(object sender, EventArgs e)
        {
            var editor = new ProfileEditor {Profile = (Profile) comboBoxProfiles.SelectedItem};
            if (editor.ShowDialog() != DialogResult.OK) return;
            Profiles.List[comboBoxProfiles.SelectedIndex] = editor.Profile;

            profilesSource.DataSource = Profiles.List;
            profilesSource.ResetBindings(false);
            labelProfileInformation.Text = ((Profile) comboBoxProfiles.SelectedItem).ProfileInformationShort();
        }
    }
}