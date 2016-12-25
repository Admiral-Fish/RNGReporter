﻿/*
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
using System.ComponentModel;
using System.Windows.Forms;

namespace RNGReporter.Controls
{
    internal class MaskedTextBox2 : MaskedTextBox
    {
        public MaskedTextBox2()
        {
            AutoSize = false;
        }

        [Browsable(true)]
        public bool Hex { get; set; }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            Focus();
            SelectAll();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // prevents users from creating blank spaces in MaskedTextBoxes
            // which trip and cause an exception when parsed by int.Parse
            if (SelectionStart > Text.Length)
                SelectionStart = Text.Length;

            if (e.KeyChar == ' ') e.KeyChar = (char) 0;

            if (SelectionStart < Mask.Length)
            {
                if (Hex && Mask[SelectionStart].Equals('A'))
                {
                    if (e.KeyChar != (char) Keys.Back && !char.IsControl(e.KeyChar))
                    {
                        if ((e.KeyChar >= 'a') && (e.KeyChar <= 'f'))
                        {
                            e.KeyChar = char.ToUpper(e.KeyChar);
                        }
                        else if (((e.KeyChar >= 'A') && (e.KeyChar <= 'F')) ||
                                 ((e.KeyChar >= '0') && (e.KeyChar <= '9')))
                        {
                        }
                        else
                            e.KeyChar = (char) 0;
                    }
                }
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Hex)
            {
                string replace = "";
                for (int charPos = 0; charPos < Text.Length; charPos++)
                {
                    if (Mask[charPos].Equals('A'))
                    {
                        if (((Text[charPos] >= 'a') && (Text[charPos] <= 'f')) ||
                            ((Text[charPos] >= 'A') && (Text[charPos] <= 'F')) ||
                            ((Text[charPos] >= '0') && (Text[charPos] <= '9')))
                        {
                            replace = replace + Text[charPos];
                        }
                    }
                }
                Text = replace;
            }

            base.OnTextChanged(e);
        }
    }
}