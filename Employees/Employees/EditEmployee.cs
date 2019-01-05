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
using Employess.Infrastructure.Extensions;

namespace Employees
{
    public partial class EditEmployee : Form
    {
        public EmployeeItem Employee;
        private List<ProjectItem> _projectItems;
        public EditEmployee(EmployeeItem employeeItem, List<ProjectItem> projects)
        {
            InitializeComponent();
            cmbPositions.DataSource = Enum.GetValues(typeof(RoleEnums));
            Employee = employeeItem;
            _projectItems = projects;
            txtName.Text = employeeItem.Name;
            txtSurname.Text = employeeItem.Surname;
            txtOib.Text = employeeItem.Oib;
            txtOib.Enabled = false;
            dtpDateOfBirth.Value = employeeItem.DateOfBirth;
            cmbPositions.SelectedItem = employeeItem.Role;
            foreach (var project in Employee.ProjectsOfEmployee)
            {
                foreach (var projectItem in _projectItems)
                {
                    if(projectItem.ProjectName==project)
                        chkProjects.Items.Add(projectItem.ProjectName, true);
                }
            }
            foreach (var project in _projectItems)
            {
                if (!chkProjects.Items.Contains(project.ProjectName))
                {
                    chkProjects.Items.Add(project.ProjectName, false);
                }
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
                Employee = new EmployeeItem(name.FirstCharOfEveryWordToUpper(),
                    surname.FirstCharOfEveryWordToUpper(), birth, oib.TrimAndRemoveMultipleWhitespaces(), role);
                Employee.ProjectsOfEmployee = new List<string>();
                foreach (var checkedItem in chkProjects.CheckedItems)
                {
                    foreach (var project in _projectItems)
                    {
                        if (project.ProjectName == checkedItem.ToString())
                        {
                            Employee.ProjectsOfEmployee.Add(project.ProjectName);
                            //NewEmployee.CalculatingHours(project.WorkingHours);
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
