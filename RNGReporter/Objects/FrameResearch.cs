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

namespace RNGReporter.Objects
{
    public class FrameResearch
    {
        public FrameResearch()
        {
            Custom7 = 0;
            Custom6 = 0;
            Custom5 = 0;
            Custom4 = 0;
            Custom3 = 0;
            Custom2 = 0;
            Custom1 = 0;
        }

        public uint FrameNumber { get; set; }

        public bool RNG64bit { get; set; }

        public uint Full32 { get; set; }

        public ulong Full64 { get; set; }

        public uint High32
        {
            get { return (uint) (Full64 >> 32); }
        }

        public uint Low32
        {
            get { return (uint) (Full64 & 0xFFFFFFFF); }
        }

        public uint High16
        {
            get { return RNG64bit ? High32 >> 16 : Full32 >> 16; }
        }

        public uint Low16
        {
            get { return RNG64bit ? High32 & 0xFFFF : Full32 & 0xFFFF; }
        }

        public ulong Custom1 { get; set; }

        public ulong Custom2 { get; set; }

        public ulong Custom3 { get; set; }

        public ulong Custom4 { get; set; }

        public ulong Custom5 { get; set; }

        public ulong Custom6 { get; set; }

        public ulong Custom7 { get; set; }

        public uint Mod25
        {
            get { return RNG64bit ? High32%25 : High16%25; }
        }

        public uint Mod100
        {
            get { return RNG64bit ? High32%100 : High16%100; }
        }

        public uint Mod3
        {
            get { return RNG64bit ? High32%3 : High16%3; }
        }

        public uint Div656
        {
            get { return High16/656; }
        }

        public uint HighBit
        {
            get { return RNG64bit ? High32 >> 31 : High16 >> 15; }
        }

        public uint LowBit
        {
            get { return RNG64bit ? High32 & 1 : High16 & 1; }
        }
    }
}