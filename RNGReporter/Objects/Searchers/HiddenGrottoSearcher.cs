using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Controls;
using RNGReporter.Objects.Generators;

namespace RNGReporter.Objects.Searchers
{
    internal class HiddenGrottoSearcher : Searcher
    {
        private readonly BindingSource frameBinding;
        private readonly List<Hollow> hollowFrames;
        private readonly HiddenGrottoSearchParams searchParams;
        private readonly object threadLock;
        private HiddenGrottoGenerator generator;
        private List<List<ButtonComboType>> keypresses;
        private List<int> months;
        private ushort year;

        public HiddenGrottoSearcher(HiddenGrottoSearchParams searchParams, object threadLock, Form caller)
        {
            this.searchParams = searchParams;
            this.threadLock = threadLock;
            this.caller = caller;

            numThreads = 1;
            waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

            hollowFrames = new List<Hollow>();
            frameBinding = new BindingSource {DataSource = hollowFrames};
            searchParams.DataGridView.DataSource = frameBinding;
            btnGenerate = searchParams.GenerateButton;
        }

        public override bool ParseInput()
        {
            ushort openHollows;
            uint maxAdvances;
            if (
                !(FormsFunctions.ParseInputD(searchParams.Year, out year) &&
                  FormsFunctions.ParseInputD(searchParams.MaxAdvances, out maxAdvances) &&
                  FormsFunctions.ParseInputD(searchParams.OpenHollows, out openHollows) && openHollows > 0 &&
                  openHollows < 21)) return false;
            generator = new HiddenGrottoGenerator {OpenHollows = openHollows, MaxAdvances = maxAdvances};
            months = new List<int>();
            // todo: move this to forms functions
            for (int month = 1; month <= 12; month++)
            {
                if (searchParams.Months.CheckBoxItems[month].Checked)
                    months.Add(month);
            }

            if (months.Count == 0)
            {
                searchParams.Months.Focus();
                return false;
            }

            keypresses = searchParams.Profile.GetKeypresses();

            int dayTotal = months.Sum(month => DateTime.DaysInMonth(year, month));

            progressTotal =
                (ulong)
                (dayTotal*86400*keypresses.Count*(searchParams.Profile.Timer0Max - searchParams.Profile.Timer0Min + 1)*
                 (maxAdvances + 1));

            var slots = new List<ushort>();
            for (ushort i = 1; i < searchParams.Slots.CheckBoxItems.Count; ++i)
            {
                if (searchParams.Slots.CheckBoxItems[i].Checked) slots.Add((ushort) (i - 1));
            }
            generator.Slots = slots.Count > 0 ? slots : null;

            var subslots = new List<ushort>();
            for (ushort i = 1; i < searchParams.SubSlots.CheckBoxItems.Count; ++i)
            {
                if (searchParams.SubSlots.CheckBoxItems[i].Checked) subslots.Add((ushort) (i - 1));
            }
            generator.SubSlots = subslots.Count > 0 ? subslots : null;

            var hollows = new List<ushort>();
            for (ushort i = 1; i < searchParams.Hollows.CheckBoxItems.Count; ++i)
            {
                if (searchParams.Hollows.CheckBoxItems[i].Checked)
                {
                    var hollow = (ushort) (i - 1);
                    // check to make sure it's possible to hit this hollow
                    if (hollow >= openHollows)
                    {
                        MessageBox.Show(
                            string.Format("It's impossible to hit hollow number {0} with {1} open hollows!", hollow,
                                          openHollows), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    hollows.Add(hollow);
                }
            }
            generator.Hollows = hollows.Count > 0 ? hollows : null;

            ushort genderRatio;
            ushort.TryParse(searchParams.GenderRatio.Text, out genderRatio);
            generator.GenderRatio = genderRatio;

            if (searchParams.Gender.Text.Length > 0) generator.Gender = searchParams.Gender.Text[0];

            /*float interval = ((float)24 / cpus + (float)0.05);

                var hourMin = new int[cpus];
                var hourMax = new int[cpus];

                jobs = new Thread[cpus];
                generators = new FrameGenerator[cpus];
                shinygenerators = new FrameGenerator[cpus];
                waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

                for (int i = 0; i < jobs.Length; i++)
                {
                    hourMin[i] = (int)(interval * i);
                    hourMax[i] = (int)(interval * (i + 1) - 1);

                    if (hourMax[i] > 23)
                    {
                        hourMax[i] = 23;
                    }
                }*/

            return true;
        }

        protected override Thread SearchThread(int threadNum)
        {
            return new Thread(Search);
        }

        protected override Thread ProgressThread()
        {
            // doesn't matter just using to prevent errors for now
            const FrameType frameType = FrameType.Method1;
            return new Thread(
                () => ManageProgress(frameBinding, searchParams.DataGridView, frameType, 0));
        }

        protected override void ResortGrid(BindingSource bindingSource, DoubleBufferedDataGridView dataGrid,
                                           FrameType frameType)
        {
            dataGrid.DataSource = bindingSource;
            bindingSource.ResetBindings(false);
        }

        private void Search() //uint minHour, uint maxHour)
        {
            const int minHour = 0;
            const int maxHour = 24;
            const int threadIndex = 0;
            // todo: move this outside the search and only do it once
            var array = new uint[80];
            array[6] = (uint) (searchParams.Profile.MAC_Address & 0xFFFF);

            if (searchParams.Profile.SoftReset)
            {
                array[6] = array[6] ^ 0x01000000;
            }

            var upperMAC = (uint) (searchParams.Profile.MAC_Address >> 16);
            array[7] = (upperMAC ^ (searchParams.Profile.VFrame*0x1000000) ^ searchParams.Profile.GxStat);

            // Get the version-unique part of the message
            Array.Copy(
                Nazos.Nazo(searchParams.Profile.Version, searchParams.Profile.Language, searchParams.Profile.DSType),
                array, 5);

            array[10] = 0x00000000;
            array[11] = 0x00000000;
            array[13] = 0x80000000;
            array[14] = 0x00000000;
            array[15] = 0x000001A0;
            List<List<ButtonComboType>> keypressList = searchParams.Profile.GetKeypresses();
            List<ButtonComboType>[] buttons = keypressList.ToArray();
            var buttonMashValue = new uint[keypressList.Count];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttonMashValue[i] = Functions.buttonMashed(buttons[i]);
            }

            //uint searchRange = generator.MaxResults;

            foreach (int month in months)
            {
                float interval = ((float) DateTime.DaysInMonth(year, month)/numThreads + (float) 0.05);

                var dayMin = (int) (interval*threadIndex + 1);
                var dayMax = (int) (interval*(threadIndex + 1));

                string yearMonth = String.Format("{0:00}", year%2000) + String.Format("{0:00}", month);
                for (int buttonCount = 0; buttonCount < keypressList.Count; buttonCount++)
                {
                    array[12] = buttonMashValue[buttonCount];
                    for (uint timer0 = searchParams.Profile.Timer0Min;
                         timer0 <= searchParams.Profile.Timer0Max;
                         timer0++)
                    {
                        array[5] = (searchParams.Profile.VCount << 16) + timer0;
                        array[5] = Functions.Reorder(array[5]);

                        for (int day = dayMin; day <= dayMax; day++)
                        {
                            var searchTime = new DateTime(year, month, day);

                            string dateString = String.Format("{0:00}", (int) searchTime.DayOfWeek);
                            dateString = String.Format("{0:00}", searchTime.Day) + dateString;
                            dateString = yearMonth + dateString;
                            array[8] = uint.Parse(dateString, NumberStyles.HexNumber);
                            array[9] = 0x0;

                            // For seeds with the same date, the contents of the SHA-1 array will be the same for the first 8 steps
                            // We are precomputing those 8 steps to save time
                            // Trying to precompute beyond 8 steps is complicated and does not save much time, also runs the risk of errors

                            uint[] alpha = Functions.alphaSHA1(array, 8);

                            // We are also precomputing select portions of the SHA-1 array during the expansion process
                            // As they are also the same

                            array[16] = Functions.RotateLeft(array[13] ^ array[8] ^ array[2] ^ array[0], 1);
                            array[18] = Functions.RotateLeft(array[15] ^ array[10] ^ array[4] ^ array[2], 1);
                            array[19] = Functions.RotateLeft(array[16] ^ array[11] ^ array[5] ^ array[3], 1);
                            array[21] = Functions.RotateLeft(array[18] ^ array[13] ^ array[7] ^ array[5], 1);
                            array[22] = Functions.RotateLeft(array[19] ^ array[14] ^ array[8] ^ array[6], 1);
                            array[24] = Functions.RotateLeft(array[21] ^ array[16] ^ array[10] ^ array[8], 1);
                            array[27] = Functions.RotateLeft(array[24] ^ array[19] ^ array[13] ^ array[11], 1);

                            for (int hour = minHour; hour <= maxHour; hour++)
                            {
                                //int seedHour = hour;
                                for (int minute = 0; minute <= 59; minute++)
                                {
                                    waitHandle.WaitOne();
                                    for (int second = 0; second <= 59; second++)
                                    {
                                        array[9] = Functions.seedSecond(second) | Functions.seedMinute(minute) |
                                                   Functions.seedHour(hour, searchParams.Profile.DSType);

                                        ulong seed = Functions.EncryptSeed(array, alpha, 9);
                                        List<Hollow> hollows = generator.Generate(seed, searchParams.Profile);
                                        foreach (Hollow t in hollows)
                                        {
                                            t.DateTime =
                                                searchTime.AddHours(hour).AddMinutes(minute).AddSeconds(second);
                                            t.Timer0 = timer0;
                                            t.Keypresses = keypressList[buttonCount];
                                        }
                                        lock (threadLock)
                                        {
                                            hollowFrames.AddRange(hollows);
                                        }
                                        progressSearched += generator.OpenHollows*generator.MaxAdvances;
                                        progressFound += (ulong) hollows.Count;
                                        progressTotal += (ulong) hollows.Count*generator.OpenHollows*
                                                         generator.MaxAdvances;
                                        refreshQueue = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class HiddenGrottoSearchParams
    {
        public DoubleBufferedDataGridView DataGridView;
        public ComboBox Gender;
        public ComboBox GenderRatio;
        public Button GenerateButton;
        public CheckBoxComboBox Hollows;
        public TextBoxBase MaxAdvances;
        public CheckBoxComboBox Months;
        public TextBoxBase OpenHollows;
        public Profile Profile;
        public CheckBoxComboBox Slots;
        public CheckBoxComboBox SubSlots;
        public TextBoxBase Year;
    }
}