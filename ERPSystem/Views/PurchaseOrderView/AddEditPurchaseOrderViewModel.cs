using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.MaterialOrderRequestServices;
using ERPSystem.Services.PurchaseOrderServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.PurchaseOrderView
{
    public class AddEditPurchaseOrderViewModel : BindableBase
    {
        Window _window;

        IUserService _userService;
        IPurchaseOrderService _purchaseOrderService;
        IMaterialService _materialService;
        IMaterialOrderRequestService _materialOrderRequestService;

        private MaterialPurchaseOrder _purchaseOrder;

        public MaterialPurchaseOrder PurchaseOrder
        {
            get { return _purchaseOrder; }
            set { SetProperty(ref _purchaseOrder, value); }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private ObservableCollection<Material> _materials;

        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set { SetProperty(ref _materials, value); }
        }

        private Material _selectedMaterial;

        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set { SetProperty(ref _selectedMaterial, value); }
        }

        private MaterialOrderRequest _request;

        public MaterialOrderRequest Request
        {
            get { return _request; }
            set { SetProperty(ref _request, value); }
        }



        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }


        public AddEditPurchaseOrderViewModel(IUserService userService, IPurchaseOrderService purchaseOrderService, IMaterialService materialService, 
            IMaterialOrderRequestService materialOrderRequestService)
        {
            _userService = userService;
            _purchaseOrderService = purchaseOrderService;
            _materialService = materialService;
            _materialOrderRequestService = materialOrderRequestService;

            AddOrderCommand = new RelayCommand(OnAddOrder);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public void SetOrder(MaterialPurchaseOrder purchaseOrder, MaterialOrderRequest request, Window window)
        {
            _window = window;
            PurchaseOrder = purchaseOrder;
            Request = request;
            Users = new ObservableCollection<User>(_userService.GetAllUsers());
            Materials = new ObservableCollection<Material>(_materialService.GetMaterialsByStockId(purchaseOrder.StockId));
        }

        public RelayCommand AddOrderCommand { get; set; }

        private void OnAddOrder()
        {
            if (PurchaseOrder != null)
            {
                if (SelectedMaterial != null ||
                    PurchaseOrder.OrderDate != DateTime.MinValue ||
                    PurchaseOrder.ExpectedDeliveryDate != DateTime.MinValue ||
                    PurchaseOrder.MaterialAmount != null)
                {
                    _purchaseOrderService.CreatePurchaseOrder(PurchaseOrder);
                    
                    Request.OrderId = PurchaseOrder.Id;
                    Request.OrderedDate = DateTime.Now;

                    _materialOrderRequestService.Edit(Request);

                    _window.Close();
                    Done();
                }
            }
            else
            {
                // Handle the case where PurchaseOrder is null
            }
        }

        public RelayCommand CancelCommand { get; set; }

        public event Action Done = delegate { };

        private void OnCancel()
        {
            _window.Close();
        }

    }
}
