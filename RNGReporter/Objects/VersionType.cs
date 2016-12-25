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
    /*internal static class Versions
    {
        public const int VERSIONS = 18;
    }

    public enum VersionType
    {
        English_Black,
        English_White,
        Japanese_Black,
        Japanese_White,
        German_Black,
        German_White,
        Spanish_Black,
        Spanish_White,
        French_Black,
        French_White,
        Italian_Black,
        Italian_White,
        Korean_Black,
        Korean_White,
        Japanese_Black2,
        Japanese_White2,
        English_Black2,
        English_White2,
        English_Black_DSi,
        English_White_DSi,
        Japanese_Black_DSi,
        Japanese_White_DSi,
        German_Black_DSi,
        German_White_DSi,
        Spanish_Black_DSi,
        Spanish_White_DSi,
        French_Black_DSi,
        French_White_DSi,
        Italian_Black_DSi,
        Italian_White_DSi,
        Korean_Black_DSi,
        Korean_White_DSi,
        Japanese_Black2_DSi,
        Japanese_White2_DSi
    };*/

    public enum Version
    {
        Black,
        White,
        Black2,
        White2
    };

    public enum DSType
    {
        DS_Lite,
        DS_DSi,
        DS_3DS
    };
}