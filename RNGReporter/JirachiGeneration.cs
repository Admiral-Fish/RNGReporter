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

            searchThread = new Thread(() => genListOut(seed));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void genListOut(uint seed)
        {
            String genlistout = calcProbable(seed);
            
            String result = genlistout.Replace("|", " | ");
            generation.Add(new ProbableGeneration
            {
                Probable = result
            });
        }

        //Credits to amab for this
        private String calcProbable(uint seed)
        {
            uint m = 0;
            String genlistout = (seed >> 30).ToString();
            uint f = seed >> 30;
            uint[] checker = { 0, 0, 0, 0 };
            uint[] compare = { 0, 1, 1, 1 };
            uint backseed = seed;
            var rng = new XdRngR(backseed);
            uint advance = 8;
            bool xCheck = false;

            while (m < 35)
            {
                backseed = rng.GetNext32BitNumber();
                genlistout = (backseed >> 30).ToString() + "|" + genlistout;

                if (m == (advance - 1))
                {
                    f = backseed >> 30;
                    genlistout = " M: " + genlistout;
                }

                m += 1;
            }

            String genlistend = genlistout.Substring(genlistout.Length - 7);
            int intAdvance = 3;

            if (genlistend[0] == '0')
                intAdvance = 7;
            if (genlistend[2] == '0' && intAdvance == 3)
                intAdvance = 5;
            if (genlistend[4] == '0' && intAdvance == 3)
                intAdvance = 3;
            if (genlistend[6] == '0' && intAdvance == 3)
                intAdvance = 3;
            int mm = genlistout.IndexOf('M');
            genlistout = genlistout.Substring(0, mm + intAdvance) + "T:" + genlistout.Substring(mm + intAdvance);
            genlistout = genlistout.Substring(2);
            genlistout = genlistout.Replace("M:", "");
            genlistout = genlistout.Replace(" ", "");
            int index = genlistout.IndexOf(':') + 1;
            char targetNum = genlistout[index];
            String string2Search = genlistout.Substring(0, index);
            genlistend = genlistout.Substring(index);
            string2Search = flip(string2Search);
            checker[int.Parse(targetNum.ToString())] = 1;
            for (int x = 0; x < string2Search.Length; x++)
            {
                if (!xCheck)
                {
                    if (string2Search[x] == '1' || string2Search[x] == '2' || string2Search[x] == '3')
                    {
                        if (string2Search[x] == targetNum)
                        {
                            if (checker[0] == 1 || checker[1] == 0 || checker[2] == 0 || checker[3] == 0)
                            {
                                string2Search = string2Search.Substring(0, x) + "XXX" + string2Search.Substring(x);
                                xCheck = true;
                            }
                        }
                        else
                        {
                            checker[int.Parse(string2Search[x].ToString())] = 1;
                        }
                    }
                }
            }

            string2Search = flip(string2Search);
            genlistout = string2Search + genlistend;

            return genlistout;
        }

        //Credits to here http://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
        public string flip(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
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
