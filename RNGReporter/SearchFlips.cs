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
using System.Collections.Generic;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class SearchFlips : Form
    {
        private readonly List<Adjacent> _adjacents;
        private readonly string[] returnArray = new string[10];

        private string returnFlips = "";

        public SearchFlips()
        {
            InitializeComponent();
        }

        public SearchFlips(List<Adjacent> adjacents) : this()
        {
            _adjacents = adjacents;
        }

        public string[] ReturnArray
        {
            get { return returnArray; }
        }

        public string ReturnFlips
        {
            get { return returnFlips; }
            set { returnFlips = value; }
        }

        public List<Adjacent> Possible { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
        }

        private void btnHeads_Click(object sender, EventArgs e)
        {
            AddLetter("H");
        }

        private void btnTails_Click(object sender, EventArgs e)
        {
            AddLetter("T");
        }

        private void AddLetter(string s)
        {
            txtFlips.Text += txtFlips.Text == "" ? s : ", " + s;
        }

        private void txtFlips_TextChanged(object sender, EventArgs e)
        {
            UpdatePossible();
        }

        private void UpdatePossible()
        {
            if (_adjacents == null || _adjacents.Count == 0) return;
            Possible = _adjacents.FindAll(HasFlips);
            labelResults.Text = "Possible Results: " + Possible.Count;
        }

        private bool HasFlips(Adjacent adjacent)
        {
            return adjacent.Flips.Contains(txtFlips.Text);
        }
    }
}