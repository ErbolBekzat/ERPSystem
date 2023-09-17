using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
using System.Windows;
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

        public MaterialListView(ITaskService taskService, IMaterialService materialService, IStockMovementService stockMovementService, ICurrentUser currentUser)
        {
            InitializeComponent();

            ViewModel = new MaterialListViewModel(taskService, materialService, stockMovementService, currentUser);

            DataContext = ViewModel; ;

            currentBorder = MaterialsBorder;
        }

        Border currentBorder;

        private void MaterialBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (currentBorder == border)
            {
                return;
            }

            SolidColorBrush leftMenuBackgroundColor = new SolidColorBrush((Color)Application.Current.Resources["LeftMenuBackgroundColor"]);
            SolidColorBrush brush = new SolidColorBrush((Color)Application.Current.Resources["BackgroundColor"]);

            currentBorder.Background = leftMenuBackgroundColor;
            currentBorder = border;
            currentBorder.Background = brush;

            if (border != null)
            {
                if (border.Name == "MaterialsBorder")
                {
                    MaterialsGrid.Visibility = System.Windows.Visibility.Visible;
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    PurchaseOrdersGrid.Visibility = Visibility.Collapsed;
                    MaterialOrderRequestListViewGrid.Visibility = Visibility.Collapsed;
                }
                else if (border.Name == "MaterialsMovementsBorder")
                {
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Visible;
                    MaterialsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    PurchaseOrdersGrid.Visibility = Visibility.Collapsed;
                    MaterialOrderRequestListViewGrid.Visibility = Visibility.Collapsed;
                }
                else if (border.Name == "PurchaseOrdersBorder")
                {
                    PurchaseOrdersGrid.Visibility = Visibility.Visible;
                    MaterialsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    MaterialOrderRequestListViewGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MaterialOrderRequestListViewGrid.Visibility = Visibility.Visible;
                    PurchaseOrdersGrid.Visibility = Visibility.Collapsed;
                    MaterialsGrid.Visibility = System.Windows.Visibility.Collapsed;
                    MaterialMovementsGrid.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void MaterialBorder_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;

            if (currentBorder == border)
            {
                return;
            }

            border.Background = new SolidColorBrush(Colors.Gray);
        }

        private void MaterialBorder_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border != null && border != currentBorder)
            {
                // Access the color resource and create a SolidColorBrush
                SolidColorBrush brush = new SolidColorBrush((Color)Application.Current.Resources["LeftMenuBackgroundColor"]);

                // Set the SolidColorBrush as the Background
                border.Background = brush;
            }
        }

    }
}
