using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;

//Author: Admiral_Fish

namespace RNGReporter
{
    public partial class thirdGenSeedToTime : Form
    {
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<SeedtoTime> seedTime;
        DateTime date = new DateTime(2000, 1, 1, 0, 0, 0);
        TimeSpan addTime;

        public thirdGenSeedToTime()
        {
            InitializeComponent();
            dataGridViewValues.DataSource = binding;
            dataGridViewValues.AutoGenerateColumns = false;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint.TryParse(seedToTimeSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint seed);

            if (seed > 0xFFFF)
            {
                seed = originSeed(seed);
                seedToTimeSeed.Text = seed.ToString("X");
            }

            seedTime = new List<SeedtoTime>();
            binding = new BindingSource { DataSource = seedTime };
            dataGridViewValues.DataSource = binding;

            searchThread = new Thread(() => seedToTime(seed));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private uint originSeed(uint seed)
        {
            while (seed > 0xFFFF)
                seed = LCRNGR(seed);
            return seed;
        }

        private uint LCRNGR(uint seed)
        {
            return (seed * 0xEEB9EB65 + 0x0A3561A1) & 0xFFFFFFFF;
        }

        //Credits to Zari for writing this
        private void seedToTime(uint seed)
        {
            for (int d = 0; d < 366; d++)
            {
                int x1 = (1440 * d) / 65536;
                int x2 = x1 + 1;

                int y1 = (int)((uint)x1 ^ seed);
                int y2 = (int)((uint)x2 ^ seed);

                int v1 = (x1 << 16) | y1;
                int v2 = (x2 << 16) | y2;

                for (int h = 0; h < 24; h++)
                {
                    for (int m = 0; m < 60; m++)
                    {
                        int v = 1440 * d + 960 * (h / 10) + 60 * (h % 10) + 16 * (m / 10) + (m % 10) + 0x5a0;
                        if (v1 == v)
                        {
                            addTime = new TimeSpan(d, h, m, 0);
                            DateTime finalTime = date + addTime;
                            String result = finalTime.ToString();
                            String seconds = ((d * 86400) + (h * 3600) + (m * 60)).ToString();
                            seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds});
                        }
                        else if (v2 == v)
                        {
                            addTime = new TimeSpan(d, h, m, 0);
                            DateTime finalTime = date + addTime;
                            String result = finalTime.ToString();
                            String seconds = ((d * 86400)) + (h * 3600) + (m * 60).ToString();
                            seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds});
                        }
                    }
                }
            }
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
    }
}
