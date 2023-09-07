using ERPSystem.Services;
using ERPSystem.Services.ProblemServices;
using ERPSystem.Views.MaterialView;
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

namespace ERPSystem.Views.ProblemView
{
    public partial class AddEditProblemView : UserControl
    {
        public AddEditProblemViewModel ViewModel;

        public AddEditProblemView(ISubtaskService subtaskService, IProblemService problemService, IUserService userService)
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Container.Resolve<AddEditProblemViewModel>();

            DataContext = ViewModel;
        }
    }
}
