using ERPSystem.Data;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
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

namespace ERPSystem.Views.TaskView
{
    public partial class TaskDetailsView : UserControl
    {
        private TaskDetailsViewModel _viewModel;

        public TaskDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public TaskDetailsView(ITaskService taskService, ISubtaskService subtaskService, IMaterialService materialService, IProjectService projectService, 
            IRoleService roleService, ICurrentUser currentUser, IBlockService blockService)
        {
            InitializeComponent();

            ViewModel = new TaskDetailsViewModel(taskService, subtaskService, materialService, projectService, roleService, currentUser, 
                blockService, TasksOrProblems, Materials);
            DataContext = ViewModel;
        }

        private void TasksProblemsButton_Click(object sender, RoutedEventArgs e)
        {
            TasksOrProblemsBorder.Visibility = Visibility.Visible;
            MaterialsBorder.Visibility = Visibility.Collapsed;
            TasksProblemsButton.IsEnabled = false;
            MaterialsButton.IsEnabled = true;
        }

        private void MaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            TasksOrProblemsBorder.Visibility = Visibility.Collapsed;
            MaterialsBorder.Visibility = Visibility.Visible;
            TasksProblemsButton.IsEnabled = true;
            MaterialsButton.IsEnabled = false;
        }
    }
}
