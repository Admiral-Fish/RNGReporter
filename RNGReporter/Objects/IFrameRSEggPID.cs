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

namespace RNGReporter.Objects
{
    public class IFrameRSEggPID
    {
        private uint frameLowerPID;
        private uint frameUpperPID;
        private uint pid;
        private bool shiny;

        public uint FrameLowerPID
        {
            get { return frameLowerPID; }
            set { frameLowerPID = value; }
        }

        public string TimeLowerPID
        {
            get
            {
                uint minutes = frameLowerPID/3600;
                uint seconds = (frameLowerPID - (3600*minutes))/60;
                uint milli = ((frameLowerPID%60)*100)/60;

                return minutes.ToString() + ":" + seconds.ToString("D2") + "." + milli.ToString("D2");
            }
        }

        public uint FrameUpperPID
        {
            get { return frameUpperPID; }
            set { frameUpperPID = value; }
        }

        public string TimeUpperPID
        {
            get
            {
                uint minutes = frameUpperPID/3600;
                uint seconds = (frameUpperPID - (3600*minutes))/60;
                uint milli = ((frameUpperPID%60)*100)/60;

                return minutes.ToString() + ":" + seconds.ToString("D2") + "." + milli.ToString("D2");
            }
        }

        public uint Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public bool Shiny
        {
            get { return shiny; }
            set { shiny = value; }
        }

        public string ShinyDisplay
        {
            get { return shiny ? "!!!" : ""; }
        }

        public string Nature
        {
            get { return Functions.NatureStrings((int) Functions.Nature(pid)); }
        }

        public string Ability
        {
            get { return (pid & 1).ToString(); }
        }

        public string SeedTime { get; set; }

        public string DisplayHp { get; set; }

        public string DisplayAtk { get; set; }

        public string DisplayDef { get; set; }

        public string DisplaySpa { get; set; }

        public string DisplaySpd { get; set; }

        public string DisplaySpe { get; set; }

        public string DisplayHpInh { get; set; }

        public string DisplayAtkInh { get; set; }

        public string DisplayDefInh { get; set; }

        public string DisplaySpaInh { get; set; }

        public string DisplaySpdInh { get; set; }

        public string DisplaySpeInh { get; set; }

        public string Female50
        {
            get { return ((pid & 0xFF) >= 127) ? "M" : "F"; }
        }

        public string Female125
        {
            get { return ((pid & 0xFF) >= 31) ? "M" : "F"; }
        }

        public string Female25
        {
            get { return ((pid & 0xFF) >= 63) ? "M" : "F"; }
        }

        public string Female75
        {
            get { return ((pid & 0xFF) >= 191) ? "M" : "F"; }
        }
    }

    public class IFrameEEggPID : IFrameRSEggPID
    {
        public uint Advances { get; set; }

        public uint Redraws { get; set; }

        public string FrameNumber { get; set; }
    }

    public class IFrameEEggPIDComparer : IComparer<IFrameEEggPID>
    {
        public string CompareType = "Frame";
        public SortOrder sortOrder = SortOrder.Ascending;

        #region IComparer<IFrameEEggPID> Members

        public int Compare(IFrameEEggPID x, IFrameEEggPID y)
        {
            int result;
            int direction = 1;

            if (sortOrder == SortOrder.Descending)
                direction = -1;

            switch (CompareType)
            {
                case "Frame":
                    result = direction*x.FrameLowerPID.CompareTo(y.FrameLowerPID);
                    if (result == 0)
                    {
                        result = direction*x.Redraws.CompareTo(y.Redraws);
                    }
                    return result;
                default:
                    //use ordinal due to better efficiency and because it uses the current culture
                    result = direction*
                             String.CompareOrdinal(x.GetType().GetProperty(CompareType).GetValue(x, null).ToString(),
                                                   y.GetType().GetProperty(CompareType).GetValue(y, null).ToString());

                    return result;
            }
        }

        #endregion
    }
}