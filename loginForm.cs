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
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = loginBox.Text, password = passwordBox.Text;
            if (username == "")
                MessageBox.Show("Введите логин!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                List<string> list = mainForm.selfRef.mySQLSelect("SELECT user_name, user_password, user_id FROM users WHERE user_login = '" + username + "'");
                if (list.Count == 0)
                    MessageBox.Show("Неверный логин!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (list[1] != password)
                    MessageBox.Show("Неверный пароль!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    this.Close();
                    mainForm.selfRef.acceptLogin(list[0], Convert.ToInt32(list[2]));
                }
            }
        }
    }
}
