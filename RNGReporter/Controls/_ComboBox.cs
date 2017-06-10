using System.ComponentModel;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class _ComboBox : ComboBox
    {
        bool enter = true;

        public _ComboBox()
        {
            InitializeComponent();
        }

        public _ComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && DroppedDown == true)
            {
                DroppedDown = false;
                enter = false;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && DroppedDown == false && enter == true)
            {
                DroppedDown = true;
            }
            else
            { enter = true; }
        }
    }
}
