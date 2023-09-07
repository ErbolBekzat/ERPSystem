using ERPSystem.Data;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using ERPSystem.Services.ProblemServices;
using ERPSystem.Services.StockMovementServices;
using ERPSystem.Services.StockServices;
using ERPSystem.Services.SubtaskRelationshipServices;
using ERPSystem.Services.TaskMaterialServices;
using System.Windows;
using System.Windows.Controls;

namespace ERPSystem.Views.SubtaskView
{
    public partial class SubtaskDetailsView : UserControl
    {
        private SubtaskDetailsViewModel _viewModel;

        public SubtaskDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public SubtaskDetailsView(IBlockService blockService, ITaskService taskService, ISubtaskService subtaskService, ISubtaskRelationshipService subtaskRelationshipService,
            IMaterialService materialService, IProjectService projectService, ITaskMaterialService taskMaterialService, IStockMovementService stockMovementService, 
            IProblemService problemService, IStockService stockService, DataBaseContext context)
        {
            InitializeComponent();

            ViewModel = new SubtaskDetailsViewModel(blockService, taskService, subtaskService, subtaskRelationshipService, materialService, projectService, 
                taskMaterialService, stockMovementService, problemService, stockService, context, Problems, Materials);
            DataContext = ViewModel;
        }

        private void ProblemsButton_Click(object sender, RoutedEventArgs e)
        {
            ProblemsBorder.Visibility = Visibility.Visible;
            MaterialsBorder.Visibility = Visibility.Collapsed;
            btnAddProblem.Visibility = Visibility.Visible;
            ProblemsButton.IsEnabled = false;
            MaterialsButton.IsEnabled = true;
        }

        private void MaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            ProblemsBorder.Visibility = Visibility.Collapsed;
            MaterialsBorder.Visibility = Visibility.Visible;
            btnAddProblem.Visibility = Visibility.Collapsed;
            ProblemsButton.IsEnabled = true;
            MaterialsButton.IsEnabled = false;
        }
    }


}
