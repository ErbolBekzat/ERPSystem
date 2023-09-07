using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
using ERPSystem.Views.StockMovementView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ERPSystem.Views.MaterialView
{
    public class MaterialListViewModel : BindableBase
    {
        ITaskService _taskService;
        IMaterialService _materialService;
        IStockMovementService _stockMovementService;

        private ObservableCollection<Material> _materials;
        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set { SetProperty(ref _materials, value); }
        }

        private ObservableCollection<Models.StockMovement> stockMovements;
        public ObservableCollection<Models.StockMovement> StockMovements
        {
            get { return stockMovements; }
            set { SetProperty(ref stockMovements, value); }
        }

        readonly Brush background;
        readonly Brush primaryTaskBG;
        readonly Brush secondaryTaskBG;
        readonly Brush primaryRectBG;
        readonly Brush secondaryRectBG;
        readonly Brush lineColor;
        readonly Brush textColor;


        public MaterialListViewModel(ITaskService taskService, IMaterialService materialService, IStockMovementService stockMovementService)
        {
            background = new SolidColorBrush((Color)Application.Current.Resources["BackgroundColor"]);
            primaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryTaskBGColor"]);
            secondaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryTaskBGColor"]);
            primaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryRectBGColor"]);
            secondaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryRectBGColor"]);
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _taskService = taskService;
            _materialService = materialService;
            _stockMovementService = stockMovementService;



            AddMaterialCommand = new RelayCommand(OnAddMaterial);
            AddStockMovementCommand = new RelayCommand(OnAddStockMovement);
            MaterialInfoCommand = new RelayCommand<Material>(OnMaterialInfo);
            
        }

        private Project _project;

        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }


        public void LoadMaterials(Project project)
        {
            Project = project;
            Materials = new ObservableCollection<Material>(_materialService.GetMaterialsByStockId(project.Stock.Id));

            StockMovements = new ObservableCollection<Models.StockMovement>(_stockMovementService.GetStockMovementsByStockId(project.Stock.Id).OrderByDescending(sm => sm.CreatedDate));

            //DisplayHeader();
            //DisplayMaterials();
        }

        public void LoadMaterials()
        {
            Materials = new ObservableCollection<Material>(_materialService.GetMaterialsByStockId(Project.Stock.Id));

            StockMovements = new ObservableCollection<Models.StockMovement>(_stockMovementService.GetStockMovementsByStockId(Project.Stock.Id).OrderByDescending(sm => sm.CreatedDate));

            //DisplayHeader();
            //DisplayMaterials();
        }

        public RelayCommand AddMaterialCommand { get; private set; }

        public event Action<Material> AddMaterialRequested = delegate { };

        AddEditMaterialView addEditMaterialViewWindow;

        private void OnAddMaterial()
        {
            addEditMaterialViewWindow = new AddEditMaterialView(_taskService, _materialService);
            addEditMaterialViewWindow.ViewModel.SaveRequested += LoadMaterials;
            addEditMaterialViewWindow.ViewModel.EditMode = false;
            addEditMaterialViewWindow.ViewModel.SetMaterial(new Material
            {
                StockId = Project.Stock.Id
            }, addEditMaterialViewWindow, Project);

            addEditMaterialViewWindow.ShowDialog();
        }

        public RelayCommand AddStockMovementCommand { get; private set; }

        AddEditStockMovementView addEditStockMovementWindow;

        private void OnAddStockMovement()
        {
            addEditStockMovementWindow = new AddEditStockMovementView(_stockMovementService, _materialService);
            addEditStockMovementWindow.ViewModel.SaveRequested += LoadMaterials;
            addEditStockMovementWindow.ViewModel.EditMode = false;
            bool canShowDialog = addEditStockMovementWindow.ViewModel.SetStockMovement(new Models.StockMovement
            {
                StockId = Project.Stock.Id
            }, addEditStockMovementWindow, Project);

            if (canShowDialog)
            {
                addEditStockMovementWindow.ShowDialog();
            }
            
        }

        public RelayCommand<Material> MaterialInfoCommand { get; private set; }

        private void OnMaterialInfo(Material material)
        {
            MaterialInfoView materialInfoViewWindow = new MaterialInfoView(_materialService);
            materialInfoViewWindow.ViewModel.SetMaterial(material);
            materialInfoViewWindow.ViewModel.EditMaterialRequested += OnEditMaterial;
            materialInfoViewWindow.ViewModel.DeleteMaterialRequested += LoadMaterials;
            materialInfoViewWindow.ShowDialog();
        }

        private void OnEditMaterial(Material material)
        {
            addEditMaterialViewWindow = new AddEditMaterialView(_taskService, _materialService);
            addEditMaterialViewWindow.ViewModel.SaveRequested += LoadMaterials;
            addEditMaterialViewWindow.ViewModel.EditMode = true;
            addEditMaterialViewWindow.ViewModel.SetMaterial(material, addEditMaterialViewWindow, Project);
            addEditMaterialViewWindow.ShowDialog();
        }
    }
}
