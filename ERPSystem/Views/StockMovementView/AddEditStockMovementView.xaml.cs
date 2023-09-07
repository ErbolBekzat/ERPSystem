using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
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
using System.Windows.Shapes;

namespace ERPSystem.Views.StockMovementView
{
    public partial class AddEditStockMovementView : Window
    {
        public AddEditStockMovementViewModel ViewModel;

        public AddEditStockMovementView(IStockMovementService stockMovementService, IMaterialService materialService)
        {
            InitializeComponent();

            ViewModel = new AddEditStockMovementViewModel(stockMovementService, materialService);

            DataContext = ViewModel;
        }
    }
}
