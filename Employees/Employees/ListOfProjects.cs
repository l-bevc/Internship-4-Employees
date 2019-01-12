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
        public bool Back { get; set; }
        internal List<ProjectItem> ListofProjectItems { get; set; }
        internal List<EmployeeItem> ListOfEmployeeItems { get; set; }

        public ListOfProjects(List<ProjectItem> listOfProjectItems, List<EmployeeItem>listOfEmployeeItems)
        {
            InitializeComponent();
            Back = false;
            ListofProjectItems = listOfProjectItems;
            ListOfEmployeeItems = listOfEmployeeItems;
            AddRefreshListView();
        }

        private void AddRefreshListView()
        {
            chkProjects.Items.Clear();           
            ListofProjectItems = StatusOfEmployeesAndProjects.ProjectItemRepository.GetAllProjectItems();
            foreach (var item in ListofProjectItems)
            {
                chkProjects.Items.Add(item);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var itemToAdd = new AddForm(ListofProjectItems, ListOfEmployeeItems);
            itemToAdd.ShowDialog();
            if (!itemToAdd.Quit)
            {
                StatusOfEmployeesAndProjects.ProjectItemRepository.Add(itemToAdd.NewProject);
                AddRefreshListView();
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject == null) return;
            var detailsProject = new DetailProject(selectedProject);
            detailsProject.ShowDialog();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject == null) return;
            var dialogResult = MessageBox.Show("Jesi li siguran", "Oprez", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                StatusOfEmployeesAndProjects.ProjectItemRepository.Delete(selectedProject.ProjectName);
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

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var selectedProject = chkProjects.SelectedItem as ProjectItem;
            if (selectedProject== null) return;
            var editTodo = new EditProject(selectedProject, ListofProjectItems, ListOfEmployeeItems);
            editTodo.ShowDialog();
            if (!editTodo.Quit)
            {
                StatusOfEmployeesAndProjects.ProjectItemRepository.Edit(editTodo.ProjectItem);
                AddRefreshListView();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Back = true;
            AddRefreshListView();
            Close();
        }
    }
}
