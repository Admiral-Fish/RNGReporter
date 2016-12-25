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
    internal class Nazos
    {
        #region nazo data

        private static readonly uint[] englishBlack = {0xB0602102, 0xAC612102, 0xAC612102, 0xF8612102, 0xF8612102};
        private static readonly uint[] englishBlackDSi = {0x90017602, 0x8C027602, 0x8C027602, 0xD8027602, 0xD8027602};
        private static readonly uint[] englishWhite = {0xD0602102, 0xCC612102, 0xCC612102, 0x18622102, 0x18622102};
        private static readonly uint[] englishWhiteDSi = {0xB0017602, 0xAC027602, 0xAC027602, 0xF8027602, 0xF8027602};
        private static readonly uint[] englishBlack2 = {0xE8AE0902, 0xE99D0302, 0x10002002, 0x64002002, 0x64002002};
        private static readonly uint[] englishBlack2DSi = {0xE8AE0902, 0xE99D0302, 0x705F7A02, 0xC45F7A02, 0xC45F7A02};
        private static readonly uint[] englishWhite2 = {0x28AF0902, 0x159E0302, 0x50002002, 0xA4002002, 0xA4002002};
        private static readonly uint[] englishWhite2DSi = {0x28AF0902, 0x159E0302, 0x905E7A02, 0xE45E7A02, 0xE45E7A02};

        private static readonly uint[] japaneseBlack = {0x105F2102, 0x0C602102, 0x0C602102, 0x58602102, 0x58602102};
        private static readonly uint[] japaneseBlackDSi = {0x50117602, 0x4C127602, 0x4C127602, 0x98127602, 0x98127602};
        private static readonly uint[] japaneseWhite = {0x305F2102, 0x2C602102, 0x2C602102, 0x78602102, 0x78602102};
        private static readonly uint[] japaneseWhiteDSi = {0x50117602, 0x4C127602, 0x4C127602, 0x98127602, 0x98127602};
        private static readonly uint[] japaneseBlack2 = {0xDCA80902, 0xC99A0302, 0xB0F91F02, 0x04FA1F02, 0x04FA1F02};
        private static readonly uint[] japaneseBlack2DSi = {0xDCA80902, 0xC99A0302, 0x30A77A02, 0x84A77A02, 0x84A77A02};
        private static readonly uint[] japaneseWhite2 = {0xFCA80902, 0xF59A0302, 0xD0F91F02, 0x24FA1F02, 0x24FA1F02};
        private static readonly uint[] japaneseWhite2DSi = {0xFCA80902, 0xF59A0302, 0xF0A57A02, 0x44A67A02, 0x44A67A02};

        private static readonly uint[] germanBlack = {0xF05F2102, 0xEC602102, 0xEC602102, 0x38612102, 0x38612102};
        private static readonly uint[] germanBlackDSi = {0xF0027602, 0xEC037602, 0xEC037602, 0x38047602, 0x38047602};
        private static readonly uint[] germanWhite = {0x10602102, 0x0C612102, 0x0C612102, 0x58612102, 0x58612102};
        private static readonly uint[] germanWhiteDSi = {0xF0027602, 0xEC037602, 0xEC037602, 0x38047602, 0x38047602};
        private static readonly uint[] germanBlack2 = {0x28AE0902, 0x699D0302, 0x50FF1F02, 0xA4FF1F02, 0xA4FF1F02};
        private static readonly uint[] germanBlack2DSi = {0x28AE0902, 0x699D0302, 0x10617A02, 0x64617A02, 0x64617A02};
        private static readonly uint[] germanWhite2 = {0x48AE0902, 0x959D0302, 0x70FF1F02, 0xC4FF1F02, 0xC4FF1F02};
        private static readonly uint[] germanWhite2DSi = {0x48AE0902, 0x959D0302, 0x10607A02, 0x64607A02, 0x64607A02};

        private static readonly uint[] spanishBlack = {0x50602102, 0x4C612102, 0x4C612102, 0x98612102, 0x98612102};
        private static readonly uint[] spanishBlackDSi = {0xF0017602, 0xEC027602, 0xEC027602, 0x38037602, 0x38037602};
        private static readonly uint[] spanishWhite = {0x70602102, 0x6C612102, 0x6C612102, 0xB8612102, 0xB8612102};
        private static readonly uint[] spanishWhiteDSi = {0xF0017602, 0xEC027602, 0xEC027602, 0x38037602, 0x38037602};
        private static readonly uint[] spanishBlack2 = {0xA8AE0902, 0xB99D0302, 0xD0FF1F02, 0x24002002, 0x24002002};
        private static readonly uint[] spanishBlack2DSi = {0xA8AE0902, 0xB99D0302, 0x70607A02, 0xC4607A02, 0xC4607A02};
        private static readonly uint[] spanishWhite2 = {0xC8AE0902, 0xE59D0302, 0xF0FF1F02, 0x44002002, 0x44002002};
        private static readonly uint[] spanishWhite2DSi = {0xC8AE0902, 0xE59D0302, 0xB05F7A02, 0x04607A02, 0x04607A02};

        private static readonly uint[] frenchBlack = {0x30602102, 0x2C612102, 0x2C612102, 0x78612102, 0x78612102};
        private static readonly uint[] frenchBlackDSi = {0x30027602, 0x2C037602, 0x2C037602, 0x78037602, 0x78037602};
        private static readonly uint[] frenchWhite = {0x50602102, 0x4C612102, 0x4C612102, 0x98612102, 0x98612102};
        private static readonly uint[] frenchWhiteDSi = {0x50027602, 0x4C037602, 0x4C037602, 0x98037602, 0x98037602};
        private static readonly uint[] frenchBlack2 = {0x08AF0902, 0xF99D0302, 0x30002002, 0x84002002, 0x84002002};
        private static readonly uint[] frenchBlack2DSi = {0x08AF0902, 0xF99D0302, 0x905F7A02, 0xE45F7A02, 0xE45F7A02};
        private static readonly uint[] frenchWhite2 = {0x28AF0902, 0x259E0302, 0x50002002, 0xA4002002, 0xA4002002};
        private static readonly uint[] frenchWhite2DSi = {0x28AF0902, 0x259E0302, 0xF05E7A02, 0x445F7A02, 0x445F7A02};

        private static readonly uint[] italianBlack = {0xB05F2102, 0xAC602102, 0xAC602102, 0xF8602102, 0xF8602102};
        private static readonly uint[] italianBlackDSi = {0xD0017602, 0xCC027602, 0xCC027602, 0x18037602, 0x18037602};
        private static readonly uint[] italianWhite = {0xD05F2102, 0xCC602102, 0xCC602102, 0x18612102, 0x18612102};
        private static readonly uint[] italianWhiteDSi = {0xD0017602, 0xCC027602, 0xCC027602, 0x18037602, 0x18037602};
        private static readonly uint[] italianBlack2 = {0xE8AD0902, 0x699D0302, 0x10FF1F02, 0x64FF1F02, 0x64FF1F02};
        private static readonly uint[] italianBlack2DSi = {0xE8AD0902, 0x699D0302, 0x705F7A02, 0xC45F7A02, 0xC45F7A02};
        private static readonly uint[] italianWhite2 = {0x28AE0902, 0x959D0302, 0x50FF1F02, 0xA4FF1F02, 0xA4FF1F02};
        private static readonly uint[] italianWhite2DSi = {0x28AE0902, 0x959D0302, 0xD05E7A02, 0x245F7A02, 0x245F7A02};

        private static readonly uint[] koreanBlack = {0xB0672102, 0xAC682102, 0xAC682102, 0xF8682102, 0xF8682102};
        private static readonly uint[] koreanWhite = {0xB0672102, 0xAC682102, 0xAC682102, 0xF8682102, 0xF8682102};
        private static readonly uint[] koreanWhite2 = {0x2CB60902, 0x01A50302, 0x70072002, 0xC4072002, 0xC4072002};
        private static readonly uint[] koreanWhite2DSi = {0x2CB60902, 0x01A50302, 0xB0577A02, 0x04587A02, 0x04587A02};

        #endregion

        /// <summary>
        ///     Returns an array containing the game-version specific portion of the SHA-1 seed encryption message.
        /// </summary>
        /// <returns>an array of size 5 with the correct nazo data, null for games without data</returns>
        public static uint[] Nazo(Version version, Language language, DSType dsType)
        {
            switch (language)
            {
                case Language.English:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? englishBlack : englishBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? englishWhite : englishWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? englishBlack2 : englishBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? englishWhite2 : englishWhite2DSi;
                    }
                    break;
                case Language.Japanese:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? japaneseBlack : japaneseBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? japaneseWhite : japaneseWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? japaneseBlack2 : japaneseBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? japaneseWhite2 : japaneseWhite2DSi;
                    }
                    break;
                case Language.German:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? germanBlack : germanBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? germanWhite : germanWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? germanBlack2 : germanBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? germanWhite2 : germanWhite2DSi;
                    }
                    break;
                case Language.Spanish:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? spanishBlack : spanishBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? spanishWhite : spanishWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? spanishBlack2 : spanishBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? spanishWhite2 : spanishWhite2DSi;
                    }
                    break;
                case Language.French:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? frenchBlack : frenchBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? frenchWhite : frenchWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? frenchBlack2 : frenchBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? frenchWhite2 : frenchWhite2DSi;
                    }
                    break;
                case Language.Italian:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? italianBlack : italianBlackDSi;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? italianWhite : italianWhiteDSi;
                        case Version.Black2:
                            return dsType == DSType.DS_Lite ? italianBlack2 : italianBlack2DSi;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? italianWhite2 : italianWhite2DSi;
                    }
                    break;
                case Language.Korean:
                    switch (version)
                    {
                        case Version.Black:
                            return dsType == DSType.DS_Lite ? koreanBlack : null;
                        case Version.White:
                            return dsType == DSType.DS_Lite ? koreanWhite : null;
                        case Version.Black2:
                            return null;
                        case Version.White2:
                            return dsType == DSType.DS_Lite ? koreanWhite2 : koreanWhite2DSi;
                    }
                    break;
            }
            return null;
        }

        public static uint[] Nazo(Profile profile)
        {
            return Nazo(profile.Version, profile.Language, profile.DSType);
        }
    }
}