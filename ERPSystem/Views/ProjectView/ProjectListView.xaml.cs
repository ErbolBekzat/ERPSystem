using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERPSystem.Views.ProjectView
{
    public partial class ProjectListView : UserControl
    {
        public ProjectListViewModel ViewModel { get; set; }

        public ProjectListView(IProjectService projectService, ITaskService taskService)
        {
            InitializeComponent();

            ViewModel = new ProjectListViewModel(projectService, taskService, ProjectsGrid);

            DataContext = ViewModel;

            Loaded += ProjectListView_Loaded;
        }

        private void ProjectListView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.DisplayProjects();
        }
    }
}
