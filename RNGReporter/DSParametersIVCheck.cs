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
    public partial class DSParametersIVCheck : Form
    {
        private BindingSource characteristicList;
        private uint[] maxstats;

        private uint[] minstats;
        private BindingSource natureList;
        private BindingSource pokemonList;

        public DSParametersIVCheck()
        {
            InitializeComponent();
        }

        public uint[] MinStats
        {
            get { return minstats; }
        }

        public uint[] MaxStats
        {
            get { return maxstats; }
        }

        private void DSParametersIVCheck_Load(object sender, EventArgs e)
        {
            comboBoxPokemon.DisplayMember = "Key";
            comboBoxPokemon.ValueMember = "Value";

            pokemonList = new BindingSource(Pokemon.PokemonCollection(), null);
            comboBoxPokemon.DataSource = pokemonList;
            comboBoxPokemon.SelectedIndex = 0;

            comboBoxNature.DisplayMember = "Key";
            comboBoxNature.ValueMember = "Value";

            natureList = new BindingSource(Nature.NatureCollection(), null);
            comboBoxNature.DataSource = natureList;
            comboBoxNature.SelectedIndex = 0;

            characteristicList = new BindingSource(Characteristic.CharacteristicCollection(), null);
            comboBoxCharacteristic.DataSource = characteristicList;
            comboBoxCharacteristic.SelectedIndex = 0;

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

            comboBoxNature.Font = font;
            comboBoxCharacteristic.Font = font;
            comboBoxPokemon.Font = font;

            natureList.ResetBindings(false);
            pokemonList.ResetBindings(false);
            characteristicList.ResetBindings(false);
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            uint hp = 0;
            uint atk = 0;
            uint def = 0;
            uint spa = 0;
            uint spd = 0;
            uint spe = 0;

            uint level = 1;

            var pokemon = (Pokemon) comboBoxPokemon.SelectedValue;
            var nature = (Nature) comboBoxNature.SelectedValue;

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

            if (maskedTextBoxLevel.Text != "")
                level = uint.Parse(maskedTextBoxLevel.Text);

            var stats = new[] {hp, atk, def, spa, spd, spe};

            Characteristic characteristic = null;

            if (comboBoxCharacteristic.SelectedItem.ToString() != "NONE")
            {
                characteristic = (Characteristic) comboBoxCharacteristic.SelectedItem;
            }

            var ivCheck = new IVCheck(pokemon, level, nature, characteristic, stats);

            minstats = new uint[6];
            maxstats = new uint[6];

            for (int statCount = 0; statCount < 6; statCount++)
            {
                if (ivCheck.Possibilities[statCount].Count == 0)
                {
                    buttonOk.Enabled = false;
                    break;
                }

                minstats[statCount] = ivCheck.Possibilities[statCount][0];
                maxstats[statCount] = ivCheck.Possibilities[statCount][ivCheck.Possibilities[statCount].Count - 1];
                buttonOk.Enabled = true;
            }

            //  Get the results back and display them to the user
            textBoxResults.Text = ivCheck.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            uint count = 1;

            for (int statCount = 0; statCount < 6; statCount++)
            {
                uint possibilities = maxstats[statCount] - minstats[statCount] + 1;
                count = count*possibilities;
            }

            if (count > 200)
            {
                MessageBox.Show(
                    "The IV ranges you have listed produce a large amount of IV combinations.  It is recommended that you narrow down the IVs to avoid false positives in parameter searches.");
            }
        }
    }
}