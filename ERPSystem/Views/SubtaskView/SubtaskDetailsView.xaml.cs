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
using System.Windows.Media;

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

        Border currentButtonBorder;
        Border currentContentBorder;

        public SubtaskDetailsView(IBlockService blockService, ITaskService taskService, ISubtaskService subtaskService, ISubtaskRelationshipService subtaskRelationshipService,
            IMaterialService materialService, IProjectService projectService, ITaskMaterialService taskMaterialService, IStockMovementService stockMovementService, 
            IProblemService problemService, IStockService stockService, DataBaseContext context)
        {
            InitializeComponent();

            ViewModel = new SubtaskDetailsViewModel(blockService, taskService, subtaskService, subtaskRelationshipService, materialService, projectService, 
                taskMaterialService, stockMovementService, problemService, stockService, context, Problems, Materials);
            DataContext = ViewModel;

            currentButtonBorder = ProblemsBorder;
            currentContentBorder = ProblemsBorderContent;
        }

        private void BorderButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (currentButtonBorder == border || border == null)
            {
                return;
            }

            SolidColorBrush leftMenuBackgroundColor = new SolidColorBrush((Color)Application.Current.Resources["LeftMenuBackgroundColor"]);
            SolidColorBrush brush = new SolidColorBrush((Color)Application.Current.Resources["BackgroundColor"]);


            Border contentBorder = (Border)this.FindName(border.Name + "Content");
            contentBorder.Visibility = Visibility.Visible;
            currentContentBorder.Visibility = Visibility.Collapsed;
            currentContentBorder = contentBorder;


            currentButtonBorder.Background = leftMenuBackgroundColor;
            currentButtonBorder = border;
            currentButtonBorder.Background = brush;
        }

        private void BorderButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;

            if (currentButtonBorder == border)
            {
                return;
            }

            border.Background = new SolidColorBrush(Colors.Gray);
        }

        private void BorderButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border != null && border != currentButtonBorder)
            {
                SolidColorBrush brush = new SolidColorBrush((Color)Application.Current.Resources["LeftMenuBackgroundColor"]);

                border.Background = brush;
            }
        }
    }


}
