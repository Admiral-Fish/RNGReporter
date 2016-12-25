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
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Controls;

namespace RNGReporter.Objects.Searchers
{
    internal class Gen3Searcher : Searcher
    {
        private readonly List<Gen3CapFrame> captureFrames;
        private readonly BindingSource frameBinding;
        private readonly Gen3SearchParams searchParams;
        // make this part of the base class?
        private readonly object threadLock;
        private FrameCompare frameCompare;
        private FrameGenerator generator;
        private ushort id;
        private uint maxFrame;
        private uint maxHour;
        private uint maxMinute;
        private uint minFrame;
        private uint minHour;
        private uint minMinute;
        private DateTime seedDate;
        private ushort sid;

        // todo: move commonly reused code to base class's constructor
        public Gen3Searcher(Gen3SearchParams searchParams, object threadLock, Form caller)
        {
            this.searchParams = searchParams;
            this.threadLock = threadLock;
            this.caller = caller;

            numThreads = 1;
            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            captureFrames = new List<Gen3CapFrame>();
            frameBinding = new BindingSource {DataSource = captureFrames};
            searchParams.dataGridView.DataSource = frameBinding;
            btnGenerate = searchParams.capButton;
        }

        public override bool ParseInput()
        {
            // passes each of the fields into a function to parse the input
            // also validates the ranges
            // min/max frame can be larger in the input box than the limit of a uint but it's low priority to fix that
            if (!(FormsFunctions.ParseInputD(searchParams.minFrame, out minFrame) &&
                  FormsFunctions.ParseInputD(searchParams.maxFrame, out maxFrame) &&
                  minFrame <= maxFrame &&
                  FormsFunctions.ParseInputD(searchParams.minHour, out minHour) &&
                  FormsFunctions.ParseInputD(searchParams.maxHour, out maxHour) &&
                  minHour <= maxHour && maxHour <= 23 &&
                  FormsFunctions.ParseInputD(searchParams.minMinute, out minMinute) &&
                  FormsFunctions.ParseInputD(searchParams.maxMinute, out maxMinute) &&
                  minMinute <= maxMinute && maxMinute <= 59)) return false;
            //parse the id/sid defaulting to 0
            FormsFunctions.ParseInputD(searchParams.id, out id);
            FormsFunctions.ParseInputD(searchParams.sid, out sid);

            // everything from here on should always be valid input
            seedDate = searchParams.date.Value;
            bool shiny = searchParams.isShiny.Checked;
            bool synch = searchParams.isSynch.Checked;
            IVFilter ivfilter = searchParams.ivfilters.IVFilter;

            List<int> encounterSlots = null;
            if (searchParams.encounterSlot.Text != "Any" && searchParams.encounterSlot.CheckBoxItems.Count > 0)
            {
                encounterSlots = new List<int>();
                for (int i = 0; i < searchParams.encounterSlot.CheckBoxItems.Count; i++)
                {
                    if (searchParams.encounterSlot.CheckBoxItems[i].Checked)
                        // We have to subtract 1 because this custom control contains a hidden item for text display
                        encounterSlots.Add(i - 1);
                }
            }

            List<uint> natures = null;
            if (searchParams.nature.Text != "Any" && searchParams.nature.CheckBoxItems.Count > 0)
            {
                natures =
                    (from t in searchParams.nature.CheckBoxItems
                     where t.Checked
                     select (uint) ((Nature) t.ComboBoxItem).Number).ToList();
            }

            frameCompare = new FrameCompare(ivfilter, natures,
                                            (int) ((ComboBoxItem) searchParams.ability.SelectedItem).Reference, shiny,
                                            synch, false, encounterSlots,
                                            (GenderFilter) (searchParams.gender.SelectedItem));

            EncounterMod currentMod = synch ? EncounterMod.Synchronize : EncounterMod.None;
            generator = new FrameGenerator
                {
                    FrameType =
                        (FrameType) ((ComboBoxItem) searchParams.frameType.SelectedItem).Reference,
                    EncounterMod = currentMod
                };
            if (currentMod == EncounterMod.Synchronize && natures == null)
            {
                generator.EncounterMod = EncounterMod.None;
            }

            generator.SynchNature = ((Nature) searchParams.synchNature.SelectedItem).Number;

            generator.EncounterType = EncounterTypeCalc.EncounterString(searchParams.encounterType.Text);

            return true;
        }

        protected override Thread SearchThread(int threadNum)
        {
            return new Thread(Search);
        }

