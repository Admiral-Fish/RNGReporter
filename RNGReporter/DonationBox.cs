using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace RNGReporter
{
    internal partial class DonationBox : Form
    {
        public DonationBox()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.smogon.com/forums/showthread.php?t=83057");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"http://www.smogon.com/forums/showthread.php?t=83057");
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DonationBox_Load(object sender, EventArgs e)
        {
            string line;
            string versionString = "";
            string url = "http://www.eggmove.com/RNGReporter/versioninfo/996.txt";

            try
            {
                var client = new WebClient();
                versionString = client.DownloadString(url);

                textBox1.Text = versionString;
            }
            catch
            {
                if (File.Exists("readme.txt"))
                {
                    var fileStream = new StreamReader("readme.txt", Encoding.Unicode, false);

                    try
                    {
                        bool start = false;
                        while ((line = fileStream.ReadLine()) != null)
                        {
                            if (start)
                            {
                                if (line.Contains("New in RNG Reporter"))
                                    break;

                                versionString = versionString + line + Environment.NewLine;
                            }

                            if (line.Contains("New in RNG Reporter"))
                            {
                                start = true;
                                versionString = versionString + line + Environment.NewLine;
                            }
                        }

                        textBox1.Text = versionString;
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
            }
        }
    }
}