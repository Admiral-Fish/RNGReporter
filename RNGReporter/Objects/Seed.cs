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
    internal class Seed
    {
        //  Needs to hold all of the information about 
        //  a seed that we have created from an IV and
        //  nature combo.

        //  Need to come up with a better name for this, as it
        //  cant seem to have the same name as the containing 
        //  class :P
        public uint MonsterSeed { get; set; }

        public uint Pid { get; set; }

        //  Both of the below are based on the PID, so we're
        //  not actually going to store anything for these
        //  guys.

        public uint Ability
        {
            get { return Pid & 1; }
        }

        //  gender number
        public string Female50
        {
            get { return ((Pid & 0xFF) > 126) ? "M" : "F"; }
        }

        public string Female125
        {
            get { return ((Pid & 0xFF) > 30) ? "M" : "F"; }
        }

        public string Female25
        {
            get { return ((Pid & 0xFF) > 63) ? "M" : "F"; }
        }

        public string Female75
        {
            get { return ((Pid & 0xFF) > 190) ? "M" : "F"; }
        }

        public string Method { get; set; }

        public FrameType FrameType { get; set; }

        public uint Sid { get; set; }
    }
}