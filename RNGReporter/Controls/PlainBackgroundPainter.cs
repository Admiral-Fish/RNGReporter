using System;
using System.ComponentModel;
using System.Drawing;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.PlainBackgroundPainter), "Icons.PlainBackgroundPainter.ico")]
    public class PlainBackgroundPainter : Component, IProgressBackgroundPainter, IDisposable
    {
        private Brush brush;
        private Color color;
        private IGlossPainter gloss;

        private EventHandler onPropertiesChanged;

        /// <summary></summary>
        public PlainBackgroundPainter()
        {
            Color = Color.FromArgb(240, 240, 240);
        }

        /// <summary></summary>
        /// <param name="color"></param>
        public PlainBackgroundPainter(Color color)
        {
            Color = color;
        }

        /// <summary></summary>
        [Category("Appearance"), Description("Gets or sets the background color"), Browsable(true)]
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                brush = new SolidBrush(color);
                FireChange();
            }
        }

        #region IProgressBackgroundPainter Members

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
        /// <summary></summary>
        [Category("Painters"), Description("Gets or sets the chain of gloss painters"), Browsable(true)]
        public IGlossPainter GlossPainter
        {
            get { return gloss; }
            set
            {
                gloss = value;
                if (gloss != null)
                {
                    gloss.PropertiesChanged += component_PropertiesChanged;
                }
                FireChange();
            }
        }

        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        public void PaintBackground(Rectangle box, Graphics g)
        {
            g.FillRectangle(brush, box);

            if (gloss != null)
            {
                gloss.PaintGloss(box, g);
            }
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void component_PropertiesChanged(object sender, EventArgs e)
        {
            FireChange();
        }

        /// <summary></summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            brush.Dispose();
        }
    }
}