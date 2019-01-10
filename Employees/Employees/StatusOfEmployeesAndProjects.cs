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
            ProjectItems[0].EmployeesWithHours.Add(new Tuple<EmployeeItem, int>(EmployeeItems[0], 10));
            ProjectItems[1].ListOfEmployees.Add(EmployeeItems[1]);
            ProjectItems[1].EmployeesWithHours.Add(new Tuple<EmployeeItem, int>(EmployeeItems[1], 15));
            EmployeeItems[0].ProjectsOfEmployee.Add(ProjectItems[0].ProjectName);
            EmployeeItems[1].ProjectsOfEmployee.Add(ProjectItems[1].ProjectName);
            EmployeeItems[0].WorkingHours = 10;
            EmployeeItems[1].WorkingHours = 15;
            AddRefreshListView();
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
            var projects = new ListOfProjects(ProjectItems, EmployeeItems,_projectItemRepository);
            projects.ShowDialog();
            ProjectItems = projects.ListofProjectItems;
            AddRefreshListView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var countItems = chkEmployess.Items.Count;
            var addingEmployee = new AddEmployee(ProjectItems);
            addingEmployee.ShowDialog();
            if (!addingEmployee.Quit)
            {
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
                    var i = 0;
                    foreach (var project in addingEmployee.NewEmployee.ProjectsOfEmployee)
                    {
                        foreach (var projectAll in ProjectItems)
                        {
                            if (projectAll.ProjectName == project)
                            {
                                projectAll.ListOfEmployees.Add(addingEmployee.NewEmployee);
                                var employeeHours = new Tuple<EmployeeItem, int>(addingEmployee.NewEmployee,
                                    addingEmployee.Hours[i]);
                                projectAll.EmployeesWithHours.Add(employeeHours);
                                i++;
                            }
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
                        foreach (var employeeWithHours in project.EmployeesWithHours)
                        {
                            if (employeeWithHours.Item1 == selectedPerson)
                                project.EmployeesWithHours.Remove(employeeWithHours);
                        }
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
            if (!detailsEmployee.Return)
            {
                CountingProjectsAfterEdit(selectedPerson, detailsEmployee.Employee,
                    detailsEmployee.Employee.ProjectsOfEmployee);
                var i = 0;
                foreach (var project in detailsEmployee.Employee.ProjectsOfEmployee)
                {
                    foreach (var projectAll in ProjectItems)
                    {
                        if (projectAll.ProjectName == project)
                        {
                            projectAll.ListOfEmployees.Add(detailsEmployee.Employee);
                            foreach (var tuple in projectAll.EmployeesWithHours)
                            {
                                if (tuple.Item1 != detailsEmployee.Employee)
                                {
                                    var employeeHours= new Tuple<EmployeeItem, int>(detailsEmployee.Employee,
                                        detailsEmployee.Hours[i]);
                                    projectAll.EmployeesWithHours.Add(employeeHours);
                                    i++;
                                }
                            }
                        }
                    }
                }

                AddRefreshListView();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedPerson = chkEmployess.SelectedItem as EmployeeItem;
            if (selectedPerson == null) return;
            var editTodo = new EditEmployee(selectedPerson, ProjectItems);
            editTodo.ShowDialog();
            if (!editTodo.Quit)
            {
                foreach (var project in ProjectItems)
                {
                        if (project.ListOfEmployees.Contains(selectedPerson))
                        {
                            project.ListOfEmployees.Remove(selectedPerson);
                            var foundTuple =
                                project.EmployeesWithHours.FirstOrDefault(eri => eri.Item1.Oib == selectedPerson.Oib);
                            project.EmployeesWithHours.Remove(foundTuple);
                        }
                }
                _employeeItemRepository.Edit(editTodo.Employee);
                CountingProjectsAfterEdit(selectedPerson, editTodo.Employee, editTodo.Employee.ProjectsOfEmployee);
                var i = 0;
                foreach (var project in editTodo.Employee.ProjectsOfEmployee)
                {
                    foreach (var projectAll in ProjectItems)
                    {
                        if (projectAll.ProjectName == project)
                        {
                            projectAll.ListOfEmployees.Add(editTodo.Employee);
                            projectAll.EmployeesWithHours.Add(
                                new Tuple<EmployeeItem, int>(editTodo.Employee, editTodo.Hours[i]));
                            i++;
                        }
                    }
                }

                AddRefreshListView();
            }
        }

        public void CountingProjectsAfterEdit(EmployeeItem selectedPerson,EmployeeItem employee, List<string>projectNames)
        {
            foreach (var project in ProjectItems)
            {
                foreach (var checkedItem in projectNames)
                {
                    if (project.ProjectName == checkedItem)
                    {
                        if (!project.ListOfEmployees.Contains(employee))
                            project.ListOfEmployees.Add(employee);
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
