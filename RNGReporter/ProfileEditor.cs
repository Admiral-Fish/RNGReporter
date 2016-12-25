using System;
using System.Diagnostics;
using System.Windows.Forms;
using RNGReporter.Objects;
using Version = RNGReporter.Objects.Version;

namespace RNGReporter
{
    public partial class ProfileEditor : Form
    {
        // todo: make it load a profile as a constructor


        public ProfileEditor()
        {
            InitializeComponent();
        }

        public Profile Profile
        {
            get { return GetProfile(); }
            set { LoadProfile(value); }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GetProfile() == null)
            {
                if (MessageBox.Show(
                    "The inputed profile data in invalid. The profile can not be saved.\r\nWould you like to continue editing this profile?",
                    "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes) return;
            }
            CheckNazos();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public Profile GetProfile()
        {
            ushort id, sid;
            uint vcount, vframe, timer0min, timer0max, gxstat;
            ulong mac;
            //validation
            if (!FormsFunctions.ParseInputD(maskedTextBoxID, out id) ||
                !FormsFunctions.ParseInputD(maskedTextBoxSID, out sid) ||
                !FormsFunctions.ParseInputH(textBoxMAC, out mac) ||
                !FormsFunctions.ParseInputH(textBoxVCount, out vcount) ||
                !FormsFunctions.ParseInputH(textBoxVFrame, out vframe) ||
                !FormsFunctions.ParseInputH(textBoxTimer0Min, out timer0min) ||
                !FormsFunctions.ParseInputH(textBoxTimer0Max, out timer0max) ||
                !FormsFunctions.ParseInputH(textBoxGxStat, out gxstat))
                return null;
            var profile = new Profile
                {
                    Name = textBoxName.Text,
                    ID = id,
                    SID = sid,
                    MAC_Address = mac,
                    Version = (Version) comboBoxVersion.SelectedIndex,
                    Language = (Language) comboBoxLanguage.SelectedIndex,
                    DSType = (DSType) comboBoxDSType.SelectedIndex,
                    VCount = vcount,
                    VFrame = vframe,
                    Timer0Min = timer0min,
                    Timer0Max = timer0max,
                    GxStat = gxstat,
                    Keypresses = GetKeypresses(),
                    SoftReset = checkBoxSoftReset.Checked,
                    SkipLR = checkBoxSkipLR.Checked,
                    MemoryLink = checkBoxMemoryLink.Checked
                };
            if (Nazos.Nazo(profile) == null)
            {
                MessageBox.Show("Warning: this version of the game is currently unsupported.",
                                "Unsupported game: " + profile.Version + " " + profile.Language + " " + profile.DSType,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return profile;
        }

        public void LoadProfile(Profile profile)
        {
            if (profile == null) return;
            textBoxName.Text = profile.Name;
            maskedTextBoxID.Text = profile.ID.ToString();
            maskedTextBoxSID.Text = profile.SID.ToString();
            textBoxMAC.Text = profile.MAC_Address.ToString("X");
            comboBoxVersion.SelectedIndex = (int) profile.Version;
            comboBoxLanguage.SelectedIndex = (int) profile.Language;
            comboBoxDSType.SelectedIndex = (int) profile.DSType;
            textBoxVCount.Text = profile.VCount.ToString("X");
            textBoxVFrame.Text = profile.VFrame.ToString("X");
            textBoxTimer0Min.Text = profile.Timer0Min.ToString("X");
            textBoxTimer0Max.Text = profile.Timer0Max.ToString("X");
            textBoxGxStat.Text = profile.GxStat.ToString("X");
            LoadKeypresses(profile.Keypresses);
            checkBoxSoftReset.Checked = profile.SoftReset;
            checkBoxSkipLR.Checked = profile.SkipLR;
            checkBoxMemoryLink.Checked = profile.MemoryLink;
        }


        private void LoadKeypresses(byte keypresses)
        {
            //  layout of keypress settings byte
            //  each bit is a flag for the number of keypresses
            //  7 6 5 4 3 2 1 0
            if (keypresses == 0)
            {
                comboBoxKeypresses.CheckBoxItems[1].Checked = true;
                return;
            }
            byte b = 0x1;
            for (int i = 1; i < 5; ++i)
            {
                comboBoxKeypresses.CheckBoxItems[i].Checked = (keypresses & b) != 0;
                b <<= 1;
            }
        }

        private byte GetKeypresses()
        {
            int keypresses = 0;
            byte b = 0x1;
            //have to start at 1 because a phantom element is added, not sure why
            for (int i = 1; i < 5; ++i)
            {
                keypresses += comboBoxKeypresses.CheckBoxItems[i].Checked ? b : 0;
                b <<= 1;
            }
            return (byte) keypresses;
        }

        private void FocusControl(object sender, MouseEventArgs e)
        {
            ((Control) sender).Focus();
        }

        private void btnParameters_Click(object sender, EventArgs e)
        {
            // this needs to be properly updated
            var dsidwizard = new DSIDWizard();
            dsidwizard.Show();
            dsidwizard.Focus();

            Close();
        }

        private void comboBoxKeypresses_MouseClick(object sender, MouseEventArgs e)
        {
            FocusControl(sender, e);
        }

        private void comboBoxVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxMemoryLink.Visible = comboBoxVersion.Text.Contains("2");
            CheckNazos();
        }

        private void CheckNazos()
        {
            Profile p = GetProfile();
            if (p != null && Nazos.Nazo(p) == null)
            {
                if (MessageBox.Show(
                    "We currently do not have information to be able to support this version of the game. Would you like to help out with adding support?",
                    "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("http://www.smogon.com/forums/showpost.php?p=4432994&postcount=977");
                }
            }
        }
    }
}