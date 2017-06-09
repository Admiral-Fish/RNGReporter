using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace RNGReporter.Objects
{
    public partial class PokeSpot : Form
    {
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private List<PokeSpotDisplay> displayList;
        private List<uint> natureList, spotList;
        private uint[] rngList;
        private uint genderFilter, abilityFilter, shinyNum, shinyval;
        private int j;
        private bool shinyCheck;

        public PokeSpot(int TID, int SID)
        {
            InitializeComponent();
            abilityType.SelectedIndex = 0;
            genderType.SelectedIndex = 0;
            id.Text = TID.ToString();
            sid.Text = SID.ToString();
            dataGridViewResult.AutoGenerateColumns = false;
            rngList = new uint[6];
        }

        private void PokeSpot_Load(object sender, EventArgs e)
        {
            comboBoxNature.Items.AddRange(Nature.NatureDropDownCollectionSearchNatures());
            comboBoxSpotType.Items.AddRange(addSpotTypes());
            setComboBox();
        }

        private void setComboBox()
        {
            comboBoxNature.CheckBoxItems[0].Checked = true;
            comboBoxNature.CheckBoxItems[0].Checked = false;
            comboBoxSpotType.CheckBoxItems[0].Checked = true;
            comboBoxSpotType.CheckBoxItems[0].Checked = false;
        }

        private void search_Click(object sender, EventArgs e)
        {
            displayList = new List<PokeSpotDisplay>();
            try
            {
                shinyval = (uint.Parse(id.Text) ^ uint.Parse(sid.Text)) >> 3;
            }
            catch
            {
                shinyval = 0;
            }

            uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint seed);
            uint frame = 3000000;
            uint.TryParse(maxFrame.Text, out frame);

            natureList = null;
            if (comboBoxNature.Text != "Any" && comboBoxNature.CheckBoxItems.Count > 0)
                natureList = (from t in comboBoxNature.CheckBoxItems where t.Checked select (uint)((Nature)t.ComboBoxItem).Number).ToList();

            spotList = null;
            List<uint> temp = new List<uint>();
            if (comboBoxSpotType.Text != "Any" && comboBoxSpotType.CheckBoxItems.Count > 0)
                for (int x = 1; x < 5; x++)
                    if (comboBoxSpotType.CheckBoxItems[x].Checked)
                        temp.Add((uint)(x - 1));

            if (temp.Count != 0)
                spotList = temp;
            else
                spotList = new List<uint> { 0, 1, 2, 3 };

            genderFilter = (uint)genderType.SelectedIndex;
            abilityFilter = (uint)abilityType.SelectedIndex;
            shinyCheck = Shiny_Check.Checked;

            searchPokeSpot(seed, frame);
            dataGridViewResult.DataSource = displayList;
            dataGridViewResult.AutoResizeColumns();
        }

        private void searchPokeSpot(uint seed, uint frame)
        {
            var rng = new XdRng(seed);
            rngList[0] = seed;

            for (int x = 1; x < 6; x++)
                rngList[x] = rng.GetNext32BitNumber();

            j = 5;

            for (uint x = 1; x < frame + 1; x++, rngList[j] = rng.GetNext32BitNumber())
            {
                if (++j > 5)
                    j = 0;
                filterPokeSpot(x);
            }
        }

        private void filterPokeSpot(uint frame)
        {
            uint pid1 = rngList[j >= 2 ? j - 2 : j + 4] >> 16;
            uint pid2 = rngList[j >= 1 ? j - 1 : j + 5] >> 16;
            uint pid = (pid1 << 16) | pid2;

            uint nature = pid - 25 * (pid / 25);
            if (natureList != null && !natureList.Contains(nature))
                return;

            shinyNum = (pid1 ^ pid2) >> 3;
            String shiny = "";
            if (shinyCheck)
            {
                if (shinyNum != shinyval)
                    return;
                shiny = "!!!";
            }

            uint ability = pid & 1;
            if (abilityFilter != 0)
            {
                if (ability != (abilityFilter - 1))
                    return;
            }

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

            calcPokeSpot(pid, frame, nature, gender, ability, shiny);
        }

        private void calcPokeSpot(uint pid, uint frame, uint nature, uint gender, uint ability, String shiny)
        {
            uint call1 = rngList[j >= 5 ? j -5 : j + 1] >> 16;
            uint currentCall = call1 - 3 * (call1 / 3);

            if (currentCall == 0)
            {
                String spotType = "";
                uint call2 = rngList[j >= 4 ? j - 4 : j + 2] >> 16;
                uint call3 = rngList[j >= 3 ? j - 3 : j + 3] >> 16;

                if (shiny == "")
                {
                    if (shinyNum == shinyval)
                        shiny = "!!!";
                }

                currentCall = call3 - 100 * (call3 / 100);

                foreach (uint x in spotList)
                {
                    if (x == 0)
                    {
                        if ((call2 - 100 * (call2 / 100)) > 9)
                            if (currentCall < 50)
                                spotType = "Common";
                    }
                    else if (x == 1)
                    {
                        if ((call2 - 100 * (call2 / 100)) > 9)
                            if (currentCall > 49 && currentCall < 85)
                                spotType = "Uncommon";
                    }
                    else if (x == 2)
                    {
                        if ((call2 - 100 * (call2 / 100)) > 9)
                            if (currentCall > 84)
                                spotType = "Rare";
                    }
                    else
                    { 
                        if ((call2 - 100 * (call2 / 100)) < 10)
                            spotType = "Munchlax";
                    }
                }

                if (spotType.Equals(""))
                    return;

                String stringNature = Natures[nature];
                char gender1;
                char gender2;
                char gender3;
                char gender4;

                gender1 = gender < 31 ? 'F' : 'M';
                gender2 = gender < 64 ? 'F' : 'M';
                gender3 = gender < 126 ? 'F' : 'M';
                gender4 = gender < 191 ? 'F' : 'M';

                displayList.Add(new PokeSpotDisplay
                {
                    Seed = rngList[j].ToString("x").ToUpper(),
                    Frame = (int)frame,
                    PID = pid.ToString("x").ToUpper(),
                    Shiny = shiny,
                    Type = spotType,
                    Nature = stringNature,
                    Ability = (int)ability,
                    Eighth = gender1,
                    Quarter = gender2,
                    Half = gender3,
                    Three_Fourths = gender4
                });

            }
        }

        private void anyPokeSpot_Click(object sender, EventArgs e)
        {
            comboBoxSpotType.ClearSelection();
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

        private String[] addSpotTypes()
        {
            return new String[]
                {
                    "Common",
                    "Uncommon",
                    "Rare",
                    "Munchlax"
                };
        }
    }
}