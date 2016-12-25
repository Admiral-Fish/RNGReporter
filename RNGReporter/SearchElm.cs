using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class SearchElm : Form
    {
        private readonly List<Adjacent> _adjacents;

        public SearchElm()
        {
            ReturnElm = "";
            RoamerText = "";
            InitializeComponent();
            labelResults.Visible = false;
        }

        public SearchElm(List<Adjacent> adjacents)
        {
            ReturnElm = "";
            RoamerText = "";
            _adjacents = adjacents;
            InitializeComponent();
            UpdatePossible();
        }

        public string ReturnElm { get; private set; }

        public int ReturnE { get; private set; }

        public int ReturnR { get; private set; }

        public int ReturnL { get; private set; }

        public string RoamerText { get; private set; }

        public List<Adjacent> Possible { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ReturnElm = textBoxResponses.Text;

            if (maskedTextBoxERoute.Text != "")
                ReturnE = int.Parse(maskedTextBoxERoute.Text);

            if (maskedTextBoxRRoute.Text != "")
                ReturnR = int.Parse(maskedTextBoxRRoute.Text);

            if (maskedTextBoxLRoute.Text != "")
                ReturnL = int.Parse(maskedTextBoxLRoute.Text);
        }

        private void buttonK_Click(object sender, EventArgs e)
        {
            AddLetter("K");
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            AddLetter("E");
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            AddLetter("P");
        }

        private void AddLetter(string s)
        {
            textBoxResponses.Text += textBoxResponses.Text == "" ? s : ", " + s;
        }

        private void textBoxResponses_TextChanged(object sender, EventArgs e)
        {
            UpdatePossible();
        }

        private void UpdatePossible()
        {
            if (_adjacents == null || _adjacents.Count == 0) return;
            Possible = _adjacents.FindAll(HasElms);
            labelResults.Text = "Possible Results: " + Possible.Count;
        }

        private bool HasElms(Adjacent adjacent)
        {
            return adjacent.ElmResponses.Contains(textBoxResponses.Text) &&
                   (RoamerText == "" || RoamerText == adjacent.RoamerLocations);
        }

        private void maskedTextBoxRoute_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBoxRRoute.Text == "" && maskedTextBoxRRoute.Text == "" && maskedTextBoxRRoute.Text == "")
            {
                RoamerText = "";
                return;
            }
            bool firstDisplay = true;
            if (maskedTextBoxRRoute.Text != "")
            {
                RoamerText += "R: " + maskedTextBoxRRoute.Text;
                firstDisplay = false;
            }

            if ((maskedTextBoxERoute.Text != ""))
            {
                if (!firstDisplay)
                    RoamerText += "  ";

                RoamerText += "E: " + maskedTextBoxERoute.Text;
                firstDisplay = false;
            }

            if ((maskedTextBoxLRoute.Text != ""))
            {
                if (!firstDisplay)
                    RoamerText += "  ";

                RoamerText += "L: " + maskedTextBoxLRoute.Text;
            }
        }

        private void radioButtonElm_CheckedChanged(object sender, EventArgs e)
        {
            labelKElm.Visible = radioButtonElm.Checked;
            labelEElm.Visible = radioButtonElm.Checked;
            labelPElm.Visible = radioButtonElm.Checked;
        }

        private void radioButtonIrwin_CheckedChanged(object sender, EventArgs e)
        {
            labelKIrwin.Visible = radioButtonIrwin.Checked;
            labelEIrwin.Visible = radioButtonIrwin.Checked;
            labelPIrwin.Visible = radioButtonIrwin.Checked;
        }
    }
}