using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Data.Models;

namespace Employees.Domain.Repositories
{
    public class ProjectItemRepository
    {
        public List<ProjectItem> ProjectItems;
        public List<string> ProjectNames;

        public ProjectItemRepository()
        {
            
            ProjectItems = new List<ProjectItem>()
            {
                new ProjectItem("DUMP Days", new DateTime(1995,12,10), new DateTime(1999,12,05), 30),
                new ProjectItem("DUMP Internship", new DateTime(2010,12,10), new DateTime(2020,12,05), 30)
            };
        }
        public List<ProjectItem> GetAllProjectItems()
        {
            ProjectNames=new List<string>();
            foreach (var project in ProjectItems)
            {
                ProjectNames.Add(project.ProjectName);
            }
            return ProjectItems;
        }

        public void Add(ProjectItem projectToAdd)
        {
            ProjectItems.Add(projectToAdd);
        }

        public bool Edit(ProjectItem projectItemToEdit)
        {
            ProjectItem itemToDelete = null;
            foreach (var projectItem in GetAllProjectItems())
            {
                if (projectItem.ProjectName == projectItemToEdit.ProjectName)
                    itemToDelete = projectItem;
            }

            if (itemToDelete == null) return false;
            ProjectItems.Remove(itemToDelete);
            ProjectItems.Add(projectItemToEdit);
            return true;
        }

    }
}
