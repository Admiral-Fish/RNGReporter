using RNGReporter.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class PIDToIVs : Form
    {
        private List<PIDIVS> results;
        private MainForm mainForm;
        private readonly String[] Method = { "XD/Colo", "Channel" };

        public PIDToIVs(MainForm mainForm)
        {
            InitializeComponent();
            dataGridViewValues.AutoGenerateColumns = true;
            this.mainForm = mainForm;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint pid);
            results = new List<PIDIVS>();
            calcFromPID(pid);
            dataGridViewValues.DataSource = results;
            dataGridViewValues.AutoResizeColumns();
        }

        private void calcFromPID(uint pid)
        {
            calcMethod124(pid);
            calcMethodXD(pid);
            calcMethodChannel(pid);
        }

        private void calcMethod124(uint pid)
        {
            uint pidl = pid & 0xFFFF;
            uint pidh = pid >> 16;

            uint test = pidh << 16;
            for (uint x = 0; x <= 0xFFFF; x++)
            {
                uint testseed = test | x;
                uint prevseed = reverse(testseed);
                uint temp = prevseed >> 16;
                if (temp == pidl)
                    addSeed(reverse(prevseed), forward(testseed));
            }
        }

        private void calcMethodXD(uint pid)
        {
            uint pidl = pid & 0xFFFF;
            uint pidh = pid >> 16;

            uint test = pidl << 16;
            for (uint x = 0; x <= 0xFFFF; x++)
            {
                uint testseed = test | x;
                uint prevseed = reverseXD(testseed);
                uint temp = prevseed >> 16;
                if (temp == pidh)
                {
                    uint iv2 = reverseXD(reverseXD(prevseed));
                    uint iv1 = reverseXD(iv2);
                    addSeedGC(reverseXD(iv1), iv1, iv2, 0);
                }
            }
        }

        private void calcMethodChannel(uint pid)
        {
            uint pidl = pid & 0xFFFF;
            uint pidh = (pid >> 16) ^ 0x8000;

            uint test = pidl << 16;
            for (uint x = 0; x <= 0xFFFF; x++)
            {
                uint testseed = test | x;
                uint prevseed = reverseXD(testseed);
                uint temp = prevseed >> 16;
                if (temp == pidh)
                {
                    uint seed = reverseXD(reverseXD(prevseed));
                    addSeedGC(seed, forwardXD(forwardXD(forwardXD(forwardXD(testseed)))), 0, 1);
                }
            }
        }

        private void addSeed(uint seed, uint iv1)
        {
            String MonsterSeed = seed.ToString("X");
            results.Add(new PIDIVS { Seed = MonsterSeed, Method = "Method 1", IVs = calcIVs1(iv1) });
            results.Add(new PIDIVS { Seed = MonsterSeed, Method = "Method 2", IVs = calcIVs2(forward(iv1)) });
            results.Add(new PIDIVS { Seed = MonsterSeed, Method = "Method 4", IVs = calcIVs4(iv1) });
        }

        private void addSeedGC(uint seed, uint iv1, uint iv2, int method)
        {
            String IVs;

            if (method == 0)
                IVs = calcIVsXD(iv1, iv2);
            else
                IVs = calcIVsChannel(iv1);

            results.Add(new PIDIVS { Seed = seed.ToString("X"), Method = Method[method], IVs = IVs });
        }

        private String calcIVs1(uint iv1)
        {
            String ivs = "";
            uint iv2 = forward(iv1) >> 16;
            iv1 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            uint iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVs2(uint iv1)
        {
            String ivs = "";
            uint iv2 = forward(iv1) >> 16;
            iv1 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            uint iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVs4(uint iv1)
        {
            String ivs = "";
            uint iv2 = forward(forward(iv1)) >> 16;
            iv1 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            uint iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVsXD(uint iv1, uint iv2)
        {
            String ivs = "";
            iv1 >>= 16;
            iv2 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                uint iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            uint iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVsChannel(uint iv1)
        {
            String ivs = "";

            uint iv2 = forwardXD(iv1);
            uint iv3 = forwardXD(iv2);
            uint iv4 = forwardXD(iv3);
            uint iv5 = forwardXD(iv4);
            uint iv6 = forwardXD(iv5);
            uint[] ivContiner = { iv1, iv2, iv3, iv5, iv6, iv4 };
            for (int x = 0; x < 6; x ++)
            {
                uint iv = ivContiner[x] >> 27;
                ivs += iv.ToString();
                if (x != 5)
                    ivs += ".";
            }

            return ivs;
        }

        private uint forward(uint seed)
        {
            return seed * 0x41c64e6d + 0x6073;
        }

        private uint reverse(uint seed)
        {
            return seed * 0xeeb9eb65 + 0xa3561a1;
        }

        private uint forwardXD(uint seed)
        {
            return seed * 0x343FD + 0x269EC3;
        }

        private uint reverseXD(uint seed)
        {
            return seed * 0xB9B33155 + 0xA170F641;
        }

        private void contextMenuStripGrid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count == 0)
                e.Cancel = true;
        }

        private void moveResultToMainForm_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                var frame = (PIDIVS)dataGridViewValues.SelectedRows[0].DataBoundItem;
                String seed = frame.Seed;
                String type = frame.Method;
                String[] ivs = frame.IVs.Split('.');
                mainForm.changeSeed(seed);
                mainForm.changeIVs(ivs);
                mainForm.changeType(getType(type));
            }
        }

        private void moveIVsToMainForm_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                var frame = (PIDIVS)dataGridViewValues.SelectedRows[0].DataBoundItem;
                String[] ivs = frame.IVs.Split('.');
                mainForm.changeIVs(ivs);
            }
        }

        private void copySeedToClipboard_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows[0] != null)
            {
                var frame = (PIDIVS)dataGridViewValues.SelectedRows[0].DataBoundItem;
                Clipboard.SetText(frame.Seed.ToString());
            }
        }

        private int getType(String type)
        {
            switch(type)
            {
                case "Method 1":
                    return 0;
                case "Method 2":
                    return 2;
                case "Method 4":
                    return 3;
                case "XD/Colo":
                    return 27;
                default:
                    return 28;
            }
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = dataGridViewValues.HitTest(e.X, e.Y);

                if (hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!((dataGridViewValues.Rows[hti.RowIndex])).Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[hti.RowIndex]).Selected = true;
                    }
                }
            }
        }
    }
}
