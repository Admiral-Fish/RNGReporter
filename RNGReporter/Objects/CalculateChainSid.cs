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


using System.Collections.Generic;

namespace RNGReporter.Objects
{
    internal class CalculateChainSid
    {
        //  Store a list of canidate SID here to start with
        public CalculateChainSid(uint id)
        {
            Pokemon = new List<CalculateChainSidPokemon>();
            CandidateSids = new List<uint>();
            //  Set up our trainer ID that we are going to
            //  use using for the duration of this run.
            Id = id;

            //  Set up our initial list of SIDS that we
            //  are going to be testing here.  We will
            //  narrow this list down during our process
            for (uint cnt = 0; cnt <= 0xFFFF; cnt += 8)
            {
                CandidateSids.Add(cnt);
            }
        }

        public List<uint> CandidateSids { get; private set; }

        public uint Id { get; set; }

        //  Store a list of the Pokemon that have been entered
        //  that we will use for display purposes in our grid

        internal List<CalculateChainSidPokemon> Pokemon { get; private set; }

        //  Add a list new Pokemon and do the breakdown
        public void Add(
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            Nature nature,
            string ability,
            GenderGenderRatio gender)
        {
            string ivs = hp + " / " + atk + " / " + def + " / " + spa + " / " + spd + " / " + spe;
            Pokemon.Add(new CalculateChainSidPokemon(ivs, nature.Name, ability, gender.ShortName));

            // Build the block for the 2nd IV here.
            uint iv2 = spe + (spa << 5) + (spd << 10);
            uint iv2set = iv2 ^ 0x8000;

            uint iv1 = hp + (atk << 5) + (def << 10);
            uint iv1set = iv1 ^ 0x8000;

            var candidatePids = new List<CandidatePid>();

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0; cnt <= 0x1FFFE; cnt++)
            {
                uint iv2_test;

                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                iv2_test = (cnt & 1) == 0 ? iv2 : iv2set;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (iv2_test << 16) + (cnt%0xFFFF);

                var rng = new PokeRngR(seed);

                uint iv1_rng = rng.GetNext16BitNumber();

                //  Check for a canididate here
                if (iv1_rng == iv1 || iv1_rng == iv1set)
                {
                    //  Get the whole adjust number in a loop
                    uint adjust = 0x0;

                    for (int adjustCnt = 0; adjustCnt < 13; adjustCnt++)
                    {
                        uint adjustRng = rng.GetNext16BitNumber()%2U;
                        adjust |= (adjustRng << (15 - adjustCnt));
                    }

                    //  Get what we think was the initial PID
                    uint pid2 = rng.GetNext16BitNumber(); //  HIGHID
                    uint pid1 = rng.GetNext16BitNumber(); //  LOWID 

                    uint adjustedLow = adjust | (pid1 & 7);

                    uint abilityNumber = adjustedLow%0x2;
                    uint genderNumber = adjustedLow & 0xFF;

                    // lol make this not suck
                    if ((ability == "Single Ability" ||
                         (
                             (abilityNumber == 0 && ability == "Ability 0") ||
                             (abilityNumber == 1 && ability == "Ability 1")
                         )) &&
                        gender.Matches(genderNumber))
                    {
                        var candidatePid = new CandidatePid {AdjustedLow = adjustedLow, NaturalHigh = pid2};
                        candidatePids.Add(candidatePid);
                    }
                }
            }

            var newSids = new List<uint>();

            foreach (uint sid in CandidateSids)
            {
                //  Check each candiate pid that we found for the 
                //  IV values and then see if the final PID with
                //  a particular nature/gender is a match.  If so
                //  we can go ahead and add the sid to the new 
                //  list and exit early.
                foreach (CandidatePid candidatePid in candidatePids)
                {
                    //  Build our full adjusted PID
                    uint adjustedHigh = candidatePid.AdjustedLow ^ Id ^ sid;
                    adjustedHigh &= 0xFFF8;
                    adjustedHigh += (candidatePid.NaturalHigh & 7);

                    //  for testing out the nature comparison
                    uint pid = (adjustedHigh << 16) + candidatePid.AdjustedLow;

                    //  If any of them work, we will add this to 
                    //  the new candidateSids list and break to 
                    //  go to the next seed.  Check the nature.
                    uint pidNature = pid%25;

                    if (nature.Number == pidNature)
                    {
                        newSids.Add(sid);
                        break;
                    }
                }
            }

            CandidateSids = newSids;
        }

        #region Nested type: CandidatePid

        private class CandidatePid
        {
            public uint AdjustedLow { get; set; }

            public uint NaturalHigh { get; set; }
        }

        #endregion
    }
}