using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.PurchaseOrderServices;
using ERPSystem.Services.StockMovementServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.PurchaseOrderView
{
    public class PurchaseOrderListViewModel : BindableBase
    {       
        IPurchaseOrderService _purchaseOrderService;
        IStockMovementService _stockMovementService;
        IMaterialService _materialService;

        private ICurrentUser _currentUser;

        public ICurrentUser CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        private ObservableCollection<MaterialPurchaseOrder> _purchaseOrders;

        public ObservableCollection<MaterialPurchaseOrder> PurchaseOrders
        {
            get { return _purchaseOrders; }
            set { SetProperty(ref _purchaseOrders, value); }
        }

        private Project _project;

        public Project Project
        {
            get { return _project; }
            set { _project = value; }
        }


        private Stock _stock;

        public Stock Stock
        {
            get { return _stock; }
            set { SetProperty(ref _stock, value); }
        }


        public PurchaseOrderListViewModel(IPurchaseOrderService purchaseOrderService, IStockMovementService stockMovementService, IMaterialService materialService,
            ICurrentUser currentUser)
        {
            _purchaseOrderService = purchaseOrderService;
            _stockMovementService = stockMovementService;
            _materialService = materialService;

            CurrentUser = currentUser;

            AcceptedCommand = new RelayCommand<MaterialPurchaseOrder>(OnAccepted);
        }

        public void LoadOrders(Project project)
        {
            Project = project;
            Stock = project.Stock;
            PurchaseOrders = new ObservableCollection<MaterialPurchaseOrder>(_purchaseOrderService.GetAllPurchaseOrdersWithStockId(Stock.Id));
        }

        public RelayCommand<MaterialPurchaseOrder> AcceptedCommand { get; set; }

        private void OnAccepted(MaterialPurchaseOrder order)
        {
            if (order != null)
            {
                order.ReceivedDate = DateTime.Now;
                order.Status = PurchaseOrderStatus.Received;

                _purchaseOrderService.EditPurchaseOrder(order);

                StockMovement movement = new StockMovement();
                movement.StockId = Stock.Id;
                movement.SubtaskId = order.SubtaskId;
                movement.Quantity = (int)order.MaterialAmount;
                movement.CreatedDate = DateTime.Now;
                movement.Description = "Куплен" + order.MaterialName;
                movement.MaterialId = order.MaterialId;

                Material material = _materialService.GetMaterialById(order.MaterialId);
                material.QuantityInStock += (int)order.MaterialAmount;

                _stockMovementService.AddStockMovement(movement);
                _materialService.UpdateMaterial(material);

                LoadOrders(Project);
            }
        }
    }
}
