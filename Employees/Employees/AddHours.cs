using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employees
{
    public partial class AddHours : Form
    {
        public int Hours { get; set; }

        public AddHours(string project)
        {
            InitializeComponent();
            lblProject.Text = project;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hours = int.Parse(txtWorkingHours.Text);
            Close();
        }

    }
}
