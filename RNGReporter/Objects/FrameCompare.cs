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
using System.Linq;

namespace RNGReporter.Objects
{
    /// <summary>
    ///     This is going to hold a list of comparison information and then have methods to check against a particular frame to see if it actually matches.
    /// </summary>
    internal class FrameCompare
    {
        private readonly bool ABcheck;
        private readonly int ability;
        private readonly CompareType atkCompare;
        private readonly uint atkValue;
        private readonly uint compareAtkA;
        private readonly uint compareAtkB;
        private readonly uint compareDefA;
        private readonly uint compareDefB;
        private readonly uint compareHpA;
        private readonly uint compareHpB;
        private readonly uint compareSpaA;
        private readonly uint compareSpaB;
        private readonly uint compareSpdA;
        private readonly uint compareSpdB;
        private readonly uint compareSpeA;
        private readonly uint compareSpeB;
        private readonly List<FrameCompare> comparers;
        private readonly CompareType defCompare;
        private readonly uint defValue;

        //  Other Values -----------------------------------
        private readonly bool dreamWorld;
        private readonly List<int> encounterSlots;
        private readonly CompareType hpCompare;
        private readonly uint hpValue;
        private readonly bool shinyOnly;
        private readonly CompareType spaCompare;
        private readonly uint spaValue;
        private readonly CompareType spdCompare;
        private readonly uint spdValue;
        private readonly CompareType speCompare;
        private readonly uint speValue;
        private readonly bool synchOnly;

        // We're making this public
        // So they can be accessed when calculating Entralink PIDs

        public FrameCompare(
            uint compareHpA,
            uint compareAtkA,
            uint compareDefA,
            uint compareSpaA,
            uint compareSpdA,
            uint compareSpeA,
            uint compareHpB,
            uint compareAtkB,
            uint compareDefB,
            uint compareSpaB,
            uint compareSpdB,
            uint compareSpeB,
            uint hpValue,
            CompareType hpCompare,
            uint atkValue,
            CompareType atkCompare,
            uint defValue,
            CompareType defCompare,
            uint spaValue,
            CompareType spaCompare,
            uint spdValue,
            CompareType spdCompare,
            uint speValue,
            CompareType speCompare,
            List<uint> natures,
            int ability,
            bool shinyOnly,
            bool checkparents,
            GenderFilter genderFilter)
        {
            comparers = new List<FrameCompare>();

            this.compareHpA = compareHpA;
            this.compareAtkA = compareAtkA;
            this.compareDefA = compareDefA;
            this.compareSpaA = compareSpaA;
            this.compareSpdA = compareSpdA;
            this.compareSpeA = compareSpeA;

            this.compareHpB = compareHpB;
            this.compareAtkB = compareAtkB;
            this.compareDefB = compareDefB;
            this.compareSpaB = compareSpaB;
            this.compareSpdB = compareSpdB;
            this.compareSpeB = compareSpeB;

            this.hpValue = hpValue;
            this.hpCompare = hpCompare;
            this.atkValue = atkValue;
            this.atkCompare = atkCompare;
            this.defValue = defValue;
            this.defCompare = defCompare;
            this.spaValue = spaValue;
            this.spaCompare = spaCompare;
            this.spdValue = spdValue;
            this.spdCompare = spdCompare;
            this.speValue = speValue;
            this.speCompare = speCompare;
            Natures = natures;
            this.ability = ability;
            this.shinyOnly = shinyOnly;
            ABcheck = checkparents;
            GenderFilter = genderFilter;
        }

