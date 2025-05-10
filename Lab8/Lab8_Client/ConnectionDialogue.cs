using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class ConnectionDialogue : Form
    {
        public string Nickname { get; private set; }
        public ConnectionDialogue(string defaultName)
        {
            Nickname = defaultName;
            InitializeComponent();
            textBoxNickname.Text = Nickname;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNickname.Text))
            {
                MessageBox.Show("Nickname cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Nickname = textBoxNickname.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
