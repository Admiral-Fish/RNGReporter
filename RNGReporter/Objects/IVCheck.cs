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
using System.Collections.Generic;

namespace RNGReporter.Objects
{
    //  This class is going to return a bundle of 
    //  IV possibilities for each different stat
    internal class IVCheck
    {
        //  We need a collection of collections.  A List of Lists is probably
        //  the easiest thing to do here.  

        private readonly Nature nature;
        private readonly Pokemon pokemon;

        private readonly string[] statNames = new[] {"HP", "Atk", "Def", "SpA", "SpD", "Spe"};

        //private bool[] valid = new bool[6] { false, false, false, false, false, false };
        //public bool[] Valid
        //{
        //    get { return valid; }
        //    set { valid = value; }
        //}

        public IVCheck(
            Pokemon pokemon, uint level, Nature nature,
            Characteristic characteristic, uint[] stats)
        {
            this.pokemon = pokemon;
            this.nature = nature;

            //  Initialize our possibilities lists.
            Possibilities = new List<List<uint>>
                {
                    new List<uint>(),
                    new List<uint>(),
                    new List<uint>(),
                    new List<uint>(),
                    new List<uint>(),
                    new List<uint>()
                };

            //  we are going to build out an array of all
            //  of the base stats based on the Pokemon.
            //
            var baseStats = new double[]
                {
                    pokemon.BaseHp, pokemon.BaseAtk, pokemon.BaseDef,
                    pokemon.BaseSpA, pokemon.BaseSpD, pokemon.BaseSpe
                };


            //uint minHp = 31;
            //uint maxHp = 0;

            //remains constant?
            double ev = 0;

            //  This is our internal storage for the IV ranges that
            //  we get with the initial set of data, before the 
            //  characteristic correction.
            var minIvs = new uint[] {31, 31, 31, 31, 31, 31};
            var maxIvs = new uint[] {0, 0, 0, 0, 0, 0};

            //  hrm can we get rid of this?
            var valid = new[] {false, false, false, false, false, false};

            //  Do the iterative test on the Hit Points
            for (uint hpCnt = 0; hpCnt <= 31; hpCnt++)
            {
                uint hp = (uint) Math.Floor(((hpCnt + 2*baseStats[0] + Math.Floor((ev/4.0)))*level/100.0)) + 10 + level;

                if (hp == stats[0])
                {
                    valid[0] = true;

                    if (hpCnt >= maxIvs[0])
                    {
                        maxIvs[0] = hpCnt;
                    }
                    if (hpCnt <= minIvs[0])
                    {
                        minIvs[0] = hpCnt;
                    }
                }
            }

            //  Do the iterative test on all other IVs, since they are
            //  all the same we will iterate through our list of items
            for (int cnt = 1; cnt < 6; cnt++)
            {
                for (uint statCnt = 0; statCnt <= 31; statCnt++)
                {
                    var stat =
                        (uint)
                        Math.Floor((Math.Floor(((baseStats[cnt]*2.0 + statCnt + Math.Floor(ev/4.0))*level)/100.0) + 5.0)*
                                   nature.Adjustments[cnt]);

                    if (stat == stats[cnt])
                    {
                        valid[cnt] = true;

                        if (statCnt >= maxIvs[cnt])
                        {
                            maxIvs[cnt] = statCnt;
                        }
                        if (statCnt <= minIvs[cnt])
                        {
                            minIvs[cnt] = statCnt;
                        }
                    }
                }
            }

            uint characteristicHigh = 31;

            //  Correct for the characteristic, building
            //  our final array of the valid values for
            //  each.
            if (characteristic != null)
            {
                if (valid[characteristic.AffectedStat])
                {
                    //  Set this to zero so we can begin to keep track
                    characteristicHigh = 0;

                    //  If this is not null we need to iterate through the ranges
                    //  of the IV that is referenced and cull out those that are
                    //  not possible.
                    for (uint charCnt = minIvs[characteristic.AffectedStat];
                         charCnt <= maxIvs[characteristic.AffectedStat];
                         charCnt++)
                    {
                        if ((charCnt%5) == characteristic.Mod5result)
                        {
                            Possibilities[(int) characteristic.AffectedStat].Add(charCnt);

                            characteristicHigh = charCnt;
                        }
                    }
                }
            }

            //  Now we want to go and explode out all of the other items, skipping 
            //  the characteristic affected stat is there was one.  We are also 
            //  going to clip to the high mark of the characteristic stat
            for (uint statCnt = 0; statCnt <= 5; statCnt++)
            {
                if (valid[statCnt])
                {
                    //  Make sure we dont make any changes to the characteristic stat
                    if (characteristic == null || characteristic.AffectedStat != statCnt)
                    {
                        // 
                        for (uint charCnt = minIvs[statCnt];
                             charCnt <= maxIvs[statCnt];
                             charCnt++)
                        {
                            if (charCnt <= characteristicHigh)
                            {
                                Possibilities[(int) statCnt].Add(charCnt);
                            }
                        }
                    }
                }
            }

            //  Should be done now, but may add the hidden power in the future
        }

        public List<List<uint>> Possibilities { get; set; }

        public bool IsValid
        {
            get
            {
                return
                    ((Possibilities[0].Count > 0) &&
                     (Possibilities[1].Count > 0) &&
                     (Possibilities[2].Count > 0) &&
                     (Possibilities[3].Count > 0) &&
                     (Possibilities[4].Count > 0) &&
                     (Possibilities[5].Count > 0));
            }
        }

        public override string ToString()
        {
            string ivs =
                pokemon + " - #" +
                pokemon.DexNumber.ToString() +
                " (" + nature.Name + ")" +
                Environment.NewLine;

            //  Need to go through each stat now and display 
            //  the ranges.  General format will be:
            //
            //  2-7,9 (Range and single possibility)
            //  2-7 (Range)
            //  2 (One possibility)

            for (int statCnt = 0; statCnt < Possibilities.Count; statCnt++)
            {
                List<uint> statBlock = Possibilities[statCnt];

                //  Heading with the name of the IV here
                ivs += statNames[statCnt] + ": ";

                if (statBlock.Count == 0)
                {
                    ivs += "Invalid";
                }
                else
                {
                    bool inBlock = false;

                    for (int statBlockCnt = 0; statBlockCnt < statBlock.Count; statBlockCnt++)
                    {
                        string statString = "";

                        if (statBlockCnt == 0)
                        {
                            statString += statBlock[statBlockCnt].ToString();
                        }
                        else
                        {
                            if (statBlock[statBlockCnt] == statBlock[statBlockCnt - 1] + 1)
                            {
                                inBlock = true;

                                //  Check to see if we need to cap here.
                                if (statBlockCnt == statBlock.Count - 1)
                                {
                                    statString += "-" + statBlock[statBlockCnt].ToString();
                                }
                            }
                            else
                            {
                                if (inBlock)
                                {
                                    inBlock = false;
                                    statString += "-" + statBlock[statBlockCnt - 1].ToString();
                                    statString += ", " + statBlock[statBlockCnt].ToString();
                                }
                                else
                                {
                                    statString += ", " + statBlock[statBlockCnt].ToString();
                                }
                            }
                        }

                        ivs += statString;
                    }
                }

                ivs += Environment.NewLine;
            }


            return ivs;
        }
    }
}