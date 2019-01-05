using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Data.Models;
using Employess.Infrastructure.Extensions;

namespace Employees.Domain.Repositories
{
    public class EmployeeItemRepository
    {
        public List<EmployeeItem> EmployeeItems;
        public List<string> EmployeeOibs;
        public EmployeeItemRepository()
        {
            EmployeeItems = new List<EmployeeItem>()
            {
                new EmployeeItem("Marin", "Maric", new DateTime(1995,06,15),"123456789", RoleEnums.Programer), 
                new EmployeeItem("Laura", "Bevc", new DateTime(1995,12,05),"12312312", RoleEnums.Računovođa)
            };
        }

        public List<EmployeeItem> GetAllEmployeeItems()
        {
            EmployeeOibs=new List<string>();
            foreach (var employee in EmployeeItems)
            {
               EmployeeOibs.Add(employee.Oib.TrimAndRemoveMultipleWhitespaces());
            }
            return EmployeeItems;
        }
        
        public bool Add(EmployeeItem employeeToAdd)
        {
            var truth = true;
            foreach (var employee in EmployeeItems)
            {  
                if (employeeToAdd.Oib.TrimAndRemoveMultipleWhitespaces() ==
                    employee.Oib.TrimAndRemoveMultipleWhitespaces())
                    truth = false;
            }

            if (!truth) return false;
            EmployeeItems.Add(employeeToAdd);
            return true;
        }

        public bool Edit(EmployeeItem employeeToEdit)
        {
            EmployeeItem itemToDelete = null;
            foreach (var employeeItem in GetAllEmployeeItems())
            {
                if (employeeItem.Oib == employeeToEdit.Oib)
                    itemToDelete = employeeItem;
            }
            if (itemToDelete == null) return false;
            EmployeeItems.Remove(itemToDelete);
            EmployeeOibs.Remove(itemToDelete.Oib);
            EmployeeItems.Add(employeeToEdit);
            EmployeeOibs.Add(employeeToEdit.Oib);
            return true;
        }

        public bool Delete(string oib)
        {
            EmployeeItem itemToDelete = null;
            foreach (var employeeItem in GetAllEmployeeItems())
            {
                if (employeeItem.Oib == oib)
                    itemToDelete = employeeItem;
            }
            if (itemToDelete == null) return false;
            EmployeeItems.Remove(itemToDelete);
            EmployeeOibs.Remove(itemToDelete.Oib);
            return true;
        }
    }
}
