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

namespace RNGReporter.Objects.Searchers
{
    internal abstract class Searcher
    {
        protected Button btnGenerate;
        protected Form caller;
        protected Thread[] jobs;
        protected int numThreads;
        protected ulong progressFound;
        protected ulong progressSearched;
        protected ulong progressTotal;
        protected bool refreshQueue;
        protected EventWaitHandle waitHandle;
        public abstract bool ParseInput();
        protected abstract Thread SearchThread(int threadNum);
        protected abstract Thread ProgressThread();

        public void RunSearch()
        {
            // disable the button
            SetGenerateButton(false);
            jobs = new Thread[numThreads];
            for (int i = 0; i < numThreads; ++i)
            {
                jobs[i] = SearchThread(i);
                jobs[i].Start();
                // still need to figure out how to fix this
                Thread.Sleep(200);
            }

            Thread progressJob = ProgressThread();
            progressJob.Start();
            progressJob.Priority = ThreadPriority.Lowest;
        }

        protected void ManageProgress(BindingSource bindingSource, DoubleBufferedDataGridView grid, FrameType frameType,
                                      int sleepTimer)
        {
            var progress = new Progress();
            progress.SetupAndShow(caller, 0, 0, false, true, waitHandle);

            progressSearched = 0;
            progressFound = 0;

            UpdateGridDelegate gridUpdater = UpdateGrid;
            var updateParams = new object[] {bindingSource};
            ResortGridDelegate gridSorter = ResortGrid;
            var sortParams = new object[] {bindingSource, grid, frameType};
            SetButtonDelegate setButton = SetGenerateButton;

            try
            {
                bool alive = true;
                while (alive)
                {
                    progress.ShowProgress(progressSearched/(float) progressTotal, progressSearched, progressFound);
                    if (refreshQueue)
                    {
                        caller.Invoke(gridUpdater, updateParams);
                        refreshQueue = false;
                    }
                    if (jobs != null)
                    {
                        foreach (Thread job in jobs)
                        {
                            if (job != null && job.IsAlive)
                            {
                                alive = true;
                                break;
                            }
                            alive = false;
                        }
                    }
                    if (sleepTimer > 0)
                        Thread.Sleep(sleepTimer);
                }
            }
            catch (ObjectDisposedException)
            {
                // This keeps the program from crashing when the Time Finder progress box
                // is closed from the Windows taskbar.
            }
            catch (Exception exception)
            {
                if (exception.Message != "Operation Cancelled")
                {
                    throw;
                }
            }
            finally
            {
                progress.Finish();

                if (jobs != null)
                {
                    foreach (Thread t in jobs)
                    {
                        if (t != null)
                        {
                            t.Abort();
                        }
                    }
                }

                caller.Invoke(setButton, true);
                caller.Invoke(gridSorter, sortParams);
            }
        }

        private void UpdateGrid(BindingSource bindingSource)
        {
            bindingSource.ResetBindings(false);
        }

        protected void SetGenerateButton(bool value)
        {
            btnGenerate.Enabled = value;
        }

        protected abstract void ResortGrid(BindingSource bindingSource, DoubleBufferedDataGridView dataGrid,
                                           FrameType frameType);

        #region Nested types

        #region Nested type: ResortGridDelegate

        protected delegate void ResortGridDelegate(
            BindingSource bindingSource, DoubleBufferedDataGridView dataGrid, FrameType frameType);

        #endregion

        #region Nested type: SetButtonDelegate

        protected delegate void SetButtonDelegate(bool value);

        #endregion

        #region Nested type: UpdateGridDelegate

        protected delegate void UpdateGridDelegate(BindingSource bindingSource);

        #endregion

        #endregion
    }
}