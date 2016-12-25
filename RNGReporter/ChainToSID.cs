using System;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class ChainToSID : Form
    {
        private CalculateChainSid calculate;
        private uint returnSid;

        private bool sidSet;

        public ChainToSID()
        {
            InitializeComponent();
        }

        public uint DefaultId { get; set; }

        public bool SidSet
        {
            get { return sidSet; }
            set { sidSet = value; }
        }

        public uint ReturnSid
        {
            get { return returnSid; }
            set { returnSid = value; }
        }

        private void ChainToSID_Load(object sender, EventArgs e)
        {
            dataGridViewValues.AutoGenerateColumns = false;

            comboBoxAbility.SelectedIndex = 0;

            comboBoxGender.DisplayMember = "Name";
            comboBoxGender.ValueMember = "Index";

            comboBoxGender.DataSource = new BindingSource(GenderGenderRatio.GenderGenderRatioCollection(), null);
            comboBoxGender.SelectedIndex = 0;

            comboBoxNature.DisplayMember = "Key";
            comboBoxNature.ValueMember = "Value";

            comboBoxNature.DataSource = new BindingSource(Nature.NatureCollection(), null);
            comboBoxNature.SelectedIndex = 0;

            maskedTextBoxID.Text = DefaultId.ToString();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            uint id = 0;

            if (maskedTextBoxID.Text != "")
            {
                id = uint.Parse(maskedTextBoxID.Text);
            }

            if (calculate == null)
            {
                //  We consider the ID locked in at this
                //  point so we are going to disable the
                //  textbox.
                calculate = new CalculateChainSid(id);
                maskedTextBoxID.Enabled = false;
            }

            //
            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            if (maskedTextBoxHP.Text != "")
                hp = uint.Parse(maskedTextBoxHP.Text);
            if (maskedTextBoxAtk.Text != "")
                atk = uint.Parse(maskedTextBoxAtk.Text);
            if (maskedTextBoxDef.Text != "")
                def = uint.Parse(maskedTextBoxDef.Text);
            if (maskedTextBoxSpA.Text != "")
                spa = uint.Parse(maskedTextBoxSpA.Text);
            if (maskedTextBoxSpD.Text != "")
                spd = uint.Parse(maskedTextBoxSpD.Text);
            if (maskedTextBoxSpe.Text != "")
                spe = uint.Parse(maskedTextBoxSpe.Text);

            //  Get Nature
            var nature = (Nature) comboBoxNature.SelectedValue;

            //  Get Gender -- Need to look for a better way to do this, would
            //  like to actually be able to directly store and get a reference
            //  in the combo box.
            GenderGenderRatio genderGenderRato =
                GenderGenderRatio.GenderGenderRatioCollection()[comboBoxGender.SelectedIndex];

            //  Get Ability
            string ability = comboBoxAbility.SelectedItem.ToString();

            calculate.Add(hp, atk, def, spa, spd, spe, nature, ability, genderGenderRato);

            //  display the new information here, checking to see if
            //  we have narrowed it down to a single sid and displaying
            //  something special in that case.
            if (calculate.CandidateSids.Count == 1)
            {
                labelInfo.Text = "SID Found - " + calculate.CandidateSids[0];

                labelSid.Text = calculate.CandidateSids[0].ToString();
                returnSid = calculate.CandidateSids[0];
                sidSet = true;
            }
            else
            {
                labelInfo.Text = calculate.CandidateSids.Count.ToString();
            }

            //  Rebind to the datagrid to show our current list of
            //  monsters that we have processed here.
            dataGridViewValues.DataSource = null;
            dataGridViewValues.DataSource = calculate.Pokemon;

            //  Clear all of the boxes that we want the user to 
            //  re-enter things into for this run and reset all
            //  of the dropdowns
            maskedTextBoxHP.Text = "";
            maskedTextBoxAtk.Text = "";
            maskedTextBoxDef.Text = "";
            maskedTextBoxSpA.Text = "";
            maskedTextBoxSpD.Text = "";
            maskedTextBoxSpe.Text = "";

            comboBoxAbility.SelectedIndex = 0;
            comboBoxGender.SelectedIndex = 0;
            comboBoxNature.SelectedIndex = 0;

            //  Focus the first control
            comboBoxNature.Focus();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //  So we will have to create a new calculation
            calculate = null;

            //  Open the ID box back up so the user may enter
            //  something new here.
            maskedTextBoxID.Enabled = true;

            //  Clear out the grid now since the information 
            //  id old at this point.
            dataGridViewValues.DataSource = null;

            labelInfo.Text = "8192";
            labelSid.Text = "";
        }
    }
}