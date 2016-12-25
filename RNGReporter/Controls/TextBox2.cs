using System;
using System.Windows.Forms;

namespace RNGReporter
{
    public class TextBox2 : TextBox
    {
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            Focus();
            SelectAll();
        }
    }
}