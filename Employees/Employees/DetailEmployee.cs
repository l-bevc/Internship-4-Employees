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
        public bool Return { get; set; }
        private List<ProjectItem> _projectItems;
        public List<int> Hours;
        public EmployeeItem Employee;
        private EmployeeItemRepository EmployeeItemRepository;
        public DetailEmployee(EmployeeItem selectedEmployee, List<ProjectItem> projects, EmployeeItemRepository employeeItemRepository)
        {
            InitializeComponent();
            Return = false;
            Hours=new List<int>();
            _projectItems = projects;
            Employee = selectedEmployee;
            EmployeeItemRepository = employeeItemRepository;
            var listOfProjects = Projects();
            lstDetails.Items.Add(selectedEmployee.ToString());
            lstDetails.Items.Add("PROJEKTI:");
            lstDetails.Items.Add($"Zaposlenik je završio: {listOfProjects[0]}");
            lstDetails.Items.Add($"Zaposlenik radi na: {listOfProjects[1]}");
            lstDetails.Items.Add($"Zaposlenik će raditi na: {listOfProjects[2]}");
            lstDetails.Items.Add($"Ovaj tjedan radi: {Employee.WorkingHours} sati");
            if (Employee.WorkingHours < 30)
                btnEdit.BackColor = Color.Yellow;
            else if (Employee.WorkingHours < 41)
                btnEdit.BackColor = Color.Green;
            else
                btnEdit.BackColor = Color.Red;

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
                        {
                            listPastProgressFutureProject[1]++;
                            foreach (var employee in projectInProjects.EmployeesWithHours)
                            {
                                if (employee.Item1 == Employee)
                                    Employee.WorkingHours += employee.Item2;
                            }
                        }
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
            EmployeeItemRepository.Edit(editTodo.Employee);
            Hours = editTodo.Hours;
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Return = true;
            Close();
        }
    }
}
