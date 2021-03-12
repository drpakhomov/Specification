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
    public partial class groupForm : Form
    {
        bool _edit = false;
        public groupForm(string set, bool edit)
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
                    mainForm.selfRef.acceptGroupChanges(setName.Text, true);
                else
                    mainForm.selfRef.acceptGroupChanges(setName.Text, false);
                this.Close();
            }
            else
                MessageBox.Show("Введите имя комплектной единицы!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
