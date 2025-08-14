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
    public partial class DismissAlarmForm : Form
    {
        public DismissAlarmForm(string customMessage)
        {
            InitializeComponent();
            lblMessage.Text = customMessage;
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

        }
    }

}
