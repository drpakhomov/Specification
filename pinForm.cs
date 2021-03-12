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
    public partial class pinForm : Form
    {
        public pinForm()
        {
            InitializeComponent();

            for (int i = 0; i < mainForm.selfRef.userList.Count; i++)
                comboBox1.Items.Add(mainForm.selfRef.userList[i].userName);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0) this.Close();
        }
    }
}
