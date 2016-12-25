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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using RNGReporter.Properties;

namespace RNGReporter.Objects
{
    public static class Profiles
    {
        public static ProfileManager ProfileManager;

        static Profiles()
        {
            List = new List<Profile>();
            ProfileManager = new ProfileManager();
        }

        public static List<Profile> List { get; set; }

        public static void LoadProfiles(string fileName)
        {
            var deserializer = new XmlSerializer(typeof (List<Profile>));
            TextReader textReader = new StreamReader(fileName);
            try
            {
                List = (List<Profile>) deserializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception)
            {
                textReader.Close();
                MessageBox.Show("Corrupt or old format profiles detected. Unable to load.");
                Settings.Default.ProfileLocation = "profiles.xml";
            }
        }

        public static void LoadProfiles()
        {
            LoadProfiles(Settings.Default.ProfileLocation);
        }

        public static void SaveProfiles(string fileName)
        {
            var serializer = new XmlSerializer(typeof (List<Profile>));
            try
            {
                TextWriter textWriter = new StreamWriter(fileName);
                serializer.Serialize(textWriter, List);
                textWriter.Close();
            }
            catch (IOException)
            {
                // try to save it again
                fileName = Assembly.GetEntryAssembly().Location + "profiles.xml";
                TextWriter textWriter = new StreamWriter(fileName);
                serializer.Serialize(textWriter, List);
                textWriter.Close();
            }
            Settings.Default.ProfileLocation = fileName;
            Settings.Default.Save();
        }

        public static void SaveProfiles()
        {
            SaveProfiles(Settings.Default.ProfileLocation);
        }
    }

    public class Profile
    {
        private uint _timer0Max;
        private uint _timer0Min;
        public string Name { get; set; }

        public ushort ID { get; set; }

        public ushort SID { get; set; }

        public ulong MAC_Address { get; set; }

        public Version Version { get; set; }

        public string VersionStr
        {
            get { return (Version).ToString(); }
        }

        public Language Language { get; set; }

        public DSType DSType { get; set; }

        public uint VCount { get; set; }

        public uint VFrame { get; set; }

        public uint Timer0Min
        {
            get { return _timer0Min; }
            set
            {
                _timer0Min = value;
                if (value > _timer0Max)
                    _timer0Max = value;
            }
        }

        public uint Timer0Max
        {
            get { return _timer0Max; }
            set
            {
                _timer0Max = value;
                if (value < _timer0Min)
                    _timer0Min = value;
            }
        }

        public uint GxStat { get; set; }

        public string KeyString
        {
            get
            {
                if (Keypresses == 0) return "None";
                string keyString = "";
                byte b = 0x1;
                for (int i = 0; i < 4; ++i)
                {
                    if ((Keypresses & b) != 0)
                    {
                        if (keyString.Length > 0)
                            keyString += ", ";
                        keyString += i == 0 ? "None" : i.ToString();
                    }
                    b <<= 1;
                }
                return keyString;
            }
        }

        public byte Keypresses { get; set; }

        public bool SoftReset { get; set; }

        public bool SkipLR { get; set; }

        // note: BW2 only
        public bool MemoryLink { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string ProfileInformation()
        {
            return
                string.Format(
                    "{0} {1} Version: {2} ID: {3} SID: {4} Timer0: {5} - {6} VCount: {7} VFrame: {8} GxStat: {9} Keypresses: {10}",
                    DSType.ToString().Replace('_', ' '), MAC_Address.ToString("X"),
                    Language + " " + Version, ID, SID,
                    Timer0Min.ToString("X"), Timer0Max.ToString("X"), VCount.ToString("X"), VFrame.ToString("X"),
                    GxStat.ToString("X"), KeyString) + (SkipLR ? " (Skip L\\R)" : "");
        }

        public string ProfileInformationShort()
        {
            return string.Format("{0} {1} Version: {2} Timer0: {3} - {4} VCount: {5} VFrame: {6} GxStat: {7}",
                                 DSType.ToString().Replace('_', ' '), MAC_Address.ToString("X"),
                                 Language + " " + Version,
                                 Timer0Min.ToString("X"), Timer0Max.ToString("X"), VCount.ToString("X"),
                                 VFrame.ToString("X"), GxStat.ToString("X"));
        }

        public List<List<ButtonComboType>> GetKeypresses()
        {
            var keypresses = new List<List<ButtonComboType>>();
            byte b = 0x1;
            //have to start at 1 because a phantom element is added, not sure why
            for (int i = 0; i < 4; ++i)
            {
                if ((Keypresses & b) != 0)
                    keypresses.AddRange(Functions.KeypressCombos(i, SkipLR));
                b <<= 1;
            }
            return keypresses;
        }

        internal bool IsBW2()
        {
            return Version == Version.Black2 || Version == Version.White2;
        }
    }
}