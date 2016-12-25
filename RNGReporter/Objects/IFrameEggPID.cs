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
    public class IFrameEggPID
    {
        private bool shiny;
        public uint Seed { get; set; }

        public uint Pid { get; set; }

        public uint Offset { get; set; }

        public Frame Frame { get; set; }

        public string Taps
        {
            get { return (((Offset - 11)/12) - 1).ToString(); }
        }

        public string Flips
        {
            get { return ((Offset - 11)%12).ToString(); }
        }

        public string Nature
        {
            get { return Functions.NatureStrings((int) Functions.Nature(Pid)); }
        }

        public string Ability
        {
            get { return (Pid & 1).ToString(); }
        }

        public uint GenderValue
        {
            get { return Pid & 0xFF; }
        }

        public string Female50
        {
            get { return ((Pid & 0xFF) >= 127) ? "M" : "F"; }
        }

        public string Female125
        {
            get { return ((Pid & 0xFF) >= 31) ? "M" : "F"; }
        }

        public string Female25
        {
            get { return ((Pid & 0xFF) >= 63) ? "M" : "F"; }
        }

        public string Female75
        {
            get { return ((Pid & 0xFF) >= 191) ? "M" : "F"; }
        }

        public string FlipSequence
        {
            get { return CoinFlips.GetFlips(Seed, 10); }
        }

        public bool Shiny
        {
            get { return shiny; }
            set { shiny = value; }
        }

        public string ShinyDisplay
        {
            get { return shiny ? "!!!" : ""; }
        }
    }

    #region

    /*
    public class IFrameBreedingComparer : IComparer<IFrameBreeding>
    {
        public string CompareType = "Seed";
        public System.Windows.Forms.SortOrder sortOrder = System.Windows.Forms.SortOrder.Ascending;

        public int Compare(IFrameCapture x, IFrameCapture y)
        {
            int result;
            int direction = 1;

            if (sortOrder == System.Windows.Forms.SortOrder.Descending)
                direction = -1;

            switch (CompareType)
            {
                case "Seed":
                    ulong seedX = x.Seed;
                    ulong seedY = y.Seed;

                    result = direction * seedX.CompareTo(seedY);
                    if (result == 0)
                    {
                        result = direction * x.Offset.CompareTo(y.Offset);
                    }
                    return result;
                case "FullSeed":
                    ulong fullseedX = x.FullSeed;
                    ulong fullseedY = y.FullSeed;

                    result = direction * fullseedX.CompareTo(fullseedY);
                    if (result == 0)
                    {
                        result = direction * x.Offset.CompareTo(y.Offset);
                    }
                    return result;
                case "Offset":
                    return direction * x.Offset.CompareTo(y.Offset);
                case "NearestShiny":
                    return direction * x.NearestShiny.CompareTo(y.NearestShiny);
                case "Pid":
                    return direction * x.Pid.CompareTo(y.Pid);
                case "Ability":
                    return direction * x.Ability.CompareTo(y.Ability);
                case "Nature":
                    return direction * x.NatureNumber.CompareTo(y.NatureNumber);
                case "HiddenPowerPower":
                    return direction * x.HiddenPowerPower.CompareTo(y.HiddenPowerPower);
                case "TimeDate":
                    result = direction * x.TimeDate.CompareTo(y.TimeDate);
                    if (result == 0)
                    {
                        result = direction * x.Offset.CompareTo(y.Offset);
                    }

                    return result;
                case "EncounterSlot":
                    return direction * x.EncounterSlot.CompareTo(y.EncounterSlot);
                case "Hp":
                    result = direction * x.Hp.CompareTo(y.Hp);
                    if (result == 0)
                    {
                        result = direction * x.Atk.CompareTo(y.Atk);
                        if (result == 0)
                        {
                            result = direction * x.Def.CompareTo(y.Def);
                            if (result == 0)
                            {
                                result = direction * x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction * x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction * x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Atk":
                    result = direction * x.Atk.CompareTo(y.Atk);
                    if (result == 0)
                    {
                        result = direction * x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction * x.Def.CompareTo(y.Def);
                            if (result == 0)
                            {
                                result = direction * x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction * x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction * x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Def":
                    result = direction * x.Def.CompareTo(y.Def);
                    if (result == 0)
                    {
                        result = direction * x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction * x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction * x.SpA.CompareTo(y.SpA);
                                if (result == 0)
                                {
                                    result = direction * x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction * x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "SpA":
                    result = direction * x.SpA.CompareTo(y.SpA);
                    if (result == 0)
                    {
                        result = direction * x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction * x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction * x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction * x.SpD.CompareTo(y.SpD);
                                    if (result == 0)
                                    {
                                        result = direction * x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "SpD":
                    result = direction * x.SpD.CompareTo(y.SpD);
                    if (result == 0)
                    {
                        result = direction * x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction * x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction * x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction * x.SpA.CompareTo(y.SpA);
                                    if (result == 0)
                                    {
                                        result = direction * x.Spe.CompareTo(y.Spe);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                case "Spe":
                    result = direction * x.Spe.CompareTo(y.Spe);
                    if (result == 0)
                    {
                        result = direction * x.Hp.CompareTo(y.Hp);
                        if (result == 0)
                        {
                            result = direction * x.Atk.CompareTo(y.Atk);
                            if (result == 0)
                            {
                                result = direction * x.Def.CompareTo(y.Def);
                                if (result == 0)
                                {
                                    result = direction * x.SpA.CompareTo(y.SpA);
                                    if (result == 0)
                                    {
                                        result = direction * x.SpD.CompareTo(y.SpD);
                                    }
                                }
                            }
                        }
                    }
                    return result;
                default:
                    result = direction * System.String.Compare(x.GetType().GetProperty(CompareType).GetValue(x, null).ToString(),
                                                   y.GetType().GetProperty(CompareType).GetValue(y, null).ToString());

                    return result;
            }
        }
    }
     */

    #endregion
}