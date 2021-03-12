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
    public partial class entityForm : Form
    {
        public entityForm(string text)
        {
            InitializeComponent();

            if (text != "")
                textBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Введите имя сущности!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mainForm.selfRef.entityName = textBox1.Text;
                this.Close();
            }
        }
    }
}
