using System;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class SearchRoamers : Form
    {
        private int returnE;
        private int returnL;

        private int returnR;

        public SearchRoamers()
        {
            InitializeComponent();
        }

        public int ReturnE
        {
            get { return returnE; }
        }

        public int ReturnR
        {
            get { return returnR; }
        }

        public int ReturnL
        {
            get { return returnL; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxERoute.Text != "")
                returnE = int.Parse(maskedTextBoxERoute.Text);

            if (maskedTextBoxRRoute.Text != "")
                returnR = int.Parse(maskedTextBoxRRoute.Text);

            if (maskedTextBoxLRoute.Text != "")
                returnL = int.Parse(maskedTextBoxLRoute.Text);
        }
    }
}