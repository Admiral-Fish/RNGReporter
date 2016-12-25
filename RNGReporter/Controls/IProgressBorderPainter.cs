using System;
using System.Drawing;

namespace RNGReporter
{
    /// <summary></summary>
    public interface IProgressBorderPainter : IDisposable
    {
        /// <summary></summary>
        int BorderWidth { get; }

        /// <summary></summary>
        /// <param name="box"></param>
        /// <param name="gr"></param>
        void PaintBorder(Rectangle box, Graphics gr);

        /// <summary></summary>
        void Resize(Rectangle box);

        /// <summary></summary>
        event EventHandler PropertiesChanged;
    }
}