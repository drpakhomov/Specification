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
    public partial class equipForm : Form
    {
        bool _edit = false, _kts = false;
        public equipForm(string name, int code, string seller, string producer, int count, int price, int currency, int delivery_type, bool edit, bool kts)
        {
            InitializeComponent();
            setName.Text = name;
            textBox1.Text = code.ToString();
            textBox2.Text = seller;
            textBox3.Text = producer;
            numericUpDown1.Value = count;
            textBox5.Text = price.ToString();
            comboBox1.SelectedIndex = currency;
            comboBox2.SelectedIndex = delivery_type;
            _edit = edit;
            _kts = kts;

            List<string> list = mainForm.selfRef.mySQLSelect("SELECT DISTINCT seller FROM catalog");
            for (int i = 0; i < list.Count; i++)
                    comboBox4.Items.Add(list[i]);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (setName.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && numericUpDown1.Value > 0 && comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                mainForm.selfRef.acceptEquipChanges(setName.Text, Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(textBox5.Text), comboBox1.SelectedIndex, comboBox2.SelectedIndex, _edit, _kts);
                this.Close();
            }
            else
                MessageBox.Show("Заполнены не все поля!", "Мастер спецификаций", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            List<string> list = mainForm.selfRef.mySQLSelect("SELECT DISTINCT * FROM catalog WHERE seller = '" + comboBox4.SelectedItem.ToString() + "'");
            for (int i = 1; i <= list.Count; i++)
                if (i % 6 == 0)
                    comboBox3.Items.Add(list[i - 4]);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = mainForm.selfRef.mySQLSelect("SELECT * FROM catalog WHERE seller = '" + comboBox4.SelectedItem.ToString() + "' AND equipment_name = '" + comboBox3.SelectedItem.ToString() + "'");
            setName.Text = list[2];
            textBox1.Text = list[1];
            textBox2.Text = list[0];
            textBox3.Text = list[4];
            textBox5.Text = list[3];
            comboBox1.SelectedIndex = Convert.ToInt32(list[5]);
        }
    }
}