        public FrameCompare(
            uint compareHpA,
            uint compareAtkA,
            uint compareDefA,
            uint compareSpaA,
            uint compareSpdA,
            uint compareSpeA,
            uint compareHpB,
            uint compareAtkB,
            uint compareDefB,
            uint compareSpaB,
            uint compareSpdB,
            uint compareSpeB,
            IVFilter ivBase,
            List<uint> natures,
            int ability,
            bool shinyOnly,
            bool checkparents,
            GenderFilter genderFilter)
        {
            comparers = new List<FrameCompare>();

            this.compareHpA = compareHpA;
            this.compareAtkA = compareAtkA;
            this.compareDefA = compareDefA;
            this.compareSpaA = compareSpaA;
            this.compareSpdA = compareSpdA;
            this.compareSpeA = compareSpeA;

            this.compareHpB = compareHpB;
            this.compareAtkB = compareAtkB;
            this.compareDefB = compareDefB;
            this.compareSpaB = compareSpaB;
            this.compareSpdB = compareSpdB;
            this.compareSpeB = compareSpeB;

            hpValue = ivBase.hpValue;
            hpCompare = ivBase.hpCompare;
            atkValue = ivBase.atkValue;
            atkCompare = ivBase.atkCompare;
            defValue = ivBase.defValue;
            defCompare = ivBase.defCompare;
            spaValue = ivBase.spaValue;
            spaCompare = ivBase.spaCompare;
            spdValue = ivBase.spdValue;
            spdCompare = ivBase.spdCompare;
            speValue = ivBase.speValue;
            speCompare = ivBase.speCompare;
            Natures = natures;
            this.ability = ability;
            this.shinyOnly = shinyOnly;
            ABcheck = checkparents;
            GenderFilter = genderFilter;
        }

        public FrameCompare(
            uint hpValue,
            CompareType hpCompare,
            uint atkValue,
            CompareType atkCompare,
            uint defValue,
            CompareType defCompare,
            uint spaValue,
            CompareType spaCompare,
            uint spdValue,
            CompareType spdCompare,
            uint speValue,
            CompareType speCompare,
            List<uint> natures,
            int ability,
            bool shinyOnly,
            bool synchOnly,
            bool dreamWorld,
            List<int> encounterSlots,
            GenderFilter genderFilter)
        {
            this.hpValue = hpValue;
            this.hpCompare = hpCompare;
            this.atkValue = atkValue;
            this.atkCompare = atkCompare;
            this.defValue = defValue;
            this.defCompare = defCompare;
            this.spaValue = spaValue;
            this.spaCompare = spaCompare;
            this.spdValue = spdValue;
            this.spdCompare = spdCompare;
            this.speValue = speValue;
            this.speCompare = speCompare;
            Natures = natures;
            this.ability = ability;
            this.shinyOnly = shinyOnly;
            this.synchOnly = synchOnly;
            this.dreamWorld = dreamWorld;
            this.encounterSlots = encounterSlots;

            GenderFilter = genderFilter;
        }

        public FrameCompare(IVFilter ivBase,
                            List<uint> natures,
                            int ability,
                            bool shinyOnly,
                            bool synchOnly,
                            bool dreamWorld,
                            List<int> encounterSlots,
                            GenderFilter genderFilter)
        {
            if (ivBase != null)
            {
                hpValue = ivBase.hpValue;
                hpCompare = ivBase.hpCompare;
                atkValue = ivBase.atkValue;
                atkCompare = ivBase.atkCompare;
                defValue = ivBase.defValue;
                defCompare = ivBase.defCompare;
                spaValue = ivBase.spaValue;
                spaCompare = ivBase.spaCompare;
                spdValue = ivBase.spdValue;
                spdCompare = ivBase.spdCompare;
                speValue = ivBase.speValue;
                speCompare = ivBase.speCompare;
            }
            Natures = natures;
            this.ability = ability;
            this.shinyOnly = shinyOnly;
            this.synchOnly = synchOnly;
            this.dreamWorld = dreamWorld;
            this.encounterSlots = encounterSlots;

            GenderFilter = genderFilter;
        }

        // someday, dynamic frame comparers will be added
        public FrameCompare()
        {
            comparers = new List<FrameCompare>();
        }

        public GenderFilter GenderFilter { get; private set; }

        public List<uint> Natures { get; private set; }

        public void Add(FrameCompare comparer)
        {
            comparers.Add(comparer);
        }

        public void Remove(FrameCompare comparer)
        {
            comparers.Remove(comparer);
        }

        public bool CompareNature(uint testNature)
        {
            //  Check the nature first
            return Natures == null || Natures.Any(nature => nature == testNature);
        }

