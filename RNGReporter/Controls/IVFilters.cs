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
using RNGReporter.Objects;

namespace RNGReporter.Controls
{
    public partial class IVFilters : UserControl
    {
        private IVFilter _ivFilter;

        public IVFilters()
        {
            InitializeComponent();
            IVFilter = new IVFilter();
        }

        internal IVFilter IVFilter
        {
            get { return _ivFilter; }
            private set
            {
                _ivFilter = value;
                OnFiltersChanged(null);
            }
        }

        public event EventHandler FiltersChanged;

        public void OnFiltersChanged(EventArgs e)
        {
            EventHandler handler = FiltersChanged;
            if (handler != null) handler(this, e);
        }

        private void IVFilters_Load(object sender, EventArgs e)
        {
            cbHP.SelectedIndex = 0;
            cbAtk.SelectedIndex = 0;
            cbDef.SelectedIndex = 0;
            cbSpA.SelectedIndex = 0;
            cbSpD.SelectedIndex = 0;
            cbSpe.SelectedIndex = 0;
        }

        #region Button Filters

        private void btn31HP_Click(object sender, EventArgs e)
        {
            txtHP.Text = "31";
            cbHP.SelectedIndex = 1;
        }

        private void btn31Atk_Click(object sender, EventArgs e)
        {
            txtAtk.Text = "31";
            cbAtk.SelectedIndex = 1;
        }

        private void btn31Def_Click(object sender, EventArgs e)
        {
            txtDef.Text = "31";
            cbDef.SelectedIndex = 1;
        }

        private void btn31SpA_Click(object sender, EventArgs e)
        {
            txtSpA.Text = "31";
            cbSpA.SelectedIndex = 1;
        }

        private void btn31SpD_Click(object sender, EventArgs e)
        {
            txtSpD.Text = "31";
            cbSpD.SelectedIndex = 1;
        }

        private void btn31Spe_Click(object sender, EventArgs e)
        {
            txtSpe.Text = "31";
            cbSpe.SelectedIndex = 1;
        }

        private void btn30HP_Click(object sender, EventArgs e)
        {
            txtHP.Text = "30";
            cbHP.SelectedIndex = 1;
        }

        private void btn30Atk_Click(object sender, EventArgs e)
        {
            txtAtk.Text = "30";
            cbAtk.SelectedIndex = 1;
        }

        private void btn30Def_Click(object sender, EventArgs e)
        {
            txtDef.Text = "30";
            cbDef.SelectedIndex = 1;
        }

        private void btn30SpA_Click(object sender, EventArgs e)
        {
            txtSpA.Text = "30";
            cbSpA.SelectedIndex = 1;
        }

        private void btn30SpD_Click(object sender, EventArgs e)
        {
            txtSpD.Text = "30";
            cbSpD.SelectedIndex = 1;
        }

        private void btn30Spe_Click(object sender, EventArgs e)
        {
            txtSpe.Text = "30";
            cbSpe.SelectedIndex = 1;
        }

        private void btnG30HP_Click(object sender, EventArgs e)
        {
            txtHP.Text = "30";
            cbHP.SelectedIndex = 2;
        }

        private void btnG30Atk_Click(object sender, EventArgs e)
        {
            txtAtk.Text = "30";
            cbAtk.SelectedIndex = 2;
        }

        private void btnG30Def_Click(object sender, EventArgs e)
        {
            txtDef.Text = "30";
            cbDef.SelectedIndex = 2;
        }

        private void btnG30SpA_Click(object sender, EventArgs e)
        {
            txtSpA.Text = "30";
            cbSpA.SelectedIndex = 2;
        }

        private void btnG30SpD_Click(object sender, EventArgs e)
        {
            txtSpD.Text = "30";
            cbSpD.SelectedIndex = 2;
        }

        private void btnG30Spe_Click(object sender, EventArgs e)
        {
            txtSpe.Text = "30";
            cbSpe.SelectedIndex = 2;
        }

