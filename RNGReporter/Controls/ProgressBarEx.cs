using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.ProgressBarEx), "Icons.ProgressBarEx.ico")]
    public class ProgressBarEx : AbstractProgressBar
    {
        protected IProgressBackgroundPainter backgroundpainter;
        protected IProgressBorderPainter borderpainter;
        protected IProgressPainter progresspainter;

        public ProgressBarEx()
        {
            backgroundpainter = new PlainBackgroundPainter();
            progresspainter = new PlainProgressPainter(Color.Gold);
            borderpainter = new PlainBorderPainter();
        }

        /// <summary></summary>
        [Category("Painters"), Description("Paints this progress bar's background"), Browsable(true)]
        public IProgressBackgroundPainter BackgroundPainter
        {
            get { return backgroundpainter; }
            set
            {
                backgroundpainter = value;
                backgroundpainter.PropertiesChanged += component_PropertiesChanged;
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Painters"), Description("Paints this progress bar's progress"), Browsable(true)]
        public IProgressPainter ProgressPainter
        {
            get { return progresspainter; }
            set
            {
                if (!(value is IAnimatedProgressPainter) && base.ProgressType == ProgressType.Animated)
                {
                    base.ProgressType = ProgressType.Smooth;
                }
                progresspainter = value;
                if (progresspainter is AbstractProgressPainter)
                {
                    ((AbstractProgressPainter) progresspainter).padding = base.ProgressPadding;
                }
                progresspainter.PropertiesChanged += component_PropertiesChanged;
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the type of progress"), Browsable(true)]
        public override ProgressType ProgressType
        {
            get { return base.type; }
            set
            {
                if (value == ProgressType.Animated && !(progresspainter is IAnimatedProgressPainter))
                {
                    throw new ArgumentException("Animated is not available with the current Progress Painter");
                }
                type = value;
            }
        }

        /// <summary></summary>
        [Category("Painters"), Description("Paints this progress bar's border"), Browsable(true)]
        public IProgressBorderPainter BorderPainter
        {
            get { return borderpainter; }
            set
            {
                borderpainter = value;
                borderpainter.PropertiesChanged += component_PropertiesChanged;
                ResizeProgress();
                Invalidate();
            }
        }

        /// <summary></summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void component_PropertiesChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void ResizeProgress()
        {
            if (base.ProgressType != ProgressType.Smooth)
            {
                return;
            }
            Rectangle newprog = base.borderbox;
            //newprog.Inflate(this.borderpainter.BorderWidth, this.borderpainter.BorderWidth);
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);
            base.backbox = newprog;

            int val = value;
            if (val > 0)
            {
                val++;
            }
            int progWidth = maximum > 0 ? (backbox.Width*val/maximum) : 1;
            if (value >= maximum && maximum > 0)
            {
                progWidth = backbox.Width;
            } /*else if (value > 0) {
				progWidth++;
			}*/
            newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            newprog.Width = progWidth - (base.ProgressPadding*2);
            //newprog.Offset(base.ProgressPadding, base.ProgressPadding);
            //newprog = new Rectangle(backbox.X + base.ProgressPadding, backbox.Y + base.ProgressPadding, progWidth - (base.ProgressPadding * 2), backbox.Height - (base.ProgressPadding * 2));
            base.progressbox = newprog;
        }

        /// <summary></summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (running)
            {
                running = false;
            }
        }

        /// <summary></summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeProgress();
            if (backgroundpainter != null)
            {
                backgroundpainter.Resize(borderbox);
            }
            if (progresspainter != null)
            {
                progresspainter.Resize(borderbox);
            }
            if (borderpainter != null)
            {
                borderpainter.Resize(borderbox);
            }
        }

        /// <summary></summary>
        /// <param name="gr"></param>
        protected override void PaintBackground(Graphics g)
        {
            if (backgroundpainter != null)
            {
                backgroundpainter.PaintBackground(backbox, g);
            }
        }

        /// <summary></summary>
        /// <param name="gr"></param>
        protected override void PaintProgress(Graphics g)
        {
            if (progresspainter != null)
            {
                progresspainter.PaintProgress(progressbox, g);
            }
        }

        /// <summary></summary>
        /// <param name="gr"></param>
        protected override void PaintText(Graphics g)
        {
            if (base.ProgressType != ProgressType.Smooth)
            {
                return;
            }
            Brush b = new SolidBrush(ForeColor);
            SizeF sf = g.MeasureString(Text, Font, Convert.ToInt32(Width), StringFormat.GenericDefault);
            float m = sf.Width;
            float x = (Width/2) - (m/2);
            float w = (Width/2) + (m/2);
            float h = borderbox.Height - (2f*borderpainter.BorderWidth);
            float y = borderpainter.BorderWidth + ((h - sf.Height)/2f);
            g.DrawString(Text, Font, b, RectangleF.FromLTRB(x, y, w, Height - 1), StringFormat.GenericDefault);
        }

        /// <summary></summary>
        /// <param name="gr"></param>
        protected override void PaintBorder(Graphics g)
        {
            if (borderpainter != null)
            {
                borderpainter.PaintBorder(borderbox, g);
            }
        }

        #region Animation

        public void StartAnimation()
        {
            if (running)
            {
                return;
            }
            var iapp = progresspainter as IAnimatedProgressPainter;
            if (iapp == null)
            {
                return;
            }
            iapp.Animating = true;
            running = true;
            timerMethod = DoAnimation;
            timer.Interval = iapp.AnimationSpeed;
            timer.Tick += timerMethod;
            timer.Enabled = true;
        }

        public void StopAnimation()
        {
            timer.Enabled = false;
            timer.Tick -= timerMethod;
            running = false;
            var iapp = progresspainter as IAnimatedProgressPainter;
            if (iapp == null)
            {
                return;
            }
            iapp.Animating = false;
        }

        private void DoAnimation(object sender, EventArgs e)
        {
            var iapp = progresspainter as IAnimatedProgressPainter;
            if (iapp == null)
            {
                return;
            }

            //Rectangle newprog = base.borderbox;
            //newprog.Offset(this.borderpainter.BorderWidth, this.borderpainter.BorderWidth);
            //newprog.Size = new Size(newprog.Size.Width - this.borderpainter.BorderWidth, newprog.Size.Height - this.borderpainter.BorderWidth);
            ////int progWidth = (int)(((float)marqueePercentage * (float)backbox.Width) / 100f);
            //int progWidth = (int)(((float)marqueePercentage * (float)backbox.Width) / 100f);
            //newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            //newprog.Width = progWidth - (base.ProgressPadding * 2);

            //base.progressbox = newprog;

            ////iapp.AnimateFrame(newprog, g, ref marqueeX);

            Invalidate();
            Refresh();
        }

        #endregion

        #region Marquee

        private readonly Timer timer = new Timer();
        private bool marqueeForward = true;
        private int marqueeX;
        private bool running;
        private EventHandler timerMethod;

        /// <summary></summary>
        public override void MarqueeStart()
        {
            if (running)
            {
                return;
            }
            running = true;
            switch (base.ProgressType)
            {
                case ProgressType.MarqueeWrap:
                    timerMethod = DoMarqueeWrap;
                    break;
                case ProgressType.MarqueeBounce:
                    timerMethod = DoMarqueeBounce;
                    break;
                case ProgressType.MarqueeBounceDeep:
                    timerMethod = DoMarqueeDeep;
                    break;
            }
            timer.Interval = base.marqueeSpeed;
            timer.Tick += timerMethod;
            timer.Enabled = true;
        }

        private void DoMarqueeWrap(object sender, EventArgs e)
        {
            Rectangle newprog = base.borderbox;
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);

            var progWidth = (int) ((marqueePercentage*(float) backbox.Width)/100f);

            marqueeX += marqueeStep;
            if (marqueeX > backbox.Width)
            {
                marqueeX = 0 - progWidth;
            }

            newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            newprog.Width = progWidth - (base.ProgressPadding*2);
            newprog.X += marqueeX;

            int leftBoundry = backbox.X + borderpainter.BorderWidth + base.ProgressPadding;
            int rightBoundry = backbox.X + backbox.Width - (borderpainter.BorderWidth + base.ProgressPadding);
            if (marqueeX <= leftBoundry)
            {
                newprog.Width -= leftBoundry - marqueeX;
                newprog.X = leftBoundry;
            }
            else if (marqueeX + newprog.Width >= rightBoundry)
            {
                newprog.Width -= (marqueeX + newprog.Width + base.ProgressPadding) - rightBoundry;
            }

            base.progressbox = newprog;

            Invalidate();
            Refresh();
        }

        private void DoMarqueeBounce(object sender, EventArgs e)
        {
            Rectangle newprog = base.borderbox;
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);

            var progWidth = (int) ((marqueePercentage*(float) backbox.Width)/100f);

            if (marqueeForward)
            {
                marqueeX += marqueeStep;
            }
            else
            {
                marqueeX -= marqueeStep;
            }

            newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            newprog.Width = progWidth - (base.ProgressPadding*2);
            newprog.X += marqueeX;

            int leftBoundry = backbox.X + borderpainter.BorderWidth + base.ProgressPadding;
            int rightBoundry = backbox.X + backbox.Width - (borderpainter.BorderWidth + base.ProgressPadding);
            if (marqueeX + progWidth >= rightBoundry)
            {
                marqueeForward = false;
            }
            else if (marqueeX <= leftBoundry)
            {
                marqueeForward = true;
            }

            base.progressbox = newprog;

            Invalidate();
            Refresh();
        }

        private void DoMarqueeDeep(object sender, EventArgs e)
        {
            Rectangle newprog = base.borderbox;
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);

            var progWidth = (int) ((marqueePercentage*(float) backbox.Width)/100f);

            if (marqueeForward)
            {
                marqueeX += marqueeStep;
            }
            else
            {
                marqueeX -= marqueeStep;
            }
            if (marqueeX > backbox.Width)
            {
                marqueeForward = false;
            }
            else if (marqueeX < backbox.X - progWidth)
            {
                marqueeForward = true;
            }

            newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            newprog.Width = progWidth - (base.ProgressPadding*2);
            newprog.X += marqueeX;

            int leftBoundry = backbox.X + borderpainter.BorderWidth + base.ProgressPadding;
            int rightBoundry = backbox.X + backbox.Width - (borderpainter.BorderWidth + base.ProgressPadding);
            if (marqueeX <= leftBoundry)
            {
                newprog.Width -= leftBoundry - marqueeX;
                newprog.X = leftBoundry;
            }
            else if (marqueeX + newprog.Width >= rightBoundry)
            {
                newprog.Width -= (marqueeX + newprog.Width + base.ProgressPadding) - rightBoundry;
            }

            base.progressbox = newprog;

            Invalidate();
            Refresh();
        }

        /// <summary></summary>
        public override void MarqueePause()
        {
            running = false;
            timer.Enabled = false;
            timer.Tick -= timerMethod;
        }

        /// <summary></summary>
        public override void MarqueeStop()
        {
            Rectangle newprog = base.borderbox;
            newprog.Offset(borderpainter.BorderWidth, borderpainter.BorderWidth);
            newprog.Size = new Size(newprog.Size.Width - borderpainter.BorderWidth,
                                    newprog.Size.Height - borderpainter.BorderWidth);

            newprog.Inflate(-base.ProgressPadding, -base.ProgressPadding);
            newprog.Width = 1;
            base.progressbox = newprog;

            running = false;
            timer.Enabled = false;
            timer.Tick -= timerMethod;

            marqueeX = 0;
            Invalidate();
        }

        #endregion
    }
}