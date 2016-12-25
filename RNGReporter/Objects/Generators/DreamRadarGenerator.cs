using System;
using System.Collections.Generic;

namespace RNGReporter.Objects.Generators
{
    internal class DreamRadarGenerator
    {
        public bool Genderless
        {
            get { return GenderThreashold == 255; }
            set
            {
                if (value) GenderThreashold = 255;
                else throw (new Exception("Unable to set to not Genderless"));
            }
        }

        public bool Male { get; set; }
        public uint GenderThreashold { get; set; }
        public uint StartingFrame { get; set; }
        public uint MaxFrame { get; set; }
        public uint TargetSlot { get; set; }
        public bool Genie { get; set; }

        public List<DreamRadarFrame> Generate(ulong seed, Profile profile)
        {
            var frames = new List<DreamRadarFrame>();
            // Build the PIDRNG
            uint initialFrame = Functions.initialPIDRNG(seed, profile);
            var pidrng = new BWRng(seed);
            pidrng.Advance(initialFrame);

            // Build the MTRNG
            // todo: use fast MTRNG when available
            var ivrng = new MersenneTwister((uint) (seed >> 32));
            // advance 8 frames for BW2
            for (uint i = 0; i < 10; ++i) ivrng.Next();

            // one single advancement for entering the menu
            pidrng.GetNext64BitNumber();

            var spins = new List<DreamRadarFrame.Spin>();
            // initial advances
            for (uint i = 0; i < initialFrame; ++i) Advance(pidrng, ivrng, spins);

            // slot advances
            // we're always doing the slot 1 advance here
            pidrng.GetNext64BitNumber();

            for (uint i = 1; i < TargetSlot; ++i) SlotAdvances(pidrng, ivrng);

            for (uint i = initialFrame; i <= MaxFrame; ++i)
            {
                DreamRadarFrame frame = GeneratePokemon(pidrng, ivrng);

                var arrSpins = new DreamRadarFrame.Spin[spins.Count];
                spins.CopyTo(arrSpins);
                frame.Spins = arrSpins;

                // add checks/comparisons on the frame here
                // nature/IVs
                frames.Add(frame);
                Advance(pidrng, ivrng, spins);
            }

            return frames;
        }

        private void Advance(BWRng pidrng, MersenneTwister ivrng, List<DreamRadarFrame.Spin> spins)
        {
            // first PIDRNG advance = spin
            spins.Add((DreamRadarFrame.Spin) pidrng.GetNext32BitNumber(8));
            pidrng.GetNext64BitNumber();
            ivrng.Next();
            ivrng.Next();
        }

        private DreamRadarFrame GeneratePokemon(BWRng pidrng2, MersenneTwister ivrng2)
        {
            var pidrng = new BWRng(pidrng2.Seed);
            var ivrng = new MersenneTwister(ivrng2);
            var frame = new DreamRadarFrame();

            frame.Pid = GeneratePID(pidrng);
            // two unknown advances
            pidrng.GetNext64BitNumber();
            pidrng.GetNext64BitNumber();
            frame.Nature = pidrng.GetNext32BitNumber(25);
            // IVs
            frame.Hp = ivrng.Next() >> 27;
            frame.Atk = ivrng.Next() >> 27;
            frame.Def = ivrng.Next() >> 27;
            frame.Spa = ivrng.Next() >> 27;
            frame.Spd = ivrng.Next() >> 27;
            frame.Spe = ivrng.Next() >> 27;

            return frame;
        }

        private uint GeneratePID(BWRng rng)
        {
            uint pid = rng.GetNext32BitNumber();
            if (Genderless) return pid;
            // clear out the lower byte
            pid &= 0xFFFFFF00;
            switch (GenderThreashold)
            {
                    // all male
                case 0:
                    pid += rng.GetNext32BitNumber(0xF6) + 8;
                    break;
                    // all female
                case 254:
                    pid += rng.GetNext32BitNumber(0x8) + 1;
                    break;
                default:
                    if (Male)
                        pid += rng.GetNext32BitNumber(0xFE - GenderThreashold) + GenderThreashold;
                    else
                        pid += rng.GetNext32BitNumber(GenderThreashold - 1) + 1;
                    break;
            }
            return pid;
        }

        // todo: remove this
        private static void SlotAdvances(BWRng pidrng, MersenneTwister ivrng)
        {
            pidrng.GetNext64BitNumber();
            pidrng.GetNext64BitNumber();
            pidrng.GetNext64BitNumber();
            pidrng.GetNext64BitNumber();
            pidrng.GetNext64BitNumber();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
            ivrng.Next();
        }
    }

    public class DreamRadarFrame : Frame
    {
        public enum Spin
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft
        }

        // need to abstract the spins
        private readonly char[] spins = {'↑', '↗', '→', '↘', '↓', '↙', '←', '↖'};

        public Spin[] Spins { get; set; }

        public char[] SpinStr
        {
            get
            {
                var val = new char[Spins.Length];
                for (int i = 0; i < val.Length; ++i)
                {
                    val[i] = spins[i];
                }
                return val;
            }
        }
    }
}