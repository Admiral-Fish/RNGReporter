using System;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class SetIVs : UserControl
    {
        public SetIVs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _MaskedTextBox1.Text = "31";
        }

        private void button1_2_Click(object sender, EventArgs e)
        {
            _MaskedTextBox1.Text = "30";
        }

        private void button1_3_Click(object sender, EventArgs e)
        {
            _MaskedTextBox1.Text = "";
        }
    }
}
