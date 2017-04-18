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
    public class Frame
    {
        private static readonly uint[] HABCDS = {0, 1, 2, 5, 3, 4};
        private static readonly uint[] ABCDS = {1, 2, 5, 3, 4};
        private static readonly uint[] ACDS = {1, 5, 3, 4};

        /// <summary>
        ///     1 or 2 for the ability number, best we can do since we don't know what the monster is actually going to be.
        /// </summary>
        private uint ability;

        private uint cGearTime;
        private uint[] characteristicIVs;
        private uint coin;
        private bool dreamAbility;
        private uint dv;
        private uint id;
        private uint inh1;
        private uint inh2;
        private uint inh3;
        private int maleOnlySpecies;
        private uint number;
        private uint par1;
        private uint par2;
        private uint par3;
        private uint pid;
        private uint seed;
        private uint sid;
        private bool synchable;

        public Frame()
        {
            Shiny = false;
            EncounterMod = EncounterMod.None;
            Offset = 0;
        }

        internal Frame(FrameType frameType)
        {
            Shiny = false;
            EncounterMod = EncounterMod.None;
            Offset = 0;
            FrameType = frameType;
        }

        public uint RngResult { get; set; }

        public uint MaxSkips { get; set; }

        public string Elm
        {
            get { return Responses.ElmResponse(RngResult); }
        }

        public string CaveSpotting
        {
            get { return RngResult >> 31 == 1 ? "Possible" : ""; }
        }

        // Chatot response for 4th Gen Games
        public string Chatot
        {
            get { return Responses.ChatotResponse(RngResult); }
        }

        // Chatot response for 5th Gen Games (64-bit RNG)
        public string Chatot64
        {
            get { return Responses.ChatotResponse64(RngResult); }
        }

        public uint Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        public uint Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        ///     Friendly display name since we want to show the S as a postfix to the split spreads right now.
        /// </summary>
        public string Name
        {
            get
            {
                string name = number.ToString();

                if (FrameType == FrameType.BredSplit)
                    name += "S";

                return name;
            }
        }

        public uint Offset { get; set; }

        public string Time
        {
            get
            {
                uint minutes = number/3600;
                uint seconds = (number - (3600*minutes))/60;
                uint milli = ((number%60)*100)/60;

                return minutes.ToString() + ":" + seconds.ToString("D2") + "." + milli.ToString("D2");
            }
        }

        public uint CGearTime
        {
            get { return cGearTime; }
            set { cGearTime = value; }
        }

        public string EntralinkTime
        {
            get
            {
                if (cGearTime == 0)
                    return "skip";
                uint minutes = cGearTime/3600;
                uint seconds = (cGearTime - (3600*minutes))/60;
                uint milli = ((cGearTime%60)*100)/60;

                return minutes.ToString() + ":" + seconds.ToString("D2") + "." + milli.ToString("D2");
            }
        }

        public bool DreamAbility
        {
            get { return dreamAbility; }
            set { dreamAbility = value; }
        }

        public bool Synchable
        {
            get { return synchable; }
            set { synchable = value; }
        }

        public FrameType FrameType { get; set; }

        public EncounterType EncounterType { get; set; }

        public EncounterMod EncounterMod { get; set; }

        public uint ItemCalc { get; set; }

        //  ID's used for checking if we have a shiny.  If these are
        //  zero'd we will not actually do the check.

        public bool Shiny { get; private set; }

        /// <summary>
        ///     Display member called by the grid control. Saves us from having to actually do anything but return string and not actually store it.
        /// </summary>
        public string ShinyDisplay
        {
            get { return Shiny ? "!!!" : ""; }
        }

        //  The following are cacluated differently based
        //  on the creation method of the pokemon. 

        public uint Pid
        {
            get { return pid; }
            set
            {
                Nature = (value%25);
                ability = (value & 1);
                coin = (value & 1);

                //  figure out if we are shiny here
                uint tid = (id & 0xffff) | ((sid & 0xffff) << 16);
                uint a = value ^ tid;
                uint b = a & 0xffff;
                uint c = (a >> 16);
                uint d = b ^ c;
                if (d < 8)
                    Shiny = true;

                pid = value;
            }
        }

        public uint Advances { get; set; }

        public uint Dv
        {
            get { return dv; }
            set
            {
                //  Split up our DV
                var dv1 = (ushort) value;
                var dv2 = (ushort) (value >> 16);

                //  Get the actual Values
                Hp = (uint) dv1 & 0x1f;
                Atk = ((uint) dv1 & 0x3E0) >> 5;
                Def = ((uint) dv1 & 0x7C00) >> 10;

                Spe = (uint) dv2 & 0x1f;
                Spa = ((uint) dv2 & 0x3E0) >> 5;
                Spd = ((uint) dv2 & 0x7C00) >> 10;


                //  Calculate the inheretence for this frame
                if (FrameType == FrameType.Bred ||
                    FrameType == FrameType.BredSplit ||
                    FrameType == FrameType.DPPtBred)
                {
                    DisplayHp = Hp.ToString();
                    DisplayAtk = Atk.ToString();
                    DisplayDef = Def.ToString();
                    DisplaySpa = Spa.ToString();
                    DisplaySpd = Spd.ToString();
                    DisplaySpe = Spe.ToString();

                    uint inherited1 = HABCDS[inh1%6];
                    switch (inherited1)
                    {
                        case 0:
                            DisplayHp = (par1 & 1) == 0 ? "A" : "B";
                            break;
                        case 1:
                            DisplayAtk = (par1 & 1) == 0 ? "A" : "B";
                            break;
                        case 2:
                            DisplayDef = (par1 & 1) == 0 ? "A" : "B";
                            break;
                        case 3:
                            DisplaySpa = (par1 & 1) == 0 ? "A" : "B";
                            break;
                        case 4:
                            DisplaySpd = (par1 & 1) == 0 ? "A" : "B";
                            break;
                        case 5:
                            DisplaySpe = (par1 & 1) == 0 ? "A" : "B";
                            break;
                    }

                    uint inherited2 = ABCDS[inh2%5];
                    switch (inherited2)
                    {
                        case 1:
                            DisplayAtk = (par2 & 1) == 0 ? "A" : "B";
                            break;
                        case 2:
                            DisplayDef = (par2 & 1) == 0 ? "A" : "B";
                            break;
                        case 3:
                            DisplaySpa = (par2 & 1) == 0 ? "A" : "B";
                            break;
                        case 4:
                            DisplaySpd = (par2 & 1) == 0 ? "A" : "B";
                            break;
                        case 5:
                            DisplaySpe = (par2 & 1) == 0 ? "A" : "B";
                            break;
                    }

                    uint inherited3 = ACDS[inh3&3];
                    switch (inherited3)
                    {
                        case 1:
                            DisplayAtk = (par3 & 1) == 0 ? "A" : "B";
                            break;
                        case 3:
                            DisplaySpa = (par3 & 1) == 0 ? "A" : "B";
                            break;
                        case 4:
                            DisplaySpd = (par3 & 1) == 0 ? "A" : "B";
                            break;
                        case 5:
                            DisplaySpe = (par3 & 1) == 0 ? "A" : "B";
                            break;
                    }
                }
                if (FrameType == FrameType.HGSSBred ||
                    FrameType == FrameType.RSBredUpper ||
                    FrameType == FrameType.RSBredUpperSplit ||
                    FrameType == FrameType.RSBredUpperAlt)
                {
                    DisplayHp = Hp.ToString();
                    DisplayAtk = Atk.ToString();
                    DisplayDef = Def.ToString();
                    DisplaySpa = Spa.ToString();
                    DisplaySpd = Spd.ToString();
                    DisplaySpe = Spe.ToString();

                    uint[] available = {0, 1, 2, 3, 4, 5};

                    // Dumb that we have to do this, but we really
                    // need these guys in an array for things to 
                    // work correctly.
                    var rngArray = new uint[6];
                    rngArray[0] = inh1;
                    rngArray[1] = inh2;
                    rngArray[2] = inh3;
                    rngArray[3] = par1;
                    rngArray[4] = par2;
                    rngArray[5] = par3;

                    for (uint cnt = 0; cnt < 3; cnt++)
                    {
                        // Decide which parent (A or B) from which we'll pick an IV
                        uint parent = rngArray[3 + cnt] & 1;

                        // Decide which stat to pick for IV inheritance
                        uint ivslot = available[rngArray[0 + cnt]%(6 - cnt)];
                        //  We have our parent and we have our slot, so lets 
                        //  put them in the correct place here 
                        string parentString = (parent == 0 ? "A" : "B");

                        switch (ivslot)
                        {
                            case 0:
                                DisplayHp = parentString;
                                break;
                            case 1:
                                DisplayAtk = parentString;
                                break;
                            case 2:
                                DisplayDef = parentString;
                                break;
                            case 3:
                                DisplaySpe = parentString;
                                break;
                            case 4:
                                DisplaySpa = parentString;
                                break;
                            case 5:
                                DisplaySpd = parentString;
                                break;
                        }

                        // Find out where taking an item from
                        //  so that we know where to start doing
                        //  doing our shift.

                        // Avoids repeat IV inheritance
                        for (uint j = 0; j < 5 - cnt; j++)
                        {
                            if (ivslot <= available[j])
                            {
                                available[j] = available[j + 1];
                            }
                        }
                    }
                }

                //  Set the actual dv value
                dv = value;
            }
        }

        public uint Ability
        {
            get { return ability; }
            set { ability = value; }
        }

        public string Coin
        {
            get { return coin == 0 ? "Tails" : "Heads"; }
        }

        public uint Nature { get; set; }

        public string NatureDisplay
        {
            get { return Functions.NatureStrings((int) Nature); }
        }

        public uint Hp { get; set; }

        public uint Atk { get; set; }

        public uint Def { get; set; }

        public uint Spa { get; set; }

        public uint Spd { get; set; }

        public uint Spe { get; set; }

        public string DisplayHp { get; set; }

        public string DisplayAtk { get; set; }

        public string DisplayDef { get; set; }

        public string DisplaySpa { get; set; }

        public string DisplaySpd { get; set; }

        public string DisplaySpe { get; set; }

        // this set of displays is used to display egg IVs
        // instead of labels like "A", "B", "Fe", "Ma"
        public string DisplayHpAlt { get; set; }

        public string DisplayAtkAlt { get; set; }

        public string DisplayDefAlt { get; set; }

        public string DisplaySpaAlt { get; set; }

        public string DisplaySpdAlt { get; set; }

        public string DisplaySpeAlt { get; set; }

        //  Hidden power information that we calculate at the same
        //  we build the DVs.
        public string HiddenPowerType
        {
            get
            {
                uint t_hp = Hp & 1;
                uint t_atk = (Atk & 1) << 1;
                uint t_def = (Def & 1) << 2;
                uint t_spe = (Spe & 1) << 3;
                uint t_spa = (Spa & 1) << 4;
                uint t_spd = (Spd & 1) << 5;

                uint hp_type = (t_hp + t_atk + t_def + t_spe + t_spa + t_spd)*15/63;

                return Functions.power(hp_type);
            }
        }

        public uint HiddenPowerPower
        {
            get
            {
                uint p_hp = (Hp & 3) > 1u ? 1u : 0u;
                uint p_atk = (Atk & 3) > 1u ? 2u : 0u;
                uint p_def = (Def & 3) > 1u ? 4u : 0u;
                uint p_spe = (Spe & 3) > 1u ? 8u : 0u;
                uint p_spa = (Spa & 3) > 1u ? 16u : 0u;
                uint p_spd = (Spd & 3) > 1u ? 32u : 0u;

                uint hp_power = (p_hp + p_atk + p_def + p_spe + p_spa + p_spd)*40/63 + 30;

                return hp_power;
            }
        }

        public uint GenderValue
        {
            get { return pid & 0xFF; }
        }

        public string Female50
        {
            get { return ((pid & 0xFF) >= 127) ? "M" : "F"; }
        }

        public string Female125
        {
            get { return ((pid & 0xFF) >= 31) ? "M" : "F"; }
        }

        public string Female25
        {
            get { return ((pid & 0xFF) >= 63) ? "M" : "F"; }
        }

        public string Female75
        {
            get { return ((pid & 0xFF) >= 191) ? "M" : "F"; }
        }

        public int MaleOnlySpecies
        {
            get { return maleOnlySpecies; }
            set { maleOnlySpecies = value; }
        }

        public string MaleOnly
        {
            get { return maleOnlySpecies == 0 ? "Nidoran-F\\Volbeat" : "Nidoran-M\\Illumise"; }
        }

        public string seedTime { get; set; }

        public int EncounterSlot { get; set; }

        public string EncounterString
        {
            get { return Functions.encounterItems(EncounterSlot); }
        }

        public bool ShakingSpotPossible
        {
            get { return (RngResult >> 28) == 0; }
        }

        public string Characteristic
        {
            get { return Functions.characteristicCalc(Pid, characteristicIVs); }
        }

        public uint[] CharacteristicIVs
        {
            get { return characteristicIVs; }
            set { characteristicIVs = value; }
        }

        /// <summary>
        ///     Generic Frame creation where the values that are to be used for each part are passed in explicitly. There will be other methods to support splitting a list and then passing them to this for creation.
        /// </summary>
        public static Frame GenerateFrame(
            uint seed,
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid1,
            uint pid2,
            uint dv1,
            uint dv2,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint id,
            uint sid,
            uint offset)
        {
            var frame = new Frame(frameType)
                {
                    Seed = seed,
                    Number = number,
                    RngResult = rngResult,
                    Offset = offset,
                    id = id,
                    sid = sid,
                    Pid = (pid2 << 16) | pid1,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3,
                    Dv = (dv2 << 16) | dv1
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            return frame;
        }

        //Emerald Eggs
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid,
            uint dv1,
            uint dv2,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint[] parentA,
            uint[] parentB,
            uint id,
            uint sid,
            uint offset)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    Offset = offset,
                    id = id,
                    sid = sid,
                    Pid = pid,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3,
                    Dv = (dv2 << 16) | dv1
                };

            //punch in the inheritence values
            if (parentA != null && parentB != null && parentA.Length == 6 && parentB.Length == 6)
            {
                frame.DisplayHpAlt = GetInheritence(frame.DisplayHp, parentA[0], parentB[0]);
                frame.DisplayAtkAlt = GetInheritence(frame.DisplayAtk, parentA[1], parentB[1]);
                frame.DisplayDefAlt = GetInheritence(frame.DisplayDef, parentA[2], parentB[2]);
                frame.DisplaySpaAlt = GetInheritence(frame.DisplaySpa, parentA[3], parentB[3]);
                frame.DisplaySpdAlt = GetInheritence(frame.DisplaySpd, parentA[4], parentB[4]);
                frame.DisplaySpeAlt = GetInheritence(frame.DisplaySpe, parentA[5], parentB[5]);
            }

            return frame;
        }

        private static string GetInheritence(string display, uint parenta, uint parentb)
        {
            return display == "A"
                       ? parenta.ToString()
                       : display == "B" ? parentb.ToString() : display;
        }

        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint dv1,
            uint dv2,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint[] parentA,
            uint[] parentB,
            uint id,
            uint sid,
            uint offset)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            var rngArray = new uint[6];
            rngArray[0] = inh1;
            rngArray[1] = inh2;
            rngArray[2] = inh3;
            rngArray[3] = par1;
            rngArray[4] = par2;
            rngArray[5] = par3;

            frame.Dv = (dv2 << 16) + dv1;

            if (parentA != null && parentB != null)
            {
                uint[] available = {0, 1, 2, 3, 4, 5};
                if (frameType == FrameType.HGSSBred)
                {
                    for (uint cnt = 0; cnt < 3; cnt++)
                    {
                        uint parent = rngArray[3 + cnt] & 1;
                        uint ivslot = available[rngArray[0 + cnt]%(6 - cnt)];
                        //  We have our parent and we have our slot, so lets 
                        //  put them in the correct place here 

                        switch (ivslot)
                        {
                            case 0:
                                frame.Hp = (parent == 0 ? parentA[0] : parentB[0]);
                                break;
                            case 1:
                                frame.Atk = (parent == 0 ? parentA[1] : parentB[1]);
                                break;
                            case 2:
                                frame.Def = (parent == 0 ? parentA[2] : parentB[2]);
                                break;
                            case 3:
                                //parrents speed = 5, egg speed = 3
                                frame.Spe = (parent == 0 ? parentA[5] : parentB[5]);
                                break;
                            case 4:
                                //parrents spa = 3, egg spa = 4
                                frame.Spa = (parent == 0 ? parentA[3] : parentB[3]);
                                break;
                            case 5:
                                //parrents spd = 4, egg spd = 6
                                frame.Spd = (parent == 0 ? parentA[4] : parentB[4]);
                                break;
                        }

                        // Find out where taking an item from
                        //  so that we know where to start doing
                        //  doing our shift.
                        for (uint j = 0; j < 5 - cnt; j++)
                        {
                            if (ivslot <= available[j])
                            {
                                available[j] = available[j + 1];
                            }
                        }
                    }
                }
                else
                {
                    uint inherited1 = HABCDS[inh1%6];
                    switch (inherited1)
                    {
                        case 0:
                            frame.Hp = (par1 & 1) == 0 ? parentA[0] : parentB[0];
                            break;
                        case 1:
                            frame.Atk = (par1 & 1) == 0 ? parentA[1] : parentB[1];
                            break;
                        case 2:
                            frame.Def = (par1 & 1) == 0 ? parentA[2] : parentB[2];
                            break;
                        case 3:
                            frame.Spa = (par1 & 1) == 0 ? parentA[3] : parentB[3];
                            break;
                        case 4:
                            frame.Spd = (par1 & 1) == 0 ? parentA[4] : parentB[4];
                            break;
                        case 5:
                            frame.Spe = (par1 & 1) == 0 ? parentA[5] : parentB[5];
                            break;
                    }

                    uint inherited2 = ABCDS[inh2%5];
                    switch (inherited2)
                    {
                        case 1:
                            frame.Atk = (par2 & 1) == 0 ? parentA[1] : parentB[1];
                            break;
                        case 2:
                            frame.Def = (par2 & 1) == 0 ? parentA[2] : parentB[2];
                            break;
                        case 3:
                            frame.Spa = (par2 & 1) == 0 ? parentA[3] : parentB[3];
                            break;
                        case 4:
                            frame.Spd = (par2 & 1) == 0 ? parentA[4] : parentB[4];
                            break;
                        case 5:
                            frame.Spe = (par2 & 1) == 0 ? parentA[5] : parentB[5];
                            break;
                    }

                    uint inherited3 = ACDS[inh3&3];
                    switch (inherited3)
                    {
                        case 1:
                            frame.Atk = (par3 & 1) == 0 ? parentA[1] : parentB[1];
                            break;
                        case 3:
                            frame.Spa = (par3 & 1) == 0 ? parentA[3] : parentB[3];
                            break;
                        case 4:
                            frame.Spd = (par3 & 1) == 0 ? parentA[4] : parentB[4];
                            break;
                        case 5:
                            frame.Spe = (par3 & 1) == 0 ? parentA[5] : parentB[5];
                            break;
                    }
                }

                frame.DisplayHpAlt = frame.Hp.ToString();
                frame.DisplayAtkAlt = frame.Atk.ToString();
                frame.DisplayDefAlt = frame.Def.ToString();
                frame.DisplaySpaAlt = frame.Spa.ToString();
                frame.DisplaySpdAlt = frame.Spd.ToString();
                frame.DisplaySpeAlt = frame.Spe.ToString();
            }

            return frame;
        }

        // for Methods 1, 2, 4
        public static Frame GenerateFrame(
            uint seed,
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid1,
            uint pid2,
            uint dv1,
            uint dv2,
            uint id,
            uint sid)
        {
            var frame = new Frame(frameType)
                {
                    seed = seed,
                    number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Pid = (pid2 << 16) | pid1,
                    Dv = (dv2 << 16) | dv1
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            return frame;
        }

        //for Channel Method
        public static Frame GenerateChannel(
            uint seed,
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid1,
            uint pid2,
            uint dv1,
            uint dv2,
            uint dv3,
            uint dv4,
            uint dv5,
            uint dv6,
            uint id,
            uint sid)
        {
            var frame = new Frame(frameType)
            {
                seed = seed,
                number = number,
                RngResult = rngResult,
                id = id,
                sid = sid,
                Pid = ((pid1 ^ 0x8000) << 16) | pid2
        };

            frame.id = id;
            frame.sid = sid;
            frame.Hp = dv1;
            frame.Atk = dv2;
            frame.Def = dv3;
            frame.Spa = dv4;
            frame.Spd = dv5;
            frame.Spe = dv6;

            return frame;
        }

        // for Methods H, J, K
        public static Frame GenerateFrame(
            uint seed,
            FrameType frameType,
            EncounterType encounterType,
            uint number,
            uint rngResult,
            uint pid1,
            uint pid2,
            uint dv1,
            uint dv2,
            uint id,
            uint sid,
            uint offset,
            int encounterSlot)
        {
            var frame = new Frame(frameType)
                {
                    Seed = seed,
                    Number = number,
                    RngResult = rngResult,
                    Offset = offset,
                    id = id,
                    sid = sid,
                    Pid = (pid2 << 16) | pid1,
                    Dv = (dv2 << 16) | dv1,
                    EncounterType = encounterType,
                    EncounterSlot = encounterSlot
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            return frame;
        }

        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid,
            uint id,
            uint sid)
        {
            var frame = new Frame(frameType)
                {Number = number, RngResult = rngResult, id = id, sid = sid, Pid = pid, Dv = 0};


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.
            //  frame.Pid = pid;

            return frame;
        }

        // for Ruby\Sapphire lower egg PID half
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint pid,
            uint compatibility)
        {
            var frame = new Frame(frameType) {RngResult = rngResult};


            // if upper 8 bits is less than 51
            // Day-Care Man holds an egg
            if ((rngResult*100)/0xFFFF < compatibility)
            {
                // If valid, assign the frame a non-zero number so it doesn't get filtered
                frame.Number = number;

                if (pid > 0xFFFD)
                {
                    frame.Pid = (pid + 3)%0xFFFF;
                }
                else
                    frame.Pid = (pid & 0xFFFF) + 1;
            }

            return frame;
        }

        // for Ruby\Sapphire full egg PID
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint lowerPID,
            uint upperPID,
            uint dv1,
            uint dv2,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint[] parentA,
            uint[] parentB,
            uint id,
            uint sid)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3,
                    Pid = (upperPID << 16) | lowerPID,
                    Dv = (dv2 << 16) | dv1
                };


            uint[] available = {0, 1, 2, 3, 4, 5};

            var rngArray = new uint[6];
            rngArray[0] = inh1;
            rngArray[1] = inh2;
            rngArray[2] = inh3;
            rngArray[3] = par1;
            rngArray[4] = par2;
            rngArray[5] = par3;

            if (parentA != null && parentB != null)
            {
                for (uint cnt = 0; cnt < 3; cnt++)
                {
                    uint parent = rngArray[3 + cnt] & 1;
                    uint ivslot = available[rngArray[0 + cnt]%(6 - cnt)];
                    //  We have our parent and we have our slot, so lets 
                    //  put them in the correct place here 

                    switch (ivslot)
                    {
                        case 0:
                            frame.Hp = (parent == 0 ? parentA[0] : parentB[0]);
                            break;
                        case 1:
                            frame.Atk = (parent == 0 ? parentA[1] : parentB[1]);
                            break;
                        case 2:
                            frame.Def = (parent == 0 ? parentA[2] : parentB[2]);
                            break;
                        case 3:
                            frame.Spe = (parent == 0 ? parentA[5] : parentB[5]);
                            break;
                        case 4:
                            frame.Spa = (parent == 0 ? parentA[3] : parentB[3]);
                            break;
                        case 5:
                            frame.Spd = (parent == 0 ? parentA[4] : parentB[4]);
                            break;
                    }

                    // Find out where taking an item from
                    //  so that we know where to start doing
                    //  doing our shift.
                    for (uint j = 0; j < 5 - cnt; j++)
                    {
                        if (ivslot <= available[j])
                        {
                            available[j] = available[j + 1];
                        }
                    }
                }
            }

            frame.DisplayHpAlt = frame.Hp.ToString();
            frame.DisplayAtkAlt = frame.Atk.ToString();
            frame.DisplayDefAlt = frame.Def.ToString();
            frame.DisplaySpaAlt = frame.Spa.ToString();
            frame.DisplaySpdAlt = frame.Spd.ToString();
            frame.DisplaySpeAlt = frame.Spe.ToString();

            return frame;
        }

        // used for 5th Gen nature generation
        public static Frame GenerateFrame(
            FrameType frameType,
            EncounterType encounterType,
            uint number,
            uint rngResult,
            uint pid,
            uint id,
            uint sid,
            uint natureValue,
            bool synch,
            int encounterSlot,
            uint itemCalc)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Pid = pid,
                    Nature = natureValue,
                    ability = (pid >> 16) & 1,
                    EncounterType = encounterType,
                    EncounterSlot = encounterSlot,
                    ItemCalc = itemCalc,
                    synchable = synch
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            return frame;
        }

        public static Frame GenerateFrame(
            FrameType frameType,
            EncounterType encounterType,
            uint number,
            uint rngResult,
            uint pid,
            uint id,
            uint sid,
            uint natureValue,
            bool synch,
            int encounterSlot,
            uint itemCalc,
            uint[] rngIVs)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Pid = pid,
                    Nature = natureValue,
                    ability = (pid >> 16) & 1,
                    EncounterType = encounterType,
                    EncounterSlot = encounterSlot,
                    ItemCalc = itemCalc,
                    synchable = synch,
                    Hp = rngIVs[0],
                    Atk = rngIVs[1],
                    Def = rngIVs[2],
                    Spa = rngIVs[3],
                    Spd = rngIVs[4],
                    Spe = rngIVs[5]
                };


            //  Set up the ID and SID before we calculate 
            //  the pid, as we are going to need this.


            return frame;
        }

        // for 5th Gen eggs
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            int maleOnlySpecies,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint pid,
            uint id,
            uint sid,
            bool dream,
            bool everstone,
            uint natureValue,
            uint maxSkips)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Pid = pid,
                    Ability = (pid >> 16) & 1,
                    Nature = natureValue,
                    dreamAbility = dream,
                    synchable = everstone,
                    maleOnlySpecies = maleOnlySpecies,
                    MaxSkips = maxSkips,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3
                };


            return frame;
        }

        // used for 5th Gen eggs when parent IVs are known
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            int maleOnlySpecies,
            uint inh1,
            uint inh2,
            uint inh3,
            uint par1,
            uint par2,
            uint par3,
            uint[] parentA,
            uint[] parentB,
            uint[] rngIVs,
            uint pid,
            uint id,
            uint sid,
            bool dream,
            bool everstone,
            uint natureValue,
            uint maxSkips)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Pid = pid,
                    Ability = (pid >> 16) & 1,
                    Nature = natureValue,
                    dreamAbility = dream,
                    synchable = everstone,
                    maleOnlySpecies = maleOnlySpecies,
                    MaxSkips = maxSkips,
                    inh1 = inh1,
                    inh2 = inh2,
                    inh3 = inh3,
                    par1 = par1,
                    par2 = par2,
                    par3 = par3,
                    Hp = rngIVs[0],
                    Atk = rngIVs[1],
                    Def = rngIVs[2],
                    Spa = rngIVs[3],
                    Spd = rngIVs[4],
                    Spe = rngIVs[5]
                };


            var rngArray = new uint[6];
            rngArray[0] = inh1;
            rngArray[1] = inh2;
            rngArray[2] = inh3;
            rngArray[3] = par1;
            rngArray[4] = par2;
            rngArray[5] = par3;

            for (uint cnt = 0; cnt < 3; cnt++)
            {
                uint parent = rngArray[3 + cnt] & 1;

                //  We have our parent and we have our slot, so lets 
                //  put them in the correct place here 
                uint parentIV = (parent == 1 ? parentA[rngArray[cnt]] : parentB[rngArray[cnt]]);

                switch (rngArray[cnt])
                {
                    case 0:
                        frame.Hp = parentIV;
                        break;
                    case 1:
                        frame.Atk = parentIV;
                        break;
                    case 2:
                        frame.Def = parentIV;
                        break;
                    case 3:
                        frame.Spa = parentIV;
                        break;
                    case 4:
                        frame.Spd = parentIV;
                        break;
                    case 5:
                        frame.Spe = parentIV;
                        break;
                }
            }

            return frame;
        }


        // for 5th Gen Wild Pokémon
        public static Frame GenerateFrame(
            FrameType frameType,
            uint number,
            uint rngResult,
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    seed = rngResult,
                    Hp = hp,
                    Atk = atk,
                    Def = def,
                    Spa = spa,
                    Spd = spd,
                    Spe = spe
                };


            return frame;
        }


        // 5th Gen Wondercards
        public static Frame GenerateFrame(
            FrameType frameType,
            uint id,
            uint sid,
            uint number,
            uint rngResult,
            uint hp,
            uint atk,
            uint def,
            uint spa,
            uint spd,
            uint spe,
            uint natureValue,
            uint pid)
        {
            var frame = new Frame(frameType)
                {
                    Number = number,
                    RngResult = rngResult,
                    id = id,
                    sid = sid,
                    Hp = hp,
                    Atk = atk,
                    Def = def,
                    Spa = spa,
                    Spd = spd,
                    Spe = spe,
                    Pid = pid ^ 0x10000
                };


            var nature = (uint) ((ulong) natureValue*25 >> 32);
            frame.Nature = nature;
            frame.Ability = (pid >> 16) & 1;

            return frame;
        }

        // This method is only called when a frame is going to be displayed
        // Avoids unnecessary costly functions
        public void DisplayPrep()
        {
            if (FrameType != FrameType.Method5Natures &&
                FrameType != FrameType.BredAlternate &&
                FrameType != FrameType.BredSplit &&
                FrameType != FrameType.Bred &&
                FrameType != FrameType.DPPtBred &&
                FrameType != FrameType.HGSSBred &&
                FrameType != FrameType.BWBred &&
                FrameType != FrameType.BWBredInternational &&
                FrameType != FrameType.RSBredUpper &&
                FrameType != FrameType.RSBredUpperSplit)
            {
                DisplayHp = Hp.ToString();
                DisplayAtk = Atk.ToString();
                DisplayDef = Def.ToString();
                DisplaySpa = Spa.ToString();
                DisplaySpd = Spd.ToString();
                DisplaySpe = Spe.ToString();
            }

            if (FrameType == FrameType.BWBred ||
                FrameType == FrameType.BWBredInternational)
            {
                var rngArray = new uint[6];
                rngArray[0] = inh1;
                rngArray[1] = inh2;
                rngArray[2] = inh3;
                rngArray[3] = par1;
                rngArray[4] = par2;
                rngArray[5] = par3;

                DisplayHpAlt = Hp.ToString();
                DisplayAtkAlt = Atk.ToString();
                DisplayDefAlt = Def.ToString();
                DisplaySpaAlt = Spa.ToString();
                DisplaySpdAlt = Spd.ToString();
                DisplaySpeAlt = Spe.ToString();

                for (uint cnt = 0; cnt < 3; cnt++)
                {
                    uint parent = rngArray[3 + cnt] & 1;

                    //  We have our parent and we have our slot, so lets 
                    //  put them in the correct place here 
                    string parentString = (parent == 1 ? "Fe" : "Ma");

                    switch (rngArray[cnt])
                    {
                        case 0:
                            DisplayHp = parentString;
                            break;
                        case 1:
                            DisplayAtk = parentString;
                            break;
                        case 2:
                            DisplayDef = parentString;
                            break;
                        case 3:
                            DisplaySpa = parentString;
                            break;
                        case 4:
                            DisplaySpd = parentString;
                            break;
                        case 5:
                            DisplaySpe = parentString;
                            break;
                    }
                }
            }
        }

        public static Frame Clone(Frame source)
        {
            // We're implementing this only for Gen 5 IVs
            // Because they can have multiple nearby shiny frames

            var clone = new Frame
                {
                    FrameType = source.FrameType,
                    number = source.number,
                    seed = source.seed,
                    Hp = source.Hp,
                    Atk = source.Atk,
                    Def = source.Def,
                    Spa = source.Spa,
                    Spd = source.Spd,
                    Spe = source.Spe,
                    DisplayHp = source.DisplayHp,
                    DisplayAtk = source.DisplayAtk,
                    DisplayDef = source.DisplayDef,
                    DisplaySpa = source.DisplaySpa,
                    DisplaySpd = source.DisplaySpd,
                    DisplaySpe = source.DisplaySpe,
                    EncounterMod = source.EncounterMod,
                    EncounterSlot = source.EncounterSlot,
                    id = source.id,
                    sid = source.sid,
                    Pid = source.Pid
                };


            return clone;
        }
    }
}