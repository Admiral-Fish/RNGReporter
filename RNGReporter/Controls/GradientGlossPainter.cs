using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RNGReporter
{
    /// <summary></summary>
    //[ToolboxBitmapAttribute(typeof(RNGReporter.GradientGlossPainter), "Icons.GradientGlossPainter.ico")]
    public class GradientGlossPainter : ChainedGlossPainter
    {
        private float angle = 90f;
        private Brush brush;
        private Color color = Color.White;
        private int highAlpha = 240;
        private int lowAlpha;
        private int percent = 50;
        private GlossStyle style = GlossStyle.Bottom;

        /// <summary></summary>
        [Category("Appearance"), Description("Gets or sets the style for this progress gloss"), Browsable(true)]
        public GlossStyle Style
        {
            get { return style; }
            set
            {
                style = value;
                FireChange();
            }
        }

        /// <summary></summary>
        [Category("Appearance"), Description("Gets or sets the percentage of surface this gloss should cover"),
         Browsable(true)]
        public int PercentageCovered
        {
            get { return percent; }
            set
            {
                percent = value;
                FireChange();
            }
        }

        /// <summary></summary>
        [Category("Appearance"), Description("Gets or sets color to gloss"), Browsable(true)]
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                FireChange();
            }
        }

        /// <summary></summary>
        [Category("Blending"), Description("Gets or sets the high alpha value"), Browsable(true)]
        public int AlphaHigh
        {
            get { return highAlpha; }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("Alpha values must be between 0 and 255.");
                }
                highAlpha = value;
                FireChange();
            }
        }

        /// <summary></summary>
        [Category("Blending"), Description("Gets or sets the low alpha value"), Browsable(true)]
        public int AlphaLow
        {
            get { return lowAlpha; }
            set
            {
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("Alpha values must be between 0 and 255.");
                }
                lowAlpha = value;
                FireChange();
            }
        }

        /// <summary></summary>
        [Category("Blending"), Description("Gets or sets angle for the gradient"), Browsable(true)]
        public float Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                FireChange();
            }
        }

        protected override void PaintThisGloss(Rectangle box, Graphics g)
        {
            var y = (int) ((box.Height*(float) percent)/100f);
            if (box.Y + y > box.Height)
            {
                y = box.Height;
            }

            Rectangle cover = box;
            switch (style)
            {
                case GlossStyle.Bottom:
                    int start = box.Height + box.Y - y;
                    cover = new Rectangle(box.X, start - 1, box.Width /*- 1*/, box.Bottom - start);
                    break;
                case GlossStyle.Top:
                    cover = new Rectangle(box.X, box.Y - 1, box.Width /*- 1*/, y + 2);
                    break;
                case GlossStyle.Both:
                    cover = box;
                    break;
            }

            Color hcolor = Color.FromArgb(highAlpha, color.R, color.G, color.B);
            Color lcolor = Color.FromArgb(lowAlpha, color.R, color.G, color.B);
            brush = new LinearGradientBrush(cover, hcolor, lcolor, angle, true);
            g.FillRectangle(brush, cover);
            //g.DrawRectangle(Pens.Red, cover);
        }

        protected override void ResizeThis(Rectangle box)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (brush != null)
            {
                brush.Dispose();
            }
        }
    }
}