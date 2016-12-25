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
using System.Windows.Forms;

namespace RNGReporter.Objects
{
    public class IFrameCapture
    {
        public ulong Seed { get; set; }

        public uint Offset { get; set; }

        public Frame Frame { get; set; }

        public uint Hour
        {
            get { return ((uint) (Seed & 0x00FF0000) - (MACAddress & 0x00FF0000)) >> 16; }
        }

        public uint MACAddress { get; set; }

        public uint NearestShiny { get; set; }

        public DateTime TimeDate { get; set; }

        public uint Timer0 { get; set; }

        public List<ButtonComboType> KeyPresses { get; set; }

        public string Keypress
        {
            get
            {
                string keyString = "";
                int i = 0;
                foreach (ButtonComboType button in KeyPresses)
                {
                    keyString = keyString + Functions.buttonStrings[(int) button];
                    i++;
                    if (i < KeyPresses.Count)
                        keyString = keyString + "-";
                }

                return keyString;
            }
        }

        /*
        private ulong debug;
        public ulong Debug
        {
            get { return debug; }
            set { debug = value; }
        }
         * */

        public bool Synchable
        {
            get { return Frame.Synchable; }
        }

        public uint Advances { get; set; }

        public uint Delay { get; set; }

        public DateTime CSeedTime
        {
            get { return TimeDate.AddSeconds(Delay/60); }
        }

        public uint Pid
        {
            get { return Frame.Pid; }
        }

        public string Nature
        {
            get { return Functions.NatureStrings((int) Frame.Nature); }
        }

        public uint NatureNumber
        {
            get { return Frame.Nature; }
        }

        public uint Ability
        {
            get { return Frame.Ability; }
        }

        public string Female125
        {
            get { return Frame.Female125; }
        }

        public string Female25
        {
            get { return Frame.Female25; }
        }

        public string Female50
        {
            get { return Frame.Female50; }
        }

        public string Female75
        {
            get { return Frame.Female75; }
        }

        public uint HiddenPowerPower
        {
            get { return Frame.HiddenPowerPower; }
        }

        public string HiddenPowerType
        {
            get { return Frame.HiddenPowerType; }
        }

        public string ShinyDisplay
        {
            get { return Frame.ShinyDisplay; }
        }

        public bool DreamAbility
        {
            get { return Frame.DreamAbility; }
        }

        public uint Hp
        {
            get { return Frame.Hp; }
        }

        public uint Atk
        {
            get { return Frame.Atk; }
        }

        public uint Def
        {
            get { return Frame.Def; }
        }

        public uint SpA
        {
            get { return Frame.Spa; }
        }

        public uint SpD
        {
            get { return Frame.Spd; }
        }

        public uint Spe
        {
            get { return Frame.Spe; }
        }

        public string DisplayHp
        {
            get { return Frame.DisplayHp; }
        }

        public string DisplayAtk
        {
            get { return Frame.DisplayAtk; }
        }

        public string DisplayDef
        {
            get { return Frame.DisplayDef; }
        }

        public string DisplaySpa
        {
            get { return Frame.DisplaySpa; }
        }

        public string DisplaySpd
        {
            get { return Frame.DisplaySpd; }
        }

        public string DisplaySpe
        {
            get { return Frame.DisplaySpe; }
        }

        public string DisplayHpAlt
        {
            get { return Frame.DisplayHpAlt; }
        }

        public string DisplayAtkAlt
        {
            get { return Frame.DisplayAtkAlt; }
        }

        public string DisplayDefAlt
        {
            get { return Frame.DisplayDefAlt; }
        }

        public string DisplaySpaAlt
        {
            get { return Frame.DisplaySpaAlt; }
        }

        public string DisplaySpdAlt
        {
            get { return Frame.DisplaySpdAlt; }
        }

        public string DisplaySpeAlt
        {
            get { return Frame.DisplaySpeAlt; }
        }


        public string EncounterMod
        {
            get { return EncounterTypeCalc.StringMod(Frame.EncounterMod); }
        }

