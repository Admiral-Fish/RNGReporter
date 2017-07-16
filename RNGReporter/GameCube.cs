using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using static RNGReporter.Objects.NatureLock;

namespace RNGReporter
{
    public partial class GameCube : Form
    {
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private readonly String[] hiddenPowers = { "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark" };
        private Thread[] searchThread;
        private BindingSource binding, bindingShadow;
        private List<DisplayList> displayList;
        private List<ShadowDisplay> shadowDisplay;
        private bool isSearching, galesFlag;
        private uint searchNumber, genderFilter, abilityFilter;
        private ShadowType shadow;
        private NatureLock natureLock = new NatureLock(0);
        private List<uint> natureList, seedList, hiddenPowerList;
        private uint[] ivsLower, ivsUpper, shinyval;
        private int natureLockIndex, cores;

        public GameCube(int TID, int SID)
        {
            InitializeComponent();
            id.Text = TID.ToString();
            sid.Text = SID.ToString();
            Reason.Visible = false;
            displayList = new List<DisplayList>();
            shadowDisplay = new List<ShadowDisplay>();
            binding = new BindingSource { DataSource = displayList };
            bindingShadow = new BindingSource { DataSource = shadowDisplay };
            seedList = new List<uint>();
            abilityType.SelectedIndex = 0;
            genderType.SelectedIndex = 0;
            searchMethod.SelectedIndex = 0;
            shadowPokemon.SelectedIndex = 0;
            comboBoxShadow.SelectedIndex = 0;
            comboBoxMethodShadow.SelectedIndex = 0;
            comboBoxAbilityShadow.SelectedIndex = 0;
            comboBoxGenderShadow.SelectedIndex = 0;
            hpLogic.SelectedIndex = 0;
            atkLogic.SelectedIndex = 0;
            defLogic.SelectedIndex = 0;
            spaLogic.SelectedIndex = 0;
            spdLogic.SelectedIndex = 0;
            speLogic.SelectedIndex = 0;
            hpLogicShadow.SelectedIndex = 0;
            atkLogicShadow.SelectedIndex = 0;
            defLogicShadow.SelectedIndex = 0;
            spaLogicShadow.SelectedIndex = 0;
            spdLogicShadow.SelectedIndex = 0;
            speLogicShadow.SelectedIndex = 0;
            dataGridViewResult.DataSource = binding;
            dataGridViewResult.AutoGenerateColumns = false;
            dataGridShadow.DataSource = bindingShadow;
            dataGridShadow.AutoGenerateColumns = false;
            shinyval = new uint[8];
            cores = Environment.ProcessorCount;
            while (cores != 1 && cores != 2 && cores != 4 && cores != 8)
                cores--;
        }

        private void GameCube_Load(object sender, EventArgs e)
        {
            comboBoxNature.Items.AddRange(Nature.NatureDropDownCollectionSearchNatures());
            comboBoxHiddenPower.Items.AddRange(addHP());
            checkBoxNatureShadow.Items.AddRange(Nature.NatureDropDownCollectionSearchNatures());
            checkBoxHPShadow.Items.AddRange(addHP());
            setComboBox();
            wshMkr.Visible = true;
            shadowPokemon.Visible = false;
            galesCheck.Visible = false;
        }

        private void GameCube_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (searchThread != null)
            {
                for (int x = 0; x < searchThread.Length; x++)
                    searchThread[x].Abort();
                status.Text = "Cancelled. - Awaiting Command";
            }
            Hide();
        }

