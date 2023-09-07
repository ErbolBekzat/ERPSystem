using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using System;
using System.Windows.Controls;

namespace ERPSystem.Views.TaskView
{    
    public partial class AddEditTaskView : UserControl
    {
        private AddEditTaskViewModel _viewModel;

        public AddEditTaskViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public AddEditTaskView(IUserService userService, ITaskService taskService, IMaterialService materialService, 
            IProjectService projectService, IBlockService blockService)
        {
            InitializeComponent();

            ViewModel = new AddEditTaskViewModel(userService, taskService, materialService, projectService, blockService);

            DataContext = ViewModel;
        }
    }
}
