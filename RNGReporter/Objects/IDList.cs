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
    public class IDListBW
    {
        public ulong Seed { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int InitialFrame { get; set; }

        public int Frame { get; set; }

        public uint ID { get; set; }

        public uint SID { get; set; }

        public string Starter { get; set; }

        public string Button { get; set; }
    }

    public class IDList
    {
        public IDList(uint seed, uint delay, uint tid, uint sid, uint seconds)
        {
            Seed = seed;
            Delay = delay;
            ID = tid;
            SID = sid;
            Seconds = seconds;
        }

        public uint Seed { get; private set; }

        public uint Delay { get; private set; }

        public uint ID { get; private set; }

        public uint SID { get; private set; }

        public uint Seconds { get; private set; }
    }

    internal class IDListIII
    {
        public uint ID { get; set; }

        public uint SID { get; set; }

        public int Frame { get; set; }
    }

    internal class SeedtoTime
    {
        public string Time { get; set; }

        public string Seconds { get; set; }
    }

    internal class RTCTime
    {
        public string Time { get; set; }

        public int Frame { get; set; }

        public String Seed { get; set; }
    }

    internal class ProbableGeneration
    {
        public String Probable { get; set; }
    }

    internal class PIDIVS
    {
        public String Seed { get; set; }
        public String Method { get; set; }
        public String IVs { get; set; }
    }

    public class IDListComparator : IComparer<IDList>
    {
        public string CompareType = "Seed";
        public SortOrder sortOrder = SortOrder.Ascending;

        public int Compare(IDList x, IDList y)
        {
            int result;
            int direction = 1;

            if (sortOrder == SortOrder.Descending)
                direction = -1;

            switch (CompareType)
            {
                case "Seed":
                    return direction * x.Seed.CompareTo(y.Seed);
                case "Delay":
                    return direction * x.Delay.CompareTo(y.Delay);
                case "ID":
                    return direction * x.ID.CompareTo(y.ID);
                case "SID":
                    return direction * x.SID.CompareTo(y.SID);
                case "Seconds":
                    return direction * x.Seconds.CompareTo(y.Seconds);
                default:
                    //use ordinal due to better efficiency and because it uses the current culture
                    result = direction *
                             String.CompareOrdinal(x.GetType().GetProperty(CompareType).GetValue(x, null).ToString(),
                                                   y.GetType().GetProperty(CompareType).GetValue(y, null).ToString());

                    return result;
            }
        }
    }

    public class IDListBWComparator : IComparer<IDListBW>
    {
        public string CompareType = "Seed";
        public SortOrder sortOrder = SortOrder.Ascending;

        public int Compare(IDListBW x, IDListBW y)
        {
            int result;
            int direction = 1;

            if (sortOrder == SortOrder.Descending)
                direction = -1;

            switch (CompareType)
            {
                case "Seed":
                    return direction * x.Seed.CompareTo(y.Seed);
                case "Initial Frame":
                    return direction * x.InitialFrame.CompareTo(y.InitialFrame);
                case "Frame":
                    return direction * x.Frame.CompareTo(y.Frame);
                case "ID":
                    return direction * x.ID.CompareTo(y.ID);
                case "SID":
                    return direction * x.SID.CompareTo(y.SID);
                default:
                    //use ordinal due to better efficiency and because it uses the current culture
                    result = direction *
                             String.CompareOrdinal(x.GetType().GetProperty(CompareType).GetValue(x, null).ToString(),
                                                   y.GetType().GetProperty(CompareType).GetValue(y, null).ToString());

                    return result;
            }
        }
    }
}