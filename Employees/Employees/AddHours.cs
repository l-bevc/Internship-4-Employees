using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees.Data.Models;

namespace Employees
{
    public partial class AddHours : Form
    {
        public int Hours { get; set; }

        public AddHours(string project, EmployeeItem person)
        {
            InitializeComponent();
            lblProject.Text = project;
            lblPerson.Text = $"Osoba: {person.Name} {person.Surname}";
            foreach (var projectItem in StatusOfEmployeesAndProjects.ProjectItemRepository.ProjectItems)
            {
                if (projectItem.ProjectName==project)
                {
                    var foundTuple =
                        projectItem.EmployeesWithHours.FirstOrDefault(eri => eri.Item1.Oib == person.Oib);
                    if ( foundTuple!=null && !string.IsNullOrEmpty(foundTuple.Item2.ToString()))
                        txtWorkingHours.Text = foundTuple.Item2.ToString();
                }
            }
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            Hours = int.Parse(txtWorkingHours.Text);
            if(Hours>0)
                Close();
            else
            {
                MessageBox.Show("Moraš unijeti broj sati veći od 0");
                return;
            }
        }

    }
}
