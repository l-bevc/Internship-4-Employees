using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employess.Infrastructure.Extensions;

namespace Employees.Data.Models
{
    public class EmployeeItem
    {
        public string Name{ get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Oib { get; set; }
        public RoleEnums Role { get; set; }
        public List<string> ProjectsOfEmployee { get; set; }
        public int WorkingHours { get; set; }

        public EmployeeItem(string name, string surname, DateTime dateOfBirth, string oib, RoleEnums role)
        {
            ProjectsOfEmployee=new List<string>();
            WorkingHours = 0;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Oib = oib;
            Role = role;  
            
        }

        public void CalculatingHours(int hours)
        {
            WorkingHours += hours;
        }

        public override string ToString()
        {
            var text =
                $"Ime: {Name}, Prezime: {Surname}, Datum rođenja: {DateOfBirth.ToString("dd-MM-yyyy")}, Oib: {Oib}, Uloga: {Role}";

            return text;
        }
    }
}
