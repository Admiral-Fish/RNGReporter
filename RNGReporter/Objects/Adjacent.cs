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
    public class Adjacent
    {
        public Adjacent()
        {
            MaxFrame = 15;
            MinFrame = 1;
        }

        public uint Seed { get; set; }

        public DateTime Date { get; set; }

        public string DisplayDate
        {
            get { return string.Format("{0:yyyy MM dd}", Date); }
        }

        public string DisplayTime
        {
            get { return string.Format("{0:HH:mm:ss}", Date); }
        }

        public int Delay { get; set; }

        public string Flips
        {
            get { return CoinFlips.GetFlips(Seed, 15); }
        }

        public int MinFrame { get; set; }

        public int MaxFrame { get; set; }

        public string IVs
        {
            get { return Gen5IVs.GetIVs(Seed, MinFrame, MaxFrame); }
        }

        public uint CGearAdjust
        {
            // outputs the first result in the 2nd Mersenne Twister table
            // this is so the RNG Check Code can be used with RNG Reporter
            get { return MersenneTwister.Next624(MersenneTwister.generateArray(Seed)); }
        }

        //  Need to have a way to store all of the roamer
        //  locations and all of the saved locations so
        //  we can do our dog calculation for each frame

        public HgSsRoamerInformation RoamerInformtion { get; set; }

        public string RoamerLocations
        {
            get
            {
                string roamerText = "";

                bool firstDisplay = true;

                if (RoamerInformtion.RCurrentRoute != 0)
                {
                    roamerText += "R: " + RoamerInformtion.RCurrentRoute;
                    firstDisplay = false;
                }

                if (RoamerInformtion.ECurrentRoute != 0)
                {
                    if (!firstDisplay)
                        roamerText += "  ";

                    roamerText += "E: " + RoamerInformtion.ECurrentRoute;
                    firstDisplay = false;
                }

                if (RoamerInformtion.LCurrentRoute != 0)
                {
                    if (!firstDisplay)
                        roamerText += "  ";

                    roamerText += "L: " + RoamerInformtion.LCurrentRoute;
                    firstDisplay = false;
                }

                if (firstDisplay)
                {
                    roamerText = "No Roamers";
                }

                return roamerText;
            }
        }

        public string ElmResponses
        {
            get { return Responses.ElmResponses(Seed, 17, RoamerInformtion.RngCalls); }
        }
    }
}