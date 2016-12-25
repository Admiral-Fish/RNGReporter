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
    internal class Gen5IVs
    {
        public static string GetIVs(uint seed, int initialFrame, int maxFrame)
        {
            string ivs = "";

            var rng = new MersenneTwister(seed);

            rng.Nextuint();
            rng.Nextuint();

            for (int n = 1; n < initialFrame; n++)
            {
                rng.Nextuint();
            }

            int rngCalls = maxFrame - initialFrame;

            for (int n = 0; n < rngCalls; n++)
            {
                uint result = rng.Nextuint();
                ivs += GetIV(result);

                if (n != rngCalls - 1)
                {
                    ivs += ", ";
                }
            }

            return ivs;
        }

        public static string GetIV(uint seed)
        {
            uint iv = seed >> 27;

            string ivs = "";
            ivs += iv;

            return ivs;
        }
    }
}