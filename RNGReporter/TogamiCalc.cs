using RNGReporter.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class TogamiCalc : Form
    {
        private Thread searchThread;
        private List<RTCTime> seedTime;
        DateTime date = new DateTime(2000, 1, 1, 0, 0, 0);
        TimeSpan addTime;
        private bool isSearching;

        public TogamiCalc()
        {
            InitializeComponent();
            dataGridViewValues.AutoGenerateColumns = false;
        }

        private void TogamiCalc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (searchThread != null)
                searchThread.Abort();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                searchText.Text = "Previous search running";
                return;
            }

            uint initial;
            bool testPRNG = uint.TryParse(initialSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out initial);
            if (!testPRNG)
            {
                MessageBox.Show("Please enter your seed in proper hex format.");
                return;
            }

            uint target;
            testPRNG = uint.TryParse(targetSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out target);
            if (!testPRNG)
            {
                MessageBox.Show("Please enter your seed in proper hex format.");
                return;
            }

            if (initial == target)
            {
                MessageBox.Show("Initial and target seed are the same. Please change one of them.");
                return;
            }

            int min = int.Parse(minFrame.Text);
            int max = int.Parse(maxFrame.Text);

            seedTime = new List<RTCTime>();

            searchThread = new Thread(() => calcRTC(initial, target, min, max));
            searchThread.Start();
        }

        private void calcRTC(uint initialSeed, uint targetSeed, int minFrame, int maxFrame)
        {
            isSearching = true;
            uint adjustedTarget = targetSeed;

            for (int x = 0; x < minFrame; x++)
            {
                adjustedTarget = reverse(adjustedTarget);
            }

            int secondsToAdd = 0;
            int secoundCount = 0;
            bool targetHit = false;
            int minutesPassed = 0;

            while (!targetHit)
            {
                searchText.Invoke((MethodInvoker)(() => searchText.Text = "Minutes added to RTC: " + minutesPassed.ToString()));
                uint prng = initialSeed;
                int framesAway = 0;

                for (int x = 0; x < maxFrame; x++)
                {
                    prng = forward(prng);
                    framesAway += 1;

                    if (prng == adjustedTarget)
                    {
                        addTime = new TimeSpan(0, 0, 0, secondsToAdd);
                        DateTime finalTime = date + addTime;
                        String result = finalTime.ToString();
                        seedTime.Add(new RTCTime { Time = result, Frame = framesAway + 1 + minFrame, Seed = initialSeed.ToString("X")});
                        isSearching = false;
                        searchText.Invoke((MethodInvoker)(() => searchText.Text = "Finish. Awaiting command"));
                        dataGridViewValues.Invoke((MethodInvoker)(() => dataGridViewValues.DataSource = seedTime));
                        dataGridViewValues.Invoke((MethodInvoker)(() => dataGridViewValues.AutoResizeColumns()));
                        return;
                    }
                }

                initialSeed = nextSeed(initialSeed);
                secondsToAdd += 1;
                secoundCount += 1;

                if (secoundCount == 60)
                {
                    minutesPassed += 1;
                    secoundCount = 0;
                }
            }
        }

        private uint forward(uint seed)
        {
            return seed * 0x000343FD + 0x00269EC3;
        }

        private uint reverse(uint seed)
        {
            return seed * 0xB9B33155 + 0xA170F641;
        }

        private uint nextSeed(uint seed)
        {
            return seed + 40500000;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                searchThread.Abort();
                isSearching = false;
                searchText.Text = "Search cancelled";
            }
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = dataGridViewValues.HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewValues.Rows[hti.RowIndex])).Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void contextMenuStripGrid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void copySeedToClipboard_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                var frame = (RTCTime)dataGridViewValues.SelectedRows[0].DataBoundItem;
                Clipboard.SetText(frame.Seed);
            }
        }
    }
}
