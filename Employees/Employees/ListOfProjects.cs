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

        public ListOfProjects(List<ProjectItem> listOfProjectItems, List<EmployeeItem>listOfEmployeeItems)
        {
            InitializeComponent();
            _projectItemRepository=new ProjectItemRepository();
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
            var itemToadd = new AddForm();
            itemToadd.ShowDialog();
            
            AddRefreshListView();
        }
    }
}
