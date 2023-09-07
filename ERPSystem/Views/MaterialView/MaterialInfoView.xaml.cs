using ERPSystem.Services;
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

namespace ERPSystem.Views.MaterialView
{
    public partial class MaterialInfoView : Window
    {
        public MaterialInfoViewModel ViewModel { get; set; }

        public MaterialInfoView(IMaterialService materialService)
        {
            InitializeComponent();

            ViewModel = new MaterialInfoViewModel(materialService, this);
            DataContext = ViewModel;
        }
    }
}
