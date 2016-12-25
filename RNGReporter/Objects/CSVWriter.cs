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
using System.IO;
using System.Text;
using System.Windows.Forms;
using RNGReporter.Objects.Generators;

namespace RNGReporter.Objects
{
    // Here is the class that eliminated all the other specialized TXT writers
    // It takes all the data from visible columns in a DataGridView object,
    // tweaks headers as necessary, then outputs to a TXT file with formatting preserved.
    // This eliminated the need to edit a TXT writer every time
    // we made a change to the display output.

    public class TXTWriter
    {
        protected DataGridView dataGrid;
        protected StringBuilder sb;
        protected List<int> selectedColumns;

        // Create a TXTWriter that prints only visible columns
        public TXTWriter(DataGridView dataGrid)
        {
            sb = new StringBuilder();
            selectedColumns = new List<int>();
            this.dataGrid = dataGrid;

            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                if (column.Visible)
                {
                    selectedColumns.Add(column.Index);
                }
            }
        }

        // Create a TXTWriter that prints only user-selected columns
        public TXTWriter(DataGridView dataGrid, List<int> selectedColumns)
        {
            sb = new StringBuilder();
            this.selectedColumns = selectedColumns;
            this.dataGrid = dataGrid;
        }

        protected void GenerateHeader()
        {
            foreach (int columnIndex in selectedColumns)
            {
                if (dataGrid.Columns[columnIndex].HeaderText == "Flip Sequence")
                {
                    sb.Append("Flip1\tFlip2\tFlip3\tFlip4\tFlip5\t");
                    sb.Append("Flip6\tFlip7\tFlip8\tFlip9\tFlip10\t");
                }
                else if (dataGrid.Columns[columnIndex].HeaderText == "IVs (Black & White)")
                {
                    sb.Append("IV1\tIV2\tIV3\tIV4\tIV5\tIV6\tIV7\tIV8\tIV9\tIV10");
                }
                else
                    sb.Append(dataGrid.Columns[columnIndex].HeaderText + "\t");
            }

            sb.Append(Environment.NewLine);
        }

        // For main window display
        public void Generate(string fileName, List<Frame> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (Frame frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For Researcher
        public void Generate(string fileName, List<FrameResearch> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (FrameResearch frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For Seed to Time Adjacent seed display
        public void Generate(string fileName, List<Adjacent> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (Adjacent frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For the capture tab on Time Finder
        public void Generate(string fileName, List<IFrameCapture> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (IFrameCapture frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For the 4th Gen egg IVs tab on Time Finder
        public void Generate(string fileName, List<IFrameBreeding> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (IFrameBreeding frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For the Shiny egg tab on Time Finder
        public void Generate(string fileName, List<IFrameEggPID> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (IFrameEggPID frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For the 3rd Gen Shiny egg tab on Time Finder
        public void Generate(string fileName, List<IFrameRSEggPID> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (IFrameRSEggPID frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        // For the XD Capture tab on Time Finder
        public void Generate(string fileName, List<IFrameCaptureXD> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (IFrameCaptureXD frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }

        protected void GenerateLine(Object frame)
        {
            foreach (int columnIndex in selectedColumns)
            {
                sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue(
                                    frame, null));
                sb.Append("\t");
            }

            sb.Append(Environment.NewLine);
        }

        protected void GenerateLine(IFrameBreeding frame)
        {
            foreach (int columnIndex in selectedColumns)
            {
                if (dataGrid.Columns[columnIndex].Name == "Flips")
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null).ToString().Replace(", ", "\t"));
                    sb.Append("\t");
                }
                else
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null));
                    sb.Append("\t");
                }
            }

            sb.Append(Environment.NewLine);
        }

        protected void GenerateLine(IFrameEggPID frame)
        {
            foreach (int columnIndex in selectedColumns)
            {
                if (dataGrid.Columns[columnIndex].Name == "ShinyFlipSequence")
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null).ToString().Replace(", ", "\t"));
                    sb.Append("\t");
                }
                else
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null));
                    sb.Append("\t");
                }
            }

            sb.Append(Environment.NewLine);
        }

        protected void GenerateLine(Adjacent frame)
        {
            // At some point I'm going to change this so column visibility is checked only once
            // And not for every row in a datagrid

            foreach (int columnIndex in selectedColumns)
            {
                if (dataGrid.Columns[columnIndex].HeaderText != "Flip Sequence" &&
                    dataGrid.Columns[columnIndex].HeaderText != "Seed")
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null));
                    sb.Append("\t");
                }
                else
                {
                    sb.AppendFormat("{0:" + dataGrid.Columns[columnIndex].DefaultCellStyle.Format + "}",
                                    frame.GetType().GetProperty(dataGrid.Columns[columnIndex].DataPropertyName).GetValue
                                        (frame, null));
                    sb.Append("\t");
                }
            }

            sb.Append(Environment.NewLine);
        }

        public void Generate(string fileName, List<Hollow> frames)
        {
            using (var myFile = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                GenerateHeader();

                foreach (Hollow frame in frames)
                {
                    GenerateLine(frame);
                }

                myFile.WriteLine(sb.ToString());
            }
        }
    }
}