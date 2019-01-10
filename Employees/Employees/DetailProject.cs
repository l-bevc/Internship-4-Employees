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
    public partial class DetailProject : Form
    {
        private List<RoleEnums> role;
        private ProjectItem selectedProject;
        public DetailProject(ProjectItem project)
        {
            InitializeComponent();
            selectedProject = project;
            role=new List<RoleEnums>();
            lstDetails.Items.Add(project.ProjectName);
            lstDetails.Items.Add(project.DateOfBeginning.ToString("dd.MM.yyyy") + "-" + project.DateOfEnd.ToString("dd.MM.yyyy"));

            foreach (var employee in project.ListOfEmployees.Distinct())
            {
               role.Add(employee.Role);
            }
            var designersCount = role.Count(role => role == RoleEnums.Dizajner);
            var developersCount = role.Count(role => role == RoleEnums.Programer);
            var accountantsCount = role.Count(role=> role== RoleEnums.Računovođa);
            var secretariesCount = role.Count(role => role == RoleEnums.Tajnik);
            lstDetails.Items.Add($"Dizajneri {(designersCount)}");
            ListOfEmployeeOfSomeRole(RoleEnums.Dizajner);
            lstDetails.Items.Add($"Programeri {(developersCount)}");
            ListOfEmployeeOfSomeRole(RoleEnums.Programer);
            lstDetails.Items.Add($"Računovođe {(accountantsCount)}");
            ListOfEmployeeOfSomeRole(RoleEnums.Računovođa);
            lstDetails.Items.Add($"Tajnici {(secretariesCount)}");
            ListOfEmployeeOfSomeRole(RoleEnums.Tajnik);
        }

        public void ListOfEmployeeOfSomeRole(RoleEnums role)
        {
            foreach (var employee in selectedProject.ListOfEmployees.Distinct())
            {
                if (employee.Role == role)
                    foreach (var employeeHours in selectedProject.EmployeesWithHours)
                    {
                        if (employeeHours.Item1 == employee)
                        {
                            lstDetails.Items.Add($"{employee.Name} {employee.Surname} ({employeeHours.Item2})");
                        }
                    }                    
            }            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
