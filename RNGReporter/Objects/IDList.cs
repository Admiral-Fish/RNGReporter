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

namespace RNGReporter.Objects
{
    internal class IDListBW
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

    internal class IDList
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
        public ushort ID { get; set; }

        public ushort SID { get; set; }

        public int Frame { get; set; }
    }

    internal class SeedtoTime
    {
        public string Time { get; set; }
    }

    internal class AdjacentSeeds
    {
        public string Seed { get; set; }

        public int Frame { get; set; }
    }
}