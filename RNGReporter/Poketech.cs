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

using System.Windows.Forms;

namespace RNGReporter
{
    public partial class Poketech : Form
    {
        public Poketech(uint offset)
        {
            InitializeComponent();

            //labelOffset.Text = offset.ToString();

            uint coin = 0;
            uint happy = 0;
            uint target = 0;

            if (offset < 13)
            {
                coin = offset - 1;

                labelNote.Text = "DO NOT SWITCH TO THE HAPPINESS APPLICATION AT ALL";
            }
            else
            {
                target = offset - 13;

                happy = target/12;
                coin = target%12;

                if (happy == 0)
                {
                    labelNote.Text = "SWITCH TO THE HAPPINESS APPLICATION ONCE BUT DO NOT CLICK";
                }
            }

            labelHappy.Text = happy.ToString();
            labelCoin.Text = coin.ToString();
        }
    }
}