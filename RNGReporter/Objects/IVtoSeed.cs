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
    /// <summary>
    ///     This class is going to do an IV/PID/Seed calculation given a particular method (1, 2 or 3, or 4). Should use the same code to develop candidate IVs.
    /// </summary>
    internal class IVtoSeed
    {
        //  We need a function to return a list of monster seeds,
        //  which will be updated to include a method.  

        public static List<Seed> GetXDSeeds(
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            uint nature,
            uint id)
        {
            var seeds = new List<Seed>();

            uint x8 = hp + (atk << 5) + (def << 10);
            uint x8_2 = x8 ^ 0x8000;

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0; cnt <= 0xFFFE; cnt++)
            {
                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                uint x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_testXD << 16) + cnt;

                var rngXD = new XdRng(seed);
                var rngXDR = new XdRngR(seed);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                //  Check Colosseum\XD
                // [IVs] [IVs] [xxx] [PID] [PID]
                // [START] [rng1] [rng3] [rng4]

                if (Check(rng1XD, rng3XD, rng4XD, spe, spa, spd, nature))
                {
                    var newSeed = new Seed
                        {
                            Method = "Colosseum/XD",
                            Pid = ((uint) rng3XD << 16) + rng4XD,
                            MonsterSeed = XDColoSeed
                        };


                    seeds.Add(newSeed);
                }
            }

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0xFFFF; cnt <= 0x1FFFE; cnt++)
            {
                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                uint x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_testXD << 16) + ((cnt + 1) & 0xFFFF);

                var rngXD = new XdRng(seed);
                var rngXDR = new XdRngR(seed);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                //  Check Colosseum\XD
                // [IVs] [IVs] [xxx] [PID] [PID]
                // [START] [rng1] [rng3] [rng4]

                if (Check(rng1XD, rng3XD, rng4XD, spe, spa, spd, nature))
                {
                    var newSeed = new Seed
                    {
                        Method = "Colosseum/XD",
                        Pid = ((uint)rng3XD << 16) + rng4XD,
                        MonsterSeed = XDColoSeed
                    };


                    seeds.Add(newSeed);
                }
            }
            return seeds;
        }

        public static List<Seed> GetSeeds(
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            uint nature,
            uint id)
        {
            var seeds = new List<Seed>();

            uint x4 = spe + (spa << 5) + (spd << 10);
            uint x4_2 = x4 ^ 0x8000;

            uint x8 = hp + (atk << 5) + (def << 10);
            uint x8_2 = x8 ^ 0x8000;

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0; cnt <= 0xFFFE; cnt++)
            {
                uint x_test;
                uint x_testXD;

                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                x_test = (cnt & 1) == 0 ? x4 : x4_2;

                x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_test << 16) + cnt;
                uint seedXD = (x_testXD << 16) + cnt;
                var rng = new PokeRngR(seed);

                var rngXD = new XdRng(seedXD);
                var rngXDR = new XdRngR(seedXD);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1 = rng.GetNext16BitNumber();
                ushort rng2 = rng.GetNext16BitNumber();
                ushort rng3 = rng.GetNext16BitNumber();
                ushort rng4 = rng.GetNext16BitNumber();

                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                uint method1Seed = rng.Seed;

                rng.GetNext16BitNumber();
                uint method234Seed = rng.Seed;
                ushort choppedPID;

                //  Check Method 1
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng3, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed();
                    newSeed.Method = "Method 1";
                    newSeed.Pid = ((uint) rng2 << 16) + rng3;
                    newSeed.MonsterSeed = method1Seed;
                    newSeed.Sid = (rng2 ^ (uint) rng3 ^ id) & 0xFFF8;

                    seeds.Add(newSeed);
                }

                //  Check Reverse Method 1
                // [PID] [PID] [IVs] [IVs]
                // [rng2] [rng3] [rng1] [START]
                if (Check(rng1, rng3, rng2, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                    {
                        Method = "Reverse Method 1",
                        Pid = ((uint)rng3 << 16) + rng2,
                        MonsterSeed = method1Seed,
                        Sid = (rng2 ^ (uint)rng3 ^ id) & 0xFFF8
                    };

                    seeds.Add(newSeed);
                }

                //  Check Wishmkr
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng3, rng2, hp, atk, def, nature))
                {
                    if (method1Seed < 0x10000)
                    {
                        //  Build a seed to add to our collection
                        var newSeed = new Seed
                        {
                            Pid = ((uint)rng3 << 16) + rng2,
                            MonsterSeed = method1Seed,
                            Sid = (rng2 ^ (uint)rng3 ^ id) & 0xFFF8
                        };
                        if (Functions.Shiny(newSeed.Pid, 20043, 0))
                            newSeed.Method = "Wishmkr Shiny"; 
                        else
                            newSeed.Method = "Wishmkr";

                        seeds.Add(newSeed);
                    }
                }

                //  Check Method 2
                // [PID] [PID] [xxxx] [IVs] [IVs]
                // [rng4] [rng3] [xxxx] [rng1] [START]
                if (Check(rng1, rng3, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                        {
                            Method = "Method 2",
                            Pid = ((uint) rng3 << 16) + rng4,
                            MonsterSeed = method234Seed,
                            Sid = (rng3 ^ (uint) rng4 ^ id) & 0xFFF8
                        };

                    seeds.Add(newSeed);
                }

                /*
                 * Removed because Method 3 doesn't exist in-game
                //  Check Method 3
                //  [PID] [xxxx] [PID] [IVs] [IVs]
                //  [rng4] [xxxx] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    Seed newSeed = new Seed();
                    newSeed.Method = "Method 3";
                    newSeed.Pid = ((uint)rng2 << 16) + (uint)rng4;
                    newSeed.MonsterSeed = method234Seed;
                    newSeed.Sid = ((uint)rng2 ^ (uint)rng4 ^ id) & 0xFFF8;

                    seeds.Add(newSeed);
                }
                 */

                //  Check Method 4
                //  [PID] [PID] [IVs] [xxxx] [IVs]
                //  [rng4] [rng3] [rng2] [xxxx] [START]
                if (Check(rng2, rng3, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                        {
                            Method = "Method 4",
                            Pid = ((uint) rng3 << 16) + rng4,
                            MonsterSeed = method234Seed,
                            Sid = (rng3 ^ (uint) rng4 ^ id) & 0xFFF8
                        };

                    seeds.Add(newSeed);
                }

                //  Check Colosseum\XD
                // [IVs] [IVs] [xxx] [PID] [PID]
                // [START] [rng1] [rng3]

                if (Check(rng1XD, rng3XD, rng4XD, spe, spa, spd, nature))
                {
                    var newSeed = new Seed
                        {
                            Method = "Colosseum/XD",
                            Pid = ((uint) rng3XD << 16) + rng4XD,
                            MonsterSeed = XDColoSeed,
                            Sid = (rng4XD ^ (uint) rng3XD ^ id) & 0xFFF8
                        };


                    seeds.Add(newSeed);
                }

                if (rng3/0x5556 != 0)
                {
                    //  Check DPPt Cute Charm
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2/0xA3E);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (DPPt)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (50% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2/0xA3E + 0x96);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (DPPt)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (25% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2/0xA3E + 0xC8);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (DPPt)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (75% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2/0xA3E + 0x4B);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (DPPt)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (87.5% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2/0xA3E + 0x32);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (DPPt)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }
                }

                if (rng3%3 != 0)
                {
                    //  Check HGSS Cute Charm
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2%25);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (HGSS)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (50% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2%25 + 0x96);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (HGSS)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (25% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2%25 + 0xC8);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (HGSS)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (75% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2%25 + 0x4B);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (HGSS)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (87.5% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort) (rng2%25 + 0x32);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                            {
                                Method = "Cute Charm (HGSS)",
                                Pid = choppedPID,
                                MonsterSeed = method1Seed,
                                Sid = (choppedPID ^ id) & 0xFFF8
                            };


                        seeds.Add(newSeed);
                    }
                }
            }

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0xFFFF; cnt <= 0x1FFFE; cnt++)
            {
                uint x_test;
                uint x_testXD;

                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                x_test = (cnt & 1) == 0 ? x4 : x4_2;

                x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_test << 16) + ((cnt + 1) & 0xFFFF);
                uint seedXD = (x_testXD << 16) + ((cnt + 1) & 0xFFFF);
                
                var rng = new PokeRngR(seed);

                var rngXD = new XdRng(seedXD);
                var rngXDR = new XdRngR(seedXD);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1 = rng.GetNext16BitNumber();
                ushort rng2 = rng.GetNext16BitNumber();
                ushort rng3 = rng.GetNext16BitNumber();
                ushort rng4 = rng.GetNext16BitNumber();

                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                uint method1Seed = rng.Seed;

                rng.GetNext16BitNumber();
                uint method234Seed = rng.Seed;
                ushort choppedPID;

                //  Check Method 1
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng3, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed();
                    newSeed.Method = "Method 1";
                    newSeed.Pid = ((uint)rng2 << 16) + rng3;
                    newSeed.MonsterSeed = method1Seed;
                    newSeed.Sid = (rng2 ^ (uint)rng3 ^ id) & 0xFFF8;

                    seeds.Add(newSeed);
                }

                //  Check Wishmkr
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng3, rng2, hp, atk, def, nature))
                {
                    if (method1Seed < 0x10000)
                    {
                        //  Build a seed to add to our collection
                        var newSeed = new Seed();
                        newSeed.Pid = ((uint)rng3 << 16) + rng2;
                        newSeed.MonsterSeed = method1Seed;
                        newSeed.Sid = (rng2 ^ (uint)rng3 ^ id) & 0xFFF8;
                        if (Functions.Shiny(newSeed.Pid, 20043, 0))
                        {
                            newSeed.Method = "Wishmkr Shiny";
                        }
                        else
                        {
                            newSeed.Method = "Wishmkr";
                        }
                        seeds.Add(newSeed);
                    }
                }

                //  Check Method 2
                // [PID] [PID] [xxxx] [IVs] [IVs]
                // [rng4] [rng3] [xxxx] [rng1] [START]
                if (Check(rng1, rng3, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                    {
                        Method = "Method 2",
                        Pid = ((uint)rng3 << 16) + rng4,
                        MonsterSeed = method234Seed,
                        Sid = (rng3 ^ (uint)rng4 ^ id) & 0xFFF8
                    };

                    seeds.Add(newSeed);
                }

                /*
                 * Removed because Method 3 doesn't exist in-game
                //  Check Method 3
                //  [PID] [xxxx] [PID] [IVs] [IVs]
                //  [rng4] [xxxx] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    Seed newSeed = new Seed();
                    newSeed.Method = "Method 3";
                    newSeed.Pid = ((uint)rng2 << 16) + (uint)rng4;
                    newSeed.MonsterSeed = method234Seed;
                    newSeed.Sid = ((uint)rng2 ^ (uint)rng4 ^ id) & 0xFFF8;

                    seeds.Add(newSeed);
                }
                 */

                //  Check Method 4
                //  [PID] [PID] [IVs] [xxxx] [IVs]
                //  [rng4] [rng3] [rng2] [xxxx] [START]
                if (Check(rng2, rng3, rng4, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                    {
                        Method = "Method 4",
                        Pid = ((uint)rng3 << 16) + rng4,
                        MonsterSeed = method234Seed,
                        Sid = (rng3 ^ (uint)rng4 ^ id) & 0xFFF8
                    };

                    seeds.Add(newSeed);
                }

                //  Check Colosseum\XD
                // [IVs] [IVs] [xxx] [PID] [PID]
                // [START] [rng1] [rng3]

                if (Check(rng1XD, rng3XD, rng4XD, spe, spa, spd, nature))
                {
                    var newSeed = new Seed
                    {
                        Method = "Colosseum/XD",
                        Pid = ((uint)rng3XD << 16) + rng4XD,
                        MonsterSeed = XDColoSeed,
                        Sid = (rng4XD ^ (uint)rng3XD ^ id) & 0xFFF8
                    };


                    seeds.Add(newSeed);
                }

                if (rng3 / 0x5556 != 0)
                {
                    //  Check DPPt Cute Charm
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 / 0xA3E);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (DPPt)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (50% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 / 0xA3E + 0x96);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (DPPt)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (25% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 / 0xA3E + 0xC8);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (DPPt)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (75% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 / 0xA3E + 0x4B);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (DPPt)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check DPPt Cute Charm (87.5% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 / 0xA3E + 0x32);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (DPPt)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }
                }

                if (rng3 % 3 != 0)
                {
                    //  Check HGSS Cute Charm
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 % 25);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (HGSS)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (50% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 % 25 + 0x96);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (HGSS)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (25% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 % 25 + 0xC8);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (HGSS)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (75% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 % 25 + 0x4B);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (HGSS)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }

                    //  Check HGSS Cute Charm (87.5% male)
                    //  [CC Check] [PID] [IVs] [IVs]
                    //  [rng3] [rng2] [rng1] [START]

                    choppedPID = (ushort)(rng2 % 25 + 0x32);
                    if (Check(rng1, 0, choppedPID, hp, atk, def, nature))
                    {
                        var newSeed = new Seed
                        {
                            Method = "Cute Charm (HGSS)",
                            Pid = choppedPID,
                            MonsterSeed = method1Seed,
                            Sid = (choppedPID ^ id) & 0xFFF8
                        };


                        seeds.Add(newSeed);
                    }
                }
            }

            return seeds;
        }

        // Overloaded method for SeedFinder's Open Search
        public static List<Seed> GetSeeds(
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            uint nature)
        {
            var seeds = new List<Seed>();

            uint x4 = spe + (spa << 5) + (spd << 10);
            uint x4_2 = x4 ^ 0x8000;

            uint x8 = hp + (atk << 5) + (def << 10);
            uint x8_2 = x8 ^ 0x8000;

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0; cnt <= 0xFFFE; cnt++)
            {
                uint x_test;
                uint x_testXD;

                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                x_test = (cnt & 1) == 0 ? x4 : x4_2;

                x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_test << 16) + (cnt & 0xFFFF);
                uint seedXD = (x_testXD << 16) + (cnt & 0xFFFF);

                var rng = new PokeRngR(seed);

                var rngXD = new XdRng(seedXD);
                var rngXDR = new XdRngR(seedXD);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1 = rng.GetNext16BitNumber();
                ushort rng2 = rng.GetNext16BitNumber();
                ushort rng3 = rng.GetNext16BitNumber();
                ushort rng4 = rng.GetNext16BitNumber();

                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                uint method1Seed = rng.Seed;

                rng.GetNext16BitNumber();
                uint method234Seed = rng.Seed;

                //  Check Method 1
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng3, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                        {
                            Method = "Method 1",
                            Pid = ((uint) rng2 << 16) + rng3,
                            MonsterSeed = method1Seed
                        };

                    seeds.Add(newSeed);
                }
            }

            //  Now we want to start with IV2 and call the RNG for
            //  values between 0 and FFFF in the low order bits.
            for (uint cnt = 0xFFFF; cnt <= 0x1FFFE; cnt++)
            {
                uint x_test;
                uint x_testXD;

                //  We want to test with the high bit
                //  both set and not set, so we're going
                //  to sneakily do them both.  god help
                //  me if i ever have to figure this out
                //  in the future.
                x_test = (cnt & 1) == 0 ? x4 : x4_2;

                x_testXD = (cnt & 1) == 0 ? x8 : x8_2;

                //  Set our test seed here so we can start
                //  working backwards to see if the rest
                //  of the information we were provided 
                //  is a match.

                uint seed = (x_test << 16) + ((cnt + 1) & 0xFFFF);
                uint seedXD = (x_testXD << 16) + ((cnt + 1) & 0xFFFF);

                var rng = new PokeRngR(seed);

                var rngXD = new XdRng(seedXD);
                var rngXDR = new XdRngR(seedXD);
                uint XDColoSeed = rngXDR.GetNext32BitNumber();

                //  Right now, this simply assumes method
                //  1 and gets the value previous to check
                //  for  match.  We need a clean way to do
                //  this for all of our methods.

                //  We have a max of 5 total RNG calls
                //  to make a pokemon and we already have
                //  one so lets go ahead and get 4 more.
                ushort rng1 = rng.GetNext16BitNumber();
                ushort rng2 = rng.GetNext16BitNumber();
                ushort rng3 = rng.GetNext16BitNumber();
                ushort rng4 = rng.GetNext16BitNumber();

                ushort rng1XD = rngXD.GetNext16BitNumber();
                ushort rng2XD = rngXD.GetNext16BitNumber();
                ushort rng3XD = rngXD.GetNext16BitNumber();
                ushort rng4XD = rngXD.GetNext16BitNumber();

                uint method1Seed = rng.Seed;

                rng.GetNext16BitNumber();
                uint method234Seed = rng.Seed;

                //  Check Method 1
                // [PID] [PID] [IVs] [IVs]
                // [rng3] [rng2] [rng1] [START]
                if (Check(rng1, rng2, rng3, hp, atk, def, nature))
                {
                    //  Build a seed to add to our collection
                    var newSeed = new Seed
                    {
                        Method = "Method 1",
                        Pid = ((uint)rng2 << 16) + rng3,
                        MonsterSeed = method1Seed
                    };

                    seeds.Add(newSeed);
                }
            }

            return seeds;
        }

        public static bool CheckPID(
            uint pid2,
            uint pid1,
            uint nature)
        {
            uint pid = (pid2 << 16) | pid1;

            uint pidNature = pid%25;

            //  Do a nature comparison with what we have selected
            //  in the dropdown and if we have a good match we can
            //  go ahead and add this to our starting seeds.
            return nature == pidNature;
        }

        public static bool Check(
            ushort iv,
            ushort pid2,
            ushort pid1,
            uint hp,
            uint atk,
            uint def,
            uint nature)
        {
            bool ret = false;

            uint test_hp = (uint) iv & 0x1f;
            uint test_atk = ((uint) iv & 0x3E0) >> 5;
            uint test_def = ((uint) iv & 0x7C00) >> 10;

            if (test_hp == hp &&
                test_atk == atk &&
                test_def == def)
            {
                //  Use these two values to see if we have a possible
                //  match for the nature of this pokemon.  Also, if
                //  we have a match then the RNG will contain a
                //  seeding possibility.

                uint pid = ((uint) pid2 << 16) | pid1;

                uint pidNature = pid - 25 * (pid / 25);

                //  Do a nature comparison with what we have selected
                //  in the dropdown and if we have a good match we can
                //  go ahead and add this to our starting seeds.
                if (nature == pidNature)
                {
                    ret = true;
                }
            }

            return ret;
        }

        // bool Check(ushort iv, short pid2, ushort pid1)
    }
}