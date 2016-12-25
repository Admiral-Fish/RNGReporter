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
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class PlatinumTimeAdjacent : Form
    {
        private readonly uint ab;
        private readonly int hour;
        private readonly uint offset;
        private List<DateTime> adjacentTimeList;
        private uint delay;
        private uint returnMaxDelay;
        private uint returnMaxOffset;
        private uint returnMinDelay;
        private uint returnMinOffset;

        private uint seed;
        private int year;

        public PlatinumTimeAdjacent(
            uint seed,
            uint offset,
            int year)
        {
            InitializeComponent();

            this.seed = seed;
            this.offset = offset;
            this.year = year;
            delay = (uint) ((seed & 0xFFFF) - (year - 2000));
            hour = (int) (seed & 0xFF0000) >> 16;
            ab = seed >> 24;
        }

        public uint Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        public List<DateTime> AdjacentTimeList
        {
            get { return adjacentTimeList; }
            set { adjacentTimeList = value; }
        }

        public uint ReturnMinSeconds { get; set; }

        public uint ReturnMaxSeconds { get; set; }

        public uint ReturnMinDelay
        {
            get { return returnMinDelay; }
            set { returnMinDelay = value; }
        }

        public uint ReturnMaxDelay
        {
            get { return returnMaxDelay; }
            set { returnMaxDelay = value; }
        }

        public uint ReturnMinOffset
        {
            get { return returnMinOffset; }
            set { returnMinOffset = value; }
        }

        public uint ReturnMaxOffset
        {
            get { return returnMaxOffset; }
            set { returnMaxOffset = value; }
        }

        private void PlatinumTimeAdjacent_Load(object sender, EventArgs e)
        {
            dataGridViewValues.AutoGenerateColumns = false;
            dateTimePicker1.CustomFormat = "dd MMM yyyy";

            //  Use what we were passed in to create some decent 
            //  default values for our dialog.
            listValidTimes(0, 0);

            //  Offset +2 or +/- 1 depending on whether it starts
            //  at three or a higher number.
            uint minOffset;
            uint maxOffset;

            if (offset == 1)
            {
                minOffset = 1;
                maxOffset = 3;
            }
            else
            {
                minOffset = offset - 1;
                maxOffset = offset + 1;
            }

            maskedTextBoxMinOffset.Text = minOffset.ToString();
            maskedTextBoxMaxOffset.Text = maxOffset.ToString();
        }

        private void listValidTimes(int monthFilter, int dayFilter)
        {
            var timeAndDelays = new List<TimeAndDelay>();

            int minMonth;
            int maxMonth;
            int minDay;
            int maxDay;

            if (monthFilter != 0)
                minMonth = maxMonth = monthFilter;
            else
            {
                minMonth = 1;
                maxMonth = 12;
            }

            minDay = maxDay = dayFilter;
            if (dayFilter == 0)
                minDay = 1;

            //  Loop through all months
            for (int month = minMonth; month <= maxMonth; month++)
            {
                if (dayFilter == 0)
                    maxDay = DateTime.DaysInMonth(year, month);

                //  Loop through all days
                for (int day = minDay; day <= maxDay; day++)
                {
                    //  Loop through all minutes
                    for (int minute = 0; minute <= 59; minute++)
                    {
                        //  Loop through all seconds
                        for (int second = 0; second <= 59; second++)
                        {
                            if (ab == ((month*day + minute + second)%0x100))
                            {
                                //  Create Date/Time and add item to collection
                                var timeAndDelay = new TimeAndDelay();

                                //  Build DateTime
                                var dateTime = new DateTime(year, month, day, hour, minute, second);

                                timeAndDelay.Date = dateTime;
                                timeAndDelay.Delay = (int) delay;

                                //  Add to collection
                                timeAndDelays.Add(timeAndDelay);
                            }
                        }
                    }
                }
            }

            //  Do our databind to the grid here so the user 
            //  can get the time listing.
            dataGridViewValues.DataSource = timeAndDelays;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //  Take all of our information and put it into the
            //  return variables.  We will use some initial values
            //  if they are not filled out.

            returnMinOffset = maskedTextBoxMinOffset.Text != "" ? uint.Parse(maskedTextBoxMinOffset.Text) : offset;

            returnMaxOffset = maskedTextBoxMaxOffset.Text != "" ? uint.Parse(maskedTextBoxMaxOffset.Text) : offset;

            if (delay > (uint) numericUpDownDelay.Value)
                returnMinDelay = delay - (uint) numericUpDownDelay.Value;
            else
                returnMinDelay = 0;

            returnMaxDelay = delay + (uint) numericUpDownDelay.Value;

            DateTime selected = ((TimeAndDelay) dataGridViewValues.SelectedRows[0].DataBoundItem).Date;
            adjacentTimeList = new List<DateTime>();

            for (int seconds = -1*(int) numericUpDownSeconds.Value;
                 seconds <= (int) numericUpDownSeconds.Value;
                 seconds++)
            {
                adjacentTimeList.Add(selected.AddSeconds(seconds));
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            year = dateTimePicker1.Value.Date.Year;
            delay = (uint) ((seed & 0xFFFF) - (year - 2000));

            listValidTimes(dateTimePicker1.Value.Date.Month, dateTimePicker1.Value.Date.Day);
        }
    }
}