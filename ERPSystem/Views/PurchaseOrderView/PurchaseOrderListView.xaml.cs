using ERPSystem.Views.UserView;
using Microsoft.Practices.Unity;
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

namespace ERPSystem.Views.PurchaseOrderView
{
    public partial class PurchaseOrderListView : UserControl
    {
        public PurchaseOrderListViewModel ViewModel { get; set; }

        public PurchaseOrderListView()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Container.Resolve<PurchaseOrderListViewModel>();

            DataContext = ViewModel;
        }
    }
}
