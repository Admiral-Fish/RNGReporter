using RNGReporter.Objects;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class IVstoFrame : Form
    {
        public IVstoFrame()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Search();
        }

        private void _Search()
        {
            label1.Focus();

            if (maskedTextBox1.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == ""
                || maskedTextBox2.Text == "" || setIVs1._MaskedTextBox1.Text == "" || setIVs2._MaskedTextBox1.Text == ""
                || setIVs3._MaskedTextBox1.Text == "" || setIVs4._MaskedTextBox1.Text == "" || setIVs5._MaskedTextBox1.Text == ""
                || _MaskedTextBox1.Text == "")
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                uint hp = uint.Parse(setIVs1._MaskedTextBox1.Text);
                uint atk = uint.Parse(setIVs2._MaskedTextBox1.Text);
                uint def = uint.Parse(setIVs3._MaskedTextBox1.Text);
                uint spa = uint.Parse(setIVs4._MaskedTextBox1.Text);
                uint spd = uint.Parse(setIVs5._MaskedTextBox1.Text);
                uint spe = uint.Parse(_MaskedTextBox1.Text);

                uint initseed = uint.Parse(maskedTextBox1.Text, NumberStyles.HexNumber);
                uint pid = Convert.ToUInt32(maskedTextBox2.Text, 16);
                uint minframes = uint.Parse(maskedTextBox3.Text);
                uint maxframes = uint.Parse(maskedTextBox4.Text);

                uint ivs = hp + (atk << 5) + (def << 10) + (spe << 16) + (spa << 21) + (spd << 26);

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                int method = comboBox1.SelectedIndex;

                if (method == 0)
                {
                    Search(pid, ivs, 2, 3, initseed, minframes, maxframes);
                }
                else if (method == 1)
                {
                    Search(pid, ivs, 3, 4, initseed, minframes, maxframes);
                }
                else if (method == 2)
                {
                    Search(pid, ivs, 2, 4, initseed, minframes, maxframes);
                }
                else
                { SearchGales(pid, ivs, initseed, minframes, maxframes); }
            }
        }

        private void Search(uint pid, uint ivs, int num1, int num2, uint initseed, uint minframes, uint maxframes)
        {
            uint pidl = pid & 0xFFFF;
            uint pidh = pid >> 16;
            uint ivsh = ivs >> 16;
            uint ivsl = ivs & 0xFFFF;
            uint[] seeds = new uint[5];
            var rng = new PokeRng(initseed);

            rng.GetNext32BitNumber((int)minframes);

            for (int x = 0; x < 5; x++)
                seeds[x] = rng.GetNext16BitNumber();

            int j = 4;
            int k;

            for (uint i = 1 + minframes; i < maxframes; i++, seeds[j] = rng.GetNext16BitNumber())
            {
                if (++j > 4)
                    j = 0;

                if (seeds[j] != pidl)
                    continue;

                if (seeds[j >= 4 ? 0 : j + 1] != pidh)
                    continue;

                k = j + num1;
                if (k > 4)
                    k -= 5;

                if ((seeds[k] & 0x7FFF) != ivsl)
                    continue;

                k = j + num2;
                if (k > 4)
                    k -= 5;

                if ((seeds[k] & 0x7FFF) != ivsh)
                    continue;

                string[] row = new string[] { i.ToString() };
                dataGridView1.Rows.Add(row);
                dataGridView1.Refresh();
                break;
            }
        }

        private void SearchGales(uint pid, uint ivs, uint initseed, uint minframes, uint maxframes)
        {
            uint pidl = pid & 0xFFFF;
            uint pidh = pid >> 16;
            uint ivsh = ivs >> 16;
            uint ivsl = ivs & 0xFFFF;
            uint[] seeds = new uint[5];
            var rng = new XdRng(initseed);

            rng.GetNext32BitNumber((int)minframes);

            for (int x = 0; x < 5; x++)
                seeds[x] = rng.GetNext16BitNumber();

            int j = 4;
            int k;

            for (uint i = 1 + minframes; i < maxframes; i++, seeds[j] = rng.GetNext16BitNumber())
            {
                if (++j > 4)
                    j = 0;

                if ((seeds[j] & 0x7FFF) != ivsl)
                    continue;

                if ((seeds[j >= 4 ? 0 : j + 1] & 0x7FFF) != ivsh)
                    continue;

                k = j + 3;
                if (k > 4)
                    k -= 5;

                if (seeds[k] != pidh)
                    continue;

                k = j + 4;
                if (k > 4)
                    k -= 5;

                if (seeds[k] != pidl)
                    continue;

                string[] row = new string[] { i.ToString() };
                dataGridView1.Rows.Add(row);
                dataGridView1.Refresh();
                break;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            }
            else
            {
                MessageBox.Show("Please select the frame", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_1_Click(object sender, EventArgs e)
        {
            label8.Focus();  //It's needed to be able to switch to the search button with the TAB key

            _MaskedTextBox1.Text = "31";
        }

        private void button1_2_Click(object sender, EventArgs e)
        {
            label8.Focus();

            _MaskedTextBox1.Text = "30";
        }

        private void button1_3_Click(object sender, EventArgs e)
        {
            label8.Focus();

            _MaskedTextBox1.Text = "";
        }

        private void _MaskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _Search();
            }
        }
    }
}