using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace RNGReporter.Objects
{
    public partial class PokeSpot : Form
    {
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private List<PokeSpotDisplay> displayList;
        private List<uint> natureList;
        private List<String> spotList;
        private uint[] rngList;
        private uint genderFilter, abilityFilter, shinyNum, shinyval, call1, call2, call3;
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
            List<String> temp = new List<String>();
            if (comboBoxSpotType.Text != "Any" && comboBoxSpotType.CheckBoxItems.Count > 0)
                for (int x = 1; x < 4; x++)
                    if (comboBoxSpotType.CheckBoxItems[x].Checked)
                        temp.Add(comboBoxSpotType.CheckBoxItems[x].Text);

            if (temp.Count != 0)
                spotList = temp;
            else
                spotList = new List<String> { "Common", "Uncommon", "Rare" };

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

            for (uint x = 1; x <= frame; x++, rngList[j] = rng.GetNext32BitNumber())
            {
                j = ++j % 6;
                filterPokeSpot(x);
            }
        }

        private void filterPokeSpot(uint frame)
        {
            uint pid1 = rngList[j >= 2 ? j - 2 : j + 4] >> 16;
            uint pid2 = rngList[j >= 1 ? j - 1 : j + 5] >> 16;
            uint pid = (pid1 << 16) | pid2;

            uint nature = pid % 25;
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

            calcPokeSpot(pid, frame, nature, gender, ability, shiny);
        }

        private void calcPokeSpot(uint pid, uint frame, uint nature, uint gender, uint ability, String shiny)
        {
            call1 = rngList[j >= 5 ? j - 5 : j + 1] >> 16;

            if (call1 % 3 == 0)
            {
                String spotType = "";
                call2 = rngList[j >= 4 ? j - 4 : j + 2] >> 16;

                // Munchlax isn't catchable and provides a frame skip
                if (call2 % 100 < 10)
                    return;

                call3 = (rngList[j >= 3 ? j - 3 : j + 3] >> 16) % 100;
                if (call3 < 50)
                    spotType = "Common";
                else if (call3 > 49 && call3 < 85)
                    spotType = "Uncommon";
                else if (call3 > 84)
                    spotType = "Rare";

                if (!spotList.Contains(spotType))
                    return;

                displayList.Add(new PokeSpotDisplay
                {
                    Seed = rngList[j].ToString("X"),
                    Frame = (int)frame,
                    PID = pid.ToString("X"),
                    Shiny = shiny == "" ? shinyNum == shinyval ? "!!!" : shiny : shiny,
                    Type = spotType,
                    Nature = Natures[nature],
                    Ability = (int)ability,
                    Eighth = gender < 31 ? 'F' : 'M',
                    Quarter = gender < 64 ? 'F' : 'M',
                    Half = gender < 126 ? 'F' : 'M',
                    Three_Fourths = gender < 191 ? 'F' : 'M'
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
                    "Rare"
                };
        }
    }
}