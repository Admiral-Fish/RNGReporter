using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class GameCube : Form
    {
        private readonly uint[] natures = { 100, 3, 2, 5, 20, 23, 11, 8, 13, 1, 16, 15, 14, 4, 17, 19, 7, 22, 10, 21, 9, 18, 6, 0, 24, 12 };
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private readonly String[] hiddenPowers = { "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark" };
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<DisplayList> displayList;
        private bool isSearching = false;
        private List<uint> slist = new List<uint>();
        private List<uint> rlist = new List<uint>();
        private uint shinyval;

        public GameCube()
        {
            InitializeComponent();
        }

        public GameCube(int TID, int SID)
        {
            InitializeComponent();
            natureType.SelectedIndex = 0;
            abilityType.SelectedIndex = 0;
            genderType.SelectedIndex = 0;
            hiddenpower.SelectedIndex = 0;
            searchMethod.SelectedIndex = 0;
            shadowPokemon.SelectedIndex = 0;
            id.Text = TID.ToString();
            sid.Text = SID.ToString();
            k_dataGridView.DataSource = binding;
            k_dataGridView.AutoGenerateColumns = false;
        }

        private void GameCube_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (searchThread != null)
            {
                searchThread.Abort();
                status.Text = "Cancelled. - Awaiting Command";
            }
            Hide();
        }

        private void search_Click(object sender, EventArgs e)
        {
            uint[] ivsLower = { (uint)HPLow.Value, (uint)AtkLow.Value, (uint)DefLow.Value, (uint)SpALow.Value, (uint)SpDLow.Value, (uint)SpeLow.Value };
            uint[] ivsUpper = { (uint)HPHigh.Value, (uint)AtkHigh.Value, (uint)DefHigh.Value, (uint)SpAHigh.Value, (uint)SpDHigh.Value, (uint)SpeHigh.Value };

            if (ivsLower[0] > ivsUpper[0])
                MessageBox.Show("HP: Lower limit > Upper limit");
            else if (ivsLower[1] > ivsUpper[1])
                MessageBox.Show("Atk: Lower limit > Upper limit");
            else if (ivsLower[2] > ivsUpper[2])
                MessageBox.Show("Def: Lower limit > Upper limit");
            else if (ivsLower[3] > ivsUpper[3])
                MessageBox.Show("SpA: Lower limit > Upper limit");
            else if (ivsLower[4] > ivsUpper[4])
                MessageBox.Show("SpD: Lower limit > Upper limit");
            else if (ivsLower[5] > ivsUpper[5])
                MessageBox.Show("Spe: Lower limit > Upper limit");
            else
            {
                k_dataGridView.Rows.Clear();

                if (isSearching)
                {
                    status.Text = "Previous search is still running";
                    return;
                }

                displayList = new List<DisplayList>();
                binding = new BindingSource { DataSource = displayList };
                k_dataGridView.DataSource = binding;
                status.Text = "Searching";
                slist.Clear();
                rlist.Clear();
                shinyval = (uint.Parse(id.Text) ^ uint.Parse(sid.Text)) >> 3;

                searchThread =
                    new Thread(
                        () =>
                        getSearch(ivsLower, ivsUpper));
                searchThread.Start();

                var update = new Thread(updateGUI);
                update.Start();
            }
        }

        private void getSearch(uint[] ivsLower, uint[] ivsUpper)
        {
            uint test = getSearchMethod();
            if (test == 0)
                getRMethod(ivsLower, ivsUpper);
            else if (test == 1)
                getMethod(ivsLower, ivsUpper);
            else
                generateChannel(ivsLower, ivsUpper, getNature());
        }

        #region Gales/Colo search
        private void getMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 16384)
                generate2(ivsLower, ivsUpper, getNature());
            else
                generate(ivsLower, ivsUpper);
        }

        #region First search method
        private void generate(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint nature = getNature();
            if (nature == 0)
                nature = 100;
            else
                nature = natures[nature];
            uint ability = getAbility();
            uint gender = getGender();
            uint hp = getHP();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
            {
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                {
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                    {
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                {
                                    checkSeed(a, b, c, d, e, f, nature, ability, gender, hp);
                                }
                            }
                        }
                    }
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        //Credit to RNG Reporter for this
        private void checkSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP)
        {
            uint x8 = 0;
            uint x8_2 = 0;

            x8 = hp + (atk << 5) + (def << 10);
            x8_2 = x8 ^ 0x8000;

            for (uint cnt = 0; cnt <= 0x1FFFE; cnt++)
            {
                uint x_testXD = (cnt & 1) == 0 ? x8 : x8_2;
                uint seed = (x_testXD << 16) + (cnt % 0xFFFF);
                uint ColoSeed = reverse(seed);

                uint rng1XD = forward(seed);
                uint rng2XD = forward(rng1XD);
                uint rng3XD = forward(rng2XD);
                uint rng4XD = forward(rng3XD);
                rng1XD >>= 16;
                rng2XD >>= 16;
                rng3XD >>= 16;
                rng4XD >>= 16;

                if (Check(rng1XD, rng3XD, rng4XD, spe, spa, spd, nature))
                {
                    filterSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hP, rng1XD, rng3XD, rng4XD, ColoSeed);
                }
            }
        }

        private static bool Check(uint iv, uint pid2, uint pid1, uint hp, uint atk, uint def, uint nature)
        {
            bool ret = false;

            uint test_hp = iv & 0x1f;
            uint test_atk = (iv & 0x3E0) >> 5;
            uint test_def = (iv & 0x7C00) >> 10;

            if (test_hp == hp && test_atk == atk && test_def == def)
            {

                if (nature == 100)
                {
                    ret = true;
                }
                else
                {
                    uint pid = (pid2 << 16) | pid1;
                    uint actualNature = pid % 25;
                    if (nature == actualNature)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        private void filterSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint rng1XD, uint rng3XD, uint rng4XD, uint seed)
        {
            uint pid = (rng3XD << 16) | rng4XD;
            if (nature == 100)
                nature = pid % 25;

            String shiny = "";
            if (Shiny_Check.Checked == true)
            {
                if (!isShiny(pid))
                {
                    return;
                }
                shiny = "!!!";
            }

            if (hP != 0)
            {
                uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
                if (actualHP != (hP - 1))
                {
                    return;
                }
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                {
                    return;
                }
            }
            ability = pid & 1;

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }

            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hP, pid, shiny, seed);
        }
        #endregion

        #region Second search method
        //Credits to Zari for this
        private void generate2(uint[] ivsLower, uint[] ivsUpper, uint nature)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            if (nature == 0)
                nature = 100;
            else
                nature = natures[nature];

            uint ability = getAbility();
            uint gender = getGender();
            uint hiddenPower = getHP();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVs(ivsLower, ivsUpper, n);
                        if (ivs.Length != 1)
                        {
                            uint pid = pidChk(n, 0);
                            uint actualNature = pid % 25;
                            if (nature == 100 || nature == actualNature)
                                filterSeed2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, slist[(int)n], pid);

                            pid = pidChk(n, 1);
                            actualNature = pid % 25;
                            if (nature == 100 || nature == actualNature)
                                filterSeed2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, (slist[(int)n] ^ 0x80000000), pid);
                        }
                    }
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void filterSeed2(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hiddenPowerValue, uint seed, uint pid)
        {
            String shiny = "";
            if (Shiny_Check.Checked == true)
            {
                if (!isShiny(pid))
                {
                    return;
                }
                shiny = "!!!";
            }

            if (hiddenPowerValue != 0)
            {
                uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
                if (actualHP != (hiddenPowerValue - 1))
                {
                    return;
                }
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                {
                    return;
                }
            }
            ability = pid & 1;

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hiddenPowerValue, pid, shiny, seed);
        }

        private uint populateRNG(uint seed)
        {
            seed = forward(seed);
            slist.Add(seed);
            rlist.Add((seed >> 16));
            return seed;
        }

        private void populate(uint seed, uint srange)
        {
            uint s = seed;
            for (uint x = 0; x < (srange + 12); x++)
            {
                s = populateRNG(s);
            }
        }

        private uint[] calcIVs(uint[] ivsLower, uint[] ivsUpper, uint frame)
        {
            uint[] ivs;
            uint iv1 = rlist[(int)(frame + 1)];
            uint iv2 = rlist[(int)(frame + 2)];
            ivs = createIVs(iv1, iv2, ivsLower, ivsUpper);
            return ivs;
        }

        private uint[] createIVs(uint iv1, uint ivs2, uint[] ivsLower, uint[] ivsUpper)
        {
            uint[] ivs = new uint[6];

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                if (iv >= ivsLower[x] && iv <= ivsUpper[x])
                    ivs[x] = iv;
                else
                {
                    ivs = new uint[1];
                    return ivs;
                }
            }

            uint iV = (ivs2 >> 5) & 31;
            if (iV >= ivsLower[3] && iV <= ivsUpper[3])
                ivs[3] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            iV = (ivs2 >> 10) & 31;
            if (iV >= ivsLower[4] && iV <= ivsUpper[4])
                ivs[4] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            iV = ivs2 & 31;
            if (iV >= ivsLower[5] && iV <= ivsUpper[5])
                ivs[5] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            return ivs;
        }

        private uint pidChk(uint frame, uint xor_val)
        {
            uint pid = (rlist[(int)(frame + 4)] << 16) + rlist[(int)(frame + 5)];
            if (xor_val == 1)
                pid = pid ^ 0x80008000;

            return pid;
        }
        #endregion
        #endregion

        #region Channel

        //Credits to Zari and amab for this
        public void generateChannel(uint[] ivsLower, uint[] ivsUpper, uint nature)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            if (nature == 0)
                nature = 100;
            else
                nature = natures[nature];

            uint ability = getAbility();
            uint gender = getGender();
            uint hiddenPower = getHP();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVsChannel(ivsLower, ivsUpper, n, 0);
                        if (ivs.Length != 1)
                        {
                            uint pid = pidChkChannel(n, 0);
                            uint actualNature = pid % 25;
                            if (nature == 100 || nature == actualNature)
                                filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, slist[(int)n], pid);

                            ivs = calcIVsChannel(ivsLower, ivsUpper, n, 1);
                            if (ivs.Length != 1)
                            {
                                pid = pidChkChannel(n, 1);
                                actualNature = pid % 25;
                                if (nature == 100 || nature == actualNature)
                                    filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, (slist[(int)n] ^ 0x80000000), pid);
                            }
                        }
                    }
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private uint[] calcIVsChannel(uint[] ivsLower, uint[] ivsUpper, uint frame, uint xorvalue)
        {
            uint[] ivs;
            if (xorvalue == 0)
            {
                uint[] iv = { rlist[(int)(frame + 7)], rlist[(int)(frame + 8)], rlist[(int)(frame + 9)], rlist[(int)(frame + 11)], rlist[(int)(frame + 12)], rlist[(int)(frame + 10)] };
                ivs = createIVsChannel(iv, ivsLower, ivsUpper);
            }
            else
            {
                uint[] iv = { rlist[(int)(frame + 7)] ^ 0x8000, rlist[(int)(frame + 8)] ^ 0x8000, rlist[(int)(frame + 9)] ^ 0x8000, rlist[(int)(frame + 11)] ^ 0x8000, rlist[(int)(frame + 12)] ^ 0x8000, rlist[(int)(frame + 10)] ^ 0x8000 };
                ivs = createIVsChannel(iv, ivsLower, ivsUpper);
            }

            return ivs;
        }

        private uint[] createIVsChannel(uint[] iv, uint[] ivsLower, uint[] ivsUpper)
        {
            uint[] ivs = new uint[6];

            for (int x = 0; x < 6; x++)
            {
                uint iV = iv[x] >> 11;
                if (iV >= ivsLower[x] && iV <= ivsUpper[x])
                    ivs[x] = iV;
                else
                {
                    ivs = new uint[1];
                    return ivs;
                }
            }

            return ivs;
        }

        private uint pidChkChannel(uint frame, uint xor_val)
        {
            uint pid1 = slist[(int)(frame + 2)] + 0x80000000;
            if (pid1 > 0xFFFFFFFF)
                pid1 = pid1 & 0xFFFFFFFF;
            uint pid = ((pid1 >> 16) << 16) + rlist[(int)(frame + 3)];
            if (xor_val == 1)
                pid = pid ^ 0x80008000;

            return pid;
        }

        private void filterSeedChannel(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hiddenPowerValue, uint seed, uint pid)
        {
            String shiny = "";

            if (nature == 100)
                nature = pid % 25;

            if (hiddenPowerValue != 0)
            {
                uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
                if (actualHP != (hiddenPowerValue - 1))
                {
                    return;
                }
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                {
                    return;
                }
            }
            ability = pid & 1;

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hiddenPowerValue, pid, shiny, seed);
        }

        #endregion

        #region Reverse Method 1
        private void getRMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            uint method = 1;

            if (wshMkr.Checked == true)
                shinyval = (20043 ^ 0) >> 3;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 16384)
                generateR2(ivsLower, ivsUpper, getNature());
            else
                generateR(ivsLower, ivsUpper);
        }

        #region Search 1
        private void generateR(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint nature = getNature();
            if (nature == 0)
                nature = 100;
            else
                nature = natures[nature];
            uint ability = getAbility();
            uint gender = getGender();
            uint hp = getHP();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
            {
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                {
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                    {
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                {
                                    checkSeedR(a, b, c, d, e, f, nature, ability, gender, hp);
                                }
                            }
                        }
                    }
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        //Credits to RNG reporter for this
        private void checkSeedR(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP)
        {
            uint x4 = 0;
            uint x4_2 = 0;

            x4 = spe + (spa << 5) + (spd << 10);
            x4_2 = x4 ^ 0x8000;

            for (uint cnt = 0; cnt <= 0x1FFFE; cnt++)
            {
                uint x_test = (cnt & 1) == 0 ? x4 : x4_2;
                uint seed = (x_test << 16) + (cnt % 0xFFFF);

                uint rng1 = reverseR(seed);
                uint rng2 = reverseR(rng1);
                uint rng3 = reverseR(rng2);
                uint rng4 = reverseR(rng3);
                uint Method1Seed = rng4;
                rng1 >>= 16;
                rng2 >>= 16;
                rng3 >>= 16;
                rng4 >>= 16;

                if (Check(rng1, rng3, rng2, hp, atk, def, nature))
                {
                    if (wshMkr.Checked == true)
                        filterSeedWsh(hp, atk, def, spa, spd, spe, nature, ability, gender, hP, rng1, rng3, rng2, Method1Seed);
                    else
                        filterSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hP, rng1, rng3, rng2, Method1Seed);
                }
            }
        }

        private void filterSeedWsh(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint rng1XD, uint rng3XD, uint rng4XD, uint seed)
        {
            if (seed > 0xFFFF)
                return;

            uint pid = (rng3XD << 16) | rng4XD;
            if (nature == 100)
                nature = pid % 25;

            String shiny = "";
            if (Shiny_Check.Checked == true)
            {
                if (!isShiny(pid))
                {
                    return;
                }
                shiny = "!!!";
            }

            if (hP != 0)
            {
                uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
                if (actualHP != (hP - 1))
                {
                    return;
                }
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                {
                    return;
                }
            }
            ability = pid & 1;

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }

            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hP, pid, shiny, seed);
        }
        #endregion

        #region Search 2
        private void generateR2(uint[] ivsLower, uint[] ivsUpper, uint nature)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            if (nature == 0)
                nature = 100;
            else
                nature = natures[nature];

            uint ability = getAbility();
            uint gender = getGender();
            uint hiddenPower = getHP();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populateR(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVsR(ivsLower, ivsUpper, n);
                        if (ivs.Length != 1)
                        {
                            uint pid = pidChkR(n, 0);
                            uint actualNature = pid % 25;
                            if (nature == 100 || nature == actualNature)
                                if (wshMkr.Checked == true)
                                    filterSeed2Wsh(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, slist[(int)(n)], pid);
                                else
                                    filterSeed2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, slist[(int)(n)], pid);

                            pid = pidChkR(n, 1);
                            actualNature = pid % 25;
                            if (nature == 100 || nature == actualNature)
                                if (wshMkr.Checked == true)
                                    filterSeed2Wsh(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, (slist[(int)(n)] ^ 0x80000000), pid);
                                else
                                    filterSeed2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], actualNature, ability, gender, hiddenPower, (slist[(int)(n)] ^ 0x80000000), pid);
                        }
                    }
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private uint populateRNGR(uint seed)
        {
            seed = forwardR(seed);
            slist.Add(seed);
            rlist.Add((seed >> 16));
            return seed;
        }

        private void populateR(uint seed, uint srange)
        {
            uint s = seed;
            for (uint x = 0; x < (srange + 10); x++)
            {
                s = populateRNGR(s);
            }
        }

        private void filterSeed2Wsh(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hiddenPowerValue, uint seed, uint pid)
        {
            if (seed > 0xFFFF)
                return;

            String shiny = "";
            if (Shiny_Check.Checked == true)
            {
                if (!isShiny(pid))
                {
                    return;
                }
                shiny = "!!!";
            }

            if (hiddenPowerValue != 0)
            {
                uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
                if (actualHP != (hiddenPowerValue - 1))
                {
                    return;
                }
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                {
                    return;
                }
            }
            ability = pid & 1;

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, hiddenPowerValue, pid, shiny, seed);
        }

        private uint[] calcIVsR(uint[] ivsLower, uint[] ivsUpper, uint frame)
        {
            uint[] ivs;
            uint iv1 = rlist[(int)(frame + 3)];
            uint iv2 = rlist[(int)(frame + 4)];
            ivs = createIVsR(iv1, iv2, ivsLower, ivsUpper);
            return ivs;
        }

        private uint[] createIVsR(uint iv1, uint ivs2, uint[] ivsLower, uint[] ivsUpper)
        {
            uint[] ivs = new uint[6];

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                if (iv >= ivsLower[x] && iv <= ivsUpper[x])
                    ivs[x] = iv;
                else
                {
                    ivs = new uint[1];
                    return ivs;
                }
            }

            uint iV = (ivs2 >> 5) & 31;
            if (iV >= ivsLower[3] && iV <= ivsUpper[3])
                ivs[3] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            iV = (ivs2 >> 10) & 31;
            if (iV >= ivsLower[4] && iV <= ivsUpper[4])
                ivs[4] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            iV = ivs2 & 31;
            if (iV >= ivsLower[5] && iV <= ivsUpper[5])
                ivs[5] = iV;
            else
            {
                ivs = new uint[1];
                return ivs;
            }

            return ivs;
        }

        private uint pidChkR(uint frame, uint xor_val)
        {
            uint pid = (rlist[(int)(frame + 1)] << 16) + rlist[(int)(frame + 2)];
            if (xor_val == 1)
                pid = pid ^ 0x80008000;

            return pid;
        }
        #endregion
        #endregion

        #region Helper methods
        private uint getNature()
        {
            if (natureType.InvokeRequired)
                return (uint)natureType.Invoke(new Func<uint>(getNature));
            else
                return (uint)natureType.SelectedIndex;
        }

        private uint getAbility()
        {
            if (abilityType.InvokeRequired)
                return (uint)abilityType.Invoke(new Func<uint>(getAbility));
            else
                return (uint)abilityType.SelectedIndex;
        }

        private uint getGender()
        {
            if (genderType.InvokeRequired)
                return (uint)genderType.Invoke(new Func<uint>(getGender));
            else
                return (uint)genderType.SelectedIndex;
        }

        private uint getHP()
        {
            if (hiddenpower.InvokeRequired)
                return (uint)hiddenpower.Invoke(new Func<uint>(getHP));
            else
                return (uint)hiddenpower.SelectedIndex;
        }

        private uint getSearchMethod()
        {
            if (searchMethod.InvokeRequired)
                return (uint)searchMethod.Invoke(new Func<uint>(getSearchMethod));
            else
                return (uint)searchMethod.SelectedIndex;
        }

        private uint forward(uint seed)
        {
            return ((seed * 0x343FD + 0x269EC3) & 0xFFFFFFFF);
        }

        private uint reverse(uint seed)
        {
            return ((seed * 0xB9B33155 + 0xA170F641) & 0xFFFFFFFF);
        }

        private uint forwardR(uint seed)
        {
            return ((seed * 0x41c64e6d + 0x6073) & 0xFFFFFFFF);
        }

        private uint reverseR(uint seed)
        {
            return ((seed * 0xeeb9eb65 + 0xa3561a1) & 0xFFFFFFFF);
        }

        private int calcHPPower(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return (int)(30 + ((((hp >> 1) & 1) + 2 * ((atk >> 1) & 1) + 4 * ((def >> 1) & 1) + 8 * ((spe >> 1) & 1) + 16 * ((spa >> 1) & 1) + 32 * ((spd >> 1) & 1)) * 40 / 63));
        }

        private bool isShiny(uint PID)
        {
            return (((PID >> 16) ^ (PID & 0xffff)) >> 3) == shinyval;
        }

        private uint calcHP(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return ((((hp & 1) + 2 * (atk & 1) + 4 * (def & 1) + 8 * (spe & 1) + 16 * (spa & 1) + 32 * (spd & 1)) * 15) / 63);
        }
        #endregion

        private void addSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint pid, String shiny, uint seed)
        {
            String stringNature = Natures[nature];
            String hPString = hiddenPowers[calcHP(hp, atk, def, spa, spd, spe)];
            int hpPower = calcHPPower(hp, atk, def, spa, spd, spe);
            gender = pid & 255;
            char gender1;
            char gender2;
            char gender3;
            char gender4;

            if (shiny == "")
            {
                if (isShiny(pid))
                {
                    shiny = "!!!";
                }
            }

            if (gender < 31)
                gender1 = 'F';
            else
                gender1 = 'M';

            if (gender < 64)
                gender2 = 'F';
            else
                gender2 = 'M';

            if (gender < 126)
                gender3 = 'F';
            else
                gender3 = 'M';

            if (gender < 191)
                gender4 = 'F';
            else
                gender4 = 'M';

            displayList.Add(new DisplayList
            {
                Seed = seed.ToString("x").ToUpper(),
                PID = pid.ToString("x").ToUpper(),
                Shiny = shiny,
                Nature = stringNature,
                Ability = (int)ability,
                Hp = (int)hp,
                Atk = (int)atk,
                Def = (int)def,
                SpA = (int)spa,
                SpD = (int)spd,
                Spe = (int)spe,
                Hidden = hPString,
                Power = hpPower,
                Eighth = gender1,
                Quarter = gender2,
                Half = gender3,
                Three_Fourths = gender4
            });
        }

        #region GUI code
        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = k_dataGridView.AutoResizeColumns;
            try
            {
                bool alive = true;
                while (alive)
                {
                    if (refresh)
                    {
                        Invoke(gridUpdate);
                        refresh = false;
                    }
                    if (searchThread == null || !searchThread.IsAlive)
                    {
                        alive = false;
                    }

                    Thread.Sleep(500);
                }
            }
            finally
            {
                Invoke(gridUpdate);
                Invoke(resizeGrid);
            }
        }


        #region Nested type: ThreadDelegate

        private delegate void ThreadDelegate();

        #endregion

        private void dataGridUpdate()
        {
            binding.ResetBindings(false);
        }
        #endregion

        #region Quick search settings
        private void button1_Click(object sender, EventArgs e)
        {
            HPLow.Value = 31;
            HPHigh.Value = 31;
            AtkLow.Value = 31;
            AtkHigh.Value = 31;
            DefLow.Value = 31;
            DefHigh.Value = 31;
            SpALow.Value = 0;
            SpAHigh.Value = 31;
            SpDLow.Value = 31;
            SpDHigh.Value = 31;
            SpeLow.Value = 31;
            SpeHigh.Value = 31;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HPLow.Value = 31;
            HPHigh.Value = 31;
            AtkLow.Value = 0;
            AtkHigh.Value = 31;
            DefLow.Value = 31;
            DefHigh.Value = 31;
            SpALow.Value = 31;
            SpAHigh.Value = 31;
            SpDLow.Value = 31;
            SpDHigh.Value = 31;
            SpeLow.Value = 31;
            SpeHigh.Value = 31;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HPLow.Value = 31;
            HPHigh.Value = 31;
            AtkLow.Value = 31;
            AtkHigh.Value = 31;
            DefLow.Value = 31;
            DefHigh.Value = 31;
            SpALow.Value = 31;
            SpAHigh.Value = 31;
            SpDLow.Value = 31;
            SpDHigh.Value = 31;
            SpeLow.Value = 31;
            SpeHigh.Value = 31;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HPLow.Value = 0;
            HPHigh.Value = 31;
            AtkLow.Value = 0;
            AtkHigh.Value = 31;
            DefLow.Value = 0;
            DefHigh.Value = 31;
            SpALow.Value = 0;
            SpAHigh.Value = 31;
            SpDLow.Value = 0;
            SpDHigh.Value = 31;
            SpeLow.Value = 0;
            SpeHigh.Value = 31;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                status.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void anyNature_Click(object sender, EventArgs e)
        {
            natureType.SelectedIndex = 0;
        }

        private void anyGender_Click(object sender, EventArgs e)
        {
            genderType.SelectedIndex = 0;
        }

        private void anyAbility_Click(object sender, EventArgs e)
        {
            abilityType.SelectedIndex = 0;
        }

        private void anyHiddenPower_Click(object sender, EventArgs e)
        {
            hiddenpower.SelectedIndex = 0;
        }
        #endregion
    }
}