using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RNGReporter.Objects;

namespace RNGReporter.Objects
{
    public partial class PokeSpot : Form
    {
        private readonly uint[] natures = { 100, 3, 2, 5, 20, 23, 11, 8, 13, 1, 16, 15, 14, 4, 17, 19, 7, 22, 10, 21, 9, 18, 6, 0, 24, 12 };
        private readonly String[] Natures = { "Hardy", "Lonely", "Brave", "Adamant", "Naughty", "Bold", "Docile", "Relaxed", "Impish", "Lax", "Timid", "Hasty", "Serious", "Jolly", "Naive", "Modest", "Mild", "Quiet", "Bashful", "Rash", "Calm", "Gentle", "Sassy", "Careful", "Quirky" };
        private Thread searchThread;
        private bool refresh;
        private List<PokeSpotDisplay> displayList;
        private ThreadDelegate gridUpdate;
        private BindingSource binding = new BindingSource();
        private bool isSearching = false;
        private uint shinyval;

        public PokeSpot()
        {
            InitializeComponent();
        }

        public PokeSpot(int TID, int SID)
        {
            InitializeComponent();
            natureType.SelectedIndex = 0;
            abilityType.SelectedIndex = 0;
            genderType.SelectedIndex = 0;
            pokeSpotType.SelectedIndex = 0;
            id.Text = TID.ToString();
            sid.Text = SID.ToString();
            k_dataGridView.DataSource = binding;
            k_dataGridView.AutoGenerateColumns = false;
        }

        private void search_Click(object sender, EventArgs e)
        {
            k_dataGridView.Rows.Clear();

            if (isSearching)
            {
                status.Text = "Previous search is still running";
                return;
            }

            displayList = new List<PokeSpotDisplay>();
            binding = new BindingSource { DataSource = displayList };
            k_dataGridView.DataSource = binding;
            status.Text = "Searching";
            shinyval = (uint.Parse(id.Text) ^ uint.Parse(sid.Text)) >> 3;

            uint seed = 0;
            uint.TryParse(textBoxSeed.Text, out seed);
            uint frame = 3000000;
            uint.TryParse(maxFrame.Text, out frame);
            uint nature = (uint)natureType.SelectedIndex;
            if (nature != 0)
                nature = natures[nature];
            uint gender = (uint)genderType.SelectedIndex;
            uint ability = (uint)abilityType.SelectedIndex;
            uint type = (uint)pokeSpotType.SelectedIndex;
            bool shinyCheck = Shiny_Check.Checked;

            searchThread =
                new Thread(
                    () =>
                    searchPokeSpot(seed, frame, nature, gender, ability, type, shinyCheck));
            searchThread.Start();

            var update = new Thread(updateGUI);
            update.Start();
        }

        private void searchPokeSpot(uint seed, uint frame, uint nature, uint gender, uint ability, uint type, bool shinyCheck)
        {
            for (uint x = 1; x < frame + 1; x++)
            {
                filterPokeSpot(seed, x, nature, gender, ability, type, shinyCheck);
                seed = forward(seed);
            }
            isSearching = false;
            status.Invoke((MethodInvoker)(() => status.Text = "Done. - Awaiting Command"));
        }

        private void filterPokeSpot(uint seed, uint frame, uint nature, uint gender, uint ability, uint type, bool shinyCheck)
        {
            uint rng3 = forward(forward(forward(seed)));
            uint rng4 = forward(rng3);
            uint pid = ((rng3 >> 16) << 16) + (rng4 >> 16);

            if (nature != 0)
            {
                if ((pid % 25) != nature)
                    return;
            }

            String shiny = "";
            if (shinyCheck)
            {
                uint shinyNum = ((pid >> 16) ^ (pid & 0xFFFF)) >> 3;
                if (shinyNum != shinyval)
                    return;
                shiny = "!!!";
            }

            if (ability != 0)
            {
                uint actualAbility = pid & 1;
                if (actualAbility != (ability - 1))
                    return;
            }

            if (gender != 0)
            {
                if (gender == 1)
                {
                    if ((pid & 255) < 127)
                    {
                        return;
                    }
                }
                else if (gender == 2)
                {
                    if ((pid & 255) > 126)
                    {
                        return;
                    }
                }
                else if (gender == 3)
                {
                    if ((pid & 255) < 191)
                    {
                        return;
                    }
                }
                else if (gender == 4)
                {
                    if ((pid & 255) > 190)
                    {
                        return;
                    }
                }
                else if (gender == 5)
                {
                    if ((pid & 255) < 64)
                    {
                        return;
                    }
                }
                else if (gender == 6)
                {
                    if ((pid & 255) > 63)
                    {
                        return;
                    }
                }
                else if (gender == 7)
                {
                    if ((pid & 255) < 31)
                    {
                        return;
                    }
                }
                else if (gender == 8)
                {
                    if ((pid & 255) > 30)
                    {
                        return;
                    }
                }
            }

            calcPokeSpot(seed, pid, frame, nature, gender, ability, type, shiny);

        }

