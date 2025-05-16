using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace lab9_reestr
{
    public partial class EditValueForm: Form
    {
        public string ValueName { get; private set; }
        public object Value { get; private set; }
        public RegistryValueKind ValueKind { get; private set; } = RegistryValueKind.String;

        public EditValueForm(string name, RegistryValueKind kind, object currentValue)
        {
            InitializeComponent();
            txtName.Text = name;
            cbType.SelectedItem = kind;

            //cbType.DataSource = Enum.GetValues(typeof(RegistryValueKind));
        }

        public EditValueForm()
        {
            InitializeComponent();

            cbType.DataSource = Enum.GetValues(typeof(RegistryValueKind));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ValueName = txtName.Text;
            Value = txtValue.Text;
            ValueKind = (RegistryValueKind)cbType.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditValueForm_Load(object sender, EventArgs e)
        {

        }
    }
}
