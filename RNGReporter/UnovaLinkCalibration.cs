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
    public partial class UnovaLinkCalibration : Form
    {
        private int cpus;
        private List<DSParameterCapture> dsParameters;
        private ulong progressFound;
        private ulong progressSearched;
        private ulong progressTotal;
        private bool refreshQueue;

        public UnovaLinkCalibration()
        {
            InitializeComponent();
        }

        public ulong MAC_address { get; set; }

        private void btnU_Click(object sender, EventArgs e)
        {
            // add a space before the arrow if there's already arrows here
            txtSpins.Text += (txtSpins.Text.Length > 0 ? " " : "") + ((Button) sender).Text;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // if there's more than one arrow remove the arrow and space
            // if not set it to blank
            txtSpins.Text = txtSpins.Text.Length > 2 ? txtSpins.Text.Substring(0, txtSpins.Text.Length - 2) : "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSpins.Text = "";
        }

        private void comboBoxDSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDSType.SelectedIndex)
            {
                case 0:
                    textBoxVCountMin.Text = "70";
                    textBoxVCountMax.Text = "90";
                    textBoxTimer0Min.Text = "10E0";
                    textBoxTimer0Max.Text = "1130";
                    textBoxGxStatMin.Text = "6";
                    textBoxGxStatMax.Text = "6";
                    textBoxVFrameMin.Text = "0";
                    textBoxVFrameMax.Text = "10";
                    break;
                case 1:
                case 2:
                    textBoxVCountMin.Text = "A0";
                    textBoxVCountMax.Text = "C0";
                    textBoxTimer0Min.Text = "1600";
                    textBoxTimer0Max.Text = "1890";
                    textBoxGxStatMin.Text = "6";
                    textBoxGxStatMax.Text = "6";
                    textBoxVFrameMin.Text = "0";
                    textBoxVFrameMax.Text = "10";
                    break;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
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
            dgvResults.DataSource = listBinding;

            cpus = Settings.Default.CPUs;
            if (cpus < 1)
            {
                cpus = 1;
            }

            var jobs = new Thread[cpus];

            var progress = new Progress {Text = "DS Parameter Progress"};
            // this is wrong
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

            MAC_address = ulong.Parse(textBoxMACAddress.Text, NumberStyles.HexNumber);
            if (textBoxMACAddress.Text.Length < 12)
            {
                MessageBox.Show("Your MAC address is missing some digits.  Double-check your MAC address.");
            }

            var version = (Version) (comboBoxVersion.SelectedIndex + 2);
            var language = (Language) comboBoxLanguage.SelectedIndex;
            var dstype = (DSType) comboBoxDSType.SelectedIndex;

            var interval = (uint) ((VCountMax - VCountMin)/(float) jobs.Length);
            uint VCountMinLower = VCountMin;
            uint VCountMinUpper = VCountMin + interval;

            var buttons = new int[3];
            buttons[0] = comboBoxButton1.SelectedIndex;
            buttons[1] = comboBoxButton2.SelectedIndex;
            buttons[2] = comboBoxButton3.SelectedIndex;
            uint button = Functions.buttonMashed(buttons);

            String[] arrows = txtSpins.Text.Split(' ');
            var pattern = new uint[arrows.Length];
            for (int i = 0; i < arrows.Length; ++i)
            {
                switch (arrows[i])
                {
                    case "↑":
                        pattern[i] = 0;
                        break;
                    case "↗":
                        pattern[i] = 1;
                        break;
                    case "→":
                        pattern[i] = 2;
                        break;
                    case "↘":
                        pattern[i] = 3;
                        break;
                    case "↓":
                        pattern[i] = 4;
                        break;
                    case "↙":
                        pattern[i] = 5;
                        break;
                    case "←":
                        pattern[i] = 6;
                        break;
                    case "↖":
                        pattern[i] = 7;
                        break;
                }
            }

            try
            {
                progress.SetupAndShow(this, 0, 0, false, true);
                for (int i = 0; i < jobs.Length; i++)
                {
                    uint lower = VCountMinLower;
                    uint upper = (i < (jobs.Length - 1)) ? VCountMinUpper : VCountMax;
                    jobs[i] =
                        new Thread(
                            () => Search(testTime, lower, upper, Timer0Min, Timer0Max, VFrameMin,
                                         VFrameMax, GxStatMin, GxStatMax, minMaxGxStat, secondsMin, secondsMax,
                                         checkBoxSoftReset.Checked, version, language, dstype, cbMemoryLink.Checked,
                                         MAC_address,
                                         button, pattern));

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
                    //btnSendProfile.Enabled = true;
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

        private void Search(DateTime date, uint vcountMin, uint vcountMax,
                            uint timer0Min, uint timer0Max, uint vframeMin, uint vframeMax, uint gxstatMin,
                            uint gxstatMax, bool minmaxgxstat, int secondsMin, int secondsMax, bool softreset,
                            Version version, Language language,
                            DSType dstype, bool memorylink, ulong macaddress, uint buttons, uint[] pattern)
        {
            var array = new uint[80];
            array[6] = (uint) (macaddress & 0xFFFF);
            if (softreset)
            {
                array[6] = array[6] ^ 0x01000000;
            }
            Array.Copy(Nazos.Nazo(version, language, dstype), array, 5);

            array[10] = 0x00000000;
            array[11] = 0x00000000;
            array[13] = 0x80000000;
            array[14] = 0x00000000;
            array[15] = 0x000001A0;

            array[12] = buttons;

            string yearMonth = String.Format("{0:00}", date.Year%2000) + String.Format("{0:00}", date.Month);
            string dateString = String.Format("{0:00}", (int) date.DayOfWeek);
            dateString = String.Format("{0:00}", date.Day) + dateString;
            dateString = yearMonth + dateString;
            array[8] = uint.Parse(dateString, NumberStyles.HexNumber);
            array[9] = 0x0;
            //uint[] alpha = Functions.alphaSHA1(array, 8);

            var upperMAC = (uint) (macaddress >> 16);

            for (uint vcount = vcountMin; vcount <= vcountMax; ++vcount)
            {
                for (uint timer0 = timer0Min; timer0 <= timer0Max; ++timer0)
                {
                    array[5] = (vcount << 16) + timer0;
                    array[5] = Functions.Reorder(array[5]);

                    for (uint vframe = vframeMin; vframe <= vframeMax; ++vframe)
                    {
                        for (uint gxstat = gxstatMin; gxstat <= gxstatMax; ++gxstat)
                        {
                            array[7] = (upperMAC ^ (vframe*0x1000000) ^ gxstat);
                            uint[] alpha = Functions.alphaSHA1(array);

                            array[16] = Functions.RotateLeft(array[13] ^ array[8] ^ array[2] ^ array[0], 1);
                            array[18] = Functions.RotateLeft(array[15] ^ array[10] ^ array[4] ^ array[2], 1);
                            array[19] = Functions.RotateLeft(array[16] ^ array[11] ^ array[5] ^ array[3], 1);
                            array[21] = Functions.RotateLeft(array[18] ^ array[13] ^ array[7] ^ array[5], 1);
                            array[22] = Functions.RotateLeft(array[19] ^ array[14] ^ array[8] ^ array[6], 1);
                            array[24] = Functions.RotateLeft(array[21] ^ array[16] ^ array[10] ^ array[8], 1);
                            array[27] = Functions.RotateLeft(array[24] ^ array[19] ^ array[13] ^ array[11], 1);

                            for (int second = secondsMin; second <= secondsMax; ++second)
                            {
                                array[9] = Functions.seedSecond(second) | Functions.seedMinute(date.Minute) |
                                           Functions.seedHour(date.Hour, dstype);
                                ulong seed = Functions.EncryptSeed(array, alpha, 9);
                                // it appears to have the same initial seed as in the main game
                                // pressing Unova link does the same probability table calls
                                uint initial = Functions.initialPIDRNG(seed, version, memorylink);
                                var rng = new BWRng(seed);
                                for (uint i = 0; i < initial; ++i) rng.GetNext64BitNumber();
                                bool found = true;
                                for (uint i = 0; i < pattern.Length; ++i)
                                {
                                    if (pattern[i] != rng.GetNext32BitNumber(8))
                                    {
                                        found = false;
                                        break;
                                    }
                                    // there's another RNG call here unreleated to the spinner
                                    rng.GetNext64BitNumber();
                                }
                                progressSearched++;
                                if (found)
                                {
                                    var parameter = new DSParameterCapture
                                        {
                                            ActualSeconds = second,
                                            GxStat = gxstat,
                                            Seed = seed,
                                            Timer0 = timer0,
                                            VCount = vcount,
                                            VFrame = vframe
                                        };
                                    dsParameters.Add(parameter);
                                    refreshQueue = true;
                                    progressFound++;
                                }
                            }


                            if (minmaxgxstat && gxstatMax > gxstatMin) gxstat = gxstatMax - 1;
                        }
                    }
                }
            }
        }

        private void UnovaLinkCalibration_Load(object sender, EventArgs e)
        {
            comboBoxVersion.SelectedIndex = 0;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxDSType.SelectedIndex = 0;
            comboBoxButton1.SelectedIndex = 0;
            comboBoxButton2.SelectedIndex = 0;
            comboBoxButton3.SelectedIndex = 0;
        }

        private void maskedTextBoxSecond_Validated(object sender, EventArgs e)
        {
            maskedTextBoxSecondsMin.Text = maskedTextBoxSecond.Text;
            maskedTextBoxSecondsMax.Text = ((int.Parse(maskedTextBoxSecond.Text) + 10)%60).ToString();
        }

        private void btnSendProfile_Click(object sender, EventArgs e)
        {
            if (!Profiles.ProfileManager.Visible) Profiles.ProfileManager.Show();
            Profiles.ProfileManager.AddProfile(GetProfile());
            Profiles.ProfileManager.Focus();
            Close();
        }

        private Profile GetProfile()
        {
            var dsParameter = (DSParameterCapture) dgvResults.SelectedRows[0].DataBoundItem;
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
    }
}