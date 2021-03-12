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
    public partial class catalogForm : Form
    {
        public catalogForm()
        {
            InitializeComponent();

            List<string> list = mainForm.selfRef.mySQLSelect("SELECT DISTINCT seller FROM catalog");
            for (int i = 0; i < list.Count; i++)
                comboBox4.Items.Add(list[i]);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (setName.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox5.Text != "" && comboBox1.SelectedIndex >= 0 && comboBox4.Text != "")
            {
                mainForm.selfRef.mySQLExecute("INSERT INTO catalog VALUES ('" + comboBox4.Text + "','" + textBox1.Text + "','" + setName.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + comboBox1.SelectedIndex + "')");
                listBox1.Items.Add(setName.Text);
                if (!comboBox4.Items.Contains(comboBox4.Text))
                    comboBox4.Items.Add(comboBox4.Text);
                /*setName.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
                comboBox1.SelectedIndex = -1;*/
                MessageBox.Show("Товар успешно добавлен в справочник!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Заполнены не все поля!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> list = mainForm.selfRef.mySQLSelect("SELECT * FROM catalog WHERE seller = '" + comboBox4.SelectedItem.ToString() + "'");
            for (int i = 1; i <= list.Count; i++)
                if (i % 6 == 0)
                    listBox1.Items.Add(list[i - 4]);
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            listBox1.Items.Clear();
            button1.Enabled = false;
            if (comboBox4.Items.Contains(comboBox4.Text))
            {
                List<string> list = mainForm.selfRef.mySQLSelect("SELECT * FROM catalog WHERE seller = '" + comboBox4.Text + "'");
                for (int i = 1; i <= list.Count; i++)
                    if (i % 6 == 0)
                        listBox1.Items.Add(list[i - 4]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && comboBox4.Text != "")
            {
                button1.Enabled = true;
                List<string> list = mainForm.selfRef.mySQLSelect("SELECT * FROM catalog WHERE seller = '" + comboBox4.SelectedItem.ToString() + "' AND equipment_name = '" + listBox1.SelectedItem.ToString() + "'");
                setName.Text = list[2];
                textBox1.Text = list[1];
                comboBox4.Text = list[0];
                textBox3.Text = list[4];
                textBox5.Text = list[3];
                comboBox1.SelectedIndex = Convert.ToInt32(list[5]);
            }
            else
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                mainForm.selfRef.mySQLExecute("DELETE FROM catalog WHERE seller = '" + comboBox4.Text + "' AND equipment_name = '" + setName.Text + "'");
                List<string> list = mainForm.selfRef.mySQLSelect("SELECT * FROM catalog WHERE seller = '" + comboBox4.Text + "'");
                if (list.Count == 0)
                {
                    comboBox4.Items.Remove(comboBox4.Text);
                    comboBox4.Text = "";
                }
                setName.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                textBox5.Text = "";
                comboBox1.SelectedIndex = -1;
            }
        }
    }
}
