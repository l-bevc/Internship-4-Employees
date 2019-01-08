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

namespace Employees
{
    public partial class ListOfProjects : Form
    {
        private ProjectItemRepository _projectItemRepository;
        private EmployeeItemRepository _employeeItemRepository;
        internal List<ProjectItem> ListofProjectItems { get; set; }
        internal List<EmployeeItem> ListOfEmployeeItems { get; set; }

        public ListOfProjects(List<ProjectItem> listOfProjectItems, List<EmployeeItem>listOfEmployeeItems,EmployeeItemRepository employeeItemRepository, ProjectItemRepository projectItemRepository)
        {
            InitializeComponent();
            _employeeItemRepository = employeeItemRepository;
            _projectItemRepository = projectItemRepository;
            ListofProjectItems = listOfProjectItems;
            ListOfEmployeeItems = listOfEmployeeItems;
            AddRefreshListView();
        }

        private void AddRefreshListView()
        {
            chkProjects.Items.Clear();           
            ListofProjectItems = _projectItemRepository.GetAllProjectItems();
            foreach (var item in ListofProjectItems)
            {
                chkProjects.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var itemToadd = new AddForm(ListofProjectItems, ListOfEmployeeItems);
            itemToadd.ShowDialog();
            _projectItemRepository.Add(itemToadd.NewProject);
            AddRefreshListView();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject == null) return;
            var detailsProject = new DetailProject(selectedProject);
            detailsProject.ShowDialog();
        }
    }
}
