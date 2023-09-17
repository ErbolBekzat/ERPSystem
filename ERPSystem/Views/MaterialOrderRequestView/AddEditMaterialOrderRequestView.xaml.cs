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
using System.Windows.Shapes;

namespace ERPSystem.Views.MaterialOrderRequestView
{
    public partial class AddEditMaterialOrderRequestView : Window
    {
        public AddEditMaterialOrderRequestViewModel ViewModel { get; set; }

        public AddEditMaterialOrderRequestView()
        {
            InitializeComponent();
        
            ViewModel = ContainerHelper.Container.Resolve<AddEditMaterialOrderRequestViewModel>();

            DataContext = ViewModel;
        }
    }
}
