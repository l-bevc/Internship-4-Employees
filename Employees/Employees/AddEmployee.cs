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
using Employees.Domain.Repositories;
using Employess.Infrastructure.Extensions;

namespace Employees
{
    public partial class AddEmployee : Form
    {
        public bool Quit { get; set; }
        public EmployeeItem NewEmployee;
        public List<int> Hours;
        private List<ProjectItem> _projectItems;
        public AddEmployee(List<ProjectItem> projects)
        {
            InitializeComponent();
            cmbPositions.DataSource = Enum.GetValues(typeof(RoleEnums));
            cmbPositions.SelectedItem = RoleEnums.Tajnik;
            _projectItems = projects;
            Hours=new List<int>();
            foreach (var project in _projectItems)
            {
                chkProjects.Items.Add(project.ProjectName);
            }           
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Quit = false;
            var name = txtName.Text;
            var surname = txtSurname.Text;
            var oib = txtOib.Text;
            var birth = dtpDateOfBirth.Value;
            var role = (RoleEnums)cmbPositions.SelectedItem;
            var now = DateTime.Now;
            if (name != "" && surname != "" && (now.Year - birth.Year) >= 18)
            {
                NewEmployee = new EmployeeItem(name.FirstCharOfEveryWordToUpper(),
                    surname.FirstCharOfEveryWordToUpper(), birth, oib.TrimAndRemoveMultipleWhitespaces(), role);
                foreach (var checkedItem in chkProjects.CheckedItems)
                {
                    if (_projectItems != null)
                    {
                        foreach (var project in _projectItems)
                        {
                            if (project.ProjectName == checkedItem.ToString())
                            {
                                NewEmployee.ProjectsOfEmployee.Add(project.ProjectName);
                            }
                        }
                    }
                }

                foreach (var project in NewEmployee.ProjectsOfEmployee)
                {
                    var hoursForm= new AddHours(project, NewEmployee);
                    hoursForm.ShowDialog();
                    Hours.Add(hoursForm.Hours);
                }
            }
            else
            {
                MessageBox.Show("Unesi ime i prezime! Uz to moraš biti punoljetan!");
                return;
            }

            Close();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Quit = true;
            Close();
        }
    }
}
