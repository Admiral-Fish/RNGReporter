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
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using Version = RNGReporter.Objects.Version;

namespace RNGReporter
{
    public partial class Pandora
    {
        private const int MAX_RESULTS = 1000;
        private readonly DataTable dt = new DataTable();
        private uint CCCCMax;
        private uint CCCCMin;
        private uint Day;
        private uint Delay;
        private uint DesiredID;
        private uint DesiredSID;
        private string ErrorMsg;
        private uint ErrorNo;
        private uint FinalXor;
        private uint Hour;
        private uint IDObtained;
        private uint LowerPID;

        private uint MaxDelay;
        private uint MaxPercent;
        private uint MinDelay;
        private uint Minute;
        private uint Month;

        private uint PID;

        private uint PIDXor;
        private uint Percent;
        private uint Second;
        private uint SecretID;
        private uint Seed;

        private uint SeedAA;
        private uint SeedBB;
        private uint SeedCCCC;

        private uint SeedsFound;

        private uint SeedsSearched;
        private uint TotalSeeds;
        private uint TrainerID;
        private uint TrainerXor;
        private uint UpperPID;
        private uint Year;
        private MersenneTwister a;
        //private char[] alpha = {'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f'};
        private BindingSource binding = new BindingSource();
        private ThreadDelegate gridUpdate;

        private bool isSearching;
        private int maxFrame;
        private int minFrame;
        private BindingSource profilesSource;
        private bool refresh;
        private int resultsCount;
        private List<IDList> resultsList;
        private List<IDListBW> resultsListBW;
        private List<IDListIII> resultsListIII;
        private Thread searchThread;
        private uint y;

        public Pandora(int generation)
        {
            InitializeComponent();
            dgvResults.DataSource = binding;
            dgvResults.AutoGenerateColumns = false;

            tabGenSelect.SelectedIndex = generation - 1;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Set year text boxes to current year
            textBoxShinyYear.Text = DateTime.Now.Year.ToString();
            textBoxIDYear.Text = DateTime.Now.Year.ToString();

            dt.Columns.Add("Seed");
            dt.Columns.Add("Delay");
            dt.Columns.Add("Trainer ID");
            dt.Columns.Add("Secret ID");
            dt.Columns.Add("Seconds");
            if (tabGenSelect.SelectedIndex == 4)
            {
                if (Profiles.List == null || Profiles.List.Count == 0)
                {
                    MessageBox.Show("No profiles were detected. Please setup a profile first.");
                    Profiles.ProfileManager.Visible = false;
                    Profiles.ProfileManager.ShowDialog();
                }
                if (Profiles.List == null || Profiles.List.Count == 0)
                {
                    Close();
                }

                profilesSource = new BindingSource {DataSource = Profiles.List};
                comboBoxProfiles.DataSource = profilesSource;
            }
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                ("Many thanks to:" +
                 ('\r' +
                  ('\r' +
                   ("TCCPhreak, for the major research into Trainer ID generation" +
                    ('\r' +
                     ('\r' +
                      ("LightningFusion, for providing a sample seed/ID combo for testing" +
                       ('\r' +
                        ('\r' +
                         ("mingot, for an easy way to search only viable seeds and other valuable coding advice" +
                          ('\r' +
                           ('\r' +
                            ("http://hocomcast.net/~charltoncr/mt19937ar.htm for providing Mersenne Twister Code" +
                             ('\r' + ('\r' + "You, for downloading and enjoying this program"))))))))))))))), "Credits!");
        }

