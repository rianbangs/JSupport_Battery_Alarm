using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSupport
{
    //assets link 
    //https://ca.pinterest.com/pin/8725793024176380/
    public partial class DismissAlarmForm : Form
    {
        private Form1 _mainForm;
        private string _profileName;
        public DismissAlarmForm(string customMessage, Form1 mainForm, string profileName)
        {

            InitializeComponent();

            lblMessage.Text = customMessage;
            _mainForm = mainForm;
            _profileName = profileName;
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

        private void DismissAlarmForm_Load(object sender, EventArgs e)
        {
            lblMessage.Font = new Font(lblMessage.Font.FontFamily, 10);
            lblMessage.AutoSize = true;
            lblMessage.MaximumSize = new Size(this.ClientSize.Width - 20, 0); // Max width
            lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;




            this.FormBorderStyle = FormBorderStyle.None; // Hide form borders
            this.WindowState = FormWindowState.Maximized; // Maximize window
            this.TopMost = true; // Stay on top

            axWindowsMediaPlayer1.URL = "assets\\alarm video.mp4";
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.SendToBack();

            lblMessage.BringToFront();
            btnDismiss.BringToFront();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                _mainForm.StopAlarm(_profileName);

                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
    }

}
