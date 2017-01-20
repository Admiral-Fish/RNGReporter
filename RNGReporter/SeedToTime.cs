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
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Win32;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class SeedToTime : Form
    {
        private readonly Activation sequence = new Activation();
        private BindingSource profilesSource;
        public bool sekrit;

        public SeedToTime()
        {
            Year = (uint) DateTime.Now.Year;
            InitializeComponent();
        }

        public uint Seed { get; set; }

        public ulong MAC_Address { get; set; }

        public uint Year { get; set; }

        public bool AutoGenerate { get; set; }

        public bool ShowMap { get; set; }

        private void SeedToTime_Load(object sender, EventArgs e)
        {
            dataGridViewValues.AutoGenerateColumns = false;
            dataGridViewAdjacents.AutoGenerateColumns = false;

            AdjacentSeed.DefaultCellStyle.Format = "X8";
            ColumnCGearAdjust.DefaultCellStyle.Format = "X8";

            textBoxSeed.Text = Seed.ToString("x");
            maskedTextBoxYear.Text = Year.ToString();

            //  Grab our items from the registry
            RegistryKey registrySoftware = Registry.CurrentUser.OpenSubKey("Software", true);
            if (registrySoftware != null)
            {
                RegistryKey registryRngReporter = registrySoftware.CreateSubKey("RNGReporter");

                if (Settings.Default.LastVersion < MainForm.VersionNumber && registryRngReporter != null)
                {
                    maskedTextBoxSeconds.Text = (string) registryRngReporter.GetValue("stt_seconds", "0");

                    checkBoxLockSeconds.Checked = false;

                    if ((string) registryRngReporter.GetValue("stt_secondslocked", "0") == "1")
                    {
                        checkBoxLockSeconds.Checked = true;
                    }

                    maskedTextBoxMDelay.Text = (string) registryRngReporter.GetValue("stt_mdelay", "10");
                    maskedTextBoxPDelay.Text = (string) registryRngReporter.GetValue("stt_pdelay", "10");
                    maskedTextBoxMSecond.Text = (string) registryRngReporter.GetValue("stt_msecond", "1");
                    maskedTextBoxPSecond.Text = (string) registryRngReporter.GetValue("stt_psecond", "1");
                }
                else
                {
                    maskedTextBoxSeconds.Text = Settings.Default.sttSeconds;

                    checkBoxLockSeconds.Checked = Settings.Default.sttSecondsLocked;

                    maskedTextBoxMDelay.Text = Settings.Default.sttMDelay;
                    maskedTextBoxPDelay.Text = Settings.Default.sttPDelay;
                    maskedTextBoxMSecond.Text = Settings.Default.sttMSecond;
                    maskedTextBoxPSecond.Text = Settings.Default.sttPSecond;
                }
            }

            //check and error otherwise
            if (Profiles.List != null || Profiles.List.Count > 0)
            {
                profilesSource = new BindingSource {DataSource = Profiles.List};
                comboBoxProfiles.DataSource = profilesSource;
                profilesSource.ResetBindings(false);
            }

            SetHgSsItems();

            if (AutoGenerate)
            {
                Generate();
            }

            if (ShowMap)
            {
                HgSsRoamerSW.Window.Show();
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewValues.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewValues.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void Generate()
        {
            if (maskedTextBoxYear.Text == "")
            {
                MessageBox.Show("You must enter a year.", "Please Enter a Year", MessageBoxButtons.OK);
                return;
            }

            int lockedSecond = 0;

            if (maskedTextBoxSeconds.Text == "" && checkBoxLockSeconds.Checked)
            {
                MessageBox.Show("You must enter a seconds value.", "Please Enter Seconds", MessageBoxButtons.OK);
                return;
            }

            if (checkBoxLockSeconds.Checked)
            {
                lockedSecond = int.Parse(maskedTextBoxSeconds.Text);
            }

            if (textBoxSeed.Text != "")
            {
                Seed = uint.Parse(textBoxSeed.Text, NumberStyles.HexNumber);
            }

            if (radioBtnDPPt.Checked)
            {
                labelVerificationType.Text = "Coin Flips for Seed:";
                labelFlipsElmsForSeed.Text = CoinFlips.GetFlips(Seed, 15);
                labelRoamerRoutes.Text = "";
            }
            else if (radioBtnHgSs.Checked)
            {
                labelVerificationType.Text = "Elm Responses for Seed:";
                //labelFlipsElmsForSeed.Text = ElmResponse.GetResponses(seed, 10, 0);

                // Handle all of the roaming Pokemon here            
                uint rRoute = 0;
                uint eRoute = 0;
                uint lRoute = 0;

                // need to tryparse out all of the route values
                if (maskedTextBoxRRoute.Text != "")
                    rRoute = uint.Parse(maskedTextBoxRRoute.Text);

                if (maskedTextBoxERoute.Text != "")
                    eRoute = uint.Parse(maskedTextBoxERoute.Text);

                if (maskedTextBoxLRoute.Text != "")
                    lRoute = uint.Parse(maskedTextBoxLRoute.Text);

                //  We need to know two things, forced advancement and the 
                //  starting route of each of the roamers the user has
                //  shown interest in --
                HgSsRoamerInformation information = HgSsRoamers.GetHgSsRoamerInformation(
                    Seed,
                    checkBoxRPresent.Checked,
                    checkBoxEPresent.Checked,
                    checkBoxLPresent.Checked,
                    rRoute,
                    eRoute,
                    lRoute);

                //  Build our roaming monster string
                //labelRoamerRoutes.Text = "";

                string labelRoamerRoutesText = "";

                bool firstDisplay = true;

                if (checkBoxRPresent.Checked)
                {
                    labelRoamerRoutesText += "R: " + information.RCurrentRoute;
                    firstDisplay = false;
                }

                if (checkBoxEPresent.Checked)
                {
                    if (!firstDisplay)
                        labelRoamerRoutesText += "  ";

                    labelRoamerRoutesText += "E: " + information.ECurrentRoute;
                    firstDisplay = false;
                }

                if (checkBoxLPresent.Checked)
                {
                    if (!firstDisplay)
                        labelRoamerRoutesText += "  ";

                    labelRoamerRoutesText += "L: " + information.LCurrentRoute;
                    firstDisplay = false;
                }

                if (!firstDisplay)
                {
                    labelRoamerRoutesText += "  ---  ";
                    labelRoamerRoutesText += "Frame(s) Advanced: " + information.RngCalls;
                }

                labelRoamerRoutes.Text = labelRoamerRoutesText;

                //  Handle elm here, letting it know the foced advancement
                labelFlipsElmsForSeed.Text = Responses.ElmResponses(Seed, 15, information.RngCalls);
            }
            else
            {
                labelVerificationType.Text = "First 10 IVs in Seed:";
                labelFlipsElmsForSeed.Text = Gen5IVs.GetIVs(Seed, 1, 10);
                labelRoamerRoutes.Text = "";
            }

            //  Break seed out into parts
            if (!radioBtnBW.Checked)
            {
                MAC_Address = 0;
            }
            uint partialmac = (uint) MAC_Address & 0xFFFFFF;
            uint ab = (Seed - partialmac) >> 24;
            uint cd = ((Seed - partialmac) & 0x00FF0000) >> 16;
            uint efgh = (Seed - partialmac) & 0x0000FFFF;

            //  Get the year and the seed from the dialog
            //  we need to get the year because we let the
            //  user change this in the dialog

            // wfy this can fail if nothing entred
            int generateYear = int.Parse(maskedTextBoxYear.Text);

            //  Get Delay
            int delay = (int) efgh + (2000 - generateYear);

            //  Get Hour
            var hour = (int) cd;

            //  We need to check here, as a user could have entered a seed
            //  that is not possible (invalid hour) to lets warn and exit
            //  on it.
            if (hour > 23)
            {
                MessageBox.Show("This seed is invalid, please verify that you have entered it correctly and try again.",
                                "Invalid Seed", MessageBoxButtons.OK);

                return;
            }

            var timeAndDeleays = new List<TimeAndDelay>();

            //  Loop through all months
            for (int month = 1; month <= 12; month++)
            {
                int daysInMonth = DateTime.DaysInMonth(generateYear, month);

                //  Loop through all days
                for (int day = 1; day <= daysInMonth; day ++)
                {
                    //  Loop through all minutes
                    for (int minute = 0; minute <= 59; minute++)
                    {
                        //  Loop through all seconds
                        for (int second = 0; second <= 59; second++)
                        {
                            if (ab == ((month*day + minute + second)&0xFF))
                            {
                                if (!checkBoxLockSeconds.Checked || second == lockedSecond)
                                {
                                    //  Create Date/Time and add item to collection
                                    var timeAndDelay = new TimeAndDelay();

                                    //  Build DateTime
                                    var dateTime = new DateTime(generateYear, month, day, hour, minute, second);

                                    timeAndDelay.Date = dateTime;
                                    timeAndDelay.Delay = delay;

                                    //  Add to collection
                                    timeAndDeleays.Add(timeAndDelay);
                                }
                            }
                        }
                    }
                }
            }

            //  Do our databind to the grid here so the user 
            //  can get the time listing.
            dataGridViewValues.DataSource = timeAndDeleays;
        }

        private void checkBoxLockSeconds_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBoxSeconds.Enabled = checkBoxLockSeconds.Checked;
        }

        private void dataGridViewAdjacents_KeyUp(object sender, KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.KeyCode))
            {
                sekrit = true;
            }
        }

        private void buttonGenerateAdjacents_Click(object sender, EventArgs e)
        {
            if (radioBtnDPPt.Checked)
            {
                ColumnFlipSequence.Visible = true;
                ColumnElmResponse.Visible = false;
                ColumnRoamers.Visible = false;
                ColumnGen5IVs.Visible = false;
            }
            else if (radioBtnHgSs.Checked)
            {
                ColumnFlipSequence.Visible = false;
                ColumnElmResponse.Visible = true;
                ColumnRoamers.Visible = true;
                ColumnGen5IVs.Visible = false;
            }
            else
            {
                ColumnFlipSequence.Visible = false;
                ColumnElmResponse.Visible = false;
                ColumnRoamers.Visible = false;
                ColumnGen5IVs.Visible = true;
                ColumnCGearAdjust.Visible = sekrit;
            }

            if (maskedTextBoxMDelay.Text == "")
            {
                maskedTextBoxMDelay.Focus();
                return;
            }

            if (maskedTextBoxPDelay.Text == "")
            {
                maskedTextBoxPDelay.Focus();
                return;
            }

            if (maskedTextBoxMSecond.Text == "")
            {
                maskedTextBoxMSecond.Focus();
                return;
            }

            if (maskedTextBoxPSecond.Text == "")
            {
                maskedTextBoxPSecond.Focus();
                return;
            }

            //  Make sure that something is selected in the main
            //  times grid and then go ahead and get that item
            if (dataGridViewValues.SelectedRows.Count == 0)
            {
                return;
            }

            //  Load all of our +/- so we can use them to generate
            //  or list of adjacent frames.
            int mDelay = int.Parse(maskedTextBoxMDelay.Text);
            int pDelay = int.Parse(maskedTextBoxPDelay.Text);
            int mSecond = int.Parse(maskedTextBoxMSecond.Text);
            int pSecond = int.Parse(maskedTextBoxPSecond.Text);

            var timeAndDelay = (TimeAndDelay) dataGridViewValues.SelectedRows[0].DataBoundItem;

            //  From the actual time we need to build a start
            //  time and an end time so that we can iterate
            DateTime startTime = timeAndDelay.Date - new TimeSpan(0, 0, mSecond);
            DateTime endTime = timeAndDelay.Date + new TimeSpan(0, 0, pSecond);

            //  Figure out how many seconds there are between the times 
            //  as this is going to be the number of times that we are
            //  going to loop, plus 1.
            TimeSpan span = endTime - startTime;

            int startDelay = timeAndDelay.Delay - mDelay;
            int endDelay = timeAndDelay.Delay + pDelay;

            // Grab our roamer information so we only have to do it one
            // time and can just run the code and pass the results into
            // the adjacents.
            uint rRoute = 0;
            uint eRoute = 0;
            uint lRoute = 0;

            // need to tryparse out all of the route values
            if (maskedTextBoxRRoute.Text != "")
                rRoute = uint.Parse(maskedTextBoxRRoute.Text);

            if (maskedTextBoxERoute.Text != "")
                eRoute = uint.Parse(maskedTextBoxERoute.Text);

            if (maskedTextBoxLRoute.Text != "")
                lRoute = uint.Parse(maskedTextBoxLRoute.Text);

            var adjacents = new List<Adjacent>();

            int oddEven = timeAndDelay.Delay & 1;

            int minFrame;
            int maxFrame;

            int.TryParse(maskedTextBoxMinFrame.Text, out minFrame);
            int.TryParse(maskedTextBoxMaxFrame.Text, out maxFrame);

            if (minFrame == 0)
            {
                minFrame = 1;
            }

            if (maxFrame == 0)
            {
                maxFrame = 10;
            }

            if (checkBoxRoamer.Checked)
            {
                minFrame++;
                maxFrame++;
            }

            for (int cnt = 0; cnt <= (int) span.TotalSeconds; cnt++)
            {
                DateTime seedTime = startTime + new TimeSpan(0, 0, cnt);

                //  Now we need to loop through all of our delay range
                //  so that we have all of the information to create 
                //  a seed.
                for (int delayCnt = startDelay; delayCnt <= endDelay; delayCnt++)
                {
                    if (!checkBoxOddEven.Checked || (delayCnt & 1) == oddEven)
                    {
                        //  Create the seed an add to the collection

                        var adjacent = new Adjacent
                            {
                                Delay = delayCnt,
                                Date = seedTime,
                                MinFrame = minFrame,
                                MaxFrame = maxFrame + 6,
                                Seed = ((((uint) seedTime.Month*
                                          (uint) seedTime.Day +
                                          (uint) seedTime.Minute +
                                          (uint) seedTime.Second)&0xFF)*0x1000000) +
                                       ((uint) seedTime.Hour*0x10000) +
                                       ((uint) seedTime.Year - 2000 + (uint) delayCnt) +
                                       // only part of the MAC Address is used
                                       ((uint) MAC_Address & 0xFFFFFF)
                            };


                        adjacent.RoamerInformtion = HgSsRoamers.GetHgSsRoamerInformation(
                            adjacent.Seed,
                            checkBoxRPresent.Checked,
                            checkBoxEPresent.Checked,
                            checkBoxLPresent.Checked,
                            rRoute,
                            eRoute,
                            lRoute);

                        adjacents.Add(adjacent);
                    }
                }
            }

            //  Bind to the collection
            dataGridViewAdjacents.DataSource = adjacents;

            //highlight the target
            int target = adjacents.FindIndex(IsTarget);
            dataGridViewAdjacents.FirstDisplayedScrollingRowIndex = target;
        }

        public void setBW()
        {
            radioBtnBW.Checked = true;
        }

        public void setHGSS()
        {
            radioBtnHgSs.Checked = true;
        }

        public void setDPPt()
        {
            radioBtnDPPt.Checked = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Settings.Default.sttSeconds = maskedTextBoxSeconds.Text;
            Settings.Default.sttSecondsLocked = checkBoxLockSeconds.Checked;
            Settings.Default.sttMDelay = maskedTextBoxMDelay.Text;
            Settings.Default.sttPDelay = maskedTextBoxPDelay.Text;
            Settings.Default.sttMSecond = maskedTextBoxMSecond.Text;
            Settings.Default.sttPSecond = maskedTextBoxPSecond.Text;
            Settings.Default.Save();

            Close();
        }

        private void buttonSearchFlips_Click(object sender, EventArgs e)
        {
            dataGridViewAdjacents.MultiSelect = false;
            if (radioBtnDPPt.Checked)
            {
                var adjacents = (List<Adjacent>) dataGridViewAdjacents.DataSource;
                var searchFlips = new SearchFlips(adjacents);
                if (searchFlips.ShowDialog() == DialogResult.OK)
                {
                    if (searchFlips.Possible.Count > 0)
                    {
                        dataGridViewAdjacents.ClearSelection();
                        dataGridViewAdjacents.MultiSelect = true;
                        int target = adjacents.FindIndex(IsTarget);
                        int closest = 0;
                        foreach (Adjacent adjacent in searchFlips.Possible)
                        {
                            int index = adjacents.IndexOf(adjacent);
                            dataGridViewAdjacents.Rows[index].Selected = true;

                            //store the closest one to the target
                            if (Math.Abs(target - closest) > Math.Abs(index - target))
                                closest = index;
                        }
                        //select the closest
                        dataGridViewAdjacents.FirstDisplayedScrollingRowIndex = closest;
                        //dataGridViewAdjacents.MultiSelect = false;
                    }
                    else
                        MessageBox.Show("No match was found for your flips.", "No Match", MessageBoxButtons.OK);
                }
            } // if (radioBtnDPPt.Checked)

            if (radioBtnHgSs.Checked)
            {
                //  We need to bring up the special searcher for roamers and/or 
                //  elm flips.  Also need to figure out how we are going to search
                var adjacents = (List<Adjacent>) dataGridViewAdjacents.DataSource;

                var searchElm = new SearchElm(adjacents);
                if (adjacents != null && searchElm.ShowDialog() == DialogResult.OK)
                {
                    if (searchElm.Possible.Count > 0)
                    {
                        dataGridViewAdjacents.ClearSelection();
                        dataGridViewAdjacents.MultiSelect = true;
                        int target = adjacents.FindIndex(IsTarget);
                        int closest = 0;
                        foreach (Adjacent adjacent in searchElm.Possible)
                        {
                            int index = adjacents.IndexOf(adjacent);
                            dataGridViewAdjacents.Rows[index].Selected = true;

                            //store the closest one to the target
                            if (Math.Abs(target - closest) > Math.Abs(index - target))
                                closest = index;
                        }
                        //select the closest
                        dataGridViewAdjacents.FirstDisplayedScrollingRowIndex = closest;
                        //dataGridViewAdjacents.MultiSelect = false;
                    }
                    else
                        MessageBox.Show("No match was found for your Elm responses.", "No Match",
                                        MessageBoxButtons.OK);
                }
            } // if (radioBtnHgSs.Checked)

            if (radioBtnBW.Checked)
            {
                var searchIVs = new SearchIVs(checkBoxRoamer.Checked);

                if (searchIVs.ShowDialog() == DialogResult.OK)
                {
                    var adjacents = (List<Adjacent>) dataGridViewAdjacents.DataSource;

                    if (adjacents != null)
                    {
                        int cnt = 0;

                        bool found = false;

                        foreach (Adjacent adjacent in adjacents)
                        {
                            if (adjacent.IVs.Contains(searchIVs.ReturnIVs))
                            {
                                dataGridViewAdjacents.FirstDisplayedScrollingRowIndex = cnt;
                                dataGridViewAdjacents.Rows[cnt].Selected = true;

                                found = true;

                                break;
                            }

                            cnt++;
                        }

                        if (!found)
                            MessageBox.Show("No match was found for your IVs.", "No Match", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private bool IsTarget(Adjacent adjacent)
        {
            var timeAndDelay = (TimeAndDelay) dataGridViewValues.SelectedRows[0].DataBoundItem;
            return adjacent.Date == timeAndDelay.Date && adjacent.Delay == timeAndDelay.Delay;
        }

        private void buttonSearchRoamers_Click(object sender, EventArgs e)
        {
            var searchRoamers = new SearchRoamers();
            if (searchRoamers.ShowDialog() == DialogResult.OK)
            {
                var adjacents = (List<Adjacent>) dataGridViewAdjacents.DataSource;

                if (adjacents != null)
                {
                    int cnt = 0;
                    bool found = false;

                    string roamerText = "";
                    bool firstDisplay = true;

                    if (searchRoamers.ReturnR != 0)
                    {
                        roamerText += "R: " + searchRoamers.ReturnR;
                        firstDisplay = false;
                    }

                    if (searchRoamers.ReturnE != 0)
                    {
                        if (!firstDisplay)
                            roamerText += "  ";

                        roamerText += "E: " + searchRoamers.ReturnE;
                        firstDisplay = false;
                    }

                    if (searchRoamers.ReturnL != 0)
                    {
                        if (!firstDisplay)
                            roamerText += "  ";

                        roamerText += "L: " + searchRoamers.ReturnL;
                    }

                    foreach (Adjacent adjacent in adjacents)
                    {
                        if (roamerText == adjacent.RoamerLocations)
                        {
                            dataGridViewAdjacents.FirstDisplayedScrollingRowIndex = cnt;
                            dataGridViewAdjacents.Rows[cnt].Selected = true;

                            found = true;

                            break;
                        }

                        cnt++;
                    }

                    if (!found)
                        MessageBox.Show("No match was found for your Roaming Pokemon.", "No Match", MessageBoxButtons.OK);
                }
            }
        }

        private void contextMenuStripAdjacents_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewAdjacents.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void copySeedToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdjacents.SelectedRows[0] != null)
            {
                var adjacent = (Adjacent) dataGridViewAdjacents.SelectedRows[0].DataBoundItem;

                Clipboard.SetText(adjacent.Seed.ToString("X8"));
            }
        }

        private void generateTXTFileToolStripMenuItem_Click(object sender, EventArgs e)
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
                var adjacents = (List<Adjacent>) dataGridViewAdjacents.DataSource;

                if (adjacents.Count > 0)
                {
                    var writer = new TXTWriter(dataGridViewAdjacents);
                    writer.Generate(saveFileDialogTxt.FileName, adjacents);
                }
            }
        }

        private void dataGridViewAdjacents_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewAdjacents.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewAdjacents.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewAdjacents.ClearSelection();

                        (dataGridViewAdjacents.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void radioBtnHgSs_CheckedChanged(object sender, EventArgs e)
        {
            SetHgSsItems();
        }

        private void radioBtnDPPt_CheckedChanged(object sender, EventArgs e)
        {
            SetHgSsItems();
        }

        private void SetHgSsItems()
        {
            checkBoxEPresent.Enabled = radioBtnHgSs.Checked;
            checkBoxRPresent.Enabled = radioBtnHgSs.Checked;
            checkBoxLPresent.Enabled = radioBtnHgSs.Checked;
            maskedTextBoxERoute.Enabled = radioBtnHgSs.Checked;
            maskedTextBoxRRoute.Enabled = radioBtnHgSs.Checked;
            maskedTextBoxLRoute.Enabled = radioBtnHgSs.Checked;

            ColumnFlipSequence.Visible = radioBtnDPPt.Checked;
            ColumnElmResponse.Visible = radioBtnHgSs.Checked;
            ColumnRoamers.Visible = radioBtnHgSs.Checked;
            ColumnGen5IVs.Visible = radioBtnBW.Checked;
            comboBoxProfiles.Visible = radioBtnBW.Checked;
            labelMAC.Visible = radioBtnBW.Checked;

            buttonSearchRoamers.Visible = radioBtnHgSs.Checked;
            buttonRoamerMap.Enabled = radioBtnHgSs.Checked;
            maskedTextBoxMinFrame.Visible = radioBtnBW.Checked;
            maskedTextBoxMaxFrame.Visible = radioBtnBW.Checked;
            labelMinFrame.Visible = radioBtnBW.Checked;
            labelMaxFrame.Visible = radioBtnBW.Checked;
            checkBoxRoamer.Visible = radioBtnBW.Checked;

            if (radioBtnHgSs.Checked)
            {
                buttonSearch.Text = "Search Calls";
            }
            else if (radioBtnDPPt.Checked)
            {
                buttonSearch.Text = "Search Flips";
            }
            else
            {
                buttonSearch.Text = "Search IVs";
            }
        }

        private void buttonRoamerMap_Click(object sender, EventArgs e)
        {
            HgSsRoamerSW.Window.Show();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            MAC_Address = ((Profile) comboBoxProfiles.SelectedItem).MAC_Address;
            labelMAC.Text = "Profile: " + MAC_Address.ToString("X");
        }
    }

    public class TimeAndDelay
    {
        private DateTime date;

        public DateTime Date
        {
            set { date = value; }
            get { return date; }
        }

        public string DisplayDate
        {
            get { return string.Format("{0:yyyy MM dd}", date); }
        }

        public string DisplayTime
        {
            get { return string.Format("{0:HH:mm:ss}", date); }
        }

        public int Delay { get; set; }
    }
}