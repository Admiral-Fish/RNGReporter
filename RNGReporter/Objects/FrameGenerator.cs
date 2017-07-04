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
    internal class FrameGenerator
    {
        protected Frame frame;
        protected FrameType frameType = FrameType.Method1;
        protected List<Frame> frames;
        private uint lastseed;
        protected uint maxResults;
        protected IRNG mt;
        protected BWRng rng64 = new BWRng(0);
        protected List<uint> rngList;

        public FrameGenerator()
        {
            maxResults = 100000;
            Compatibility = 20;
            InitialFrame = 1;
            InitialSeed = 0;
            SynchNature = -1;
            EncounterMod = EncounterMod.None;
        }

        public uint Calibration { get; set; }

        public FrameType FrameType
        {
            get { return frameType; }
            set
            {
                frameType = value;
                SelectRNG();
            }
        }

        public EncounterType EncounterType { get; set; }

        public EncounterMod EncounterMod { get; set; }

        public bool Everstone { get; set; }

        public int SynchNature { get; set; }

        public ulong InitialSeed { get; set; }

        public uint InitialFrame { get; set; }

        public uint MaxResults
        {
            get { return maxResults; }
            set
            {
                maxResults = value;
                SelectRNG();
            }
        }

        public byte MotherAbility { get; set; }

        public bool DittoUsed { get; set; }

        public bool MaleOnlySpecies { get; set; }

        public bool ShinyCharm { get; set; }

        public uint[] ParentA { get; set; }

        public uint[] ParentB { get; set; }

        public uint[] RNGIVs { get; set; }

        public bool isBW2 { get; set; }

        //a static portion of the PID
        //either lower (R/S eggs) or the entire PID (E eggs)
        public uint StaticPID { get; set; }

        public uint Compatibility { get; set; }

        // by declaring these only once you get a huge performance boost

        // This method ensures that an RNG is only created once,
        // and not every time a Generate function is called
        protected void SelectRNG()
        {
            switch (frameType)
            {
                case FrameType.Gen4Normal:
                    mt = new MersenneTwister(0);
                    break;
                case FrameType.Gen4International:
                    mt = new MersenneTwister(0);
                    break;
                case FrameType.Method5Standard:
                    if ((maxResults + InitialFrame) < 221)
                    {
                        mt = new MersenneTwisterFast(0, (int) (maxResults + InitialFrame + 5));
                    }
                    else
                        mt = new MersenneTwister(0);

                    rngList = new List<uint>();
                    break;
                case FrameType.Method5CGear:
                    if ((maxResults + InitialFrame) < 219)
                    {
                        mt = new MersenneTwisterFast(0, (int) (maxResults + InitialFrame + 9));
                    }
                    else
                        mt = new MersenneTwister(0);

                    rngList = new List<uint>();
                    break;
                case FrameType.Method5Natures:
                    rngList = new List<uint>();
                    break;
            }
        }

        public FrameGenerator Clone()
        {
            var clone = new FrameGenerator
                {
                    frameType = frameType,
                    EncounterType = EncounterType,
                    EncounterMod = EncounterMod,
                    SynchNature = SynchNature,
                    InitialSeed = InitialSeed,
                    InitialFrame = InitialFrame,
                    maxResults = maxResults,
                    DittoUsed = DittoUsed,
                    MaleOnlySpecies = MaleOnlySpecies,
                    ParentA = ParentA,
                    ParentB = ParentB,
                    RNGIVs = RNGIVs
                };

            clone.SelectRNG();

            return clone;
        }


        public List<Frame> Generate(
            FrameCompare frameCompare,
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            List<uint> natures,
            uint minEfgh,
            uint maxEfgh,
            uint id,
            uint sid)
        {
            frames = new List<Frame>();
            var candidates = new List<Frame>();

            var rng = new PokeRngR(0);

            uint x_test = spe | (spa << 5) | (spd << 10);
            uint y_test = hp | (atk << 5) | (def << 10);

            #region

            // Experimentally derived
            // Any possible test seed will have at most
            // a difference of 0x31 from the target seed.
            // If it's close enough, we can then modify it
            // to match.


            /*
            for (uint cnt = 0xFFFF; cnt > 0xF2CC; cnt--)
            {
                uint seed = (x_test << 16) | cnt;                

                // Do a quick search for matching seeds
                // with a lower 16-bits between 0xFFFF and 0xF2CD.
                // We'll take the closest matches and subtract 0xD33
                // until it produces the correct seed (or doesn't).

                // Best we can do until we find a way to
                // calculate them directly.

                rng.Seed = seed;
                uint rng1 = rng.GetNext16BitNumber();

                // We don't have to worry about unsigned overflow
                // because y_test is never more than 0x7FFF
                if (y_test < 0x31)
                {
                    if (rng1 <= (y_test - 0x31))
                    {
                        while ((seed & 0xFFFF) > 0xD32 && (rng1 & 0x7FFF) < y_test)
                        {
                            seed = seed - 0xD33;
                            rng.Seed = seed;
                            rng1 = rng.GetNext16BitNumber();
                        }
                    }
                }
                else
                {
                    if (rng1 >= (y_test - 0x31))
                    {
                        while ((seed & 0xFFFF) > 0xD32 && (rng1 & 0x7FFF) < y_test)
                        {
                            seed = seed - 0xD33;
                            rng.Seed = seed;
                            rng1 = rng.GetNext16BitNumber();
                        }
                    }
                }
                */

            #endregion

            for (uint cnt = 0x0; cnt < 0xFFFF; cnt++)
            {
                uint seed = (x_test << 16) | cnt;

                rng.Seed = seed;
                uint rng1 = rng.GetNext16BitNumber();
                // Check to see if the next frame yields
                // the HP, Attack, and Defense IVs we're searching for
                // If not, skip 'em.
                if ((rng1 & 0x7FFF) != y_test)
                    continue;

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                uint seedWondercard = rng.GetNext32BitNumber();
                var rng2 = seedWondercard >> 16;
                uint rng3 = rng.GetNext16BitNumber();
                uint rng4 = rng.GetNext16BitNumber();

                uint method1Seed = rng.Seed;

                // Instead of re-searching the entire space for seeds that are
                // basically identical except for the upper bit, we'll
                // just flip the upper seed bits instead.
                for (int upperBit = 0; upperBit < 2; upperBit++)
                {
                    rng2 = (ushort) (rng2 ^ 0x8000);
                    rng3 = (ushort) (rng3 ^ 0x8000);
                    rng4 = (ushort) (rng4 ^ 0x8000);
                    method1Seed = method1Seed ^ 0x80000000;
                    rng.Seed = rng.Seed ^ 0x80000000;

                    if (frameType == FrameType.WondercardIVs)
                    {
                        seedWondercard = seedWondercard ^ 0x80000000;
                        frame = Frame.GenerateFrame(seedWondercard,
                                                    frameType, EncounterType,
                                                    0,
                                                    seedWondercard,
                                                    0, 0,
                                                    rng1, x_test,
                                                    id, sid,
                                                    0, 0);

                        candidates.Add(frame);
                    }

                    foreach (uint nature in natures)
                    {
                        if (frameType == FrameType.ChainedShiny)
                        {
                            var testRng = new PokeRngR(rng.Seed);
                            var rngCalls = new uint[15];

                            rngCalls[0] = rng2;
                            rngCalls[1] = rng3;
                            rngCalls[2] = rng4;

                            for (int calls = 3; calls < 15; calls++)
                            {
                                rngCalls[calls] = testRng.GetNext16BitNumber();
                            }

                            testRng.GetNext32BitNumber();
                            testRng.GetNext32BitNumber();

                            uint chainedPIDLower = Functions.ChainedPIDLower(
                                rngCalls[14],
                                rngCalls[0],
                                rngCalls[1],
                                rngCalls[2],
                                rngCalls[3],
                                rngCalls[4],
                                rngCalls[5],
                                rngCalls[6],
                                rngCalls[7],
                                rngCalls[8],
                                rngCalls[9],
                                rngCalls[10],
                                rngCalls[11],
                                rngCalls[12]);

                            uint chainedPIDUpper = Functions.ChainedPIDUpper(rngCalls[13], chainedPIDLower, id, sid);

                            if (IVtoSeed.CheckPID(chainedPIDUpper, chainedPIDLower, nature))
                            {
                                frame = Frame.GenerateFrame(testRng.Seed,
                                                            frameType, EncounterType,
                                                            0,
                                                            testRng.Seed,
                                                            chainedPIDLower, chainedPIDUpper,
                                                            rng1, x_test,
                                                            id, sid,
                                                            0, 0);

                                candidates.Add(frame);
                            }
                        }

                        if (frameType == FrameType.Method1)
                        {
                            //  Check Method 1
                            // [PID] [PID] [IVs] [IVs]
                            // [rng3] [rng2] [rng1] [START]

                            if (IVtoSeed.CheckPID(rng2, rng3, nature))
                            {
                                frame = Frame.GenerateFrame(method1Seed,
                                                            frameType, EncounterType,
                                                            0,
                                                            method1Seed,
                                                            rng3, rng2,
                                                            rng1, x_test,
                                                            id, sid,
                                                            0, 0);

                                candidates.Add(frame);
                            }
                        }

                        if (frameType == FrameType.MethodJ)
                        {
                            //  Check Method 1, then roll back to see if it is a hittable frame
                            if (IVtoSeed.CheckPID(rng2, rng3, nature))
                            {
                                var testRng = new PokeRngR(rng.Seed);

                                uint testPid;
                                uint nextRng = rng4;
                                uint nextRng2 = testRng.GetNext32BitNumber();
                                uint slot = 0;

                                // A spread is only made accessible if there are no other PIDs between
                                // it and the calling frame that have the same nature as the spread.

                                do
                                {
                                    bool skipFrame = false;
                                    // Check to see if this is a valid non-Synch calling frame
                                    // If it is, we won't bother checking it with Synch because it works either way

                                    if (nextRng/0xA3E == nature)
                                    {
                                        uint testSeed = testRng.Seed;
                                        var encounterMod = EncounterMod.None;

                                        if (EncounterType != EncounterType.Stationary)
                                        {
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            slot = nextRng2;
                                            if (EncounterType == EncounterType.WildSurfing)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            }
                                            else if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble <= 48)
                                                {
                                                    if (nibble > 24)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble <= 98)
                                                {
                                                    if (nibble > 49)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble <= 99)
                                                {
                                                    if (nibble > 74)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                        }

                                        if (!skipFrame)
                                        {
                                            frame = Frame.GenerateFrame(testSeed,
                                                                        frameType, EncounterType,
                                                                        0,
                                                                        testSeed,
                                                                        rng3, rng2,
                                                                        rng1, x_test,
                                                                        id, sid,
                                                                        0, 0);

                                            frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                  EncounterType);
                                            frame.EncounterMod = encounterMod;
                                            candidates.Add(frame);
                                        }

                                        // Check if the frame appears as the result of a failed Synch                                        
                                        if (nextRng2 >> 31 == 1)
                                        {
                                            if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble > 24)
                                                {
                                                    skipFrame = true;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble > 49)
                                                {
                                                    skipFrame = true;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble > 74)
                                                {
                                                    skipFrame = true;
                                                }
                                            }

                                            slot = slot*0xeeb9eb65 + 0xa3561a1;
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;

                                            if (!skipFrame)
                                            {
                                                frame = Frame.GenerateFrame(testSeed,
                                                                            frameType, EncounterType,
                                                                            0,
                                                                            testSeed,
                                                                            rng3, rng2,
                                                                            rng1, x_test,
                                                                            id, sid,
                                                                            0, 0);

                                                frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                      EncounterType);

                                                frame.EncounterMod = EncounterMod.Synchronize;
                                                candidates.Add(frame);
                                            }
                                        }
                                    }
                                        // Check to see if the spread is hittable with Synchronize
                                    else if (nextRng >> 15 == 0)
                                    {
                                        uint testSeed = testRng.Seed;

                                        if (EncounterType != EncounterType.Stationary)
                                        {
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            slot = nextRng2;
                                            if (EncounterType == EncounterType.WildSurfing)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            }
                                            else if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble > 24)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)/656;

                                                if (nibble > 49)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;

                                                uint nibble = (testSeed >> 16)/656;
                                                if (nibble > 74)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                        }

                                        if (!skipFrame)
                                        {
                                            frame = Frame.GenerateFrame(testSeed,
                                                                        frameType, EncounterType,
                                                                        0,
                                                                        testSeed,
                                                                        rng3, rng2,
                                                                        rng1, x_test,
                                                                        id, sid,
                                                                        0, 0);

                                            frame.Synchable = true;
                                            frame.EncounterMod = EncounterMod.Synchronize;
                                            frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                  EncounterType);
                                            candidates.Add(frame);
                                        }
                                    }

                                    testPid = (nextRng << 16) | nextRng2 >> 16;

                                    nextRng = testRng.GetNext16BitNumber();
                                    nextRng2 = testRng.GetNext32BitNumber();
                                } while (testPid%25 != nature);
                            }


                            //  Check DPPt Cute Charm (female)
                            //  [CC Check] [PID] [IVs] [IVs]
                            //  [rng3] [rng2] [rng1] [START]

                            if (rng3/0x5556 != 0)
                            {
                                uint CCSeed;
                                uint slot = 0;
                                bool skipFrame = false;

                                if (EncounterType == EncounterType.Stationary)
                                    CCSeed = method1Seed;
                                else
                                {
                                    CCSeed = method1Seed*0xeeb9eb65 + 0xa3561a1;
                                    slot = method1Seed;
                                    if (EncounterType == EncounterType.WildSurfing)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildOldRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)/656 > 24)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildGoodRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)/656 > 49)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildSuperRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)/656 > 74)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                }

                                // Each gender ratio has a different
                                // unbiased (non-nature-affective) number that is
                                // added to the PID
                                var choppedPID = (ushort) (rng2/0xA3E);
                                if (!skipFrame && choppedPID % 25 == nature)
                                {
                                    foreach (uint buffer in Functions.UnbiasedBuffer)
                                    {
                                        frame = Frame.GenerateFrame(CCSeed,
                                                                    frameType, EncounterType, 0,
                                                                    CCSeed,
                                                                    choppedPID + buffer, 0,
                                                                    rng1, x_test,
                                                                    id, sid,
                                                                    0, 0);

                                        frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                              EncounterType);
                                        switch (buffer)
                                        {
                                            case 0x0:
                                                frame.EncounterMod = EncounterMod.CuteCharmFemale;
                                                break;
                                            case 0x96:
                                                frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                break;
                                            case 0xC8:
                                                frame.EncounterMod = EncounterMod.CuteCharm25M;
                                                break;
                                            case 0x4B:
                                                frame.EncounterMod = EncounterMod.CuteCharm75M;
                                                break;
                                            case 0x32:
                                                frame.EncounterMod = EncounterMod.CuteCharm875M;
                                                break;
                                            default:
                                                frame.EncounterMod = EncounterMod.CuteCharm;
                                                break;
                                        }
                                        candidates.Add(frame);
                                    }
                                }
                            }
                        }
                        else if (frameType == FrameType.MethodK)
                        {
                            //  Check Method 1, then roll back to see if it is a hittable frame
                            if (IVtoSeed.CheckPID(rng2, rng3, nature))
                            {
                                var testRng = new PokeRngR(rng.Seed);

                                uint testPid;
                                uint nextRng = rng4;
                                uint nextRng2 = testRng.GetNext32BitNumber();
                                uint slot = 0;

                                // A spread is only made accessible if there are no other PIDs between
                                // it and the calling frame that have the same nature as the spread.

                                do
                                {
                                    bool skipFrame = false;
                                    // Check to see if this is a valid non-Synch calling frame
                                    // If it is, we won't bother checking it with Synch because it works either way

                                    if (nextRng%25 == nature)
                                    {
                                        uint testSeed = testRng.Seed;
                                        var encounterMod = EncounterMod.None;

                                        if (EncounterType != EncounterType.Stationary)
                                        {
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            slot = nextRng2;
                                            if (EncounterType == EncounterType.WildSurfing ||
                                                EncounterType == EncounterType.BugCatchingContest ||
                                                EncounterType == EncounterType.Headbutt)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            }
                                            else if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble <= 48)
                                                {
                                                    if (nibble > 24)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble <= 98)
                                                {
                                                    if (nibble > 49)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble <= 99)
                                                {
                                                    if (nibble > 74)
                                                    {
                                                        encounterMod = EncounterMod.SuctionCups;
                                                    }
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                                else
                                                    skipFrame = true;
                                            }
                                        }

                                        if (!skipFrame)
                                        {
                                            frame = Frame.GenerateFrame(testSeed,
                                                                        frameType, EncounterType,
                                                                        0,
                                                                        testSeed,
                                                                        rng3, rng2,
                                                                        rng1, x_test,
                                                                        id, sid,
                                                                        0, 0);

                                            frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                  EncounterType);
                                            frame.EncounterMod = encounterMod;
                                            candidates.Add(frame);
                                        }

                                        // Check if the frame appears as the result of a failed Synch                                        
                                        if (((nextRng2 >> 16) & 1) == 1)
                                        {
                                            if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble > 24)
                                                {
                                                    skipFrame = true;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble > 49)
                                                {
                                                    skipFrame = true;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble > 74)
                                                {
                                                    skipFrame = true;
                                                }
                                            }

                                            slot = slot*0xeeb9eb65 + 0xa3561a1;
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;

                                            if (!skipFrame)
                                            {
                                                frame = Frame.GenerateFrame(testSeed,
                                                                            frameType, EncounterType,
                                                                            0,
                                                                            testSeed,
                                                                            rng3, rng2,
                                                                            rng1, x_test,
                                                                            id, sid,
                                                                            0, 0);

                                                frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                      EncounterType);

                                                frame.EncounterMod = EncounterMod.Synchronize;
                                                candidates.Add(frame);
                                            }
                                        }
                                    }
                                        // Check to see if the spread is hittable with Synchronize
                                    else if ((nextRng & 1) == 0)
                                    {
                                        uint testSeed = testRng.Seed;

                                        if (EncounterType != EncounterType.Stationary)
                                        {
                                            testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            slot = nextRng2;
                                            if (EncounterType == EncounterType.WildSurfing ||
                                                EncounterType == EncounterType.BugCatchingContest ||
                                                EncounterType == EncounterType.Headbutt)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                            }
                                            else if (EncounterType == EncounterType.WildOldRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble > 24)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildGoodRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                uint nibble = (testSeed >> 16)%100;

                                                if (nibble > 49)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                            else if (EncounterType == EncounterType.WildSuperRod)
                                            {
                                                slot = testSeed;
                                                testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;

                                                uint nibble = (testSeed >> 16)%100;
                                                if (nibble > 74)
                                                {
                                                    skipFrame = true;
                                                }
                                                else
                                                {
                                                    testSeed = testSeed*0xeeb9eb65 + 0xa3561a1;
                                                }
                                            }
                                        }

                                        if (!skipFrame)
                                        {
                                            frame = Frame.GenerateFrame(testSeed,
                                                                        frameType, EncounterType,
                                                                        0,
                                                                        testSeed,
                                                                        rng3, rng2,
                                                                        rng1, x_test,
                                                                        id, sid,
                                                                        0, 0);

                                            frame.Synchable = true;
                                            frame.EncounterMod = EncounterMod.Synchronize;
                                            frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                                  EncounterType);
                                            candidates.Add(frame);
                                        }
                                    }

                                    testPid = (nextRng << 16) | nextRng2 >> 16;

                                    nextRng = testRng.GetNext16BitNumber();
                                    nextRng2 = testRng.GetNext32BitNumber();
                                } while (testPid%25 != nature);
                            }

                            if (rng3%3 != 0)
                            {
                                uint CCSeed;
                                uint slot = 0;
                                bool skipFrame = false;

                                if (EncounterType == EncounterType.Stationary)
                                    CCSeed = method1Seed;
                                else
                                {
                                    CCSeed = method1Seed*0xeeb9eb65 + 0xa3561a1;
                                    slot = method1Seed;
                                    if (EncounterType == EncounterType.WildSurfing ||
                                        EncounterType == EncounterType.BugCatchingContest)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildOldRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)%100 > 24)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildGoodRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)%100 > 49)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                    else if (EncounterType == EncounterType.WildSuperRod)
                                    {
                                        CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                        slot = slot*0xeeb9eb65 + 0xa3561a1;

                                        if ((CCSeed >> 16)%100 > 74)
                                            skipFrame = true;
                                        else
                                            CCSeed = CCSeed*0xeeb9eb65 + 0xa3561a1;
                                    }
                                }

                                //  Check HGSS Cute Charm
                                //  [CC Check] [PID] [IVs] [IVs]
                                //  [rng3] [rng2] [rng1] [START]

                                // Each gender ratio has a different
                                // unbiased (non-nature-affective) number that is
                                // added to the PID
                                var choppedPID = (ushort) (rng2%25);
                                if (!skipFrame && choppedPID == nature)
                                {
                                    foreach (uint buffer in Functions.UnbiasedBuffer)
                                    {
                                        frame = Frame.GenerateFrame(CCSeed,
                                                                    frameType, EncounterType, 0,
                                                                    CCSeed,
                                                                    choppedPID + buffer, 0,
                                                                    rng1, x_test,
                                                                    id, sid,
                                                                    0, 0);

                                        frame.EncounterSlot = EncounterSlotCalc.encounterSlot(slot, frameType,
                                                                                              EncounterType);
                                        switch (buffer)
                                        {
                                            case 0x0:
                                                frame.EncounterMod = EncounterMod.CuteCharmFemale;
                                                break;
                                            case 0x96:
                                                frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                break;
                                            case 0xC8:
                                                frame.EncounterMod = EncounterMod.CuteCharm25M;
                                                break;
                                            case 0x4B:
                                                frame.EncounterMod = EncounterMod.CuteCharm75M;
                                                break;
                                            case 0x32:
                                                frame.EncounterMod = EncounterMod.CuteCharm875M;
                                                break;
                                            default:
                                                frame.EncounterMod = EncounterMod.CuteCharm;
                                                break;
                                        }
                                        candidates.Add(frame);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Now that we have some possibilities for frames,
            // We'll filter out ones that don't meet user criteria
            // Then roll back the RNG for each of them to make sure
            // each is within the user-specified maximum frames
            // from a DPPtHGSS-style seed.
            foreach (Frame candidate in candidates)
            {
                if (frameCompare.Compare(candidate))
                {
                    // start backing up frames until the user-specified max
                    rng.Seed = candidate.Seed;

                    const uint start = 1;

                    for (uint backCount = start; backCount <= MaxResults; backCount++)
                    {
                        uint testSeed = rng.Seed;

                        //uint seedAB = testSeed >> 24;
                        uint seedCD = (testSeed & 0x00FF0000) >> 16;
                        uint seedEFGH = testSeed & 0x0000FFFF;

                        // Check to see if seed follows ABCDEFGH format
                        // And matches-user specified delay
                        if (seedEFGH > minEfgh && seedEFGH < maxEfgh)
                        {
                            // CD must be between 0-23
                            if (seedCD < 23)
                            {
                                if (backCount >= InitialFrame)
                                {
                                    Frame frameCopy = Frame.Clone(candidate);

                                    frameCopy.Seed = testSeed;
                                    frameCopy.Number = backCount;
                                    frames.Add(frameCopy);
                                }
                            }
                        }

                        rng.GetNext32BitNumber();
                    }
                }
            }

            return frames;
        }

        // This is the smarter way of generating spreads
        // Takes a desired spread and derives the seed
        // Rather than searching all spreads for a match
        public List<Frame> Generate(
            uint minhp,
            uint maxhp,
            uint minatk,
            uint maxatk,
            uint mindef,
            uint maxdef,
            uint minspa,
            uint maxspa,
            uint minspd,
            uint maxspd,
            uint minspe,
            uint maxspe,
            uint nature)
        {
            var frames = new List<Frame>();
            var rngXD = new XdRng(0);

            for (uint hp = minhp; hp <= maxhp; hp++)
            {
                for (uint atk = minatk; atk <= maxatk; atk++)
                {
                    for (uint def = mindef; def <= maxdef; def++)
                    {
                        for (uint spa = minspa; spa <= maxspa; spa++)
                        {
                            for (uint spd = minspd; spd <= maxspd; spd++)
                            {
                                for (uint spe = minspe; spe <= maxspe; spe++)
                                {
                                    frame = null;

                                    uint x_test = hp | (atk << 5) | (def << 10);
                                    uint y_test = spe | (spa << 5) | (spd << 10);

                                    //  Now we want to start with IV2 and call the RNG for
                                    //  values between 0 and FFFF in the low order bits.
                                    for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
                                    {
                                        //  Set our test seed here so we can start
                                        //  working backwards to see if the rest
                                        //  of the information we were provided 
                                        //  is a match.

                                        uint seed = (x_test << 16) | cnt;

                                        rngXD.Seed = seed;

                                        //  We have a max of 5 total RNG calls
                                        //  to make a pokemon and we already have
                                        //  one so lets go ahead and get 4 more.
                                        uint rng1XD = rngXD.GetNext16BitNumber();
                                        if ((rng1XD & 0x7FFF) != y_test)
                                            continue;

                                        // this second call isn't used for anything we know of
                                        uint rng2XD = rngXD.GetNext16BitNumber();
                                        uint rng3XD = rngXD.GetNext16BitNumber();
                                        uint rng4XD = rngXD.GetNext16BitNumber();

                                        uint XDColoSeed = seed*0xB9B33155 + 0xA170F641;

                                        //  Check Colosseum\XD
                                        // [IVs] [IVs] [xxx] [PID] [PID]
                                        // [START] [rng1] [rng3] [rng4]

                                        for (int highBit = 0; highBit < 2; highBit++)
                                        {
                                            XDColoSeed = XDColoSeed ^ 0x80000000;
                                            rng3XD = rng3XD ^ 0x8000;
                                            rng4XD = rng4XD ^ 0x8000;

                                            if (((rng3XD << 16) | rng4XD) % 25 == nature)
                                            {
                                                frame = Frame.GenerateFrame
                                                    (XDColoSeed,
                                                     FrameType.ColoXD,
                                                     0,
                                                     XDColoSeed,
                                                     rng4XD,
                                                     rng3XD,
                                                     x_test,
                                                     rng1XD,
                                                     0, 0, 0, 0, 0, 0, 0, 0, 0);
                                                frame.DisplayPrep();

                                                frames.Add(frame);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return frames;
        }

        // This generates a single frame, using IVs recalled from a stored hashtable
        public List<Frame> Generate(FrameCompare frameCompare, uint seed, uint IVHash, uint frameNumber)
        {
            frames = new List<Frame>();

            frame = Frame.GenerateFrame(
                FrameType.Method5Standard,
                frameNumber,
                seed,
                (IVHash & 0x1F),
                (IVHash & 0x3E0) >> 5,
                (IVHash & 0x7C00) >> 10,
                ((IVHash >> 16) & 0x3E0) >> 5,
                ((IVHash >> 16) & 0x7C00) >> 10,
                ((IVHash >> 16) & 0x1F));

            if (frameCompare.Compare(frame))
            {
                frames.Add(frame);
            }

            return frames;
        }

        public List<Frame> Generate(
            FrameCompare frameCompare,
            uint id,
            uint sid)
        {
            frames = new List<Frame>();

            //  The first thing we need to do is check for
            //  whether we are using the LCRNG or MTRNG
            if (frameType == FrameType.Gen4Normal ||
                frameType == FrameType.Gen4International)
            {
                mt.Reseed((uint) InitialSeed);
                frame = null;

                for (uint cnt = 1; cnt < InitialFrame + maxResults; cnt++)
                {
                    if (cnt < InitialFrame)
                    {
                        mt.Nextuint();
                        continue;
                    }

                    switch (frameType)
                    {
                        case FrameType.Gen4Normal:
                            uint mtResult = mt.Nextuint();

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.Gen4Normal,
                                    cnt,
                                    mtResult,
                                    mtResult,
                                    id, sid);

                            break;

                        case FrameType.Gen4International:

                            //  We want to get our random number
                            //  first and then go through and check
                            //  to see if it is shiny.
                            uint pid = mt.Nextuint();

                            for (int n = 0; n <= 3; n++)
                            {
                                uint tid = (id & 0xffff) | ((sid & 0xffff) << 16);

                                uint a = pid ^ tid;
                                uint b = a & 0xffff;
                                uint c = (a >> 16) & 0xffff;
                                uint d = b ^ c;

                                if (d < 8)
                                {
                                    break;
                                }

                                // ARNG
                                pid = pid*0x6c078965 + 1;
                            }

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.Gen4International,
                                    cnt,
                                    pid,
                                    pid,
                                    id, sid);

                            break;
                    }


                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.Method5Standard)
            {
                mt.Reseed((uint) InitialSeed);

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    mt.Nextuint();

                for (int i = 0; i < 6; i++)
                    rngList.Add(mt.Nextuint() >> 27);

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(mt.Nextuint() >> 27))
                {
                    if (EncounterType == EncounterType.Roamer)
                    {
                        if (!frameCompare.CompareIV(0, rngList[1]))
                            continue;
                        if (!frameCompare.CompareIV(1, rngList[2]))
                            continue;
                        if (!frameCompare.CompareIV(2, rngList[3]))
                            continue;
                        if (!frameCompare.CompareIV(3, rngList[6]))
                            continue;
                        if (!frameCompare.CompareIV(4, rngList[4]))
                            continue;
                        if (!frameCompare.CompareIV(5, rngList[5]))
                            continue;

                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5Standard,
                                cnt + InitialFrame,
                                (uint) InitialSeed,
                                rngList[1],
                                rngList[2],
                                rngList[3],
                                rngList[6],
                                rngList[4],
                                rngList[5]);
                    }
                    else
                    {
                        if (!frameCompare.CompareIV(0, rngList[0]))
                            continue;
                        if (!frameCompare.CompareIV(1, rngList[1]))
                            continue;
                        if (!frameCompare.CompareIV(2, rngList[2]))
                            continue;
                        if (!frameCompare.CompareIV(3, rngList[3]))
                            continue;
                        if (!frameCompare.CompareIV(4, rngList[4]))
                            continue;
                        if (!frameCompare.CompareIV(5, rngList[5]))
                            continue;

                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5Standard,
                                cnt + InitialFrame,
                                (uint) InitialSeed,
                                rngList[0],
                                rngList[1],
                                rngList[2],
                                rngList[3],
                                rngList[4],
                                rngList[5]);
                    }

                    frames.Add(frame);
                }
            }
            else if (frameType == FrameType.Method5CGear)
            {
                mt.Reseed((uint) InitialSeed);

                // first two frames are skipped
                mt.Nextuint();
                mt.Nextuint();

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    mt.Nextuint();

                for (int i = 0; i < 6; i++)
                    rngList.Add(mt.Nextuint() >> 27);

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(mt.Nextuint() >> 27))
                {
                    frame = null;

                    if (EncounterType == EncounterType.Roamer)
                    {
                        if (!frameCompare.CompareIV(0, rngList[1]))
                            continue;
                        if (!frameCompare.CompareIV(1, rngList[2]))
                            continue;
                        if (!frameCompare.CompareIV(2, rngList[3]))
                            continue;
                        if (!frameCompare.CompareIV(3, rngList[6]))
                            continue;
                        if (!frameCompare.CompareIV(4, rngList[4]))
                            continue;
                        if (!frameCompare.CompareIV(5, rngList[5]))
                            continue;

                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5CGear,
                                cnt + InitialFrame,
                                (uint) InitialSeed,
                                rngList[1],
                                rngList[2],
                                rngList[3],
                                rngList[6],
                                rngList[4],
                                rngList[5]);
                    }
                    else
                    {
                        if (!frameCompare.CompareIV(0, rngList[0]))
                            continue;
                        if (!frameCompare.CompareIV(1, rngList[1]))
                            continue;
                        if (!frameCompare.CompareIV(2, rngList[2]))
                            continue;
                        if (!frameCompare.CompareIV(3, rngList[3]))
                            continue;
                        if (!frameCompare.CompareIV(4, rngList[4]))
                            continue;
                        if (!frameCompare.CompareIV(5, rngList[5]))
                            continue;

                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5CGear,
                                cnt + InitialFrame,
                                (uint) InitialSeed,
                                rngList[0],
                                rngList[1],
                                rngList[2],
                                rngList[3],
                                rngList[4],
                                rngList[5]);
                    }

                    frames.Add(frame);
                }
            }
            else if (frameType == FrameType.Method5Natures)
            {
                rng64.Seed = InitialSeed;
                int encounterSlot = 0;
                const uint item = 0;
                var mod = EncounterMod.None;

                uint idLower = (id & 1) ^ (sid & 1);

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng64.GetNext64BitNumber();

                for (int cnt = 0; cnt < 7; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                var entreeTimer = new CGearTimer();
                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    uint nature;
                    uint pid;
                    bool synchable;
                    if (EncounterType == EncounterType.Gift || EncounterType == EncounterType.Roamer)
                    {
                        nature = (uint) (((ulong) rngList[1]*25) >> 32);
                        synchable = false;

                        pid = rngList[0];
                        if (EncounterType != EncounterType.Roamer)
                            pid = pid ^ 0x10000;
                    }
                    else
                    {
                        uint idTest;
                        if (EncounterType == EncounterType.Wild || EncounterType == EncounterType.WildSurfing ||
                            EncounterType == EncounterType.WildWaterSpot)
                        {
                            encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType, EncounterType,
                                                                            isBW2);

                            if (EncounterMod == EncounterMod.Synchronize)
                            {
                                synchable = (rngList[0] >> 31) == 1;
                                if (synchable)
                                    nature = (uint) SynchNature;
                                else
                                    nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                pid = rngList[3];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.CuteCharm)
                            {
                                pid = rngList[3];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                // failed CC check
                                if (!synchable)
                                {
                                    // leave it as-is
                                    nature = (uint) (((ulong) rngList[4]*25) >> 32);
                                }
                                else
                                {
                                    pid = Functions.GenderModPID(pid, rngList[4], SynchNature);
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);
                                }

                                synchable = false;
                            }
                            else if (EncounterMod == EncounterMod.Compoundeyes ||
                                     EncounterMod == EncounterMod.SuctionCups)
                            {
                                synchable = false;
                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[0], frameType,
                                                                                EncounterType);
                                nature = (uint) (((ulong) rngList[3]*25) >> 32);

                                pid = rngList[2];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.Search)
                            {
                                pid = rngList[3];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                // passed CC check
                                if (synchable)
                                {
                                    // Add all the Cute Charm possibilities
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                    for (int i = -4; i < 5; i++)
                                    {
                                        if (i == 0)
                                            continue;

                                        uint tempPid = Functions.GenderModPID(pid, rngList[4], i);

                                        idTest = (idLower ^ (tempPid & 1) ^ (tempPid >> 31));
                                        if (idTest == 1)
                                            tempPid = (tempPid ^ 0x80000000);

                                        frame = Frame.GenerateFrame(
                                            FrameType.Method5Natures,
                                            EncounterType,
                                            cnt + InitialFrame,
                                            rngList[0],
                                            tempPid,
                                            id,
                                            sid,
                                            nature,
                                            false,
                                            encounterSlot,
                                            item);

                                        switch (i)
                                        {
                                            case 1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                break;
                                            case 2:
                                                frame.EncounterMod = EncounterMod.CuteCharm75M;
                                                break;
                                            case 3:
                                                frame.EncounterMod = EncounterMod.CuteCharm25M;
                                                break;
                                            case 4:
                                                frame.EncounterMod = EncounterMod.CuteCharm875M;
                                                break;
                                            case -1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50F;
                                                break;
                                            case -2:
                                                frame.EncounterMod = EncounterMod.CuteCharm75F;
                                                break;
                                            case -3:
                                                frame.EncounterMod = EncounterMod.CuteCharm25F;
                                                break;
                                            case -4:
                                                frame.EncounterMod = EncounterMod.CuteCharm125F;
                                                break;
                                        }

                                        if (frameCompare.Compare(frame))
                                        {
                                            frames.Add(frame);
                                        }
                                    }
                                }

                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                if (synchable && !frameCompare.CompareNature(nature))
                                    mod = EncounterMod.Synchronize;
                                else
                                    mod = EncounterMod.None;
                            }
                            else
                            {
                                pid = rngList[3];
                                pid = pid ^ 0x10000;

                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);
                            }

                            idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                            if (idTest == 1)
                                pid = (pid ^ 0x80000000);
                        }
                        else if (EncounterType == EncounterType.WildCaveSpot)
                        {
                            if (((ulong) rngList[0]*1000 >> 32) < 400)
                            {
                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[2], frameType,
                                                                                EncounterType);
                            }
                            else
                            {
                                uint calc = ((ulong) rngList[1]*1000 >> 32) < 100 ? 1000u : 1700u;

                                uint result = (uint) ((ulong) rngList[2]*calc >> 32)/100;

                                if (calc == 1000)
                                    encounterSlot = (int) result + 13;
                                else
                                    encounterSlot = (int) result + 23;
                            }

                            if (EncounterMod == EncounterMod.Synchronize)
                            {
                                synchable = (rngList[1] >> 31) == 1;
                                if (synchable)
                                    nature = (uint) SynchNature;
                                else
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                pid = rngList[4];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.CuteCharm)
                            {
                                pid = rngList[4];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[1]*0xFFFF) >> 32)/656) < 67;

                                // failed CC check
                                if (!synchable)
                                {
                                    // leave it as-is
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);
                                }
                                else
                                {
                                    pid = Functions.GenderModPID(pid, rngList[5], SynchNature);
                                    nature = (uint) (((ulong) rngList[6]*25) >> 32);
                                }

                                synchable = false;
                            }
                            else if (EncounterMod == EncounterMod.Compoundeyes ||
                                     EncounterMod == EncounterMod.SuctionCups)
                            {
                                synchable = false;
                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                EncounterType);
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                pid = rngList[3];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.Search)
                            {
                                // Check for item or battle
                                if (((ulong) rngList[0]*1000 >> 32) < 400)
                                {
                                    encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                    EncounterType);
                                }
                                else
                                    continue;

                                // Let's do Suction Cups since it affects the hittable frames

                                pid = rngList[3];
                                pid = pid ^ 0x10000;

                                nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                frame = Frame.GenerateFrame(
                                    FrameType.Method5Natures,
                                    EncounterType,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    pid,
                                    id,
                                    sid,
                                    nature,
                                    false,
                                    encounterSlot,
                                    item);

                                if (frameCompare.Compare(frame))
                                {
                                    frame.EncounterMod = EncounterMod.SuctionCups;
                                    frames.Add(frame);
                                }

                                // Now for regular\Synchronize\Cute Charm encounters

                                pid = rngList[4];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[1]*0xFFFF) >> 32)/656) < 67;

                                // passed CC check
                                if (synchable)
                                {
                                    // Add all the Cute Charm possibilities
                                    nature = (uint) (((ulong) rngList[6]*25) >> 32);

                                    for (int i = -4; i < 5; i++)
                                    {
                                        if (i == 0)
                                            continue;

                                        uint tempPid = Functions.GenderModPID(pid, rngList[5], i);

                                        idTest = (idLower ^ (tempPid & 1) ^ (tempPid >> 31));
                                        if (idTest == 1)
                                            tempPid = (tempPid ^ 0x80000000);

                                        frame = Frame.GenerateFrame(
                                            FrameType.Method5Natures,
                                            EncounterType,
                                            cnt + InitialFrame,
                                            rngList[0],
                                            tempPid,
                                            id,
                                            sid,
                                            nature,
                                            false,
                                            encounterSlot,
                                            item);

                                        switch (i)
                                        {
                                            case 1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                break;
                                            case 2:
                                                frame.EncounterMod = EncounterMod.CuteCharm75M;
                                                break;
                                            case 3:
                                                frame.EncounterMod = EncounterMod.CuteCharm25M;
                                                break;
                                            case 4:
                                                frame.EncounterMod = EncounterMod.CuteCharm875M;
                                                break;
                                            case -1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50F;
                                                break;
                                            case -2:
                                                frame.EncounterMod = EncounterMod.CuteCharm75F;
                                                break;
                                            case -3:
                                                frame.EncounterMod = EncounterMod.CuteCharm25F;
                                                break;
                                            case -4:
                                                frame.EncounterMod = EncounterMod.CuteCharm125F;
                                                break;
                                        }

                                        if (frameCompare.Compare(frame))
                                        {
                                            frames.Add(frame);
                                        }
                                    }
                                }

                                idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                                if (idTest == 1)
                                    pid = (pid ^ 0x80000000);

                                synchable = (rngList[1] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                if (synchable && !frameCompare.CompareNature(nature))
                                    mod = EncounterMod.Synchronize;
                                else
                                    mod = EncounterMod.None;
                            }
                            else
                            {
                                pid = rngList[4];
                                pid = pid ^ 0x10000;

                                synchable = (rngList[1] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);
                            }

                            idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                            if (idTest == 1)
                                pid = (pid ^ 0x80000000);
                        }
                        else if (EncounterType == EncounterType.WildSwarm)
                        {
                            bool swarm = (((ulong) rngList[1]*0xFFFF/0x290) >> 32) < 40;
                            if (swarm)
                                // we'll use non-existent slot 12 to denote a swarm
                                encounterSlot = 12;
                            else
                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[2], frameType,
                                                                                EncounterType);

                            if (EncounterMod == EncounterMod.Synchronize)
                            {
                                synchable = (rngList[0] >> 31) == 1;
                                if (synchable)
                                    nature = (uint) SynchNature;
                                else
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                pid = rngList[4];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.CuteCharm)
                            {
                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                pid = rngList[4];
                                pid = pid ^ 0x10000;

                                // failed CC check
                                if (!synchable)
                                {
                                    // leave it as-is
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);
                                }
                                else
                                {
                                    pid = Functions.GenderModPID(pid, rngList[5], SynchNature);
                                    nature = (uint) (((ulong) rngList[6]*25) >> 32);
                                }

                                synchable = false;
                            }
                            else if (EncounterMod == EncounterMod.Compoundeyes)
                            {
                                swarm = (((ulong) rngList[0]*0xFFFF/0x290) >> 32) < 40;
                                if (swarm)
                                    // we'll use non-existent slot 12 to denote a swarm
                                    encounterSlot = 12;
                                else
                                    encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                    EncounterType);

                                synchable = false;
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                pid = rngList[3];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.Search)
                            {
                                pid = rngList[4];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                // also never used
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                // Add all the Cute Charm possibilities
                                nature = (uint) (((ulong) rngList[6]*25) >> 32);

                                for (int i = -4; i < 5; i++)
                                {
                                    if (i == 0)
                                        continue;

                                    uint tempPid = Functions.GenderModPID(pid, rngList[5], i);

                                    idTest = (idLower ^ (tempPid & 1) ^ (tempPid >> 31));
                                    if (idTest == 1)
                                        tempPid = (tempPid ^ 0x80000000);

                                    frame = Frame.GenerateFrame(
                                        FrameType.Method5Natures,
                                        EncounterType,
                                        cnt + InitialFrame,
                                        rngList[0],
                                        tempPid,
                                        id,
                                        sid,
                                        nature,
                                        false,
                                        encounterSlot,
                                        item);

                                    switch (i)
                                    {
                                        case 1:
                                            frame.EncounterMod = EncounterMod.CuteCharm50M;
                                            break;
                                        case 2:
                                            frame.EncounterMod = EncounterMod.CuteCharm75M;
                                            break;
                                        case 3:
                                            frame.EncounterMod = EncounterMod.CuteCharm25M;
                                            break;
                                        case 4:
                                            frame.EncounterMod = EncounterMod.CuteCharm875M;
                                            break;
                                        case -1:
                                            frame.EncounterMod = EncounterMod.CuteCharm50F;
                                            break;
                                        case -2:
                                            frame.EncounterMod = EncounterMod.CuteCharm75F;
                                            break;
                                        case -3:
                                            frame.EncounterMod = EncounterMod.CuteCharm25F;
                                            break;
                                        case -4:
                                            frame.EncounterMod = EncounterMod.CuteCharm125F;
                                            break;
                                    }

                                    if (frameCompare.Compare(frame))
                                    {
                                        frames.Add(frame);
                                    }
                                }

                                idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                                if (idTest == 1)
                                    pid = (pid ^ 0x80000000);

                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                if (synchable && !frameCompare.CompareNature(nature))
                                    mod = EncounterMod.Synchronize;
                                else
                                    mod = EncounterMod.None;
                            }
                            else
                            {
                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                pid = rngList[4];
                                pid = pid ^ 0x10000;
                            }

                            idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                            if (idTest == 1)
                                pid = (pid ^ 0x80000000);
                        }
                        else if (EncounterType == EncounterType.Stationary)
                        {
                            if (EncounterMod == EncounterMod.Synchronize)
                            {
                                synchable = (rngList[0] >> 31) == 1;
                                if (synchable)
                                    nature = (uint) SynchNature;
                                else
                                    nature = (uint) (((ulong) rngList[2]*25) >> 32);

                                pid = rngList[1];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.CuteCharm)
                            {
                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                pid = rngList[1];
                                pid = pid ^ 0x10000;

                                // failed CC check
                                if (!synchable)
                                {
                                    // leave it as-is
                                    nature = (uint) (((ulong) rngList[2]*25) >> 32);
                                }
                                else
                                {
                                    pid = Functions.GenderModPID(pid, rngList[2], SynchNature);
                                    nature = (uint) (((ulong) rngList[3]*25) >> 32);
                                }

                                synchable = false;
                            }
                            else if (EncounterMod == EncounterMod.Compoundeyes)
                            {
                                synchable = false;
                                nature = (uint) (((ulong) rngList[1]*25) >> 32);

                                pid = rngList[0];
                                pid = pid ^ 0x10000;
                            }
                            else if (EncounterMod == EncounterMod.Search)
                            {
                                pid = rngList[1];
                                pid = pid ^ 0x10000;

                                // not a synch, but the CC check -- need to relabel (unfinished)
                                synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                // CC check
                                if (synchable)
                                {
                                    // Add only 50% Cute Charm possibilities because the only applicable
                                    // stationaries have a 50\50 male-female ratio.
                                    nature = (uint) (((ulong) rngList[3]*25) >> 32);

                                    for (int i = -1; i < 2; i++)
                                    {
                                        if (i == 0)
                                            continue;

                                        uint tempPid = Functions.GenderModPID(pid, rngList[2], i);

                                        idTest = (idLower ^ (tempPid & 1) ^ (tempPid >> 31));
                                        if (idTest == 1)
                                            tempPid = (tempPid ^ 0x80000000);

                                        frame = Frame.GenerateFrame(
                                            FrameType.Method5Natures,
                                            EncounterType,
                                            cnt + InitialFrame,
                                            rngList[0],
                                            tempPid,
                                            id,
                                            sid,
                                            nature,
                                            false,
                                            encounterSlot,
                                            item);

                                        switch (i)
                                        {
                                            case 1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                break;
                                            case -1:
                                                frame.EncounterMod = EncounterMod.CuteCharm50F;
                                                break;
                                        }

                                        if (frameCompare.Compare(frame))
                                        {
                                            frames.Add(frame);
                                        }
                                    }
                                }

                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[2]*25) >> 32);

                                if (synchable && !frameCompare.CompareNature(nature))
                                    mod = EncounterMod.Synchronize;
                                else
                                    mod = EncounterMod.None;
                            }
                            else
                            {
                                synchable = (rngList[0] >> 31) == 1;
                                nature = (uint) (((ulong) rngList[2]*25) >> 32);

                                pid = rngList[1];
                                pid = pid ^ 0x10000;
                            }

                            idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                            if (idTest == 1)
                                pid = (pid ^ 0x80000000);
                        }
                        else if (EncounterType == EncounterType.AllEncounterShiny)
                        {
                            // used for Time Finder searches only

                            pid = rngList[3];
                            pid = pid ^ 0x10000;

                            nature = (uint) (((ulong) rngList[4]*25) >> 32);

                            synchable = ((rngList[0] >> 31) == 1) && ((rngList[2] >> 31) == 1);

                            idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                            if (idTest == 1)
                            {
                                // not the actual PID, but guaranteed to be non-shiny so it won't show up in search results
                                pid = id << 16 | sid ^ 0x100;
                            }
                        }
                        else if (EncounterType == EncounterType.LarvestaEgg)
                        {
                            pid = rngList[0];
                            nature = (uint) (((ulong) rngList[2]*25) >> 32);
                            synchable = false;
                        }
                        else if (EncounterType == EncounterType.Entralink)
                        {
                            pid = rngList[0];

                            synchable = false;

                            // genderless
                            if (frameCompare.GenderFilter.GenderValue == 0xFF)
                            {
                                // leave it as-is
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);
                            }
                                // always female
                            else if (frameCompare.GenderFilter.GenderValue == 0xFE)
                            {
                                var genderAdjustment = (uint) ((0x8*(ulong) rngList[1]) >> 32);
                                pid = (pid & 0xFFFFFF00) | (genderAdjustment + 1);
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);
                            }
                                // always male
                            else if (frameCompare.GenderFilter.GenderValue == 0x0)
                            {
                                var genderAdjustment = (uint) ((0xF6*(ulong) rngList[1]) >> 32);
                                pid = (pid & 0xFFFFFF00) | (genderAdjustment + 8);
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);
                            }
                            else
                            {
                                if (frameCompare.GenderFilter.GenderCriteria == GenderCriteria.Male)
                                {
                                    var genderAdjustment =
                                        (uint)
                                        (((0xFE - frameCompare.GenderFilter.GenderValue)*(ulong) rngList[1]) >>
                                         32);
                                    pid = (pid & 0xFFFFFF00) |
                                          (genderAdjustment + frameCompare.GenderFilter.GenderValue);
                                }
                                else if (frameCompare.GenderFilter.GenderCriteria == GenderCriteria.Female)
                                {
                                    var genderAdjustment =
                                        (uint)
                                        (((frameCompare.GenderFilter.GenderValue - 1)*(ulong) rngList[1]) >> 32);
                                    pid = (pid & 0xFFFFFF00) | (genderAdjustment + 1);
                                }
                                nature = (uint) (((ulong) rngList[5]*25) >> 32);
                            }
                            if ((pid & 0x10000) == 0x10000)
                                pid = pid ^ 0x10000;

                            //note: might be wrong
                            pid ^= 0x10000000;
                        }
                        else if (EncounterType == EncounterType.HiddenGrotto)
                        {
                            // unknown call at 0
                            synchable = (rngList[1] >> 31) == 1;
                            pid = rngList[2];

                            // genderless
                            if (frameCompare.GenderFilter.GenderValue == 0xFF)
                            {
                                // leave it as-is
                                nature = (uint) (((ulong) rngList[3]*25) >> 32);
                            }
                                // always female
                            else if (frameCompare.GenderFilter.GenderValue == 0xFE)
                            {
                                var genderAdjustment = (uint) ((0x8*(ulong) rngList[3]) >> 32);
                                pid = (pid & 0xFFFFFF00) | (genderAdjustment + 1);
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);
                            }
                                // always male
                            else if (frameCompare.GenderFilter.GenderValue == 0x0)
                            {
                                var genderAdjustment = (uint) ((0xF6*(ulong) rngList[3]) >> 32);
                                pid = (pid & 0xFFFFFF00) | (genderAdjustment + 8);
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);
                            }
                            else
                            {
                                if (frameCompare.GenderFilter.GenderCriteria == GenderCriteria.Male)
                                {
                                    var genderAdjustment =
                                        (uint)
                                        (((0xFE - frameCompare.GenderFilter.GenderValue)*(ulong) rngList[3]) >>
                                         32);
                                    pid = (pid & 0xFFFFFF00) |
                                          (genderAdjustment + frameCompare.GenderFilter.GenderValue);
                                }
                                else if (frameCompare.GenderFilter.GenderCriteria == GenderCriteria.Female)
                                {
                                    var genderAdjustment =
                                        (uint)
                                        (((frameCompare.GenderFilter.GenderValue - 1)*(ulong) rngList[3]) >> 32);
                                    pid = (pid & 0xFFFFFF00) | (genderAdjustment + 1);
                                }
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);
                            }
                            if (synchable && EncounterMod == EncounterMod.Synchronize)
                                nature = (uint) SynchNature;
                            pid = pid ^ 0x10000;
                        }
                        else
                        {
                            // Fishing, Shaking Grass Spots

                            // check for Fishing nibble
                            if (EncounterMod == EncounterMod.SuctionCups && EncounterType == EncounterType.WildSuperRod)
                            {
                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                EncounterType);

                                synchable = false;
                                nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                pid = rngList[3];
                                pid = pid ^ 0x10000;
                            }
                            else
                            {
                                if (EncounterType == EncounterType.WildSuperRod && (rngList[1] >> 16)/656 >= 50)
                                    continue;

                                encounterSlot = EncounterSlotCalc.encounterSlot(rngList[2], frameType,
                                                                                EncounterType);

                                if (EncounterMod == EncounterMod.Synchronize)
                                {
                                    synchable = (rngList[0] >> 31) == 1;
                                    if (synchable)
                                        nature = (uint) SynchNature;
                                    else
                                        nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                    pid = rngList[4];
                                    pid = pid ^ 0x10000;
                                }
                                else if (EncounterMod == EncounterMod.CuteCharm)
                                {
                                    // not a synch, but the CC check -- need to relabel (unfinished)
                                    synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                    pid = rngList[4];
                                    pid = pid ^ 0x10000;

                                    // failed CC check
                                    if (!synchable)
                                    {
                                        // leave it as-is
                                        nature = (uint) (((ulong) rngList[5]*25) >> 32);
                                    }
                                    else
                                    {
                                        pid = Functions.GenderModPID(pid, rngList[5], SynchNature);
                                        nature = (uint) (((ulong) rngList[6]*25) >> 32);
                                    }

                                    synchable = false;
                                }
                                else if (EncounterMod == EncounterMod.Compoundeyes)
                                {
                                    if (EncounterType == EncounterType.WildSuperRod &&
                                        (rngList[0] >> 16)/656 >= 50)
                                        continue;

                                    synchable = false;
                                    encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                    EncounterType);
                                    nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                    pid = rngList[3];
                                    pid = pid ^ 0x10000;
                                }
                                else if (EncounterMod == EncounterMod.SuctionCups)
                                {
                                    synchable = false;
                                    encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                    EncounterType);
                                    nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                    pid = rngList[3];
                                    pid = pid ^ 0x10000;
                                }
                                else if (EncounterMod == EncounterMod.Search)
                                {
                                    if (EncounterType == EncounterType.WildSuperRod)
                                    {
                                        // Do the Suction Cups check for fishing frames
                                        encounterSlot = EncounterSlotCalc.encounterSlot(rngList[1], frameType,
                                                                                        EncounterType);
                                        nature = (uint) (((ulong) rngList[4]*25) >> 32);

                                        pid = rngList[3];
                                        pid = pid ^ 0x10000;

                                        idTest = (idLower ^ (pid & 1) ^ (pid >> 31));
                                        if (idTest == 1)
                                            pid = (pid ^ 0x80000000);

                                        frame = Frame.GenerateFrame(
                                            FrameType.Method5Natures,
                                            EncounterType,
                                            cnt + InitialFrame,
                                            rngList[0],
                                            pid,
                                            id,
                                            sid,
                                            nature,
                                            false,
                                            encounterSlot,
                                            item);

                                        if (frameCompare.Compare(frame))
                                        {
                                            frame.EncounterMod = EncounterMod.SuctionCups;
                                            frames.Add(frame);
                                        }
                                    }

                                    pid = rngList[4];
                                    pid = pid ^ 0x10000;

                                    // not a synch, but the CC check -- need to relabel (unfinished)
                                    synchable = ((((ulong) rngList[0]*0xFFFF) >> 32)/656) < 67;

                                    // passed CC check
                                    if (synchable)
                                    {
                                        // Add all the Cute Charm possibilities
                                        nature = (uint) (((ulong) rngList[6]*25) >> 32);

                                        for (int i = -4; i < 5; i++)
                                        {
                                            if (i == 0)
                                                continue;

                                            uint tempPid = Functions.GenderModPID(pid, rngList[5], i);

                                            idTest = (idLower ^ (tempPid & 1) ^ (tempPid >> 31));
                                            if (idTest == 1)
                                                tempPid = (tempPid ^ 0x80000000);

                                            frame = Frame.GenerateFrame(
                                                FrameType.Method5Natures,
                                                EncounterType,
                                                cnt + InitialFrame,
                                                rngList[0],
                                                tempPid,
                                                id,
                                                sid,
                                                nature,
                                                false,
                                                encounterSlot,
                                                item);

                                            switch (i)
                                            {
                                                case 1:
                                                    frame.EncounterMod = EncounterMod.CuteCharm50M;
                                                    break;
                                                case 2:
                                                    frame.EncounterMod = EncounterMod.CuteCharm75M;
                                                    break;
                                                case 3:
                                                    frame.EncounterMod = EncounterMod.CuteCharm25M;
                                                    break;
                                                case 4:
                                                    frame.EncounterMod = EncounterMod.CuteCharm875M;
                                                    break;
                                                case -1:
                                                    frame.EncounterMod = EncounterMod.CuteCharm50F;
                                                    break;
                                                case -2:
                                                    frame.EncounterMod = EncounterMod.CuteCharm75F;
                                                    break;
                                                case -3:
                                                    frame.EncounterMod = EncounterMod.CuteCharm25F;
                                                    break;
                                                case -4:
                                                    frame.EncounterMod = EncounterMod.CuteCharm125F;
                                                    break;
                                            }

                                            if (frameCompare.Compare(frame))
                                            {
                                                frames.Add(frame);
                                            }
                                        }
                                    }

                                    pid = rngList[4];
                                    pid = pid ^ 0x10000;

                                    synchable = (rngList[0] >> 31) == 1;
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                    if (synchable && !frameCompare.CompareNature(nature))
                                        mod = EncounterMod.Synchronize;
                                    else
                                        mod = EncounterMod.None;
                                }
                                else
                                {
                                    synchable = (rngList[0] >> 31) == 1;
                                    nature = (uint) (((ulong) rngList[5]*25) >> 32);

                                    pid = rngList[4];
                                    pid = pid ^ 0x10000;
                                }
                            }

                            idTest = idLower ^ (pid & 1) ^ (pid >> 31);
                            if (idTest == 1)
                            {
                                pid = (pid ^ 0x80000000);
                            }
                        }
                    }

                    // worthless calculation
                    int ability = (int) (pid >> 16) & 1;

                    if (RNGIVs != null)
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5Natures,
                                EncounterType,
                                cnt + InitialFrame,
                                rngList[0],
                                pid,
                                id,
                                sid,
                                nature,
                                synchable,
                                encounterSlot,
                                item,
                                RNGIVs);
                    }
                    else
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.Method5Natures,
                                EncounterType,
                                cnt + InitialFrame,
                                rngList[0],
                                pid,
                                id,
                                sid,
                                nature,
                                synchable,
                                encounterSlot,
                                item);
                    }

                    frame.EncounterMod = mod;
                    frame.CGearTime = entreeTimer.GetTime(rngList[0]);

                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.BWBred)
            {
                rng64.Seed = InitialSeed;
                rngList = new List<uint>();

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (uint cnt = 0; cnt < InitialFrame + 20; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    frame = null;
                    // there's two extra rng calls before the eggs are generated
                    int index = 2;

                    uint nature;
                    bool dream;
                    bool everstone = false;

                    // for Nidoran\Volbeat\Illumise determination
                    // if 0, get Nidoran-F\Volbeat
                    // if 1, get Nidoran-M\Illumise
                    var speciesSpecial = (int) (rngList[index] >> 31);
                    // Everstone activation calc
                    //int off = MaleOnlySpecies ? 1 : 0;
                    if (MaleOnlySpecies) index++;
                    //index = 1
                    nature = (uint) ((ulong) (rngList[index++])*25 >> 32);
                    if (SynchNature > -1)
                    {
                        if ((rngList[index++] >> 31) == 1)
                        {
                            nature = (uint) SynchNature;
                            everstone = true;
                        }
                    }
                    // it appears to only actually use this for dream world otherwise it's ignored
                    /*uint test = Functions.RNGRange(rngArray[index++ + cnt], 0x64);
                    switch (MotherAbility)
                    {
                        case 0:
                            ability = test > 0x50 ? 1u : 0;
                            break;
                        case 1:
                            ability = test > 0x14 ? 1u : 0;
                            break;
                        case 2:
                            if (test < 0x14) ability = 0;
                            else ability = test >= 0x28 ? 2u : 1;
                            break;
                        default:
                            ability = test > 0x50 ? 1u : 0;
                            break;
                    }
                    // again a worthless calculation
                    if (DittoUsed) ability = Functions.RNGRange(rngArray[index++ + cnt], 2); */
                    dream = Functions.RNGRange(rngList[index++], 0x64) >= 0x28 && !DittoUsed;
                    if (DittoUsed) ++index;

                    // IV Inheritance calc
                    // Uses every two RNG calls, first to determine IV, second to determine which parent
                    // If an IV is repeated it skips two RNG calls and checks the next
                    uint inh = Functions.RNGRange(rngList[index], 6);
                    uint inh1 = inh;
                    uint par1 = rngList[index + 1] >> 31;

                    uint maxSkips = 0;

                    index = index + 2;
                    inh = Functions.RNGRange(rngList[index], 6);
                    while (inh == inh1)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = Functions.RNGRange(rngList[index], 6);

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                                rngList.Add(rng64.GetNext32BitNumber());
                        }
                    }
                    uint inh2 = inh;
                    uint par2 = rngList[index + 1] >> 31;

                    index = index + 2;
                    inh = Functions.RNGRange(rngList[index], 6);
                    while (inh == inh1 || inh == inh2)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = Functions.RNGRange(rngList[index], 6);

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                                rngList.Add(rng64.GetNext32BitNumber());
                        }
                    }
                    uint inh3 = inh;
                    uint par3 = rngList[index + 1] >> 31;

                    uint pid = Functions.RNGRange(rngList[index + 2], 0xFFFFFFFF);

                    if (ParentA != null & ParentB != null && RNGIVs != null)
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                ParentA,
                                ParentB,
                                RNGIVs,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }
                    else
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }

                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }

            else if (frameType == FrameType.BWBredInternational)
            {
                rng64.Seed = InitialSeed;
                rngList = new List<uint>();

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (uint cnt = 0; cnt < InitialFrame + 40; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    frame = null;
                    int index = 1;

                    uint nature;
                    bool dream;
                    bool everstone = false;

                    // for Nidoran\Volbeat\Illumise determination
                    // if 0, get Nidoran-F\Volbeat
                    // if 1, get Nidoran-M\Illumise
                    var speciesSpecial = (int) (rngList[index] >> 31);
                    // Everstone activation calc
                    //int off = MaleOnlySpecies ? 1 : 0;
                    if (MaleOnlySpecies) index++;
                    //index = 1
                    if (SynchNature > -1)
                    {
                        nature = (uint) ((ulong) (rngList[index++])*25 >> 32);
                        if ((rngList[index++] >> 31) == 1)
                        {
                            nature = (uint) SynchNature;
                            everstone = true;
                        }
                        // Dream World ability calc
                        dream = !DittoUsed && (uint) ((ulong) (rngList[index++])*5 >> 32) > 1;
                    }
                    else
                    {
                        nature = (uint) ((ulong) (rngList[index++])*25 >> 32);
                        // Dream World ability calc
                        dream = !DittoUsed && (uint) ((ulong) (rngList[index++])*5 >> 32) > 1;
                    }
                    if (DittoUsed) index = index + 2;

                    // IV Inheritance calc
                    // Uses every two RNG calls, first to determine IV, second to determine which parent
                    // If an IV is repeated it skips two RNG calls and checks the next
                    uint inh = Functions.RNGRange(rngList[index], 6);
                    uint inh1 = inh;
                    uint par1 = rngList[index + 1] >> 31;

                    uint maxSkips = 0;

                    index = index + 2;
                    inh = Functions.RNGRange(rngList[index], 6);
                    while (inh == inh1)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = Functions.RNGRange(rngList[index], 6);

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                                rngList.Add(rng64.GetNext32BitNumber());
                        }
                    }
                    uint inh2 = inh;
                    uint par2 = rngList[index + 1] >> 31;

                    index = index + 2;
                    inh = Functions.RNGRange(rngList[index], 6);
                    while (inh == inh1 || inh == inh2)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = Functions.RNGRange(rngList[index], 6);

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                                rngList.Add(rng64.GetNext32BitNumber());
                        }
                    }
                    uint inh3 = inh;
                    uint par3 = rngList[index + 1] >> 31;

                    uint pid = rngList[index + 2] - 1;
                    const int masuda = 6;
                    for (int n = 0; n < masuda; n++)
                    {
                        pid = rngList[index + n + 2] - 1;
                        uint tid = (id & 0xffff) | ((sid & 0xffff) << 16);

                        uint a = pid ^ tid;
                        uint b = a & 0xffff;
                        uint c = (a >> 16) & 0xffff;
                        uint d = b ^ c;

                        if (d < 8)
                        {
                            break;
                        }
                    }

                    if (ParentA != null & ParentB != null && RNGIVs != null)
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                ParentA,
                                ParentB,
                                RNGIVs,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }
                    else
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }

                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.BW2BredInternational)
            {
                rng64.Seed = InitialSeed;
                uint rngResult;
                List<uint> inhArray = new List<uint>();

                int callCount = 13;
                if (SynchNature > -1)
                    callCount++; // Everstone consumes extra RNG call

                if (DittoUsed)
                    callCount++; // Ditto consumes extra RNG call

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (uint cnt = 0; cnt < InitialFrame + 20; cnt++)
                {
                    rngResult = rng64.GetNext32BitNumber();
                    rngList.Add(rngResult);
                    inhArray.Add((rngResult) * 6 >> 32);
                }

                for (uint cnt = 0; cnt < maxResults; cnt++, rngResult = rng64.GetNext32BitNumber(), rngList.RemoveAt(0), rngList.Add(rngResult), inhArray.RemoveAt(0), inhArray.Add((rngResult) * 6 >> 32))
                {
                    frame = null;
                    int index = 1;

                    uint nature;
                    bool dream;
                    bool everstone = false;

                    // for Nidoran\Volbeat\Illumise determination
                    // if 0, get Nidoran-F\Volbeat
                    // if 1, get Nidoran-M\Illumise
                    var speciesSpecial = (int) (rngList[index] >> 31);
                    // Everstone activation calc
                    //int off = MaleOnlySpecies ? 1 : 0;
                    if (MaleOnlySpecies) index++;
                    //index = 1
                    if (SynchNature > -1)
                    {
                        nature = (uint) ((ulong) (rngList[index++])*25 >> 32);
                        if ((rngList[index++] >> 31) == 1)
                        {
                            nature = (uint) SynchNature;
                            everstone = true;
                        }
                        // Dream World ability calc
                        dream = !DittoUsed && (uint) ((ulong) (rngList[index++])*5 >> 32) > 1;
                    }
                    else
                    {
                        nature = (uint) ((ulong) (rngList[index++])*25 >> 32);
                        // Dream World ability calc
                        dream = !DittoUsed && (uint) ((ulong) (rngList[index++])*5 >> 32) > 1;
                    }

                    // IV Inheritance calc
                    // Uses every two RNG calls, first to determine IV, second to determine which parent
                    // If an IV is repeated it skips two RNG calls and checks the next
                    uint inh = inhArray[index];
                    uint inh1 = inh;
                    uint par1 = rngList[index + 1] >> 31;

                    uint maxSkips = 0;

                    index = index + 2;
                    inh = inhArray[index];
                    while (inh == inh1)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = inhArray[index];

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                            {
                                rngResult = rng64.GetNext32BitNumber();
                                rngList.Add(rngResult);
                                inhArray.Add((rngResult)*6 >> 32);
                            }
                        }
                    }
                    uint inh2 = inh;
                    uint par2 = rngList[index + 1] >> 31;

                    index = index + 2;
                    inh = inhArray[index];
                    while (inh == inh1 || inh == inh2)
                    {
                        maxSkips += 2;
                        index = index + 2;
                        inh = inhArray[index];

                        if (index >= rngList.Count - 3)
                        {
                            for (int refill = 0; refill < 20; refill++)
                            {
                                rngResult = rng64.GetNext32BitNumber();
                                rngList.Add(rngResult);
                                inhArray.Add((rngResult) * 6 >> 32);
                            }
                        }
                    }
                    uint inh3 = inh;
                    uint par3 = rngList[index + 1] >> 31;

                    uint pid = rngList[index + 2] - 1;
                    int masuda = ShinyCharm ? 8 : 6;
                    for (int n = 0; n < masuda; n++)
                    {
                        pid = rngList[index + n + 2] - 1;
                        uint tid = (id & 0xffff) | ((sid & 0xffff) << 16);

                        uint a = pid ^ tid;
                        uint b = a & 0xffff;
                        uint c = (a >> 16) & 0xffff;
                        uint d = b ^ c;

                        if (d < 8)
                        {
                            break;
                        }
                    }

                    if (ParentA != null & ParentB != null && RNGIVs != null)
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                ParentA,
                                ParentB,
                                RNGIVs,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }
                    else
                    {
                        frame =
                            Frame.GenerateFrame(
                                FrameType.BWBred,
                                cnt + InitialFrame,
                                rngList[0],
                                speciesSpecial,
                                inh1,
                                inh2,
                                inh3,
                                par1,
                                par2,
                                par3,
                                pid,
                                id,
                                sid,
                                dream,
                                everstone,
                                nature,
                                maxSkips);
                    }

                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.Wondercard5thGen)
            {
                rng64.Seed = InitialSeed;
                rngList = new List<uint>();

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (int cnt = 0; cnt < 33; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    Frame frame = Frame.GenerateFrame(
                        FrameType.Wondercard5thGen,
                        id, sid,
                        cnt + InitialFrame,
                        rngList[0],
                        rngList[22] >> 27,
                        rngList[23] >> 27,
                        rngList[24] >> 27,
                        rngList[25] >> 27,
                        rngList[26] >> 27,
                        rngList[27] >> 27,
                        rngList[32],
                        rngList[30]);


                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.Wondercard5thGenFixed)
            {
                rng64.Seed = InitialSeed;
                rngList = new List<uint>();

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                {
                    rng64.GetNext64BitNumber();
                }

                for (int cnt = 0; cnt < 36; cnt++)
                {
                    rngList.Add(rng64.GetNext32BitNumber());
                }

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    //note: pid field is unused look into later
                    uint pid = Functions.GenderModPID(rngList[30], rngList[31], 0);
                    Frame frame =
                        Frame.GenerateFrame(
                            FrameType.Wondercard5thGenFixed,
                            id, sid,
                            cnt + InitialFrame,
                            rngList[0],
                            rngList[24] >> 27,
                            rngList[25] >> 27,
                            rngList[26] >> 27,
                            rngList[27] >> 27,
                            rngList[28] >> 27,
                            rngList[29] >> 27,
                            rngList[35],
                            rngList[30]);


                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if (frameType == FrameType.MethodH1 || frameType == FrameType.MethodH2 ||
                     frameType == FrameType.MethodH4)
            {
                //  Instantiate our hunt RNG and fail RNG
                var huntRng = new PokeRng(0);

                //  Instantiate our regular RNG
                var rng1 = new PokeRng((uint) InitialSeed);
                var rng2 = new PokeRng(0);

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng1.Next();

                for (ulong cnt = InitialFrame; cnt < maxResults + InitialFrame; cnt++)
                {
                    //  Get our first RNG call.  If we are using a 
                    //  syncher this will determine whether we are
                    //  going to honor it.
                    uint seed = rng1.Seed;
                    // skip the level and battle calcs
                    rng1.GetNext16BitNumber();

                    //  Basically save our RNG state here
                    //  so that we only advance rng1 once
                    //  per monster.
                    rng2.Seed = rng1.Seed;

                    // if it's safari zone it's one before
                    // note: this should all be done inside the encounter slot calc
                    uint encounterCall = EncounterType == EncounterType.SafariZone ? seed : rng2.Seed;
                    int encounterSlot = EncounterSlotCalc.encounterSlot(encounterCall, frameType, EncounterType);

                    rng2.GetNext16BitNumber();
                    uint firstRng = rng2.GetNext16BitNumber();

                    uint nature = 0;
                    ulong offset = cnt + 3;

                    Compare CuteCheck = x => true;
                    if (EncounterMod != EncounterMod.Synchronize && EncounterMod != EncounterMod.CuteCharm)
                    {
                        //  Dont want to synch at all, just use the 
                        //  RNG call to get our hunt nature,
                        nature = firstRng%25;
                    }
                    else if (EncounterMod == EncounterMod.Synchronize)
                    {
                        //  Check for even/odd on the high order bit.
                        if ((firstRng & 1) == 0)
                        {
                            //  Set the hunt to the nature of our syncher
                            nature = (uint) SynchNature;
                        }
                        else
                        {
                            //  Ok, we wanted to synch, but missed it so lets
                            //  get the next RNG call and then go ahead and
                            //  use that for the nature.
                            nature = rng2.GetNext16BitNumber()%25;
                            offset++;
                        }
                    }
                    else if (EncounterMod == EncounterMod.CuteCharm)
                    {
                        //  Check for even/odd on the high order bit.
                        if ((firstRng%3) > 0)
                        {
                            switch (SynchNature)
                            {
                                case -1:
                                    CuteCheck = x => (x & 0xFF) < 127;
                                    break;
                                case 1:
                                    CuteCheck = x => (x & 0xFF) >= 127;
                                    break;
                                case -2:
                                    CuteCheck = x => (x & 0xFF) < 191;
                                    break;
                                case 2:
                                    CuteCheck = x => (x & 0xFF) >= 63;
                                    break;
                                case -3:
                                    CuteCheck = x => (x & 0xFF) < 63;
                                    break;
                                case 3:
                                    CuteCheck = x => (x & 0xFF) >= 191;
                                    break;
                                case -4:
                                    CuteCheck = x => (x & 0xFF) < 31;
                                    break;
                                case 4:
                                    CuteCheck = x => (x & 0xFF) >= 31;
                                    break;
                                default:
                                    CuteCheck = x => true;
                                    break;
                            }
                        }
                        // either way, eat up a frame for the Cute Charm check and determine nature
                        nature = rng2.GetNext16BitNumber()%25;
                        offset++;
                    }

                    // Seed the hunt rng
                    huntRng.Seed = rng2.Seed;

                    bool found = false;


                    //  Now we are going to have to hunt for the next
                    //  matching method 1 spread that we can display
                    //  to the user.
                    while (!found)
                    {
                        uint pid1 = huntRng.GetNext16BitNumber();
                        uint pid2 = huntRng.GetNext16BitNumber();

                        uint pid = (pid2 << 16) + pid1;

                        if (pid%25 == nature && CuteCheck(pid))
                        {
                            // Wild Pokémon IVs are generally generated by Methods 2 and 4
                            uint iv1;
                            if (frameType == FrameType.MethodH2)
                            {
                                huntRng.GetNext32BitNumber();
                                iv1 = huntRng.GetNext16BitNumber();
                            }
                            else if (frameType == FrameType.MethodH4)
                            {
                                iv1 = huntRng.GetNext16BitNumber();
                                huntRng.GetNext32BitNumber();
                            }
                            else
                            {
                                // Use Method 1, which is what usually happens for stationary Pokémon
                                // Emphasis on "usually".

                                iv1 = huntRng.GetNext16BitNumber();
                            }

                            uint iv2 = huntRng.GetNext16BitNumber();

                            //  We found a match, we need to buid the frame 
                            //  as usual now based on the next few RNG calls
                            frame =
                                Frame.GenerateFrame(
                                    seed,
                                    frameType,
                                    EncounterType,
                                    (uint) cnt,
                                    firstRng,
                                    pid1,
                                    pid2,
                                    iv1,
                                    iv2,
                                    id, sid, (uint) offset,
                                    encounterSlot);


                            if (frameCompare.Compare(frame))
                            {
                                frames.Add(frame);
                            }

                            found = true;
                        }

                        offset += 2;
                    }
                }
            }
            else if (frameType == FrameType.MethodJ)
            {
                //  Instantiate our hunt RNG and fail RNG
                var huntRng = new PokeRng(0);

                //  Instantiate our regular RNG
                var rng1 = new PokeRng((uint) InitialSeed);
                var rng2 = new PokeRng(0);

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng1.Next();

                for (uint cnt = InitialFrame; cnt < maxResults + InitialFrame; cnt++)
                {
                    //  Get our first RNG call.  If we are using a 
                    //  syncher this will determine whether we are
                    //  going to honor it.
                    uint seed = rng1.Seed;
                    uint firstRng = rng1.GetNext16BitNumber();
                    uint rngResult = firstRng;

                    //  Basically save our RNG state here
                    //  so that we only advance rng1 once
                    //  per monster.
                    rng2.Seed = rng1.Seed;

                    uint offset = cnt + 1;
                    int encounterSlot = 0;

                    if (EncounterType == EncounterType.Wild)
                    {
                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.Seed, frameType, EncounterType);
                        firstRng = rng2.GetNext16BitNumber();
                        offset++;
                    }
                    else if (EncounterType == EncounterType.WildSurfing)
                    {
                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.Seed, frameType, EncounterType);
                        firstRng = rng2.GetNext16BitNumber();
                        firstRng = rng2.GetNext16BitNumber();
                        offset += 2;
                    }
                    else if (EncounterType == EncounterType.WildOldRod || EncounterType == EncounterType.WildGoodRod ||
                             EncounterType == EncounterType.WildSuperRod)
                    {
                        uint threshold;
                        if (EncounterType == EncounterType.WildOldRod)
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 48u : 24u;
                        }
                        else if (EncounterType == EncounterType.WildGoodRod)
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 98u : 49u;
                        }
                        else
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 100u : 74u;
                        }

                        // Skip this call if it an encounter cannot be made
                        if (firstRng/656 > threshold)
                            continue;

                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.GetNext32BitNumber(), frameType,
                                                                        EncounterType);

                        // one of these is item calc, another is level
                        firstRng = rng2.GetNext16BitNumber();
                        firstRng = rng2.GetNext16BitNumber();

                        offset += 3;
                    }

                    uint nature;
                    if (EncounterMod == EncounterMod.Synchronize)
                    {
                        //  Check for even/odd on the high order bit.
                        if (firstRng >> 15 == 0)
                        {
                            //  Set the hunt to the nature our our syncher
                            nature = (uint) SynchNature;
                        }
                        else
                        {
                            //  Ok, we wanted to synch, but missed it so lets
                            //  get the next RNG call and then go ahead and
                            //  use that for the nature.
                            nature = rng2.GetNext16BitNumber()/0xA3E;
                            offset++;
                        }
                    }
                    else if (EncounterMod == EncounterMod.CuteCharm)
                    {
                        nature = rng2.GetNext16BitNumber() / 0xA3E;
                    }
                    else
                    {
                        //  Dont want to synch at all, just use the 
                        //  RNG call to get our hunt nature,
                        nature = firstRng/0xA3E;
                    }

                    if (EncounterMod == EncounterMod.CuteCharm && firstRng/0x5556 != 0)
                    {
                        uint buffer = 0;
                        if (SynchNature > -1)
                        {
                            switch (SynchNature)
                            {
                                case 1:
                                    buffer = 0x96;
                                    break;
                                case 2:
                                    buffer = 0x4B;
                                    break;
                                case 3:
                                    buffer = 0xC8;
                                    break;
                                case 4:
                                    buffer = 0x32;
                                    break;
                                default:
                                    buffer = 0;
                                    break;
                            }
                        }

                        //  We found a match, we need to buid the frame 
                        //  as usual now based on the next few RNG calls
                        frame =
                            Frame.GenerateFrame(
                                seed,
                                FrameType.MethodJ,
                                EncounterType,
                                cnt,
                                rngResult,
                                nature + buffer,
                                0,
                                rng2.GetNext16BitNumber(),
                                rng2.GetNext16BitNumber(),
                                id, sid, offset - 2,
                                encounterSlot);

                        if (frameCompare.Compare(frame))
                        {
                            frames.Add(frame);
                        }
                    }
                    else
                    {
                        // Seed the hunt rng
                        huntRng.Seed = rng2.Seed;

                        bool found = false;
                        //  Now we are going to have to hunt for the next
                        //  matching method 1 spread that we can display
                        //  to the user.
                        while (!found)
                        {
                            uint pid1 = huntRng.GetNext16BitNumber();
                            uint pid2 = huntRng.GetNext16BitNumber();

                            uint pid = (pid2 << 16) | pid1;

                            if (pid%25 == nature)
                            {
                                //  We found a match, we need to buid the frame 
                                //  as usual now based on the next few RNG calls
                                frame =
                                    Frame.GenerateFrame(
                                        seed,
                                        FrameType.MethodJ,
                                        EncounterType,
                                        cnt,
                                        rngResult,
                                        pid1,
                                        pid2,
                                        huntRng.GetNext16BitNumber(),
                                        huntRng.GetNext16BitNumber(),
                                        id, sid, offset,
                                        encounterSlot);

                                if (frameCompare.Compare(frame))
                                {
                                    frames.Add(frame);
                                }

                                found = true;
                            }

                            offset += 2;
                        }
                    }
                }
            }
            else if (frameType == FrameType.MethodK)
            {
                //  Instantiate our hunt RNG and fail RNG
                var huntRng = new PokeRng(0);

                //  Instantiate our regular RNG
                var rng1 = new PokeRng((uint) InitialSeed);
                var rng2 = new PokeRng(0);

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                {
                    rng1.Next();
                }

                for (uint cnt = InitialFrame; cnt < maxResults + InitialFrame; cnt++)
                {
                    //  Get our first RNG call.  If we are using a 
                    //  syncher this will determine whether we are
                    //  going to honor it.
                    uint seed = rng1.Seed;
                    uint firstRng = rng1.GetNext16BitNumber();
                    uint rngResult = firstRng;

                    //  Basically save our RNG state here
                    //  so that we only advance rng1 once
                    //  per monster.
                    rng2.Seed = rng1.Seed;

                    uint offset = cnt + 1;
                    int encounterSlot = 0;

                    if (EncounterType == EncounterType.Wild || EncounterType == EncounterType.SafariZone)
                    {
                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.Seed, frameType, EncounterType);
                        firstRng = rng2.GetNext16BitNumber();
                        offset++;
                    }
                    else if (EncounterType == EncounterType.WildSurfing || EncounterType == EncounterType.Headbutt ||
                             EncounterType == EncounterType.BugCatchingContest)
                    {
                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.Seed, frameType, EncounterType);
                        firstRng = rng2.GetNext16BitNumber();
                        firstRng = rng2.GetNext16BitNumber();
                        offset += 2;
                    }
                    else if (EncounterType == EncounterType.WildOldRod ||
                             EncounterType == EncounterType.WildGoodRod ||
                             EncounterType == EncounterType.WildSuperRod)
                    {
                        uint threshold;
                        if (EncounterType == EncounterType.WildOldRod)
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 48u : 24u;
                        }
                        else if (EncounterType == EncounterType.WildGoodRod)
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 98u : 49u;
                        }
                        else
                        {
                            threshold = EncounterMod == EncounterMod.SuctionCups ? 100u : 74u;
                        }

                        // Skip this call if it an encounter cannot be made
                        if (firstRng%100 > threshold)
                            continue;

                        encounterSlot = EncounterSlotCalc.encounterSlot(rng2.GetNext32BitNumber(), frameType,
                                                                        EncounterType);

                        // one of these is item calc, another is level
                        firstRng = rng2.GetNext16BitNumber();
                        firstRng = rng2.GetNext16BitNumber();

                        offset += 3;
                    }

                    uint nature;
                    if (EncounterMod == EncounterMod.Synchronize)
                    {
                        //  Check for even/odd on the low order bit
                        if ((firstRng & 1) == 0)
                        {
                            //  Set the hunt to the nature our our syncher
                            nature = (uint) SynchNature;
                        }
                        else
                        {
                            //  Ok, we wanted to synch, but missed it so lets
                            //  get the next RNG call and then go ahead and
                            //  use that for the nature.
                            nature = rng2.GetNext16BitNumber() % 25;
                            offset++;
                        }
                    }
                    else if (EncounterMod == EncounterMod.CuteCharm)
                    {
                        nature = rng2.GetNext16BitNumber() % 25;
                    }
                    else
                    {
                        //  Dont want to synch at all, just use the 
                        //  RNG call to get our hunt nature,
                        nature = firstRng%25;
                    }

                    if (EncounterMod == EncounterMod.CuteCharm && firstRng%3 != 0)
                    {
                        uint buffer = 0;
                        if (SynchNature > -1)
                        {
                            switch (SynchNature)
                            {
                                case 1:
                                    buffer = 0x96;
                                    break;
                                case 2:
                                    buffer = 0x4B;
                                    break;
                                case 3:
                                    buffer = 0xC8;
                                    break;
                                case 4:
                                    buffer = 0x32;
                                    break;
                                default:
                                    buffer = 0;
                                    break;
                            }
                        }

                        //  We found a match, we need to buid the frame 
                        //  as usual now based on the next few RNG calls
                        frame =
                            Frame.GenerateFrame(
                                seed,
                                FrameType.MethodK,
                                EncounterType,
                                cnt,
                                rngResult,
                                nature + buffer,
                                0,
                                rng2.GetNext16BitNumber(),
                                rng2.GetNext16BitNumber(),
                                id, sid, offset - 2,
                                encounterSlot);

                        if (EncounterType == EncounterType.BugCatchingContest ||
                            EncounterType == EncounterType.SafariZone)
                            frame.Synchable = true;

                        if (frameCompare.Compare(frame))
                        {
                            frames.Add(frame);
                        }
                    }
                    else
                    {
                        // Seed the hunt rng
                        huntRng.Seed = rng2.Seed;

                        bool found = false;
                        //  Now we are going to have to hunt for the next
                        //  matching method 1 spread that we can display
                        //  to the user.
                        while (!found)
                        {
                            uint pid1 = huntRng.GetNext16BitNumber();
                            uint pid2 = huntRng.GetNext16BitNumber();

                            uint pid = (pid2 << 16) | pid1;

                            if (pid%25 == nature)
                            {
                                //  We found a match, we need to buid the frame 
                                //  as usual now based on the next few RNG calls
                                frame =
                                    Frame.GenerateFrame(
                                        seed,
                                        FrameType.MethodK,
                                        EncounterType,
                                        cnt,
                                        rngResult,
                                        pid1,
                                        pid2,
                                        huntRng.GetNext16BitNumber(),
                                        huntRng.GetNext16BitNumber(),
                                        id, sid, offset,
                                        encounterSlot);

                                if (frameCompare.Compare(frame))
                                {
                                    frames.Add(frame);
                                }

                                found = true;
                            }
                            offset += 2;
                        }
                    }
                }
            }
            else if (frameType == FrameType.ColoXD)
            {
                var rng = new XdRng((uint) InitialSeed);
                rngList = new List<uint>();

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng.GetNext32BitNumber();

                for (uint cnt = 0; cnt < 5; cnt++)
                    rngList.Add(rng.GetNext16BitNumber());

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng.GetNext16BitNumber()))
                {
                    frame = Frame.GenerateFrame(
                        0,
                        FrameType.ColoXD,
                        cnt + InitialFrame,
                        rngList[0],
                        rngList[4],
                        rngList[3],
                        rngList[0],
                        rngList[1],
                        id, sid);


                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else if(frameType == FrameType.Channel)
            {
                var rng = new XdRng((uint)InitialSeed);
                rngList = new List<uint>();

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng.GetNext32BitNumber();

                for (uint cnt = 0; cnt < 12; cnt++)
                    rngList.Add(rng.GetNext16BitNumber());

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng.GetNext16BitNumber()))
                {
                    frame = Frame.GenerateChannel(
                        0,
                        FrameType.Channel,
                        cnt + InitialFrame,
                        rngList[0],
                        rngList[1],
                        rngList[2],
                        (rngList[6]) >> 11,
                        (rngList[7]) >> 11,
                        (rngList[8]) >> 11,
                        (rngList[10]) >> 11,
                        (rngList[11]) >> 11,
                        (rngList[9]) >> 11,
                        40122, rngList[0]);


                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }
                }
            }
            else
            {
                //  We are going to grab our initial set of rngs here and
                //  then start our loop so that we can iterate as many 
                //  times as we have to.
                var rng = new PokeRng((uint) InitialSeed);
                rngList = new List<uint>();

                for (uint cnt = 1; cnt < InitialFrame; cnt++)
                    rng.GetNext32BitNumber();

                for (uint cnt = 0; cnt < 20; cnt++)
                    rngList.Add(rng.GetNext16BitNumber());

                lastseed = rng.Seed;

                for (uint cnt = 0; cnt < maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng.GetNext16BitNumber()))
                {
                    Frame frameSplit = null;

                    switch (frameType)
                    {
                        case FrameType.Method1:
                            frame =
                                Frame.GenerateFrame(
                                    0,
                                    FrameType.Method1,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[1],
                                    rngList[2],
                                    rngList[3],
                                    0, 0, 0, 0, 0, 0,
                                    id, sid, cnt);

                            break;

                        case FrameType.Method1Reverse:
                            frame =
                                Frame.GenerateFrame(
                                    0,
                                    FrameType.Method1,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[1],
                                    rngList[0],
                                    rngList[2],
                                    rngList[3],
                                    0, 0, 0, 0, 0, 0,
                                    id, sid, cnt);

                            break;

                        case FrameType.Method2:
                            frame =
                                Frame.GenerateFrame(
                                    0,
                                    FrameType.Method2,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[1],
                                    rngList[3],
                                    rngList[4],
                                    0, 0, 0, 0, 0, 0,
                                    id, sid, cnt);

                            break;

                        case FrameType.Method3:
                            frame =
                                Frame.GenerateFrame(
                                    0,
                                    FrameType.Method3,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[2],
                                    rngList[3],
                                    rngList[4],
                                    0, 0, 0, 0, 0, 0,
                                    id, sid, cnt);

                            break;

                        case FrameType.Method4:
                            frame =
                                Frame.GenerateFrame(
                                    0,
                                    FrameType.Method4,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[1],
                                    rngList[2],
                                    rngList[4],
                                    0, 0, 0, 0, 0, 0,
                                    id, sid, cnt);

                            break;

                        case FrameType.ChainedShiny:
                            uint chainedPIDLower = Functions.ChainedPIDLower(
                                rngList[1],
                                rngList[15],
                                rngList[14],
                                rngList[13],
                                rngList[12],
                                rngList[11],
                                rngList[10],
                                rngList[9],
                                rngList[8],
                                rngList[7],
                                rngList[6],
                                rngList[5],
                                rngList[4],
                                rngList[3]);

                            frame = Frame.GenerateFrame(
                                0,
                                FrameType.ChainedShiny,
                                cnt + InitialFrame,
                                rngList[0],
                                chainedPIDLower,
                                Functions.ChainedPIDUpper(rngList[2], chainedPIDLower, id, sid),
                                rngList[16],
                                rngList[17],
                                0, 0, 0, 0, 0, 0,
                                id, sid, cnt);
                            break;
                        case FrameType.EBredPID:
                            uint pid = GetEPid(cnt, out uint total);
                            if (pid == 0)
                            {
                                continue;
                            }
                            //generate frame with bogus RNG result
                            frame = Frame.GenerateFrame(FrameType.EBredPID, cnt + InitialFrame, 0, pid, id, sid);
                            frame.Advances = total;
                            //new Frame {FrameType = FrameType.EBredPID, Number = cnt + InitialFrame, Pid = pid};
                            break;

                        case FrameType.RSBredLower:
                            frame =
                                Frame.GenerateFrame(
                                    FrameType.RSBredLower,
                                    cnt + InitialFrame,
                                    rngList[18],
                                    rngList[19],
                                    Compatibility
                                    );
                            break;

                        case FrameType.RSBredUpper:
                            frame =
                                Frame.GenerateFrame(
                                    FrameType.RSBredUpper,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[3],
                                    rngList[5],
                                    rngList[6],
                                    // vblank
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    rngList[13],
                                    ParentA,
                                    ParentB,
                                    id, sid);
                            break;

                        case FrameType.FRLGBredLower:
                            frame =
                                Frame.GenerateFrame(
                                    FrameType.FRLGBredLower,
                                    cnt + InitialFrame,
                                    rngList[18],
                                    rngList[19],
                                    Compatibility
                                    );
                            break;

                        case FrameType.FRLGBredUpper:
                            frame =
                                Frame.GenerateFrame(
                                    FrameType.FRLGBredUpper,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[3],
                                    rngList[5],
                                    rngList[6],
                                    // vblank
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    rngList[13],
                                    ParentA,
                                    ParentB,
                                    id, sid);
                            break;
                        case FrameType.Bred:

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.Bred,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[4],
                                    rngList[5],
                                    rngList[7],
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            break;

                        case FrameType.BredSplit:
                            //  This is going to add both of the frames and 
                            //  the logic below will decide whether to add 
                            //  it to the output.

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.Bred,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[4],
                                    rngList[5],
                                    //  Garbage
                                    rngList[7],
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            frameSplit =
                                Frame.GenerateFrame(
                                    FrameType.BredSplit,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[4],
                                    rngList[6],
                                    //  Garbage
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    rngList[13],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            break;

                        case FrameType.BredAlternate:

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.Bred,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    StaticPID,
                                    rngList[4],
                                    rngList[5],
                                    rngList[8],
                                    rngList[9],
                                    rngList[10],
                                    rngList[11],
                                    rngList[12],
                                    rngList[13],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            break;

                        case FrameType.DPPtBred:

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.DPPtBred,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[1],
                                    rngList[2],
                                    rngList[3],
                                    rngList[4],
                                    rngList[5],
                                    rngList[6],
                                    rngList[7],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            break;

                        case FrameType.HGSSBred:

                            frame =
                                Frame.GenerateFrame(
                                    FrameType.HGSSBred,
                                    cnt + InitialFrame,
                                    rngList[0],
                                    rngList[0],
                                    rngList[1],
                                    rngList[2],
                                    rngList[3],
                                    rngList[4],
                                    rngList[5],
                                    rngList[6],
                                    rngList[7],
                                    ParentA,
                                    ParentB,
                                    id, sid, cnt);

                            break;

                        case FrameType.WondercardIVs:

                            if (EncounterType == EncounterType.Manaphy)
                            {
                                uint pid1 = rngList[0];
                                uint pid2 = rngList[1];

                                while ((pid1 ^ pid2 ^ id ^ sid) < 8)
                                {
                                    uint testPID = pid1 | (pid2 << 16);

                                    // Call the ARNG to change the PID
                                    testPID = testPID*0x6c078965 + 1;

                                    pid1 = testPID & 0xFFFF;
                                    pid2 = testPID >> 16;
                                }

                                frame =
                                    Frame.GenerateFrame(
                                        0,
                                        FrameType.WondercardIVs,
                                        cnt + InitialFrame,
                                        rngList[0],
                                        pid1,
                                        pid2,
                                        rngList[2],
                                        rngList[3],
                                        0, 0, 0, 0, 0, 0,
                                        id, sid, cnt);
                            }
                            else
                            {
                                frame =
                                    Frame.GenerateFrame(
                                        0,
                                        FrameType.WondercardIVs,
                                        cnt + InitialFrame,
                                        rngList[0],
                                        0,
                                        0,
                                        rngList[0],
                                        rngList[1],
                                        rngList[2],
                                        rngList[3],
                                        rngList[4],
                                        rngList[5],
                                        rngList[6],
                                        rngList[7],
                                        id, sid, cnt);
                            }

                            break;
                    }


                    //  Now we need to filter and decide if we are going
                    //  to add this to our collection for display to the
                    //  user.

                    if (frameCompare.Compare(frame))
                    {
                        frames.Add(frame);
                    }

                    if (frameType == FrameType.BredSplit)
                    {
                        if (frameCompare.Compare(frameSplit))
                        {
                            frames.Add(frameSplit);
                        }
                    }
                }
            }

            return frames;
        }

        private uint GetEPid(uint cnt, out uint total)
        {
            total = 0;
            int i = 0;
            uint pid;
            // check for compatibility
            if ((rngList[i++]*100)/0xFFFF >= Compatibility) return 0;

            //check the everstone
            bool useEverstone = Everstone ? (rngList[i++] >> 15) == 0 : false;

            // set up the TRNG
            var trng = new PokeRng((cnt + InitialFrame - Calibration) & 0xFFFF);

            if (!useEverstone)
            {
                // generate lower
                if (rngList[i] > 0xFFFD)
                    pid = (rngList[i] + 3)%0xFFFF;
                else
                    pid = (rngList[i] & 0xFFFF) + 1;

                // generate upper
                pid += trng.GetNext16BitNumber()*0x10000;

                return pid;
            }

            do
            {
                //always appears to vblank at 17
                if (total == 17)
                    ++i;

                // check if we need to add to the rngArray
                // if we do add another 20 elements
                if (i >= rngList.Count)
                    AddToRngList();

                // generate lower
                pid = (rngList[i++] & 0xFFFF);

                // generate upper
                pid += trng.GetNext16BitNumber()*0x10000;
                ++total;
            } while (pid%0x19 != SynchNature);

            return pid;
        }

        private void AddToRngList()
        {
            int i = rngList.Count;

            // seed the new RNG with the last seed
            var rng = new PokeRng(lastseed);
            // add in the new elements
            for (; i < rngList.Count + 20; ++i)
                rngList.Add(rng.GetNext16BitNumber());

            lastseed = rng.Seed;
        }

        public List<Frame> GenerateWonderCard(
            FrameCompare frameCompare,
            uint id,
            uint sid,
            int shiny)
        {
            frames = new List<Frame>();

            if (frameType == FrameType.Wondercard5thGen)
            {
                rng64.Seed = InitialSeed;

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (int cnt = 0; cnt < 33; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                for (uint cnt = InitialFrame; cnt < InitialFrame + maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    uint pid = rngList[30];
                    switch (shiny)
                    {
                        case 0:
                            pid = ForceNonShiny(pid, id, sid);
                            break;
                        case 2:
                            pid = ForceShiny(pid, id, sid);
                            break;
                    }

                    Frame frame = Frame.GenerateFrame(
                        FrameType.Wondercard5thGen,
                        id, sid,
                        cnt,
                        rngList[0],
                        rngList[22] >> 27,
                        rngList[23] >> 27,
                        rngList[24] >> 27,
                        rngList[25] >> 27,
                        rngList[26] >> 27,
                        rngList[27] >> 27,
                        rngList[32],
                        pid);


                    if (frameCompare.Compare(frame))
                        frames.Add(frame);
                }
            }
            else if (frameType == FrameType.Wondercard5thGenFixed)
            {
                rng64.Seed = InitialSeed;

                rngList = new List<uint>();

                for (uint cnt = 0; cnt < InitialFrame - 1; cnt++)
                    rng64.GetNext64BitNumber();

                for (int cnt = 0; cnt < 36; cnt++)
                    rngList.Add(rng64.GetNext32BitNumber());

                for (uint cnt = InitialFrame; cnt < InitialFrame + maxResults; cnt++, rngList.RemoveAt(0), rngList.Add(rng64.GetNext32BitNumber()))
                {
                    uint pid = Functions.GenderModPID(rngList[30], rngList[31], 0);
                    switch (shiny)
                    {
                        case 0:
                            pid = ForceNonShiny(pid, id, sid);
                            break;
                        case 2:
                            pid = ForceShiny(pid, id, sid);
                            break;
                    }

                    frame = Frame.GenerateFrame(
                        FrameType.Wondercard5thGenFixed,
                        id, sid,
                        cnt,
                        rngList[0],
                        rngList[24] >> 27,
                        rngList[25] >> 27,
                        rngList[26] >> 27,
                        rngList[27] >> 27,
                        rngList[28] >> 27,
                        rngList[29] >> 27,
                        rngList[35],
                        pid);

                    if (frameCompare.Compare(frame))
                        frames.Add(frame);
                }
            }
            return frames;
        }

        private static uint ForceShiny(uint pid, uint tid, uint sid)
        {
            uint lowByte = pid & 0x000000ff;
            return ((lowByte ^ tid ^ sid) << 16) | lowByte;
        }

        private static uint ForceNonShiny(uint pid, uint tid, uint sid)
        {
            if (((pid >> 16) ^ (pid & 0xffff) ^ sid ^ tid) < 8)
                pid = pid ^ 0x10000000;

            return pid;
        }

        #region Nested type: Compare

        protected delegate bool Compare(uint x);

        #endregion
    }

    // The C-Gear advances at a rate determined by each successive RNG call
    internal class CGearTimer
    {
        private uint currentTime;
        private State timerState;

        public CGearTimer()
        {
            timerState = State.First;
        }

        public uint GetTime(uint rngResult)
        {
            switch (timerState)
            {
                case State.First:
                    timerState = State.Skip;
                    currentTime += 21;
                    return currentTime;
                case State.Long:
                    timerState = State.Short;
                    currentTime = currentTime + (uint) (((ulong) rngResult*152) >> 32) + 60;
                    return currentTime;
                case State.Short:
                    timerState = State.Skip;
                    currentTime = currentTime + (uint) (((ulong) rngResult*40) >> 32) + 60;
                    return currentTime;
                case State.Skip:
                    timerState = State.Long;
                    return 0;
            }

            return 0;
        }

        #region Nested type: State

        private enum State
        {
            First,
            Long,
            Skip,
            Short
        }

        #endregion
    }

    // Maybe someday we'll do something with this quick all-shiny search
    // But for now, other methods are more practical
    /*
    public List<Frame> Generate(
        FrameCompare frameCompare,
        uint minEfgh,
        uint maxEfgh,
        uint id,
        uint sid)
    {
        List<Frame> frames = new List<Frame>();
        List<Frame> candidates = new List<Frame>();

        uint minNature = 0;
        uint maxNature = 24;
        if (frameCompare.Nature != -1)
        {
            minNature = (uint)frameCompare.Nature;
            maxNature = (uint)frameCompare.Nature;
        }

        PokeRng rng = new PokeRng(0);
        PokeRngR rngR = new PokeRngR(0);

        // We XOR the ID and SID se we have a pattern
        // for shininess.  Given the u16 of a PID we can find any
        // corresponding lower 16-bits such that the Pokemon
        // will be shiny.
        uint shinyPattern = (id ^ sid) & 0xFFF7;

        // There are 8 possibilities for the lower 3 bits
        // so we need to test all 64 uPID-lPID combinations
        uint basePID1;
        uint basePID2;
        uint pid1;
        uint pid2;

        for (uint pidcnt = 0x0; pidcnt < 0xFF; pidcnt++)
        {
            basePID1 = pidcnt << 3;

            for (uint i = 0; i < 8; i++)
            {
                pid1 = basePID1 | i;
                basePID2 = pid1 ^ shinyPattern;
                for (uint j = 0; j < 8; j++)
                {
                    pid2 = basePID2 | j;

                    uint baseSeed = pid1 << 16;
                    for (uint cnt = 0; cnt < 0xFFFF; cnt++)
                    {
                        uint seed = baseSeed | cnt;
                        rng.Seed = seed;

                        uint rng1 = rng.GetNext16BitNumber();
                        if (rng1 == pid2)
                        {
                            rngR.Seed = seed;
                            Frame frame = Frame.GenerateFrame(rngR.GetNext32BitNumber(), FrameType.Method1,
                                                                1, seed,
                                                                pid1, pid2,
                                                                rng.GetNext16BitNumber(), rng.GetNext16BitNumber(),
                                                                id, sid);

                            if (frameCompare.Compare(frame))
                                frames.Add(frame);
                        }
                    }
                }
            }
        }

        return frames;
    }
     */
}