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
    internal class Characteristic
    {
        private readonly uint index;

        public Characteristic(uint index, uint affectedStat, uint mod5result)
        {
            this.index = index;
            AffectedStat = affectedStat;
            Mod5result = mod5result;
        }

        public string Name
        {
            get { return Functions.characteristicStrings(index); }
        }

        public uint AffectedStat { get; set; }

        public uint Mod5result { get; set; }

        public override string ToString()
        {
            return Functions.characteristicStrings(index);
        }

        public static List<object> CharacteristicCollection()
        {
            var characteristicCollection =
                new List<object>
                    {
                        "NONE",
                        new Characteristic(0, 0, 0),
                        new Characteristic(1, 0, 1),
                        new Characteristic(2, 0, 2),
                        new Characteristic(3, 0, 3),
                        new Characteristic(4, 0, 4),
                        new Characteristic(5, 1, 0),
                        new Characteristic(6, 1, 1),
                        new Characteristic(7, 1, 2),
                        new Characteristic(8, 1, 3),
                        new Characteristic(9, 1, 4),
                        new Characteristic(10, 2, 0),
                        new Characteristic(11, 2, 1),
                        new Characteristic(12, 2, 2),
                        new Characteristic(13, 2, 3),
                        new Characteristic(14, 2, 4),
                        new Characteristic(15, 5, 0),
                        new Characteristic(16, 5, 1),
                        new Characteristic(17, 5, 2),
                        new Characteristic(18, 5, 3),
                        new Characteristic(19, 5, 4),
                        new Characteristic(20, 3, 0),
                        new Characteristic(21, 3, 1),
                        new Characteristic(22, 3, 2),
                        new Characteristic(23, 3, 3),
                        new Characteristic(24, 3, 4),
                        new Characteristic(25, 4, 0),
                        new Characteristic(26, 4, 1),
                        new Characteristic(27, 4, 2),
                        new Characteristic(28, 4, 3),
                        new Characteristic(29, 4, 4)
                    };

            return characteristicCollection;
        }
    }
}