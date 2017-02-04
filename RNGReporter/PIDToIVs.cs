using RNGReporter.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class PIDToIVs : Form
    {
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<PIDIVS> results;
        private readonly String[] Method = { "Method 1", "Method 2", "Method 4", "XD/Colo", "Channel" };

        public PIDToIVs()
        {
            InitializeComponent();
            dataGridViewValues.DataSource = binding;
            dataGridViewValues.AutoGenerateColumns = true;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint pid = 0;
            uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out pid);

            results = new List<PIDIVS>();
            binding = new BindingSource { DataSource = results };
            dataGridViewValues.DataSource = binding;

            searchThread =
                new Thread(
                    () =>
                    calcFromPID(pid));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void calcFromPID(uint pid)
        {
            calcMethod1(pid);
            calcMethod2(pid);
            calcMethod4(pid);
            calcMethodXD(pid);
            calcMethodChannel(pid);
        }

        private void calcMethod1(uint pid)
        {
            long pidl = (long)pid & 0xFFFF;
            long pidh = (long)pid >> 16;

            long test = pidh * 0x10000;
            for (int x = 0; x < 65536; x++)
            {
                long testseed = test + x;
                long prevseed = reverse(testseed);
                long temp = prevseed >> 16;
                if (temp == pidl)
                    addSeed(reverse(prevseed),0);
            }
        }

        private void calcMethod2(uint pid)
        {
            long pidl = (long)pid & 0xFFFF;
            long pidh = (long)pid >> 16;

            long test = pidh * 0x10000;
            for (int x = 0; x < 65536; x++)
            {
                long testseed = test + x;
                long prevseed = reverse(testseed);
                long temp = prevseed >> 16;
                if (temp == pidl)
                    addSeed(reverse(prevseed), 1);
            }
        }

        private void calcMethod4(uint pid)
        {
            long pidl = (long)pid & 0xFFFF;
            long pidh = (long)pid >> 16;

            long test = pidh * 0x10000;
            for (int x = 0; x < 65536; x++)
            {
                long testseed = test + x;
                long prevseed = reverse(testseed);
                long temp = prevseed >> 16;
                if (temp == pidl)
                    addSeed(reverse(prevseed), 2);
            }
        }

        private void calcMethodXD(uint pid)
        {
            long pidl = (long)pid & 0xFFFF;
            long pidh = (long)pid >> 16;

            long test = pidl * 0x10000;
            for (int x = 0; x < 65536; x++)
            {
                long testseed = test + x;
                long prevseed = reverseXD(testseed);
                long temp = prevseed >> 16;
                if (temp == pidh)
                    addSeed(reverseXD(reverseXD(reverseXD(reverseXD(prevseed)))), 3);
            }
        }

        private void calcMethodChannel(uint pid)
        {
            long pidl = (long)pid & 0xFFFF;
            long pidh = ((long)pid >> 16) ^ 0x8000;

            long test = pidl * 0x10000;
            for (int x = 0; x < 65536; x++)
            {
                long testseed = test + x;
                long prevseed = reverseXD(testseed);
                long temp = prevseed >> 16;
                if (temp == pidh)
                    addSeed(reverseXD(reverseXD(prevseed)), 4);
            }
        }

        private void addSeed(long seed, int method)
        {
            String methodType = Method[method];
            uint actualSeed = Convert.ToUInt32(seed);
            String MonsterSeed = actualSeed.ToString("x").ToUpper();
            String IVs;

            if (method == 0)
                IVs = calcIVs1(seed);
            else if (method == 1)
                IVs = calcIVs2(seed);
            else if (method == 2)
                IVs = calcIVs4(seed);
            else if (method == 3)
                IVs = calcIVsXD(seed);
            else
                IVs = calcIVsChannel(seed);

            results.Add(new PIDIVS { Seed = MonsterSeed, Method = methodType, IVs = IVs});
        }

        private String calcIVs1(long seed)
        {
            String ivs = "";
            long iv1 = forward(forward(forward(seed)));
            long iv2 = forward(iv1);
            iv1 >>= 16;
            iv2 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                long iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            long iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVs2(long seed)
        {
            String ivs = "";
            long iv1 = forward(forward(forward(forward(seed))));
            long iv2 = forward(iv1);
            iv1 >>= 16;
            iv2 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                long iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            long iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVs4(long seed)
        {
            String ivs = "";
            long iv1 = forward(forward(forward(seed)));
            long iv2 = forward(forward(iv1));
            iv1 >>= 16;
            iv2 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                long iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            long iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVsXD(long seed)
        {
            String ivs = "";
            long iv1 = forwardXD(seed);
            long iv2 = forwardXD(iv1);
            iv1 >>= 16;
            iv2 >>= 16;

            for (int x = 0; x < 3; x++)
            {
                int q = x * 5;
                long iv = (iv1 >> q) & 31;
                ivs += iv.ToString();
                ivs += ".";
            }

            long iV = (iv2 >> 5) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = (iv2 >> 10) & 31;
            ivs += iV.ToString();
            ivs += ".";

            iV = iv2 & 31;
            ivs += iV.ToString();

            return ivs;
        }

        private String calcIVsChannel(long seed)
        {
            String ivs = "";

            long iv1 = forwardXD(forwardXD(forwardXD(forwardXD(forwardXD(forwardXD(forwardXD(seed)))))));
            long iv2 = forwardXD(iv1);
            long iv3 = forwardXD(iv2);
            long iv4 = forwardXD(iv3);
            long iv5 = forwardXD(iv4);
            long iv6 = forwardXD(iv5);
            long[] ivContiner = new long[] { iv1, iv2, iv3, iv5, iv6, iv4 };
            for (int x = 0; x < 6; x ++)
            {
                long iv = ivContiner[x] >> 27;
                ivs += iv.ToString();
                if (x != 5)
                    ivs += ".";
            }

            return ivs;
        }

        private long forward(long seed)
        {
            return (seed * 0x41c64e6d + 0x6073) & 0xFFFFFFFF;
        }

        private long reverse(long seed)
        {
            return (seed * 0xeeb9eb65 + 0xa3561a1) & 0xFFFFFFFF;
        }

        private long forwardXD(long seed)
        {
            return (seed * 0x343FD + 0x269EC3) & 0xFFFFFFFF;
        }

        private long reverseXD(long seed)
        {
            return (seed * 0xB9B33155 + 0xA170F641) & 0xFFFFFFFF;
        }

        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = dataGridViewValues.AutoResizeColumns;
            try
            {
                bool alive = true;
                while (alive)
                {
                    if (refresh)
                    {
                        Invoke(gridUpdate);
                        refresh = false;
                    }
                    if (searchThread == null || !searchThread.IsAlive)
                    {
                        alive = false;
                    }

                    Thread.Sleep(500);
                }
            }
            finally
            {
                Invoke(gridUpdate);
                Invoke(resizeGrid);
            }
        }


        #region Nested type: ThreadDelegate

        private delegate void ThreadDelegate();

        #endregion

        private void dataGridUpdate()
        {
            binding.ResetBindings(false);
        }
    }
}
