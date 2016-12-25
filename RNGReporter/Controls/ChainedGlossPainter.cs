using System;
using System.ComponentModel;
using System.Drawing;

namespace RNGReporter
{
    /// <summary>Extending this class allows you to chain multiple IGlossPainters together.</summary>
    public abstract class ChainedGlossPainter : Component, IGlossPainter, IDisposable
    {
        private EventHandler onPropertiesChanged;
        private IGlossPainter successor;

        /// <summary></summary>
        [Category("Painters"), Description("Gets or sets the next gloss in the chain"), Browsable(true)]
        public IGlossPainter Successor
        {
            get { return successor; }
            set
            {
                IGlossPainter nextPainter = value;
                while (nextPainter != null && nextPainter is ChainedGlossPainter)
                {
                    if (ReferenceEquals(this, nextPainter))
                    {
                        throw new ArgumentException(
                            "Gloss cannot eventually be it's own successor, an infinite loop will result");
                    }
                    nextPainter = ((ChainedGlossPainter) nextPainter).Successor;
                }

                successor = value;
                if (successor != null)
                {
                    successor.PropertiesChanged += successor_PropertiesChanged;
                }
                FireChange();
            }
        }

        #region IGlossPainter Members

        /// <summary></summary>
        public event EventHandler PropertiesChanged
        {
            add
            {
                if (onPropertiesChanged != null)
                {
                    foreach (Delegate d in onPropertiesChanged.GetInvocationList())
                    {
                        if (ReferenceEquals(d, value))
                        {
                            return;
                        }
                    }
                }
                onPropertiesChanged = (EventHandler) Delegate.Combine(onPropertiesChanged, value);
            }
            remove { onPropertiesChanged = (EventHandler) Delegate.Remove(onPropertiesChanged, value); }
        }

        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        public void PaintGloss(Rectangle box, Graphics g)
        {
            if (box.Width < 1)
            {
                return;
            }
            PaintThisGloss(box, g);
            if (successor != null)
            {
                successor.PaintGloss(box, g);
            }
        }

        /// <summary></summary>
        /// <param name="box"></param>
        public void Resize(Rectangle box)
        {
            ResizeThis(box);
            if (successor != null)
            {
                successor.Resize(box);
            }
        }

        #endregion

        private void successor_PropertiesChanged(object sender, EventArgs e)
        {
            FireChange();
        }

        /// <summary></summary>
        protected void FireChange()
        {
            if (onPropertiesChanged != null)
            {
                onPropertiesChanged(this, EventArgs.Empty);
            }
        }

        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        protected abstract void PaintThisGloss(Rectangle box, Graphics g);

        protected abstract void ResizeThis(Rectangle box);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (successor != null)
            {
                successor.Dispose();
            }
        }
    }
}