using System;
using System.ComponentModel;
using System.Drawing;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.PlainBorderPainter), "Icons.PlainBorderPainter.ico")]
    public class PlainBorderPainter : Component, IProgressBorderPainter, IDisposable
    {
        #region PlainBorderStyle enum

        /// <summary></summary>
        public enum PlainBorderStyle
        {
            Flat,
            Sunken,
            Raised
        }

        #endregion

        private Pen clearb;
        private Pen cleart;

        private Color color;
        private EventHandler onPropertiesChanged;
        private Pen penb;
        private Pen pent;
        private bool rounded;
        private PlainBorderStyle style = PlainBorderStyle.Flat;

        /// <summary></summary>
        public PlainBorderPainter()
        {
            Color = Color.Black;
            //this.clear = new Pen(new SolidBrush(SystemColors.Control));
        }

        /// <summary></summary>
        /// <param name="color"></param>
        public PlainBorderPainter(Color color)
        {
            Color = color;
            //this.clear = new Pen(new SolidBrush(SystemColors.Control));
        }

        /// <summary></summary>
        [Category("Appearance"), Description("Gets or sets the border color"), Browsable(true)]
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                if (style == PlainBorderStyle.Flat)
                {
                    pent = new Pen(new SolidBrush(color), 1f);
                    penb = pent;
                    cleart = new Pen(new SolidBrush(Color.FromArgb(64, color.R, color.G, color.B)));
                    clearb = cleart;
                    FireChange();
                }
            }
        }

        /// <summary></summary>
        [Category("Appearance"), Description("Determines wether or not to make the border a flat sqaure"),
         Browsable(true)]
        public bool RoundedCorners
        {
            get { return rounded; }
            set
            {
                rounded = value;
                FireChange();
            }
        }

        [Category("Appearance"), Description("Gets or sets the border style"), Browsable(true)]
        public PlainBorderStyle Style
        {
            get { return style; }
            set
            {
                style = value;
                switch (style)
                {
                    case PlainBorderStyle.Flat:
                        pent = new Pen(new SolidBrush(color), 1f);
                        penb = pent;
                        cleart = new Pen(new SolidBrush(Color.FromArgb(64, color.R, color.G, color.B)));
                        clearb = cleart;
                        break;
                    case PlainBorderStyle.Raised:
                        pent = new Pen(new SolidBrush(SystemColors.ControlLightLight), 1f);
                        penb = new Pen(new SolidBrush(SystemColors.ControlDark), 1f);
                        cleart =
                            new Pen(
                                new SolidBrush(Color.FromArgb(64, SystemColors.ControlLightLight.R,
                                                              SystemColors.ControlLightLight.G,
                                                              SystemColors.ControlLightLight.B)));
                        clearb =
                            new Pen(
                                new SolidBrush(Color.FromArgb(64, SystemColors.ControlDark.R, SystemColors.ControlDark.G,
                                                              SystemColors.ControlDark.B)));
                        break;
                    case PlainBorderStyle.Sunken:
                        pent = new Pen(new SolidBrush(SystemColors.ControlDark), 1f);
                        penb = new Pen(new SolidBrush(SystemColors.ControlLightLight), 1f);
                        cleart =
                            new Pen(
                                new SolidBrush(Color.FromArgb(64, SystemColors.ControlDark.R, SystemColors.ControlDark.G,
                                                              SystemColors.ControlDark.B)));
                        clearb =
                            new Pen(
                                new SolidBrush(Color.FromArgb(64, SystemColors.ControlLightLight.R,
                                                              SystemColors.ControlLightLight.G,
                                                              SystemColors.ControlLightLight.B)));
                        break;
                }
                FireChange();
            }
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
            //try {
            //    box.Width -= 1;
            //    box.Height -= 1;
            //} catch {}
            if (rounded)
            {
                //// draws the left and right side (because they're shorter) to cover the corner pixels.
                //g.DrawLine(clear, box.X, 0, box.X, box.Height);
                //g.DrawLine(clear, box.Width, 0, box.Width, box.Height);

                g.DrawLine(cleart, box.X, box.Y, box.Right - 1, box.Y); // top
                g.DrawLine(cleart, box.X, box.Y, box.X, box.Bottom - 1); // left
                g.DrawLine(clearb, box.X, box.Bottom, box.Right, box.Bottom); // bottom
                g.DrawLine(clearb, box.Right, box.Y, box.Right, box.Bottom); // right

                //g.DrawRectangle(clear, box);
                g.DrawLine(pent, box.X + 1, box.Y, box.Right - 1, box.Y); // top
                g.DrawLine(penb, box.X + 1, box.Bottom, box.Right - 1, box.Bottom); // bottom
                g.DrawLine(pent, box.X, box.Y + 1, box.X, box.Bottom - 1); // left
                g.DrawLine(penb, box.Right, box.Y + 1, box.Right, box.Bottom - 1); // right
            }
            else
            {
                //g.DrawRectangle(pen, box);
                g.DrawLine(pent, box.X, box.Y, box.Right, box.Y); // top
                g.DrawLine(pent, box.X, box.Y, box.X, box.Bottom); // left
                g.DrawLine(penb, box.X, box.Bottom, box.Right, box.Bottom); // bottom
                g.DrawLine(penb, box.Right, box.Y, box.Right, box.Bottom); // right
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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            pent.Dispose();
            penb.Dispose();
            cleart.Dispose();
            clearb.Dispose();
        }
    }
}