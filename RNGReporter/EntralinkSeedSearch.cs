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
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class EntralinkSeedSearch : Form
    {
        private static readonly object threadLock = new object();
        private readonly int _profile;

        // Column identifiers
        // We use these so we don't have to make string compares
        // for every cell that needs formatting, and edit column
        // indices every time we change columns
        private int CapHPIndex;
        private int CapNatureIndex;
        private int CapSpeedIndex;
        private int cpus;
        private FrameCompare frameCompare;
        private bool grey;
        private List<IFrameCapture> iframes;

        // Multithreading
        private Thread[] jobs;
        private BindingSource listBindingCap;
        private BindingSource profilesSource;
        private ulong progressFound;
        private ulong progressSearched;
        private ulong progressTotal;
        private bool refreshQueue;
        private EventWaitHandle waitHandle;

        public EntralinkSeedSearch()
        {
            InitializeComponent();
            _profile = 0;
        }

        public EntralinkSeedSearch(uint seed, uint frame, int profile)
        {
            InitializeComponent();

            textBoxSeed.Text = seed.ToString("X8");
            maskedTextBoxCGearFrame.Text = frame.ToString();
            _profile = profile;
        }

        private void PlatinumTime_Load(object sender, EventArgs e)
        {
            if (Profiles.List != null && Profiles.List.Count > 0)
            {
                profilesSource = new BindingSource {DataSource = Profiles.List};
                comboBoxProfiles.DataSource = profilesSource;
                comboBoxProfiles.SelectedIndex = _profile;
            }

            comboBoxNature.Items.AddRange(Objects.Nature.NatureDropDownCollectionSearchNatures());

            Settings.Default.PropertyChanged += ChangeLanguage;
            SetLanguage();

            // Obtain the indices of the datagrid columns by name,
            // so we don't have to keep track of them

            CapHPIndex = CapHP.Index;
            CapSpeedIndex = CapSpe.Index;
            CapNatureIndex = Nature.Index;

            dataGridViewCapValues.AutoGenerateColumns = false;
            CapSeed.DefaultCellStyle.Format = "X16";
            Timer0.DefaultCellStyle.Format = "X";
            SeedTime.DefaultCellStyle.Format = "MM/dd/yy HH:mm:ss";
            CSeedTime.DefaultCellStyle.Format = "MM/dd/yy HH:mm:ss";

            maskedTextBoxYear.Text = DateTime.Now.Year.ToString();

            cpus = Settings.Default.CPUs;
            if (cpus < 1) cpus = 1;
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
            comboBoxNature.Font = CellStyle.Font;

            for (int checkBoxIndex = 1; checkBoxIndex < comboBoxNature.Items.Count; checkBoxIndex++)
            {
                comboBoxNature.CheckBoxItems[checkBoxIndex].Text =
                    comboBoxNature.CheckBoxItems[checkBoxIndex].ComboBoxItem.ToString();
                comboBoxNature.CheckBoxItems[checkBoxIndex].Font = CellStyle.Font;
            }

            // This is a rather hackish way of making the custom control
            // display the desired text upon loading
            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;

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

        // Can't proceed with a 5th gen search without knowing all DS parameters

        private bool ParametersInputCheck()
        {
            if (textBoxSeed.Text == "")
            {
                textBoxSeed.Focus();
                textBoxSeed.SelectAll();
                return false;
            }

            if (maskedTextBoxCGearFrame.Text == "")
            {
                maskedTextBoxCGearFrame.Focus();
                maskedTextBoxCGearFrame.SelectAll();
                return false;
            }

            if (maskedTextBoxGroupSize.Text == "")
            {
                maskedTextBoxGroupSize.Focus();
                maskedTextBoxGroupSize.SelectAll();
                return false;
            }

            if (maskedTextBoxDelayCalibration.Text == "")
            {
                maskedTextBoxDelayCalibration.Focus();
                maskedTextBoxDelayCalibration.SelectAll();
                return false;
            }

            return true;
        }

        private List<List<ButtonComboType>> GetKeypresses()
        {
            var keypresses = new List<List<ButtonComboType>>();
            var profile = (Profile) comboBoxProfiles.SelectedItem;
            byte b = 0x1;
            //have to start at 1 because a phantom element is added, not sure why
            for (int i = 0; i < 4; ++i)
            {
                if ((profile.Keypresses & b) != 0)
                    keypresses.AddRange(Functions.KeypressCombos(i, profile.SkipLR));
                b <<= 1;
            }
            return keypresses;
        }

        private void buttonSeedGenerate_Click(object sender, EventArgs e)
        {
            if (!ParametersInputCheck())
                return;

            #region Initialize

            if (comboBoxNature.Text == "Any")
            {
                MessageBox.Show("Please select a specific list of natures.");
                return;
            }
            List<uint> natures =
                (from t in comboBoxNature.CheckBoxItems where t.Checked select (uint) ((Nature) t.ComboBoxItem).Number).
                    ToList();

            var profile = (Profile) comboBoxProfiles.SelectedItem;
            uint mac_partial = (uint) profile.MAC_Address & 0xFFFFFF;

            uint minFrame = uint.Parse(maskedTextBoxCapMinOffset.Text);
            uint maxFrame = uint.Parse(maskedTextBoxCapMaxOffset.Text);

            uint groupSize = uint.Parse(maskedTextBoxGroupSize.Text);

            uint seedCGear = uint.Parse(textBoxSeed.Text, NumberStyles.HexNumber);

            int generateYear = int.Parse(maskedTextBoxYear.Text);

            if (generateYear < 2000 || generateYear > 2099)
            {
                MessageBox.Show("Year must be a value between 2000 and 2099, inclusive.");
                return;
            }

            uint frameCGear = uint.Parse(maskedTextBoxCGearFrame.Text);
            //generate the CGear Seed Times
            uint ab = seedCGear - mac_partial >> 24;
            uint cd = (seedCGear - mac_partial & 0x00FF0000) >> 16;
            uint efgh = seedCGear - mac_partial & 0x0000FFFF;

            //  Get Delay
            uint delay = efgh + (uint) (2000 - generateYear);

            //  Get Calibration
            uint calibration = uint.Parse(maskedTextBoxDelayCalibration.Text);

            //  Store the Calibrated Delay and offset
            uint calibratedDelay = delay + calibration;

            long offset = -calibratedDelay/60;

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

            maskedTextBoxDelay.Text = delay.ToString(CultureInfo.InvariantCulture);

            List<List<ButtonComboType>> keypresses = GetKeypresses();

            iframes = new List<IFrameCapture>();
            var generator = new FrameGenerator
                {
                    InitialSeed = seedCGear,
                    FrameType = FrameType.Method5CGear,
                    InitialFrame = frameCGear,
                    MaxResults = 1
                };

            GenderFilter genderFilter = checkBoxGenderless.Checked
                                            ? new GenderFilter("Genderless", 0xFF, GenderCriteria.DontCareGenderless)
                                            : new GenderFilter("Gendered", 0, GenderCriteria.DontCareGenderless);

            var possibleDates = new List<DateTime>();
            //  Loop through all months
            for (int month = 1; month <= 12; month++)
            {
                int daysInMonth = DateTime.DaysInMonth(generateYear, month);

                //  Loop through all days
                for (int day = 1; day <= daysInMonth; day++)
                {
                    //  Loop through all minutes
                    for (int minute = 0; minute <= 59; minute++)
                    {
                        //  Loop through all seconds
                        for (int second = 0; second <= 59; second++)
                        {
                            if (ab != ((month*day + minute + second)%0x100)) continue;
                            var dateTime = new DateTime(generateYear, month, day, hour, minute, second);

                            // Standard seed time will be the C-Gear seed time, minus the delay
                            // We'll skip seeds that cross over into the next day and cause unwanted advances
                            // Added calibration to the delay to account for the fact that people aren't perfectly fast
                            DateTime possibleDate = dateTime.AddSeconds(offset);
                            if (dateTime.Day == possibleDate.Day)
                                possibleDates.Add(possibleDate);
                        }
                    }
                }
            }

            // Generate the IVs for the corresponding C-Gear seed first

            var rngIVs = new uint[6];

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

            List<Frame> IVs = generator.Generate(frameCompare, 0, 0);
            if (IVs.Count > 0)
            {
                rngIVs[0] = IVs[0].Hp;
                rngIVs[1] = IVs[0].Atk;
                rngIVs[2] = IVs[0].Def;
                rngIVs[3] = IVs[0].Spa;
                rngIVs[4] = IVs[0].Spd;
                rngIVs[5] = IVs[0].Spe;
            }

            // Now that each combo box item is a custom object containing the FrameType reference
            // We can simply retrieve the FrameType from the selected item
            generator.FrameType = FrameType.Method5Natures;
            generator.EncounterType = EncounterType.Entralink;
            generator.RNGIVs = rngIVs;

            generator.InitialFrame = minFrame;
            generator.MaxResults = maxFrame - minFrame + 1;

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

            frameCompare = new FrameCompare(
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                0, CompareType.None,
                natures,
                -1,
                false,
                false,
                false,
                null,
                genderFilter);

            #endregion

            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            jobs = new Thread[cpus];
            //divide the possible times into even groups
            int split = possibleDates.Count/cpus;
            for (int i = 0; i < cpus; ++i)
            {
                List<DateTime> dates = i < cpus - 1
                                           ? possibleDates.GetRange(i*split, split)
                                           : possibleDates.GetRange(i*split, split + possibleDates.Count%cpus);
                //if the last i make sure we add the remainder as well
                // technically supposed to copy profile and send in a copy because now the threads are
                // using a reference to the same profile but that's fine because the profile isn't getting
                // mutated anyway
                jobs[i] =
                    new Thread(
                        () =>
                        Generate(generator.Clone(), dates, minFrame, maxFrame,
                                 profile, groupSize, calibratedDelay));
                jobs[i].Start();
            }

            listBindingCap = new BindingSource {DataSource = iframes};
            dataGridViewCapValues.DataSource = listBindingCap;


            progressTotal =
                (ulong)
                (maxFrame - minFrame + 1)*(profile.Timer0Max - profile.Timer0Min + 1)*(ulong) keypresses.Count*
                (ulong) possibleDates.Count;
            var progressJob =
                new Thread(() => ManageProgress(listBindingCap, dataGridViewCapValues, generator.FrameType, 0));
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;

            buttonSeedGenerate.Enabled = false;
        }

        private void UpdateGrid(BindingSource bindingSource)
        {
            bindingSource.ResetBindings(false);
        }

        private void ResortGrid(BindingSource bindingSource, DoubleBufferedDataGridView dataGrid, FrameType frameType)
        {
            /*switch (frameType)
            {
                case FrameType.MethodJ:
                case FrameType.MethodK:
                    IFrameCaptureComparer iframeCaptureComparer = new IFrameCaptureComparer { CompareType = "Seed" };
                    ((List<IFrameCapture>)bindingSource.DataSource).Sort(iframeCaptureComparer);
                    dataGrid.Columns["CapSeed.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    break;
            }*/

            dataGrid.DataSource = bindingSource;
            bindingSource.ResetBindings(false);
        }

        private void EnableSeedGenerate()
        {
            buttonSeedGenerate.Enabled = true;
        }

        private void Generate(FrameGenerator generator, IEnumerable<DateTime> possibleDates, uint minFrame,
                              uint maxFrame, Profile profile, uint groupSize, uint calibratedDelay)
        {
            const uint incrementFound = 1;
            List<List<ButtonComboType>> keypresses = profile.GetKeypresses();

            foreach (DateTime seedTime in possibleDates)
            {
                waitHandle.WaitOne();
                foreach (var combo in keypresses)
                {
                    for (uint timer0 = profile.Timer0Min; timer0 <= profile.Timer0Max; timer0++)
                    {
                        ulong seed = Functions.EncryptSeed(seedTime, profile, timer0, Functions.buttonMashed(combo));

                        generator.InitialSeed = seed;
                        generator.InitialFrame = Functions.initialPIDRNG(seed, profile) + minFrame;
                        generator.MaxResults = maxFrame - minFrame + 1;

                        List<Frame> frames = generator.Generate(frameCompare, 0, 0);

                        IFrameCapture previous = null;
                        bool previouslyAdded = false;

                        var frameGroup = new List<IFrameCapture>();
                        // quick check and if it's impossible that this is a match don't waste our time loop
                        // breaks search progress so that needs to get fixed
                        if (frames.Count < groupSize) continue;
                        foreach (Frame frame in frames)
                        {
                            var iframe = new IFrameCapture
                                {
                                    Offset = frame.Number,
                                    Seed = seed,
                                    Frame = frame,
                                    TimeDate = seedTime,
                                    Timer0 = timer0,
                                    Delay = calibratedDelay,
                                    KeyPresses = combo
                                };

                            //  Calibrated delay instead of the real delay for correct CGear Times

                            if (previous != null &&
                                (iframe.Offset == previous.Offset + 1 || iframe.Offset == previous.Offset + 2))
                            {
                                if (!previouslyAdded)
                                    frameGroup.Add(previous);

                                frameGroup.Add(iframe);
                                previouslyAdded = true;
                            }
                            else
                            {
                                if (frameGroup.Count >= groupSize)
                                {
                                    lock (threadLock)
                                    {
                                        foreach (IFrameCapture t in frameGroup)
                                            t.Frame.Synchable = grey;
                                        iframes.AddRange(frameGroup);
                                        grey = !grey;
                                    }
                                    refreshQueue = true;
                                    progressFound += incrementFound;
                                }

                                frameGroup = new List<IFrameCapture>();
                                previouslyAdded = false;
                            }
                            previous = iframe;
                        }
                        progressSearched += (uint) frames.Count;

                        if (frameGroup.Count >= groupSize)
                        {
                            lock (threadLock)
                            {
                                for (int i = 0; i < frameGroup.Count; ++i)
                                    frameGroup[i].Frame.Synchable = grey;
                                iframes.AddRange(frameGroup);
                                grey = !grey;
                            }
                            refreshQueue = true;
                            progressFound += incrementFound;
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
            ThreadDelegate enableGenerateButton = EnableSeedGenerate;

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

                if ((bool) dataGridViewCapValues.Rows[e.RowIndex].Cells["Grey"].Value)
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
                    if (!dataGridViewCapValues.Rows[Hti.RowIndex].Selected)
                    {
                        dataGridViewCapValues.ClearSelection();

                        dataGridViewCapValues.Rows[Hti.RowIndex].Selected = true;
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
                    //  replace tab with space
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

        private void buttonAnyNature_Click(object sender, EventArgs e)
        {
            comboBoxNature.ClearSelection();
        }

        private void FocusControl(object sender, MouseEventArgs e)
        {
            ((Control) sender).Focus();
        }

        private void comboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelProfileInformation.Text = ((Profile) comboBoxProfiles.SelectedItem).ProfileInformation();
        }

        private void buttonEditProfile_Click(object sender, EventArgs e)
        {
            var editor = new ProfileEditor {Profile = (Profile) comboBoxProfiles.SelectedItem};
            if (editor.ShowDialog() != DialogResult.OK) return;
            Profiles.List[comboBoxProfiles.SelectedIndex] = editor.Profile;

            profilesSource.DataSource = Profiles.List;
            profilesSource.ResetBindings(false);
            labelProfileInformation.Text = ((Profile) comboBoxProfiles.SelectedItem).ProfileInformation();
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
    }
}