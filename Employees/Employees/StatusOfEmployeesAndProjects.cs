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
    public partial class StatusOfEmployeesAndProjects : Form
    {
        public List<ProjectItem> ProjectItems { get; set; }
        public List<EmployeeItem> EmployeeItems { get; set; }
        private EmployeeItemRepository _employeeItemRepository;
        private ProjectItemRepository _projectItemRepository;
        public StatusOfEmployeesAndProjects()
        {
            InitializeComponent();
            _employeeItemRepository = new EmployeeItemRepository();
            _projectItemRepository = new ProjectItemRepository();
            AddRefreshListView();
            ProjectItems[0].ListOfEmployees.Add(EmployeeItems[0]);
            ProjectItems[1].ListOfEmployees.Add(EmployeeItems[1]);
            EmployeeItems[0].ProjectsOfEmployee.Add(ProjectItems[0].ProjectName);
            EmployeeItems[1].ProjectsOfEmployee.Add(ProjectItems[1].ProjectName);
        }

        public void AddRefreshListView()
        {
            chkEmployess.Items.Clear();
            EmployeeItems = _employeeItemRepository.GetAllEmployeeItems();
            ProjectItems = _projectItemRepository.GetAllProjectItems();
            foreach (var employeeItem in EmployeeItems)
            {
                chkEmployess.Items.Add(employeeItem);
            }
            foreach (var employee in EmployeeItems)
            {
                if (!_employeeItemRepository.EmployeeOibs.Contains(employee.Oib.TrimAndRemoveMultipleWhitespaces()))
                {
                    _employeeItemRepository.EmployeeOibs.Add(employee.Oib.TrimAndRemoveMultipleWhitespaces());
                    _employeeItemRepository.EmployeeItems.Add(employee);
                }
            }
            foreach (var project in ProjectItems)
            {
                if (!_projectItemRepository.ProjectNames.Contains(project.ProjectName))
                {
                    _projectItemRepository.ProjectNames.Add(project.ProjectName);
                    _projectItemRepository.ProjectItems.Add(project);
                }
                project.ListOfProjectOfEmployee();
            }
        }

        private void BtnProjects_Click(object sender, EventArgs e)
        {
            var projects = new ListOfProjects(ProjectItems, EmployeeItems);
            projects.ShowDialog();
            ProjectItems = projects.ListofProjectItems;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (var employeeItem in EmployeeItems)
            {
                MessageBox.Show(employeeItem.Name + employeeItem.ProjectsOfEmployee.Count().ToString());

            }
            foreach (var employeeItem in ProjectItems)
            {
                MessageBox.Show(employeeItem.ProjectName + employeeItem.ListOfEmployees.Count().ToString());
            }
            var countItems = chkEmployess.Items.Count;
            var addingEmployee = new AddEmployee(ProjectItems);
            addingEmployee.ShowDialog();
            _employeeItemRepository.Add(addingEmployee.NewEmployee);
            AddRefreshListView();
            var countAfterAdding = chkEmployess.Items.Count;
            if (countItems == countAfterAdding)
            {
                MessageBox.Show("OIB mora biti jedinstven");
                return;
            }
            else
            {
                foreach (var project in addingEmployee.NewEmployee.ProjectsOfEmployee)
                {
                    foreach (var projectAll in ProjectItems)
                    {
                        if (projectAll.ProjectName == project)
                        {
                            projectAll.ListOfEmployees.Add(addingEmployee.NewEmployee);
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedPerson = chkEmployess.SelectedItem as EmployeeItem;
            if (selectedPerson == null) return;
            var dialogResult = MessageBox.Show("Jesi li siguran", "Oprez", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes )
            {
               foreach (var project in ProjectItems)
               {
                    if (!project.IsOnlyEmployee(selectedPerson))
                    {
                        _employeeItemRepository.Delete(selectedPerson.Oib);
                        project.ListOfEmployees.Remove(selectedPerson);
                        EmployeeItems.Remove(selectedPerson);
                    }
                    else
                    {
                         MessageBox.Show("Ne mozes ga obrisati jer je jedini na projektu");
                         return;
                    }
               }   
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }          
            AddRefreshListView();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            var selectedPerson = chkEmployess.SelectedItem as EmployeeItem;
            if (selectedPerson == null) return;
            var detailsEmployee = new DetailEmployee(selectedPerson, ProjectItems, _employeeItemRepository);
            detailsEmployee.ShowDialog();
            CountingProjectsAfterEdit(selectedPerson, detailsEmployee.Employee, detailsEmployee.Employee.ProjectsOfEmployee);
            AddRefreshListView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedPerson = chkEmployess.SelectedItem as EmployeeItem;
            if (selectedPerson == null) return;
            var editTodo = new EditEmployee(selectedPerson, ProjectItems);
            editTodo.ShowDialog();
            _employeeItemRepository.Edit(editTodo.Employee);
            CountingProjectsAfterEdit(selectedPerson, editTodo.Employee, editTodo.Employee.ProjectsOfEmployee);
            AddRefreshListView();
        }

        public void CountingProjectsAfterEdit(EmployeeItem selectedPerson,EmployeeItem employee, List<string>projectNames)
        {
            foreach (var checkedItem in projectNames)
            {
                foreach (var project in ProjectItems)
                {
                    if (project.ListOfEmployees.Contains(selectedPerson))
                        project.ListOfEmployees.Remove(selectedPerson);
                    if (project.ProjectName == checkedItem.ToString())
                    {
                        if (!project.ListOfEmployees.Contains(employee))
                            project.ListOfEmployees.Add(employee);
                        //NewEmployee.CalculatingHours(project.WorkingHours);
                    }
                }
            }
            foreach (var projectItem in ProjectItems)
            {
                if (projectItem.ListOfEmployees.Count == 0)
                {
                    MessageBox.Show("Mora ostati barem 1 osoba na projektu " + projectItem.ProjectName);
                    return;
                }
            }
        }

    }
}