        public bool Compare(Frame frame)
        {
            //  Check the nature first
            if (Natures != null)
            {
                // If the frame can be synchronized, it doesn't need to pass the check
                if (frame.EncounterMod != EncounterMod.Synchronize)
                {
                    bool test = Natures.Any(nature => nature == frame.Nature);
                    if (!test)
                        return false;
                }
            }

            if (synchOnly)
            {
                if (!frame.Synchable)
                {
                    return false;
                }
            }

            if (!GenderFilter.Filter(frame.GenderValue))
                return false;

            if (!GenderFilter.Filter(frame.EncounterMod))
                return false;

            // For 3rd Gen eggs - if an egg is not generated on that frame, ignore it
            if (frame.FrameType == FrameType.RSBredLower)
            {
                // need to replace this now that we're using numbered natures
                if (frame.Number == 0)
                    return false;
            }

            //  Go through and check each IV against what the user has required.
            //  Skip if it's a FrameType that doesn't use IVs
            if (frame.FrameType != FrameType.Method5Natures &&
                frame.FrameType != FrameType.Gen4Normal &&
                frame.FrameType != FrameType.Gen4International &&
                frame.FrameType != FrameType.RSBredLower)
            {
                if (ABcheck)
                {
                    uint frameIv = frame.Hp;

                    if (frame.DisplayHp == "A") frameIv = compareHpA;
                    if (frame.DisplayHp == "B") frameIv = compareHpB;

                    if (!CompareIV(hpCompare, frameIv, hpValue))
                        return false;


                    frameIv = frame.Atk;
                    if (frame.DisplayAtk == "A") frameIv = compareAtkA;
                    if (frame.DisplayAtk == "B") frameIv = compareAtkB;

                    if (!CompareIV(atkCompare, frameIv, atkValue))
                        return false;


                    frameIv = frame.Def;
                    if (frame.DisplayDef == "A") frameIv = compareDefA;
                    if (frame.DisplayDef == "B") frameIv = compareDefB;


                    if (!CompareIV(defCompare, frameIv, defValue))
                        return false;


                    frameIv = frame.Spa;
                    if (frame.DisplaySpa == "A") frameIv = compareSpaA;
                    if (frame.DisplaySpa == "B") frameIv = compareSpaB;


                    if (!CompareIV(spaCompare, frameIv, spaValue))
                        return false;


                    frameIv = frame.Spd;
                    if (frame.DisplaySpd == "A") frameIv = compareSpdA;
                    if (frame.DisplaySpd == "B") frameIv = compareSpdB;


                    if (!CompareIV(spdCompare, frameIv, spdValue))
                        return false;


                    frameIv = frame.Spe;
                    if (frame.DisplaySpe == "A") frameIv = compareSpeA;
                    if (frame.DisplaySpe == "B") frameIv = compareSpeB;


                    if (!CompareIV(speCompare, frameIv, speValue))
                        return false;
                }

                else
                {
                    if (frame.DisplayHp != "A" && frame.DisplayHp != "B")
                    {
                        if (!CompareIV(hpCompare, frame.Hp, hpValue))
                            return false;
                    }

                    if (frame.DisplayAtk != "A" && frame.DisplayAtk != "B")
                    {
                        if (!CompareIV(atkCompare, frame.Atk, atkValue))
                            return false;
                    }

                    if (frame.DisplayDef != "A" && frame.DisplayDef != "B")
                    {
                        if (!CompareIV(defCompare, frame.Def, defValue))
                            return false;
                    }

                    if (frame.DisplaySpa != "A" && frame.DisplaySpa != "B")
                    {
                        if (!CompareIV(spaCompare, frame.Spa, spaValue))
                            return false;
                    }

                    if (frame.DisplaySpd != "A" && frame.DisplaySpd != "B")
                    {
                        if (!CompareIV(spdCompare, frame.Spd, spdValue))
                            return false;
                    }

                    if (frame.DisplaySpe != "A" && frame.DisplaySpe != "B")
                    {
                        if (!CompareIV(speCompare, frame.Spe, speValue))
                            return false;
                    }
                }
            }
            if (shinyOnly)
            {
                if (!frame.Shiny)
                {
                    return false;
                }
            }

            if (dreamWorld)
            {
                if (!frame.DreamAbility)
                {
                    return false;
                }
            }

            if (encounterSlots != null)
            {
                bool test = false;
                foreach (int slot in encounterSlots)
                {
                    if (slot == frame.EncounterSlot)
                    {
                        test = true;
                        break;
                    }
                }

                if (!test)
                    return false;
            }

            if (ability != -1)
            {
                if ((ability == 0) && (frame.Ability != 0))
                    return false;

                if ((ability == 1) && (frame.Ability != 1))
                    return false;
            }

            return true;
        }

