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
using System.IO;
using System.Windows.Forms;
using RNGReporter.Objects;
using RNGReporter.Properties;

namespace RNGReporter
{
    public partial class ProfileManager : Form
    {
        public ProfileManager()
        {
            InitializeComponent();
            dataGridViewValues.AutoGenerateColumns = false;

            if (File.Exists(Settings.Default.ProfileLocation))
                Profiles.LoadProfiles();
            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }

        private void dataGridViewValues_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hti = dataGridViewValues.HitTest(e.X, e.Y);

                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (!dataGridViewValues.Rows[Hti.RowIndex].Selected)
                    {
                        dataGridViewValues.ClearSelection();

                        (dataGridViewValues.Rows[Hti.RowIndex]).Selected = true;
                    }
                }
            }
        }

        private void ProfilesProfileManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (string.IsNullOrEmpty(Settings.Default.ProfileLocation))
            {
                Settings.Default.ProfileLocation = "profiles.xml";
                Settings.Default.Save();
            }
            Profiles.SaveProfiles(Settings.Default.ProfileLocation);
            Hide();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogXml.ShowDialog() == DialogResult.OK)
            {
                Profiles.LoadProfiles(openFileDialogXml.FileName);
                Settings.Default.ProfileLocation = openFileDialogXml.FileName;
                profilesBindingSource.DataSource = Profiles.List;
                profilesBindingSource.ResetBindings(false);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profiles.List.Clear();
            profilesBindingSource.ResetBindings(false);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogXml.ShowDialog() == DialogResult.OK)
            {
                Profiles.SaveProfiles(saveFileDialogXml.FileName);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            var editor = new ProfileEditor();
            if (editor.ShowDialog() != DialogResult.OK) return;
            Profile profile = editor.Profile;
            if (profile == null) return;
            Profiles.List.Add(profile);
            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count <= 0) return;
            int currentProfile = dataGridViewValues.SelectedRows[0].Index;
            var editor = new ProfileEditor {Profile = Profiles.List[currentProfile]};
            if (editor.ShowDialog() != DialogResult.OK) return;

            Profile profile = editor.Profile;
            if (profile == null) return;

            Profiles.List[currentProfile] = profile;
            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewValues.SelectedRows.Count; ++i)
            {
                // offset the row index with i because if more than one row is deleted we need to account for the shift
                // note: it's currently impossible to remove more than one row anyway
                Profiles.List.RemoveAt(dataGridViewValues.SelectedRows[i].Index - i);
            }
            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.SelectedRows.Count <= 0) return;
            Profile profile = Profiles.List[dataGridViewValues.SelectedRows[0].Index];
            Profiles.List.Add(profile);

            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }

        // todo: clean this up, change how it's done
        public void AddProfile(Profile profile)
        {
            var editor = new ProfileEditor {Profile = profile};
            if (editor.ShowDialog() != DialogResult.OK) return;
            Profile editorProfile = editor.Profile;
            if (editorProfile == null) return;

            Profiles.List.Add(editorProfile);
            profilesBindingSource.DataSource = Profiles.List;
            profilesBindingSource.ResetBindings(false);
        }
    }
}