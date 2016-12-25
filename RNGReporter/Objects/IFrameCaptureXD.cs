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
    public class IFrameCaptureXD
    {
        public ulong Seed { get; set; }

        public uint Ticks
        {
            get { return (uint) Seed; }
        }

        public double Time
        {
            get { return (double) Seed/6000000; }
        }

        public Frame Frame { get; set; }

        public uint Pid
        {
            get { return Frame.Pid; }
        }

        public string Nature
        {
            get { return Functions.NatureStrings((int) Frame.Nature); }
        }

        public uint Ability
        {
            get { return Frame.Ability; }
        }

        public string Female125
        {
            get { return Frame.Female125; }
        }

        public string Female25
        {
            get { return Frame.Female25; }
        }

        public string Female50
        {
            get { return Frame.Female50; }
        }

        public string Female75
        {
            get { return Frame.Female75; }
        }

        public uint HiddenPowerPower
        {
            get { return Frame.HiddenPowerPower; }
        }

        public string HiddenPowerType
        {
            get { return Frame.HiddenPowerType; }
        }

        public string ShinyDisplay
        {
            get { return Frame.ShinyDisplay; }
        }

        public string DisplayHp
        {
            get { return Frame.DisplayHp; }
        }

        public string DisplayAtk
        {
            get { return Frame.DisplayAtk; }
        }

        public string DisplayDef
        {
            get { return Frame.DisplayDef; }
        }

        public string DisplaySpa
        {
            get { return Frame.DisplaySpa; }
        }

        public string DisplaySpd
        {
            get { return Frame.DisplaySpd; }
        }

        public string DisplaySpe
        {
            get { return Frame.DisplaySpe; }
        }
    }
}