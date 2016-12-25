using System.Windows.Forms;

namespace RNGReporter
{
    public partial class RoamerMap : Form
    {
        public RoamerMap()
        {
            InitializeComponent();
        }

        private void RoamerMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}