        public bool CompareEggIVs(Frame frame)
        {
            // Checks to see if the frame has at least three IVs that fit criteria
            // For 5th Gen Eggs

            int passCount = 0;

            if (CompareIV(hpCompare, frame.Hp, hpValue))
                passCount++;

            if (CompareIV(atkCompare, frame.Atk, atkValue))
                passCount++;

            if (CompareIV(defCompare, frame.Def, defValue))
                passCount++;

            if (CompareIV(spaCompare, frame.Spa, spaValue))
                passCount++;

            if (CompareIV(spdCompare, frame.Spd, spdValue))
                passCount++;

            if (CompareIV(speCompare, frame.Spe, speValue))
                passCount++;

            return passCount >= 3;
        }


        public bool CompareIV(CompareType compare, uint frameIv, uint testIv)
        {
            bool passed = true;

            //  Anything set not to compare is considered pass
            if (compare != CompareType.None)
            {
                switch (compare)
                {
                    case CompareType.Equal:
                        if (frameIv != testIv)
                            passed = false;
                        break;

                    case CompareType.GtEqual:
                        if (frameIv < testIv)
                            passed = false;
                        break;

                    case CompareType.LtEqual:
                        if (frameIv > testIv)
                            passed = false;
                        break;

                    case CompareType.NotEqual:
                        if (frameIv == testIv)
                            passed = false;
                        break;

                    case CompareType.Even:
                        if ((frameIv & 1) != 0)
                            passed = false;

                        break;

                    case CompareType.Odd:
                        if ((frameIv & 1) == 0)
                            passed = false;

                        break;

                    case CompareType.Hidden:
                        if ((((frameIv + 2)&3) != 0) && (((frameIv + 5)&3) != 0))
                            passed = false;
                        break;

                    case CompareType.HiddenEven:
                        if (((frameIv + 2)&3) != 0)
                            passed = false;
                        break;

                    case CompareType.HiddenOdd:
                        if (((frameIv + 5)&3) != 0)
                            passed = false;
                        break;
                }
            }

            return passed;
        }

        public bool CompareIV(int index, uint frameIv)
        {
            bool passed = true;

            uint testIv;
            CompareType compare;
            switch (index)
            {
                case 0:
                    testIv = hpValue;
                    compare = hpCompare;
                    break;
                case 1:
                    testIv = atkValue;
                    compare = atkCompare;
                    break;
                case 2:
                    testIv = defValue;
                    compare = defCompare;
                    break;
                case 3:
                    testIv = spaValue;
                    compare = spaCompare;
                    break;
                case 4:
                    testIv = spdValue;
                    compare = spdCompare;
                    break;
                case 5:
                    testIv = speValue;
                    compare = speCompare;
                    break;
                default:
                    testIv = hpValue;
                    compare = hpCompare;
                    break;
            }

            //  Anything set not to compare is considered pass
            if (compare != CompareType.None)
            {
                switch (compare)
                {
                    case CompareType.Equal:
                        if (frameIv != testIv)
                            passed = false;
                        break;

                    case CompareType.GtEqual:
                        if (frameIv < testIv)
                            passed = false;
                        break;

                    case CompareType.LtEqual:
                        if (frameIv > testIv)
                            passed = false;
                        break;

                    case CompareType.NotEqual:
                        if (frameIv == testIv)
                            passed = false;
                        break;

                    case CompareType.Even:
                        if ((frameIv & 1) != 0)
                            passed = false;

                        break;

                    case CompareType.Odd:
                        if ((frameIv & 1) == 0)
                            passed = false;

                        break;

                    case CompareType.Hidden:
                        if ((((frameIv + 2)&3) != 0) && (((frameIv + 5)&3) != 0))
                            passed = false;
                        break;

                    case CompareType.HiddenEven:
                        if (((frameIv + 2)&3) != 0)
                            passed = false;
                        break;

                    case CompareType.HiddenOdd:
                        if (((frameIv + 5)&3) != 0)
                            passed = false;
                        break;

                    case CompareType.HiddenTrickRoom:
                        if (frameIv != 2 && frameIv != 3)
                            passed = false;
                        break;
                }
            }

            return passed;
        }
    }
}