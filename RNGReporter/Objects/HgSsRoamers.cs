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
    internal class HgSsRoamers
    {
        public static HgSsRoamerInformation GetHgSsRoamerInformation(
            uint seed,
            bool rRoaming,
            bool eRoaming,
            bool lRoaming,
            uint rPreviousRoute,
            uint ePreviousRoute,
            uint lPreviousRoute)
        {
            var information = new HgSsRoamerInformation();

            var rng = new PokeRng(seed);

            uint rngCalls = 0;

            if (rRoaming)
            {
                while (true)
                {
                    rngCalls++;
                    ushort rngReturn = rng.GetNext16BitNumber();
                    uint route = RouteFromRngJ(rngReturn);

                    if (rPreviousRoute != route)
                    {
                        information.RCurrentRoute = route;
                        break;
                    }
                }
            }

            if (eRoaming)
            {
                while (true)
                {
                    rngCalls++;
                    ushort rngReturn = rng.GetNext16BitNumber();
                    uint route = RouteFromRngJ(rngReturn);

                    if (ePreviousRoute != route)
                    {
                        information.ECurrentRoute = route;
                        break;
                    }
                }
            }

            if (lRoaming)
            {
                while (true)
                {
                    rngCalls++;
                    ushort rngReturn = rng.GetNext16BitNumber();
                    uint route = RouteFromRngK(rngReturn);

                    if (lPreviousRoute != route)
                    {
                        information.LCurrentRoute = route;
                        break;
                    }
                }
            }

            information.RngCalls = rngCalls;

            return information;
        }

        public static uint RouteFromRngJ(ushort rng)
        {
            uint route = 0;

            uint rngmod = (ushort) (rng%16);

            if (rngmod < 11)
                route = rngmod + 29;
            else
                route = rngmod + 31;

            return route;
        }

        public static uint RouteFromRngK(ushort rng)
        {
            uint route = 0;

            uint rngmod = (ushort) (rng%25);

            if (rngmod < 22)
                route = rngmod + 1;
            else
                switch (rngmod)
                {
                    case 22:
                        route = 24;
                        break;
                    case 23:
                        route = 26;
                        break;
                    case 24:
                        route = 28;
                        break;
                }

            return route;
        }
    }

    public class HgSsRoamerInformation
    {
        // Current Locations
        public HgSsRoamerInformation()
        {
            RngCalls = 0;
            ECurrentRoute = 0;
            LCurrentRoute = 0;
            RCurrentRoute = 0;
        }

        public uint RCurrentRoute { get; set; }

        public uint ECurrentRoute { get; set; }

        public uint LCurrentRoute { get; set; }

        // RNG Progressions

        public uint RngCalls { get; set; }

        // Some display options
    }
}