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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class GenderRatioLookup : Form
    {
        private uint genderRatio;
        private BindingSource pokemonList;

        public GenderRatioLookup()
        {
            InitializeComponent();
        }

        public uint GenderRatio
        {
            get { return genderRatio; }
        }

        private void GenderRatioLookup_Load(object sender, EventArgs e)
        {
            comboBoxPokemon.DisplayMember = "Key";
            comboBoxPokemon.ValueMember = "Value";

            pokemonList = new BindingSource(Pokemon.PokemonCollection(), null);
            comboBoxPokemon.DataSource = pokemonList;
            comboBoxPokemon.SelectedIndex = 0;

            Settings.Default.PropertyChanged += ChangeLanguage;
            SetLanguage();
        }

        public void ChangeLanguage(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language")
            {
                SetLanguage();
            }
        }

        public void SetLanguage()
        {
            Font font;
            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    font = new Font("Meiryo", 7.25F);
                    if (font.Name != "Meiryo")
                    {
                        font = new Font("Arial Unicode MS", 8.25F);
                        if (font.Name != "Arial Unicode MS")
                        {
                            font = new Font("MS Mincho", 8.25F);
                        }
                    }
                    break;
                case (Language.Korean):
                    font = new Font("Malgun Gothic", 8.25F);
                    if (font.Name != "Malgun Gothic")
                    {
                        font = new Font("Gulim", 9.25F);
                        if (font.Name != "Gulim")
                        {
                            font = new Font("Arial Unicode MS", 8.25F);
                        }
                    }
                    break;
                default:
                    font = DefaultFont;
                    break;
            }

            comboBoxPokemon.Font = font;
            pokemonList.ResetBindings(false);
        }

        private void comboBoxPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            genderRatio = ((Pokemon) (comboBoxPokemon.SelectedItem)).GenderRatio;

            switch (genderRatio)
            {
                case 127:
                    labelRatio.Text = "50% M \\ 50% F";
                    break;
                case 191:
                    labelRatio.Text = "25% M \\ 75% F";
                    break;
                case 63:
                    labelRatio.Text = "75% M \\ 25% F";
                    break;
                case 31:
                    labelRatio.Text = "87.5% M \\ 12.5% F";
                    break;
                case 0:
                    labelRatio.Text = "100% M";
                    break;
                case 254:
                    labelRatio.Text = "100% F";
                    break;
                case 255:
                    labelRatio.Text = "Genderless";
                    break;
            }
        }
    }
}