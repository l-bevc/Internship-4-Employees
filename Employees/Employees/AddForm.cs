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
    public partial class AddForm : Form
    {
        public ProjectItem NewProject;
        private List<ProjectItem> _projects;
        private List<EmployeeItem> _employeeItems;
        private List<string> _projectNames;
        public List<int> Hours;
        public AddForm(List<ProjectItem>projectItems, List<EmployeeItem> employees)
        {
            InitializeComponent();
            _projects = projectItems;
            _employeeItems = employees;
            _projectNames=new List<string>();
            Hours = new List<int>();
            foreach (var employee in _employeeItems)
            {
                chkEmployees.Items.Add(employee.Oib + " " + employee.Name + " " + employee.Surname);
            }

            foreach (var project in _projects)
            {
                _projectNames.Add(project.ProjectName.ToLower());
            }
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var beginning = dtpBegin.Value;
            var end = dtpEnd.Value;
            if (!_projectNames.Contains(name.ToLower().TrimAndRemoveMultipleWhitespaces()) && beginning<end && chkEmployees.CheckedItems.Count!=0)
            {
                NewProject = new ProjectItem(name.TrimAndRemoveMultipleWhitespaces(),beginning,end);
                foreach (var checkedItem in chkEmployees.CheckedItems)
                {
                     foreach (var employee in _employeeItems)
                     {
                         if (employee.Oib == checkedItem.ToString().Split(' ')[0])
                         {
                            NewProject.ListOfEmployees.Add(employee);
                            var hoursForm = new AddHours(name);
                            hoursForm.ShowDialog();
                            Hours.Add(hoursForm.Hours);
                            employee.WorkingHours = hoursForm.Hours;
                            NewProject.ListOfProjectOfEmployee();
                            NewProject.EmployeesWithHours.Add(new Tuple<EmployeeItem, int>(employee, hoursForm.Hours));
                         }
                     }
                }
            }
            else
            {
                MessageBox.Show("Ime je jedinstveno, a početni datum je manji od krajnjega. Uz to projekt mora imati barem 1 radnika");
                return;
            }

            Close();
        }
    }
}
