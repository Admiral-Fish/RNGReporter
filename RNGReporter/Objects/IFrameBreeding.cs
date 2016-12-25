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
    public class IFrameBreeding
    {
        public uint Seed { get; set; }

        public uint Delay { get; set; }

        public DateTime SeedTime { get; set; }

        public FrameType FrameType { get; set; }

        public uint Offset { get; set; }

        public string Hp { get; set; }

        public string Atk { get; set; }

        public string Def { get; set; }

        public string Spa { get; set; }

        public string Spd { get; set; }

        public string Spe { get; set; }

        public string DisplayHp
        {
            get { return Hp; }
            set { Hp = value; }
        }

        public string DisplayAtk
        {
            get { return Atk; }
            set { Atk = value; }
        }

        public string DisplayDef
        {
            get { return Def; }
            set { Def = value; }
        }

        public string DisplaySpa
        {
            get { return Spa; }
            set { Spa = value; }
        }

        public string DisplaySpd
        {
            get { return Spd; }
            set { Spd = value; }
        }

        public string DisplaySpe
        {
            get { return Spe; }
            set { Spe = value; }
        }

        public string DisplayHpInh { get; set; }

        public string DisplayAtkInh { get; set; }

        public string DisplayDefInh { get; set; }

        public string DisplaySpaInh { get; set; }

        public string DisplaySpdInh { get; set; }

        public string DisplaySpeInh { get; set; }

        public string Flips
        {
            get { return CoinFlips.GetFlips(Seed, 10); }
        }

        public string ElmResponses
        {
            get { return Responses.ElmResponses(Seed, Offset - 1, 0); }
        }
    }
}