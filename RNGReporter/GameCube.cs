using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using RNGReporter.Objects;
using System.Linq;
using System.ComponentModel;
using System.IO;
using System.Globalization;

namespace RNGReporter
{
    public partial class GameCube : Form
    {
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private readonly String[] hiddenPowers = { "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark" };
        private Thread[] searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<DisplayList> displayList;
        private List<ShadowDisplay> shadowDisplay;
        private bool isSearching = false;
        private List<uint> slist = new List<uint>();
        private List<uint> rlist = new List<uint>();
        private uint shinyval;
        private NatureLock natureLock;
        private uint searchNumber;
        private uint shadow;
        private static List<uint> natureList;
        private static List<uint> hiddenPowerList;
        private static bool galesFlag = false;
        private static List<uint> seedList;

        public GameCube(int TID, int SID)
        {
            InitializeComponent();
            id.Text = TID.ToString();
            sid.Text = SID.ToString();
            Reason.Visible = false;
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
        }

        private void GameCube_Load(object sender, EventArgs e)
        {
            comboBoxNature.Items.AddRange(Nature.NatureDropDownCollectionSearchNatures());
            comboBoxHiddenPower.Items.AddRange(addHP());
            checkBoxNatureShadow.Items.AddRange(Nature.NatureDropDownCollectionSearchNatures());
            checkBoxHPShadow.Items.AddRange(addHP());
            comboBoxShadowMethod.Items.AddRange(addShadowMethod());
            setComboBox();
            wshMkr.Visible = true;
            shadowMethodLabel.Visible = false;
            comboBoxShadowMethod.Visible = false;
            anyShadowMethod.Visible = false;
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
            getIVs(out uint[] ivsLower, out uint[] ivsUpper);
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
                dataGridViewResult.Rows.Clear();

                if (isSearching)
                {
                    status.Text = "Previous search is still running";
                    return;
                }

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

                displayList = new List<DisplayList>();
                binding = new BindingSource { DataSource = displayList };
                dataGridViewResult.DataSource = binding;
                status.Text = "Searching";
                slist.Clear();
                rlist.Clear();
                try
                {
                    shinyval = (uint.Parse(id.Text) ^ uint.Parse(sid.Text)) >> 3;
                }
                catch
                {
                    shinyval = 0;
                }
                searchNumber = getSearchMethod();

                if (galesCheck.Checked)
                    Reason.Visible = true;
                else
                    Reason.Visible = false;

                getSearch(ivsLower, ivsUpper);
            }
        }

        private void getSearch(uint[] ivsLower, uint[] ivsUpper)
        {
            if (searchNumber == 0)
            {
                if (wshMkr.Checked)
                {
                    searchThread = new Thread[1];
                    searchThread[0] = new Thread(() => generateWishmkr(ivsLower, ivsUpper));
                    searchThread[0].Start();
                    var update = new Thread(updateGUI);
                    update.Start();
                }
                else
                    getRMethod(ivsLower, ivsUpper);
            }
            else if (searchNumber == 1)
            {
                if (galesCheck.Checked == true)
                    getGalesMethod(ivsLower, ivsUpper);
                else
                    getMethod(ivsLower, ivsUpper);
            }
            else
                getChannelMethod(ivsLower, ivsUpper);
        }
        #endregion

