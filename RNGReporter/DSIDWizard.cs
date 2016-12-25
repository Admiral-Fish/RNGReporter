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
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;
using Version = RNGReporter.Objects.Version;

namespace RNGReporter
{
    public partial class DSIDWizard : Form
    {
        // Multithreading
        private int cpus;
        private ulong directSeed;
        private List<DSParameterCapture> dsParameters;
        private bool findDirectSeed;
        private ulong progressFound;
        private ulong progressSearched;
        private ulong progressTotal;
        private bool refreshQueue;

        public DSIDWizard()
        {
            InitializeComponent();
            comboBoxDSType.SelectedIndex = 0;
            comboBoxVersion.SelectedIndex = 0;
            comboBoxButton1.SelectedIndex = 0;
            comboBoxButton2.SelectedIndex = 0;
            comboBoxButton3.SelectedIndex = 0;
        }

        public ulong MAC_address { get; set; }

        private void maskedTextBoxSeconds_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBoxSecond.Text != "")
            {
                maskedTextBoxSecondsMin.Text = maskedTextBoxSecond.Text;
                try
                {
                    maskedTextBoxSecondsMax.Text = ((int.Parse(maskedTextBoxSecond.Text) + 10)%60).ToString();
                }
                catch
                {
                }
            }
        }

        private void comboBoxDSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDSType.SelectedIndex)
            {
                case 0:
                    // BW1 DS
                    if (comboBoxVersion.SelectedIndex < 2)
                    {
                        textBoxVCountMin.Text = "50";
                        textBoxVCountMax.Text = "70";
                        textBoxTimer0Min.Text = "C60";
                        textBoxTimer0Max.Text = "CA0";
                    }
                        // BW2 DS
                    else
                    {
                        textBoxVCountMin.Text = "70";
                        textBoxVCountMax.Text = "90";
                        textBoxTimer0Min.Text = "10E0";
                        textBoxTimer0Max.Text = "1130";
                    }
                    textBoxGxStatMin.Text = "6";
                    textBoxGxStatMax.Text = "6";
                    textBoxVFrameMin.Text = "0";
                    textBoxVFrameMax.Text = "10";
                    break;
                case 1:
                case 2:
                    // BW1 DSi
                    if (comboBoxVersion.SelectedIndex < 2)
                    {
                        textBoxVCountMin.Text = "80";
                        textBoxVCountMax.Text = "92";
                        textBoxTimer0Min.Text = "1140";
                        textBoxTimer0Max.Text = "12D0";
                    }
                        // BW2 DSi
                    else
                    {
                        textBoxVCountMin.Text = "A0";
                        textBoxVCountMax.Text = "C0";
                        textBoxTimer0Min.Text = "1600";
                        textBoxTimer0Max.Text = "1790";
                    }
                    textBoxGxStatMin.Text = "6";
                    textBoxGxStatMax.Text = "6";
                    textBoxVFrameMin.Text = "0";
                    textBoxVFrameMax.Text = "10";
                    break;
            }
        }

        private void comboBoxVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDSType_SelectedIndexChanged(sender, e);
        }

        private bool ValidateInput()
        {
            if (textBoxDirectSeed.Text == "")
            {
                if (maskedTextBoxHPMin.Text == "")
                {
                    maskedTextBoxHPMin.Focus();
                    maskedTextBoxHPMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxAtkMin.Text == "")
                {
                    maskedTextBoxAtkMin.Focus();
                    maskedTextBoxAtkMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxDefMin.Text == "")
                {
                    maskedTextBoxDefMin.Focus();
                    maskedTextBoxDefMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpAtkMin.Text == "")
                {
                    maskedTextBoxSpAtkMin.Focus();
                    maskedTextBoxSpAtkMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpDefMin.Text == "")
                {
                    maskedTextBoxSpDefMin.Focus();
                    maskedTextBoxSpDefMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpeedMin.Text == "")
                {
                    maskedTextBoxSpeedMin.Focus();
                    maskedTextBoxSpeedMin.SelectAll();
                    return false;
                }

                if (maskedTextBoxHPMax.Text == "")
                {
                    maskedTextBoxHPMax.Focus();
                    maskedTextBoxHPMax.SelectAll();
                    return false;
                }

                if (maskedTextBoxAtkMax.Text == "")
                {
                    maskedTextBoxAtkMax.Focus();
                    maskedTextBoxAtkMax.SelectAll();
                    return false;
                }

                if (maskedTextBoxDefMax.Text == "")
                {
                    maskedTextBoxDefMax.Focus();
                    maskedTextBoxDefMax.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpAtkMax.Text == "")
                {
                    maskedTextBoxSpAtkMax.Focus();
                    maskedTextBoxSpAtkMax.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpDefMax.Text == "")
                {
                    maskedTextBoxSpDefMax.Focus();
                    maskedTextBoxSpDefMax.SelectAll();
                    return false;
                }

                if (maskedTextBoxSpeedMax.Text == "")
                {
                    maskedTextBoxSpeedMax.Focus();
                    maskedTextBoxSpeedMax.SelectAll();
                    return false;
                }
            }

            if (maskedTextBoxHour.Text == "")
            {
                maskedTextBoxHour.Focus();
                maskedTextBoxHour.SelectAll();
                return false;
            }

            if (maskedTextBoxMinute.Text == "")
            {
                maskedTextBoxMinute.Focus();
                maskedTextBoxMinute.SelectAll();
                return false;
            }

            if (textBoxMACAddress.Text == "")
            {
                textBoxMACAddress.Focus();
                textBoxMACAddress.SelectAll();
                return false;
            }

            if (textBoxVCountMax.Text == "")
            {
                textBoxVCountMax.Focus();
                textBoxVCountMax.SelectAll();
                return false;
            }

            if (maskedTextBoxSecondsMin.Text == "")
            {
                maskedTextBoxSecondsMin.Focus();
                maskedTextBoxSecondsMin.SelectAll();
                return false;
            }

            if (maskedTextBoxSecondsMax.Text == "")
            {
                maskedTextBoxSecondsMax.Focus();
                maskedTextBoxSecondsMax.SelectAll();
                return false;
            }

            if (textBoxVCountMin.Text == "")
            {
                textBoxVCountMin.Focus();
                textBoxVCountMin.SelectAll();
                return false;
            }

            if (textBoxVCountMax.Text == "")
            {
                textBoxVCountMax.Focus();
                textBoxVCountMax.SelectAll();
                return false;
            }

            if (textBoxTimer0Min.Text == "")
            {
                textBoxTimer0Min.Focus();
                textBoxTimer0Min.SelectAll();
                return false;
            }

            if (textBoxTimer0Max.Text == "")
            {
                textBoxTimer0Max.Focus();
                textBoxTimer0Max.SelectAll();
                return false;
            }

            if (textBoxGxStatMin.Text == "")
            {
                textBoxGxStatMin.Focus();
                textBoxGxStatMin.SelectAll();
                return false;
            }

            if (textBoxGxStatMax.Text == "")
            {
                textBoxGxStatMax.Focus();
                textBoxGxStatMax.SelectAll();
                return false;
            }

            if (textBoxVFrameMin.Text == "")
            {
                textBoxVFrameMin.Focus();
                textBoxVFrameMin.SelectAll();
                return false;
            }

            if (textBoxVFrameMax.Text == "")
            {
                textBoxVFrameMax.Focus();
                textBoxVFrameMax.SelectAll();
                return false;
            }
            return true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            btnSendTimeFinder.Enabled = false;

            uint VCountMin = uint.Parse(textBoxVCountMin.Text, NumberStyles.HexNumber);
            uint VCountMax = uint.Parse(textBoxVCountMax.Text, NumberStyles.HexNumber);
            uint Timer0Min = uint.Parse(textBoxTimer0Min.Text, NumberStyles.HexNumber);
            uint Timer0Max = uint.Parse(textBoxTimer0Max.Text, NumberStyles.HexNumber);
            uint GxStatMin = uint.Parse(textBoxGxStatMin.Text, NumberStyles.HexNumber);
            uint GxStatMax = uint.Parse(textBoxGxStatMax.Text, NumberStyles.HexNumber);
            uint VFrameMin = uint.Parse(textBoxVFrameMin.Text, NumberStyles.HexNumber);
            uint VFrameMax = uint.Parse(textBoxVFrameMax.Text, NumberStyles.HexNumber);
            int secondsMin = int.Parse(maskedTextBoxSecondsMin.Text);
            int secondsMax = int.Parse(maskedTextBoxSecondsMax.Text);
            if (secondsMax < secondsMin)
            {
                secondsMax = secondsMax + 60;
            }
            bool minMaxGxStat = cbGxStat.Checked;

            dsParameters = new List<DSParameterCapture>();
            var listBinding = new BindingSource {DataSource = dsParameters};
            dataGridView1.DataSource = listBinding;

            cpus = Settings.Default.CPUs;
            if (cpus < 1)
            {
                cpus = 1;
            }

            var jobs = new Thread[cpus];

            var progress = new Progress {Text = "DS Parameter Progress"};
            progressTotal = (ulong) ((VCountMax - VCountMin + 1)
                                     *(Timer0Max - Timer0Min + 1)
                                     // if only min max gx stat search is 2 (or 1 if they're the same) else it's max-min
                                     *(minMaxGxStat ? (GxStatMax > GxStatMin ? 2u : 1) : (GxStatMax - GxStatMin + 1))
                                     *(VFrameMax - VFrameMin + 1)
                                     *(secondsMax - secondsMin + 1));

            progressSearched = 0;
            progressFound = 0;
            var testTime = new DateTime(
                datePicker.Value.Year,
                datePicker.Value.Month,
                datePicker.Value.Day,
                int.Parse(maskedTextBoxHour.Text),
                int.Parse(maskedTextBoxMinute.Text),
                secondsMin);

            var minIVs = new int[6];
            var maxIVs = new int[6];

            MAC_address = ulong.Parse(textBoxMACAddress.Text, NumberStyles.HexNumber);

            if (textBoxMACAddress.Text.Length < 12)
            {
                MessageBox.Show("Your MAC address is missing some digits.  Double-check your MAC address.");
            }

            if (textBoxDirectSeed.Text != "")
            {
                directSeed = ulong.Parse(textBoxDirectSeed.Text, NumberStyles.HexNumber);
                findDirectSeed = true;
            }
            else
            {
                findDirectSeed = false;

                minIVs[0] = int.Parse(maskedTextBoxHPMin.Text);
                minIVs[1] = int.Parse(maskedTextBoxAtkMin.Text);
                minIVs[2] = int.Parse(maskedTextBoxDefMin.Text);
                minIVs[3] = int.Parse(maskedTextBoxSpAtkMin.Text);
                minIVs[4] = int.Parse(maskedTextBoxSpDefMin.Text);
                minIVs[5] = int.Parse(maskedTextBoxSpeedMin.Text);

                maxIVs[0] = int.Parse(maskedTextBoxHPMax.Text);
                maxIVs[1] = int.Parse(maskedTextBoxAtkMax.Text);
                maxIVs[2] = int.Parse(maskedTextBoxDefMax.Text);
                maxIVs[3] = int.Parse(maskedTextBoxSpAtkMax.Text);
                maxIVs[4] = int.Parse(maskedTextBoxSpDefMax.Text);
                maxIVs[5] = int.Parse(maskedTextBoxSpeedMax.Text);

                int combinations = (maxIVs[0] - minIVs[0] + 1) +
                                   (maxIVs[1] - minIVs[1] + 1) +
                                   (maxIVs[2] - minIVs[2] + 1) +
                                   (maxIVs[3] - minIVs[3] + 1) +
                                   (maxIVs[4] - minIVs[4] + 1) +
                                   (maxIVs[5] - minIVs[5] + 1);

                if (combinations > 200)
                {
                    MessageBox.Show(
                        "There were too many combinations of IV possibilities to accurately find your intitial seed (" +
                        combinations + ") please try with a higher level Pokemon,", "Too many IV Combinations");
                    return;
                }
            }

            var version = (Version) comboBoxVersion.SelectedIndex;
            var language = (Language) comboBoxLanguage.SelectedIndex;
            var dstype = (DSType) comboBoxDSType.SelectedIndex;

            var interval = (uint) ((VCountMax - VCountMin)/(float) jobs.Length);
            uint VCountMinLower = VCountMin;
            uint VCountMinUpper = VCountMin + interval;

            var button = new int[3];
            button[0] = comboBoxButton1.SelectedIndex;
            button[1] = comboBoxButton2.SelectedIndex;
            button[2] = comboBoxButton3.SelectedIndex;

            try
            {
                progress.SetupAndShow(this, 0, 0, false, true);
                for (int i = 0; i < jobs.Length; i++)
                {
                    if (i < (jobs.Length - 1))
                    {
                        uint lower = VCountMinLower;
                        uint upper = VCountMinUpper;
                        jobs[i] =
                            new Thread(
                                () => GenerateSearchJob(testTime, minIVs, maxIVs, lower, upper,
                                                        Timer0Min, Timer0Max, GxStatMin, GxStatMax, VFrameMin,
                                                        VFrameMax, secondsMin, secondsMax, version, language, dstype,
                                                        button,
                                                        checkBoxSoftReset.Checked, checkBoxRoamer.Checked, minMaxGxStat));
                    }
                    else
                    {
                        uint lower = VCountMinLower;
                        jobs[i] =
                            new Thread(
                                () => GenerateSearchJob(testTime, minIVs, maxIVs, lower, VCountMax, Timer0Min,
                                                        Timer0Max, GxStatMin, GxStatMax, VFrameMin, VFrameMax,
                                                        secondsMin, secondsMax, version, language, dstype, button,
                                                        checkBoxSoftReset.Checked, checkBoxRoamer.Checked, minMaxGxStat));
                    }

                    jobs[i].Start();
                    Thread.Sleep(100);
                    VCountMinLower = VCountMinLower + interval + 1;
                    VCountMinUpper = VCountMinUpper + interval + 1;
                }
                bool alive = true;
                while (alive)
                {
                    progress.ShowProgress(progressSearched/(float) progressTotal, progressSearched, progressFound);
                    if (refreshQueue)
                    {
                        listBinding.ResetBindings(false);
                        refreshQueue = false;
                    }
                    foreach (Thread job in jobs)
                    {
                        if (job != null && job.IsAlive)
                        {
                            alive = true;
                            break;
                        }
                        alive = false;
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.Message != "Operation Cancelled")
                {
                    throw;
                }
            }
            finally
            {
                progress.Finish();
                if (dsParameters.Count > 0)
                {
                    btnSendTimeFinder.Enabled = true;
                }
                for (int i = 0; i < jobs.Length; i++)
                {
                    if (jobs[i] != null)
                    {
                        jobs[i].Abort();
                    }
                }
            }
        }

        private void refreshDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dsParameters;
        }

        private void GenerateSearchJob(DateTime testTime, int[] minIVs, int[] maxIVs,
                                       uint VCountMin, uint VCountMax, uint Timer0Min, uint Timer0Max,
                                       uint GxStatMin, uint GxStatMax, uint VFrameMin, uint VFrameMax,
                                       int secondsMin, int secondsMax, Version version, Language language, DSType dstype,
                                       int[] button,
                                       bool softReset, bool roamer, bool minMaxGxStat)
        {
            // offset the start by 2 for BW2
            int offset = version == Version.Black2 || version == Version.White2 ? 2 : 0;
            uint buttonMashed = Functions.buttonMashed(button);

            var array = new uint[80];
            uint[] h = {0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0};

            array[6] = (uint) (MAC_address & 0xFFFF);
            if (softReset)
            {
                array[6] = array[6] ^ 0x01000000;
            }
            var upperMAC = (uint) (MAC_address >> 16);

            // Get the version-unique part of the message
            Array.Copy(Nazos.Nazo(version, language, dstype), array, 5);

            array[10] = 0x00000000;
            array[11] = 0x00000000;
            array[12] = buttonMashed;
            array[13] = 0x80000000;
            array[14] = 0x00000000;
            array[15] = 0x000001A0;


            array[8] = Functions.seedDate(testTime);
            var GxStatList = new List<uint> {GxStatMin};

            // build the GxStat ranges
            if (GxStatMin != GxStatMax)
                if (!minMaxGxStat)
                    GxStatList.Add(GxStatMax);
                else
                    for (uint i = GxStatMin + 1; i <= GxStatMax; ++i)
                        GxStatList.Add(i);

            for (int cntSeconds = secondsMin; cntSeconds <= secondsMax; cntSeconds++)
            {
                array[9] = Functions.seedTime(testTime, dstype);

                for (uint cntVCount = VCountMin; cntVCount <= VCountMax; cntVCount++)
                {
                    for (uint cntTimer0 = Timer0Min; cntTimer0 <= Timer0Max; cntTimer0++)
                    {
                        array[5] = cntVCount*0x10000 + cntTimer0;
                        array[5] = Functions.Reorder(array[5]);
                        foreach (uint GxStat in GxStatList)
                        {
                            for (uint cntVFrame = VFrameMin; cntVFrame <= VFrameMax; cntVFrame++)
                            {
                                uint a = h[0];
                                uint b = h[1];
                                uint c = h[2];
                                uint d = h[3];
                                uint e = h[4];
                                uint f = 0;
                                uint k = 0;

                                array[7] = (upperMAC ^ (cntVFrame*0x1000000) ^ GxStat);

                                for (int i = 0; i < 80; i++)
                                {
                                    if (i < 20)
                                    {
                                        f = (b & c) | ((~b) & d);
                                        k = 0x5A827999;
                                    }
                                    if (i < 40 && i >= 20)
                                    {
                                        f = b ^ c ^ d;
                                        k = 0x6ED9EBA1;
                                    }
                                    if (i < 60 && i >= 40)
                                    {
                                        f = (b & c) | (b & d) | (c & d);
                                        k = 0x8F1BBCDC;
                                    }
                                    if (i >= 60)
                                    {
                                        f = b ^ c ^ d;
                                        k = 0xCA62C1D6;
                                    }

                                    if (i > 15)
                                    {
                                        array[i] =
                                            Functions.RotateLeft(
                                                array[i - 3] ^ array[i - 8] ^ array[i - 14] ^ array[i - 16], 1);
                                    }

                                    uint temp = Functions.RotateLeft(a, 5) + f + e + k + array[i];
                                    e = d;
                                    d = c;
                                    c = Functions.RotateRight(b, 2);
                                    b = a;
                                    a = temp;
                                }

                                uint part1 = Functions.Reorder(h[0] + a);
                                uint part2 = Functions.Reorder(h[1] + b);

                                ulong seed2 = (ulong) part1*0x6C078965;
                                uint seed1 = part2*0x6C078965 + (uint) (seed2 >> 32);
                                seed1 = seed1 + (part1*0x5D588B65);
                                seed2 = (uint) (seed2 & 0xFFFFFFFF) + 0x269EC3;

                                ulong seed = (ulong) (seed1*0x100000000) + seed2;
                                var tempSeed = (uint) (seed >> 32);

                                progressSearched++;
                                if (!findDirectSeed)
                                {
                                    var IVArray = new int[6];
                                    MersenneTwisterFast mt;
                                    if (roamer)
                                    {
                                        mt = new MersenneTwisterFast(tempSeed, 7);
                                        mt.Nextuint();
                                        IVArray[0] = (int) (mt.Nextuint() >> 27);
                                        IVArray[1] = (int) (mt.Nextuint() >> 27);
                                        IVArray[2] = (int) (mt.Nextuint() >> 27);
                                        IVArray[4] = (int) (mt.Nextuint() >> 27);
                                        IVArray[5] = (int) (mt.Nextuint() >> 27);
                                        IVArray[3] = (int) (mt.Nextuint() >> 27);
                                    }
                                    else
                                    {
                                        //advance for BW2
                                        mt = new MersenneTwisterFast(tempSeed, 6 + offset);
                                        for (int i = 0; i < offset; ++i) mt.Nextuint();

                                        IVArray[0] = (int) (mt.Nextuint() >> 27);
                                        IVArray[1] = (int) (mt.Nextuint() >> 27);
                                        IVArray[2] = (int) (mt.Nextuint() >> 27);
                                        IVArray[3] = (int) (mt.Nextuint() >> 27);
                                        IVArray[4] = (int) (mt.Nextuint() >> 27);
                                        IVArray[5] = (int) (mt.Nextuint() >> 27);
                                    }
                                    if ((IVArray[0] >= minIVs[0] && IVArray[0] <= maxIVs[0]) &&
                                        (IVArray[1] >= minIVs[1] && IVArray[1] <= maxIVs[1]) &&
                                        (IVArray[2] >= minIVs[2] && IVArray[2] <= maxIVs[2]) &&
                                        (IVArray[3] >= minIVs[3] && IVArray[3] <= maxIVs[3]) &&
                                        (IVArray[4] >= minIVs[4] && IVArray[4] <= maxIVs[4]) &&
                                        (IVArray[5] >= minIVs[5] && IVArray[5] <= maxIVs[5]))
                                    {
                                        var dsParameterFound =
                                            new DSParameterCapture
                                                {
                                                    ActualSeconds = testTime.Second,
                                                    VCount = cntVCount,
                                                    Timer0 = cntTimer0,
                                                    GxStat = GxStat,
                                                    VFrame = cntVFrame,
                                                    Seed = seed
                                                };
                                        dsParameters.Add(dsParameterFound);
                                        refreshQueue = true;
                                        progressFound++;
                                    }
                                }
                                else
                                {
                                    if (checkBoxHalfSeed.Checked)
                                    {
                                        if (tempSeed == (int) directSeed)
                                        {
                                            var dsParameterFound =
                                                new DSParameterCapture
                                                    {
                                                        ActualSeconds = testTime.Second,
                                                        VCount = cntVCount,
                                                        Timer0 = cntTimer0,
                                                        GxStat = GxStat,
                                                        VFrame = cntVFrame,
                                                        Seed = seed
                                                    };
                                            dsParameters.Add(dsParameterFound);
                                            refreshQueue = true;
                                            progressFound++;
                                        }
                                    }
                                    else
                                    {
                                        if (seed == directSeed)
                                        {
                                            var dsParameterFound =
                                                new DSParameterCapture
                                                    {
                                                        ActualSeconds = testTime.Second,
                                                        VCount = cntVCount,
                                                        Timer0 = cntTimer0,
                                                        GxStat = GxStat,
                                                        VFrame = cntVFrame,
                                                        Seed = seed
                                                    };
                                            dsParameters.Add(dsParameterFound);
                                            refreshQueue = true;
                                            progressFound++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                testTime = testTime.AddSeconds(1);
            }
        }

        private void frmWizard_Load(object sender, EventArgs e)
        {
            resetGridFormatting();
        }

        private void resetGridFormatting()
        {
            clmnVCount.DefaultCellStyle.Format = "X";
            clmnTimer0.DefaultCellStyle.Format = "X";
            clmnGxStat.DefaultCellStyle.Format = "X";
            clmnVFrame.DefaultCellStyle.Format = "X";
            clmnSeed.DefaultCellStyle.Format = "X16";
            clmnActualSeconds.Width = 110;
            clmnVCount.Width = 60;
            clmnTimer0.Width = 60;
            clmnGxStat.Width = 60;
            clmnVFrame.Width = 60;
        }

        private void btnSendTimeFinder_Click(object sender, EventArgs e)
        {
            if (!Profiles.ProfileManager.Visible) Profiles.ProfileManager.Show();
            Profiles.ProfileManager.AddProfile(GetProfile());
            Profiles.ProfileManager.Focus();
            Close();
        }

        private Profile GetProfile()
        {
            var dsParameter = (DSParameterCapture) dataGridView1.SelectedRows[0].DataBoundItem;
            var profile = new Profile
                {
                    MAC_Address = MAC_address,
                    VCount = dsParameter.VCount,
                    Timer0Min = dsParameter.Timer0,
                    Version = (Version) comboBoxVersion.SelectedIndex,
                    Language = (Language) comboBoxLanguage.SelectedIndex,
                    VFrame = dsParameter.VFrame,
                    GxStat = dsParameter.GxStat,
                    DSType = (DSType) comboBoxDSType.SelectedIndex
                };
            return profile;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxHalfSeed_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDirectSeed.MaxLength = checkBoxHalfSeed.Checked ? 8 : 16;
        }

        private void maskedTextBoxHPMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxHPMax.Text = maskedTextBoxHPMin.Text;
        }

        private void maskedTextBoxAtkMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxAtkMax.Text = maskedTextBoxAtkMin.Text;
        }

        private void maskedTextBoxDefMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxDefMax.Text = maskedTextBoxDefMin.Text;
        }

        private void maskedTextBoxSpAtkMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxSpAtkMax.Text = maskedTextBoxSpAtkMin.Text;
        }

        private void maskedTextBoxSpDefMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxSpDefMax.Text = maskedTextBoxSpDefMin.Text;
        }

        private void maskedTextBoxSpeedMin_Leave(object sender, EventArgs e)
        {
            maskedTextBoxSpeedMax.Text = maskedTextBoxSpeedMin.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSoftReset.Checked)
            {
                textBoxGxStatMin.Text = "6";
                textBoxGxStatMax.Text = "86";
            }
            else
            {
                textBoxGxStatMin.Text = "6";
                textBoxGxStatMax.Text = "6";
            }
        }

        private void buttonCalcIVs_Click(object sender, EventArgs e)
        {
            var ivCheck = new DSParametersIVCheck();

            if (ivCheck.ShowDialog() == DialogResult.OK)
            {
                maskedTextBoxHPMin.Text = ivCheck.MinStats[0].ToString();
                maskedTextBoxAtkMin.Text = ivCheck.MinStats[1].ToString();
                maskedTextBoxDefMin.Text = ivCheck.MinStats[2].ToString();
                maskedTextBoxSpAtkMin.Text = ivCheck.MinStats[3].ToString();
                maskedTextBoxSpDefMin.Text = ivCheck.MinStats[4].ToString();
                maskedTextBoxSpeedMin.Text = ivCheck.MinStats[5].ToString();

                maskedTextBoxHPMax.Text = ivCheck.MaxStats[0].ToString();
                maskedTextBoxAtkMax.Text = ivCheck.MaxStats[1].ToString();
                maskedTextBoxDefMax.Text = ivCheck.MaxStats[2].ToString();
                maskedTextBoxSpAtkMax.Text = ivCheck.MaxStats[3].ToString();
                maskedTextBoxSpDefMax.Text = ivCheck.MaxStats[4].ToString();
                maskedTextBoxSpeedMax.Text = ivCheck.MaxStats[5].ToString();
            }
        }
    }
}