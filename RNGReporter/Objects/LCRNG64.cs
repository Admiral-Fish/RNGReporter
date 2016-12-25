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
    internal class GenericRng64 : IRNG64
    {
        //  This is the generic base that all of the other 64-bit lcrngs will
        //  use. They will pass in a multiplier, and adder, and a seed.
        private readonly ulong add;
        private readonly ulong mult;
        private ulong seed;

        public GenericRng64(ulong seed, ulong mult, ulong add)
        {
            this.seed = seed;

            this.mult = mult;
            this.add = add;
        }

        public ulong Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        #region IRNG64 Members

        public ulong Next()
        {
            return GetNext64BitNumber();
        }

        #endregion

        public uint GetNext32BitNumber()
        {
            return (uint) (GetNext64BitNumber() >> 32);
        }

        public uint GetNext32BitNumber(uint max)
        {
            return (uint) (((GetNext64BitNumber() >> 32)*max) >> 32);
        }

        public ulong GetNext64BitNumber()
        {
            seed = seed*mult + add;
            return seed;
        }

        public void Advance(uint numFrames)
        {
            for (uint i = 0; i < numFrames; ++i) GetNext64BitNumber();
        }

        // Interface call

        public uint Nextuint()
        {
            return GetNext32BitNumber();
        }
    }

    internal class BWRng : GenericRng64
    {
        public BWRng(ulong seed)
            : base(seed, 0x5d588b656c078965, 0x269ec3)
        {
        }
    }

    internal class BWRngR : GenericRng64
    {
        public BWRngR(ulong seed)
            : base(seed, 0xdedcedae9638806d, 0x9b1ae6e9a384e6f9)
        {
        }
    }
}