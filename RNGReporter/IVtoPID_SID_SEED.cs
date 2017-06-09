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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class IVtoPID_SID_SEED : Form
    {
        private bool seedSet;
        private bool sidSet;
        private uint tid;

        public IVtoPID_SID_SEED()
        {
            InitializeComponent();
        }

        public uint Tid
        {
            get { return tid; }
            set { tid = value; }
        }

        public bool SidSet
        {
            get { return sidSet; }
            set { sidSet = value; }
        }

        public uint ReturnSid { get; set; }

        public bool SeedSet
        {
            get { return seedSet; }
            set { seedSet = value; }
        }

        public uint ReturnSeed { get; set; }

        private void IVtoPID_SID_SEED_Load(object sender, EventArgs e)
        {
            comboBoxNature.DataSource = Nature.NatureDropDownCollectionSearchNatures();
            SetLanguage();
            comboBoxNature.SelectedIndex = 0;
            dataGridViewValues.AutoGenerateColumns = false;

            maskedTextBoxID.Text = tid.ToString();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            if (maskedTextBoxHP.Text != "")
                hp = uint.Parse(maskedTextBoxHP.Text);
            if (maskedTextBoxAtk.Text != "")
                atk = uint.Parse(maskedTextBoxAtk.Text);
            if (maskedTextBoxDef.Text != "")
                def = uint.Parse(maskedTextBoxDef.Text);
            if (maskedTextBoxSpA.Text != "")
                spa = uint.Parse(maskedTextBoxSpA.Text);
            if (maskedTextBoxSpD.Text != "")
                spd = uint.Parse(maskedTextBoxSpD.Text);
            if (maskedTextBoxSpe.Text != "")
                spe = uint.Parse(maskedTextBoxSpe.Text);

            int test = ((Nature) comboBoxNature.SelectedItem).Number;
            var nature = (uint) test;

            if (maskedTextBoxID.Text != "")
                tid = uint.Parse(maskedTextBoxID.Text);

            List<Seed> seeds =
                IVtoSeed.GetSeeds(
                    hp,
                    atk,
                    def,
                    spa,
                    spd,
                    spe,
                    nature,
                    tid);

            dataGridViewValues.DataSource = seeds;
        }

        private void setSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Get currently selected item
            if (dataGridViewValues.SelectedRows.Count > 0)
            {
                var seed = (Seed) dataGridViewValues.SelectedRows[0].DataBoundItem;

                ReturnSeed = seed.MonsterSeed;
                SeedSet = true;

                labelSeed.Text = seed.MonsterSeed.ToString("X");
                Clipboard.SetText(ReturnSeed.ToString("X"));
            }
        }

        private void setSIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Get currently selected item
            if (dataGridViewValues.SelectedRows.Count > 0)
            {
                var seed = (Seed) dataGridViewValues.SelectedRows[0].DataBoundItem;

                ReturnSid = seed.Sid;
                sidSet = true;

                labelSid.Text = seed.Sid.ToString();
                Clipboard.SetText(ReturnSid.ToString());
            }
        }

        private void copyPIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Get currently selected item
            if (dataGridViewValues.SelectedRows.Count > 0)
            {
                var seed = (Seed)dataGridViewValues.SelectedRows[0].DataBoundItem;
                Clipboard.SetText(seed.Pid.ToString("X"));
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            sidSet = false;
            seedSet = false;
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo Hti;

            if (e.Button == MouseButtons.Right)
            {
                Hti = dataGridViewValues.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewValues.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        public void SetLanguage()
        {
            var CellStyle = new DataGridViewCellStyle();
            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    CellStyle.Font = new Font("Meiryo", 7.25F);
                    if (CellStyle.Font.Name != "Meiryo")
                    {
                        CellStyle.Font = new Font("Arial Unicode MS", 8.25F);
                        if (CellStyle.Font.Name != "Arial Unicode MS")
                        {
                            CellStyle.Font = new Font("MS Mincho", 8.25F);
                        }
                    }
                    break;
                case (Language.Korean):
                    CellStyle.Font = new Font("Malgun Gothic", 8.25F);
                    if (CellStyle.Font.Name != "Malgun Gothic")
                    {
                        CellStyle.Font = new Font("Gulim", 9.25F);
                        if (CellStyle.Font.Name != "Gulim")
                        {
                            CellStyle.Font = new Font("Arial Unicode MS", 8.25F);
                        }
                    }
                    break;
                default:
                    CellStyle.Font = DefaultFont;
                    break;
            }
            comboBoxNature.Font = CellStyle.Font;
        }
    }
}