        private void btnClearHP_Click(object sender, EventArgs e)
        {
            cbHP.SelectedIndex = 0;
            txtHP.Text = "";
        }

        private void btnClearAtk_Click(object sender, EventArgs e)
        {
            cbAtk.SelectedIndex = 0;
            txtAtk.Text = "";
        }

        private void btnClearDef_Click(object sender, EventArgs e)
        {
            cbDef.SelectedIndex = 0;
            txtDef.Text = "";
        }

        private void btnClearSpA_Click(object sender, EventArgs e)
        {
            cbSpA.SelectedIndex = 0;
            txtSpA.Text = "";
        }

        private void btnClearSpD_Click(object sender, EventArgs e)
        {
            cbSpD.SelectedIndex = 0;
            txtSpD.Text = "";
        }

        private void btnClearSpe_Click(object sender, EventArgs e)
        {
            cbSpe.SelectedIndex = 0;
            txtSpe.Text = "";
        }

        public void changeIVs(String[] ivs)
        {
            txtHP.Text = ivs[0];
            cbHP.SelectedIndex = 1;
            txtAtk.Text = ivs[1];
            cbAtk.SelectedIndex = 1;
            txtDef.Text = ivs[2];
            cbDef.SelectedIndex = 1;
            txtSpA.Text = ivs[3];
            cbSpA.SelectedIndex = 1;
            txtSpD.Text = ivs[4];
            cbSpD.SelectedIndex = 1;
            txtSpe.Text = ivs[5];
            cbSpe.SelectedIndex = 1;
        }

        #endregion

        #region Value Changed

        private void txtHP_TextChanged(object sender, EventArgs e)
        {
            uint hp;
            if (uint.TryParse(txtHP.Text, out hp))
                IVFilter.hpValue = hp;
            OnFiltersChanged(e);
        }

        private void txtAtk_TextChanged(object sender, EventArgs e)
        {
            uint atk;
            if (uint.TryParse(txtAtk.Text, out atk))
                IVFilter.atkValue = atk;
        }

        private void txtDef_TextChanged(object sender, EventArgs e)
        {
            uint def;
            if (uint.TryParse(txtDef.Text, out def))
                IVFilter.defValue = def;
            OnFiltersChanged(e);
        }

        private void txtSpA_TextChanged(object sender, EventArgs e)
        {
            uint spa;
            if (uint.TryParse(txtSpA.Text, out spa))
                IVFilter.spaValue = spa;
            OnFiltersChanged(e);
        }

        private void txtSpD_TextChanged(object sender, EventArgs e)
        {
            uint spd;
            if (uint.TryParse(txtSpD.Text, out spd))
                IVFilter.spdValue = spd;
            OnFiltersChanged(e);
        }

        private void txtSpe_TextChanged(object sender, EventArgs e)
        {
            uint spe;
            if (uint.TryParse(txtSpe.Text, out spe))
                IVFilter.speValue = spe;
            OnFiltersChanged(e);
        }

        private void cbHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.hpCompare = (CompareType) cbHP.SelectedIndex;
            OnFiltersChanged(e);
        }

        private void cbAtk_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.atkCompare = (CompareType) cbAtk.SelectedIndex;
            OnFiltersChanged(e);
        }

        private void cbDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.defCompare = (CompareType) cbDef.SelectedIndex;
            OnFiltersChanged(e);
        }

        private void cbSpA_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.spaCompare = (CompareType) cbSpA.SelectedIndex;
            OnFiltersChanged(e);
        }

        private void cbSpD_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.spdCompare = (CompareType) cbSpD.SelectedIndex;
            OnFiltersChanged(e);
        }

        private void cbSpe_SelectedIndexChanged(object sender, EventArgs e)
        {
            IVFilter.speCompare = (CompareType) cbSpe.SelectedIndex;
            OnFiltersChanged(e);
        }

        #endregion
    }
}