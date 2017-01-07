using RNGReporter.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class TogamiCalc : Form
    {
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<RTCTime> seedTime;
        DateTime date = new DateTime(2000, 1, 1, 0, 0, 0);
        TimeSpan addTime;
        private bool isSearching = false;

        public TogamiCalc()
        {
            InitializeComponent();
            dataGridViewValues.DataSource = binding;
            dataGridViewValues.AutoGenerateColumns = false;
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

            int min = int.Parse(minFrame.Text);
            int max = int.Parse(maxFrame.Text);

            seedTime = new List<RTCTime>();
            binding = new BindingSource { DataSource = seedTime };
            dataGridViewValues.DataSource = binding;

            searchThread =
                new Thread(
                    () =>
                    calcRTC(initial, target, min, max));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
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
                        seedTime.Add(new RTCTime { Time = result, Frame = framesAway + 1 + minFrame});
                        isSearching = false;
                        searchText.Invoke((MethodInvoker)(() => searchText.Text = "Finish. Awaiting command"));
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
            return ((seed * 0x000343FD + 0x00269EC3) & 0xFFFFFFFF);
        }

        private uint reverse(uint seed)
        {
            return ((seed * 0xB9B33155 + 0xA170F641) & 0xFFFFFFFF);
        }

        private uint nextSeed(uint seed)
        {
            seed += 40500000;
            if (seed > 0xFFFFFFFF)
                seed &= 0xFFFFFFFF;
            return seed;
        }

        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = dataGridViewValues.AutoResizeColumns;
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


        #region Nested type: ThreadDelegate

        private delegate void ThreadDelegate();

        #endregion

        private void dataGridUpdate()
        {
            binding.ResetBindings(false);
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
    }
}
