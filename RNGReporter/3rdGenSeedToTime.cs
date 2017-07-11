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
        private List<SeedtoTime> seedTime;
        DateTime start = new DateTime(2000, 1, 1, 0, 0, 0);
        TimeSpan addTime;

        public thirdGenSeedToTime()
        {
            InitializeComponent();
            dataGridViewValues.AutoGenerateColumns = false;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint.TryParse(seedToTimeSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint seed);
            int.TryParse(maskedTextBoxYear.Text, out int year);

            if (seed > 0xFFFF)
            {
                seed = originSeed(seed);
                seedToTimeSeed.Text = seed.ToString("X");
            }

            seedTime = new List<SeedtoTime>();
            seedToTime(seed, year);

            dataGridViewValues.DataSource = seedTime;
            dataGridViewValues.AutoResizeColumns();
        }

        private uint originSeed(uint seed)
        {
            while (seed > 0xFFFF)
                seed = LCRNGR(seed);
            return seed;
        }

        private uint LCRNGR(uint seed)
        {
            return (seed * 0xEEB9EB65 + 0x0A3561A1);
        }

        //Credits to Zari for writing this
        private void seedToTime(uint seed, int year)
        {
            int maxDay = 0;
            int minDay = 0;

            if (year < 2000 || year > 2037)
            {
                MessageBox.Show("Please enter a year between 2000 and 2037");
                return;
            }

            if (year != 2000)
            {
                for (int x = 2000; x < year; x++)
                    for (int months = 1; months < 13; months++)
                    {
                        minDay += DateTime.DaysInMonth(x, months);
                        maxDay += DateTime.DaysInMonth(x, months);
                    }
            }

            for (int month = 1; month < 13; month++)
            {
                maxDay += DateTime.DaysInMonth(year, month);
                for (int day = minDay; day < maxDay; day++)
                {
                    int x1 = (1440 * day) >> 16;
                    int x2 = x1 + 1;

                    int y1 = (int)((uint)x1 ^ seed);
                    int y2 = (int)((uint)x2 ^ seed);

                    int v1 = (x1 << 16) | y1;
                    int v2 = (x2 << 16) | y2;

                    for (int hour = 0; hour < 24; hour++)
                    {
                        for (int minute = 0; minute < 60; minute++)
                        {
                            int v = 1440 * day + 960 * (hour / 10) + 60 * (hour % 10) + 16 * (minute / 10) + (minute % 10) + 0x5a0;
                            if (v1 == v || v2 == v)
                            {
                                addTime = new TimeSpan(day, hour, minute, 0);
                                DateTime finalTime = start + addTime;
                                String result = finalTime.ToString();
                                int seconds = ((day * 86400) + (hour * 3600) + (minute * 60));
                                seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds });
                            }
                        }
                    }
                }
                minDay += DateTime.DaysInMonth(year, month);
            }
        }
    }
}