        #region Spread Searcher
        #region Start search
        private void search_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                status.Text = "Previous search is still running";
                return;
            }

            getIVs(out ivsLower, out ivsUpper);
            galesFlag = false;

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
                natureList = null;
                if (comboBoxNature.Text != "Any" && comboBoxNature.CheckBoxItems.Count > 0)
                    natureList = (from t in comboBoxNature.CheckBoxItems where t.Checked select (uint)((Nature)t.ComboBoxItem).Number).ToList();

                hiddenPowerList = null;
                List<uint> temp = new List<uint>();
                if (comboBoxHiddenPower.Text != "Any" && comboBoxHiddenPower.CheckBoxItems.Count > 0)
                    for (int x = 1; x <= 16; x++)
                        if (comboBoxHiddenPower.CheckBoxItems[x].Checked)
                            temp.Add((uint)(x - 1));

                if (temp.Count != 0)
                    hiddenPowerList = temp;

                abilityFilter = (uint)abilityType.SelectedIndex;
                genderFilter = (uint)genderType.SelectedIndex;

                displayList.Clear();
                seedList.Clear();
                binding.ResetBindings(false);
                status.Text = "Searching";
                isSearching = true;
                try
                {
                    shinyval[0] = (uint.Parse(id.Text) ^ uint.Parse(sid.Text)) >> 3;
                }
                catch
                {
                    shinyval[0] = 0;
                }
                searchNumber = (uint)searchMethod.SelectedIndex;

                dataGridViewResult.Columns[17].Visible = galesCheck.Checked && searchMethod.SelectedIndex == 1;

                getSearch();
            }
        }

        private void getSearch()
        {
            if (searchNumber == 0)
            {
                if (wshMkr.Checked)
                {
                    searchThread = new Thread[1];
                    searchThread[0] = new Thread(() => generateWishmkr());
                    searchThread[0].Start();
                }
                else
                    getRMethod();
            }
            else if (searchNumber == 1)
            {
                if (galesCheck.Checked)
                    getGalesMethod();
                else
                    getColoMethod();
            }
            else
                getChannelMethod();
        }
        #endregion

        #region Gales Search
        private void getGalesMethod()
        {
            natureLockIndex = shadowPokemon.SelectedIndex;
            galesFlag = natureLockIndex != 41;
            natureLock.changeLock(natureLockIndex);
            shadow = natureLock.getType();

            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            switch (cores)
            {
                case 1:
                    if (method > 162268)
                    {
                        searchThread = new Thread[1];
                        
                        searchThread[0] = new Thread(() => generateGales2(0, 64));
                        searchThread[0].Start();
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateGales());
                        searchThread[0].Start();
                    }
                    break;
                case 2:
                    if (method > 83720)
                    {
                        searchThread = new Thread[2];
                        for (int i = 0; i < 2; i++)
                        {
                            searchThread[i] = new Thread(() => generateGales2((uint)(i * 0x40000000), 32));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateGales());
                        searchThread[0].Start();
                    }
                    break;
                case 4:
                    if (method > 45918)
                    {
                        searchThread = new Thread[4];
                        for (int i = 0; i < 4; i++)
                        {
                            searchThread[i] = new Thread(() => generateGales2((uint)(i * 0x20000000), 16));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateGales());
                        searchThread[0].Start();
                    }
                    break;
                case 8:
                    if (method > 34970)
                    {
                        searchThread = new Thread[8];
                        for (int i = 0; i < 8; i++)
                        {
                            searchThread[i] = new Thread(() => generateGales2((uint)(i * 0x10000000), 8));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateGales());
                        searchThread[0].Start();
                    }
                    break;
            }

        }

        #region First search method
        private void generateGales()
        {
            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedGales(a, b, c, d, e, f);
                        }
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeedGales(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            uint x8 = hp | (atk << 5) | (def << 10);
            uint ex8 = spe | (spa << 5) | (spd << 10);
            uint ivs_1b = x8 << 16;
            uint ivs_2, seedb, pid1, pid2, pid, nature, galesSeed, xorSeed, xorPID, xorNature, cnt;
            bool pass, xorPass;

            switch (shadow)
            {
                case ShadowType.NoLock:
                    for (cnt = 0; cnt <= 0xFFFF; cnt++)
                    {
                        seedb = ivs_1b | cnt;
                        ivs_2 = forwardXD(seedb);
                        if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                        {
                            pid1 = forwardXD(forwardXD(ivs_2));
                            pid2 = forwardXD(pid1);
                            pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                            nature = pid % 25;
                            galesSeed = reverseXD(seedb);
                            pass = (natureList == null || natureList.Contains(nature));

                            xorSeed = galesSeed ^ 0x80000000;
                            xorPID = pid ^ 0x80008000;
                            xorNature = xorPID % 25;
                            xorPass = (natureList == null || natureList.Contains(xorNature));

                            if (pass)
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 0);
                            if (xorPass)
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 0);
                        }
                    }
                    break;
                case ShadowType.FirstShadow:
                    for (cnt = 0; cnt <= 0xFFFF; cnt++)
                    {
                        seedb = ivs_1b | cnt;
                        ivs_2 = forwardXD(seedb);
                        if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                        {
                            pid1 = forwardXD(forwardXD(ivs_2));
                            pid2 = forwardXD(pid1);
                            pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                            nature = pid % 25;
                            galesSeed = reverseXD(seedb);
                            pass = (natureList == null || natureList.Contains(nature));

                            xorSeed = galesSeed ^ 0x80000000;
                            xorPID = pid ^ 0x80008000;
                            xorNature = xorPID % 25;
                            xorPass = (natureList == null || natureList.Contains(xorNature));

                            if (pass && natureLock.method1FirstShadow(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 0);
                            else if (xorPass && natureLock.method1FirstShadow(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 0);
                        }
                    }
                    break;
                case ShadowType.SingleLock:
                    for (cnt = 0; cnt <= 0xFFFF; cnt++)
                    {
                        seedb = ivs_1b | cnt;
                        ivs_2 = forwardXD(seedb);
                        if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                        {
                            pid1 = forwardXD(forwardXD(ivs_2));
                            pid2 = forwardXD(pid1);
                            pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                            nature = pid % 25;
                            galesSeed = reverseXD(seedb);
                            pass = (natureList == null || natureList.Contains(nature));

                            xorSeed = galesSeed ^ 0x80000000;
                            xorPID = pid ^ 0x80008000;
                            xorNature = xorPID % 25;
                            xorPass = (natureList == null || natureList.Contains(xorNature));

                            if (pass && natureLock.method1SingleNL(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 0);
                            else if (xorPass && natureLock.method1SingleNL(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 0);
                        }
                    }
                    break;
                case ShadowType.Salamence:
                    for (cnt = 0; cnt <= 0xFFFF; cnt++)
                    {
                        seedb = ivs_1b | cnt;
                        ivs_2 = forwardXD(seedb);
                        if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                        {
                            pid1 = forwardXD(forwardXD(ivs_2));
                            pid2 = forwardXD(pid1);
                            pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                            nature = pid % 25;
                            galesSeed = reverseXD(seedb);
                            pass = (natureList == null || natureList.Contains(nature));

                            xorSeed = galesSeed ^ 0x80000000;
                            xorPID = pid ^ 0x80008000;
                            xorNature = xorPID % 25;
                            xorPass = (natureList == null || natureList.Contains(xorNature));

                            if (pass && natureLock.salamenceSet(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 1);
                            else if (xorPass && natureLock.salamenceSet(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 1);
                            else if (pass && natureLock.salamenceUnset(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 2);
                            else if (xorPass && natureLock.salamenceUnset(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 2);
                            else if (pass && natureLock.salamenceShinySkip(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 3);
                            else if (xorPass && natureLock.salamenceShinySkip(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 3);
                        }
                    }
                    break;
                case ShadowType.SecondShadow:
                    for (cnt = 0; cnt <= 0xFFFF; cnt++)
                    {
                        seedb = ivs_1b | cnt;
                        ivs_2 = forwardXD(seedb);
                        if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                        {
                            pid1 = forwardXD(forwardXD(ivs_2));
                            pid2 = forwardXD(pid1);
                            pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                            nature = pid % 25;
                            galesSeed = reverseXD(seedb);
                            pass = (natureList == null || natureList.Contains(nature));

                            xorSeed = galesSeed ^ 0x80000000;
                            xorPID = pid ^ 0x80008000;
                            xorNature = xorPID % 25;
                            xorPass = (natureList == null || natureList.Contains(xorNature));

                            if (pass && natureLock.method1SecondShadowSet(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 1);
                            else if (xorPass && natureLock.method1SecondShadowSet(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 1);
                            else if (pass && natureLock.method1SecondShadowUnset(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 2);
                            else if (xorPass && natureLock.method1SecondShadowUnset(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 2);
                            else if (pass && natureLock.method1SecondShadowShinySkip(galesSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, pid, nature, galesSeed, 3);
                            else if (xorPass && natureLock.method1SecondShadowShinySkip(xorSeed))
                                filterSeedGales(hp, atk, def, spa, spd, spe, xorPID, xorNature, xorSeed, 3);
                        }
                    }
                    break;
            }
        }

        private void filterSeedGales(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint pid, uint nature, uint seed, int num)
        {
            String shiny = "";
            if (!galesFlag && Shiny_Check.Checked)
            {
                if (!isShiny(pid, 0))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null && !hiddenPowerList.Contains(actualHP))
                    return;

            uint ability = pid & 1;
            if (abilityFilter != 0 && (ability != (abilityFilter - 1)))
                    return;

            uint gender = pid & 255;
            switch (genderFilter)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }

            String reason = "";
            if (num == 0)
                reason = "Pass NL";
            else if (num == 1)
                reason = "1st shadow set";
            else if (num == 2)
                reason = "1st shadow unset";
            else
            {
                reason = "Shiny skip";
                var reverse = new XdRngR(seed);
                reverse.GetNext32BitNumber();
                uint tsv = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                uint tsvtemp = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                while (tsv == tsvtemp)
                {
                    tsvtemp = tsv;
                    tsv = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                }
                reason = reason + " (TSV: " + tsv + ")";
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, reason, 0);
        }
        #endregion

        #region Second search method
        private void generateGales2(uint inseed, int num1)
        {
            var rng = new XdRng(inseed);
            uint pid, iv1, iv2, nature, seed;
            uint[] ivs;
            int count;
            var info = new NatureLock(natureLockIndex);

            switch (shadow)
            {
                case ShadowType.NoLock:
                    uint[] rand = new uint[6];
                    rand[0] = inseed;

                    for (int x = 1; x < 6; x++)
                        rand[x] = rng.GetNext32BitNumber();

                    int j = 5;

                    for (uint z = 0; z < 32; z++)
                    {
                        for (uint h = 0; h < num1; h++)
                        {
                            for (uint n = 0; n < 1048576; n++, rand[j] = rng.GetNext32BitNumber())
                            {
                                if (++j > 5)
                                    j = 0;
                                ivs = createIVs(rand[j >= 5 ? j - 5 : j + 1] >> 16, rand[j >= 4 ? j - 4 : j + 2] >> 16);
                                if (ivs != null)
                                {
                                    pid = (rand[j >= 2 ? j - 2 : j + 4] & 0xFFFF0000) | (rand[j >= 1 ? j - 1 : j + 5] >> 16);
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                        filterSeedGales(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, rand[j], 0);

                                    pid ^= 80008000;
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                        filterSeedGales(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, rand[j] ^ 0x80000000, 0);
                                }
                            }
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                        }
                    }
                    break;
                case ShadowType.SingleLock:
                case ShadowType.FirstShadow:
                    info.rand.Add(inseed);
                    for (uint x = 0; x < 2999; x++)
                        info.rand.Add(rng.GetNext32BitNumber());

                    for (uint z = 0; z < 32; z++)
                    {
                        for (uint h = 0; h < num1; h++)
                        {
                            for (uint n = 0; n < 1048576; n++, info.rand.RemoveAt(0), info.rand.Add(rng.GetNext32BitNumber()))
                            {
                                info.method2FirstShadow(false, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 0);
                                    }
                                }

                                info.method2FirstShadow(true, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 0);
                                    }
                                }
                            }
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                        }
                    }
                    break;
                case ShadowType.Salamence:
                case ShadowType.SecondShadow:
                    info.rand.Add(inseed);
                    for (uint x = 0; x < 2999; x++)
                        info.rand.Add(rng.GetNext32BitNumber());

                    for (uint z = 0; z < 32; z++)
                    {
                        for (uint h = 0; h < num1; h++)
                        {
                            for (uint n = 0; n < 1048576; n++, info.rand.RemoveAt(0), info.rand.Add(rng.GetNext32BitNumber()))
                            {
                                count = info.method2SecondShadowPassNL(false);

                                info.method2SecondShadowSet(false, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 1);
                                    }
                                }

                                info.method2SecondShadowUnset(false, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 2);
                                    }
                                }

                                info.method2SecondShinySkip(false, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 3);
                                    }
                                }

                                count = info.method2SecondShadowPassNL(true);

                                info.method2SecondShadowSet(true, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 1);
                                    }
                                }

                                info.method2SecondShadowUnset(true, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 2);
                                    }
                                }

                                info.method2SecondShinySkip(true, count, out seed, out pid, out iv1, out iv2);
                                if (!seedList.Contains(seed))
                                {
                                    nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        ivs = createIVs(iv1, iv2);
                                        if (ivs != null)
                                            filterSeedGales2(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seed, 3);
                                    }
                                }
                            }
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                        }
                    }
                    break;
            }
            info.rand.Clear();
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void filterSeedGales2(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint pid, uint nature, uint seed, int num)
        {
            String shiny = "";
            if (!galesFlag && Shiny_Check.Checked)
            {
                if (!isShiny(pid, 0))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null && !hiddenPowerList.Contains(actualHP))
                return;

            uint ability = pid & 1;
            if (abilityFilter != 0 && (ability != (abilityFilter - 1)))
                return;

            uint gender = pid & 255;
            switch (genderFilter)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }

            String reason = "";
            if (num == 0)
                reason = "Pass NL";
            else if (num == 1)
                reason = "1st shadow set";
            else if (num == 2)
                reason = "1st shadow unset";
            else
            {
                reason = "Shiny skip";
                var reverse = new XdRngR(seed);
                reverse.GetNext32BitNumber();
                uint tsv = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                uint tsvtemp = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                while (tsv == tsvtemp)
                {
                    tsvtemp = tsv;
                    tsv = (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
                }
                reason = reason + " (TSV: " + tsv + ")";
            }
            seedList.Add(seed);
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, reason, 0);
        }
        #endregion
        #endregion

        #region Colo search
        private void getColoMethod()
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            switch (cores)
            {
                case 1:
                    if (method > 162268)
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateColo2(0, 64));
                        searchThread[0].Start();
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateColo());
                        searchThread[0].Start();
                    }
                    break;
                case 2:
                    if (method > 83720)
                    {
                        searchThread = new Thread[2];
                        for (int i = 0; i < 2; i++)
                        {
                            searchThread[i] = new Thread(() => generateColo2((uint)(i * 0x40000000), 32));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateColo());
                        searchThread[0].Start();
                    }
                    break;
                case 4:
                    if (method > 45918)
                    {
                        searchThread = new Thread[4];
                        for (int i = 0; i < 4; i++)
                        {
                            searchThread[i] = new Thread(() => generateColo2((uint)(i * 0x20000000), 16));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateColo());
                        searchThread[0].Start();
                    }
                    break;
                case 8:
                    if (method > 34970)
                    {
                        searchThread = new Thread[8];
                        for (int i = 0; i < 8; i++)
                        {
                            searchThread[i] = new Thread(() => generateColo2((uint)(i * 0x10000000), 8));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateColo());
                        searchThread[0].Start();
                    }
                    break;
            }
        }

        #region First search method
        private void generateColo()
        {
            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeed(a, b, c, d, e, f);
                        }
            
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            uint x8 = hp | (atk << 5) | (def << 10);
            uint ex8 = spe | (spa << 5) | (spd << 10);
            uint ivs_1b = x8 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b | cnt;
                uint ivs_2 = forwardXD(seedb);
                if (((ivs_2 >> 16) & 0x7FFF) == ex8)
                {
                    uint pid1 = forwardXD(forwardXD(ivs_2));
                    uint pid2 = forwardXD(pid1);
                    uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                    uint nature = pid % 25;
                    uint seed = reverseXD(seedb);
                    if (natureList == null || natureList.Contains(nature))
                        filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                    pid ^= 0x80008000;
                    nature = pid % 25;
                    if (natureList == null || natureList.Contains(nature))
                        filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                }
                
            }

            /*uint x_test = spe | (spa << 5) | (spd << 10);
            uint y_test = hp | (atk << 5) | (def << 10);
            uint y_compare = (y_test - 0x31) & 0xFFFF;
            uint x8 = x_test << 16;
            uint seed;
            uint rng1;

            // Any possible test seed will have at most
            // a difference of 0x31 from the target seed.
            // If it's close enough, we can then modify it
            // to match.
            if (y_test < 0x31)
            {
                for (uint cnt = 0xFFFF; cnt > 0xF2CC; cnt--)
                {
                    seed = x8 | cnt;

                    // Do a quick search for matching seeds
                    // with a lower 16-bits between 0xFFFF and 0xF2CD.
                    // We'll take the closest matches and subtract 0xD33
                    // until it produces the correct seed (or doesn't).

                    // Best we can do until we find a way to
                    // calculate them directly.

                    rng1 = (reverseXD(seed) >> 16);

                    // We don't have to worry about unsigned overflow
                    // because y_test is never more than 0x7FFF
                    if (rng1 <= y_compare)
                    {
                        rng1 &= 0x7FFF;
                        while ((seed & 0xFFFF) > 0xD32 && rng1 > y_test)
                        {
                            seed = seed - 0xD33;
                            rng1 = (reverseXD(seed) >> 16) & 0x7FFF;
                        }
                    }
                    else
                        rng1 &= 0x7FFF;

                    if (rng1 == y_test)
                    {
                        uint pid1 = forwardXD(forwardXD(seed));
                        uint pid2 = forwardXD(pid1);
                        uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                        uint nature = pid % 25;
                        seed = reverseXD(reverseXD(seed));
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                        pid ^= 0x80008000;
                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                    }
                }
            }
            else
            {
                for (uint cnt = 0xFFFF; cnt > 0xF2CC; cnt--)
                {
                    seed = x8 | cnt;

                    // Do a quick search for matching seeds
                    // with a lower 16-bits between 0xFFFF and 0xF2CD.
                    // We'll take the closest matches and subtract 0xD33
                    // until it produces the correct seed (or doesn't).

                    // Best we can do until we find a way to
                    // calculate them directly.

                    rng1 = (reverseXD(seed) >> 16) & 0x7FFF;

                    // We don't have to worry about unsigned overflow
                    // because y_test is never more than 0x7FFF
                    if (rng1 >= y_compare)
                    {
                        rng1 &= 0x7FFF;
                        while ((seed & 0xFFFF) > 0xD32 && rng1 < y_test)
                        {
                            seed = seed - 0xD33;
                            rng1 = (reverseXD(seed) >> 16) & 0x7FFF;
                        }
                    }
                    else
                        rng1 &= 0x7FFF;

                    if (rng1 == y_test)
                    {
                        uint pid1 = forwardXD(forwardXD(seed));
                        uint pid2 = forwardXD(pid1);
                        uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                        uint nature = pid % 25;
                        seed = reverseXD(reverseXD(seed));
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                        pid ^= 0x80008000;
                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                    }
                }
            }*/
        }

        private void filterSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint pid, uint nature, uint seed)
        {
            String shiny = "";
            if (Shiny_Check.Checked)
            {
                if (!isShiny(pid, 0))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null && !hiddenPowerList.Contains(actualHP))
                return;

            uint ability = pid & 1;
            if (abilityFilter != 0 && (ability != (abilityFilter - 1)))
                return;

            uint gender = pid & 255;
            switch (genderFilter)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, "", 0);
        }
        #endregion

        #region Second search method
        private void generateColo2(uint seed, int num1)
        {
            uint[] seedShort = new uint[6];
            uint[] seedLong = new uint[6];
            seedShort[0] = seed >> 16;
            seedLong[0] = seed;
            var rng = new XdRng(seed);

            for (int i = 1; i < 6; i ++)
            {
                seedShort[i] = rng.GetNext16BitNumber();
                seedLong[i] = rng.Seed;
            }

            int j = 5;
            uint pid, nature;
            uint[] ivs;

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < num1; h++)
                {
                    for (uint n = 0; n < 1048576; n++, seedShort[j] = rng.GetNext16BitNumber(), seedLong[j] = rng.Seed)
                    {
                        if (++j > 5)
                            j = 0;

                        ivs = createIVs(seedShort[j >= 5 ? j - 5 : j + 1], seedShort[j >= 4 ? j - 4 : j + 2]);
                        if (ivs != null)
                        {
                            pid = seedLong[j >= 2 ? j - 2 : j + 4] & 0xFFFF0000 | seedShort[j >= 1 ? j - 1 : j + 5];
                            nature = pid % 25;
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seedLong[j]);

                            pid ^= 0x80008000;
                            nature = pid % 25;
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seedLong[j] ^ 0x80000000);
                        }
                    }
                    Invoke(new Action(() => { binding.ResetBindings(false); }));
                }
            }
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion
        #endregion

        #region Channel

        private void getChannelMethod()
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            switch (cores)
            {
                case 1:
                    if (method > 94)
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateChannel2(0, 64, 0));
                        searchThread[0].Start();
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateChannel());
                        searchThread[0].Start();
                    }
                    break;
                case 2:
                    if (method > 48)
                    {
                        searchThread = new Thread[2];
                        for (int i = 0; i < 1; i++)
                        {
                            searchThread[i] = new Thread(() => generateChannel2((uint)i * 0x40000000, 32, i));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateChannel());
                        searchThread[0].Start();
                    }
                    break;
                case 4:
                    if (method > 26)
                    {
                        searchThread = new Thread[4];
                        for (int i = 0; i < 4; i++)
                        {
                            searchThread[i] = new Thread(() => generateChannel2((uint)i * 0x20000000, 16, i));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateChannel());
                        searchThread[0].Start();
                    }
                    break;
                case 8:
                    if (method > 22)
                    {
                        searchThread = new Thread[8];
                        for (int i = 0; i < 8; i++)
                        {
                            searchThread[i] = new Thread(() => generateChannel2((uint)i * 0x10000000, 8, i));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateChannel());
                        searchThread[0].Start();
                    }
                    break;
            }
        }

        #region Search 1
        private void generateChannel()
        {
            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedChannel(a, b, c, d, e, f);
                        }

            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeedChannel(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            uint x16 = spd << 27;
            uint upper = x16 | 0x7ffffff;
            var rng = new XdRngR(0);

            while (x16 < upper)
            {
                rng.Seed = ++x16;
                if (rng.GetNext16BitNumber() >> 11 == spa)
                {
                    if (rng.GetNext16BitNumber() >> 11 == spe)
                    {
                        if (rng.GetNext16BitNumber() >> 11 == def)
                        {
                            if (rng.GetNext16BitNumber() >> 11 == atk)
                            {
                                if (rng.GetNext16BitNumber() >> 11 == hp)
                                {
                                    rng.GetNext32BitNumber(3);
                                    uint pid2 = rng.GetNext16BitNumber();
                                    uint pid1 = rng.GetNext16BitNumber();
                                    uint sid = rng.GetNext16BitNumber();
                                    uint pid = (pid1 << 16) | pid2;
                                    if ((pid2 > 7 ? 0 : 1) != (pid1 ^ sid ^ 40122))
                                        pid ^= 0x80000000;
                                    uint nature = pid % 25;
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        uint seed = rng.GetNext32BitNumber();
                                        shinyval[0] = (40122 ^ (sid)) >> 3;
                                        filterSeedChannel(hp, atk, def, spa, spd, spe, seed, pid, nature, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Search 2
        //Credits to Zari and amab for this
        private void generateChannel2(uint seed, uint num1, int shinyIndex)
        {
            uint[] seedShort = new uint[13];
            uint[] seedLong = new uint[13];
            seedShort[0] = seed >> 16;
            seedLong[0] = seed;
            var rng = new XdRng(seed);

            for (int i = 0; i < 12; i++)
            {
                seedShort[i] = rng.GetNext16BitNumber();
                seedLong[i] = rng.GetNext32BitNumber();
            }

            uint pid, pid1, pid2, nature, sid;
            uint[] ivs;
            int j = 12;

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < num1; h++)
                {
                    for (uint n = 0; n < 1048576; n++, seedShort[j] = rng.GetNext16BitNumber(), seedLong[j] = rng.Seed)
                    {
                        if (++j > 12)
                            j = 0;

                        pid1 = seedShort[j >= 11 ? j - 11 : j + 2];
                        pid2 = seedShort[j >= 10 ? j - 10 : j + 3];
                        pid = (pid1 << 16) | pid2;
                        sid = seedShort[j >= 12 ? j - 12 : j + 1];
                        if ((pid2 > 7 ? 0 : 1) != (pid1 ^ sid ^ 40122))
                            pid ^= 0x80000000;
                        nature = pid % 25;

                        if (natureList == null || natureList.Contains(nature))
                        {
                            
                            ivs = createIVsChannel(new uint[] { seedShort[j >= 6 ? j - 6 : j + 7] >> 11,
                                                            seedShort[j >= 5 ? j - 5 : j + 8] >> 11,
                                                            seedShort[j >= 4 ? j - 4 : j + 9] >> 11,
                                                            seedShort[j >= 2 ? j - 2 : j + 11] >> 11,
                                                            seedShort[j >= 1 ? j - 1 : j + 12] >> 11,
                                                            seedShort[j >= 3 ? j - 3 : j + 10] >> 11 });
                            if (ivs != null)
                            {
                                shinyval[shinyIndex] = (40122 ^ sid) >> 3;
                                filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], seedLong[j], pid, nature, shinyIndex);
                            }
                        }

                        pid ^= 0x80008000;
                        nature = pid % 25;

                        if (natureList == null || natureList.Contains(nature))
                        {
                            ivs = createIVsChannel(new uint[] { (seedShort[j >= 6 ? j - 6 : j + 7] ^ 0x8000) >> 11,
                                                            (seedShort[j >= 5 ? j - 5 : j + 8] ^ 0x8000) >> 11,
                                                            (seedShort[j >= 4 ? j - 4 : j + 9] ^ 0x8000) >> 11,
                                                            (seedShort[j >= 2 ? j - 2 : j + 11] ^ 0x8000) >> 11,
                                                            (seedShort[j >= 1 ? j - 1 : j + 12] ^ 0x8000) >> 11,
                                                            (seedShort[j >= 3 ? j - 3 : j + 10] ^ 0x8000) >> 11 });
                            if (ivs != null)
                            {
                                shinyval[shinyIndex] = (40122 ^ (sid ^ 0x8000)) >> 3;
                                filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], seedLong[j] ^ 0x80000000, pid, nature, shinyIndex);
                            }
                        }
                    }
                    Invoke(new Action(() => { binding.ResetBindings(false); }));
                }
            }
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private uint[] createIVsChannel(uint[] iv)
        {
            for (int x = 0; x < 6; x++)
                if (iv[x] < ivsLower[x] || iv[x] > ivsUpper[x])
                    return null;
            return iv;
        }

        private void filterSeedChannel(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint seed, uint pid, uint nature, int shinyIndex)
        {
            String shiny = "";
            if (Shiny_Check.Checked)
            {
                if (!isShiny(pid, shinyIndex))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null && !hiddenPowerList.Contains(actualHP))
                return;

            uint ability = pid & 1;
            if (abilityFilter != 0 && (ability != (abilityFilter - 1)))
                return;

            uint gender = pid & 255;
            switch (genderFilter)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, "", shinyIndex);
        }
        #endregion
        #endregion

        #region Reverse Method 1
        private void getRMethod()
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            switch (cores)
            {
                case 1:
                    if (method > 162268)
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateR2(0, 64));
                        searchThread[0].Start();
                        Thread.Sleep(200);
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateR());
                        searchThread[0].Start();
                    }
                    break;
                case 2:
                    if (method > 83720)
                    {
                        searchThread = new Thread[2];
                        for (int i = 0; i < 2; i++)
                        {
                            searchThread[i] = new Thread(() => generateR2((uint)(i * 0x40000000), 32));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateR());
                        searchThread[0].Start();
                    }
                    break;
                case 4:
                    if (method > 45918)
                    {
                        searchThread = new Thread[4];
                        for (int i = 0; i < 4; i++)
                        {
                            searchThread[i] = new Thread(() => generateR2((uint)(i * 0x20000000), 16));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateR());
                        searchThread[0].Start();
                    }
                    break;
                case 8:
                    if (method > 34970)
                    {
                        searchThread = new Thread[8];
                        for (int i = 0; i < 8; i++)
                        {
                            searchThread[i] = new Thread(() => generateR2((uint)(i * 0x10000000), 8));
                            searchThread[i].Start();
                            Thread.Sleep(200);
                        }
                    }
                    else
                    {
                        searchThread = new Thread[1];
                        searchThread[0] = new Thread(() => generateR());
                        searchThread[0].Start();
                    }
                    break;
            }
        }

        #region Search 1
        private void generateR()
        {
            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                        {
                            Invoke(new Action(() => { binding.ResetBindings(false); }));
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedR(a, b, c, d, e, f);
                        }

            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeedR(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            uint x4 = hp | (atk << 5) | (def << 10);
            uint ex4 = spe | (spa << 5) | (spd << 10);
            uint ivs_1b = x4 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b | cnt;
                uint ivs_2 = forward(seedb) >> 16;
                if ((ivs_2 & 0x7FFF) == ex4)
                {
                    uint pid2 = reverse(seedb);
                    uint pid1 = reverse(pid2);
                    uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                    uint nature = pid % 25;
                    uint seed = reverse(pid1);
                    if (natureList == null || natureList.Contains(nature))
                        filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                    pid ^= 0x80008000;
                    nature = pid % 25;
                    if (natureList == null || natureList.Contains(nature))
                        filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                }
            }

            /*uint x_test = spe | (spa << 5) | (spd << 10);
            uint y_test = hp | (atk << 5) | (def << 10);
            uint y_compare = (y_test - 0x31) & 0xFFFF;
            uint x8 = x_test << 16;
            uint seed;
            uint rng1;

            // Any possible test seed will have at most
            // a difference of 0x31 from the target seed.
            // If it's close enough, we can then modify it
            // to match.
            if (y_test < 0x31)
            {
                for (uint cnt = 0xFFFF; cnt > 0xF2CC; cnt--)
                {
                    seed = x8 | cnt;

                    // Do a quick search for matching seeds
                    // with a lower 16-bits between 0xFFFF and 0xF2CD.
                    // We'll take the closest matches and subtract 0xD33
                    // until it produces the correct seed (or doesn't).

                    // Best we can do until we find a way to
                    // calculate them directly.

                    rng1 = reverse(seed) >> 16;

                    // We don't have to worry about unsigned overflow
                    // because y_test is never more than 0x7FFF
                    if (rng1 <= y_compare)
                    {
                        rng1 &= 0x7FFF;
                        while ((seed & 0xFFFF) > 0xD32 && rng1 > y_test)
                        {
                            seed = seed - 0xD33;
                            rng1 = (reverse(seed) >> 16) & 0x7FFF;
                        }
                    }
                    else
                        rng1 &= 0x7FFF;

                    if (rng1 == y_test)
                    {
                        uint pid2 = reverse(reverse(seed));
                        uint pid1 = reverse(pid2);
                        uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                        uint nature = pid % 25;
                        seed = reverse(pid1);
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                        pid ^= 0x80008000;
                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                    }
                }
            }
            else
            {
                for (uint cnt = 0xFFFF; cnt > 0xF2CC; cnt--)
                {
                    seed = x8 | cnt;

                    // Do a quick search for matching seeds
                    // with a lower 16-bits between 0xFFFF and 0xF2CD.
                    // We'll take the closest matches and subtract 0xD33
                    // until it produces the correct seed (or doesn't).

                    // Best we can do until we find a way to
                    // calculate them directly.

                    rng1 = reverse(seed) >> 16;

                    // We don't have to worry about unsigned overflow
                    // because y_test is never more than 0x7FFF
                    if (rng1 >= y_compare)
                    {
                        rng1 &= 0x7FFF;
                        while ((seed & 0xFFFF) > 0xD32 && rng1 < y_test)
                        {
                            seed = seed - 0xD33;
                            rng1 = (reverse(seed) >> 16) & 0x7FFF;
                        }
                    }
                    else
                        rng1 &= 0x7FFF;

                    if (rng1 == y_test)
                    {
                        uint pid2 = reverse(reverse(seed));
                        uint pid1 = reverse(pid2);
                        uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                        uint nature = pid % 25;
                        seed = reverse(pid1);
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed);

                        pid ^= 0x80008000;
                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                            filterSeed(hp, atk, def, spa, spd, spe, pid, nature, seed ^ 0x80000000);
                    }
                }
            }*/
        }
        #endregion

        #region Search 2
        private void generateR2(uint seed, int num1)
        {
            uint[] seedLong = new uint[5];
            uint[] seedShort = new uint[5];
            seedLong[0] = seed;
            seedShort[0] = seed >> 16;
            var rng = new PokeRng(seed);

            for (int i = 1; i < 5; i++)
            {
                seedShort[i] = rng.GetNext16BitNumber();
                seedLong[i] = rng.Seed;
            }

            uint pid, nature;
            uint[] ivs;
            int j = 4;

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < num1; h++)
                {
                    for (uint n = 0; n < 1048576; n++, seedShort[j] = rng.GetNext16BitNumber(), seedLong[j] = rng.Seed)
                    {
                        if (++j > 4)
                            j = 0;

                        ivs = createIVs(seedShort[j >= 2 ? j - 2 : j + 3], seedShort[j >= 1 ? j - 1 : j + 4]);
                        if (ivs != null)
                        {
                            pid = seedLong[j >= 4 ? j - 4 : j + 1] & 0xFFFF0000 | seedShort[j >= 3 ? j - 3 : j + 2];
                            nature = pid % 25;
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seedLong[j]);

                            pid ^= 0x80008000;
                            nature = pid % 25;
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, seedLong[j] ^ 0x80000000);
                        }
                    }
                    Invoke(new Action(() => { binding.ResetBindings(false); }));
                }
            }
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion
        #endregion

        #region Wishmkr
        private void generateWishmkr()
        {
            shinyval[0] = 2505;
            for (uint x = 0; x <= 0xFFFF; x++)
            {
                uint pid1 = forward(x);
                uint pid2 = forward(pid1);
                uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                uint nature = pid % 25;

                if (natureList == null || natureList.Contains(nature))
                {
                    uint ivs1 = forward(pid2);
                    uint ivs2 = forward(ivs1);
                    ivs1 >>= 16;
                    ivs2 >>= 16;
                    uint[] ivs = createIVs(ivs1, ivs2);
                    if (ivs != null)
                        filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], pid, nature, x);
                }
            }
            isSearching = false;
            Invoke(new Action(() => { binding.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion

        private void addSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint pid, String shiny, uint seed, String output, int shinyIndex)
        {
            displayList.Insert(0, new DisplayList
            {
                Seed = seed.ToString("X"),
                PID = pid.ToString("X"),
                Shiny = !galesFlag ? shiny == "" ? isShiny(pid, shinyIndex) ? "!!!" : "" : shiny : shiny,
                Nature = Natures[nature],
                Ability = ability,
                Hp = hp,
                Atk = atk,
                Def = def,
                SpA = spa,
                SpD = spd,
                Spe = spe,
                Hidden = hiddenPowers[hP],
                Power = calcHPPower(hp, atk, def, spa, spd, spe),
                Eighth = gender < 31 ? 'F' : 'M',
                Quarter = gender < 64 ? 'F' : 'M',
                Half = gender < 126 ? 'F' : 'M',
                Three_Fourths = gender < 191 ? 'F' : 'M',
                Reason = output
            });
        }
        #endregion

        #region Shadow Checker

        private void generateShadow_Click(object sender, EventArgs e)
        {
            getIVsShadow(out ivsLower, out ivsUpper);
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
                if (isSearching)
                {
                    status.Text = "Previous search is still running";
                    return;
                }

                natureList = null;
                if (checkBoxNatureShadow.Text != "Any" && checkBoxNatureShadow.CheckBoxItems.Count > 0)
                    natureList = (from t in checkBoxNatureShadow.CheckBoxItems where t.Checked select (uint)((Nature)t.ComboBoxItem).Number).ToList();

                hiddenPowerList = null;
                List<uint> temp = new List<uint>();
                if (checkBoxHPShadow.Text != "Any" && checkBoxHPShadow.CheckBoxItems.Count > 0)
                    for (int x = 1; x <= 16; x++)
                        if (checkBoxHPShadow.CheckBoxItems[x].Checked)
                            temp.Add((uint)(x - 1));

                if (temp.Count != 0)
                    hiddenPowerList = temp;

                uint.TryParse(maskedTextBoxStartingFrame.Text, out uint initialFrame);
                uint.TryParse(maskedTextBoxMaxFrames.Text, out uint maxFrame);
                uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint seed);
                int shadowMethod = comboBoxMethodShadow.SelectedIndex;
                genderFilter = (uint)comboBoxGenderShadow.SelectedIndex;
                abilityFilter = (uint)comboBoxAbilityShadow.SelectedIndex;
                natureLock.changeLock(comboBoxShadow.SelectedIndex);
                shadow = natureLock.getType();

                shadowDisplay.Clear();
                bindingShadow.ResetBindings(false);
                isSearching = true;
                status.Text = "Searching";

                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => shadowSearch(initialFrame, maxFrame, seed, shadowMethod));
                searchThread[0].Start();
            }
        }

        private void shadowSearch(uint initialFrame, uint maxFrame, uint inSeed, int secondMethod)
        {
            var rng = new XdRng(inSeed);
            rng.GetNext32BitNumber(initialFrame == 0 ? 0 : (int)initialFrame - 1);

            uint pid, iv1, iv2, nature;
            uint[] ivs;

            switch(shadow)
            {
                case ShadowType.NoLock:

                    uint[] rand = new uint[6];
                    rand[0] = rng.Seed >> 16;

                    for (int x = 1; x < 6; x++)
                        rand[x] = rng.GetNext16BitNumber();

                    int j = 5;

                    for (uint cnt = 0; cnt < maxFrame; cnt++, rand[j] = rng.GetNext16BitNumber())
                    {
                        if (++j > 5)
                            j = 0;

                        pid = (rand[j >= 2 ? j - 2 : j + 4] << 16) | rand[j >= 1 ? j - 1 : j + 5];
                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                        {
                            ivs = createIVs(rand[j >= 5 ? j - 5 : j + 1], rand[j >= 4 ? j - 4 : j + 2]);
                            if (ivs != null)
                                filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(initialFrame + cnt), nature, pid);
                        }
                    }

                    break;
                case ShadowType.SingleLock:
                case ShadowType.FirstShadow:
                    natureLock.rand.Add(rng.Seed);
                    for (uint x = 0; x < 2999; x++)
                        natureLock.rand.Add(rng.GetNext32BitNumber());

                    for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                    {
                        natureLock.methodShadowFirstShadow(out pid, out iv1, out iv2);

                        nature = pid % 25;
                        if (natureList == null || natureList.Contains(nature))
                        {
                            ivs = createIVs(iv1, iv2);
                            if (ivs != null)
                                filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid);
                        }
                    }
                    break;
                case ShadowType.Salamence:
                case ShadowType.SecondShadow:
                    switch(secondMethod)
                    {
                        //Set
                        case 0:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2999; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.methodShadowSecondShadowSet(out pid, out iv1, out iv2);

                                nature = pid % 25;
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid);
                                }
                            }
                            break;
                        //Unset
                        case 1:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2999; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.methodShadowSecondShadowUnset(out pid, out iv1, out iv2);

                                nature = pid % 25;
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid);
                                }
                            }
                            break;
                        //Shiny skip
                        case 2:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2999; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.methodShadowSecondShinySkip(out pid, out iv1, out iv2);

                                nature = pid % 25;
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid);
                                }
                            }
                            break;
                    }
                    break;
            }
            natureLock.rand.Clear();
            isSearching = false;
            Invoke(new Action(() => { bindingShadow.ResetBindings(false); }));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        public void filterSeedShadow(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int frame, uint nature, uint pid)
        {
            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null)
                if (!hiddenPowerList.Contains(actualHP))
                    return;

            uint ability = pid & 1;
            if (abilityFilter != 0)
                if (ability != (abilityFilter - 1))
                    return;

            uint gender = pid & 255;
            switch (genderFilter)
            {
                case 1:
                    if (gender < 127)
                        return;
                    break;
                case 2:
                    if (gender > 126)
                        return;
                    break;
                case 3:
                    if (gender < 191)
                        return;
                    break;
                case 4:
                    if (gender > 190)
                        return;
                    break;
                case 5:
                    if (gender < 64)
                        return;
                    break;
                case 6:
                    if (gender > 63)
                        return;
                    break;
                case 7:
                    if (gender < 31)
                        return;
                    break;
                case 8:
                    if (gender > 30)
                        return;
                    break;
            }
            addSeedShadow(hp, atk, def, spa, spd, spe, frame, nature, ability, gender, actualHP, pid);
        }

        private void addSeedShadow(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int frame, uint nature, uint ability, uint gender, uint hP, uint pid)
        {
            shadowDisplay.Add(new ShadowDisplay
            {
                Frame = frame,
                PID = pid.ToString("X"),
                Nature = Natures[nature],
                Ability = ability,
                Hp = hp,
                Atk = atk,
                Def = def,
                SpA = spa,
                SpD = spd,
                Spe = spe,
                Hidden = hiddenPowers[hP],
                Power = calcHPPower(hp, atk, def, spa, spd, spe),
                Eighth = gender < 31 ? 'F' : 'M',
                Quarter = gender < 64 ? 'F' : 'M',
                Half = gender < 126 ? 'F' : 'M',
                Three_Fourths = gender < 191 ? 'F' : 'M'
            });
        }

        #endregion

        #region Helper methods
        private void getIVs(out uint[] IVsLower, out uint[] IVsUpper)
        {
            IVsLower = new uint[6];
            IVsUpper = new uint[6];
            uint[] ivs = new uint[6];

            uint.TryParse(hpValue.Text, out ivs[0]);
            uint.TryParse(atkValue.Text, out ivs[1]);
            uint.TryParse(defValue.Text, out ivs[2]);
            uint.TryParse(spaValue.Text, out ivs[3]);
            uint.TryParse(spdValue.Text, out ivs[4]);
            uint.TryParse(speValue.Text, out ivs[5]);

            int[] ivsLogic = { hpLogic.SelectedIndex, atkLogic.SelectedIndex, defLogic.SelectedIndex, spaLogic.SelectedIndex, spdLogic.SelectedIndex, speLogic.SelectedIndex };

            for (int x = 0; x < 6; x++)
            {
                if (ivsLogic[x] == 0)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = ivs[x];
                }
                else if (ivsLogic[x] == 1)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = 31;
                }
                else
                {
                    IVsLower[x] = 0;
                    IVsUpper[x] = ivs[x];
                }
            }
        }

        private void getIVsShadow(out uint[] IVsLower, out uint[] IVsUpper)
        {
            IVsLower = new uint[6];
            IVsUpper = new uint[6];
            uint[] ivs = new uint[6];

            uint.TryParse(hpShadow.Text, out ivs[0]);
            uint.TryParse(atkShadow.Text, out ivs[1]);
            uint.TryParse(defShadow.Text, out ivs[2]);
            uint.TryParse(spaShadow.Text, out ivs[3]);
            uint.TryParse(spdShadow.Text, out ivs[4]);
            uint.TryParse(speShadow.Text, out ivs[5]);

            int[] ivsLogic = { hpLogicShadow.SelectedIndex, atkLogicShadow.SelectedIndex, defLogicShadow.SelectedIndex, spaLogicShadow.SelectedIndex, spdLogicShadow.SelectedIndex, speLogicShadow.SelectedIndex };

            for (int x = 0; x < 6; x++)
            {
                if (ivsLogic[x] == 0)
                {
                    IVsLower[x] = 0;
                    IVsUpper[x] = 31;
                }
                else if (ivsLogic[x] == 1)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = ivs[x];
                }
                else if (ivsLogic[x] == 2)
                {
                    IVsLower[x] = ivs[x];
                    IVsUpper[x] = 31;
                }
                else
                {
                    IVsLower[x] = 0;
                    IVsUpper[x] = ivs[x];
                }
            }
        }

        private uint[] createIVs(uint iv1, uint ivs2)
        {
            uint[] ivs = new uint[6];

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                if (iv >= ivsLower[x] && iv <= ivsUpper[x])
                    ivs[x] = iv;
                else
                    return null;
            }

            uint iV = (ivs2 >> 5) & 31;
            if (iV >= ivsLower[3] && iV <= ivsUpper[3])
                ivs[3] = iV;
            else
                return null;

            iV = (ivs2 >> 10) & 31;
            if (iV >= ivsLower[4] && iV <= ivsUpper[4])
                ivs[4] = iV;
            else
                return null;

            iV = ivs2 & 31;
            if (iV >= ivsLower[5] && iV <= ivsUpper[5])
                ivs[5] = iV;
            else
                return null;

            return ivs;
        }

        private uint forwardXD(uint seed)
        {
            return seed * 0x343FD + 0x269EC3;
        }

        private uint reverseXD(uint seed)
        {
            return seed * 0xB9B33155 + 0xA170F641;
        }

        private uint forward(uint seed)
        {
            return seed * 0x41c64e6d + 0x6073;
        }

        private uint reverse(uint seed)
        {
            return seed * 0xeeb9eb65 + 0xa3561a1;
        }

        private int calcHPPower(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return (int)(30 + ((((hp >> 1) & 1) + 2 * ((atk >> 1) & 1) + 4 * ((def >> 1) & 1) + 8 * ((spe >> 1) & 1) + 16 * ((spa >> 1) & 1) + 32 * ((spd >> 1) & 1)) * 40 / 63));
        }

        private bool isShiny(uint PID, int shinyIndex)
        {
            return (((PID >> 16) ^ (PID & 0xffff)) >> 3) == shinyval[shinyIndex];
        }

        private uint calcHP(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return ((((hp & 1) + 2 * (atk & 1) + 4 * (def & 1) + 8 * (spe & 1) + 16 * (spa & 1) + 32 * (spd & 1)) * 15) / 63);
        }
        #endregion

        #region Quick search settings

        private void hpClear_Click(object sender, EventArgs e)
        {
            hpValue.Text = "0";
            hpLogic.SelectedIndex = 1;
        }

        private void atkClear_Click(object sender, EventArgs e)
        {
            atkValue.Text = "0";
            atkLogic.SelectedIndex = 1;
        }

        private void defClear_Click(object sender, EventArgs e)
        {
            defValue.Text = "0";
            defLogic.SelectedIndex = 1;
        }

        private void spaClear_Click(object sender, EventArgs e)
        {
            spaValue.Text = "0";
            spaLogic.SelectedIndex = 1;
        }

        private void spdClear_Click(object sender, EventArgs e)
        {
            spdValue.Text = "0";
            spdLogic.SelectedIndex = 1;
        }

        private void speClear_Click(object sender, EventArgs e)
        {
            speValue.Text = "0";
            speLogic.SelectedIndex = 1;
        }

        private void hp31Quick_Click(object sender, EventArgs e)
        {
            hpValue.Text = "31";
            hpLogic.SelectedIndex = 0;
        }

        private void hp30Quick_Click(object sender, EventArgs e)
        {
            hpValue.Text = "30";
            hpLogic.SelectedIndex = 0;
        }

        private void hp30Above_Click(object sender, EventArgs e)
        {
            hpValue.Text = "30";
            hpLogic.SelectedIndex = 1;
        }

        private void atk31Quick_Click(object sender, EventArgs e)
        {
            atkValue.Text = "31";
            atkLogic.SelectedIndex = 0;
        }

        private void atk30Quick_Click(object sender, EventArgs e)
        {
            atkValue.Text = "30";
            atkLogic.SelectedIndex = 0;
        }

        private void atk30Above_Click(object sender, EventArgs e)
        {
            atkValue.Text = "30";
            atkLogic.SelectedIndex = 1;
        }

        private void def31Quick_Click(object sender, EventArgs e)
        {
            defValue.Text = "31";
            defLogic.SelectedIndex = 0;
        }

        private void def30Quick_Click(object sender, EventArgs e)
        {
            defValue.Text = "30";
            defLogic.SelectedIndex = 0;
        }

        private void def30Above_Click(object sender, EventArgs e)
        {
            defValue.Text = "30";
            defLogic.SelectedIndex = 1;
        }

        private void spa31Quick_Click(object sender, EventArgs e)
        {
            spaValue.Text = "31";
            spaLogic.SelectedIndex = 0;
        }

        private void spa30Quick_Click(object sender, EventArgs e)
        {
            spaValue.Text = "30";
            spaLogic.SelectedIndex = 0;
        }

        private void spa30Above_Click(object sender, EventArgs e)
        {
            spaValue.Text = "30";
            spaLogic.SelectedIndex = 1;
        }

        private void spd31Quick_Click(object sender, EventArgs e)
        {
            spdValue.Text = "31";
            spdLogic.SelectedIndex = 0;
        }

        private void spd30Quick_Click(object sender, EventArgs e)
        {
            spdValue.Text = "30";
            spdLogic.SelectedIndex = 0;
        }

        private void spd30Above_Click(object sender, EventArgs e)
        {
            spdValue.Text = "30";
            spdLogic.SelectedIndex = 1;
        }

        private void spe31Quick_Click(object sender, EventArgs e)
        {
            speValue.Text = "31";
            speLogic.SelectedIndex = 0;
        }

        private void spe30Quick_Click(object sender, EventArgs e)
        {
            speValue.Text = "30";
            speLogic.SelectedIndex = 0;
        }

        private void spe30Above_Click(object sender, EventArgs e)
        {
            speValue.Text = "30";
            speLogic.SelectedIndex = 1;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                status.Text = "Cancelled. - Awaiting Command";
                for (int x = 0; x < searchThread.Length; x++)
                    searchThread[x].Abort();
                natureLock.rand.Clear();
                binding.ResetBindings(false);
            }
        }

        private void anyNature_Click(object sender, EventArgs e)
        {
            comboBoxNature.ClearSelection();
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
            comboBoxHiddenPower.ClearSelection();
        }

        private void hpMaxShadow_Click(object sender, EventArgs e)
        {
            hpShadow.Text = "31";
            hpLogicShadow.SelectedIndex = 1;
        }

        private void hpNearMaxShadow_Click(object sender, EventArgs e)
        {
            hpShadow.Text = "30";
            hpLogicShadow.SelectedIndex = 1;
        }

        private void hpAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            hpShadow.Text = "30";
            hpLogicShadow.SelectedIndex = 2;
        }

        private void hpClearShadow_Click(object sender, EventArgs e)
        {
            hpShadow.Text = "";
            hpLogicShadow.SelectedIndex = 0;
        }

        private void atkMaxShadow_Click(object sender, EventArgs e)
        {
            atkShadow.Text = "31";
            atkLogicShadow.SelectedIndex = 1;
        }

        private void atkNearMaxShadow_Click(object sender, EventArgs e)
        {
            atkShadow.Text = "30";
            atkLogicShadow.SelectedIndex = 1;
        }

        private void atkAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            atkShadow.Text = "30";
            atkLogicShadow.SelectedIndex = 2;
        }

        private void atkClearShadow_Click(object sender, EventArgs e)
        {
            atkShadow.Text = "";
            atkLogicShadow.SelectedIndex = 0;
        }

        private void defMaxShadow_Click(object sender, EventArgs e)
        {
            defShadow.Text = "31";
            defLogicShadow.SelectedIndex = 1;
        }

        private void defNearMaxShadow_Click(object sender, EventArgs e)
        {
            defShadow.Text = "30";
            defLogicShadow.SelectedIndex = 1;
        }

        private void defAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            defShadow.Text = "30";
            defLogicShadow.SelectedIndex = 2;
        }

        private void defClearShadow_Click(object sender, EventArgs e)
        {
            defShadow.Text = "";
            defLogicShadow.SelectedIndex = 0;
        }

        private void spaMaxShadow_Click(object sender, EventArgs e)
        {
            spaShadow.Text = "31";
            spaLogicShadow.SelectedIndex = 1;
        }

        private void spaNearMaxShadow_Click(object sender, EventArgs e)
        {
            spaShadow.Text = "30";
            spaLogicShadow.SelectedIndex = 1;
        }

        private void spaAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            spaShadow.Text = "30";
            spaLogicShadow.SelectedIndex = 2;
        }

        private void spaClearShadow_Click(object sender, EventArgs e)
        {
            spaShadow.Text = "";
            spaLogicShadow.SelectedIndex = 0;
        }

        private void spdMaxShadow_Click(object sender, EventArgs e)
        {
            spdShadow.Text = "31";
            spdLogicShadow.SelectedIndex = 1;
        }

        private void spdNearMaxShadow_Click(object sender, EventArgs e)
        {
            spdShadow.Text = "30";
            spdLogicShadow.SelectedIndex = 1;
        }

        private void spdAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            spdShadow.Text = "30";
            spdLogicShadow.SelectedIndex = 2;
        }

        private void spdClearShadow_Click(object sender, EventArgs e)
        {
            spdShadow.Text = "";
            spdLogicShadow.SelectedIndex = 0;
        }

        private void speMaxShadow_Click(object sender, EventArgs e)
        {
            speShadow.Text = "31";
            speLogicShadow.SelectedIndex = 1;
        }

        private void speNearMaxShadow_Click(object sender, EventArgs e)
        {
            speShadow.Text = "30";
            speLogicShadow.SelectedIndex = 1;
        }

        private void speAlmostMaxShadow_Click(object sender, EventArgs e)
        {
            speShadow.Text = "30";
            speLogicShadow.SelectedIndex = 2;
        }

        private void speClearShadow_Click(object sender, EventArgs e)
        {
            speShadow.Text = "";
            speLogicShadow.SelectedIndex = 0;
        }

        private void anyNatureShadow_Click(object sender, EventArgs e)
        {
            checkBoxNatureShadow.ClearSelection();
        }

        private void anyGenderShadow_Click(object sender, EventArgs e)
        {
            comboBoxGenderShadow.SelectedIndex = 0;
        }

        private void anyAbilityShadow_Click(object sender, EventArgs e)
        {
            comboBoxAbilityShadow.SelectedIndex = 0;
        }

        private void anyHPShadow_Click(object sender, EventArgs e)
        {
            checkBoxHPShadow.ClearSelection();
        }
        #endregion

        #region ComboBox
        private String[] addHP()
        {
            return new String[]
                {
                    "Fighting",
                    "Flying",
                    "Poison",
                    "Ground",
                    "Rock",
                    "Bug",
                    "Ghost",
                    "Steel",
                    "Fire",
                    "Water",
                    "Grass",
                    "Electric",
                    "Psychic",
                    "Ice",
                    "Dragon",
                    "Dark"
                };
        }

        private void setComboBox()
        {
            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = true;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = false;
            checkBoxNatureShadow.CheckBoxItems[0].Checked = true;
            checkBoxNatureShadow.CheckBoxItems[0].Checked = false;
            checkBoxHPShadow.CheckBoxItems[0].Checked = true;
            checkBoxHPShadow.CheckBoxItems[0].Checked = false;
        }

        private void galesCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (galesCheck.Checked == true)
            {
                if (shadowPokemon.SelectedIndex == 41)
                    Shiny_Check.Visible = true;
                else
                    Shiny_Check.Visible = false;
            }
            else
                Shiny_Check.Visible = true;
        }

        private void shadowPokemon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (galesCheck.Checked)
            {
                if (shadowPokemon.SelectedIndex == 41)
                    Shiny_Check.Visible = true;
                else
                    Shiny_Check.Visible = false;
            }
        }

        private void searchMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int method = searchMethod.SelectedIndex;
            if (method == 0)
            {
                wshMkr.Visible = true;
                Shiny_Check.Visible = true;
                shadowPokemon.Visible = false;
                galesCheck.Visible = false;
            }
            else if (method == 1)
            {
                wshMkr.Visible = false;
                Shiny_Check.Visible = true;
                if (galesCheck.Checked)
                {
                    if (shadowPokemon.SelectedIndex == 41)
                        Shiny_Check.Visible = true;
                    else
                        Shiny_Check.Visible = false;
                }
                shadowPokemon.Visible = true;
                galesCheck.Visible = true;
            }
            else
            {
                wshMkr.Visible = false;
                Shiny_Check.Visible = true;
                shadowPokemon.Visible = false;
                galesCheck.Visible = false;
            }
        }

        private void comboBoxShadow_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<int> secondShadows = new List<int> { 0, 6, 8, 10, 21, 30, 32, 37, 50, 57, 66, 67, 75, 93 };
            if (secondShadows.Contains(comboBoxShadow.SelectedIndex))
            {
                comboBoxMethodShadow.Visible = true;
                label21.Visible = true;
            }
            else
            {
                comboBoxMethodShadow.Visible = false;
                label21.Visible = false;
            }
        }
        #endregion

        #region Grid commands
        private void contextMenuStripGrid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewResult.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void copySeedToClipboard_Click(object sender, EventArgs e)
        {
            if (dataGridViewResult.SelectedRows[0] != null)
            {
                var frame = (DisplayList)dataGridViewResult.SelectedRows[0].DataBoundItem;
                Clipboard.SetText(frame.Seed.ToString());
            }
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = dataGridViewResult.HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewResult.Rows[hti.RowIndex])).Selected)
                    {
                        dataGridViewResult.ClearSelection();

                        (dataGridViewResult.Rows[hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void outputResultsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter file = new System.IO.StreamWriter("rngreporter.txt");
            String result = "Seed\t\t" + "PID\t\t" + "Shiny\t" + "Nature\t" + "Ability\t" + "HP\t" + "Atk\t" + "Def\t" + "SpA\t" + "SpD\t" + "Spe\t" + "Hidden\t\t" + "Power\t" + "12.5%F\t" + "25%F\t" + "50%\t" + "75%\t" + "Reason\t\n";
            file.WriteLine(result);
            for (int x = 0; x < displayList.Count; x++)
            {
                String seed = displayList[x].Seed;
                while (seed.Length < 8)
                    seed = "0" + seed;
                String pid = displayList[x].PID;
                while (pid.Length < 8)
                    pid = "0" + pid;
                String temp = "" + seed + "\t" + pid + "\t" + displayList[x].Shiny + "\t" + displayList[x].Nature + "\t" + displayList[x].Ability + "\t" + displayList[x].Hp + "\t" + displayList[x].Atk + "\t" + displayList[x].Def + "\t" + displayList[x].SpA + "\t" + displayList[x].SpD + "\t" + displayList[x].Spe + "\t" + displayList[x].Hidden;
                if (displayList[x].Hidden.Length < 8)
                    temp += "\t";
                temp = temp + "\t" + displayList[x].Power + "\t" + displayList[x].Eighth + "\t" + displayList[x].Quarter + "\t" + displayList[x].Half + "\t" + displayList[x].Three_Fourths + "\t" + displayList[x].Reason + "\n";
                file.WriteLine(temp);
            }
            file.Close();
            MessageBox.Show("Results exported to folder with RNGReporter.exe");
        }

        private void dataGridViewResult_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewResult.DataSource != null && displayList != null && binding != null)
            {
                DataGridViewColumn selectedColumn = dataGridViewResult.Columns[e.ColumnIndex];

                var idisplayListComparer = new IDisplayListComparator
                { CompareType = selectedColumn.DataPropertyName };

                if (selectedColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    idisplayListComparer.sortOrder = SortOrder.Descending;

                displayList.Sort(idisplayListComparer);

                binding.ResetBindings(false);
                selectedColumn.HeaderCell.SortGlyphDirection = idisplayListComparer.sortOrder;
            }
        }
        #endregion
    }
}