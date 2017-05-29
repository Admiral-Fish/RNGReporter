using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class _DataGridView : DataGridView
    {
        public _DataGridView()
        {
            InitializeComponent();
        }

        public _DataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                HitTestInfo hti = HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((Rows[hti.RowIndex])).Selected)
                    {
                        ClearSelection();

                        (Rows[hti.RowIndex]).Selected = true;
                    }
                }
            }
            base.OnMouseDown(e);
        }
    }
}
