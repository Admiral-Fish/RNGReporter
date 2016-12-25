using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RNGReporter
{
    /// <summary></summary>
    public enum ProgressType
    {
        Smooth,
        MarqueeWrap,
        MarqueeBounce,
        MarqueeBounceDeep,
        Animated
    }

    /// <summary></summary>
    public abstract class AbstractProgressBar : Control
    {
        protected EventHandler OnValueChanged;
        protected Rectangle backbox;
        protected Rectangle borderbox;
        protected int maximum = 100;
        protected int minimum;
        private int padding;
        protected Rectangle progressbox;
        private bool showPercent;
        protected int value;

        #region Marquee

        protected int marqueePercentage = 25;
        protected int marqueeSpeed = 30;
        protected int marqueeStep = 1;
        protected ProgressType type = ProgressType.Smooth;

        #endregion

        public AbstractProgressBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets whether or not to draw the percentage text"), Browsable(true)]
        public bool ShowPercentage
        {
            get { return showPercent; }
            set
            {
                showPercent = value;
                Invalidate();
                if (!showPercent)
                {
                    Text = "";
                }
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the minimum value"), Browsable(true)]
        public virtual int Minimum
        {
            get { return minimum; }
            set
            {
                if (value > maximum)
                {
                    throw new ArgumentException("Minimum must be smaller than maximum.");
                }
                minimum = value;
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the maximum value"), Browsable(true)]
        public virtual int Maximum
        {
            get { return maximum; }
            set
            {
                if (value < minimum)
                {
                    throw new ArgumentException("Maximum must be larger than minimum.");
                }
                maximum = value;
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the current value"), Browsable(true)]
        public int Value
        {
            get { return value; }
            set
            {
                if (value < minimum)
                {
                    throw new ArgumentException("Value must be greater than or equal to minimum.");
                }
                if (value > maximum)
                {
                    throw new ArgumentException("Value must be less than or equal to maximum.");
                }
                this.value = value;
                if (showPercent)
                {
                    var percent = (int) ((this.value/(maximum - 1f))*100f);
                    if (percent > 0)
                    {
                        if (percent > 100)
                        {
                            percent = 100;
                        }
                        Text = string.Format("{0}%", percent.ToString());
                    }
                    else
                    {
                        Text = "";
                    }
                }
                if (OnValueChanged != null)
                {
                    OnValueChanged(this, EventArgs.Empty);
                }
                ResizeProgress();
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the number of pixels to pad between the border and progress"),
         Browsable(true)]
        public int ProgressPadding
        {
            get { return padding; }
            set
            {
                padding = value;
                if (OnValueChanged != null)
                {
                    OnValueChanged(this, EventArgs.Empty);
                }
                //ResizeProgress();
                OnResize(EventArgs.Empty);
                Invalidate();
            }
        }

        /// <summary></summary>
        [Category("Progress"), Description("Gets or sets the type of progress"), Browsable(true)]
        public virtual ProgressType ProgressType
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary></summary>
        [Browsable(false)]
        public Rectangle BorderBox
        {
            get { return borderbox; }
        }

        /// <summary></summary>
        [Browsable(false)]
        public Rectangle BackBox
        {
            get { return backbox; }
        }

        /// <summary></summary>
        [Browsable(false)]
        public Rectangle ProgressBox
        {
            get { return progressbox; }
        }

        #region Marquee

        /// <summary></summary>
        [Category("Marquee"), Description("Gets or sets the number of milliseconds between marquee steps"),
         Browsable(true)]
        public int MarqueeSpeed
        {
            get { return marqueeSpeed; }
            set
            {
                marqueeSpeed = value;
                if (marqueeSpeed < 1)
                {
                    marqueeSpeed = 1;
                }
            }
        }

        /// <summary></summary>
        [Category("Marquee"), Description("Gets or sets the number of pixels to progress the marquee bar"),
         Browsable(true)]
        public int MarqueeStep
        {
            get { return marqueeStep; }
            set { marqueeStep = value; }
        }

        /// <summary></summary>
        [Category("Marquee"), Description("Gets or sets the percentage of the width that the marquee progress fills"),
         Browsable(true)]
        public int MarqueePercentage
        {
            get { return marqueePercentage; }
            set
            {
                if (value < 5 || value > 95)
                {
                    throw new ArgumentException("Marquee percentage width must be between 5% and 95%.");
                }
                marqueePercentage = value;
            }
        }

        #endregion

        /// <summary></summary>
        public event EventHandler ValueChanged
        {
            add
            {
                if (OnValueChanged != null)
                {
                    foreach (Delegate d in OnValueChanged.GetInvocationList())
                    {
                        if (ReferenceEquals(d, value))
                        {
                            return;
                        }
                    }
                }
                OnValueChanged = (EventHandler) Delegate.Combine(OnValueChanged, value);
            }
            remove { OnValueChanged = (EventHandler) Delegate.Remove(OnValueChanged, value); }
        }

        /// <summary></summary>
        /// <param name="gr"></param>
        protected abstract void PaintBackground(Graphics gr);

        /// <summary></summary>
        /// <param name="gr"></param>
        protected abstract void PaintProgress(Graphics gr);

        /// <summary></summary>
        /// <param name="gr"></param>
        protected abstract void PaintText(Graphics gr);

        /// <summary></summary>
        /// <param name="gr"></param>
        protected abstract void PaintBorder(Graphics gr);

        /// <summary></summary>
        protected abstract void ResizeProgress();

        /// <summary></summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            borderbox = new Rectangle(0, 0, Width - 1, Height - 1);
            backbox = new Rectangle(0, 0, Width - 1, Height - 1);
            ResizeProgress();
        }

        /// <summary></summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintBackground(e.Graphics);
            PaintProgress(e.Graphics);
            e.Graphics.Clip = new Region(new Rectangle(0, 0, Width, Height));
            PaintText(e.Graphics);
            PaintBorder(e.Graphics);
        }

        /// <summary></summary>
        public abstract void MarqueeStart();

        /// <summary></summary>
        public abstract void MarqueePause();

        /// <summary></summary>
        public abstract void MarqueeStop();
    }
}