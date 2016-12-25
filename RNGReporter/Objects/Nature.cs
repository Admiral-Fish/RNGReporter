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
    internal class Nature
    {
        public Nature(int number, double[] adjustments)
        {
            Number = number;
            Adjustments = adjustments;
        }

        public string Name
        {
            get { return Functions.NatureStrings(Number); }
        }

        public int Number { get; set; }

        public double[] Adjustments { get; set; }

        public override string ToString()
        {
            return Functions.NatureStrings(Number);
        }

        public static List<Nature> NatureCollection()
        {
            var natureCollection = new List<Nature>
                {
                    new Nature(0, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(1, new[] {1.0, 1.1, 0.9, 1.0, 1.0, 1.0}),
                    new Nature(2, new[] {1.0, 1.1, 1.0, 1.0, 1.0, 0.9}),
                    new Nature(3, new[] {1.0, 1.1, 1.0, 0.9, 1.0, 1.0}),
                    new Nature(4, new[] {1.0, 1.1, 1.0, 1.0, 0.9, 1.0}),
                    new Nature(5, new[] {1.0, 0.9, 1.1, 1.0, 1.0, 1.0}),
                    new Nature(6, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(7, new[] {1.0, 1.0, 1.1, 1.0, 1.0, 0.9}),
                    new Nature(8, new[] {1.0, 1.0, 1.1, 0.9, 1.0, 1.0}),
                    new Nature(9, new[] {1.0, 1.0, 1.1, 1.0, 0.9, 1.0}),
                    new Nature(10, new[] {1.0, 0.9, 1.0, 1.0, 1.0, 1.1}),
                    new Nature(11, new[] {1.0, 1.0, 0.9, 1.0, 1.0, 1.1}),
                    new Nature(12, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(13, new[] {1.0, 1.0, 1.0, 0.9, 1.0, 1.1}),
                    new Nature(14, new[] {1.0, 1.0, 1.0, 1.0, 0.9, 1.1}),
                    new Nature(15, new[] {1.0, 0.9, 1.0, 1.1, 1.0, 1.0}),
                    new Nature(16, new[] {1.0, 1.0, 0.9, 1.1, 1.0, 1.0}),
                    new Nature(17, new[] {1.0, 1.0, 1.0, 1.1, 1.0, 0.9}),
                    new Nature(18, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(19, new[] {1.0, 1.0, 1.0, 1.1, 0.9, 1.0}),
                    new Nature(20, new[] {1.0, 0.9, 1.0, 1.0, 1.1, 1.0}),
                    new Nature(21, new[] {1.0, 1.0, 0.9, 1.0, 1.1, 1.0}),
                    new Nature(22, new[] {1.0, 1.0, 1.0, 1.0, 1.1, 0.9}),
                    new Nature(23, new[] {1.0, 1.0, 1.0, 0.9, 1.1, 1.0}),
                    new Nature(24, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0})
                };

            return natureCollection;
        }

        public static List<Nature> NatureDropDownCollection()
        {
            var natureCollection = new List<Nature>
                {
                    new Nature(-2, new double[] {0, 0, 0, 0, 0, 0}),
                    new Nature(3, new[] {1.0, 1.1, 1.0, 0.9, 1.0, 1.0}),
                    new Nature(5, new[] {1.0, 0.9, 1.1, 1.0, 1.0, 1.0}),
                    new Nature(2, new[] {1.0, 1.1, 1.0, 1.0, 1.0, 0.9}),
                    new Nature(20, new[] {1.0, 0.9, 1.0, 1.0, 1.1, 1.0}),
                    new Nature(23, new[] {1.0, 1.0, 1.0, 0.9, 1.1, 1.0}),
                    new Nature(11, new[] {1.0, 1.0, 0.9, 1.0, 1.0, 1.1}),
                    new Nature(8, new[] {1.0, 1.0, 1.1, 0.9, 1.0, 1.0}),
                    new Nature(13, new[] {1.0, 1.0, 1.0, 0.9, 1.0, 1.1}),
                    new Nature(1, new[] {1.0, 1.1, 0.9, 1.0, 1.0, 1.0}),
                    new Nature(16, new[] {1.0, 1.0, 0.9, 1.1, 1.0, 1.0}),
                    new Nature(15, new[] {1.0, 0.9, 1.0, 1.1, 1.0, 1.0}),
                    new Nature(14, new[] {1.0, 1.0, 1.0, 1.0, 0.9, 1.1}),
                    new Nature(4, new[] {1.0, 1.1, 1.0, 1.0, 0.9, 1.0}),
                    new Nature(17, new[] {1.0, 1.0, 1.0, 1.1, 1.0, 0.9}),
                    new Nature(19, new[] {1.0, 1.0, 1.0, 1.1, 0.9, 1.0}),
                    new Nature(7, new[] {1.0, 1.0, 1.1, 1.0, 1.0, 0.9}),
                    new Nature(22, new[] {1.0, 1.0, 1.0, 1.0, 1.1, 0.9}),
                    new Nature(10, new[] {1.0, 0.9, 1.0, 1.0, 1.0, 1.1}),
                    new Nature(21, new[] {1.0, 1.0, 0.9, 1.0, 1.1, 1.0}),
                    new Nature(9, new[] {1.0, 1.0, 1.1, 1.0, 0.9, 1.0}),
                    new Nature(18, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(6, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(0, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(24, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0}),
                    new Nature(12, new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0})
                };

            return natureCollection;
        }

        /*
        public static ComboBoxItem[] NatureDropDownCollection()
        {
            ComboBoxItem[] natures = new ComboBoxItem[] {
                new ComboBoxItem("Any", -1),
                new ComboBoxItem(Functions.NatureStrings(3), 3),
                new ComboBoxItem(Functions.NatureStrings(5), 5),
                new ComboBoxItem(Functions.NatureStrings(2), 2),
                new ComboBoxItem(Functions.NatureStrings(20), 20),
                new ComboBoxItem(Functions.NatureStrings(23), 23),
                new ComboBoxItem(Functions.NatureStrings(11), 11),
                new ComboBoxItem(Functions.NatureStrings(8), 8),
                new ComboBoxItem(Functions.NatureStrings(13), 13),
                new ComboBoxItem(Functions.NatureStrings(1), 1),
                new ComboBoxItem(Functions.NatureStrings(16), 16),
                new ComboBoxItem(Functions.NatureStrings(15), 15),
                new ComboBoxItem(Functions.NatureStrings(14), 14),
                new ComboBoxItem(Functions.NatureStrings(4), 4),
                new ComboBoxItem(Functions.NatureStrings(17), 17),
                new ComboBoxItem(Functions.NatureStrings(19), 19),
                new ComboBoxItem(Functions.NatureStrings(7), 7),
                new ComboBoxItem(Functions.NatureStrings(22), 22),
                new ComboBoxItem(Functions.NatureStrings(10), 10),
                new ComboBoxItem(Functions.NatureStrings(21), 21),
                new ComboBoxItem(Functions.NatureStrings(9), 9),
                new ComboBoxItem(Functions.NatureStrings(18), 18),
                new ComboBoxItem(Functions.NatureStrings(6), 6),
                new ComboBoxItem(Functions.NatureStrings(0), 0),
                new ComboBoxItem(Functions.NatureStrings(24), 24),
                new ComboBoxItem(Functions.NatureStrings(12), 12)
            };

            return natures;
        }
         */

        public static List<Nature> NatureDropDownCollectionSynch()
        {
            List<Nature> natures = NatureDropDownCollection();
            natures[0] = new Nature(-1, new double[] {0, 0, 0, 0, 0, 0});

            return natures;
        }

        public static Nature[] NatureDropDownCollectionSearchNatures()
        {
            // note: we may want to convert this to return an Object array instead to prevent covariance issues
            // the other option would be to directly make the nature dropdop collection and just set it as such
            List<Nature> natures = NatureDropDownCollection();
            natures.RemoveAt(0);

            return natures.ToArray();
        }

        /*
        public static ComboBoxItem[] NatureDropDownCollectionSearchNatures()
        {
            ComboBoxItem[] natures = new ComboBoxItem[] {
                new ComboBoxItem(Functions.NatureStrings(3), 3),
                new ComboBoxItem(Functions.NatureStrings(5), 5),
                new ComboBoxItem(Functions.NatureStrings(2), 2),
                new ComboBoxItem(Functions.NatureStrings(20), 20),
                new ComboBoxItem(Functions.NatureStrings(23), 23),
                new ComboBoxItem(Functions.NatureStrings(11), 11),
                new ComboBoxItem(Functions.NatureStrings(8), 8),
                new ComboBoxItem(Functions.NatureStrings(13), 13),
                new ComboBoxItem(Functions.NatureStrings(1), 1),
                new ComboBoxItem(Functions.NatureStrings(16), 16),
                new ComboBoxItem(Functions.NatureStrings(15), 15),
                new ComboBoxItem(Functions.NatureStrings(14), 14),
                new ComboBoxItem(Functions.NatureStrings(4), 4),
                new ComboBoxItem(Functions.NatureStrings(17), 17),
                new ComboBoxItem(Functions.NatureStrings(19), 19),
                new ComboBoxItem(Functions.NatureStrings(7), 7),
                new ComboBoxItem(Functions.NatureStrings(22), 22),
                new ComboBoxItem(Functions.NatureStrings(10), 10),
                new ComboBoxItem(Functions.NatureStrings(21), 21),
                new ComboBoxItem(Functions.NatureStrings(9), 9),
                new ComboBoxItem(Functions.NatureStrings(18), 18),
                new ComboBoxItem(Functions.NatureStrings(6), 6),
                new ComboBoxItem(Functions.NatureStrings(0), 0),
                new ComboBoxItem(Functions.NatureStrings(24), 24),
                new ComboBoxItem(Functions.NatureStrings(12), 12)
            };

            return natures;
        }
         */
    }
}