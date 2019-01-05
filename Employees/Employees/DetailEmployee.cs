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
    public partial class DetailEmployee : Form
    {
        private List<ProjectItem> _projectItems;
        public EmployeeItem Employee;
        private EmployeeItemRepository _employeeItemRepository;
        public DetailEmployee(EmployeeItem selectedEmployee, List<ProjectItem> projects, EmployeeItemRepository employeeItemRepository)
        {
            InitializeComponent();
            _projectItems = projects;
            Employee = selectedEmployee;
            _employeeItemRepository = employeeItemRepository;
            var listOfProjects = Projects();
            lstDetails.Items.Add(selectedEmployee.ToString());
            lstDetails.Items.Add("PROJEKTI:");
            lstDetails.Items.Add($"Zaposlenik je završio: {listOfProjects[0]}");
            lstDetails.Items.Add($"Zaposlenik radi na: {listOfProjects[1]}");
            lstDetails.Items.Add($"Zaposlenik će raditi na: {listOfProjects[2]}");
        }

        public List<int> Projects()
        {
            var listPastProgressFutureProject=new List<int> {0,0,0};
            foreach (var project in Employee.ProjectsOfEmployee)
            {
                foreach (var projectInProjects in _projectItems)
                {
                    if (projectInProjects.ProjectName == project)
                    {
                        if (projectInProjects.StatusOfProject == "gotov")
                            listPastProgressFutureProject[0]++;
                        else if (projectInProjects.StatusOfProject == "sadasnji")
                            listPastProgressFutureProject[1]++;
                        else
                            listPastProgressFutureProject[2]++;
                    }
                }
            }
            return listPastProgressFutureProject;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var editTodo = new EditEmployee(Employee, _projectItems);
            editTodo.ShowDialog();
            _employeeItemRepository.Edit(editTodo.Employee);
            Close();
        }
    }
}
