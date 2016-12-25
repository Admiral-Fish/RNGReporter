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

namespace RNGReporter
{
    public partial class SearchIVs : Form
    {
        private readonly bool isRoamer;
        private readonly int[] returnArray = new int[6];

        private string returnIVs = "";

        public SearchIVs(bool roamer)
        {
            InitializeComponent();
            isRoamer = roamer;
        }

        public int[] ReturnArray
        {
            get { return returnArray; }
        }

        public string ReturnIVs
        {
            get { return returnIVs; }
            set { returnIVs = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            int result = 0;

            if (!int.TryParse(comboBoxHP.Text, out result))
            {
                comboBoxHP.Text = "0";
            }
            returnIVs += comboBoxHP.Text + ", ";
            returnArray[0] = result;
            if (!int.TryParse(comboBoxAtk.Text, out result))
            {
                comboBoxAtk.Text = "0";
            }
            returnIVs += comboBoxAtk.Text + ", ";
            returnArray[1] = result;
            if (!int.TryParse(comboBoxDef.Text, out result))
            {
                comboBoxDef.Text = "0";
            }
            returnIVs += comboBoxDef.Text + ", ";
            returnArray[2] = result;
            if (!isRoamer)
            {
                if (!int.TryParse(comboBoxSpAtk.Text, out result))
                {
                    comboBoxSpAtk.Text = "0";
                }
                returnIVs += comboBoxSpAtk.Text + ", ";
                returnArray[3] = result;
                if (!int.TryParse(comboBoxSpDef.Text, out result))
                {
                    comboBoxSpDef.Text = "0";
                }
                returnIVs += comboBoxSpDef.Text + ", ";
                returnArray[4] = result;
                if (!int.TryParse(comboBoxSpeed.Text, out result))
                {
                    comboBoxSpeed.Text = "0";
                }
                returnIVs += comboBoxSpeed.Text;
                returnArray[5] = result;
            }
            else
            {
                if (!int.TryParse(comboBoxSpDef.Text, out result))
                {
                    comboBoxSpDef.Text = "0";
                }
                returnIVs += comboBoxSpDef.Text + ", ";
                returnArray[3] = result;
                if (!int.TryParse(comboBoxSpeed.Text, out result))
                {
                    comboBoxSpeed.Text = "0";
                }
                returnIVs += comboBoxSpeed.Text + ", ";
                returnArray[4] = result;
                if (!int.TryParse(comboBoxSpAtk.Text, out result))
                {
                    comboBoxSpAtk.Text = "0";
                }
                returnIVs += comboBoxSpAtk.Text;
                returnArray[5] = result;
            }
        }
    }
}