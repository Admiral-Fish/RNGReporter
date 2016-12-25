using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
//using System.Threading;

namespace RNGReporter
{
    public partial class TwoStageTimer : Form
    {
        public TwoStageTimer()
        {
            InitializeComponent();
        }

        Timer StageOne;
        Timer StageTwo;

        //TimerCallback timerDelegate = new TimerCallback(

        DateTime startTime;
        DateTime endTime;
        DateTime blinkTime;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            label2.Text = Convert.ToString(String.Format("{0:0.00}", double.Parse(textBox1.Text) / 60.0));
            label10.Text = Convert.ToString(double.Parse(textBox2.Text) + double.Parse(label2.Text) % 60);
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            label10.Text = Convert.ToString(double.Parse(textBox2.Text) + double.Parse(label2.Text) % 60);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StageOne = new Timer();
            StageOne.Interval = 1;
            startTime = DateTime.Now;
            endTime = DateTime.Now.AddSeconds(Convert.ToInt64(textBox2.Text));
            StageOne.Enabled = true;
            StageOne.Start();
            StageOne.Tick += new EventHandler(StageOne_Tick);
        }

        public void StageOne_Tick(object sender, EventArgs eArgs)
        {
            if (sender == StageOne)
            {
                TimeSpan diff = endTime.Subtract(startTime);

                if (endTime.CompareTo(startTime) <= 0)
                {
                    StageOne.Stop();
                    StageTwo = new Timer();
                    StageTwo.Interval = 1;
                    startTime = DateTime.Now;
                    endTime = DateTime.Now.AddMilliseconds(double.Parse(label2.Text) * 1000);
                    button1.BackColor = System.Drawing.Color.Green;                    
                    StageTwo.Start();
                    StageTwo.Tick += new EventHandler(StageTwo_Tick);
                    System.Console.Beep(1760, 100);                    
                }

                if (diff.Seconds <= Convert.ToInt16(textBox3.Text) && diff.Seconds > 0 && diff.Milliseconds <= 40)
                {
                    button1.BackColor = System.Drawing.Color.Red;
                    blinkTime = DateTime.Now.AddMilliseconds(150);
                    System.Console.Beep(880, 100);
                }

                if (diff.Minutes < 100)
                {
                    label4.Text = String.Format("{0:00}", diff.Minutes);
                }
                else
                {
                    label4.Text = String.Format("{0:n}", diff.Minutes);
                }
                
                label5.Text = String.Format("{0:00}", diff.Seconds);
                label6.Text = String.Format("{0:00}", diff.Milliseconds / 10);

                startTime = DateTime.Now;

                if (blinkTime.CompareTo(DateTime.Now) <= 0)
                {
                    button1.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        public void StageTwo_Tick(object sender, EventArgs eArgs)
        {
            if (sender == StageTwo)
            {
                TimeSpan diff = endTime.Subtract(startTime);

                if (endTime.CompareTo(startTime) <= 0)
                {
                    System.Console.Beep(1760, 100);
                    button1.BackColor = System.Drawing.Color.Green;
                    label6.Text = "00";
                    StageTwo.Stop();
                    return;
                }

                if (diff.Seconds <= Convert.ToInt32(textBox4.Text) && diff.Seconds > 0 && diff.Milliseconds <= 40)
                {
                    button1.BackColor = System.Drawing.Color.Red;
                    blinkTime = DateTime.Now.AddMilliseconds(150);
                    System.Console.Beep(880, 100);
                }

                label4.Text = String.Format("{0:00}", diff.Minutes);
                label5.Text = String.Format("{0:00}", diff.Seconds);
                label6.Text = String.Format("{0:00}", diff.Milliseconds / 10);

                startTime = DateTime.Now;

                if (blinkTime.CompareTo(DateTime.Now) <= 0)
                {
                    button1.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }



    }
}
