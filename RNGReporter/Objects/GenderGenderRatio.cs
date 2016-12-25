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
    internal class GenderGenderRatio
    {
        public GenderGenderRatio(string name, string shortName, uint lowValue, uint highValue)
        {
            Name = name;
            ShortName = shortName;
            LowValue = lowValue;
            HighValue = highValue;
        }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public uint LowValue { get; set; }

        public uint HighValue { get; set; }

        public bool Matches(uint genderNumber)
        {
            bool matches = genderNumber >= LowValue && genderNumber <= HighValue;

            return matches;
        }

        // This is the gender ratio collection for Chain to SID
        // Not going to mess with old code, so we'll leave it as is
        public static List<GenderGenderRatio> GenderGenderRatioCollection()
        {
            var genderGenderRatioCollection =
                new List<GenderGenderRatio>
                    {
                        new GenderGenderRatio("Genderless", "Genderless", 0, 255),
                        new GenderGenderRatio("Male (50% Male / 50% Female)", "M 50% F", 127, 255),
                        new GenderGenderRatio("Female  (50% Male / 50% Female)", "F 50^ F", 0, 126),
                        new GenderGenderRatio("Male (25% Male / 75% Female)", "M 75% F", 191, 255),
                        new GenderGenderRatio("Female (25% Male / 75% Female)", "F 75% F", 0, 190),
                        new GenderGenderRatio("Male (75% Male / 25% Female)", "M 25% F", 64, 255),
                        new GenderGenderRatio("Female  (75% Male / 25% Female)", "F 25% F", 0, 63),
                        new GenderGenderRatio("Male (87.5% Male / 12.5% Female)", "M 12.5 F", 31, 255),
                        new GenderGenderRatio("Female (87.5% Male / 12.5% Female)", "F 12.5 F", 0, 30),
                        new GenderGenderRatio("Male (100% Male)", "M 100% M", 0, 255),
                        new GenderGenderRatio("Female (100% Female)", "F 100% F", 0, 255)
                    };

            return genderGenderRatioCollection;
        }

        // This is the gender ratio collection we'll use for frame filters
        public static List<GenderGenderRatio> GenderRatioCollection()
        {
            var genderRatioCollection =
                new List<GenderGenderRatio>
                    {
                        new GenderGenderRatio("Genderless", "Genderless", 0, 255),
                        new GenderGenderRatio("Male (50% Male / 50% Female)", "M 50% F", 127, 255),
                        new GenderGenderRatio("Female  (50% Male / 50% Female)", "F 50% F", 0, 126),
                        new GenderGenderRatio("Male (25% Male / 75% Female)", "M 75% F", 191, 255),
                        new GenderGenderRatio("Female (25% Male / 75% Female)", "F 75% F", 0, 190),
                        new GenderGenderRatio("Male (75% Male / 25% Female)", "M 25% F", 63, 255),
                        new GenderGenderRatio("Female  (75% Male / 25% Female)", "F 25% F", 0, 62),
                        new GenderGenderRatio("Male (87.5% Male / 12.5% Female)", "M 12.5% F", 31, 255),
                        new GenderGenderRatio("Female (87.5% Male / 12.5% Female)", "F 12.5% F", 0, 30),
                        new GenderGenderRatio("Male (100% Male)", "M 100% M", 0, 0),
                        new GenderGenderRatio("Female (100% Female)", "F 100% F", 255, 255)
                    };

            return genderRatioCollection;
        }
    }

    internal enum GenderCriteria
    {
        DontCareGenderless,
        Male,
        Female
    }

    internal class GenderFilter
    {
        public GenderFilter(string name, uint genderValue, GenderCriteria genderCriteria)
        {
            Name = name;
            GenderValue = genderValue;
            GenderCriteria = genderCriteria;
        }

        public GenderFilter()
        {
            // blank constructor for the NoFilter class
        }

        public string Name { get; protected set; }

        public uint GenderValue { get; protected set; }

        public GenderCriteria GenderCriteria { get; protected set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Filter(uint genderValue)
        {
            if (GenderCriteria == GenderCriteria.Male)
            {
                return genderValue >= GenderValue;
            }

            if (GenderCriteria == GenderCriteria.Female)
            {
                return genderValue < GenderValue;
            }

            return true;
        }

        public bool Filter(EncounterMod encounterMod)
        {
            switch (encounterMod)
            {
                case EncounterMod.CuteCharmFemale:
                    return GenderCriteria != GenderCriteria.Male && GenderValue != 0 && GenderValue != 254;
                case EncounterMod.CuteCharm875M:
                    return GenderValue == 255 || GenderValue == 31;
                case EncounterMod.CuteCharm50M:
                    return GenderValue == 255 || GenderValue == 127;
                case EncounterMod.CuteCharm75M:
                    return GenderValue == 255 || GenderValue == 63;
                case EncounterMod.CuteCharm25M:
                    return GenderValue == 255 || GenderValue == 191;
                case EncounterMod.CuteCharm125F:
                    return GenderValue == 255 || GenderValue == 31;
                case EncounterMod.CuteCharm50F:
                    return GenderValue == 255 || GenderValue == 127;
                case EncounterMod.CuteCharm75F:
                    return GenderValue == 255 || GenderValue == 191;
                case EncounterMod.CuteCharm25F:
                    return GenderValue == 255 || GenderValue == 63;
                default:
                    return true;
            }
        }

        public static List<GenderFilter> GenderFilterCollection()
        {
            var GenderFilterCollection =
                new List<GenderFilter>
                    {
                        new NoGenderFilter("Don't Care / Genderless / Fixed Gender"),
                        new GenderFilter("Male (50% Male / 50% Female)", 127, GenderCriteria.Male),
                        new GenderFilter("Female (50% Male / 50% Female)", 127, GenderCriteria.Female),
                        new GenderFilter("Male (25% Male / 75% Female)", 191, GenderCriteria.Male),
                        new GenderFilter("Female (25% Male / 75% Female)", 191, GenderCriteria.Female),
                        new GenderFilter("Male (75% Male / 25% Female)", 63, GenderCriteria.Male),
                        new GenderFilter("Female (75% Male / 25% Female)", 63, GenderCriteria.Female),
                        new GenderFilter("Male (87.5% Male / 12.5% Female)", 31, GenderCriteria.Male),
                        new GenderFilter("Female (87.5% Male / 12.5% Female)", 31, GenderCriteria.Female)
                    };

            return GenderFilterCollection;
        }

        // This is the collection we use for the main window
        // Entralink PIDs need to know gender values
        // So we added Genderless and 100% male\female entries
        public static List<GenderFilter> GenderFilterCollectionMain()
        {
            var GenderFilterCollection =
                new List<GenderFilter>
                    {
                        new GenderFilter("Don't Care / Genderless", 255, GenderCriteria.DontCareGenderless),
                        new GenderFilter("Male (50% Male / 50% Female)", 127, GenderCriteria.Male),
                        new GenderFilter("Female (50% Male / 50% Female)", 127, GenderCriteria.Female),
                        new GenderFilter("Male (25% Male / 75% Female)", 191, GenderCriteria.Male),
                        new GenderFilter("Female (25% Male / 75% Female)", 191, GenderCriteria.Female),
                        new GenderFilter("Male (75% Male / 25% Female)", 63, GenderCriteria.Male),
                        new GenderFilter("Female (75% Male / 25% Female)", 63, GenderCriteria.Female),
                        new GenderFilter("Male (87.5% Male / 12.5% Female)", 31, GenderCriteria.Male),
                        new GenderFilter("Female (87.5% Male / 12.5% Female)", 31, GenderCriteria.Female),
                        new GenderFilter("Male (100% Male)", 0, GenderCriteria.DontCareGenderless),
                        new GenderFilter("Female (100% Female)", 254, GenderCriteria.DontCareGenderless)
                    };

            return GenderFilterCollection;
        }
    }

    internal class NoGenderFilter : GenderFilter
    {
        public NoGenderFilter()
        {
        }

        public NoGenderFilter(string name)
        {
            Name = name;
        }

        public new bool Filter(uint genderValue)
        {
            return true;
        }
    }
}