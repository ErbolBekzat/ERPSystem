using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace ERPSystem.Views.MaterialView
{
    public partial class MaterialListView : UserControl
    {
        MaterialListViewModel viewModel;

        public MaterialListViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public MaterialListView(ITaskService taskService, IMaterialService materialService, IStockMovementService stockMovementService)
        {
            InitializeComponent();

            ViewModel = new MaterialListViewModel(taskService, materialService, stockMovementService);

            DataContext = ViewModel; ;
        }

        private void MaterialBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (border != null)
            {
                if (border.Name == "MaterialsBorder")
                {
                    MaterialsGrid.Visibility = System.Windows.Visibility.Visible;
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    MaterialsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void MaterialBorder_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border b = sender as Border;
            b.Background = new SolidColorBrush(Colors.Gray);
        }

        private void MaterialBorder_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border b = sender as Border;
            if (b != null)
            {
                // Access the color resource and create a SolidColorBrush
                SolidColorBrush brush = this.Resources["BackgroundBrush"] as SolidColorBrush;

                // Set the SolidColorBrush as the Background
                b.Background = brush;
            }
        }

    }
}
