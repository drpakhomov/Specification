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
    public partial class groupingForm : Form
    {
        public groupingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.selfRef.handleGrouping(0);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.selfRef.handleGrouping(1);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.selfRef.handleGrouping(2);
            this.Close();
        }
    }
}
