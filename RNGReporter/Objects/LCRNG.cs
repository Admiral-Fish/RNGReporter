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
    internal class GenericRng : IRNG
    {
        //  This is the generic base that all of the other lcrngs will
        //  use. They will pass in a multiplier, and adder, and a seed.
        protected uint add;
        protected uint mult;

        public GenericRng(uint seed, uint mult, uint add)
        {
            Seed = seed;

            this.mult = mult;
            this.add = add;
        }

        public uint Seed { get; set; }

        #region IRNG Members

        public void Reseed(uint seed)
        {
            Seed = seed;
        }

        // Interface call
        public uint Next()
        {
            return GetNext32BitNumber();
        }

        // Interface call
        public uint Nextuint()
        {
            return GetNext32BitNumber();
        }

        #endregion

        public uint GetNext16BitNumber()
        {
            return GetNext32BitNumber() >> 16;
        }

        public virtual uint GetNext32BitNumber()
        {
            Seed = Seed*mult + add;

            return Seed;
        }

        public void GetNext32BitNumber(int num)
        {
            for (int i = 0; i < num; i++)
                Seed = Seed * mult + add;
        }
    }

    internal class PokeRng : GenericRng
    {
        public PokeRng(uint seed)
            : base(seed, 0x41c64e6d, 0x6073)
        {
        }
    }

    internal class PokeRngR : GenericRng
    {
        public PokeRngR(uint seed)
            : base(seed, 0xeeb9eb65, 0xa3561a1)
        {
        }
    }

    internal class XdRng : GenericRng
    {
        public XdRng(uint seed)
            : base(seed, 0x343FD, 0x269EC3)
        {
        }
    }

    internal class XdRngR : GenericRng
    {
        public XdRngR(uint seed)
            : base(seed, 0xB9B33155, 0xA170F641)
        {
        }
    }

    internal class ARng : GenericRng
    {
        public ARng(uint seed)
            : base(seed, 0x6c078965, 0x01)
        {
        }
    }

    internal class ARngR : GenericRng
    {
        public ARngR(uint seed)
            : base(seed, 0x9638806d, 0x69c77f93)
        {
        }
    }

    internal class GRng : GenericRng
    {
        public GRng(uint seed)
            : base(seed, 0x45, 0x1111)
        {
        }

        public override uint GetNext32BitNumber()
        {
            Seed = (Seed*mult + add) & 0x7fffffff;

            return Seed;
        }
    }

    internal class GRngR : GenericRng
    {
        public GRngR(uint seed)
            : base(seed, 0x233f128d, 0x789467a3)
        {
        }

        public override uint GetNext32BitNumber()
        {
            Seed = (Seed*mult + add) & 0x7fffffff;

            return Seed;
        }
    }

    internal class EncounterRng : GenericRng
    {
        public EncounterRng(uint seed)
            : base(seed, 0x41c64e6d, 0x3039)
        {
        }
    }

    internal class EncounterRngR : GenericRng
    {
        public EncounterRngR(uint seed)
            : base(seed, 0xeeb9eb65, 0xfc77a683)
        {
        }
    }

    internal class MersenneTwisterTable : GenericRng
    {
        public MersenneTwisterTable(uint seed)
            : base(seed, 0x6c078965, 0x01)
        {
        }

        public override uint GetNext32BitNumber()
        {
            Seed = (Seed ^ (Seed >> 30))*mult + add;
            add = (add + 1)%624;

            return Seed;
        }
    }
}