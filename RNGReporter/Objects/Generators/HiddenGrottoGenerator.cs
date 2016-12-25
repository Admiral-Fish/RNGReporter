using System;
using System.Collections.Generic;

namespace RNGReporter.Objects.Generators
{
    internal class HiddenGrottoGenerator
    {
        private static readonly uint[] slots = {0x1, 0x4, 0xF, 0x1, 0x4, 0xA, 0x19, 0x1, 0x4, 0xA};
        public uint MaxAdvances { get; set; }
        public ushort OpenHollows { get; set; }
        public List<ushort> Hollows { get; set; }
        public List<ushort> Slots { get; set; }
        public List<ushort> SubSlots { get; set; }
        public char Gender { get; set; }
        public ushort GenderRatio { get; set; }

        public List<Hollow> Generate(ulong seed, Profile profile)
        {
            var hollows = new List<Hollow>();
            uint startingFrame = Functions.initialPIDRNGBW2(seed, profile);
            uint[] rngArray = FillRNGArray(startingFrame, seed);

            for (uint i = 0; i <= MaxAdvances; ++i)
            {
                uint offset = i;
                for (ushort h = 0; h < OpenHollows; ++h)
                {
                    // hollow check
                    if (Functions.RNGRange(rngArray[offset++], 100) >= 5) continue;
                    Hollow hollow = GenerateHollow(rngArray, offset);
                    // if it's not what we're searching for skip it, null = show all
                    if (Hollows != null && !Hollows.Contains(h)) continue;
                    if ((Slots != null && !Slots.Contains(hollow.Slot)) ||
                        (SubSlots != null && !SubSlots.Contains(hollow.SubSlot))) continue;
                    // gender filter goes here
                    switch (GenderRatio)
                    {
                        case 60:
                            if (hollow.Female60 != Gender) continue;
                            break;
                        case 30:
                            if (hollow.Female30 != Gender) continue;
                            break;
                        case 10:
                            if (hollow.Female10 != Gender) continue;
                            break;
                        case 5:
                            if (hollow.Female5 != Gender) continue;
                            break;
                    }

                    hollow.Seed = seed;
                    hollow.StartingFrame = startingFrame;
                    hollow.Frame = startingFrame + i;
                    hollow.HollowNumber = h;
                    hollows.Add(hollow);
                }
            }

            return hollows;
        }

        private uint[] FillRNGArray(uint startingFrame, ulong seed)
        {
            var rng = new BWRng(seed);
            for (uint i = 1; i < startingFrame; ++i)
            {
                rng.GetNext64BitNumber();
            }
            // max of 4 calls per hollow
            var rngArray = new uint[MaxAdvances + OpenHollows*4];
            for (int i = 0; i < rngArray.Length; ++i)
            {
                rngArray[i] = rng.GetNext32BitNumber();
            }
            return rngArray;
        }

        public Hollow GenerateHollow(uint[] rngArray, uint rngStart)
        {
            var hollow = new Hollow {SubSlot = (ushort) Functions.RNGRange(rngArray[rngStart++], 4)};
            uint rslot = Functions.RNGRange(rngArray[rngStart++], 100) + 1;
            ushort slot = 0;
            for (uint sum = 0; slot < 10; ++slot)
            {
                sum += slots[slot];
                if (sum >= rslot) break;
            }
            // in the game there is a check here and if slot = 2
            // is possible to loop up to 2 more times
            // however it looks like that actually never happens
            // so we are skipping it
            hollow.Slot = slot;
            hollow.Gender = Functions.RNGRange(rngArray[rngStart], 100);
            return hollow;
        }
    }

    public class Hollow
    {
        public ushort Slot { get; set; }
        public ushort SubSlot { get; set; }
        public uint Gender { get; set; }
        public ulong Seed { get; set; }
        public uint StartingFrame { get; set; }
        public uint Frame { get; set; }
        public uint HollowNumber { get; set; }

        public char Female60
        {
            get { return Gender < 60 ? 'F' : 'M'; }
        }

        public char Female30
        {
            get { return Gender < 30 ? 'F' : 'M'; }
        }

        public char Female10
        {
            get { return Gender < 10 ? 'F' : 'M'; }
        }

        public char Female5
        {
            get { return Gender < 5 ? 'F' : 'M'; }
        }

        public DateTime DateTime { get; set; }
        public uint Timer0 { get; set; }
        public List<ButtonComboType> Keypresses { get; set; }

        public string Keypress
        {
            get
            {
                string keyString = "";
                int i = 0;
                // make for loop for optimizations
                foreach (ButtonComboType button in Keypresses)
                {
                    if (i > 0) keyString += "-";
                    keyString = keyString + Functions.buttonStrings[(int) button];
                    i++;
                }

                return keyString;
            }
        }
    }
}