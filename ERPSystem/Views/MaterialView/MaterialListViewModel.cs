using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
using ERPSystem.Views.MaterialOrderRequestView;
using ERPSystem.Views.PurchaseOrderView;
using ERPSystem.Views.StockMovementView;
using Microsoft.Practices.Unity;
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

        private ICurrentUser _currentUser;

        public ICurrentUser CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        private PurchaseOrderListView _purchaseOrderListView;

        public PurchaseOrderListView PurchaseOrderListView
        {
            get { return _purchaseOrderListView; }
            set { SetProperty(ref _purchaseOrderListView, value); }
        }

        private MaterialOrderRequestListView _materialOrderRequestListView;

        public MaterialOrderRequestListView MaterialOrderRequestListView
        {
            get { return _materialOrderRequestListView; }
            set { SetProperty(ref _materialOrderRequestListView, value); }
        }


        readonly Brush background;
        readonly Brush primaryTaskBG;
        readonly Brush secondaryTaskBG;
        readonly Brush primaryRectBG;
        readonly Brush secondaryRectBG;
        readonly Brush lineColor;
        readonly Brush textColor;


        public MaterialListViewModel(ITaskService taskService, IMaterialService materialService, IStockMovementService stockMovementService, ICurrentUser currentUser)
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
            
            CurrentUser = currentUser;


            PurchaseOrderListView = ContainerHelper.Container.Resolve<PurchaseOrderListView>();
            MaterialOrderRequestListView = ContainerHelper.Container.Resolve<MaterialOrderRequestListView>();

            AddMaterialCommand = new RelayCommand(OnAddMaterial);
            AddStockMovementCommand = new RelayCommand(OnAddStockMovement);
            AddPurchaseOrderCommand = new RelayCommand<MaterialOrderRequest>(OnAddPurchaseOrder);
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
            PurchaseOrderListView.ViewModel.LoadOrders(Project);
            MaterialOrderRequestListView.ViewModel.LoadRequests(Project, AddPurchaseOrderCommand);
        }

        public void LoadMaterials()
        {
            Materials = new ObservableCollection<Material>(_materialService.GetMaterialsByStockId(Project.Stock.Id));

            StockMovements = new ObservableCollection<Models.StockMovement>(_stockMovementService.GetStockMovementsByStockId(Project.Stock.Id).OrderByDescending(sm => sm.CreatedDate));
            PurchaseOrderListView.ViewModel.LoadOrders(Project);
            MaterialOrderRequestListView.ViewModel.LoadRequests(Project, AddPurchaseOrderCommand);
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

        public RelayCommand<MaterialOrderRequest> AddPurchaseOrderCommand { get; private set; }

        AddEditPurchaseOrderView addEditPurchaseOrderWindow;

        private void OnAddPurchaseOrder(MaterialOrderRequest request)
        {
            if (request.OrderId != null)
            {
                MessageBox.Show("По этому запросу уже сделан заказ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            Material material = _materialService.GetMaterialById(request.MaterialId);

            MaterialPurchaseOrder purchaseOrder = new MaterialPurchaseOrder();
            purchaseOrder.StockId = Project.Stock.Id;
            purchaseOrder.SubtaskId = request.SubtaskId;
            purchaseOrder.RequestId = request.Id;
            purchaseOrder.MaterialId = request.MaterialId;
            purchaseOrder.MaterialName = request.MaterialName;
            purchaseOrder.MaterialAmount = request.MaterialAmount;
            purchaseOrder.AuthorizedById = CurrentUser.Id;
            purchaseOrder.AuthorizedByName = CurrentUser.FirstName + " " + CurrentUser.LastName;
            purchaseOrder.OrderDate = DateTime.Now;
            purchaseOrder.RequestedById = request.RequestedById;
            purchaseOrder.RequestedByName = request.RequestedByName;
            purchaseOrder.AuthorizedByName = CurrentUser?.FirstName + " " + CurrentUser?.LastName;
            purchaseOrder.AuthorizedById = CurrentUser.Id;
            purchaseOrder.Price = (float)material.UnitPrice * request.MaterialAmount;

            addEditPurchaseOrderWindow = new AddEditPurchaseOrderView();
            addEditPurchaseOrderWindow.ViewModel.SetOrder(purchaseOrder, request, addEditPurchaseOrderWindow);
            addEditPurchaseOrderWindow.ViewModel.EditMode = false;
            addEditPurchaseOrderWindow.ViewModel.Done += LoadMaterials;

            addEditPurchaseOrderWindow.ShowDialog();
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
