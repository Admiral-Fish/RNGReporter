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
using System.Drawing;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class EggParents : Form
    {
        private readonly EncounterType encounterType;
        private readonly FrameType frameType;

        private readonly uint[] parentA = new uint[6];

        private readonly uint[] parentB = new uint[6];

        private readonly uint[] rngIVs = new uint[6];
        private readonly uint seed;
        public bool BW2;

        public bool CompleteIVs;
        public bool RNGIVsOnly;

        public EggParents(FrameType frameType, EncounterType encounterType, uint seed, bool BW2)
            : this(frameType, encounterType, seed)
        {
            this.BW2 = BW2;
        }

        public EggParents(FrameType frameType, EncounterType encounterType, uint seed)
        {
            InitializeComponent();

            this.seed = seed;
            this.encounterType = encounterType;
            this.frameType = frameType;

            if (frameType == FrameType.BWBred || frameType == FrameType.BWBredInternational)
            {
                Text = "Display Parent IVs";
                buttonRetrieveIVs.Text = "Get IVs from IVRNG (Frame 8)";
            }
            else if (frameType == FrameType.DPPtBred ||
                     frameType == FrameType.HGSSBred ||
                     frameType == FrameType.Bred ||
                     frameType == FrameType.BredSplit ||
                     frameType == FrameType.BredAlternate)
            {
                labelParentA.Text = "Parent A";
                labelParentB.Text = "Parent B";

                labelParentA.Location = new Point(39, 40);
                labelParentB.Location = new Point(39, 66);

                labelIVRNG.Visible = false;
                maskedTextBoxHP_IVRNG.Visible = false;
                maskedTextBoxAtk_IVRNG.Visible = false;
                maskedTextBoxDef_IVRNG.Visible = false;
                maskedTextBoxSpA_IVRNG.Visible = false;
                maskedTextBoxSpD_IVRNG.Visible = false;
                maskedTextBoxSpe_IVRNG.Visible = false;
            }
            else
            {
                maskedTextBoxHP_ParentA.Enabled = false;
                maskedTextBoxAtk_ParentA.Enabled = false;
                maskedTextBoxDef_ParentA.Enabled = false;
                maskedTextBoxSpA_ParentA.Enabled = false;
                maskedTextBoxSpD_ParentA.Enabled = false;
                maskedTextBoxSpe_ParentA.Enabled = false;

                maskedTextBoxHP_ParentB.Enabled = false;
                maskedTextBoxAtk_ParentB.Enabled = false;
                maskedTextBoxDef_ParentB.Enabled = false;
                maskedTextBoxSpA_ParentB.Enabled = false;
                maskedTextBoxSpD_ParentB.Enabled = false;
                maskedTextBoxSpe_ParentB.Enabled = false;

                Text = "Display Characteristics in List";
                buttonRetrieveIVs.Text = encounterType == EncounterType.LarvestaEgg
                                             ? "Get IVs from IVRNG (Frame 2)"
                                             : "Get IVs from IVRNG (Frame 1)";
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public uint[] ParentA
        {
            get { return parentA; }
        }

        public uint[] ParentB
        {
            get { return parentB; }
        }

        public uint[] RNGIVs
        {
            get { return rngIVs; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // we can't make int null
            // so we replace it with a value impossible for an IV (32)
            // another function will handle the null value

            if (!uint.TryParse(maskedTextBoxHP_ParentA.Text, out parentA[0]))
                parentA[0] = 32;
            if (!uint.TryParse(maskedTextBoxAtk_ParentA.Text, out parentA[1]))
                parentA[1] = 32;
            if (!uint.TryParse(maskedTextBoxDef_ParentA.Text, out parentA[2]))
                parentA[2] = 32;
            if (!uint.TryParse(maskedTextBoxSpA_ParentA.Text, out parentA[3]))
                parentA[3] = 32;
            if (!uint.TryParse(maskedTextBoxSpD_ParentA.Text, out parentA[4]))
                parentA[4] = 32;
            if (!uint.TryParse(maskedTextBoxSpe_ParentA.Text, out parentA[5]))
                parentA[5] = 32;

            if (!uint.TryParse(maskedTextBoxHP_ParentB.Text, out parentB[0]))
                parentB[0] = 32;
            if (!uint.TryParse(maskedTextBoxAtk_ParentB.Text, out parentB[1]))
                parentB[1] = 32;
            if (!uint.TryParse(maskedTextBoxDef_ParentB.Text, out parentB[2]))
                parentB[2] = 32;
            if (!uint.TryParse(maskedTextBoxSpA_ParentB.Text, out parentB[3]))
                parentB[3] = 32;
            if (!uint.TryParse(maskedTextBoxSpD_ParentB.Text, out parentB[4]))
                parentB[4] = 32;
            if (!uint.TryParse(maskedTextBoxSpe_ParentB.Text, out parentB[5]))
                parentB[5] = 32;

            if (!uint.TryParse(maskedTextBoxHP_IVRNG.Text, out rngIVs[0]))
                rngIVs[0] = 32;
            if (!uint.TryParse(maskedTextBoxAtk_IVRNG.Text, out rngIVs[1]))
                rngIVs[1] = 32;
            if (!uint.TryParse(maskedTextBoxDef_IVRNG.Text, out rngIVs[2]))
                rngIVs[2] = 32;
            if (!uint.TryParse(maskedTextBoxSpA_IVRNG.Text, out rngIVs[3]))
                rngIVs[3] = 32;
            if (!uint.TryParse(maskedTextBoxSpD_IVRNG.Text, out rngIVs[4]))
                rngIVs[4] = 32;
            if (!uint.TryParse(maskedTextBoxSpe_IVRNG.Text, out rngIVs[5]))
                rngIVs[5] = 32;

            CompleteIVs = true;
            RNGIVsOnly = true;

            // Check to see if user filled out all fields
            // If not, we can't allow main form to display characteristics
            for (int i = 0; i < 6; i++)
            {
                if (parentA[i] == 32 || parentB[i] == 32 || rngIVs[i] == 32)
                {
                    CompleteIVs = false;
                    break;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (rngIVs[i] == 32)
                {
                    RNGIVsOnly = false;
                    break;
                }
            }
        }

        private void buttonRetrieveIVs_Click(object sender, EventArgs e)
        {
            IRNG mt = new MersenneTwister(seed);

            var rngArray = new uint[7];

            // BW eggs start on frame 8
            if (frameType == FrameType.BWBred || frameType == FrameType.BWBredInternational)
            {
                for (uint cnt = 1; cnt < 8; cnt++)
                {
                    mt.Nextuint();
                }
            }

            // this is lazy and bad and should be done differently
            if (BW2)
            {
                for (uint cnt = 0; cnt < 2; cnt++)
                {
                    mt.Nextuint();
                }
            }

            for (int i = 0; i < 7; i++)
            {
                rngArray[i] = mt.Nextuint() >> 27;
            }

            Frame frame;
            if (encounterType == EncounterType.Roamer)
            {
                frame = Frame.GenerateFrame(
                    FrameType.Method5Standard,
                    0,
                    seed,
                    rngArray[1],
                    rngArray[2],
                    rngArray[3],
                    rngArray[6],
                    rngArray[4],
                    rngArray[5]);
            }
            else if (encounterType == EncounterType.LarvestaEgg)
            {
                frame = Frame.GenerateFrame(
                    FrameType.Method5Standard,
                    0,
                    seed,
                    rngArray[1],
                    rngArray[2],
                    rngArray[3],
                    rngArray[4],
                    rngArray[5],
                    rngArray[6]);
            }
            else
            {
                frame = Frame.GenerateFrame(
                    FrameType.Method5Standard,
                    0,
                    seed,
                    rngArray[0],
                    rngArray[1],
                    rngArray[2],
                    rngArray[3],
                    rngArray[4],
                    rngArray[5]);
            }

            maskedTextBoxHP_IVRNG.Text = frame.Hp.ToString();
            maskedTextBoxAtk_IVRNG.Text = frame.Atk.ToString();
            maskedTextBoxDef_IVRNG.Text = frame.Def.ToString();
            maskedTextBoxSpA_IVRNG.Text = frame.Spa.ToString();
            maskedTextBoxSpD_IVRNG.Text = frame.Spd.ToString();
            maskedTextBoxSpe_IVRNG.Text = frame.Spe.ToString();
        }
    }
}