        private void calcPokeSpot(uint seed, uint pid, uint frame, uint nature, uint gender, uint ability, uint type, String shiny)
        {
            uint call1 = forward(seed);

            if (call1 % 3 == 0)
            {
                String spotType = "";
                uint call2 = forward(call1);
                uint call3 = forward(call2);

                if (shiny == "")
                {
                    uint shinyNum = ((pid >> 16) ^ (pid & 0xFFFF)) >> 3;
                    if (shinyNum == shinyval)
                        shiny = "!!!";
                }

                if (frame != 0)
                {
                    if (frame == 1)
                    {
                        if (call3 % 100 > 49)
                            return;
                        spotType = "Common";
                    }
                    else if (frame == 2)
                    {
                        if (call3 % 100 < 50 && call3 % 100 > 84)
                            return;
                        spotType = "Uncommon";
                    }
                    else if (frame == 3)
                    {
                        if (call3 % 100 < 85)
                            return;
                        spotType = "Rare";
                    }
                    else if (frame == 4)
                    {
                        if (call2 % 100 > 9)
                            return;
                        spotType = "Munchlax";
                    }
                }
                else
                {
                    if (call2 % 100 < 10)
                        spotType = "Munchlax";
                    else if (call3 % 100 < 50)
                        spotType = "Common";
                    else if (call3 % 100 > 49 && call3 % 100 < 85)
                        spotType = "Uncommon";
                    else
                        spotType = "Rare";
                }

                String stringNature = Natures[nature];
                gender = pid & 255;
                char gender1;
                char gender2;
                char gender3;
                char gender4;
                ability = pid & 1;

                if (gender < 31)
                    gender1 = 'F';
                else
                    gender1 = 'M';

                if (gender < 64)
                    gender2 = 'F';
                else
                    gender2 = 'M';

                if (gender < 126)
                    gender3 = 'F';
                else
                    gender3 = 'M';

                if (gender < 191)
                    gender4 = 'F';
                else
                    gender4 = 'M';

                displayList.Add(new PokeSpotDisplay
                {
                    Seed = seed.ToString("x").ToUpper(),
                    Frame = (int)frame,
                    PID = pid.ToString("x").ToUpper(),
                    Shiny = shiny,
                    Type = spotType,
                    Nature = stringNature,
                    Ability = (int)ability,
                    Eighth = gender1,
                    Quarter = gender2,
                    Half = gender3,
                    Three_Fourths = gender4
                });

            }
        }

        private uint forward(uint seed)
        {
            return (seed * 0x343FD + 0x269EC3) & 0xFFFFFFFF;
        }

        private uint reverse(uint seed)
        {
            return (seed * 0xB9B33155 + 0xA170F641) & 0xFFFFFFFF;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                isSearching = false;
                status.Text = "Cancelled. - Awaiting Command";
                searchThread.Abort();
            }
        }

        private void updateGUI()
        {
            gridUpdate = dataGridUpdate;
            ThreadDelegate resizeGrid = k_dataGridView.AutoResizeColumns;
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

        private void anyPokeSpot_Click(object sender, EventArgs e)
        {
            pokeSpotType.SelectedIndex = 0;
        }

        private void anyNature_Click(object sender, EventArgs e)
        {
            natureType.SelectedIndex = 0;
        }

        private void anyGender_Click(object sender, EventArgs e)
        {
            genderType.SelectedIndex = 0;
        }

        private void anyAbility_Click(object sender, EventArgs e)
        {
            abilityType.SelectedIndex = 0;
        }
    }
}