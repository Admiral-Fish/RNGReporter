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
    public class ResearcherProfile
    {
        #region RNGType enum

        public enum RNGType
        {
            LCRNG,
            LCRNGR,
            MT,
            BWRNG,
            BWRNGR,
            XDRNG,
            XDRNGR,
            ARNG,
            ARNGR,
            GRNG,
            GRNGR,
            EncounterLCRNG,
            EncounterLCRNGR,
            MTUntempered,
            MTFast,
            MTTable,
            Custom
        }

        #endregion

        public RNGType Type { get; set; }
        //note: may want to switch to use ulong instead of string
        public string CustomM { get; set; }
        public string CustomA { get; set; }
        public bool Is64Bit { get; set; }
        public int MaxResults { get; set; }
        //note: may want to switch to use ulong instead of string
        public string Seed { get; set; }

        public CustomResearcher[] Custom { get; set; }
    }

    public class CustomResearcher
    {
        #region Operator enum

        public enum Operator
        {
            Division,
            Modulo,
            RShift,
            LShift,
            AND,
            OR,
            XOR,
            Add,
            Subtract,
            Multiply
        }

        #endregion

        #region RelativeOperand enum

        public enum RelativeOperand
        {
            None,
            b64,
            b32,
            b32h,
            b32l,
            b16h,
            b16l,
            Custom1,
            Custom2,
            Custom3,
            Custom4,
            Custom5,
            Custom6,
            Previous1,
            Previous2,
            Previous3,
            Previous4,
            Previous5,
            Previous6
        }

        #endregion

        #region ValueType enum

        public enum ValueType
        {
            b64,
            b32,
            b32h,
            b32l,
            b16h,
            b16l,
            Custom1,
            Custom2,
            Custom3,
            Custom4,
            Custom5,
            Custom6,
            Previous1,
            Previous2,
            Previous3,
            Previous4,
            Previous5,
            Previous6
        }

        #endregion

        public ValueType Type { get; set; }
        public Operator Operation { get; set; }
        //note: may want to switch to use ulong instead of string
        public string Operand { get; set; }
        public bool isHex { get; set; }
        public RelativeOperand RelOperand { get; set; }
    }
}