        public string EncounterSlot
        {
            get { return Frame.EncounterString; }
        }
    }

    public class IFrameCaptureComparer : IComparer<IFrameCapture>
    {
        public string CompareType = "Seed";
        public SortOrder sortOrder = SortOrder.Ascending;

        #region IComparer<IFrameCapture> Members

        public int Compare(IFrameCapture x, IFrameCapture y)
        {
            int result;
            int direction = 1;

            if (sortOrder == SortOrder.Descending)
                direction = -1;

            switch (CompareType)
            {
                case "Seed":
                    ulong seedX = x.Seed;
                    ulong seedY = y.Seed;

                    result = direction*seedX.CompareTo(seedY);
                    if (result == 0)
                    {
                        result = direction*x.Offset.CompareTo(y.Offset);
                    }
                    return result;
                case "Hour":
                    uint hourX = x.Hour;
                    uint hourY = y.Hour;

                    result = direction*hourX.CompareTo(hourY);
                    if (result == 0)
                    {
                        result = direction*x.Offset.CompareTo(y.Offset);
                    }
                    return result;
                case "Offset":
                    return direction*x.Offset.CompareTo(y.Offset);
                case "NearestShiny":
                    return direction*x.NearestShiny.CompareTo(y.NearestShiny);
                case "Pid":
                    return direction*x.Pid.CompareTo(y.Pid);
                case "Ability":
                    return direction*x.Ability.CompareTo(y.Ability);
                case "Nature":
                    return direction*x.NatureNumber.CompareTo(y.NatureNumber);
                case "HiddenPowerPower":
                    return direction*x.HiddenPowerPower.CompareTo(y.HiddenPowerPower);
                case "TimeDate":
                    result = direction*x.TimeDate.CompareTo(y.TimeDate);
                    if (result == 0)
                    {
                        result = direction*x.Offset.CompareTo(y.Offset);
                    }

                    return result;
                case "EncounterSlot":
                    return direction*x.Frame.EncounterSlot.CompareTo(y.Frame.EncounterSlot);
                case "Hp":
                    result = direction*x.Hp.CompareTo(y.Hp);
                    if (result == 0)
                    {
                        result = direction*x.Atk.CompareTo(y.Atk);
                        if (result == 0)
                        {
                            result = direction*x.Def.CompareTo(y.Def);
                            if (result == 0)
                            {
                                result = direction*x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction*x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction*x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Atk":
                    result = direction*x.Atk.CompareTo(y.Atk);
                    if (result == 0)
                    {
                        result = direction*x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction*x.Def.CompareTo(y.Def);
                            if (result == 0)
                            {
                                result = direction*x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction*x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction*x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Def":
                    result = direction*x.Def.CompareTo(y.Def);
                    if (result == 0)
                    {
                        result = direction*x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction*x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction*x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction*x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction*x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "SpA":
                    result = direction*x.SpA.CompareTo(y.SpA);
                    if (result == 0)
                    {
                        result = direction*x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction*x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction*x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction*x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction*x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "SpD":
                    result = direction*x.SpD.CompareTo(y.SpD);
                    if (result == 0)
                    {
                        result = direction*x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction*x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction*x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction*x.SpA.CompareTo(y.SpA);
                                    if (result == 0)
                                    {
                                        result = direction*x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Spe":
                    result = direction*x.Spe.CompareTo(y.Spe);
                    if (result == 0)
                    {
                        result = direction*x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction*x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction*x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction*x.SpA.CompareTo(y.SpA);
                                    if (result == 0)
                                    {
                                        result = direction*x.SpD.CompareTo(y.SpD);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                default:
                    //use ordinal due to better efficiency and because it uses the current culture
                    result = direction*
                             String.CompareOrdinal(x.GetType().GetProperty(CompareType).GetValue(x, null).ToString(),
                                                   y.GetType().GetProperty(CompareType).GetValue(y, null).ToString());

                    return result;
            }
        }

        #endregion
    }
}