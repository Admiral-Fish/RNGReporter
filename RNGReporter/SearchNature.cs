using System;
using System.Drawing;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class SearchNature : Form
    {
        //  Array nature where we return our results
        private readonly int[] returnArray = new int[3];

        public SearchNature()
        {
            InitializeComponent();
        }

        public int[] ReturnArray
        {
            get { return returnArray; }
        }

        private void SearchNature_Load(object sender, EventArgs e)
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

            comboBoxNature1.Font = font;
            comboBoxNature2.Font = font;
            comboBoxNature3.Font = font;

            comboBoxNature1.DataSource = Nature.NatureDropDownCollectionSearchNatures();
            comboBoxNature2.DataSource = Nature.NatureDropDownCollectionSearchNatures();
            comboBoxNature3.DataSource = Nature.NatureDropDownCollectionSearchNatures();

            comboBoxNature1.SelectedIndex = 0;
            comboBoxNature2.SelectedIndex = 0;
            comboBoxNature3.SelectedIndex = 0;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            returnArray[0] = ((Nature) comboBoxNature1.SelectedItem).Number;
            returnArray[1] = ((Nature) comboBoxNature2.SelectedItem).Number;
            returnArray[2] = ((Nature) comboBoxNature3.SelectedItem).Number;
        }
    }
}