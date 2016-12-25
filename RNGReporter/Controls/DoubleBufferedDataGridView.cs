using System;
using System.Drawing;
using System.Windows.Forms;

namespace RNGReporter
{
    public class DoubleBufferedDataGridView : DataGridView
    {
        public DoubleBufferedDataGridView()
        {
            RowTemplate.Height = 20;
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                float ratio = g.DpiX/96;

                foreach (DataGridViewColumn column in Columns)
                {
                    column.Width = (int) (column.Width*ratio + 1);
                }

                //this.ColumnHeadersHeight = 100;

                ColumnHeadersHeight = (int) (ColumnHeadersHeight*ratio);
            }


            DoubleBuffered = true;
        }
    }
}