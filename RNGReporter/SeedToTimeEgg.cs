using System;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class SeedToTimeEgg : Form
    {
        private readonly uint flips;
        private readonly uint seed;
        private readonly uint taps;
        private DateTime targetTime;

        public SeedToTimeEgg(
            uint seed,
            DateTime targetTime,
            uint flips,
            uint taps)
        {
            this.seed = seed;
            this.targetTime = targetTime;
            this.flips = flips;
            this.taps = taps;

            InitializeComponent();
        }

        private void SeedToTimeEgg_Load(object sender, EventArgs e)
        {
            //  Set up the labels
            labelSeed.Text = seed.ToString();
            labelTargetTime.Text = "";
            labelFlips.Text = flips.ToString();
            labelTaps.Text = taps.ToString();

            //  Generate the Adjacents
        }

        private void buttonSearchFlips_Click(object sender, EventArgs e)
        {
        }

        private void buttonGenerateAdjacents_Click(object sender, EventArgs e)
        {
        }
    }
}