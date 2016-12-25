/*
 * This file is part of RNG Reporter
 * Copyright (C) 2012 by Bill Young, Mike Suleski, and Andrew Ringer
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using System;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace RNGReporter.Objects
{
    public class Activation
    {
        private readonly Keys[] _code =
            {
                Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left,
                Keys.Right, Keys.B, Keys.A
            };

        private readonly int _codeLength;

        private readonly Timer _quantum = new Timer();
        private readonly int _sequenceMax;
        private int _sequenceIndex;

        public Activation()
        {
            _codeLength = _code.Length - 1;
            _sequenceMax = _code.Length;

            _quantum.Interval = 6000; //ms before reset
            _quantum.Elapsed += timeout;
        }

        public bool IsCompletedBy(Keys key)
        {
            _quantum.Start();

            _sequenceIndex %= _sequenceMax;
            _sequenceIndex = (_code[_sequenceIndex] == key) ? ++_sequenceIndex : 0;

            return _sequenceIndex > _codeLength;
        }

        private void timeout(object o, EventArgs e)
        {
            _quantum.Stop();
            _sequenceIndex = 0;
        }
    }
}