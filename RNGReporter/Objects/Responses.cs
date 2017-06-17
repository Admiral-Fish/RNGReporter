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
    internal class Responses
    {
        public static string ElmResponses(uint seed, uint count, uint skips)
        {
            string responses = "";

            var rng = new PokeRng(seed);

            uint rngCalls = count + skips;

            if (skips > 0)
            {
                responses += "(";
            }

            for (int n = 0; n < rngCalls; n++)
            {
                uint response = rng.GetNext16BitNumber();

                response %= 3;

                if (response == 0)
                    responses += "E";
                if (response == 1)
                    responses += "K";
                if (response == 2)
                    responses += "P";

                //  Skip the last item
                if (n != rngCalls - 1)
                {
                    if (skips != 0 && skips == n + 1)
                    {
                        responses += " skipped)   ";
                    }
                    else
                    {
                        responses += ", ";
                    }
                }
            }

            return responses;
        }

        public static string ElmResponse(uint rngResult)
        {
            uint response = rngResult%3;

            string responses = "";

            if (response == 0)
                responses += "E";
            if (response == 1)
                responses += "K";
            if (response == 2)
                responses += "P";

            return responses;
        }

        public static string ChatotResponse(uint rngResult)
        {
            uint result = ((rngResult & 0x1FFF)*100) >> 13;
            if (result < 20)
                return "Low (" + result.ToString() + ")";
            if (result < 40)
                return "Mid-Low (" + result.ToString() + ")";
            if (result < 60)
                return "Mid (" + result.ToString() + ")";
            if (result < 80)
                return "Mid-High (" + result.ToString() + ")";
            return "High (" + result.ToString() + ")";
        }

        public static string ChatotResponse64(uint rngResult)
        {
            uint result = (uint) (((ulong) rngResult*0x1FFF) >> 32)/82;
            if (result < 20)
                return "Low (" + result.ToString() + ")";
            if (result < 40)
                return "Mid-Low (" + result.ToString() + ")";
            if (result < 60)
                return "Mid (" + result.ToString() + ")";
            if (result < 80)
                return "Mid-High (" + result.ToString() + ")";
            return "High (" + result.ToString() + ")";
        }

        public static string ChatotResponse64Short(uint rngResult)
        {
            uint result = (uint) (((ulong) rngResult*0x1FFF) >> 32)/82;
            if (result < 20)
                return "L (" + result.ToString() + ")";
            if (result < 40)
                return "ML (" + result.ToString() + ")";
            if (result < 60)
                return "M (" + result.ToString() + ")";
            if (result < 80)
                return "MH (" + result.ToString() + ")";
            return "H (" + result.ToString() + ")";
        }

        public static string ChatotResponses64(ulong seed, Profile profile)
        {
            string responses = "";

            var rng = new BWRng(seed);
            uint initialFrame = Functions.initialPIDRNG(seed, profile);

            for (uint cnt = 1; cnt < initialFrame; cnt++)
            {
                rng.Next();
            }

            for (uint cnt = 0; cnt < 20; cnt++)
            {
                responses += ChatotResponse64Short(rng.GetNext32BitNumber());
                // skip last item
                if (cnt != 19)
                {
                    responses += ", ";
                }
            }

            return responses;
        }
    }
}