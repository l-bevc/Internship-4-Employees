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
        internal List<ProjectItem> ListofProjectItems { get; set; }
        internal List<EmployeeItem> ListOfEmployeeItems { get; set; }

        public ListOfProjects(List<ProjectItem> listOfProjectItems, List<EmployeeItem>listOfEmployeeItems,ProjectItemRepository projectItemRepository)
        {
            InitializeComponent();
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
            var itemToAdd = new AddForm(ListofProjectItems, ListOfEmployeeItems);
            itemToAdd.ShowDialog();
            if (!itemToAdd.Quit)
            {
                _projectItemRepository.Add(itemToAdd.NewProject);
                AddRefreshListView();
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject == null) return;
            var detailsProject = new DetailProject(selectedProject);
            detailsProject.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject == null) return;
            var dialogResult = MessageBox.Show("Jesi li siguran", "Oprez", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _projectItemRepository.Delete(selectedProject.ProjectName);
                foreach (var employee in selectedProject.ListOfEmployees)
                {
                    employee.ProjectsOfEmployee.Remove(selectedProject.ProjectName);
                    employee.CalculatingHours(-employee.WorkingHours);
                }
                ListofProjectItems.Remove(selectedProject);

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            AddRefreshListView();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject== null) return;
            var editTodo = new EditProject(selectedProject, ListofProjectItems, ListOfEmployeeItems);
            editTodo.ShowDialog();
            if (!editTodo.Quit)
            {
                _projectItemRepository.Edit(editTodo.ProjectItem);
                AddRefreshListView();
            }
        }
    }
}
