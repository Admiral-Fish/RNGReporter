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
            long h = 0;
            long m = 0;
            long d = 0;

            while (d < 366)
            {
                long x1 = (1440 * d) / 65536;
                long x2 = x1 + 1;

                long y1 = x1 ^ seed;
                long y2 = x2 ^ seed;

                long v1 = (x1 << 16) | y1;
                long v2 = (x2 << 16) | y2;

                while (h < 24)
                {
                    while (m < 60)
                    {
                        long v = 1440 * d + 96 * h + 60 * (h - 10 * (h / 10)) + 16 * (m / 10) + (m - 10 * (m / 10)) + 0x5a0;
                        if (v1 == v)
                        {
                            addTime = new TimeSpan((int)d, (int)h, (int)m, 0);
                            DateTime finalTime = date + addTime;
                            String result = finalTime.ToString();
                            String seconds = (((int)d * 86400) + ((int)h * 3600) + ((int)m * 60)).ToString();
                            seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds});
                        }
                        else if (v2 == v)
                        {
                            addTime = new TimeSpan((int)d, (int)h, (int)m, 0);
                            DateTime finalTime = date + addTime;
                            String result = finalTime.ToString();
                            String seconds = (((int)d * 86400)) + ((int)h * 3600) + ((int)m * 60).ToString();
                            seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds});
                        }
                        m++;
                    }
                    m = 0;
                    h++;
                }
                h = 0;
                d++;
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
