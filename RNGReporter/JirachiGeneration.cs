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
    public partial class JirachiGeneration : Form
    {
        private Thread searchThread;
        private bool refresh;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private List<ProbableGeneration> generation;

        public JirachiGeneration()
        {
            InitializeComponent();
            dataGridViewValues.DataSource = binding;
            dataGridViewValues.AutoGenerateColumns = true;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            uint seed = 0;
            uint.TryParse(textBoxSeed.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out seed);

            generation = new List<ProbableGeneration>();
            binding = new BindingSource { DataSource = generation };
            dataGridViewValues.DataSource = binding;

            searchThread =
                new Thread(
                    () =>
                    genListOut(seed));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void genListOut(uint seed)
        {
            String genlistout = calcProbable(seed);
            String genlistend = genlistout.Substring(genlistout.Length -7);
            int advance = 3;

            if (genlistend[0] == '0')
                advance = 7;
            if (genlistend[2] == '0' && advance == 3)
                advance = 5;
            if (genlistend[4] == '0' && advance == 3)
                advance = 3;
            if (genlistend[6] == '0' && advance == 3)
                advance = 3;
            int mm = genlistout.IndexOf('M');
            String result = genlistout.Substring(0, mm + advance) + "T" + genlistout.Substring(mm + advance);
            result = result.Substring(2);
            result = result.Replace("|", " | ");
            generation.Add(new ProbableGeneration
            {
                Result = result
            });
        }

        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        //Credits to amab for this
        private String calcProbable(uint seed)
        {
            uint m = 0;
            List<uint> ms = new List<uint>();
            String genlistout = (seed >> 30).ToString();
            uint f = seed >> 30;
            uint[] checker = { 0, 0, 0, 0 };
            uint backseed = seed;
            uint advance = 8;
            bool xCheck = false;

            while (m < 35)
            {
                backseed = reverse(backseed);
                genlistout = (backseed >> 30).ToString() + "|" + genlistout;

                if (m == advance - 1)
                {
                    f = backseed >> 30;
                    ms.Clear();
                    for (int x = 0; x < 4; x++)
                        checker[x] = 0;
                    genlistout = " M: " + genlistout;
                }
                uint g = backseed >> 30;
                if (ms.Count == 0 && g == f)
                {
                    if (m >= 8)
                    {
                        if (!xCheck)
                        {
                            xCheck = true;
                            genlistout = "XXX" + genlistout;
                        }
                    }
                }
                if (g != 0)
                {
                    if (checker[g] != 1)
                        checker[g] = 1;
                }
                if (checker[0] == 0 && checker[1] == 1 && checker[2] == 1 && checker[3] == 1)
                {
                    for (int x = 0; x < 4; x++)
                        checker[x] = 0;
                    ms.Add(m);
                }
                m += 1;
            }
            return genlistout;
        }

        private uint reverse(uint seed)
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
