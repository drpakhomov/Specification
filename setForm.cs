using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Specification
{
    public partial class setForm : Form
    {
        bool _edit = false;
        public setForm(string set, bool edit)
        {
            InitializeComponent();
            setName.Text = set;
            _edit = edit;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (setName.Text != "")
            {
                if (_edit)
                    mainForm.selfRef.acceptSetChanges(setName.Text, true);
                else
                    mainForm.selfRef.acceptSetChanges(setName.Text, false);
                this.Close();
            }
            else
                MessageBox.Show("Введите имя КТС!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
