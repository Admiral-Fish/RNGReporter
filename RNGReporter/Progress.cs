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
using System.Threading;
using System.Windows.Forms;

namespace RNGReporter
{
    public partial class Progress : Form, IMessageFilter
    {
        private const int WM_PAINT = 0x000F, WM_NCACTIVATE = 0x086;

        //private Form caller;
        //private DateTime showTime; //time when form should show, UTC
        private bool allowCancel; //controls whether a manual close should cause an exception
        private bool allowPause;
        private bool pleaseAbort; //set when user presses cancel
        private bool pleasePause; //set when user presses Pause
        private bool pleaseUnpause;
        private Cursor priorCursor = Cursors.Default;
        private EventWaitHandle waitHandle;

        public Progress()
        {
            InitializeComponent();
            //progressBar.MarqueeStart();
        }

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            bool isgoodtype = m.Msg == WM_PAINT;
            bool allowed = isgoodtype ||
                           (Visible && (m.HWnd == Handle || m.HWnd == buttonCancel.Handle));
            return !allowed;
        }

        #endregion

        public void SetupAndShow(
            Form caller,
            int searched,
            int found,
            bool delayedDisplay,
            bool allowCancel)
        {
            this.allowCancel = allowCancel;
            buttonCancel.Visible = allowCancel;
            allowPause = false;
            buttonPause.Visible = allowPause;
            Show();
            /*
            if (caller == null)
            {
                if (Application.OpenForms.Count == 0) return;
                caller = Application.OpenForms[0];
            }
            this.allowCancel = allowCancel;
            buttonCancel.Visible = allowCancel;

            labelSearched.Text = searched.ToString();
            labelFound.Text = found.ToString();
            
            this.caller = caller;
            progressBar.Value = 0;
            if (delayedDisplay)
                showTime = DateTime.UtcNow.AddSeconds(0.5);
            else
            {
                showTime = DateTime.MinValue;
                ShowProgress(0, 0, 0);
            }

            priorCursor = caller.Cursor;
            Application.AddMessageFilter(this);
             * */
        }

        public void SetupAndShow(
            Form caller,
            int searched,
            int found,
            bool delayedDisplay,
            bool allowCancel,
            EventWaitHandle waitHandle)
        {
            this.allowCancel = allowCancel;
            buttonCancel.Visible = allowCancel;

            allowPause = true;
            buttonPause.Visible = allowPause;
            this.waitHandle = waitHandle;

            Show();
        }

        public void ShowProgress(
            double percent,
            ulong searched,
            ulong found)
        {
            if (percent > 0)
            {
                try
                {
                    progressBar.MasterValue = (int) (percent*100.0);
                }
                catch
                {
                }
            }

            labelSearched.Text = searched.ToString();
            labelFound.Text = found.ToString();

            /*
            //  Paint
            if (!Visible && showTime < DateTime.UtcNow)
            {
                Show();
                Cursor = Cursors.WaitCursor;
                if (buttonCancel.Visible) buttonCancel.Focus();
            }
             */

            //  Only allow certain things
            if (pleaseAbort)
            {
                Application.RemoveMessageFilter(this);
                Hide();
                throw new Exception("Operation Cancelled");
            }
            if (allowPause)
            {
                if (pleasePause)
                {
                    waitHandle.Reset();
                    pleasePause = false;
                }
                if (pleaseUnpause)
                {
                    waitHandle.Set();
                    pleaseUnpause = false;
                }
            }
            Application.DoEvents();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            pleaseAbort = true;
        }

        public void Finish()
        {
            Application.DoEvents(); //this clears the event queue of unwanted clicks
            Hide();
            Application.RemoveMessageFilter(this);
            Dispose();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Text == "Pause")
            {
                pleasePause = true;
                buttonPause.Text = "Resume";
            }
            else
            {
                pleaseUnpause = true;
                buttonPause.Text = "Pause";
            }
        }
    }
}