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
    internal class CalculateChainSidPokemon
    {
        public CalculateChainSidPokemon(
            string ivs,
            string nature,
            string ability,
            string gender)
        {
            Ivs = ivs;
            Nature = nature;
            Ability = ability;
            Gender = gender;
        }

        public string Ivs { get; set; }

        public string Nature { get; set; }

        public string Ability { get; set; }

        public string Gender { get; set; }
    }
}