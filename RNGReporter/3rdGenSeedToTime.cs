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
                seed = reverse(seed);
            return seed;
        }

        private uint reverse(uint seed) => seed * 0xEEB9EB65 + 0x0A3561A1;

        private void seedToTime(uint seed, int year)
        {
            uint maxDay = 0;
            uint minDay = 0;

            if (year < 2000 || year > 2037)
            {
                MessageBox.Show("Please enter a year between 2000 and 2037");
                return;
            }

            DateTime start = new DateTime(year == 2000 ? 2000 : 2001, 1, 1, 0, 0, 0);

            // Game decides to ignore a year of counting days
            for (int i = 2001; i < year; i++)
            {
                minDay += DateTime.IsLeapYear(i) ? (uint)366 : 365;
                maxDay += DateTime.IsLeapYear(i) ? (uint)366 : 365;
            }

            for (int month = 1; month < 13; month++)
            {
                maxDay += (uint)DateTime.DaysInMonth(year, month);
                for (uint day = minDay; day < maxDay; day++)
                {
                    for (uint hour = 0; hour < 24; hour++)
                    {
                        for (uint minute = 0; minute < 60; minute++)
                        {
                            uint v = 1440 * day + 960 * (hour / 10) + 60 * (hour % 10) + 16 * (minute / 10) + (minute % 10) + 0x5a0;
                            v = (v >> 16) ^ (v & 0xFFFF);
                            if (v == seed)
                            {
                                addTime = new TimeSpan((int)day, (int)hour, (int)minute, 0);
                                DateTime finalTime = start + addTime;
                                String result = finalTime.ToString();
                                int seconds = (int)((day * 86400) + (hour * 3600) + (minute * 60));
                                seedTime.Add(new SeedtoTime { Time = result, Seconds = seconds });
                            }
                        }
                    }
                }
                minDay += (uint)DateTime.DaysInMonth(year, month);
            }
        }
    }
}
