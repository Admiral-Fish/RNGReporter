using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class _MaskedTextBox : MaskedTextBox
    {
        int pos1;
        int start;
        bool select = false;
        bool Check = false;

        public _MaskedTextBox()
        {
            InitializeComponent();
        }

        public _MaskedTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            pos1 = MousePosition.X;          
            
            if (e.Button == MouseButtons.Right)
            {
                select = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (SelectionStart > Text.Length)
            {
                Select(Text.Length, 0);
            }
            else if (SelectedText == Text)
            {
                Select(0, Text.Length);
            }
            else
            { Select(SelectionStart, SelectedText.Length); }


            if (pos1 > MousePosition.X && SelectedText != "")
            {
                start = SelectionStart + SelectedText.Length;
            }
            else if (pos1 < MousePosition.X && SelectedText != "")
            {
                start = SelectionStart;
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnEnter(EventArgs e)
        {
            Focus();

            if (Text != "" || select == true)
            {
                Select(0, Text.Length);
                select = false;
            }
            else
            { SelectAll(); }
            
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (SelectedText == "")
            {
                Select(SelectionStart, 0);
            }

            if (!e.Control && !e.Shift && e.KeyCode == Keys.Right)  //Right
            {
                if (SelectionStart == Text.Length)
                {
                    e.Handled = true;
                }
                else if (SelectionStart < Text.Length && SelectedText != "")
                {
                    Select(SelectionStart + SelectedText.Length, 0);
                    e.Handled = true;
                }
            }
            else if (!e.Control && e.Shift && e.KeyCode == Keys.Right)  //Shift + Right
            {
                try
                {
                    if (SelectionStart == Text.Length)
                    {
                        e.Handled = true;
                    }
                    else if (SelectionStart == 0 && Text.Substring(SelectedText.Length, 1) == " ")
                    {
                        //this statement is needed just to catch when the selection goes over the lenght of the text
                    }
                    else if (SelectionStart < Text.Length && SelectedText != "" && Text.Substring(SelectionStart + SelectedText.Length, 1) == " ")
                    {
                        //same ^^^^
                    }
                }
                catch { e.Handled = true; }

                if (SelectedText == "")
                {
                    start = SelectionStart;
                }

                if (start > SelectionStart && SelectionStart >= 0 && SelectedText.Length > 1)
                {
                    Select(SelectionStart + 1, SelectedText.Length - 1);
                    e.Handled = true;
                }
                else if (start > SelectionStart && SelectionStart > 0 && SelectedText.Length == 1)
                {
                    Select(SelectionStart + 1, 0);
                    e.Handled = true;
                }
            }
            else if ((!e.Shift && e.KeyCode == Keys.Down)  //Down
                || (!e.Shift && e.Control && e.KeyCode == Keys.Right)  //Ctrl + Right
                || (!e.Shift && e.KeyCode == Keys.End))  //End
            {
                if (SelectedText != "")
                {
                    Select(SelectionStart + SelectedText.Length, 0);
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if ((e.Shift && e.KeyCode == Keys.Down)  //Shift + Down
                || (e.Control && e.Shift && e.KeyCode == Keys.Right)  //Ctrl + Shift + Right
                || (e.Shift && e.KeyCode == Keys.End))  //Shift + End
            {
                if (SelectedText == "")
                {
                    start = SelectionStart;
                }

                if (SelectionStart == Text.Length)
                {
                    e.Handled = true;
                }
                else if (SelectedText != "" && start > SelectionStart)
                {
                    Select(start, Text.Length - start);
                    e.Handled = true;
                }
                else if (SelectedText != "" || SelectedText == "")
                {
                    Select(SelectionStart, Text.Length - SelectionStart);
                    e.Handled = true;
                }
                else if (SelectionStart + SelectedText.Length == Text.Length)
                {
                    Select(Text.Length, 0);
                }
            }
            else if (!e.Shift && e.KeyCode == Keys.Left)  //Left
            {
                if (SelectionStart < Text.Length && SelectedText != "")
                {
                    Select(SelectionStart, 0);
                    e.Handled = true;
                }
                else if (SelectionStart == Text.Length)
                {
                    SelectedText = "";
                }
            }
            else if (!e.Control && e.Shift && e.KeyCode == Keys.Left)  //Shift + Left
            {
                if (SelectedText == "")
                {
                    start = SelectionStart;
                }

                if (start > SelectionStart && SelectionStart > 0)
                {
                    Select(SelectionStart - 1, SelectedText.Length + 1);
                    e.Handled = true;
                }
                else if (start > SelectionStart && SelectionStart == 0)
                {
                    e.Handled = true;
                }
            }
            else if ((!e.Shift && e.KeyCode == Keys.Up)  //Up
                || (!e.Shift && e.KeyCode == Keys.Home))  //Home
            {
                if (SelectedText != "")
                {
                    Select(SelectionStart, 0);
                    e.Handled = true;
                }
                else
                { Select(0, 0); }
            }
            else if ((e.Shift && e.KeyCode == Keys.Up)  //Shift + Up
                || (e.Control && e.Shift && e.KeyCode == Keys.Left)  //Ctrl + Shift + Left
                || (e.Shift && e.KeyCode == Keys.Home))  //Shift + Home
            {
                if (SelectedText == "")
                {
                    start = SelectionStart;
                }

                if (start > 0 && SelectedText != "")
                {
                    Select(0, start);
                    e.Handled = true;
                }
                else
                {
                    Select(0, SelectionStart);
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Space)  //Space
            {
                if (SelectedText != "")
                {
                    SelectedText = "";
                }
            }

            if (SelectionStart > Text.Length)
            {
                SelectionStart = Text.Length;
            }
            base.OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                Select(0, Text.Length);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                try
                {
                    int HexTest = int.Parse(e.KeyChar.ToString(), NumberStyles.HexNumber);
                    e.KeyChar = char.ToUpper(e.KeyChar);
                    Check = true;
                }
                catch { e.KeyChar = (char)0; }
            }
            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Tag == "ivs" && Text.Substring(Text.Length - 1, 1) != "_" && int.Parse(Text) > 31)
            {
                Text = "31";
            }
            else if (Tag == "frame" && Text.Substring(Text.Length - 1, 1) != "_" && ulong.Parse(Text) > 4294967295)
            {
                Text = "4294967295";
            }

            if (Check == false)
            {
                string NewText = "";
                string ReplacedText = Text.Replace("_", "");

                foreach (char a in ReplacedText)
                {
                    try
                    {
                        int HexTest = int.Parse(a.ToString(), NumberStyles.HexNumber);
                        NewText = NewText + char.ToUpper(a);
                    }
                    catch { }
                }
                Text = NewText;
            }
            else { Check = false; }
            
            base.OnTextChanged(e);
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (SelectionStart > Text.Length)
            {
                SelectionStart = Text.Length;
            }
            base.OnKeyUp(e);
        }
    }
}
