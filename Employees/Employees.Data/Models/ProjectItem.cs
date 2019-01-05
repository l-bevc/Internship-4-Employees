using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employess.Infrastructure.Extensions;

namespace Employees.Data.Models
{
    public class ProjectItem
    {
        public string ProjectName { get; set; }
        public List<EmployeeItem> ListOfEmployees{ get; set; }
        public int WorkingHours { get; set; }
        public DateTime DateOfBeginning { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string StatusOfProject { get; set; }

        public ProjectItem(string projectName, DateTime dateOfBeginning, DateTime dateOfEnd, int workingHours)
        {
            ListOfEmployees=new List<EmployeeItem>();
            ProjectName = projectName;
            DateOfBeginning = dateOfBeginning;
            DateOfEnd = dateOfEnd;
            WorkingHours = workingHours;
            StatusOfProject = IsDone();
        }

        public void ListOfProjectOfEmployee()
        {
            foreach (var employee in ListOfEmployees)
            {
                if(!employee.ProjectsOfEmployee.Contains(ProjectName))
                    employee.ProjectsOfEmployee.Add(ProjectName);
            }
        }

        public string IsDone()
        {
            var status = "";
            var now = DateTime.Now;
            if (DateOfEnd > now)
            {
                status = "buduci";
                if (DateOfBeginning < now)
                    status = "sadasnji";
            }
            else
            {
                status = "gotov";
            }

            return status;
        }

        public bool IsOnlyEmployee(EmployeeItem employee)
        {
            var isOnly = false;
            if (ListOfEmployees.Count == 1)
            {
                if (ListOfEmployees[0].Oib == employee.Oib)
                    isOnly = true;
            }
            return isOnly;
        }

        public override string ToString()
        {
            var text = $"Ime: {ProjectName}, Zaposlenici:";
            foreach (var employee in ListOfEmployees)
            {
                text +=
                     $"{employee}";
                employee.CalculatingHours(WorkingHours);
            }

            text += $"Beginning {DateOfBeginning.ToString("dd-MM-yyyy")}, End: {DateOfEnd.ToString("dd-MM-yyyy")}";
            return text;
        }
    }
}
