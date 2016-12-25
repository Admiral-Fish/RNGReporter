using System;
using System.Drawing;

namespace RNGReporter
{
    public interface IGlossPainter : IDisposable
    {
        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="g"></param>
        void PaintGloss(Rectangle box, Graphics g);

        /// <summary></summary>
        void Resize(Rectangle box);

        /// <summary></summary>
        event EventHandler PropertiesChanged;
    }
}