        #region Gales Search
        private void getGalesMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            int natureLockIndex = getNatureLock();
            natureLock = new NatureLock(natureLockIndex);
            shadow = natureLock.getType();

            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 16384)
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateGales(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
            else
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateGales(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
            
        }

        #region First search method
        private void generateGales(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint ability = getAbility();
            uint gender = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedGales(a, b, c, d, e, f, ability, gender);
                            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeedGales(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender)
        {
            uint x8 = hp | (atk << 5) | (def << 10);
            uint x8_2 = x8 ^ 0x8000;
            uint ex8 = spe | (spa << 5) | (spd << 10);
            uint ex8_2 = ex8 ^ 0x8000;
            uint ivs_1b = x8 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b | cnt;
                uint ivs_2 = forwardXD(seedb) >> 16;
                if (ivs_2 == ex8  || ivs_2 == ex8_2)
                {
                    uint pid1 = forwardXD(forwardXD(forwardXD(seedb)));
                    uint pid2 = forwardXD(pid1);
                    uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                    uint nature = pid - 25 * (pid / 25);

                    if (natureList == null || natureList.Contains(nature))
                    {
                        uint coloSeed = reverseXD(seedb);
                        uint xorSeed = coloSeed ^ 0x80000000;
                        switch (shadow)
                        {
                            //No NL
                            case 0:
                                filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed, 0);
                                break;
                            //First shadow
                            case 1:
                                bool cont = natureLock.method1FirstShadow(coloSeed);
                                if (cont)
                                {
                                    filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed, 0);
                                }
                                else
                                {
                                    cont = natureLock.method1FirstShadow(xorSeed);
                                    if (cont)
                                    {
                                        pid ^= 0x80008000;
                                        nature = pid - 25 * (pid / 25);
                                        filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, xorSeed, 0);
                                    }
                                }
                                break;
                            //Second shadow
                            case 6:
                                cont = natureLock.method1SecondShadowSet(coloSeed);
                                if (cont)
                                {
                                    filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed, 1);
                                }
                                else
                                {
                                    cont = natureLock.method1SecondShadowSet(xorSeed);
                                    if (cont)
                                    {
                                        pid ^= 0x80008000;
                                        nature = pid - 25 * (pid / 25);
                                        filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, xorSeed, 1);
                                    }
                                }

                                if (cont)
                                    continue;

                                cont = natureLock.method1SecondShadowUnset(coloSeed);
                                if (cont)
                                {
                                    filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed, 2);
                                }
                                else
                                {
                                    cont = natureLock.method1SecondShadowUnset(xorSeed);
                                    if (cont)
                                    {
                                        pid ^= 0x80008000;
                                        nature = pid - 25 * (pid / 25);
                                        filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, xorSeed, 2);
                                    }
                                }

                                if (cont)
                                    continue;

                                cont = natureLock.method1SecondShadowShinySkip(coloSeed);
                                if (cont)
                                {
                                    filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed, 3);
                                }
                                else
                                {
                                    cont = natureLock.method1SecondShadowShinySkip(xorSeed);
                                    if (cont)
                                    {
                                        pid ^= 0x80008000;
                                        nature = pid - 25 * (pid / 25);
                                        filterSeedGales(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, xorSeed, 3);
                                    }
                                }
                                break; 
                        }
                    }
                }
            }
        }

        private void filterSeedGales(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender, uint pid, uint nature, uint seed, int num)
        {
            String shiny = "";
            if (getNatureLock() == 41)
                if (Shiny_Check.Checked)
                {
                    if (!isShiny(pid))
                        return;
                    shiny = "!!!";
                }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null)
                if (!hiddenPowerList.Contains(actualHP))
                    return;

            if (ability != 0)
            {
                if ((pid & 1) != (ability - 1))
                    return;
            }
            ability = pid & 1;

            switch(gender)
            {
                case 1:
                    if ((pid & 255) < 127)
                        return;
                    break;
                case 2:
                    if ((pid & 255) > 126)
                        return;
                    break;
                case 3:
                    if ((pid & 255) < 191)
                        return;
                    break;
                case 4:
                    if ((pid & 255) > 190)
                        return;
                    break;
                case 5:
                    if ((pid & 255) < 64)
                        return;
                    break;
                case 6:
                    if ((pid & 255) > 63)
                        return;
                    break;
                case 7:
                    if ((pid & 255) < 31)
                        return;
                    break;
                case 8:
                    if ((pid & 255) > 30)
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
                uint pid2 = forwardXD(forwardXD(seed));
                uint pid1 = forwardXD(pid2);
                int tsv = (int)((pid2 >> 16) ^ (pid1 >> 16)) >> 3;
                reason = reason + " (TSV: " + tsv + ")";
            }
            if (seedList != null)
                seedList.Add(seed);
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, reason);
        }
        #endregion

        #region Second search method
        private void generateGales2(uint[] ivsLower, uint[] ivsUpper)
        {
            seedList = new List<uint>(); 
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            uint ability = getAbility();
            uint gender = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange + 1500);
                    for (uint n = 0; n < srange; n++)
                    {  
                        for (uint sisterSeed = 0; sisterSeed < 2; sisterSeed++)
                        {
                            uint seed = sisterSeed == 0 ? slist[(int)n] : slist[(int)n] ^ 0x80000000;
                            /*if (natureLock[0] == 1)
                            {
                                int forward = method2SingleNL(seed, n, sisterSeed);
                                uint tempSeed = sisterSeed == 0 ? slist[(int)(n + forward)] : slist[(int)(n + forward)] ^ 0x80000000;
                                if (!seedList.Contains(tempSeed))
                                {
                                    uint[] ivs = calcIVs(ivsLower, ivsUpper, (uint)(n + forward));
                                    if (ivs != null)
                                    {
                                        uint pid = pidChk((uint)(n + forward), sisterSeed);
                                        uint nature = pid - 25 * (pid / 25);
                                        if (natureList == null || natureList.Contains(nature))
                                            filterSeedGales(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, tempSeed, 0);
                                    }
                                }
                            }
                            else
                            {
                                if (natureLock[1] == 1)
                                {
                                    int forward = method2MultiNL(seed, n, sisterSeed);
                                    forward += 7;
                                    uint tempSeed = sisterSeed == 0 ? slist[(int)(n + forward)] : slist[(int)(n + forward)] ^ 0x80000000;
                                    if (!seedList.Contains(tempSeed))
                                    {
                                        uint[] ivs = calcIVs(ivsLower, ivsUpper, (uint)(n + forward));
                                        if (ivs != null)
                                        {
                                            uint pid = pidChk((uint)(n + forward), sisterSeed);
                                            uint nature = pid - 25 * (pid / 25);
                                            if (natureList == null || natureList.Contains(nature))
                                                filterSeedGales(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, tempSeed, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    int forward = 0; //method2MultiNL(seed, n, sisterSeed);
                                    foreach (int secondShadowNum in secondShadow)
                                    {
                                        uint pid;
                                        int shinySkipCount = 0;
                                        if (secondShadowNum == 1)
                                        {
                                            forward += 5;
                                            pid = pidChk((uint)(n + forward), sisterSeed);
                                        }
                                        else if (secondShadowNum == 2)
                                        {
                                            forward += 7;
                                            pid = pidChk((uint)(n + forward), sisterSeed);
                                        }
                                        else
                                        {
                                            forward += 7;
                                            pid = pidChk((uint)(n + forward), sisterSeed);
                                            uint tsv = ((pid >> 16) ^ (pid & 0xFFFF)) >> 3;
                                            bool shinySkipFlag = true;
                                            while(shinySkipFlag)
                                            {
                                                shinySkipCount += 2;
                                                pid = pidChk((uint)(n + forward + shinySkipCount), sisterSeed);
                                                uint temptsv = ((pid >> 16) ^ (pid & 0xFFFF)) >> 3;
                                                if (temptsv != tsv)
                                                    shinySkipFlag = false;
                                                else
                                                    tsv = temptsv;
                                            }
                                        }
                                        uint tempSeed = sisterSeed == 0 ? slist[(int)(n + forward)] : slist[(int)(n + forward)] ^ 0x80000000;
                                        if (!seedList.Contains(tempSeed))
                                        {
                                            uint[] ivs = calcIVs(ivsLower, ivsUpper, (uint)(n + forward + shinySkipCount));
                                            if (ivs != null)
                                            {
                                                uint nature = pid == 0 ? 0 : pid - 25 * (pid / 25);
                                                if (natureList == null || natureList.Contains(nature))
                                                    filterSeedGales(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, tempSeed, secondShadowNum);
                                            }
                                        }
                                    }
                                }
                            }*/
                        }
                    }
                    refresh = true;
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion
        #endregion

        #region Colo search
        private void getMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 84095)
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generate2(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
            else
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generate(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
        }

        #region First search method
        private void generate(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint ability = getAbility();
            uint gender = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeed(a, b, c, d, e, f, ability, gender);
                            }
            
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        //Credit to RNG Reporter for this
        private void checkSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender)
        {
            uint x8 = hp + (atk << 5) + (def << 10);
            uint x8_2 = x8 ^ 0x8000;
            uint ex8 = spe + (spa << 5) + (spd << 10);
            uint ex8_2 = ex8 ^ 0x8000;
            uint ivs_1b = x8 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b | cnt;
                uint ivs_2 = forwardXD(seedb) >> 16;
                if (ivs_2 == ex8 || ivs_2 == ex8_2)
                {
                    uint pid1 = forwardXD(forwardXD(forwardXD(seedb)));
                    uint pid2 = forwardXD(pid1);
                    uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                    uint nature = pid - 25 * (pid / 25);
                    if (natureList == null || natureList.Contains(nature))
                    {
                        uint coloSeed = reverseXD(seedb);
                        filterSeed(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed);
                    }

                    pid ^= 0x80008000;
                    nature = pid - 25 * (pid / 25);
                    if (natureList == null || natureList.Contains(nature))
                    {
                        uint coloSeed = reverseXD(seedb) ^ 0x80000000;
                        filterSeed(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, coloSeed);
                    }
                }
                
            }
        }

        private void filterSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender, uint pid, uint nature, uint seed)
        {
            String shiny = "";
            if (!galesFlag)
                if (Shiny_Check.Checked == true)
                {
                    if (!isShiny(pid))
                        return;
                    shiny = "!!!";
                }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null)
                if (!hiddenPowerList.Contains(actualHP))
                    return;

            if (ability != 0)
                if ((pid & 1) != (ability - 1))
                    return;
            ability = pid & 1;

            switch (gender)
            {
                case 1:
                    if ((pid & 255) < 127)
                        return;
                    break;
                case 2:
                    if ((pid & 255) > 126)
                        return;
                    break;
                case 3:
                    if ((pid & 255) < 191)
                        return;
                    break;
                case 4:
                    if ((pid & 255) > 190)
                        return;
                    break;
                case 5:
                    if ((pid & 255) < 64)
                        return;
                    break;
                case 6:
                    if ((pid & 255) > 63)
                        return;
                    break;
                case 7:
                    if ((pid & 255) < 31)
                        return;
                    break;
                case 8:
                    if ((pid & 255) > 30)
                        return;
                    break;
            }

            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, "");
        }
        #endregion

        #region Second search method
        //Credits to Zari for this
        private void generate2(uint[] ivsLower, uint[] ivsUpper)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            uint ability = getAbility();
            uint gender = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVs(ivsLower, ivsUpper, n);
                        if (ivs != null)
                        {
                            uint pid = pidChk(n, 0);
                            uint nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, slist[(int)n]);

                            pid = pidChk(n, 1);
                            nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, slist[(int)n] ^ 0x8000000);
                        }
                    }
                    refresh = true;
                    s = slist[(int)srange];
                    slist.Clear();
                    rlist.Clear();
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private uint populateRNG(uint seed)
        {
            seed = forwardXD(seed);
            slist.Add(seed);
            rlist.Add((seed >> 16));
            return seed;
        }

        private void populate(uint seed, uint srange)
        {
            uint s = seed;
            for (uint x = 0; x < (srange + 12); x++)
                s = populateRNG(s);
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

        private uint pidChk(uint frame, uint xor_val)
        {
            uint pid = (rlist[(int)(frame + 4)] << 16) | rlist[(int)(frame + 5)];
            if (xor_val == 1)
                pid = pid ^ 0x80008000;

            return pid;
        }
        #endregion
        #endregion

        #region Channel

        private void getChannelMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 120)
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateChannel2(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
            else
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateChannel(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
        }

        #region Search 1
        private void generateChannel(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint ability = getAbility();
            uint gender = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedChannel(a, b, c, d, e, f, ability, gender);
                            }

            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void checkSeedChannel(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender)
        {
            uint x16 = spd << 27;
            uint upper = x16 | 0x7ffffff;

            while (x16 < upper)
            {
                var rng = new XdRngR(++x16);
                uint temp = rng.GetNext32BitNumber() >> 27;
                if (temp == spa)
                {
                    temp = rng.GetNext32BitNumber() >> 27;
                    if (temp == spe)
                    {
                        temp = rng.GetNext32BitNumber() >> 27;
                        if (temp == def)
                        {
                            temp = rng.GetNext32BitNumber() >> 27;
                            if (temp == atk)
                            {
                                temp = rng.GetNext32BitNumber() >> 27;
                                if (temp == hp)
                                {
                                    for (int x = 0; x < 3; x++)
                                        rng.GetNext32BitNumber();
                                    uint pid2 = rng.GetNext16BitNumber();
                                    uint pid1 = rng.GetNext16BitNumber();
                                    uint sid = rng.GetNext16BitNumber();
                                    uint pid = (pid1 << 16) | pid2;
                                    if ((pid2 > 7 ? 0 : 1) != (pid1 ^ sid ^ 40122))
                                        pid ^= 0x80000000;
                                    uint nature = pid - 25 * (pid / 25);
                                    if (natureList == null || natureList.Contains(nature))
                                    {
                                        uint seed = rng.GetNext32BitNumber();
                                        shinyval = (40122 ^ (sid)) >> 3;
                                        filterSeedChannel(hp, atk, def, spa, spd, spe, ability, gender, seed, pid, nature);
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
        private void generateChannel2(uint[] ivsLower, uint[] ivsUpper)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            uint ability = getAbility();
            uint gender = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populate(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVsChannel(ivsLower, ivsUpper, n, 0);
                        if (ivs != null)
                        {
                            uint pid = pidChkChannel(rlist[(int)(n + 2)], rlist[(int)(n + 3)], rlist[(int)n + 1]);
                            uint nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                            {
                                shinyval = (40122 ^ rlist[(int)n + 1]) >> 3;
                                filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, slist[(int)n], pid, nature);
                            }

                            ivs = calcIVsChannel(ivsLower, ivsUpper, n, 1);
                            if (ivs != null)
                            {
                                pid = pidChkChannel(rlist[(int)(n + 2)] ^ 0x8000, rlist[(int)(n + 3)] ^ 0x8000, rlist[(int)n + 1] ^ 0x8000);
                                nature = pid - 25 * (pid / 25);
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    shinyval = (40122 ^ (rlist[(int)n + 1] ^ 0x8000)) >> 3;
                                    filterSeedChannel(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, (slist[(int)n] ^ 0x80000000), pid, nature);
                                }
                            }
                        }
                    }
                    refresh = true;
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
                    return null;
            }

            return ivs;
        }

        private uint pidChkChannel(uint pid1, uint pid2, uint sid)
        {
            uint pid = pid1 << 16 | pid2;
            if ((pid2 > 7 ? 0 : 1) != (pid1 ^ sid ^ 40122))
                pid ^= 0x80000000;
            return pid;
        }

        private void filterSeedChannel(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender, uint seed, uint pid, uint nature)
        {
            String shiny = "";
            if (Shiny_Check.Checked == true)
            {
                if (!isShiny(pid))
                    return;
                shiny = "!!!";
            }

            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null)
                if (!hiddenPowerList.Contains(actualHP))
                    return;

            if (ability != 0)
                if ((pid & 1) != (ability - 1))
                    return;
            ability = pid & 1;

            switch (gender)
            {
                case 1:
                    if ((pid & 255) < 127)
                        return;
                    break;
                case 2:
                    if ((pid & 255) > 126)
                        return;
                    break;
                case 3:
                    if ((pid & 255) < 191)
                        return;
                    break;
                case 4:
                    if ((pid & 255) > 190)
                        return;
                    break;
                case 5:
                    if ((pid & 255) < 64)
                        return;
                    break;
                case 6:
                    if ((pid & 255) > 63)
                        return;
                    break;
                case 7:
                    if ((pid & 255) < 31)
                        return;
                    break;
                case 8:
                    if ((pid & 255) > 30)
                        return;
                    break;
            }
            addSeed(hp, atk, def, spa, spd, spe, nature, ability, gender, actualHP, pid, shiny, seed, "");
        }
        #endregion
        #endregion

        #region Reverse Method 1
        private void getRMethod(uint[] ivsLower, uint[] ivsUpper)
        {
            uint method = 1;

            for (int x = 0; x < 6; x++)
            {
                uint temp = ivsUpper[x] - ivsLower[x] + 1;
                method *= temp;
            }

            if (method > 76871)
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateR2(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
            else
            {
                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => generateR(ivsLower, ivsUpper));
                searchThread[0].Start();
                var update = new Thread(updateGUI);
                update.Start();
            }
        }

        #region Search 1
        private void generateR(uint[] ivsLower, uint[] ivsUpper)
        {
            isSearching = true;
            uint ability = getAbility();
            uint gender = getGender();

            for (uint a = ivsLower[0]; a <= ivsUpper[0]; a++)
                for (uint b = ivsLower[1]; b <= ivsUpper[1]; b++)
                    for (uint c = ivsLower[2]; c <= ivsUpper[2]; c++)
                        for (uint d = ivsLower[3]; d <= ivsUpper[3]; d++)
                            for (uint e = ivsLower[4]; e <= ivsUpper[4]; e++)
                            {
                                refresh = true;
                                for (uint f = ivsLower[5]; f <= ivsUpper[5]; f++)
                                    checkSeedR(a, b, c, d, e, f, ability, gender);
                            }

            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        //Credits to RNG reporter for this
        private void checkSeedR(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint ability, uint gender)
        {
            uint x4 = hp | (atk << 5) | (def << 10);
            uint x4_2 = x4 ^ 0x8000;
            uint ex4 = spe | (spa << 5) | (spd << 10);
            uint ex4_2 = ex4 ^ 0x8000;
            uint ivs_1b = x4 << 16;

            for (uint cnt = 0; cnt <= 0xFFFF; cnt++)
            {
                uint seedb = ivs_1b | cnt;
                uint ivs_2 = forward(seedb) >> 16;
                if (ivs_2 == ex4 || ivs_2 == ex4_2)
                {
                    uint pid2 = reverse(seedb);
                    uint pid1 = reverse(pid2);
                    uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                    uint nature = pid - 25 * (pid / 25);
                    uint seed = reverse(pid1);
                    if (natureList == null || natureList.Contains(nature))
                    {
                        filterSeed(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, seed);
                    }

                    pid ^= 0x80008000;
                    nature = pid - 25 * (pid / 25);
                    if (natureList == null || natureList.Contains(nature))
                    {
                        seed ^= 0x80000000;
                        filterSeed(hp, atk, def, spa, spd, spe, ability, gender, pid, nature, seed);
                    }
                }
            }
        }
        #endregion

        #region Search 2
        private void generateR2(uint[] ivsLower, uint[] ivsUpper)
        {
            uint s = 0;
            uint srange = 1048576;
            isSearching = true;

            uint ability = getAbility();
            uint gender = getGender();

            for (uint z = 0; z < 32; z++)
            {
                for (uint h = 0; h < 64; h++)
                {
                    populateR(s, srange);
                    for (uint n = 0; n < srange; n++)
                    {
                        uint[] ivs = calcIVsR(ivsLower, ivsUpper, n);
                        if (ivs != null)
                        {
                            uint pid = pidChkR(n, 0);
                            uint nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, slist[(int)(n)]);

                            pid = pidChkR(n, 1);
                            nature = pid - 25 * (pid / 25);
                            if (natureList == null || natureList.Contains(nature))
                                filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, slist[(int)(n)] ^ 0x80000000);
                        }
                    }
                    refresh = true;
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
            seed = forward(seed);
            slist.Add(seed);
            rlist.Add((seed >> 16));
            return seed;
        }

        private void populateR(uint seed, uint srange)
        {
            uint s = seed;
            for (uint x = 0; x < (srange + 10); x++)
                s = populateRNGR(s);
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

        private uint pidChkR(uint frame, uint xor_val)
        {
            uint pid = (rlist[(int)(frame + 1)] << 16) | rlist[(int)(frame + 2)];
            if (xor_val == 1)
                pid = pid ^ 0x80008000;

            return pid;
        }
        #endregion
        #endregion

        #region Wishmkr
        private void generateWishmkr(uint[] IVsLower, uint[] IVsUpper)
        {
            isSearching = true;
            shinyval = 2505;
            uint ability = getAbility();
            uint gender = getGender();

            for (uint x = 0; x <= 0xFFFF; x++)
            {
                uint pid1 = forward(x);
                uint pid2 = forward(pid1);
                uint pid = (pid1 & 0xFFFF0000) | (pid2 >> 16);
                uint nature = pid - 25 * (pid / 25);

                if (natureList == null || natureList.Contains(nature))
                {
                    uint ivs1 = forward(pid2);
                    uint ivs2 = forward(ivs1);
                    ivs1 >>= 16;
                    ivs2 >>= 16;
                    uint[] ivs = createIVsR(ivs1, ivs2, IVsLower, IVsUpper);
                    if (ivs != null)
                        filterSeed(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], ability, gender, pid, nature, x);
                }
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }
        #endregion

        private void addSeed(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, uint nature, uint ability, uint gender, uint hP, uint pid, String shiny, uint seed, String output)
        {
            String stringNature = Natures[nature];
            String hPString = hiddenPowers[calcHP(hp, atk, def, spa, spd, spe)];
            int hpPower = calcHPPower(hp, atk, def, spa, spd, spe);
            gender = pid & 255;
            char gender1;
            char gender2;
            char gender3;
            char gender4;

            if (!galesFlag)
                if (shiny == "")
                    if (isShiny(pid))
                        shiny = "!!!";

            if (galesFlag && output.Equals(""))
                output = "Pass NL";

            gender1 = gender < 31 ? 'F' : 'M';
            gender2 = gender < 64 ? 'F' : 'M';
            gender3 = gender < 126 ? 'F' : 'M';
            gender4 = gender < 191 ? 'F' : 'M';

            displayList.Insert(0, new DisplayList
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
                Three_Fourths = gender4,
                Reason = output
            });
        }
        #endregion

        #region Shadow Checker

        private void generateShadow_Click(object sender, EventArgs e)
        {
            getIVsShadow(out uint[] ivsLower, out uint[] ivsUpper);
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
                dataGridShadow.Rows.Clear();
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
                uint gender = (uint)comboBoxGenderShadow.SelectedIndex;
                uint ability = (uint)comboBoxAbilityShadow.SelectedIndex;
                natureLock = new NatureLock(comboBoxShadow.SelectedIndex);
                shadow = natureLock.getType();

                shadowDisplay = new List<ShadowDisplay>();
                binding = new BindingSource { DataSource = shadowDisplay };
                dataGridShadow.DataSource = binding;
                status.Text = "Searching";

                searchThread = new Thread[1];
                searchThread[0] = new Thread(() => shadowSearch(ivsLower, ivsUpper, initialFrame, maxFrame, seed, shadowMethod, gender, ability));
                searchThread[0].Start();
            }
        }

        private void shadowSearch(uint[] ivsLower, uint[] ivsUpper, uint initialFrame, uint maxFrame, uint inSeed, int secondMethod, uint gender, uint ability)
        {
            var rng = new XdRng(inSeed);
            var hunt = new XdRng(0);

            for (uint cnt = 1; cnt < initialFrame; cnt++)
                rng.GetNext32BitNumber();

            uint pid, iv1, iv2, nature;
            uint[] ivs;

            switch(shadow)
            {
                //No NL
                case 0:

                    List<uint> rand = new List<uint>();

                    for (int x = 0; x < 5; x++)
                        rand.Add(rng.GetNext16BitNumber());

                    for (uint cnt = 0; cnt < maxFrame; cnt++, rand.RemoveAt(0), rand.Add(rng.GetNext16BitNumber()))
                    {
                        pid = (rand[3] << 16) | rand[4];
                        nature = pid - 25 * (pid / 25);
                        if (natureList == null || natureList.Contains(nature))
                        {
                            ivs = createIVs(rand[0], rand[1], ivsLower, ivsUpper);
                            if (ivs != null)
                                filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(initialFrame + cnt), nature, pid, gender, ability);
                        }
                    }

                    break;
                //First shadow
                case 1:
                    natureLock.rand.Add(rng.Seed);
                    for (int x = 0; x < 2000; x++)
                        natureLock.rand.Add(rng.GetNext32BitNumber());

                    for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                    {
                        natureLock.method2FirstShadow(out pid, out iv1, out iv2);

                        nature = pid - 25 * (pid / 25);
                        if (natureList == null || natureList.Contains(nature))
                        {
                            ivs = createIVs(iv1, iv2, ivsLower, ivsUpper);
                            if (ivs != null)
                                filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid, gender, ability);
                        }
                    }
                    break;
                //Second shadow
                case 6:
                    switch(secondMethod)
                    {
                        //Set
                        case 0:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2000; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.method2SecondShadowSet(out pid, out iv1, out iv2);

                                nature = pid - 25 * (pid / 25);
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2, ivsLower, ivsUpper);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid, gender, ability);
                                }
                            }
                            break;
                        //Unset
                        case 1:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2000; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.method2SecondShadowUnset(out pid, out iv1, out iv2);

                                nature = pid - 25 * (pid / 25);
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2, ivsLower, ivsUpper);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid, gender, ability);
                                }
                            }
                            break;
                        //Shiny skip
                        case 2:
                            natureLock.rand.Add(rng.Seed);
                            for (int x = 0; x < 2000; x++)
                                natureLock.rand.Add(rng.GetNext32BitNumber());

                            for (uint cnt = 0; cnt < maxFrame; cnt++, natureLock.rand.RemoveAt(0), natureLock.rand.Add(rng.GetNext32BitNumber()))
                            {
                                natureLock.method2SecondShinySkip(out pid, out iv1, out iv2);

                                nature = pid - 25 * (pid / 25);
                                if (natureList == null || natureList.Contains(nature))
                                {
                                    ivs = createIVs(iv1, iv2, ivsLower, ivsUpper);
                                    if (ivs != null)
                                        filterSeedShadow(ivs[0], ivs[1], ivs[2], ivs[3], ivs[4], ivs[5], (int)(cnt + initialFrame), nature, pid, gender, ability);
                                }
                            }
                            break;
                    }
                    break;
            }
            isSearching = false;
            dataGridShadow.Invoke((MethodInvoker)(() => dataGridShadow.Refresh()));
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        public void filterSeedShadow(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int frame, uint nature, uint pid, uint gender, uint ability)
        {
            uint actualHP = calcHP(hp, atk, def, spa, spd, spe);
            if (hiddenPowerList != null)
                if (!hiddenPowerList.Contains(actualHP))
                    return;

            if (ability != 0)
                if ((pid & 1) != (ability - 1))
                    return;
            ability = pid & 1;

            switch (gender)
            {
                case 1:
                    if ((pid & 255) < 127)
                        return;
                    break;
                case 2:
                    if ((pid & 255) > 126)
                        return;
                    break;
                case 3:
                    if ((pid & 255) < 191)
                        return;
                    break;
                case 4:
                    if ((pid & 255) > 190)
                        return;
                    break;
                case 5:
                    if ((pid & 255) < 64)
                        return;
                    break;
                case 6:
                    if ((pid & 255) > 63)
                        return;
                    break;
                case 7:
                    if ((pid & 255) < 31)
                        return;
                    break;
                case 8:
                    if ((pid & 255) > 30)
                        return;
                    break;
            }
            addSeedShadow(hp, atk, def, spa, spd, spe, frame, nature, ability, gender, actualHP, pid);
        }

        private void addSeedShadow(uint hp, uint atk, uint def, uint spa, uint spd, uint spe, int frame, uint nature, uint ability, uint gender, uint hP, uint pid)
        {
            String stringNature = Natures[nature];
            String hPString = hiddenPowers[calcHP(hp, atk, def, spa, spd, spe)];
            int hpPower = calcHPPower(hp, atk, def, spa, spd, spe);
            gender = pid & 255;
            char gender1;
            char gender2;
            char gender3;
            char gender4;

            gender1 = gender < 31 ? 'F' : 'M';
            gender2 = gender < 64 ? 'F' : 'M';
            gender3 = gender < 126 ? 'F' : 'M';
            gender4 = gender < 191 ? 'F' : 'M';

            shadowDisplay.Add(new ShadowDisplay
            {
                Frame = frame,
                PID = pid.ToString("x").ToUpper(),
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

        private int getNatureLock()
        {
            if (shadowPokemon.InvokeRequired)
                return (int)shadowPokemon.Invoke(new Func<int>(getNatureLock));
            else
                return (int)shadowPokemon.SelectedIndex;
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

        private uint getSearchMethod()
        {
            if (searchMethod.InvokeRequired)
                return (uint)searchMethod.Invoke(new Func<uint>(getSearchMethod));
            else
                return (uint)searchMethod.SelectedIndex;
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

        private bool isShiny(uint PID)
        {
            return (((PID >> 16) ^ (PID & 0xffff)) >> 3) == shinyval;
        }

        private uint calcHP(uint hp, uint atk, uint def, uint spa, uint spd, uint spe)
        {
            return ((((hp & 1) + 2 * (atk & 1) + 4 * (def & 1) + 8 * (spe & 1) + 16 * (spa & 1) + 32 * (spd & 1)) * 15) / 63);
        }
        #endregion

        #region GUI code
        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = dataGridViewResult.AutoResizeColumns;
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
                    if (searchThread == null || !isSearching)
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
            binding.ResetBindings(true);
        }

        private String[] addHP()
        {
            String[] temp = new String[]
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

            return temp;
        }

        private String[] addShadowMethod()
        {
            String[] temp = new String[]
                {
                    "Set",
                    "Unset",
                    "Shiny Skip"
                };

            return temp;
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

        private void anyShadowMethod_Click(object sender, EventArgs e)
        {
            comboBoxShadowMethod.ClearSelection();
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
        private void setComboBox()
        {
            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = true;
            comboBoxHiddenPower.CheckBoxItems[0].Checked = false;
            comboBoxShadowMethod.CheckBoxItems[0].Checked = true;
            comboBoxShadowMethod.CheckBoxItems[0].Checked = false;
            checkBoxNatureShadow.CheckBoxItems[0].Checked = true;
            checkBoxNatureShadow.CheckBoxItems[0].Checked = false;
            checkBoxHPShadow.CheckBoxItems[0].Checked = true;
            checkBoxHPShadow.CheckBoxItems[0].Checked = false;
        }

        private void galesCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (galesCheck.Checked == true)
            {
                List<int> secondShadows = new List<int> { 0, 6, 8, 10, 21, 30, 32, 37, 50, 57, 66, 67, 75, 93 };
                if (secondShadows.Contains(shadowPokemon.SelectedIndex))
                {
                    //shadowMethodLabel.Visible = true;
                    //comboBoxShadowMethod.Visible = true;
                    //anyShadowMethod.Visible = true;
                }
                else if (shadowPokemon.SelectedIndex == 41)
                {
                    shadowMethodLabel.Visible = false;
                    comboBoxShadowMethod.Visible = false;
                    anyShadowMethod.Visible = false;
                    Shiny_Check.Visible = true;
                }
                else
                {
                    shadowMethodLabel.Visible = false;
                    comboBoxShadowMethod.Visible = false;
                    anyShadowMethod.Visible = false;
                    Shiny_Check.Visible = false;
                }
            }
            else
            {
                shadowMethodLabel.Visible = false;
                comboBoxShadowMethod.Visible = false;
                anyShadowMethod.Visible = false;
                Shiny_Check.Visible = true;
            }
        }

        private void shadowPokemon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (galesCheck.Checked)
            {
                List<int> secondShadows = new List<int> { 0, 6, 8, 10, 21, 30, 32, 37, 50, 57, 66, 67, 75, 93 };
                if (secondShadows.Contains(shadowPokemon.SelectedIndex))
                {
                    //shadowMethodLabel.Visible = true;
                    //comboBoxShadowMethod.Visible = true;
                    //anyShadowMethod.Visible = true;
                }
                else if (shadowPokemon.SelectedIndex == 41)
                {
                    shadowMethodLabel.Visible = false;
                    comboBoxShadowMethod.Visible = false;
                    anyShadowMethod.Visible = false;
                    Shiny_Check.Visible = true;
                }
                else
                {
                    shadowMethodLabel.Visible = false;
                    comboBoxShadowMethod.Visible = false;
                    anyShadowMethod.Visible = false;
                    Shiny_Check.Visible = false;
                }
            }
        }

        private void searchMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int method = searchMethod.SelectedIndex;
            if (method == 0)
            {
                wshMkr.Visible = true;
                Shiny_Check.Visible = true;
                shadowMethodLabel.Visible = false;
                comboBoxShadowMethod.Visible = false;
                anyShadowMethod.Visible = false;
                shadowPokemon.Visible = false;
                galesCheck.Visible = false;
            }
            else if (method == 1)
            {
                wshMkr.Visible = false;
                Shiny_Check.Visible = true;
                if (galesCheck.Checked)
                {
                    List<int> secondShadows = new List<int> { 0, 6, 8, 10, 21, 30, 32, 37, 50, 57, 66, 67, 75, 93 };
                    Shiny_Check.Visible = false;
                    if (secondShadows.Contains(shadowPokemon.SelectedIndex))
                    {
                        //shadowMethodLabel.Visible = true;
                        //comboBoxShadowMethod.Visible = true;
                        //anyShadowMethod.Visible = true;
                    }
                    else if (shadowPokemon.SelectedIndex == 41)
                    {
                        shadowMethodLabel.Visible = false;
                        comboBoxShadowMethod.Visible = false;
                        anyShadowMethod.Visible = false;
                        Shiny_Check.Visible = true;
                    }
                    else
                    {
                        shadowMethodLabel.Visible = false;
                        comboBoxShadowMethod.Visible = false;
                        anyShadowMethod.Visible = false;
                        Shiny_Check.Visible = false;
                    }
                }
                shadowPokemon.Visible = true;
                galesCheck.Visible = true;
            }
            else
            {
                wshMkr.Visible = false;
                Shiny_Check.Visible = true;
                shadowMethodLabel.Visible = false;
                comboBoxShadowMethod.Visible = false;
                anyShadowMethod.Visible = false;
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