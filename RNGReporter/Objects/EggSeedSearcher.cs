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
using System.Windows.Forms;

namespace RNGReporter.Objects
{
    internal static class EggSeedSearcher
    {
        public static bool LoadSeeds(string seedFile, out List<ulong> seedSet)
        {
            seedSet = new List<ulong>();

            if (!File.Exists(seedFile)) return false;

            char[] header = "SEED_DELTA_FILE".ToCharArray();
            const uint version = 0x0100;

            try
            {
                FileStream reader = File.OpenRead(seedFile);
                var buffer = new byte[16];
                //check the header
                reader.Read(buffer, 0, header.Length);
                if (!AreEquivalentArray(buffer, header)) throw new Exception("Bad cache file");
                //check the version
                //note: this is very hackish and should be fixed asap
                reader.Read(buffer, 0, 3);
                ushort fileVersion = BitConverter.ToUInt16(buffer, 1);
                if (fileVersion != version) throw new Exception("File versions do not match");

                uint fullseed = 0;
                uint seedCount = 0;

                int chunkPos = 0;
                ulong chunk = 0;

                while (reader.Position < reader.Length)
                {
                    uint delta = 0;
                    var b = (byte) reader.ReadByte();
                    //signed for << operation
                    int pos = 0;
                    while ((b > 0x7f) && reader.Position < reader.Length)
                    {
                        delta |= (uint) ((b & 0x7f) << pos);
                        b = (byte) reader.ReadByte();
                        pos += 7;
                    }

                    if (b > 0x7f) throw new Exception("Bad cache file");

                    delta |= (uint) (b << pos);

                    //get next seed
                    fullseed += delta;
                    ++seedCount;

                    //determine next seed data chunk to use
                    var nextChunkPos = (int) (fullseed >> 6);
                    if (nextChunkPos != chunkPos)
                    {
                        //new chunk, so store previous chunk
                        while (chunkPos >= seedSet.Count)
                            seedSet.Add(0);
                        seedSet[chunkPos] = chunk;
                        chunk = 0;
                        chunkPos = nextChunkPos;
                    }

                    //mark bit in chunk
                    chunk |= 0x1UL << (int) (fullseed & 0x3f);
                }

                //write final chunk
                seedSet[chunkPos] = chunk;

                if (reader.Position < reader.Length) throw new Exception("Bad cache file");

                //add check for seed count here
            }

            catch (OutOfMemoryException)
            {
                MessageBox.Show(string.Format(
                    "You do not have enough free memory to load {0} for the fast egg search.", seedFile));
            }

            catch (Exception e)
            {
                MessageBox.Show(string.Format("Problem loading {0} for the fast egg search:\r\n{1}", seedFile, e.Message));
                return false;
            }

            return true;
        }

        public static bool AreEquivalentArray(byte[] a, char[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                if ((char) a[i] != b[i])
                    return false;
            }
            return true;
        }
    }
}