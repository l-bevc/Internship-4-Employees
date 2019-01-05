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
        public EmployeeItem NewEmployee;
        private List<ProjectItem> _projectItems;
        public AddEmployee(List<ProjectItem> projects)
        {
            InitializeComponent();
            cmbPositions.DataSource = Enum.GetValues(typeof(RoleEnums));
            cmbPositions.SelectedItem = RoleEnums.Tajnik;
            _projectItems = projects;
            foreach (var project in _projectItems)
            {
                chkProjects.Items.Add(project.ProjectName);
            }           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                                //NewEmployee.CalculatingHours(project.WorkingHours);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Unesi ime i prezime! Uz to moraš biti punoljetan!");
                return;
            }

            Close();
        }
    }
}