        protected override Thread ProgressThread()
        {
            var frameType = (FrameType) ((ComboBoxItem) searchParams.frameType.SelectedItem).Reference;
            return new Thread(
                () => ManageProgress(frameBinding, searchParams.dataGridView, frameType, 0));
        }

        protected override void ResortGrid(BindingSource bindingSource, DoubleBufferedDataGridView dataGrid,
                                           FrameType frameType)
        {
            // note: there should be a safer way to do this
            switch (frameType)
            {
                case FrameType.Method1:
                case FrameType.Method2:
                case FrameType.Method4:
                    dataGrid.Columns["Offset"].Visible = false;
                    dataGrid.Columns["EncounterSlot"].Visible = false;
                    break;
                case FrameType.MethodH1:
                case FrameType.MethodH2:
                case FrameType.MethodH4:
                    dataGrid.Columns["Offset"].Visible = true;
                    dataGrid.Columns["EncounterSlot"].Visible = true;
                    break;
            }
            dataGrid.DataSource = bindingSource;
            bindingSource.ResetBindings(false);
        }

        private void Search()
        {
            generator.InitialFrame = minFrame;
            generator.MaxResults = maxFrame;

            for (uint hour = minHour; hour <= maxHour; ++hour)
            {
                for (uint minute = minMinute; minute <= maxMinute; ++minute)
                {
                    waitHandle.WaitOne();

                    DateTime time = seedDate.AddHours(hour).AddMinutes(minute);
                    uint seed = Functions.CalculateSeedGen3(time);
                    generator.InitialSeed = seed;

                    List<Frame> frames = generator.Generate(frameCompare, id, sid);
                    progressSearched += maxFrame;
                    progressFound += (ulong) frames.Count;
                    progressTotal += (ulong) frames.Count*maxFrame;
                    lock (threadLock)
                    {
                        foreach (Frame frame in frames)
                        {
                            frame.DisplayPrep();
                            captureFrames.Add(new Gen3CapFrame(frame, hour, minute));
                        }
                    }
                    refreshQueue = true;
                }
            }
        }
    }

    // todo: abstract this
    public class Gen3SearchParams
    {
        public ComboBox ability;
        public Button capButton;
        public DoubleBufferedDataGridView dataGridView;
        public DateTimePicker date;
        public CheckBoxComboBox encounterSlot;
        public ComboBox encounterType;
        public ComboBox frameType;
        public ComboBox gender;
        public TextBoxBase id;
        public CheckBox isShiny, isSynch;
        public IVFilters ivfilters;
        public TextBoxBase maxFrame;
        public TextBoxBase maxHour;
        public TextBoxBase maxMinute;
        public TextBoxBase minFrame;
        public TextBoxBase minHour;
        public TextBoxBase minMinute;
        public CheckBoxComboBox nature;
        public TextBoxBase sid;
        public ComboBox synchNature;
    }

    public class Gen3CapFrame
    {
        private readonly Frame frame;

        public Gen3CapFrame(Frame frame, uint hour, uint minute)
        {
            this.frame = frame;
            SeedTime = string.Format("{0:00}:{1:00}", hour, minute);
        }

        public string SeedTime { get; private set; }

        public uint Number
        {
            get { return frame.Number; }
        }

        public uint Offset
        {
            get { return frame.Offset; }
        }

        public string FrameTime
        {
            get { return frame.Time; }
        }

        public int EncounterSlot
        {
            get { return frame.EncounterSlot; }
        }

        public uint Pid
        {
            get { return frame.Pid; }
        }

        public string ShinyDisplay
        {
            get { return frame.ShinyDisplay; }
        }

        public string Nature
        {
            get { return frame.NatureDisplay; }
        }

        public uint Ability
        {
            get { return frame.Ability; }
        }

        public string HP
        {
            get { return frame.DisplayHp; }
        }

        public string Atk
        {
            get { return frame.DisplayAtk; }
        }

        public string Def
        {
            get { return frame.DisplayDef; }
        }

        public string Spa
        {
            get { return frame.DisplaySpa; }
        }

        public string Spd
        {
            get { return frame.DisplaySpd; }
        }

        public string Spe
        {
            get { return frame.DisplaySpe; }
        }

        public string HiddenPowerType
        {
            get { return frame.HiddenPowerType; }
        }

        public uint HiddenPowerPower
        {
            get { return frame.HiddenPowerPower; }
        }

        public string Female50
        {
            get { return frame.Female50; }
        }

        public string Female125
        {
            get { return frame.Female125; }
        }

        public string Female25
        {
            get { return frame.Female25; }
        }

        public string Female75
        {
            get { return frame.Female75; }
        }
    }
}