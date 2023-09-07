using ERPSystem.Views.SubtaskView;
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

namespace ERPSystem.Views.BlockView
{
    public partial class AddEditBlockWindowView : Window
    {
        public AddEditBlockWindowViewModel ViewModel { get; set; }

        public AddEditBlockWindowView()
        {
            InitializeComponent();
            ViewModel = ContainerHelper.Container.Resolve<AddEditBlockWindowViewModel>();
            DataContext = ViewModel;
        }
    }
}