        private void cbxSearchSID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSearchSID.Checked)
            {
                lblSecretID.Enabled = true;
                textBoxDesiredSID.Enabled = true;
            }
            else
            {
                lblSecretID.Enabled = false;
                textBoxDesiredSID.Enabled = false;
            }
        }

        private void btnShinyGo_Click(object sender, EventArgs e)
        {
            gridUpdate = dataGridUpdate;
            formatGrid();

            // Check for invalid inputs
            if (textBoxShinyPID.Text == "")
            {
                textBoxShinyPID.Focus();
                return;
            }
            PID = Convert.ToUInt32(textBoxShinyPID.Text, 16);

            if (txtShinyMinDelay.Text == "")
            {
                MessageBox.Show("A recommended minimum delay value is 5000.");
                txtShinyMinDelay.Focus();
                return;
            }
            MinDelay = uint.Parse(txtShinyMinDelay.Text);

            if (!cbxShinyInf.Checked)
            {
                Year = uint.Parse(textBoxShinyYear.Text);
                if (Year < 2000 || Year > 2099)
                {
                    MessageBox.Show("Year must be between 2000 and 2099, inclusive.");
                    textBoxShinyYear.Focus();
                    return;
                }

                if (txtShinyMaxDelay.Text == "")
                {
                    txtShinyMaxDelay.Focus();
                    return;
                }
                MaxDelay = uint.Parse(txtShinyMaxDelay.Text);
            }

            if (cbxSearchID.Checked)
            {
                if (!uint.TryParse(textBoxShinyTID.Text, out DesiredID) || DesiredID > 65535)
                {
                    MessageBox.Show("Trainer ID must be a value betwwen 0 and 65535, inclusive.");
                    textBoxShinyTID.Focus();
                    return;
                }
            }

            binding = new BindingSource();
            resultsList = new List<IDList>();
            binding.DataSource = resultsList;
            dgvResults.DataSource = binding;

            btnShinyGo.Enabled = false;
            btnIDGo.Enabled = false;
            btnSeedGo.Enabled = false;
            btnSimpleGo.Enabled = false;
            btnShinyCancel.Enabled = true;

            if (!cbxShinyInf.Checked)
            {
                bgwShiny.RunWorkerAsync();
            }
            else
            {
                bgwShinyInf.RunWorkerAsync();
            }
        }

        private void btnIDGo_Click(object sender, EventArgs e)
        {
            gridUpdate = dataGridUpdate;
            formatGrid();

            // Check for invalid inputs
            if (textBoxDesiredTID.Text == "")
            {
                textBoxDesiredTID.Focus();
                return;
            }
            DesiredID = uint.Parse(textBoxDesiredTID.Text);

            if ((DesiredID > 65535) || (DesiredID < 0))
            {
                MessageBox.Show("Trainer ID must be between 0 and 65535.");
                textBoxDesiredTID.Focus();
                return;
            }

            if (cbxSearchSID.Checked)
            {
                if (textBoxDesiredSID.Text == "")
                {
                    textBoxDesiredSID.Focus();
                    return;
                }
                DesiredSID = uint.Parse(textBoxDesiredSID.Text);

                if ((DesiredSID > 65535) || (DesiredSID < 0))
                {
                    MessageBox.Show("Secret ID must be between 0 and 65535.");
                    textBoxDesiredSID.Focus();
                    return;
                }
            }

            if (textBoxIDMinDelay.Text == "")
            {
                MessageBox.Show("A recommended minimum delay value is 5000.");
                textBoxIDMinDelay.Focus();
                return;
            }
            MinDelay = uint.Parse(textBoxIDMinDelay.Text);

            Year = uint.Parse(textBoxIDYear.Text);
            if (Year < 2000 || Year > 2099)
            {
                MessageBox.Show("Year must be between 2000 and 2099, inclusive.");
                textBoxIDYear.Focus();
                return;
            }
            Year = Year - 2000;

            if (!cbxIDInf.Checked)
            {
                if (textBoxIDMaxDelay.Text == "")
                {
                    textBoxIDMaxDelay.Focus();
                    return;
                }
                MaxDelay = uint.Parse(textBoxIDMaxDelay.Text);

                if (MaxDelay < MinDelay)
                {
                    MessageBox.Show("Max delay must be greater than or equal to min delay.");
                    textBoxIDMaxDelay.Focus();
                    return;
                }
            }

            btnShinyGo.Enabled = false;
            btnIDGo.Enabled = false;
            btnSeedGo.Enabled = false;
            btnSimpleGo.Enabled = false;
            btnIDCancel.Enabled = true;

            binding = new BindingSource();
            resultsList = new List<IDList>();
            binding.DataSource = resultsList;
            dgvResults.DataSource = binding;
            dt.Rows.Clear();

            if (!cbxIDInf.Checked)
            {
                // IDSearch
                bgwID.RunWorkerAsync();
            }
            else
            {
                // IDInfSearch
                bgwIDInf.RunWorkerAsync();
            }

            contextMenuStrip.Enabled = true;
        }

        private void btnSeedGo_Click(object sender, EventArgs e)
        {
            gridUpdate = dataGridUpdate;
            formatGrid();

            btnShinyGo.Enabled = false;
            btnIDGo.Enabled = false;
            btnSeedGo.Enabled = false;
            btnSimpleGo.Enabled = false;
            ErrorNo = 0;
            ErrorMsg = "";
            if (((!IsNumeric(txtSeedYr.Text))
                 || ((!IsNumeric(txtSeedMinDelay.Text))
                     || ((!IsNumeric(txtSeedMaxDelay.Text))
                         || ((!IsNumeric(txtIDObtained.Text))
                             || ((!IsNumeric(txtMonth.Text))
                                 || ((!IsNumeric(txtDay.Text))
                                     || ((!IsNumeric(txtHour.Text))
                                         || (!IsNumeric(txtMinute.Text))))))))))
            {
                ErrorMsg = "At least one of the required fields does not contain a number";
                ErrorNo = (ErrorNo + 1);
            }
            else
            {
                Year = uint.Parse(txtSeedYr.Text);

                if (((Year < 2000)
                     || (Year > 2099)))
                {
                    ErrorMsg = "Invalid Year (2000 <= Year <= 2099)";
                    ErrorNo = (ErrorNo + 1);
                }

                IDObtained = uint.Parse(txtIDObtained.Text);
                if (((IDObtained > 65535)
                     || (IDObtained < 0)))
                {
                    if ((ErrorNo > 0))
                    {
                        ErrorMsg = (ErrorMsg + '\r');
                    }
                    ErrorMsg = (ErrorMsg + "Invalid Trainer ID (0 <= ID <= 65535)");
                    ErrorNo = (ErrorNo + 1);
                }
                Month = uint.Parse(txtMonth.Text);
                if (((Month > 12)
                     || (Month < 1)))
                {
                    if ((ErrorNo > 0))
                    {
                        ErrorMsg = (ErrorMsg + '\r');
                    }
                    ErrorMsg = (ErrorMsg + "Invalid Month (1 <= Month <= 12)");
                    ErrorNo = (ErrorNo + 1);
                }
                Day = uint.Parse(txtDay.Text);
                if (((Day > 31)
                     || ((Day < 1)
                         || ((((Month == 4)
                               || ((Month == 6)
                                   || ((Month == 9)
                                       || (Month == 11))))
                              && (Day > 30))
                             || (((Month == 2)
                                  && (Day > 29))
                                 || ((Month == 2)
                                     && (((Year&3)
                                          != 0)
                                         && (Day > 28))))))))
                {
                    if ((ErrorNo > 0))
                    {
                        ErrorMsg = (ErrorMsg + '\r');
                    }
                    ErrorMsg = (ErrorMsg + "Invalid Day");
                    ErrorNo = (ErrorNo + 1);
                }
                Hour = uint.Parse(txtHour.Text);
                if (((Hour > 23)
                     || (Hour < 0)))
                {
                    if ((ErrorNo > 0))
                    {
                        ErrorMsg = (ErrorMsg + '\r');
                    }
                    ErrorMsg = (ErrorMsg + "Invalid Hour (0 <= Hour <= 23)");
                    ErrorNo = (ErrorNo + 1);
                }
                Minute = uint.Parse(txtMinute.Text);
                if (((Minute > 59)
                     || (Minute < 0)))
                {
                    if ((ErrorNo > 0))
                    {
                        ErrorMsg = (ErrorMsg + '\r');
                    }
                    ErrorMsg = (ErrorMsg + "Invalid Minute (0 <= Minute <= 59)");
                    ErrorNo = (ErrorNo + 1);
                }
            }
            if ((ErrorNo > 0))
            {
                MessageBox.Show(ErrorMsg, "Error(s) Occurred");
            }
            else
            {
                MinDelay = uint.Parse(txtSeedMinDelay.Text);
                MaxDelay = uint.Parse(txtSeedMaxDelay.Text);
                if ((MinDelay <= MaxDelay))
                {
                    MinDelay = uint.Parse(txtSeedMinDelay.Text);
                    MaxDelay = uint.Parse(txtSeedMaxDelay.Text);
                }
                else
                {
                    MaxDelay = uint.Parse(txtSeedMinDelay.Text);
                    MinDelay = uint.Parse(txtSeedMaxDelay.Text);
                }
                SeedsFound = 0;
                lblAction.Text = ("Searching for Obtained ID Seeds... Seeds Found: " + SeedsFound);

                // Set up DataTable for this operation                
                binding = new BindingSource();
                resultsList = new List<IDList>();
                binding.DataSource = resultsList;
                dgvResults.DataSource = binding;
                dt.Rows.Clear();

                // Loop through viable seeds [AABBCCCC] for min and max delays
                // [AA] includes Month/Day/Minute/Seconds, [BB] includes Hours, and [CCCC] includes Year/Delay
                // First establish bounds of [CCCC] based on user input

                CCCCMin = ((Year - 2000) + MinDelay);
                CCCCMax = ((Year - 2000) + MaxDelay);

                SeedBB = Hour;
                for (Second = 0; (Second <= 59); Second++)
                {
                    for (SeedCCCC = CCCCMin; (SeedCCCC <= CCCCMax); SeedCCCC++)
                    {
                        // Establish seed
                        SeedAA = (((Month*Day)
                                   + (Minute + Second)) &0xFF);
                        Seed = (SeedCCCC
                                + ((65536*SeedBB) + (16777216*SeedAA)));
                        // Initialize Mersenne Twister (or IRNG) with new seed
                        a = new MersenneTwister(Seed);

                        // Call MT twice
                        y = a.Nextuint();
                        y = a.Nextuint();
                        TrainerID = (y & 0xFFFF);
                        SecretID = y >> 16;
                        if ((TrainerID == IDObtained))
                        {
                            Delay = (SeedCCCC + (2000 - Year));
                            SeedsFound = (SeedsFound + 1);
                            resultsList.Add(new IDList(Seed, Delay, TrainerID, SecretID, Second));
                            lblAction.Text = ("Searching for Obtained ID Seeds... Seeds Found: " + SeedsFound);
                        }
                    }
                }
                lblAction.Text = ("Desired Obtained Seed Search Completed! Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
            }
            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;
            btnSimpleGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private void bgwShiny_DoWork(object sender, DoWorkEventArgs e)
        {
            var bwShiny = ((BackgroundWorker) (sender));
            e.Result = ShinySearch(bwShiny, e);
            if (bwShiny.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private long ShinySearch(BackgroundWorker worker, DoWorkEventArgs e)
        {
            SeedsSearched = 0;
            SeedsFound = 0;
            Percent = 0;
            MaxPercent = 0;

            lblAction.Text = ("Searching for Shiny Seeds (0% Complete)... Seeds Found: " + SeedsFound);
            // Determine Upper PID Xor Lower PID for comparison with Trainer ID combos generated
            UpperPID = PID >> 16;
            LowerPID = (PID & 0xFFFF);
            PIDXor = UpperPID ^ LowerPID;
            // Loop through viable seeds [AABBCCCC] for min and max delays
            // [AA] includes Month/Day/Minute/Seconds, [BB] includes Hours, and [CCCC] includes Year/Delay
            // First establish bounds of [CCCC] based on user input
            CCCCMin = ((Year - 2000)
                       + MinDelay);
            CCCCMax = ((Year - 2000)
                       + MaxDelay);
            TotalSeeds = ((1
                           + (CCCCMax - CCCCMin))
                          *6144);
            for (SeedCCCC = CCCCMin; (SeedCCCC <= CCCCMax); SeedCCCC++)
            {
                for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                {
                    for (SeedBB = 0; (SeedBB <= 23); SeedBB++)
                    {
                        // Establish seed
                        Seed = (SeedCCCC + ((65536*SeedBB) + (16777216*SeedAA)));
                        // Initialize Mersenne Twister (or IRNG) with new seed
                        a = new MersenneTwister(Seed);
                        // Call MT twice
                        y = a.Nextuint();
                        y = a.Nextuint();
                        TrainerID = (y & 0xFFFF);
                        SecretID = y >> 16;
                        TrainerXor = TrainerID ^ SecretID;
                        // Determine if this ID combo causes the desired PID to return shiny
                        FinalXor = PIDXor ^ TrainerXor;
                        SeedsSearched = (SeedsSearched + 1);

                        Percent = 100*SeedsSearched/TotalSeeds;

                        if ((Percent > MaxPercent))
                        {
                            lblAction.Text = ("Searching for Shiny Seeds ("
                                              + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                            MaxPercent = Percent;
                        }

                        if (((FinalXor < 8)
                             && !(cbxSearchID.Checked
                                  && (TrainerID != DesiredID))))
                        {
                            Delay = (SeedCCCC + (2000 - Year));
                            SeedsFound = (SeedsFound + 1);
                            //dt.Rows.Add(Hex(Seed), Delay, TrainerID, SecretID, "");
                            resultsList.Add(new IDList(Seed, Delay, TrainerID, SecretID, 0));
                            Invoke(gridUpdate);
                            lblAction.Text = ("Searching for Shiny Seeds ("
                                              + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }

            return 0L;
        }

        private void bgwShiny_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblAction.Text = "An error occurred.";
            }
            else if (e.Cancelled)
            {
                lblAction.Text = ("Shiny Seed Search Canceled. Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            else
            {
                lblAction.Text = ("Shiny Seed Search Completed! Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;
            btnShinyCancel.Enabled = false;
            btnSimpleGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private void btnShinyCancel_Click(object sender, EventArgs e)
        {
            bgwShiny.CancelAsync();
            bgwShinyInf.CancelAsync();
            btnShinyCancel.Enabled = false;
        }

        private void bgwID_DoWork(object sender, DoWorkEventArgs e)
        {
            var bwID = ((BackgroundWorker) (sender));
            e.Result = IDSearch(bwID, e);
            if (bwID.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private long IDSearch(BackgroundWorker worker, DoWorkEventArgs e)
        {
            SeedsSearched = 0;
            SeedsFound = 0;
            Percent = 0;
            MaxPercent = 0;
            lblAction.Text = ("Searching for Desired ID Seeds (0% Complete)... Seeds Found: " + SeedsFound);
            // Loop through viable seeds [AABBCCCC] for min and max delays
            // [AA] includes Month/Day/Minute/Seconds, [BB] includes Hours, and [CCCC] includes Year/Delay
            // First establish bounds of [CCCC] based on user input
            CCCCMin = (Year
                       + MinDelay);
            CCCCMax = (Year
                       + MaxDelay);
            TotalSeeds = ((1
                           + (CCCCMax - CCCCMin))
                          *6144);
            for (SeedCCCC = CCCCMin; (SeedCCCC <= CCCCMax); SeedCCCC++)
            {
                for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                {
                    for (SeedBB = 0; (SeedBB <= 23); SeedBB++)
                    {
                        // Establish seed
                        Seed = SeedCCCC | (SeedBB << 16) | (SeedAA << 24);
                        // Initialize Mersenne Twister (or IRNG) with new seed
                        a = new MersenneTwister(Seed);
                        // Call MT twice
                        y = a.Nextuint();
                        y = a.Nextuint();
                        TrainerID = (y & 0xFFFF);
                        SecretID = y >> 16;
                        SeedsSearched = (SeedsSearched + 1);
                        Percent = 100*SeedsSearched/TotalSeeds;
                        if ((Percent > MaxPercent))
                        {
                            lblAction.Text = ("Searching for Desired ID Seeds ("
                                              + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                            MaxPercent = Percent;
                        }
                        if (((TrainerID == DesiredID)
                             && !(cbxSearchSID.Checked
                                  && (SecretID != DesiredSID))))
                        {
                            Delay = (SeedCCCC - Year);
                            SeedsFound = (SeedsFound + 1);
                            resultsList.Add(new IDList(Seed, Delay, TrainerID, SecretID, 0));
                            Invoke(gridUpdate);
                            lblAction.Text = ("Searching for Desired ID Seeds ("
                                              + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }

            return 0L;
        }

        private void bgwID_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblAction.Text = "An error occurred.";
            }
            else if (e.Cancelled)
            {
                lblAction.Text = ("Desired ID Seed Search Canceled. Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            else
            {
                lblAction.Text = ("Desired ID Seed Search Completed! Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;
            btnIDCancel.Enabled = false;
            btnSimpleGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private void btnIDCancel_Click(object sender, EventArgs e)
        {
            bgwID.CancelAsync();
            bgwIDInf.CancelAsync();
            btnIDCancel.Enabled = false;
        }

        private void cbxIDInf_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxIDInf.Checked)
            {
                lblIDYr.Enabled = false;
                textBoxIDYear.Enabled = false;
                lblIDMaxDelay.Enabled = false;
                textBoxIDMaxDelay.Enabled = false;
            }
            else
            {
                lblIDYr.Enabled = true;
                textBoxIDYear.Enabled = true;
                lblIDMaxDelay.Enabled = true;
                textBoxIDMaxDelay.Enabled = true;
            }
        }

        private void bgwIDInf_DoWork(object sender, DoWorkEventArgs e)
        {
            var bwIDInf = ((BackgroundWorker) (sender));
            e.Result = IDInfSearch(bwIDInf, e);
            if (bwIDInf.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private long IDInfSearch(BackgroundWorker worker, DoWorkEventArgs e)
        {
            SeedsSearched = 0;
            SeedsFound = 0;
            Percent = 0;
            MaxPercent = 0;

            lblAction.Text = ("Searching for Desired ID Seeds (Through 0 Delay, 0% Complete)... Seeds Found: " +
                              SeedsFound);

            // Loop through viable seeds [AABBCCCC] for min and max delays
            // [AA] includes Month/Day/Minute/Seconds, [BB] includes Hours, and [CCCC] includes Year/Delay
            if ((MinDelay < 65536))
            {
                TotalSeeds = 3892314112 + (65536 - MinDelay)*6144;
            }
            else
            {
                TotalSeeds = 3892314112 + (65536 - MinDelay)*256;
            }
            if ((MinDelay < 65536))
            {
                for (SeedCCCC = MinDelay; (SeedCCCC <= 65535); SeedCCCC++)
                {
                    for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                    {
                        for (SeedBB = 0; (SeedBB <= 23); SeedBB++)
                        {
                            // Establish seed
                            Seed = SeedCCCC | (SeedBB << 16) | (SeedAA << 24);
                            // Initialize Mersenne Twister (or IRNG) with new seed
                            a = new MersenneTwister(Seed);
                            // Call MT twice
                            y = a.Nextuint();
                            y = a.Nextuint();
                            TrainerID = (y & 0xFFFF);
                            SecretID = y >> 16;
                            SeedsSearched = (SeedsSearched + 1);

                            MaxPercent = 100*SeedsSearched/TotalSeeds;

                            lblAction.Text = ("Searching for Desired ID Seeds (Through "
                                              + (SeedCCCC + (" Delay, "
                                                             +
                                                             (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)))));
                            // If Percent > MaxPercent Then
                            //   lblAction.Text = "Searching for Desired ID Seeds (Through " & SeedCCCC & " Delay, " & MaxPercent & "% Complete)... Seeds Found: " & SeedsFound
                            //    MaxPercent = Percent
                            // End If
                            if (((TrainerID == DesiredID)
                                 && !(cbxSearchSID.Checked
                                      && (SecretID != DesiredSID))))
                            {
                                SeedsFound = (SeedsFound + 1);
                                //dt.Rows.Add(Hex(Seed), SeedCCCC, TrainerID, SecretID, "");
                                resultsList.Add(new IDList(Seed, SeedCCCC, TrainerID, SecretID, 0));
                                Invoke(gridUpdate);
                                lblAction.Text = ("Searching for Desired ID Seeds (Through"
                                                  + (SeedCCCC + (" Delay, "
                                                                 +
                                                                 (MaxPercent +
                                                                  ("% Complete)... Seeds Found: " + SeedsFound)))));
                            }
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                SeedBB = 23;
                for (SeedCCCC = Math.Max(65536, MinDelay); (SeedCCCC <= 15269887); SeedCCCC++)
                {
                    for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                    {
                        // Establish seed
                        Seed = SeedCCCC | (SeedBB << 16) | (SeedAA << 24);
                        // Initialize Mersenne Twister (or IRNG) with new seed
                        a = new MersenneTwister(Seed);
                        // Call MT twice
                        y = a.Nextuint();
                        y = a.Nextuint();
                        TrainerID = (y & 0xFFFF);
                        SecretID = y >> 16;
                        SeedsSearched = (SeedsSearched + 1);

                        MaxPercent = 100*SeedsSearched/TotalSeeds;

                        lblAction.Text = ("Searching for Desired ID Seeds (Through "
                                          + (SeedCCCC + (" Delay, "
                                                         + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)))));
                        // If Percent > MaxPercent Then
                        // lblAction.Text = "Searching for Desired ID Seeds (Through " & SeedCCCC & " Delay, " & MaxPercent & "% Complete)... Seeds Found: " & SeedsFound
                        // MaxPercent = Percent
                        // End If
                        if (((TrainerID == DesiredID)
                             && !(cbxSearchSID.Checked
                                  && (SecretID != DesiredSID))))
                        {
                            SeedsFound = (SeedsFound + 1);
                            //dt.Rows.Add(Hex(Seed), SeedCCCC, TrainerID, SecretID, "");
                            resultsList.Add(new IDList(Seed, SeedCCCC, TrainerID, SecretID, 0));
                            Invoke(gridUpdate);
                            lblAction.Text = ("Searching for Desired ID Seeds (Through"
                                              + (SeedCCCC + (" Delay, "
                                                             +
                                                             (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)))));
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }

            return 0L;
        }

        private void bgwIDInf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblAction.Text = ("An error occurred. Seeds Searched: "
                                  + (SeedsSearched + (" ("
                                                      + (SeedCCCC + (" Delay), Seeds Found: " + SeedsFound)))));
            }
            else if (e.Cancelled)
            {
                lblAction.Text = ("Desired ID Seed Search Canceled. Seeds Searched: "
                                  + (SeedsSearched + (" ("
                                                      + (SeedCCCC + (" Delay), Seeds Found: " + SeedsFound)))));
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            else
            {
                lblAction.Text = ("Desired ID Seed Search Completed! Seeds Searched: "
                                  + (SeedsSearched + (" ("
                                                      + (SeedCCCC + (" Delay), Seeds Found: " + SeedsFound)))));
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;
            btnIDCancel.Enabled = false;
            btnSimpleGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private void btnSimpleGo_Click(object sender, EventArgs e)
        {
            resultsList = new List<IDList>();
            binding.DataSource = resultsList;
            formatGrid();

            btnShinyGo.Enabled = false;
            btnIDGo.Enabled = false;
            btnSeedGo.Enabled = false;
            ErrorNo = 0;
            ErrorMsg = "";
            if ((textBoxSeed.Text == ""))
            {
                ErrorMsg = "The seed field was left blank.";
                ErrorNo = (ErrorNo + 1);
            }
            if ((ErrorNo > 0))
            {
                MessageBox.Show(ErrorMsg, "Error(s) Occurred");
            }
            else
            {
                Seed = Convert.ToUInt32(textBoxSeed.Text, 16);

                SeedAA = Seed >> 24;
                SeedBB = (Seed >> 16) & 0xFF;
                SeedCCCC = Seed & 0xFFFF;

                // Initialize Mersenne Twister (or IRNG) with new seed
                a = new MersenneTwister(Seed);
                // Call MT twice
                y = a.Nextuint();
                y = a.Nextuint();
                TrainerID = y & 0xFFFF;
                SecretID = y >> 16;
                //dt.Rows.Add(Hex(Seed), SeedCCCC, TrainerID, SecretID, "");
                resultsList.Add(new IDList(Seed, SeedCCCC, TrainerID, SecretID, 0));
                Invoke(gridUpdate);
                lblAction.Text = "Simple Seed to ID/SID conversion complete!";
            }
            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private void cbxSearchID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSearchID.Checked)
            {
                lblShinyTrainerID.Enabled = true;
                textBoxShinyTID.Enabled = true;
            }
            else
            {
                lblShinyTrainerID.Enabled = false;
                textBoxShinyTID.Enabled = false;
            }
        }

        private void cbxShinyInf_CheckedChanged(object sender, EventArgs e)
        {
            if ((cbxShinyInf.Checked))
            {
                lblShinyYr.Enabled = false;
                textBoxShinyYear.Enabled = false;
                lblShinyMaxDelay.Enabled = false;
                txtShinyMaxDelay.Enabled = false;
            }
            else
            {
                lblShinyYr.Enabled = true;
                textBoxShinyYear.Enabled = true;
                lblShinyMaxDelay.Enabled = true;
                txtShinyMaxDelay.Enabled = true;
            }
        }

        private void bgwShinyInf_DoWork(object sender, DoWorkEventArgs e)
        {
            var bwShinyInf = ((BackgroundWorker) (sender));
            e.Result = ShinyInfSearch(bwShinyInf, e);
            if (bwShinyInf.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private long ShinyInfSearch(BackgroundWorker worker, DoWorkEventArgs e)
        {
            SeedsSearched = 0;
            SeedsFound = 0;
            Percent = 0;
            MaxPercent = 0;

            lblAction.Text = ("Searching for Shiny Seeds (Through 0 Delay, 0% Complete)... Seeds Found: " + SeedsFound);
            // Determine Upper PID Xor Lower PID for comparison with Trainer ID combos generated
            UpperPID = PID >> 16;
            LowerPID = PID & 0xFFFF;
            PIDXor = UpperPID ^ LowerPID;

            // Loop through viable seeds [AABBCCCC] for min and max delays
            // [AA] includes Month/Day/Minute/Seconds, [BB] includes Hours, 
            // and [CCCC] includes Year/Delay
            if ((MinDelay < 65536))
            {
                TotalSeeds = 3892314112 + (65536 - MinDelay)*6144;
            }
            else
            {
                TotalSeeds = 3892314112 + (65536 - MinDelay)*256;
            }

            if ((MinDelay < 65536))
            {
                for (SeedCCCC = MinDelay; (SeedCCCC <= 65535); SeedCCCC++)
                {
                    for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                    {
                        for (SeedBB = 0; (SeedBB <= 23); SeedBB++)
                        {
                            // Establish seed
                            Seed = SeedCCCC | (SeedBB << 16) | (SeedAA << 24);
                            // Initialize Mersenne Twister (or IRNG) with new seed
                            a = new MersenneTwister(Seed);
                            // Call MT twice
                            y = a.Nextuint();
                            y = a.Nextuint();
                            TrainerID = y & 0xFFFF;
                            SecretID = (y >> 16);
                            TrainerXor = TrainerID ^ SecretID;
                            FinalXor = PIDXor ^ TrainerXor;
                            SeedsSearched = (SeedsSearched + 1);

                            MaxPercent = 100*SeedsSearched/TotalSeeds;

                            lblAction.Text = ("Searching for Shiny Seeds (Through "
                                              + (SeedCCCC + (" Delay, "
                                                             +
                                                             (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)))));

                            if (((FinalXor < 8)
                                 && !(cbxSearchID.Checked
                                      && (TrainerID != DesiredID))))
                            {
                                SeedsFound = (SeedsFound + 1);
                                resultsList.Add(new IDList(Seed, SeedCCCC, TrainerID, SecretID, 0));
                                Invoke(gridUpdate);
                                lblAction.Text = ("Searching for Shiny Seeds ("
                                                  + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                            }
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;

                        break;
                    }
                }
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                SeedBB = 23;

                for (SeedCCCC = Math.Max(65536, MinDelay); (SeedCCCC <= 15269887); SeedCCCC++)
                {
                    for (SeedAA = 0; (SeedAA <= 255); SeedAA++)
                    {
                        // Establish seed
                        Seed = SeedCCCC | (SeedBB << 16) | (SeedAA << 24);

                        // Initialize Mersenne Twister (or IRNG) with new seed
                        a = new MersenneTwister(Seed);
                        // Call MT twice
                        y = a.Nextuint();
                        y = a.Nextuint();
                        TrainerID = (y & 0xFFFF);
                        SecretID = y >> 16;
                        TrainerXor = TrainerID ^ SecretID;
                        FinalXor = PIDXor ^ TrainerXor;
                        SeedsSearched = (SeedsSearched + 1);

                        MaxPercent = 100*SeedsSearched/TotalSeeds;

                        lblAction.Text = ("Searching for Shiny Seeds (Through "
                                          + (SeedCCCC + (" Delay, "
                                                         + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)))));

                        if (((FinalXor < 8)
                             && !(cbxSearchID.Checked
                                  && (TrainerID != DesiredID))))
                        {
                            SeedsFound = (SeedsFound + 1);
                            resultsList.Add(new IDList(Seed, SeedCCCC, TrainerID, SecretID, 0));
                            //dt.Rows.Add(Hex(Seed), SeedCCCC, TrainerID, SecretID, "");
                            Invoke(gridUpdate);
                            lblAction.Text = ("Searching for Shiny Seeds ("
                                              + (MaxPercent + ("% Complete)... Seeds Found: " + SeedsFound)));
                        }
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }

            return 0L;
        }

        private void bgwShinyInf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblAction.Text = "An error occurred.";
            }
            else if (e.Cancelled)
            {
                lblAction.Text = ("Shiny Seed Search Canceled. Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }
            else
            {
                lblAction.Text = ("Shiny Seed Search Completed! Seeds Found: " + SeedsFound);
                Invoke(gridUpdate);
                //dgvResults.DataSource = dt;
            }

            btnShinyGo.Enabled = true;
            btnIDGo.Enabled = true;
            btnSeedGo.Enabled = true;
            btnShinyCancel.Enabled = false;
            btnSimpleGo.Enabled = true;

            contextMenuStrip.Enabled = true;
        }

        private bool IsNumeric(string strTextEntry)
        {
            var objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strTextEntry);
        }

        //Added for 5th gen

        private void checkTID_CheckedChanged(object sender, EventArgs e)
        {
            textVTID.Enabled = checkVTID.Checked;
        }

        private void checkVSID_CheckedChanged(object sender, EventArgs e)
        {
            textVSID.Enabled = checkVSID.Checked;
        }

        private void textVSeed_TextChanged(object sender, EventArgs e)
        {
            textVSeed.Enabled = checkVSeed.Checked;
            textVFrame.Enabled = checkVSeed.Checked;
            if (checkVSeed.Checked) checkVPID.Checked = false;
        }

        private void checkVPID_CheckedChanged(object sender, EventArgs e)
        {
            textVPID.Enabled = checkVPID.Checked;
            if (checkVPID.Checked) checkVSeed.Checked = false;
        }

        private void buttonVFindSeeds_Click(object sender, EventArgs e)
        {
            formatGridBW();

            if (isSearching)
            {
                MessageBox.Show("The previous search is still running.");
                return;
            }

            contextMenuStrip.Enabled = false;

            try
            {
                minFrame = int.Parse(textVMinFrame.Text);
                maxFrame = int.Parse(textVMaxFrame.Text);
                ulong seed;
                ulong.TryParse(textVSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out seed);
                uint pid;
                uint.TryParse(textVPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out pid);
                uint tid;
                uint.TryParse(textVTID.Text, out tid);
                uint sid;
                uint.TryParse(textVSID.Text, out sid);
                int frame;
                int.TryParse(textVFrame.Text, out frame);
                var dateTime = new DateTime(dateTimeSearch.Value.Year, dateTimeSearch.Value.Month,
                                            dateTimeSearch.Value.Day);
                bool calcMinFrame = checkBoxMinFrameCalc.Checked;
                bool existingFile = checkBoxSaveExists.Checked;

                if (minFrame > maxFrame)
                {
                    textVMinFrame.Focus();
                    textVMinFrame.Text = "28";
                    textVMaxFrame.Text = "40";
                    return;
                }
                if (tid > 65535)
                    MessageBox.Show("IDs can only be between 0 and 65535");

                lblAction.Text = "Searching..";
                resultsListBW = new List<IDListBW>();
                binding = new BindingSource {DataSource = resultsListBW};
                dgvResults.DataSource = binding;

                var profile = (Profile) comboBoxProfiles.SelectedItem;
                searchThread =
                    new Thread(
                        () =>
                        searchGenV(dateTime, checkVMonth.Checked, checkVSeed.Checked, seed, frame, checkVPID.Checked,
                                   pid,
                                   checkVTID.Checked, tid, checkVSID.Checked, sid, calcMinFrame, existingFile,
                                   profile.MAC_Address, profile.Version, profile.Language, profile.DSType,
                                   profile.SoftReset,
                                   profile.VCount, profile.Timer0Min, profile.GxStat, profile.VFrame));
                searchThread.Start();

                var update = new Thread(updateGUI);
                update.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
                throw;
            }
        }

        private void searchGenV(DateTime date, bool entireMonth, bool useSeed, ulong shinySeed, int seedFrame,
                                bool usePID, uint pid, bool useID,
                                uint id, bool useSID, uint sid, bool calcMinFrame, bool existingFile, ulong mAC,
                                Version version, Language language, DSType dstype, bool softRest, uint vCount,
                                uint timer0, uint gxStat,
                                uint vFrame)
        {
            int dayMin, dayMax;
            uint shinyUpper = 0;
            bool xOR2 = false, starter = false;
            var rng = new BWRng(0);
            resultsCount = 0;
            int[] buttons = {0};

            isSearching = true;

            if (useSeed)
            {
                rng.Seed = shinySeed;
                for (int i = 0; i < seedFrame; i++)
                {
                    rng.Next();
                }

                shinyUpper = (uint) (rng.Seed >> 32);

                xOR2 = Convert.ToBoolean((shinyUpper >> 31) ^ (shinyUpper & 1));
            }

            if (entireMonth)
            {
                dayMin = 1;
                dayMax = DateTime.DaysInMonth(date.Year, date.Month);
            }
            else
                dayMin = dayMax = date.Day;

            long total = 86400*(dayMax - dayMin + 1);

            for (int day = dayMin; day <= dayMax; day++)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    for (int minute = 0; minute < 60; minute++)
                    {
                        for (int second = 0; second < 60; second++)
                        {
                            var dTime = new DateTime(date.Year, date.Month, day, hour, minute, second);

                            for (int button = 0; button < 13; button++)
                            {
                                buttons[0] = button;
                                ulong seed = Functions.EncryptSeed(dTime, mAC, version, language, dstype, softRest,
                                                                   vCount, timer0,
                                                                   gxStat,
                                                                   vFrame, Functions.buttonMashed(buttons));

                                rng.Seed = seed;
                                ulong oSeed = seed;

                                if (calcMinFrame)
                                {
                                    minFrame = (int) Functions.initialPIDRNG_ID(seed, existingFile, version);
                                }

                                for (int frame = 0; frame < minFrame; frame++)
                                {
                                    rng.Next();
                                }

                                for (int frame = minFrame; frame <= maxFrame; frame++)
                                {
                                    rng.Next();
                                    seed = rng.Seed;

                                    var upper = (uint) (((seed >> 32)*0xFFFFFFFF) >> 32);
                                    uint tid = (upper & 0xFFFF);
                                    uint tsid = (upper >> 16);

                                    if (useSeed)
                                    {
                                        bool xOR1 = Convert.ToBoolean((tid + tsid) & 1);

                                        if (xOR1 ^ xOR2)
                                        {
                                            pid = shinyUpper ^ 0x80010000;
                                            starter = false;
                                        }
                                        else
                                        {
                                            pid = shinyUpper ^ 0x00010000;
                                            starter = true;
                                        }

                                        pid = ((pid >> 16) ^ (pid << 16 >> 16));
                                    }

                                    if ((!useID || (tid == id)) && (!useSID || (tsid == sid)))
                                    {
                                        if (useSeed && (tid ^ tsid ^ pid) >= 8) continue;
                                        if (usePID && !Functions.Shiny(pid, tid, tsid)) continue;
                                        var iDSeed = new IDListBW
                                            {
                                                Seed = oSeed,
                                                Date = dTime.ToShortDateString(),
                                                Time = dTime.ToString("HH:mm:ss"),
                                                InitialFrame = minFrame,
                                                Frame = frame,
                                                ID = tid,
                                                SID = tsid,
                                                Starter = starter.ToString(),
                                                Button = Functions.buttonStrings[button]
                                            };

                                        resultsListBW.Add(iDSeed);
                                        refresh = true;

                                        if (resultsCount++ >= MAX_RESULTS)
                                        {
                                            lblAction.Text =
                                                "Search stopped - results max reached. Narrow your search for better results.";

                                            isSearching = false;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    lblAction.Text = ((((day - dayMin)*24 + hour + 1)*3600)/(float) total*100) + "%";
                }
            }
            isSearching = false;

            lblAction.Text = "Done. - Awaiting Command";
        }

        private void buttonVFindSeedHit_Click(object sender, EventArgs e)
        {
            formatGridBW();

            if (isSearching)
            {
                MessageBox.Show("The previous search is still running.");
                return;
            }

            contextMenuStrip.Enabled = false;

            try
            {
                minFrame = int.Parse(textVMinFrameHit.Text);
                maxFrame = int.Parse(textVMaxFrameHit.Text);
                int hour = int.Parse(textVHour.Text);
                int minute = int.Parse(textVMinute.Text);
                int minSec = int.Parse(textVMinSec.Text);
                int maxSec = int.Parse(textVMaxSec.Text);
                uint idcheck = uint.Parse(textVTIDReceived.Text);

                if (minute >= 60)
                {
                    textVMinute.Focus();
                    textVMinute.Text = "";
                    return;
                }
                if (hour >= 24)
                {
                    textVHour.Focus();
                    textVHour.Text = "";
                    return;
                }
                if (minSec > maxSec)
                {
                    textVMinSec.Focus();
                    textVMinSec.Text = "0";
                    textVMaxSec.Text = "59";
                    return;
                }
                if (minFrame > maxFrame)
                {
                    textVMinFrameHit.Focus();
                    textVMinFrameHit.Text = "28";
                    textVMaxFrameHit.Text = "40";
                    return;
                }
                if (idcheck > 65535)
                    MessageBox.Show("IDs can only be between 0 and 65535");

                dgvResults.DataSource = null;

                lblAction.Text = "Searching..";
                resultsListBW = new List<IDListBW>();
                binding = new BindingSource {DataSource = resultsListBW};
                dgvResults.DataSource = binding;

                var dateTime = new DateTime(dateTimeSeedSearch.Value.Year, dateTimeSeedSearch.Value.Month,
                                            dateTimeSeedSearch.Value.Day);
                var profile = (Profile) comboBoxProfiles.SelectedItem;
                searchThread =
                    new Thread(
                        () =>
                        searchGenVHit(dateTime, hour, minute, minSec, maxSec, minFrame, maxFrame, idcheck,
                                      profile.MAC_Address, profile.Version, profile.Language, profile.DSType,
                                      profile.SoftReset,
                                      profile.VCount, profile.Timer0Min, profile.GxStat, profile.VFrame));
                searchThread.Start();

                var update = new Thread(updateGUI);
                update.Start();
            }
            catch
            {
                MessageBox.Show("Something went wrong.\rMake sure all inputs necessary inputs contain a value.");
            }
        }

        private void searchGenVHit(DateTime date, int hour, int minute, int minSec, int maxSec, int minframe,
                                   int maxframe, uint id, ulong mAC, Version version, Language language, DSType dstype,
                                   bool softReset,
                                   uint vCount, uint timer0, uint gxStat, uint vFrame)
        {
            var rng = new BWRng(0);
            int[] buttons = {0};

            isSearching = true;

            for (int sec = minSec; sec <= maxSec; sec++)
            {
                var dTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, sec);

                for (int button = 0; button < 13; button++)
                {
                    buttons[0] = button;
                    ulong seed = Functions.EncryptSeed(dTime, mAC, version, language, dstype, softReset, vCount, timer0,
                                                       gxStat,
                                                       vFrame,
                                                       Functions.buttonMashed(buttons));

                    rng.Seed = seed;
                    ulong oSeed = seed;

                    for (int frame = 0; frame < minframe; frame++)
                    {
                        rng.Next();
                    }

                    for (int frame = minframe; frame <= maxframe; frame++)
                    {
                        rng.Next();
                        seed = rng.Seed;

                        var upper = (uint) (((seed >> 32)*0xFFFFFFFF) >> 32);
                        uint tid = (upper & 0xFFFF);
                        uint sid = (upper >> 16);

                        if (tid == id)
                        {
                            var iDSeed = new IDListBW
                                {
                                    Seed = oSeed,
                                    Date = dTime.ToShortDateString(),
                                    Time = dTime.ToString("HH:mm:ss"),
                                    Frame = frame,
                                    InitialFrame = (int) Functions.initialPIDRNG_ID(oSeed, false, version),
                                    ID = tid,
                                    SID = sid,
                                    Starter = "N/A",
                                    Button = Functions.buttonStrings[button]
                                };

                            resultsListBW.Add(iDSeed);

                            refresh = true;
                        }
                    }
                }
            }
            isSearching = false;

            lblAction.Text = "Done. - Awaiting Command";
        }

        private void buttonVCancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                lblAction.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = dgvResults.AutoResizeColumns;
            try
            {
                bool alive = true;
                while (alive)
                {
                    if (refresh)
                    {
                        Invoke(gridUpdate);
                        refresh = false;
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
                Invoke(gridUpdate);
                Invoke(resizeGrid);
            }
        }

        private void dataGridUpdate()
        {
            binding.ResetBindings(false);
        }

        private void formatGridBW()
        {
            clmSeed.DefaultCellStyle.Format = "X16";
            clmID.DefaultCellStyle.Format = "D5";
            clmSID.DefaultCellStyle.Format = "D5";
            clmSeed.Visible = true;
            clmDelay.Visible = false;
            clmSeconds.Visible = false;
            clmInitialFrame.Visible = true;
            clmFrame.Visible = true;
            clmDate.Visible = true;
            clmTime.Visible = true;
            clmStarter.Visible = true;
            clmButton.Visible = true;
            dgvResults.AutoResizeColumns();
        }

        private void formatGrid()
        {
            clmSeed.DefaultCellStyle.Format = "X8";
            clmID.DefaultCellStyle.Format = "D5";
            clmSID.DefaultCellStyle.Format = "D5";
            clmSeed.Visible = true;
            clmDelay.Visible = true;
            clmSeconds.Visible = true;
            clmInitialFrame.Visible = false;
            clmFrame.Visible = false;
            clmDate.Visible = false;
            clmTime.Visible = false;
            clmStarter.Visible = false;
            clmButton.Visible = false;
            dgvResults.AutoResizeColumns();
        }

        private void formatGridIII()
        {
            clmID.DefaultCellStyle.Format = "D5";
            clmSID.DefaultCellStyle.Format = "D5";
            clmSeed.Visible = false;
            clmDelay.Visible = false;
            clmSeconds.Visible = false;
            clmInitialFrame.Visible = false;
            clmFrame.Visible = true;
            clmDate.Visible = false;
            clmTime.Visible = false;
            clmStarter.Visible = false;
            clmButton.Visible = false;
            dgvResults.AutoResizeColumns();
        }

        private void checkBoxMinFrameCalc_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSaveExists.Enabled = checkBoxMinFrameCalc.Checked;
        }

        private void dgvResults_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dgvResults.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!dgvResults.Rows[Hti.RowIndex].Selected)
                    {
                        dgvResults.ClearSelection();

                        dgvResults.Rows[Hti.RowIndex].Selected = true;
                    }
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0 || tabGenSelect.SelectedIndex != 0 || tabGenSelect.SelectedIndex != 1 || tabGenSelect.SelectedIndex != 2)
            {
                e.Cancel = true;
            }
        }

        private void copySeedToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows[0] != null)
            {
                var frame = (IDList)dgvResults.SelectedRows[0].DataBoundItem;

                Clipboard.SetText(frame.Seed.ToString("X8"));
            }
        }

        private void generateTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows[0] != null)
            {
                var frame = (IDList)dgvResults.SelectedRows[0].DataBoundItem;

                // This is a bit of a strange hack, because this window
                //  needs to be hidden before we load the seed to time
                //  form or it wont be able to be focused. 
                bool showMap = HgSsRoamerSW.Window.Map.Visible;
                HgSsRoamerSW.Window.Hide();

                var seedToTime = new SeedToTime();
                seedToTime.setDPPt();

                seedToTime.AutoGenerate = true;
                seedToTime.ShowMap = showMap;
                seedToTime.Seed = frame.Seed;

                //  Grab this from what the user had searched on
                seedToTime.Year = (uint)DateTime.Now.Year;

                seedToTime.Show();
            }
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

        private void buttonIIIFindFrames_Click(object sender, EventArgs e)
        {
            formatGridIII();

            uint id, sid;
            uint pid;
            int hour, minute;

            if (isSearching)
            {
                MessageBox.Show("The previous search is still running.");
                return;
            }

            //check the parameters
            if (!CheckParameters(out pid, out id, out sid, out hour, out minute)) return;

            DateTime seedTime = dateIII.Value;
            seedTime = seedTime.AddHours(hour);
            seedTime = seedTime.AddMinutes(minute);

            lblAction.Text = "Searching..";
            resultsListIII = new List<IDListIII>();
            binding = new BindingSource {DataSource = resultsListIII};
            dgvResults.DataSource = binding;

            searchThread = new Thread(() => searchGenIII(seedTime, checkIIIPID.Checked, pid, checkIIITID.Checked, id, checkIIISID.Checked, sid));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private bool CheckParameters(out uint pid, out uint id, out uint sid, out int hour, out int minute)
        {
            pid = 0;
            id = 0;
            sid = 0;
            hour = 0;
            minute = 0;
            if (checkIIIPID.Checked &&
                !uint.TryParse(textIIIPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out pid))
            {
                textIIIPID.Focus();
                return false;
            }

            if (checkIIITID.Checked && !uint.TryParse(textIIITID.Text, out id))
            {
                textIIITID.Focus();
                return false;
            }

            if (checkIIISID.Checked && !uint.TryParse(textIIISID.Text, out sid))
            {
                textIIISID.Focus();
                return false;
            }

            if (!int.TryParse(textIIIMinFrame.Text, out minFrame))
            {
                textIIIMinFrame.Focus();
                return false;
            }

            if (!int.TryParse(textIIIMaxFrame.Text, out maxFrame) || maxFrame < minFrame)
            {
                textIIIMaxFrame.Focus();
                return false;
            }

            if (!int.TryParse(textIIIHour.Text, out hour) || hour < 0 || hour > 23)
            {
                textIIIHour.Focus();
                return false;
            }

            if (!int.TryParse(textIIIMinute.Text, out minute) || minute < 0 || minute > 59)
            {
                textIIIMinute.Focus();
                return false;
            }

            return true;
        }

        private void searchGenIII(DateTime seedTime, bool usePID, uint pid, bool useID, uint searchID, bool useSID, uint searchSID)
        {
            uint seed;

            if (radioButton1.Checked == true)
            {
                seed = uint.Parse(maskedTextBox21.Text, NumberStyles.HexNumber);
            }
            else
            {
                //empty should be 0x000005A0
                seed = Functions.CalculateSeedGen3(seedTime);
                //dry batteries always use this seed
            }
            var rng = new PokeRng(seed);
            resultsCount = 0;

            isSearching = true;
            //get to the proper starting frame and throw out the first call at that frame (generation takes 3 calls)
            for (int i = 0; i < minFrame; ++i) rng.GetNext32BitNumber();
            //prepare for the first sid call by setting the is now
            uint id = rng.GetNext16BitNumber();

            for (int frame = minFrame; frame <= maxFrame; ++frame)
            {
                //sid is always the id of the previous frame
                uint sid = id;
                id = rng.GetNext16BitNumber();

                //check criteria
                if ((!usePID || Functions.Shiny(pid, id, sid)) && (!useID || searchID == id) &&
                    (!useSID || searchSID == sid))
                {
                    resultsListIII.Add(new IDListIII {Frame = frame, ID = id, SID = sid});
                    ++resultsCount;
                }
            }

            isSearching = false;

            lblAction.Text = "Done. - Awaiting Command";
        }

        private void searchGenFRLGE(uint id,uint pid)
        {
            //FireRed,LeafGreen,Emerald all use the TID as the base seed when determining SID
            uint seed = id;
            var rng = new PokeRng(seed);
            resultsCount = 0;

            //Take frame min and max from form
            int minFrame = int.Parse(genFRLGEMinFrame.Text);
            int maxFrame = int.Parse(genFRLGEMaxFrame.Text);

            isSearching = true;
            //get to the proper starting frame and throw out the first call at that frame (generation takes 3 calls)
            for (int i = 1; i < minFrame; ++i) rng.GetNext32BitNumber();
            //prepare for the first sid call by setting the is now
            uint sid = rng.GetNext16BitNumber();

            for (int frame = minFrame; frame <= maxFrame; ++frame)
            {
                
                sid = rng.GetNext16BitNumber();

                //check criteria
                if (Functions.Shiny(pid, id, sid))
                {
                    resultsListIII.Add(new IDListIII { Frame = frame, ID = id, SID = sid });
                    ++resultsCount;
                }
            }

            isSearching = false;

            lblAction.Text = "Done. - Awaiting Command";
        }

        private void buttonIIICancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                lblAction.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void checkIIIPID_CheckedChanged(object sender, EventArgs e)
        {
            textIIIPID.Enabled = checkIIIPID.Checked;
        }

        private void checkIIITID_CheckedChanged(object sender, EventArgs e)
        {
            textIIITID.Enabled = checkIIITID.Checked;
        }

        private void checkIIISID_CheckedChanged(object sender, EventArgs e)
        {
            textIIISID.Enabled = checkIIISID.Checked;
        }

        private void checkIIIClock_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIIIClock.Checked)
            {
                dateIII.Enabled = false;
                textIIIHour.Enabled = false;
                textIIIMinute.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                maskedTextBox21.Enabled = false;
                //set the values for a dead battery
                dateIII.Value = new DateTime(2000, 1, 1);
                textIIIHour.Text = "0";
                textIIIMinute.Text = "0";
            }
            else
            {
                dateIII.Enabled = true;
                textIIIHour.Enabled = true;
                textIIIMinute.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                maskedTextBox21.Enabled = true;
            }
        }

        #region Nested type: ThreadDelegate

        private delegate void ThreadDelegate();

        #endregion

        private void tabGenSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabGenSelect.SelectedIndex == 4)
            {
                if (Profiles.List == null || Profiles.List.Count == 0)
                {
                    MessageBox.Show("No profiles were detected. Please setup a profile first.");
                    Profiles.ProfileManager.Visible = false;
                    Profiles.ProfileManager.ShowDialog();
                }
                if (Profiles.List == null || Profiles.List.Count == 0)
                {
                    Close();
                }

                profilesSource = new BindingSource { DataSource = Profiles.List };
                comboBoxProfiles.DataSource = profilesSource;
            }
        }

        private void genCancelFRLGE_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                lblAction.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void genSearchFRLGE_Click(object sender, EventArgs e)
        {

            formatGridIII();

            uint id;
            uint.TryParse(genFRLGETID.Text, out id);
            uint pid;
            uint.TryParse(genFRLGEPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out pid);            

            if (isSearching)
            {
                MessageBox.Show("The previous search is still running.");
                return;
            }

            lblAction.Text = "Searching..";
            resultsListIII = new List<IDListIII>();
            binding = new BindingSource { DataSource = resultsListIII };
            dgvResults.DataSource = binding;

            searchThread =
                new Thread(
                    () =>
                    searchGenFRLGE(id,pid));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void searchGenXDColo_Click(object sender, EventArgs e)
        {
            formatGridIII();

            uint prng;
            uint pid;
            bool testPRNG = uint.TryParse(XDColoPRNG.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out prng);
            bool testPID = uint.TryParse(XDColoPID.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out pid);

            if (isSearching)
            {
                MessageBox.Show("The previous search is still running.");
                return;
            }

            lblAction.Text = "Searching..";
            resultsListIII = new List<IDListIII>();
            binding = new BindingSource { DataSource = resultsListIII };
            dgvResults.DataSource = binding;

            searchThread =
                new Thread(
                    () =>
                    searchXDColo(prng, pid));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void searchXDColo(uint prng, uint pid)
        {
            var rng = new XdRng(prng);
            resultsCount = 0;

            isSearching = true;

            int minFrame = int.Parse(XDColoMinFrame.Text);
            int maxFrame = int.Parse(XDColoMaxFrame.Text);

            //get to the proper starting frame and throw out the first call at that frame (generation takes 3 calls)
            for (int i = 0; i < minFrame+1; ++i) rng.GetNext32BitNumber();
            //prepare for the first sid call by setting the is now
            uint sid = rng.GetNext16BitNumber();

            for (int frame = minFrame; frame <= maxFrame; ++frame)
            {
                //sid is always the id of the previous frame
                uint id = sid;
                sid = rng.GetNext16BitNumber();

                //check criteria
                if (Functions.Shiny(pid, id, sid))
                {
                    resultsListIII.Add(new IDListIII { Frame = frame, ID = id, SID = sid });
                    ++resultsCount;
                }
            }

            isSearching = false;

            lblAction.Text = "Done. - Awaiting Command";
        }

        private void genCancelXDColo_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                lblAction.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void dataGridViewCapValues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tabGenSelect.SelectedIndex == 3)
            {
                if (dgvResults.DataSource != null && resultsList != null && binding != null)
                {
                    DataGridViewColumn selectedColumn = dgvResults.Columns[e.ColumnIndex];

                    var idListComparator = new IDListComparator
                    { CompareType = selectedColumn.DataPropertyName };

                    if (selectedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                        idListComparator.sortOrder = SortOrder.Descending;

                    resultsList.Sort(idListComparator);

                    binding.ResetBindings(false);
                    selectedColumn.HeaderCell.SortGlyphDirection = idListComparator.sortOrder;
                }
            }
            else if (tabGenSelect.SelectedIndex == 4)
            {
                if (dgvResults.DataSource != null && resultsListBW != null && binding != null)
                {
                    DataGridViewColumn selectedColumn = dgvResults.Columns[e.ColumnIndex];

                    var idListBWComparator = new IDListBWComparator
                    { CompareType = selectedColumn.DataPropertyName };

                    if (selectedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                        idListBWComparator.sortOrder = SortOrder.Descending;

                    resultsListBW.Sort(idListBWComparator);

                    binding.ResetBindings(false);
                    selectedColumn.HeaderCell.SortGlyphDirection = idListBWComparator.sortOrder;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                dateIII.Enabled = false;
                textIIIHour.ReadOnly = true;
                textIIIMinute.ReadOnly = true;
                maskedTextBox21.ReadOnly = false;
            }
            else
            {
                dateIII.Enabled = true;
                textIIIHour.ReadOnly = false;
                textIIIMinute.ReadOnly = false;
                maskedTextBox21.ReadOnly = true;
            }
        }
    }
}