using System;
using System.ComponentModel;
using System.Drawing;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.DualProgressBar), "Icons.DualProgressBar.ico")]
    public class DualProgressBar : ProgressBarEx
    {
        protected EventHandler OnMasterValueChanged;
        private bool masterBottom;
        private Rectangle masterbox;
        private int mastermax = 100;
        private IProgressPainter masterpainter;
        private int masterval;
        private int padding;

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the maximum value"), Browsable(true)]
        public override int Maximum
        {
            get { return base.maximum; }
            set
            {
                base.Maximum = value;
                mastermax = value;
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the value of the master progress"), Browsable(true)]
        public int MasterValue
        {
            get { return masterval; }
            set
            {
                masterval = value;
                if (OnMasterValueChanged != null)
                {
                    OnMasterValueChanged(this, EventArgs.Empty);
                }
                ResizeMasterProgress();
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the maximum value for the master progress"), Browsable(true)]
        public int MasterMaximum
        {
            get { return mastermax; }
            set
            {
                mastermax = value;
                ResizeMasterProgress();
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the padding for the master progress"), Browsable(true)]
        public int MasterProgressPadding
        {
            get { return padding; }
            set
            {
                padding = value;
                if (OnValueChanged != null)
                {
                    OnValueChanged(this, EventArgs.Empty);
                }
                ResizeMasterProgress();
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Painters"), Description("Paints this progress bar's master progress"), Browsable(true)]
        public IProgressPainter MasterPainter
        {
            get { return masterpainter; }
            set
            {
                masterpainter = value;
                if (masterpainter is AbstractProgressPainter)
                {
                    ((AbstractProgressPainter) masterpainter).padding = base.ProgressPadding;
                }
                masterpainter.PropertiesChanged += component_PropertiesChanged;
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"),
         Description("Determines whether or not the master progress is painted under the main progress"),
         Browsable(true)]
        public bool PaintMasterFirst
        {
            get { return masterBottom; }
            set
            {
                masterBottom = value;
                Invalidate();
            }
        }

        /// <summary></summary>
        public event EventHandler MasterValueChanged
        {
            add
            {
                if (OnMasterValueChanged != null)
                {
                    foreach (Delegate d in OnMasterValueChanged.GetInvocationList())
                    {
                        if (ReferenceEquals(d, value))
                        {
                            return;
                        }
                    }
                }
                OnMasterValueChanged = (EventHandler) Delegate.Combine(OnMasterValueChanged, value);
            }
            remove { OnMasterValueChanged = (EventHandler) Delegate.Remove(OnMasterValueChanged, value); }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeProgress();
            ResizeMasterProgress();
            if (backgroundpainter != null)
            {
                backgroundpainter.Resize(borderbox);
            }
            if (masterBottom && masterpainter != null)
            {
                masterpainter.Resize(masterbox);
            }
            if (progresspainter != null)
            {
                progresspainter.Resize(borderbox);
            }
            if (!masterBottom && masterpainter != null)
            {
                masterpainter.Resize(masterbox);
            }
            if (borderpainter != null)
            {
                borderpainter.Resize(borderbox);
            }
        }

        private void ResizeMasterProgress()
        {
            Rectangle newprog = base.borderbox;
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);
            base.backbox = newprog;

            int val = masterval;
            if (val > 0)
            {
                val++;
            }
            int progWidth = mastermax > 0 ? (backbox.Width*val/mastermax) : 1;
            if (value >= mastermax && mastermax > 0)
            {
                progWidth = backbox.Width;
            } /*else if (value > 0) {
				progWidth++;
			}*/
            //newprog = new Rectangle(backbox.X + base.ProgressPadding, backbox.Y + base.ProgressPadding, progWidth - (base.ProgressPadding * 2), backbox.Height - (base.ProgressPadding * 2));
            //newprog = new Rectangle(backbox.X, backbox.Y, progWidth, backbox.Height);
            newprog = new Rectangle(backbox.X + padding, backbox.Y + padding, progWidth - (padding*2),
                                    backbox.Height - (padding*2));
            masterbox = newprog;
        }

        ///// <summary></summary>
        //protected override void MarqueeStart() {
        //}
        ///// <summary></summary>
        //protected override void MarqueePause() {
        //}
        ///// <summary></summary>
        //protected override void MarqueeStop() {
        //}

        /// <summary></summary>
        /// <param name="gr"></param>
        protected override void PaintProgress(Graphics g)
        {
            if (progresspainter != null)
            {
                if (masterBottom && masterpainter != null)
                {
                    masterpainter.PaintProgress(masterbox, g);
                }
                progresspainter.PaintProgress(progressbox, g);
                if (!masterBottom && masterpainter != null)
                {
                    masterpainter.PaintProgress(masterbox, g);
                }
            }
        }
    }
}