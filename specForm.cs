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
    public partial class specForm : Form
    {
        bool _edit = false;
        public specForm(string specification, bool edit)
        {
            InitializeComponent();
            specName.Text = specification;
            _edit = edit;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (specName.Text != "")
            {
                if (_edit)
                    mainForm.selfRef.acceptSpecChanges(specName.Text, true);
                else
                    mainForm.selfRef.acceptSpecChanges(specName.Text, false);
                this.Close();
            }
            else
                MessageBox.Show("Введите имя спецификации!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
