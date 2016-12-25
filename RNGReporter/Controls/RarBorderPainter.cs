using System;
using System.ComponentModel;
using System.Drawing;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.RarBorderPainter), "Icons.RarBorderPainter.ico")]
    public class RarBorderPainter : Component, IProgressBorderPainter, IDisposable
    {
        private readonly Pen border;

        private EventHandler onPropertiesChanged;

        /// <summary></summary>
        public RarBorderPainter()
        {
            border = new Pen(new SolidBrush(Color.FromArgb(064, 064, 070)), 1f);
        }

        #region IProgressBorderPainter Members

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
        [Browsable(false)]
        public int BorderWidth
        {
            get { return 1; }
        }

        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        public void PaintBorder(Rectangle box, Graphics g)
        {
            g.DrawRectangle(new Pen(new SolidBrush(SystemColors.Control), 1f), 0, 0, box.Width, box.Height);
            g.DrawLine(border, 2, box.Height, box.Width, box.Height);
            g.DrawLine(border, box.Width, 3, box.Width, box.Height);
        }

        /// <summary></summary>
        public void Resize(Rectangle box)
        {
        }

        #endregion

        private void FireChange()
        {
            if (onPropertiesChanged != null)
            {
                onPropertiesChanged(this, EventArgs.Empty);
            }
        }

        /// <summary></summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            border.Dispose();
        }
    }
}