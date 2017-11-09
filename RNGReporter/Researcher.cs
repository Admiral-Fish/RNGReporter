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
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using RNGReporter.Objects;

namespace RNGReporter
{
    public partial class Researcher : Form
    {
        private string columnHover;
        private List<int> columns;
        private StringBuilder sb;

        public Researcher()
        {
            InitializeComponent();
        }

        private void Researcher_Load(object sender, EventArgs e)
        {
            dataGridViewValues.AutoGenerateColumns = false;
            Column64Bit.DefaultCellStyle.Format = "X16";
            Column32Bit.DefaultCellStyle.Format = "X8";
            Column32BitHigh.DefaultCellStyle.Format = "X8";
            Column32BitLow.DefaultCellStyle.Format = "X8";
            Column16BitHigh.DefaultCellStyle.Format = "X4";
            Column16BitLow.DefaultCellStyle.Format = "X4";

            comboBoxLValue1.SelectedIndex = 0;
            comboBoxLValue2.SelectedIndex = 0;
            comboBoxLValue3.SelectedIndex = 0;
            comboBoxLValue4.SelectedIndex = 0;
            comboBoxLValue5.SelectedIndex = 0;
            comboBoxLValue6.SelectedIndex = 0;
            comboBoxLValue7.SelectedIndex = 0;
            comboBoxLValue8.SelectedIndex = 0;
            comboBoxLValue9.SelectedIndex = 0;
            comboBoxLValue10.SelectedIndex = 0;

            comboBoxOperator1.SelectedIndex = 0;
            comboBoxOperator2.SelectedIndex = 0;
            comboBoxOperator3.SelectedIndex = 0;
            comboBoxOperator4.SelectedIndex = 0;
            comboBoxOperator5.SelectedIndex = 0;
            comboBoxOperator6.SelectedIndex = 0;
            comboBoxOperator7.SelectedIndex = 0;
            comboBoxOperator8.SelectedIndex = 0;
            comboBoxOperator9.SelectedIndex = 0;
            comboBoxOperator10.SelectedIndex = 0;

            comboBoxRValue2.SelectedIndex = 0;
            comboBoxRValue3.SelectedIndex = 0;
            comboBoxRValue4.SelectedIndex = 0;
            comboBoxRValue5.SelectedIndex = 0;
            comboBoxRValue6.SelectedIndex = 0;
            comboBoxRValue7.SelectedIndex = 0;
            comboBoxRValue8.SelectedIndex = 0;
            comboBoxRValue9.SelectedIndex = 0;
            comboBoxRValue10.SelectedIndex = 0;

            comboBoxRNG.SelectedIndex = 0;

            glassComboBox1.SelectedIndex = 0;

            columns = new List<int>();
            sb = new StringBuilder();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            //  Initial seed that we are going to used for our frame generation
            ulong seed = 0;
            if (textBoxSeed.Text != "")
            {
                seed = ulong.Parse(textBoxSeed.Text, NumberStyles.HexNumber);
            }

            uint maxFrames = 1000;
            if (maskedTextBoxMaxFrames.Text != "")
            {
                maxFrames = uint.Parse(maskedTextBoxMaxFrames.Text);
            }

            //  Generic RNG Interface
            IRNG rng = null;
            IRNG64 rng64 = null;

            //  Instantiate based on the type that
            //  the user has selected ------------
            if (radioButtonCommon.Checked)
            {
                switch (comboBoxRNG.SelectedIndex)
                {
                    case 0:
                        rng = new PokeRng((uint)seed);
                        break;
                    case 1:
                        rng = new PokeRngR((uint)seed);
                        break;
                    case 2:
                        // if the given seed is 64 bit, remove the lower 32 bits.
                        if (seed >= 0x100000000) seed >>= 32;
                        rng = new MersenneTwister((uint)seed);
                        break;
                    case 3:
                        rng64 = new BWRng(seed);
                        break;
                    case 4:
                        rng64 = new BWRngR(seed);
                        break;
                    case 5:
                        rng = new XdRng((uint)seed);
                        break;
                    case 6:
                        rng = new XdRngR((uint)seed);
                        break;
                    case 7:
                        rng = new ARng((uint)seed);
                        break;
                    case 8:
                        rng = new ARngR((uint)seed);
                        break;
                    case 9:
                        rng = new GRng((uint)seed);
                        break;
                    case 10:
                        rng = new GRngR((uint)seed);
                        break;
                    case 11:
                        rng = new EncounterRng((uint)seed);
                        break;
                    case 12:
                        rng = new EncounterRngR((uint)seed);
                        break;
                    case 13:
                        rng = new MersenneTwisterUntempered((int)seed);
                        break;
                    case 14:
                        rng = new MersenneTwisterFast((uint)seed, (int)maxFrames);
                        break;
                    case 15:
                        rng = new MersenneTwisterTable((uint)seed);
                        break;
                }
            }

            if (radioButtonCustom.Checked)
            {
                //  Check to see if we had valid values in the entry fields, and if so
                //  covert them over to the adding and multiplier so we can instantiate
                //  a generic LCRNG.

                ulong mult = 0;
                ulong add = 0;

                if (textBoxMult.Text != "")
                {
                    mult = ulong.Parse(textBoxMult.Text, NumberStyles.HexNumber);
                }

                if (textBoxAdd.Text != "")
                {
                    add = ulong.Parse(textBoxAdd.Text, NumberStyles.HexNumber);
                }

                if (checkBox64bit.Checked)
                {
                    rng64 = new GenericRng64(seed, mult, add);
                }
                else
                {
                    rng = new GenericRng((uint)seed, (uint)mult, (uint)add);
                }
            }

            //  This is our collection of operators. At some point, if this gets
            //  fancy, we may want to break it off and have it in it's own class
            var calculators = new Dictionary<string, Calculator>();

            calculators["%"] = (x, y) => x % y;
            calculators["*"] = (x, y) => x * y;
            calculators["/"] = (x, y) => y == 0 ? 0 : x / y;
            calculators["&"] = (x, y) => x & y;
            calculators["^"] = (x, y) => x ^ y;
            calculators["|"] = (x, y) => x | y;
            calculators["+"] = (x, y) => x + y;
            calculators["-"] = (x, y) => x - y;
            calculators[">>"] = (x, y) => x >> (int)y;
            calculators["<<"] = (x, y) => x << (int)y;

            bool calcCustom1 =
                textBoxRValue1.Text != "" &&
                (string)comboBoxOperator1.SelectedItem != null &&
                (string)comboBoxLValue1.SelectedItem != null;
            bool calcCustom2 =
                (textBoxRValue2.Text != "" || comboBoxRValue2.SelectedIndex != 0) &&
                (string)comboBoxOperator2.SelectedItem != null &&
                (string)comboBoxLValue2.SelectedItem != null;
            bool calcCustom3 =
                (textBoxRValue3.Text != "" || comboBoxRValue3.SelectedIndex != 0) &&
                (string)comboBoxOperator3.SelectedItem != null &&
                (string)comboBoxLValue3.SelectedItem != null;
            bool calcCustom4 =
                (textBoxRValue4.Text != "" || comboBoxRValue4.SelectedIndex != 0) &&
                (string)comboBoxOperator4.SelectedItem != null &&
                (string)comboBoxLValue4.SelectedItem != null;
            bool calcCustom5 =
                (textBoxRValue5.Text != "" || comboBoxRValue5.SelectedIndex != 0) &&
                (string)comboBoxOperator5.SelectedItem != null &&
                (string)comboBoxLValue5.SelectedItem != null;
            bool calcCustom6 =
                (textBoxRValue6.Text != "" || comboBoxRValue6.SelectedIndex != 0) &&
                (string)comboBoxOperator6.SelectedItem != null &&
                (string)comboBoxLValue6.SelectedItem != null;
            bool calcCustom7 =
                (textBoxRValue7.Text != "" || comboBoxRValue7.SelectedIndex != 0) &&
                (string)comboBoxOperator7.SelectedItem != null &&
                (string)comboBoxLValue7.SelectedItem != null;
            bool calcCustom8 =
                (textBoxRValue8.Text != "" || comboBoxRValue8.SelectedIndex != 0) &&
                (string)comboBoxOperator8.SelectedItem != null &&
                (string)comboBoxLValue8.SelectedItem != null;
            bool calcCustom9 =
                (textBoxRValue9.Text != "" || comboBoxRValue9.SelectedIndex != 0) &&
                (string)comboBoxOperator9.SelectedItem != null &&
                (string)comboBoxLValue9.SelectedItem != null;
            bool calcCustom10 =
                (textBoxRValue10.Text != "" || comboBoxRValue10.SelectedIndex != 0) &&
                (string)comboBoxOperator10.SelectedItem != null &&
                (string)comboBoxLValue10.SelectedItem != null;

            //  Build our custom item transform classes so that we can use them in
            //  the future without having to do a parse of all of the items.

            ulong customRValue1;
            ulong customRValue2;
            ulong customRValue3;
            ulong customRValue4;
            ulong customRValue5;
            ulong customRValue6;
            ulong customRValue7;
            ulong customRValue8;
            ulong customRValue9;
            ulong customRValue10;

            try
            {
                customRValue1 = (textBoxRValue1.Text == ""
                                     ? 0
                                     : (checkBoxCustom1Hex.Checked
                                            ? ulong.Parse(textBoxRValue1.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue1.Text)));
                customRValue2 = (textBoxRValue2.Text == ""
                                     ? 0
                                     : (checkBoxCustom2Hex.Checked
                                            ? ulong.Parse(textBoxRValue2.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue2.Text)));
                customRValue3 = (textBoxRValue3.Text == ""
                                     ? 0
                                     : (checkBoxCustom3Hex.Checked
                                            ? ulong.Parse(textBoxRValue3.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue3.Text)));
                customRValue4 = (textBoxRValue4.Text == ""
                                     ? 0
                                     : (checkBoxCustom4Hex.Checked
                                            ? ulong.Parse(textBoxRValue4.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue4.Text)));
                customRValue5 = (textBoxRValue5.Text == ""
                                     ? 0
                                     : (checkBoxCustom5Hex.Checked
                                            ? ulong.Parse(textBoxRValue5.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue5.Text)));
                customRValue6 = (textBoxRValue6.Text == ""
                                     ? 0
                                     : (checkBoxCustom6Hex.Checked
                                            ? ulong.Parse(textBoxRValue6.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue6.Text)));
                customRValue7 = (textBoxRValue7.Text == ""
                                     ? 0
                                     : (checkBoxCustom7Hex.Checked
                                            ? ulong.Parse(textBoxRValue7.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue7.Text)));
                customRValue8 = (textBoxRValue8.Text == ""
                                     ? 0
                                     : (checkBoxCustom8Hex.Checked
                                            ? ulong.Parse(textBoxRValue8.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue8.Text)));
                customRValue9 = (textBoxRValue9.Text == ""
                                     ? 0
                                     : (checkBoxCustom9Hex.Checked
                                            ? ulong.Parse(textBoxRValue9.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue9.Text)));
                customRValue10 = (textBoxRValue10.Text == ""
                                     ? 0
                                     : (checkBoxCustom10Hex.Checked
                                            ? ulong.Parse(textBoxRValue10.Text, NumberStyles.HexNumber)
                                            : ulong.Parse(textBoxRValue10.Text)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must check off the Hex box in order to calculate using hex values.", ex.Message);
                return;
            }

            Calculator custom1Calc = ((string)comboBoxOperator1.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator1.SelectedItem]);
            Calculator custom2Calc = ((string)comboBoxOperator2.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator2.SelectedItem]);
            Calculator custom3Calc = ((string)comboBoxOperator3.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator3.SelectedItem]);
            Calculator custom4Calc = ((string)comboBoxOperator4.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator4.SelectedItem]);
            Calculator custom5Calc = ((string)comboBoxOperator5.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator5.SelectedItem]);
            Calculator custom6Calc = ((string)comboBoxOperator6.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator6.SelectedItem]);
            Calculator custom7Calc = ((string)comboBoxOperator7.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator7.SelectedItem]);
            Calculator custom8Calc = ((string)comboBoxOperator8.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator8.SelectedItem]);
            Calculator custom9Calc = ((string)comboBoxOperator9.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator9.SelectedItem]);
            Calculator custom10Calc = ((string)comboBoxOperator10.SelectedItem == null
                                          ? null
                                          : calculators[(string)comboBoxOperator10.SelectedItem]);

            //  Decide on whether we are going to display each of these items as
            //  decimal or hex. Can be very useful either way, so it is an option.
            Custom1.DefaultCellStyle.Format = checkBoxCustom1Hex.Checked ? "X8" : "";
            Custom2.DefaultCellStyle.Format = checkBoxCustom2Hex.Checked ? "X8" : "";
            Custom3.DefaultCellStyle.Format = checkBoxCustom3Hex.Checked ? "X8" : "";
            Custom4.DefaultCellStyle.Format = checkBoxCustom4Hex.Checked ? "X8" : "";
            Custom5.DefaultCellStyle.Format = checkBoxCustom5Hex.Checked ? "X8" : "";
            Custom6.DefaultCellStyle.Format = checkBoxCustom6Hex.Checked ? "X8" : "";
            Custom7.DefaultCellStyle.Format = checkBoxCustom7Hex.Checked ? "X8" : "";
            Custom8.DefaultCellStyle.Format = checkBoxCustom8Hex.Checked ? "X8" : "";
            Custom9.DefaultCellStyle.Format = checkBoxCustom9Hex.Checked ? "X8" : "";
            Custom10.DefaultCellStyle.Format = checkBoxCustom10Hex.Checked ? "X8" : "";

            var frames = new List<FrameResearch>();

            bool rngIs64Bit = (comboBoxRNG.SelectedIndex == 3 || comboBoxRNG.SelectedIndex == 4 ||
                               checkBox64bit.Checked && radioButtonCustom.Checked);
            //  Loop through X times and create our research frames.
            for (uint cnt = 0; cnt < maxFrames; cnt++)
            {
                FrameResearch frame;
                if (!rngIs64Bit)
                {
                    Column64Bit.Visible = false;
                    Column32Bit.Visible = true;
                    Column32BitHigh.Visible = false;
                    Column32BitLow.Visible = false;

                    uint rngResult = rng.Next();

                    //  Start building the research frame that we are going to use
                    frame = new FrameResearch { RNG64bit = rngIs64Bit, FrameNumber = cnt + 1, Full32 = rngResult };
                }
                else
                {
                    Column64Bit.Visible = true;
                    Column32Bit.Visible = false;
                    Column32BitHigh.Visible = true;
                    Column32BitLow.Visible = true;

                    ulong rngResult = rng64.Next();

                    //  Start building the research frame that we are going to use
                    frame = new FrameResearch { RNG64bit = rngIs64Bit, FrameNumber = cnt + 1, Full64 = rngResult };
                }

                //  Call Custom 1 ////////////////////////////////////////////////////////////////
                if (calcCustom1)
                {
                    ulong customLValue1 = CustomCalcs(comboBoxLValue1, frame, frames);

                    if (!rngIs64Bit)
                        customLValue1 = (uint)customLValue1;

                    frame.Custom1 = custom1Calc(customLValue1, customRValue1);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 2 ////////////////////////////////////////////////////////////////
                if (calcCustom2)
                {
                    ulong customLValue2 = CustomCalcs(comboBoxLValue2, frame, frames);
                    if ((string)comboBoxRValue2.SelectedItem != "None")
                        customRValue2 = CustomCalcs(comboBoxRValue2, frame, frames);

                    if (!rngIs64Bit)
                        customLValue2 = (uint)customLValue2;

                    frame.Custom2 = custom2Calc(customLValue2, customRValue2);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 3 ////////////////////////////////////////////////////////////////
                if (calcCustom3)
                {
                    ulong customLValue3 = CustomCalcs(comboBoxLValue3, frame, frames);
                    if ((string)comboBoxRValue3.SelectedItem != "None")
                        customRValue3 = CustomCalcs(comboBoxRValue3, frame, frames);

                    if (!rngIs64Bit)
                        customLValue3 = (uint)customLValue3;

                    frame.Custom3 = custom3Calc(customLValue3, customRValue3);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 4 ////////////////////////////////////////////////////////////////
                if (calcCustom4)
                {
                    ulong customLValue4 = CustomCalcs(comboBoxLValue4, frame, frames);
                    if ((string)comboBoxRValue4.SelectedItem != "None")
                        customRValue4 = CustomCalcs(comboBoxRValue4, frame, frames);

                    if (!rngIs64Bit)
                        customLValue4 = (uint)customLValue4;

                    frame.Custom4 = custom4Calc(customLValue4, customRValue4);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 5 ////////////////////////////////////////////////////////////////
                if (calcCustom5)
                {
                    ulong customLValue5 = CustomCalcs(comboBoxLValue5, frame, frames);
                    if ((string)comboBoxRValue5.SelectedItem != "None")
                        customRValue5 = CustomCalcs(comboBoxRValue5, frame, frames);

                    if (!rngIs64Bit)
                        customLValue5 = (uint)customLValue5;

                    frame.Custom5 = custom5Calc(customLValue5, customRValue5);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 6 ////////////////////////////////////////////////////////////////
                if (calcCustom6)
                {
                    ulong customLValue6 = CustomCalcs(comboBoxLValue6, frame, frames);
                    if ((string)comboBoxRValue6.SelectedItem != "None")
                        customRValue6 = CustomCalcs(comboBoxRValue6, frame, frames);

                    if (!rngIs64Bit)
                        customLValue6 = (uint)customLValue6;

                    frame.Custom6 = custom6Calc(customLValue6, customRValue6);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 7 ////////////////////////////////////////////////////////////////
                if (calcCustom7)
                {
                    ulong customLValue7 = CustomCalcs(comboBoxLValue7, frame, frames);
                    if ((string)comboBoxRValue7.SelectedItem != "None")
                        customRValue7 = CustomCalcs(comboBoxRValue7, frame, frames);

                    if (!rngIs64Bit)
                        customLValue7 = (uint)customLValue7;

                    frame.Custom7 = custom7Calc(customLValue7, customRValue7);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 8 ////////////////////////////////////////////////////////////////
                if (calcCustom8)
                {
                    ulong customLValue8 = CustomCalcs(comboBoxLValue8, frame, frames);
                    if ((string)comboBoxRValue8.SelectedItem != "None")
                        customRValue8 = CustomCalcs(comboBoxRValue8, frame, frames);

                    if (!rngIs64Bit)
                        customLValue8 = (uint)customLValue8;

                    frame.Custom8 = custom8Calc(customLValue8, customRValue8);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 9 ////////////////////////////////////////////////////////////////
                if (calcCustom9)
                {
                    ulong customLValue9 = CustomCalcs(comboBoxLValue9, frame, frames);
                    if ((string)comboBoxRValue9.SelectedItem != "None")
                        customRValue9 = CustomCalcs(comboBoxRValue9, frame, frames);

                    if (!rngIs64Bit)
                        customLValue9 = (uint)customLValue9;

                    frame.Custom9 = custom9Calc(customLValue9, customRValue9);
                }
                //////////////////////////////////////////////////////////////////////////////////

                //  Call Custom 10 ////////////////////////////////////////////////////////////////
                if (calcCustom10)
                {
                    ulong customLValue10 = CustomCalcs(comboBoxLValue10, frame, frames);
                    if ((string)comboBoxRValue10.SelectedItem != "None")
                        customRValue10 = CustomCalcs(comboBoxRValue10, frame, frames);

                    if (!rngIs64Bit)
                        customLValue10 = (uint)customLValue10;

                    frame.Custom10 = custom10Calc(customLValue10, customRValue10);
                }
                //////////////////////////////////////////////////////////////////////////////////

                frames.Add(frame);
            }

            //  Bind to the grid
            dataGridViewValues.DataSource = frames;
            dataGridViewValues.Focus();
        }

        private void textBoxHex_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = "";

            s += e.KeyChar;

            byte[] b = Encoding.ASCII.GetBytes(s);

            if (e.KeyChar != (char)Keys.Back && !char.IsControl(e.KeyChar))
            {
                if (!(((0x30 <= b[0]) && (b[0] <= 0x39)) ||
                      ((0x41 <= b[0]) && (b[0] <= 0x46)) ||
                      ((0x61 <= b[0]) && (b[0] <= 0x66))))
                {
                    e.Handled = true;
                }
                else
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
            }
        }

        private ulong CustomCalcs(ComboBox selection, FrameResearch frame, List<FrameResearch> frames)
        {
            switch ((string)selection.SelectedItem)
            {
                case "64Bit":
                    return frame.Full64;
                case "32Bit High":
                    return frame.High32;
                case "32Bit Low":
                    return frame.Low32;
                case "32Bit":
                    return frame.Full32;
                case "16Bit High":
                    return frame.High16;
                case "16Bit Low":
                    return frame.Low16;
                case "Custom 1":
                    return frame.Custom1;
                case "Custom 2":
                    return frame.Custom2;
                case "Custom 3":
                    return frame.Custom3;
                case "Custom 4":
                    return frame.Custom4;
                case "Custom 5":
                    return frame.Custom5;
                case "Custom 6":
                    return frame.Custom6;
                case "Custom 7":
                    return frame.Custom7;
                case "Custom 8":
                    return frame.Custom8;
                case "Custom 9":
                    return frame.Custom9;
                case "Custom 10":
                    return frame.Custom10;
                case "Previous 1":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom1;
                case "Previous 2":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom2;
                case "Previous 3":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom3;
                case "Previous 4":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom4;
                case "Previous 5":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom5;
                case "Previous 6":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom6;
                case "Previous 7":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom7;
                case "Previous 8":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom8;
                case "Previous 9":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom9;
                case "Previous 10":
                    return frames.Count == 0 ? 0 : frames[frames.Count - 1].Custom10;
                default:
                    return 0;
            }
        }

        private void contextMenuStripGrid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }

            if (columnHover != null)
            {
                copyItemToolStripMenuItem.Text = "Copy " + dataGridViewValues.Columns[columnHover].HeaderText;
            }

            if (columns.Count == 0)
            {
                copySelectedColumnsToolStripMenuItem.Enabled = false;
                copyColumnToolStripMenuItem.Enabled = false;
                outputSelectedColumnsToTXTToolStripMenuItem.Enabled = false;
            }
            else
            {
                copySelectedColumnsToolStripMenuItem.Enabled = true;
                copyColumnToolStripMenuItem.Enabled = true;
                outputSelectedColumnsToTXTToolStripMenuItem.Enabled = true;
            }
        }

        private void copyColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                if (columns != null && columns.Count > 0)
                {
                    sb = new StringBuilder();
                    var frame = (FrameResearch)dataGridViewValues.SelectedRows[0].DataBoundItem;
                    for (int i = 0; i < columns.Count; i++)
                    {
                        int columnIndex = columns[i];
                        sb.AppendFormat("{0:" + dataGridViewValues.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                        frame.GetType().GetProperty(
                                            dataGridViewValues.Columns[columnIndex].DataPropertyName).GetValue(frame,
                                                                                                               null));
                        if (i < columns.Count - 1)
                            sb.Append("\t");
                    }
                    Clipboard.SetText(sb.ToString());
                }
            }
        }

        private void copySelectedColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (columns != null && columns.Count > 0)
            {
                sb = new StringBuilder();
                foreach (DataGridViewRow row in dataGridViewValues.Rows)
                {
                    var frame = (FrameResearch)row.DataBoundItem;
                    for (int i = 0; i < columns.Count; i++)
                    {
                        int columnIndex = columns[i];
                        sb.AppendFormat("{0:" + dataGridViewValues.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                        frame.GetType().GetProperty(
                                            dataGridViewValues.Columns[columnIndex].DataPropertyName).GetValue(frame,
                                                                                                               null));
                        if (i < columns.Count - 1)
                            sb.Append("\t");
                    }
                    sb.Append(Environment.NewLine);
                }

                Clipboard.SetText(sb.ToString());
            }
        }

        private void copyItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                var frame = (FrameResearch)dataGridViewValues.SelectedRows[0].DataBoundItem;
                sb = new StringBuilder();

                sb.AppendFormat("{0:" + dataGridViewValues.Columns[columnHover].DefaultCellStyle.Format + "}",
                                frame.GetType().GetProperty(dataGridViewValues.Columns[columnHover].DataPropertyName).
                                      GetValue(frame, null));
                Clipboard.SetText(sb.ToString());
            }
        }

        private void outputResultsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogTxt.AddExtension = true;
            saveFileDialogTxt.Title = "Save Output to TXT";
            saveFileDialogTxt.Filter = "TXT Files|*.txt";
            saveFileDialogTxt.FileName = "rngreporter.txt";
            if (saveFileDialogTxt.ShowDialog() == DialogResult.OK)
            {
                //  Get the name of the file and then go ahead 
                //  and create and save the thing to the hard
                //  drive.   
                var frames = (List<FrameResearch>)dataGridViewValues.DataSource;

                if (frames.Count > 0)
                {
                    var writer = new TXTWriter(dataGridViewValues);
                    writer.Generate(saveFileDialogTxt.FileName, frames);
                }
            }
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            dataGridViewValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridView.HitTestInfo Hti = dataGridViewValues.HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    columnHover = dataGridViewValues.Columns[Hti.ColumnIndex].Name;
                    if (!((dataGridViewValues.Rows[Hti.RowIndex])).Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (Hti.Type == DataGridViewHitTestType.ColumnHeader && dataGridViewValues.DataSource != null)
                {
                    if (dataGridViewValues.Columns[Hti.ColumnIndex].HeaderCell.Style.BackColor == Color.DarkGray)
                    {
                        dataGridViewValues.Columns[Hti.ColumnIndex].HeaderCell.Style.BackColor =
                            SystemColors.InactiveBorder;
                        columns.Remove(Hti.ColumnIndex);
                    }
                    else
                    {
                        dataGridViewValues.Columns[Hti.ColumnIndex].HeaderCell.Style.BackColor = Color.DarkGray;
                        columns.Add(Hti.ColumnIndex);
                        columns.Sort();
                    }
                }
            }
        }

        private void Researcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }

        private void checkBox64bit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox64bit.Checked)
            {
                textBoxMult.MaxLength = 16;
                textBoxAdd.MaxLength = 16;
            }
            else
            {
                textBoxMult.MaxLength = 8;
                textBoxAdd.MaxLength = 8;
            }
        }

        private void outputSelectedColumnsToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (columns != null && columns.Count > 0)
            {
                saveFileDialogTxt.AddExtension = true;
                saveFileDialogTxt.Title = "Save Output to TXT";
                saveFileDialogTxt.Filter = "TXT Files|*.txt";
                saveFileDialogTxt.FileName = "rngreporter.txt";
                if (saveFileDialogTxt.ShowDialog() == DialogResult.OK)
                {
                    //  Get the name of the file and then go ahead 
                    //  and create and save the thing to the hard
                    //  drive.   
                    var frames = (List<FrameResearch>)dataGridViewValues.DataSource;

                    if (frames.Count > 0)
                    {
                        var writer = new TXTWriter(dataGridViewValues, columns);
                        writer.Generate(saveFileDialogTxt.FileName, frames);
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var profile = new ResearcherProfile { MaxResults = 1000, Custom = new CustomResearcher[10] };
            for (int i = 0; i < 9; ++i)
                profile.Custom[i] = new CustomResearcher();
            SetProfile(profile);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ResearcherProfile profile = OpenProfile(openFileDialog1.FileName);
                SetProfile(profile);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ResearcherProfile profile = GetProfile();
                SaveProfile(profile, saveFileDialog1.FileName);
            }
        }

        private void SetProfile(ResearcherProfile profile)
        {
            if (profile.Custom.Length != 10)
            {
                MessageBox.Show("Corrupt profile detected");
                return;
            }

            if (profile.Type != ResearcherProfile.RNGType.Custom)
            {
                radioButtonCommon.Checked = true;
                comboBoxRNG.SelectedIndex = (int)profile.Type;
            }
            else
            {
                radioButtonCustom.Checked = true;
                textBoxMult.Text = profile.CustomM;
                textBoxAdd.Text = profile.CustomA;
                checkBox64bit.Checked = profile.Is64Bit;
            }
            maskedTextBoxMaxFrames.Text = profile.MaxResults.ToString();
            textBoxSeed.Text = profile.Seed;

            //todo: automoate this
            comboBoxLValue1.SelectedIndex = (int)profile.Custom[0].Type;
            comboBoxOperator1.SelectedIndex = (int)profile.Custom[0].Operation;
            textBoxRValue1.Text = profile.Custom[0].Operand;
            checkBoxCustom1Hex.Checked = profile.Custom[0].isHex;

            comboBoxLValue2.SelectedIndex = (int)profile.Custom[1].Type;
            comboBoxOperator2.SelectedIndex = (int)profile.Custom[1].Operation;
            textBoxRValue2.Text = profile.Custom[1].Operand;
            checkBoxCustom2Hex.Checked = profile.Custom[1].isHex;
            comboBoxRValue2.SelectedIndex = (int)profile.Custom[1].RelOperand;

            comboBoxLValue3.SelectedIndex = (int)profile.Custom[2].Type;
            comboBoxOperator3.SelectedIndex = (int)profile.Custom[2].Operation;
            textBoxRValue3.Text = profile.Custom[2].Operand;
            checkBoxCustom3Hex.Checked = profile.Custom[2].isHex;
            comboBoxRValue3.SelectedIndex = (int)profile.Custom[2].RelOperand;

            comboBoxLValue4.SelectedIndex = (int)profile.Custom[3].Type;
            comboBoxOperator4.SelectedIndex = (int)profile.Custom[3].Operation;
            textBoxRValue4.Text = profile.Custom[3].Operand;
            checkBoxCustom4Hex.Checked = profile.Custom[3].isHex;
            comboBoxRValue4.SelectedIndex = (int)profile.Custom[3].RelOperand;

            comboBoxLValue5.SelectedIndex = (int)profile.Custom[4].Type;
            comboBoxOperator5.SelectedIndex = (int)profile.Custom[4].Operation;
            textBoxRValue5.Text = profile.Custom[4].Operand;
            checkBoxCustom5Hex.Checked = profile.Custom[4].isHex;
            comboBoxRValue5.SelectedIndex = (int)profile.Custom[4].RelOperand;

            comboBoxLValue6.SelectedIndex = (int)profile.Custom[5].Type;
            comboBoxOperator6.SelectedIndex = (int)profile.Custom[5].Operation;
            textBoxRValue6.Text = profile.Custom[5].Operand;
            checkBoxCustom6Hex.Checked = profile.Custom[5].isHex;
            comboBoxRValue6.SelectedIndex = (int)profile.Custom[5].RelOperand;

            comboBoxLValue7.SelectedIndex = (int)profile.Custom[6].Type;
            comboBoxOperator7.SelectedIndex = (int)profile.Custom[6].Operation;
            textBoxRValue7.Text = profile.Custom[6].Operand;
            checkBoxCustom7Hex.Checked = profile.Custom[6].isHex;
            comboBoxRValue7.SelectedIndex = (int)profile.Custom[6].RelOperand;

            comboBoxLValue8.SelectedIndex = (int)profile.Custom[7].Type;
            comboBoxOperator8.SelectedIndex = (int)profile.Custom[7].Operation;
            textBoxRValue8.Text = profile.Custom[7].Operand;
            checkBoxCustom8Hex.Checked = profile.Custom[7].isHex;
            comboBoxRValue8.SelectedIndex = (int)profile.Custom[7].RelOperand;

            comboBoxLValue9.SelectedIndex = (int)profile.Custom[8].Type;
            comboBoxOperator9.SelectedIndex = (int)profile.Custom[8].Operation;
            textBoxRValue9.Text = profile.Custom[8].Operand;
            checkBoxCustom9Hex.Checked = profile.Custom[8].isHex;
            comboBoxRValue9.SelectedIndex = (int)profile.Custom[8].RelOperand;

            comboBoxLValue10.SelectedIndex = (int)profile.Custom[9].Type;
            comboBoxOperator10.SelectedIndex = (int)profile.Custom[9].Operation;
            textBoxRValue10.Text = profile.Custom[9].Operand;
            checkBoxCustom10Hex.Checked = profile.Custom[9].isHex;
            comboBoxRValue10.SelectedIndex = (int)profile.Custom[9].RelOperand;
        }

        private ResearcherProfile GetProfile()
        {
            var profile = new ResearcherProfile();

            if (radioButtonCommon.Checked)
            {
                profile.Type = (ResearcherProfile.RNGType)comboBoxRNG.SelectedIndex;
            }
            else
            {
                profile.Type = ResearcherProfile.RNGType.Custom;
                profile.CustomM = textBoxMult.Text;
                profile.CustomA = textBoxAdd.Text;
                profile.Is64Bit = checkBox64bit.Checked;
            }
            profile.MaxResults = Int32.Parse(maskedTextBoxMaxFrames.Text);
            profile.Seed = textBoxSeed.Text;

            profile.Custom = new CustomResearcher[10];
            for (int i = 0; i < 10; ++i)
                profile.Custom[i] = new CustomResearcher();
            profile.Custom[0].Type = (CustomResearcher.ValueType)comboBoxLValue1.SelectedIndex;
            profile.Custom[0].Operation = (CustomResearcher.Operator)comboBoxOperator1.SelectedIndex;
            profile.Custom[0].Operand = textBoxRValue1.Text;
            profile.Custom[0].isHex = checkBoxCustom1Hex.Checked;

            profile.Custom[1].Type = (CustomResearcher.ValueType)comboBoxLValue2.SelectedIndex;
            profile.Custom[1].Operation = (CustomResearcher.Operator)comboBoxOperator2.SelectedIndex;
            profile.Custom[1].Operand = textBoxRValue2.Text;
            profile.Custom[1].isHex = checkBoxCustom2Hex.Checked;
            profile.Custom[1].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue2.SelectedIndex;

            profile.Custom[2].Type = (CustomResearcher.ValueType)comboBoxLValue3.SelectedIndex;
            profile.Custom[2].Operation = (CustomResearcher.Operator)comboBoxOperator3.SelectedIndex;
            profile.Custom[2].Operand = textBoxRValue3.Text;
            profile.Custom[2].isHex = checkBoxCustom3Hex.Checked;
            profile.Custom[2].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue3.SelectedIndex;

            profile.Custom[3].Type = (CustomResearcher.ValueType)comboBoxLValue4.SelectedIndex;
            profile.Custom[3].Operation = (CustomResearcher.Operator)comboBoxOperator4.SelectedIndex;
            profile.Custom[3].Operand = textBoxRValue4.Text;
            profile.Custom[3].isHex = checkBoxCustom4Hex.Checked;
            profile.Custom[3].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue4.SelectedIndex;

            profile.Custom[4].Type = (CustomResearcher.ValueType)comboBoxLValue5.SelectedIndex;
            profile.Custom[4].Operation = (CustomResearcher.Operator)comboBoxOperator5.SelectedIndex;
            profile.Custom[4].Operand = textBoxRValue5.Text;
            profile.Custom[4].isHex = checkBoxCustom5Hex.Checked;
            profile.Custom[4].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue5.SelectedIndex;

            profile.Custom[5].Type = (CustomResearcher.ValueType)comboBoxLValue6.SelectedIndex;
            profile.Custom[5].Operation = (CustomResearcher.Operator)comboBoxOperator6.SelectedIndex;
            profile.Custom[5].Operand = textBoxRValue6.Text;
            profile.Custom[5].isHex = checkBoxCustom6Hex.Checked;
            profile.Custom[5].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue6.SelectedIndex;

            profile.Custom[6].Type = (CustomResearcher.ValueType)comboBoxLValue7.SelectedIndex;
            profile.Custom[6].Operation = (CustomResearcher.Operator)comboBoxOperator7.SelectedIndex;
            profile.Custom[6].Operand = textBoxRValue7.Text;
            profile.Custom[6].isHex = checkBoxCustom7Hex.Checked;
            profile.Custom[6].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue7.SelectedIndex;

            profile.Custom[7].Type = (CustomResearcher.ValueType)comboBoxLValue8.SelectedIndex;
            profile.Custom[7].Operation = (CustomResearcher.Operator)comboBoxOperator8.SelectedIndex;
            profile.Custom[7].Operand = textBoxRValue8.Text;
            profile.Custom[7].isHex = checkBoxCustom8Hex.Checked;
            profile.Custom[7].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue8.SelectedIndex;

            profile.Custom[8].Type = (CustomResearcher.ValueType)comboBoxLValue9.SelectedIndex;
            profile.Custom[8].Operation = (CustomResearcher.Operator)comboBoxOperator9.SelectedIndex;
            profile.Custom[8].Operand = textBoxRValue9.Text;
            profile.Custom[8].isHex = checkBoxCustom9Hex.Checked;
            profile.Custom[8].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue9.SelectedIndex;

            profile.Custom[9].Type = (CustomResearcher.ValueType)comboBoxLValue10.SelectedIndex;
            profile.Custom[9].Operation = (CustomResearcher.Operator)comboBoxOperator10.SelectedIndex;
            profile.Custom[9].Operand = textBoxRValue10.Text;
            profile.Custom[9].isHex = checkBoxCustom10Hex.Checked;
            profile.Custom[9].RelOperand = (CustomResearcher.RelativeOperand)comboBoxRValue10.SelectedIndex;

            return profile;
        }

        private ResearcherProfile OpenProfile(string fileName)
        {
            var deserializer = new XmlSerializer(typeof(ResearcherProfile));
            TextReader textReader = new StreamReader(fileName);
            var profile = (ResearcherProfile)deserializer.Deserialize(textReader);
            textReader.Close();
            return profile;
        }

        private void SaveProfile(ResearcherProfile profile, string fileName)
        {
            var serializer = new XmlSerializer(typeof(ResearcherProfile));
            TextWriter textWriter = new StreamWriter(fileName);
            serializer.Serialize(textWriter, profile);
            textWriter.Close();
        }

        #region Nested type: Calculator

        private delegate ulong Calculator(ulong x, ulong y);

        #endregion

        private void searchValue(bool search)
        {
            dataGridViewValues.Focus();                     

            if (dataGridViewValues.RowCount > 0 && textBoxSearch.Text.Length > 0)
            {
                string newTextBoxValue = textBoxSearch.Text;

                while (newTextBoxValue.Length > 1 && newTextBoxValue.Substring(0, 1) == "0")
                {
                    newTextBoxValue = newTextBoxValue.Substring(1, newTextBoxValue.Length - 1);
                }
                int rowIndex = 0;
                int columnIndex = 0;

                foreach (DataGridViewColumn col in dataGridViewValues.Columns)
                {
                    if (col.Name == "Column" + glassComboBox1.SelectedItem.ToString())
                    {
                        break;
                    }
                    columnIndex++;
                }

                if (search == false)
                {
                    for (rowIndex = dataGridViewValues.CurrentCell.RowIndex + 1; rowIndex < dataGridViewValues.Rows.Count; rowIndex++)
                    {
                        string cellValue = long.Parse(dataGridViewValues["Column" + glassComboBox1.SelectedItem.ToString(), rowIndex].Value.ToString()).ToString("X");

                        if (cellValue == newTextBoxValue)
                        {
                            dataGridViewValues.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                            dataGridViewValues.Rows[rowIndex].Cells[columnIndex].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridViewValues.Rows)
                    {
                        string cellValue = long.Parse(dataGridViewValues["Column" + glassComboBox1.SelectedItem.ToString(), rowIndex].Value.ToString()).ToString("X");

                        if (cellValue == newTextBoxValue)
                        {
                            dataGridViewValues.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                            dataGridViewValues.Rows[rowIndex].Cells[columnIndex].Selected = true;
                            break;
                        }
                        rowIndex++;
                    }
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            searchValue(true);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            searchValue(false);
        }

        private void comboBoxRNG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRNG.SelectedIndex != 3 && comboBoxRNG.SelectedIndex != 4)
            {
                glassComboBox1.Items.Remove("64Bit");
                glassComboBox1.Items.Remove("32BitHigh");
                glassComboBox1.Items.Remove("32BitLow");

                if (!glassComboBox1.Items.Contains("32Bit"))
                {
                    glassComboBox1.Items.Insert(0, "32Bit");
                }
                glassComboBox1.SelectedIndex = 0;
            }
            else if (comboBoxRNG.SelectedIndex == 3 || comboBoxRNG.SelectedIndex == 4)
            {
                glassComboBox1.Items.Remove("32Bit");

                if (!glassComboBox1.Items.Contains("64Bit"))
                {
                    glassComboBox1.Items.Insert(0, "64Bit");
                }
                if (!glassComboBox1.Items.Contains("32BitHigh"))
                {
                    glassComboBox1.Items.Insert(1, "32BitHigh");
                }
                if (!glassComboBox1.Items.Contains("32BitLow"))
                {
                    glassComboBox1.Items.Insert(2, "32BitLow");
                }
                glassComboBox1.SelectedIndex = 0;
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V || e.Modifiers == Keys.Shift && e.KeyCode == Keys.Insert)
            {
                string NewText = "";

                foreach (char a in Clipboard.GetText())
                {
                    if ((a >= 'a' && a <= 'f') || (a >= 'A' && a <= 'F') || (a >= '0' && a <= '9'))
                    {
                        NewText = NewText + char.ToUpper(a);
                    }
                }

                if (NewText != "")
                {
                    Clipboard.SetText(NewText);
                }
                else
                { Clipboard.Clear(); }
            }
        }
    }
}
