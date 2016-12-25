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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Win32;
using RNGReporter.Objects;
using RNGReporter.Properties;
using Version = RNGReporter.Objects.Version;

namespace RNGReporter
{
    public partial class SeedFinder : Form
    {
        private uint date;
        private uint gxstat;
        private uint hours;
        private BindingSource listCharacteristic_Stat;
        private BindingSource listNature_A;
        private BindingSource listNature_IVRange;
        private BindingSource listNature_Stat;
        private BindingSource listPokemon_Stat;
        private ulong mac_address;
        private uint minutes;
        private uint month;
        private ulong returnSeed;
        private uint seconds;
        private uint timer0;
        private uint vcount;
        private uint vframe;
        private uint wait;
        private uint year;

        public SeedFinder(int toolTab, int generation)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = toolTab;
            if (generation == 4)
            {
                radioButton4thGen.Checked = true;
            }
            else
            {
                radioButton5thGenNonCGear.Checked = true;
            }
        }

        public ulong ReturnSeed
        {
            get { return returnSeed; }
            set
            {
                returnSeed = value;
                labelSeed.Text = returnSeed.ToString("X");
            }
        }

        private void PlatinumSeed_Load(object sender, EventArgs e)
        {
            listPokemon_Stat = new BindingSource(Pokemon.PokemonCollection(), null);
            listNature_A = new BindingSource(Nature.NatureCollection(), null);
            listNature_Stat = new BindingSource(Nature.NatureCollection(), null);
            listNature_IVRange = new BindingSource(Nature.NatureCollection(), null);
            listCharacteristic_Stat = new BindingSource(Characteristic.CharacteristicCollection(), null);

            //  Load our previous information out of the 
            //  registry so users do not constantly have
            //  to re-enter the information.
            RegistryKey registrySoftware = Registry.CurrentUser.OpenSubKey("Software", true);
            if (registrySoftware != null)
            {
                RegistryKey registryRngReporter = registrySoftware.OpenSubKey("RNGReporter");

                if (Settings.Default.LastVersion < MainForm.VersionNumber && registryRngReporter != null)
                {
                    maskedTextBoxYear.Text = (string) registryRngReporter.GetValue("year", "2000");
                    maskedTextBoxMonth.Text = (string) registryRngReporter.GetValue("month", "01");
                    maskedTextBoxDate.Text = (string) registryRngReporter.GetValue("date", "01");
                    maskedTextBoxHours.Text = (string) registryRngReporter.GetValue("hours", "00");
                    maskedTextBoxMinutes.Text = (string) registryRngReporter.GetValue("minutes", "00");
                    maskedTextBoxSeconds.Text = (string) registryRngReporter.GetValue("seconds", "00");
                    maskedTextBoxDelay.Text = (string) registryRngReporter.GetValue("wait", "0");

                    maskedTextBoxYear_A.Text = (string) registryRngReporter.GetValue("year_a", "2000");
                    maskedTextBoxMonth_A.Text = (string) registryRngReporter.GetValue("month_a", "01");
                    maskedTextBoxDate_A.Text = (string) registryRngReporter.GetValue("date_a", "01");
                    maskedTextBoxHours_A.Text = (string) registryRngReporter.GetValue("hours_a", "00");
                    maskedTextBoxMinutes_A.Text = (string) registryRngReporter.GetValue("minutes_a", "00");

                    var SIV_Mode = (string) registryRngReporter.GetValue("siv_mode", "DPP");

                    switch (SIV_Mode)
                    {
                        case "DPP":
                            radioButton_SIV_DPP.Checked = true;
                            break;
                        case "HGSS":
                            radioButton_SIV_HGSS.Checked = true;
                            break;
                        case "CUSTOM":
                            radioButton_SIV_CUSTOM.Checked = true;
                            break;
                        case "OPEN":
                            radioButton_SIV_OPEN.Checked = true;
                            break;
                    }

                    SIV_RadioChange();

                    var SIV_LowDelay = (string) registryRngReporter.GetValue("siv_lowdelay", "FALSE");
                    checkBoxLowDelay.Checked = SIV_LowDelay == "TRUE";

                    maskedTextBoxYear_Stat.Text = (string) registryRngReporter.GetValue("year_stat", "2000");
                    maskedTextBoxMonth_Stat.Text = (string) registryRngReporter.GetValue("month_stat", "01");
                    maskedTextBoxDate_Stat.Text = (string) registryRngReporter.GetValue("date_stat", "01");
                    maskedTextBoxHours_Stat.Text = (string) registryRngReporter.GetValue("hours_stat", "00");
                    maskedTextBoxMinutes_Stat.Text = (string) registryRngReporter.GetValue("minutes_stat", "00");

                    var SS_Mode = (string) registryRngReporter.GetValue("ss_mode", "DPP");

                    switch (SS_Mode)
                    {
                        case "DPP":
                            radioButton_SS_DPP.Checked = true;
                            break;
                        case "HGSS":
                            radioButton_SS_HGSS.Checked = true;
                            break;
                        case "CUSTOM":
                            radioButton_SS_CUSTOM.Checked = true;
                            break;
                    }

                    SS_RadioChange();

                    maskedTextBoxYear_IVRange.Text = (string) registryRngReporter.GetValue("year_ivrange", "2000");
                    maskedTextBoxMonth_IVRange.Text = (string) registryRngReporter.GetValue("month_ivrange", "01");
                    maskedTextBoxDate_IVRange.Text = (string) registryRngReporter.GetValue("date_ivrange", "01");
                    maskedTextBoxHours_IVRange.Text = (string) registryRngReporter.GetValue("hours_ivrange", "00");
                    maskedTextBoxMinutes_IVRange.Text = (string) registryRngReporter.GetValue("minutes_ivrange", "00");
                    maskedTextBoxMinDelay_IVRange.Text =
                        (string) registryRngReporter.GetValue("min_delay_ivrange", "490");
                    maskedTextBoxMaxDelay_IVRange.Text =
                        (string) registryRngReporter.GetValue("max_delay_ivrange", "1200");

                    maskedTextBoxMinDelay_A.Text = (string) registryRngReporter.GetValue("mindelay_a", "490");
                    maskedTextBoxMaxDelay_A.Text = (string) registryRngReporter.GetValue("maxdelay_a", "1200");

                    maskedTextBoxMinDelay_Stat.Text = (string) registryRngReporter.GetValue("mindelay_stat", "490");
                    maskedTextBoxMaxDelay_Stat.Text = (string) registryRngReporter.GetValue("maxdelay_stat", "1200");
                    Font font;
                    switch ((Language) Settings.Default.Language)
                    {
                        case (Language.Japanese):
                            font = new Font("Meiryo", 7.25F);
                            if (font.Name != "Meiryo")
                            {
                                font = new Font("Arial Unicode MS", 8.25F);
                                if (font.Name != "Arial Unicode MS")
                                {
                                    font = new Font("MS Mincho", 8.25F);
                                }
                            }
                            break;
                        case (Language.Korean):
                            font = new Font("Malgun Gothic", 8.25F);
                            if (font.Name != "Malgun Gothic")
                            {
                                font = new Font("Gulim", 9.25F);
                                if (font.Name != "Gulim")
                                {
                                    font = new Font("Arial Unicode MS", 8.25F);
                                }
                            }
                            break;
                        default:
                            font = DefaultFont;
                            break;
                    }

                    comboBoxPokemon_Stat.Font = font;
                    comboBoxNature_Stat.Font = font;
                    comboBoxNature_A.Font = font;
                    comboBoxNature_IVRange.Font = font;
                    comboBoxCharacteristic_Stat.Font = font;

                    comboBoxPokemon_Stat.DataSource = listPokemon_Stat;
                    comboBoxPokemon_Stat.SelectedIndex = 0;

                    comboBoxNature_Stat.DataSource = listNature_Stat;
                    comboBoxNature_Stat.SelectedIndex = 0;

                    comboBoxNature_A.DataSource = listNature_A;
                    comboBoxNature_A.SelectedIndex = 0;

                    comboBoxNature_IVRange.DataSource = listNature_IVRange;
                    comboBoxNature_IVRange.SelectedIndex = 0;

                    comboBoxCharacteristic_Stat.DataSource = listCharacteristic_Stat;
                    comboBoxCharacteristic_Stat.SelectedIndex = 0;

                    comboBoxVersion.SelectedIndex = (int) registryRngReporter.GetValue("gameversion", 0);
                    comboBoxDSType.SelectedIndex = (int) registryRngReporter.GetValue("dstype", 0);
                }
                else
                {
                    maskedTextBoxYear.Text = Settings.Default.Year;
                    maskedTextBoxMonth.Text = Settings.Default.Month;
                    maskedTextBoxDate.Text = Settings.Default.Date;
                    maskedTextBoxHours.Text = Settings.Default.Hours;
                    maskedTextBoxMinutes.Text = Settings.Default.Minutes;
                    maskedTextBoxSeconds.Text = Settings.Default.Seconds;
                    maskedTextBoxDelay.Text = Settings.Default.Wait;

                    maskedTextBoxYear_A.Text = Settings.Default.YearA;
                    maskedTextBoxMonth_A.Text = Settings.Default.MonthA;
                    maskedTextBoxDate_A.Text = Settings.Default.DateA;
                    maskedTextBoxHours_A.Text = Settings.Default.HoursA;
                    maskedTextBoxMinutes_A.Text = Settings.Default.MinutesA;
                    maskedTextBoxMinDelay_A.Text = Settings.Default.DelayMinA;
                    maskedTextBoxMaxDelay_A.Text = Settings.Default.DelayMaxA;

                    switch (Settings.Default.SIVMode)
                    {
                        case "DPP":
                            radioButton_SIV_DPP.Checked = true;
                            break;
                        case "HGSS":
                            radioButton_SIV_HGSS.Checked = true;
                            break;
                        case "CUSTOM":
                            radioButton_SIV_CUSTOM.Checked = true;
                            break;
                        case "OPEN":
                            radioButton_SIV_OPEN.Checked = true;
                            break;
                    }

                    SIV_RadioChange();

                    checkBoxLowDelay.Checked = Settings.Default.SIVLowDelay;

                    maskedTextBoxYear_Stat.Text = Settings.Default.YearS;
                    maskedTextBoxMonth_Stat.Text = Settings.Default.MonthS;
                    maskedTextBoxDate_Stat.Text = Settings.Default.DateS;
                    maskedTextBoxHours_Stat.Text = Settings.Default.HoursS;
                    maskedTextBoxMinutes_Stat.Text = Settings.Default.MinutesS;
                    maskedTextBoxMinDelay_Stat.Text = Settings.Default.DelayMinS;
                    maskedTextBoxMaxDelay_Stat.Text = Settings.Default.DelayMaxS;

                    switch (Settings.Default.SSMode)
                    {
                        case "DPP":
                            radioButton_SS_DPP.Checked = true;
                            break;
                        case "HGSS":
                            radioButton_SS_HGSS.Checked = true;
                            break;
                        case "CUSTOM":
                            radioButton_SS_CUSTOM.Checked = true;
                            break;
                    }

                    SS_RadioChange();

                    maskedTextBoxYear_IVRange.Text = Settings.Default.YearIVR;
                    maskedTextBoxMonth_IVRange.Text = Settings.Default.MonthIVR;
                    maskedTextBoxDate_IVRange.Text = Settings.Default.DateIVR;
                    maskedTextBoxHours_IVRange.Text = Settings.Default.HoursIVR;
                    maskedTextBoxMinutes_IVRange.Text = Settings.Default.MinutesIVR;
                    maskedTextBoxMinDelay_IVRange.Text = Settings.Default.DelayMinIVR;
                    maskedTextBoxMaxDelay_IVRange.Text = Settings.Default.DelayMaxIVR;

                    Font font;
                    switch ((Language) Settings.Default.Language)
                    {
                        case (Language.Japanese):
                            font = new Font("Meiryo", 7.25F);
                            if (font.Name != "Meiryo")
                            {
                                font = new Font("Arial Unicode MS", 8.25F);
                                if (font.Name != "Arial Unicode MS")
                                {
                                    font = new Font("MS Mincho", 8.25F);
                                }
                            }
                            break;
                        case (Language.Korean):
                            font = new Font("Malgun Gothic", 8.25F);
                            if (font.Name != "Malgun Gothic")
                            {
                                font = new Font("Gulim", 9.25F);
                                if (font.Name != "Gulim")
                                {
                                    font = new Font("Arial Unicode MS", 8.25F);
                                }
                            }
                            break;
                        default:
                            font = DefaultFont;
                            break;
                    }

                    comboBoxPokemon_Stat.Font = font;
                    comboBoxNature_Stat.Font = font;
                    comboBoxNature_A.Font = font;
                    comboBoxNature_IVRange.Font = font;
                    comboBoxCharacteristic_Stat.Font = font;

                    comboBoxPokemon_Stat.DataSource = listPokemon_Stat;
                    comboBoxPokemon_Stat.SelectedIndex = 0;

                    comboBoxNature_Stat.DataSource = listNature_Stat;
                    comboBoxNature_Stat.SelectedIndex = 0;

                    comboBoxNature_A.DataSource = listNature_A;
                    comboBoxNature_A.SelectedIndex = 0;

                    comboBoxNature_IVRange.DataSource = listNature_IVRange;
                    comboBoxNature_IVRange.SelectedIndex = 0;

                    comboBoxCharacteristic_Stat.DataSource = listCharacteristic_Stat;
                    comboBoxCharacteristic_Stat.SelectedIndex = 0;
                }
            }

            comboBoxButton1.SelectedIndex = 0;
            comboBoxButton2.SelectedIndex = 0;
            comboBoxButton3.SelectedIndex = 0;
            comboBoxButton4.SelectedIndex = 0;
            comboBoxButton5.SelectedIndex = 0;
            comboBoxButton6.SelectedIndex = 0;
            comboBoxButton7.SelectedIndex = 0;
            comboBoxVersion.SelectedIndex = 0;
            comboBoxDSType.SelectedIndex = 0;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            if (maskedTextBoxHP_A.Text != "")
                hp = uint.Parse(maskedTextBoxHP_A.Text);
            if (maskedTextBoxAtk_A.Text != "")
                atk = uint.Parse(maskedTextBoxAtk_A.Text);
            if (maskedTextBoxDef_A.Text != "")
                def = uint.Parse(maskedTextBoxDef_A.Text);
            if (maskedTextBoxSpA_A.Text != "")
                spa = uint.Parse(maskedTextBoxSpA_A.Text);
            if (maskedTextBoxSpD_A.Text != "")
                spd = uint.Parse(maskedTextBoxSpD_A.Text);
            if (maskedTextBoxSpe_A.Text != "")
                spe = uint.Parse(maskedTextBoxSpe_A.Text);

            uint year_a = 0;
            uint month_a = 1;
            uint date_a = 1;
            uint hours_a = 0;
            uint minutes_a = 0;

            if (maskedTextBoxYear_A.Text != "")
                year_a = uint.Parse(maskedTextBoxYear_A.Text);

            if (maskedTextBoxMonth_A.Text != "")
                month_a = uint.Parse(maskedTextBoxMonth_A.Text);

            if (maskedTextBoxDate_A.Text != "")
                date_a = uint.Parse(maskedTextBoxDate_A.Text);

            if (maskedTextBoxHours_A.Text != "")
                hours_a = uint.Parse(maskedTextBoxHours_A.Text);

            if (maskedTextBoxMinutes_A.Text != "")
                minutes_a = uint.Parse(maskedTextBoxMinutes_A.Text);

            var nature = (Nature) comboBoxNature_A.SelectedValue;

            bool showMonster = checkBoxShowMonster.Checked;

            //  Get a list of possible starting seeds that we are going to use
            //  to work backwards and find probable initial seeds.
            List<Seed> startingSeeds =
                IVtoSeed.GetSeeds(hp, atk, def, spa, spd, spe, (uint) nature.Number);

            //  Now that we have developed a list of possible seeds we need to
            //  start working those backwards and then building a list of
            //  initial seeds that may have been possible.
            var seeds = new List<SeedInitial>();

            bool monsterFound = false;
            bool initialFound = false;

            uint minDefaultDelay = 550;
            if (radioButton_SIV_HGSS.Checked)
                minDefaultDelay = 400;

            uint maxDefaultDelay = 3600;

            if (maskedTextBoxMinDelay_A.Text != "" && radioButton_SIV_CUSTOM.Checked)
                minDefaultDelay = uint.Parse(maskedTextBoxMinDelay_A.Text);
            if (maskedTextBoxMaxDelay_A.Text != "" && radioButton_SIV_CUSTOM.Checked)
                maxDefaultDelay = uint.Parse(maskedTextBoxMaxDelay_A.Text);


            foreach (Seed seed in startingSeeds)
            {
                if (seed.FrameType == FrameType.Method1)
                {
                    if (showMonster)
                    {
                        seeds.Add(new SeedInitial(seed.MonsterSeed, 0, "Monster", 0, 0));
                    }

                    monsterFound = true;

                    //  start backing up, we are going to back up
                    //  a grand totol of 1000 times max for the
                    //  time being,
                    var rng = new PokeRngR(seed.MonsterSeed);

                    for (uint backCount = 1; backCount < 2000; backCount++)
                    {
                        uint testSeed = rng.Seed;
                        rng.GetNext16BitNumber();

                        uint seedAB = testSeed >> 24;
                        uint seedCD = (testSeed & 0x00FF0000) >> 16;
                        uint seedEFGH = testSeed & 0x0000FFFF;

                        if ((seedEFGH > (minDefaultDelay + year_a - 2000) &&
                             seedEFGH < (maxDefaultDelay + year_a - 2000)) ||
                            radioButton_SIV_OPEN.Checked)
                        {
                            //  Disqualify on hours second as it is very likely
                            //  to be a poor choice for use to work with.
                            if ((seedCD == hours_a) || radioButton_SIV_OPEN.Checked)
                            {
                                if (!radioButton_SIV_OPEN.Checked)
                                {
                                    for (uint secondsCount = 0; secondsCount < 60; secondsCount++)
                                    {
                                        if (((month_a*date_a + minutes_a + secondsCount)%0x100) == seedAB)
                                        {
                                            var initialSeed =
                                                new SeedInitial(
                                                    testSeed,
                                                    backCount,
                                                    "Initial",
                                                    secondsCount,
                                                    seedEFGH - (year_a - 2000));

                                            seeds.Add(initialSeed);

                                            initialFound = true;
                                        }
                                    }
                                }
                                else // if (!checkBoxOpenSearch.Checked)
                                {
                                    //  Do the open search, which is completely open and does not
                                    //  honor the min/max delay of the advanced search, so we should
                                    //  likely make the selection of those mutially exclusive.
                                    if (seedCD < 24)
                                    {
                                        if ((checkBoxLowDelay.Checked && seedEFGH < 10000) || !checkBoxLowDelay.Checked)
                                        {
                                            var initialSeed =
                                                new SeedInitial(
                                                    testSeed,
                                                    backCount,
                                                    "Initial",
                                                    0,
                                                    seedEFGH - (year_a - 2000));

                                            seeds.Add(initialSeed);

                                            initialFound = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            dataGridViewValues.DataSource = seeds;

            if (!monsterFound)
            {
                MessageBox.Show("No matches found for the IVs entered.  Please check and try again.", "No Data Found",
                                MessageBoxButtons.OK);
            }
            else if (!initialFound)
            {
                MessageBox.Show("No reasonable initial seed found. Please check your DATE and TIME.", "No Data Found",
                                MessageBoxButtons.OK);
            }

            Settings.Default.YearA = year_a.ToString();
            Settings.Default.MonthA = month_a.ToString();
            Settings.Default.DateA = date_a.ToString();
            Settings.Default.MinutesA = minutes_a.ToString();
            Settings.Default.HoursA = hours_a.ToString();

            if (maskedTextBoxMinDelay_A.Text != "" && radioButton_SIV_CUSTOM.Checked)
            {
                minDefaultDelay = uint.Parse(maskedTextBoxMinDelay_A.Text);
                Settings.Default.DelayMinA = minDefaultDelay.ToString();
            }
            if (maskedTextBoxMaxDelay_A.Text != "" && radioButton_SIV_CUSTOM.Checked)
            {
                maxDefaultDelay = uint.Parse(maskedTextBoxMaxDelay_A.Text);
                Settings.Default.DelayMaxA = maxDefaultDelay.ToString();
            }

            string SIV_Mode = "DPP";
            if (radioButton_SIV_DPP.Checked)
            {
                SIV_Mode = "DPP";
            }
            else if (radioButton_SIV_HGSS.Checked)
            {
                SIV_Mode = "HGSS";
            }
            else if (radioButton_SIV_CUSTOM.Checked)
            {
                SIV_Mode = "CUSTOM";
            }
            else if (radioButton_SIV_OPEN.Checked)
            {
                SIV_Mode = "OPEN";
            }
            Settings.Default.SIVMode = SIV_Mode;
            Settings.Default.SIVLowDelay = checkBoxLowDelay.Checked;
            Settings.Default.Save();
        }

        private void buttonFind_Stat_Click(object sender, EventArgs e)
        {
            uint year_stat = 0;
            uint month_stat = 1;
            uint date_stat = 1;
            uint hours_stat = 0;
            uint minutes_stat = 0;

            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            uint level = 1;

            var pokemon = (Pokemon) comboBoxPokemon_Stat.SelectedValue;
            var nature = (Nature) comboBoxNature_Stat.SelectedValue;

            if (maskedTextBoxHP_Stat.Text != "")
                hp = uint.Parse(maskedTextBoxHP_Stat.Text);

            if (maskedTextBoxAtk_Stat.Text != "")
                atk = uint.Parse(maskedTextBoxAtk_Stat.Text);

            if (maskedTextBoxDef_Stat.Text != "")
                def = uint.Parse(maskedTextBoxDef_Stat.Text);

            if (maskedTextBoxSpA_Stat.Text != "")
                spa = uint.Parse(maskedTextBoxSpA_Stat.Text);

            if (maskedTextBoxSpD_Stat.Text != "")
                spd = uint.Parse(maskedTextBoxSpD_Stat.Text);

            if (maskedTextBoxSpe_Stat.Text != "")
                spe = uint.Parse(maskedTextBoxSpe_Stat.Text);

            if (maskedTextBoxLevel_Stat.Text != "")
                level = uint.Parse(maskedTextBoxLevel_Stat.Text);

            if (maskedTextBoxYear_Stat.Text != "")
                year_stat = uint.Parse(maskedTextBoxYear_Stat.Text);

            if (maskedTextBoxMonth_Stat.Text != "")
                month_stat = uint.Parse(maskedTextBoxMonth_Stat.Text);

            if (maskedTextBoxDate_Stat.Text != "")
                date_stat = uint.Parse(maskedTextBoxDate_Stat.Text);

            if (maskedTextBoxHours_Stat.Text != "")
                hours_stat = uint.Parse(maskedTextBoxHours_Stat.Text);

            if (maskedTextBoxMinutes_Stat.Text != "")
                minutes_stat = uint.Parse(maskedTextBoxMinutes_Stat.Text);

            bool showMonster = checkBoxShowMonster.Checked;

            var stats = new[] {hp, atk, def, spa, spd, spe};

            Characteristic characteristic = null;

            if (comboBoxCharacteristic_Stat.SelectedItem.ToString() != "NONE")
            {
                characteristic = (Characteristic) comboBoxCharacteristic_Stat.SelectedItem;
            }

            //  Run the IV checker with the stats and monster information
            //  that were entered into the intial seed finder,
            var ivCheck = new IVCheck(pokemon, level, nature, characteristic, stats);

            //  If there are any invalid IVs we need to notify the user
            //  what we may not proceed.
            if (!ivCheck.IsValid)
            {
                MessageBox.Show(
                    "There was a problem with the stats/nature/Pokemon you have entered.  Please check them and try again. " +
                    Environment.NewLine + Environment.NewLine + ivCheck, "Invalid Stats");
            }
            else
            {
                uint minDefaultDelay = 550;
                if (radioButton_SS_HGSS.Checked)
                    minDefaultDelay = 400;

                uint maxDefaultDelay = 1200;

                if (maskedTextBoxMinDelay_Stat.Text != "" && radioButton_SS_CUSTOM.Checked)
                    minDefaultDelay = uint.Parse(maskedTextBoxMinDelay_Stat.Text);
                if (maskedTextBoxMaxDelay_Stat.Text != "" && radioButton_SS_CUSTOM.Checked)
                    maxDefaultDelay = uint.Parse(maskedTextBoxMaxDelay_Stat.Text);

                int combinations =
                    ivCheck.Possibilities[0].Count*
                    ivCheck.Possibilities[1].Count*
                    ivCheck.Possibilities[2].Count*
                    ivCheck.Possibilities[3].Count*
                    ivCheck.Possibilities[4].Count*
                    ivCheck.Possibilities[5].Count;

                //  If there are too many different combinations we need
                //  to notify the user that they may not proceed.
                if (combinations > 100)
                {
                    MessageBox.Show(
                        "There were too many combinations of IV possibilities to accurately find your intitial seed (" +
                        combinations + ") please try with a higher level Pokemon,", "To many IV Combinations");
                }
                else
                {
                    var allSeeds = new List<Seed>();

                    //  Iterate through all of the combinations of IVs and check
                    //  so something good, first using the IvToPID.
                    foreach (uint combo_HP in ivCheck.Possibilities[0])
                        foreach (uint combo_Atk in ivCheck.Possibilities[1])
                            foreach (uint combo_Def in ivCheck.Possibilities[2])
                                foreach (uint combo_SpA in ivCheck.Possibilities[3])
                                    foreach (uint combo_SpD in ivCheck.Possibilities[4])
                                        foreach (uint combo_Spe in ivCheck.Possibilities[5])
                                        {
                                            List<Seed> startingSeeds =
                                                IVtoSeed.GetSeeds(
                                                    combo_HP, combo_Atk, combo_Def,
                                                    combo_SpA, combo_SpD, combo_Spe,
                                                    (uint) nature.Number, 0);

                                            allSeeds.AddRange(startingSeeds);
                                        }

                    //  We now have a complete list of starting seeds so we can run the 
                    //  same logic that we normally run here, but we might want to tone
                    //  down how much we actually search.

                    //  Now that we have developed a list of possible seeds we need to
                    //  start working those backwards and then building a list of
                    //  initial seeds that may have been possible.
                    var seeds = new List<SeedInitial>();

                    bool monsterFound = false;
                    bool initialFound = false;

                    foreach (Seed seed in allSeeds)
                    {
                        if (seed.FrameType == FrameType.Method1)
                        {
                            if (showMonster)
                            {
                                seeds.Add(new SeedInitial(seed.MonsterSeed, 0, "Monster", 0, 0));
                            }

                            monsterFound = true;

                            //  start backing up, we are going to back up
                            //  a grand totol of 10,000 times max for the
                            //  time being,
                            var rng = new PokeRngR(seed.MonsterSeed);

                            //  back to 500
                            for (uint backCount = 1; backCount < 500; backCount++)
                            {
                                uint testSeed = rng.Seed;
                                rng.GetNext32BitNumber();

                                uint seedAB = testSeed >> 24;
                                uint seedCD = (testSeed & 0x00FF0000) >> 16;
                                uint seedEFGH = testSeed & 0x0000FFFF;

                                if (seedEFGH > (minDefaultDelay + year_stat - 2000) &&
                                    seedEFGH < (maxDefaultDelay + year_stat - 2000))
                                {
                                    //  Disqualify on hours second as it is very likely
                                    //  to be a poor choice for use to work with.
                                    //wfy back to exact hour
                                    if (seedCD == hours_stat)
                                    {
                                        for (uint secondsCount = 0; secondsCount < 60; secondsCount++)
                                        {
                                            if (((month_stat*date_stat + minutes_stat + secondsCount)%0x100) == seedAB)
                                            {
                                                var initialSeed =
                                                    new SeedInitial(
                                                        testSeed,
                                                        backCount,
                                                        "Initial",
                                                        secondsCount,
                                                        seedEFGH - (year_stat - 2000));

                                                seeds.Add(initialSeed);

                                                initialFound = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    dataGridViewValues_Stat.DataSource = seeds;

                    if (!monsterFound)
                    {
                        MessageBox.Show("No matches found for the IVs entered.  Please check and try again.",
                                        "No Data Found", MessageBoxButtons.OK);
                    }
                    else if (!initialFound)
                    {
                        MessageBox.Show("No reasonable initial seed found. Please check your DATE and TIME.",
                                        "No Data Found", MessageBoxButtons.OK);
                    }

                    //  Save our information for the next run
                    Settings.Default.YearS = year_stat.ToString();
                    Settings.Default.MonthS = month_stat.ToString();
                    Settings.Default.DateS = date_stat.ToString();
                    Settings.Default.MinutesS = minutes_stat.ToString();
                    Settings.Default.HoursS = hours_stat.ToString();

                    if (maskedTextBoxMinDelay_Stat.Text != "" && radioButton_SIV_CUSTOM.Checked)
                    {
                        minDefaultDelay = uint.Parse(maskedTextBoxMinDelay_Stat.Text);
                        Settings.Default.DelayMinS = minDefaultDelay.ToString();
                    }
                    if (maskedTextBoxMaxDelay_Stat.Text != "" && radioButton_SIV_CUSTOM.Checked)
                    {
                        maxDefaultDelay = uint.Parse(maskedTextBoxMaxDelay_Stat.Text);
                        Settings.Default.DelayMaxS = maxDefaultDelay.ToString();
                    }


                    string SS_Mode = "DPP";

                    if (radioButton_SS_DPP.Checked)
                    {
                        SS_Mode = "DPP";
                    }
                    else if (radioButton_SS_HGSS.Checked)
                    {
                        SS_Mode = "HGSS";
                    }
                    else if (radioButton_SS_CUSTOM.Checked)
                    {
                        SS_Mode = "CUSTOM";
                    }
                    Settings.Default.SSMode = SS_Mode;
                    Settings.Default.Save();
                }
            }
        }

        private void buttonSimpleSeed_Click(object sender, EventArgs e)
        {
            //  Calculate our seed.

            if (maskedTextBoxYear.Text != "")
                year = uint.Parse(maskedTextBoxYear.Text);
            if (maskedTextBoxMonth.Text != "")
                month = uint.Parse(maskedTextBoxMonth.Text);
            if (maskedTextBoxDate.Text != "")
                date = uint.Parse(maskedTextBoxDate.Text);
            if (maskedTextBoxHours.Text != "")
                hours = uint.Parse(maskedTextBoxHours.Text);
            if (maskedTextBoxMinutes.Text != "")
                minutes = uint.Parse(maskedTextBoxMinutes.Text);
            if (maskedTextBoxSeconds.Text != "")
                seconds = uint.Parse(maskedTextBoxSeconds.Text);
            if (maskedTextBoxDelay.Text != "")
                wait = uint.Parse(maskedTextBoxDelay.Text);

            if (textBoxMACAddress.Text != "")
                mac_address = ulong.Parse(textBoxMACAddress.Text, NumberStyles.HexNumber);

            if (textBoxVCount.Text != "")
                vcount = uint.Parse(textBoxVCount.Text, NumberStyles.HexNumber);
            if (textBoxTimer0.Text != "")
                timer0 = uint.Parse(textBoxTimer0.Text, NumberStyles.HexNumber);
            if (textBoxGxStat.Text != "")
                gxstat = uint.Parse(textBoxGxStat.Text, NumberStyles.HexNumber);
            if (textBoxVFrame.Text != "")
                vframe = uint.Parse(textBoxVFrame.Text, NumberStyles.HexNumber);

            Settings.Default.Year = year.ToString();
            Settings.Default.Month = month.ToString();
            Settings.Default.Date = date.ToString();
            Settings.Default.Minutes = minutes.ToString();
            Settings.Default.Hours = hours.ToString();
            Settings.Default.Seconds = seconds.ToString();
            Settings.Default.Wait = wait.ToString();
            Settings.Default.Save();

            //  Set seed variable
            if (radioButton4thGen.Checked)
                ReturnSeed = (((month*date + minutes + seconds)%0x100)*0x1000000) + (hours*0x10000) +
                             (year - 2000 + wait);
            else if (radioButton5thGenCGear.Checked)
                ReturnSeed = (((month*date + minutes + seconds)%0x100)*0x1000000) + (hours*0x10000) +
                             (year - 2000 + wait) + (mac_address & 0xFFFFFF);
            else if (radioButton5thGenNonCGear.Checked)
            {
                var version = (Version) comboBoxVersion.SelectedIndex;
                var language = (Language) comboBoxLanguage.SelectedIndex;
                var dstype = (DSType) comboBoxDSType.SelectedIndex;

                var buttonHeld = new int[7];
                buttonHeld[0] = comboBoxButton1.SelectedIndex;
                buttonHeld[1] = comboBoxButton2.SelectedIndex;
                buttonHeld[2] = comboBoxButton3.SelectedIndex;
                buttonHeld[3] = comboBoxButton4.SelectedIndex;
                buttonHeld[4] = comboBoxButton5.SelectedIndex;
                buttonHeld[5] = comboBoxButton6.SelectedIndex;
                buttonHeld[6] = comboBoxButton7.SelectedIndex;

                var dateTime = new DateTime((int) year, (int) month, (int) date, (int) hours, (int) minutes,
                                            (int) seconds);
                ReturnSeed = Functions.EncryptSeed(dateTime, mac_address, version, language, dstype,
                                                   checkBoxSoftReset.Checked,
                                                   vcount, timer0, gxstat, vframe, Functions.buttonMashed(buttonHeld));
            }
        }

        private void buttonFind_ByIVRange_Click(object sender, EventArgs e)
        {
            uint year_ivrange = 0;
            uint month_ivrange = 1;
            uint date_ivrange = 1;
            uint hours_ivrange = 0;
            uint minutes_ivrange = 0;

            var nature = (Nature) comboBoxNature_IVRange.SelectedValue;

            if (maskedTextBoxYear_IVRange.Text != "")
                year_ivrange = uint.Parse(maskedTextBoxYear_IVRange.Text);

            if (maskedTextBoxMonth_IVRange.Text != "")
                month_ivrange = uint.Parse(maskedTextBoxMonth_IVRange.Text);

            if (maskedTextBoxDate_IVRange.Text != "")
                date_ivrange = uint.Parse(maskedTextBoxDate_IVRange.Text);

            if (maskedTextBoxHours_IVRange.Text != "")
                hours_ivrange = uint.Parse(maskedTextBoxHours_IVRange.Text);

            if (maskedTextBoxMinutes_IVRange.Text != "")
                minutes_ivrange = uint.Parse(maskedTextBoxMinutes_IVRange.Text);

            bool showMonster = checkBoxShowMonster.Checked;

            var Possibilities =
                new List<List<uint>>
                    {
                        new List<uint>(),
                        new List<uint>(),
                        new List<uint>(),
                        new List<uint>(),
                        new List<uint>(),
                        new List<uint>()
                    };

            var minIvs = new uint[6];
            uint.TryParse(maskedTextBoxMinHP.Text, out minIvs[0]);
            uint.TryParse(maskedTextBoxMinAtk.Text, out minIvs[1]);
            uint.TryParse(maskedTextBoxMinDef.Text, out minIvs[2]);
            uint.TryParse(maskedTextBoxMinSpAtk.Text, out minIvs[3]);
            uint.TryParse(maskedTextBoxMinSpDef.Text, out minIvs[4]);
            uint.TryParse(maskedTextBoxMinSpeed.Text, out minIvs[5]);
            var maxIvs = new uint[6];
            uint.TryParse(maskedTextBoxMaxHP.Text, out maxIvs[0]);
            uint.TryParse(maskedTextBoxMaxAtk.Text, out maxIvs[1]);
            uint.TryParse(maskedTextBoxMaxDef.Text, out maxIvs[2]);
            uint.TryParse(maskedTextBoxMaxSpAtk.Text, out maxIvs[3]);
            uint.TryParse(maskedTextBoxMaxSpDef.Text, out maxIvs[4]);
            uint.TryParse(maskedTextBoxMaxSpeed.Text, out maxIvs[5]);

            uint minDefaultDelay;
            uint.TryParse(maskedTextBoxMinDelay_IVRange.Text, out minDefaultDelay);
            uint maxDefaultDelay;
            uint.TryParse(maskedTextBoxMaxDelay_IVRange.Text, out maxDefaultDelay);

            for (int statCnt = 0; statCnt <= 5; statCnt++)
            {
                for (uint charCnt = minIvs[statCnt]; charCnt <= maxIvs[statCnt]; charCnt++)
                {
                    Possibilities[statCnt].Add(charCnt);
                }
            }

            int combinations =
                Possibilities[0].Count*
                Possibilities[1].Count*
                Possibilities[2].Count*
                Possibilities[3].Count*
                Possibilities[4].Count*
                Possibilities[5].Count;


            //  If there are too many different combinations we need
            //  to notify the user that they may not proceed.
            if (combinations > 100)
            {
                MessageBox.Show(
                    "There were too many combinations of IV possibilities to accurately find your intitial seed (" +
                    combinations + ") please try with a higher level Pokemon,", "Too many IV Combinations");
            }
            else
            {
                var allSeeds = new List<Seed>();

                //  Iterate through all of the combinations of IVs and check
                //  so something good, first using the IvToPID.
                foreach (uint combo_HP in Possibilities[0])
                    foreach (uint combo_Atk in Possibilities[1])
                        foreach (uint combo_Def in Possibilities[2])
                            foreach (uint combo_SpA in Possibilities[3])
                                foreach (uint combo_SpD in Possibilities[4])
                                    foreach (uint combo_Spe in Possibilities[5])
                                    {
                                        List<Seed> startingSeeds =
                                            IVtoSeed.GetSeeds(
                                                combo_HP, combo_Atk, combo_Def,
                                                combo_SpA, combo_SpD, combo_Spe,
                                                (uint) nature.Number, 0);

                                        allSeeds.AddRange(startingSeeds);
                                    }

                //  We now have a complete list of starting seeds so we can run the 
                //  same logic that we normally run here, but we might want to tone
                //  down how much we actually search.

                //  Now that we have developed a list of possible seeds we need to
                //  start working those backwards and then building a list of
                //  initial seeds that may have been possible.
                var seeds = new List<SeedInitial>();

                bool monsterFound = false;
                bool initialFound = false;

                foreach (Seed seed in allSeeds)
                {
                    if (seed.FrameType == FrameType.Method1)
                    {
                        if (showMonster)
                        {
                            seeds.Add(new SeedInitial(seed.MonsterSeed, 0, "Monster", 0, 0));
                        }

                        monsterFound = true;

                        //  start backing up, we are going to back up
                        //  a grand totol of 10,000 times max for the
                        //  time being,
                        var rng = new PokeRngR(seed.MonsterSeed);

                        //  back to 500
                        for (uint backCount = 1; backCount < 500; backCount++)
                        {
                            uint testSeed = rng.Seed;
                            rng.GetNext32BitNumber();

                            uint seedAB = testSeed >> 24;
                            uint seedCD = (testSeed & 0x00FF0000) >> 16;
                            uint seedEFGH = testSeed & 0x0000FFFF;

                            if (seedEFGH > (minDefaultDelay + year_ivrange - 2000) &&
                                seedEFGH < (maxDefaultDelay + year_ivrange - 2000))
                            {
                                //  Disqualify on hours second as it is very likely
                                //  to be a poor choice for use to work with.
                                //wfy back to exact hour
                                if (seedCD == hours_ivrange)
                                {
                                    for (uint secondsCount = 0; secondsCount < 60; secondsCount++)
                                    {
                                        if (((month_ivrange*date_ivrange + minutes_ivrange + secondsCount)%0x100) ==
                                            seedAB)
                                        {
                                            var initialSeed =
                                                new SeedInitial(
                                                    testSeed,
                                                    backCount,
                                                    "Initial",
                                                    secondsCount,
                                                    seedEFGH - (year_ivrange - 2000));

                                            seeds.Add(initialSeed);

                                            initialFound = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                dataGridViewSeeds_IVRange.DataSource = seeds;

                if (!monsterFound)
                {
                    MessageBox.Show("No matches found for the IVs entered.  Please check and try again.",
                                    "No Data Found", MessageBoxButtons.OK);
                }
                else if (!initialFound)
                {
                    MessageBox.Show("No reasonable initial seed found. Please check your DATE and TIME.",
                                    "No Data Found", MessageBoxButtons.OK);
                }

                Settings.Default.YearIVR = year_ivrange.ToString();
                Settings.Default.MonthIVR = month_ivrange.ToString();
                Settings.Default.DateIVR = date_ivrange.ToString();
                Settings.Default.MinutesIVR = minutes_ivrange.ToString();
                Settings.Default.HoursIVR = hours_ivrange.ToString();
                Settings.Default.DelayMinIVR = minDefaultDelay.ToString();
                Settings.Default.DelayMaxIVR = maxDefaultDelay.ToString();
                Settings.Default.Save();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //  We are done and returning a seed.
        }

        private void dataGridViewValues_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count > 0)
            {
                var seed = (SeedInitial) dataGridViewValues.SelectedRows[0].DataBoundItem;

                ReturnSeed = seed.RngSeed;
            }
        }

        private void dataGridViewValues_Stat_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewValues_Stat.SelectedRows.Count > 0)
            {
                var seed = (SeedInitial) dataGridViewValues_Stat.SelectedRows[0].DataBoundItem;

                ReturnSeed = seed.RngSeed;
            }
        }

        private void radioButton_SIV_CUSTOM_CheckedChanged(object sender, EventArgs e)
        {
            SIV_RadioChange();
        }

        private void radioButton_SIV_OPEN_CheckedChanged(object sender, EventArgs e)
        {
            SIV_RadioChange();
        }

        private void SIV_RadioChange()
        {
            label_SIV_MinDelay.Visible = radioButton_SIV_CUSTOM.Checked;
            label_SIV_MaxDelay.Visible = radioButton_SIV_CUSTOM.Checked;
            maskedTextBoxMaxDelay_A.Visible = radioButton_SIV_CUSTOM.Checked;
            maskedTextBoxMinDelay_A.Visible = radioButton_SIV_CUSTOM.Checked;

            checkBoxLowDelay.Visible = radioButton_SIV_OPEN.Checked;

            maskedTextBoxYear_A.Enabled = !radioButton_SIV_OPEN.Checked;
            maskedTextBoxMonth_A.Enabled = !radioButton_SIV_OPEN.Checked;
            maskedTextBoxDate_A.Enabled = !radioButton_SIV_OPEN.Checked;
            maskedTextBoxHours_A.Enabled = !radioButton_SIV_OPEN.Checked;
            maskedTextBoxMinutes_A.Enabled = !radioButton_SIV_OPEN.Checked;
        }

        private void radioButton_SS_CUSTOM_CheckedChanged(object sender, EventArgs e)
        {
            SS_RadioChange();
        }

        private void SS_RadioChange()
        {
            label_SS_MinDelay.Visible = radioButton_SS_CUSTOM.Checked;
            label_SS_MaxDelay.Visible = radioButton_SS_CUSTOM.Checked;
            maskedTextBoxMaxDelay_Stat.Visible = radioButton_SS_CUSTOM.Checked;
            maskedTextBoxMinDelay_Stat.Visible = radioButton_SS_CUSTOM.Checked;
        }

        private void comboBoxButtonHeld_SelectedIndexChanged(object sender, EventArgs e)
        {
            var buttonHeld = new int[7];
            buttonHeld[0] = comboBoxButton1.SelectedIndex;
            buttonHeld[1] = comboBoxButton2.SelectedIndex;
            buttonHeld[2] = comboBoxButton3.SelectedIndex;
            buttonHeld[3] = comboBoxButton4.SelectedIndex;
            buttonHeld[4] = comboBoxButton5.SelectedIndex;
            buttonHeld[5] = comboBoxButton6.SelectedIndex;
            buttonHeld[6] = comboBoxButton7.SelectedIndex;

            textBoxButtonCode.Text = Functions.buttonMashed(buttonHeld).ToString("X4");
        }
    }
}