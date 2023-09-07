using ERPSystem.Views.ProblemView;
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

namespace ERPSystem.Views.WorkerView
{
    public partial class WorkerListView : UserControl
    {
        public WorkerListViewModel ViewModel { get; set; }

        public WorkerListView()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Container.Resolve<WorkerListViewModel>();

            DataContext = ViewModel;
        }
    }
}
