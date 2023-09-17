using ERPSystem.Models;
using ERPSystem.Services.MaterialOrderRequestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.MaterialOrderRequestView
{
    public class MaterialOrderRequestListViewModel : BindableBase
    {
        IMaterialOrderRequestService _materialOrderRequestService;

        private ObservableCollection<MaterialOrderRequest> _requests;

        public ObservableCollection<MaterialOrderRequest> Requests
        {
            get { return _requests; }
            set { SetProperty(ref _requests, value); }
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

        private bool _canMakeOrder;

        public bool CanMakeOrder
        {
            get { return _canMakeOrder; }
            set { SetProperty(ref _canMakeOrder, value); }
        }


        public MaterialOrderRequestListViewModel(IMaterialOrderRequestService materialOrderRequestService)
        {
            _materialOrderRequestService = materialOrderRequestService;
        }

        public void LoadRequests(Project project)
        {
            Project = project;
            Stock = Project.Stock;

            Requests = new ObservableCollection<MaterialOrderRequest>(_materialOrderRequestService.GetAllByStockId(Stock.Id));
            CanMakeOrder = false;
        }

        public void LoadRequests(Project project, RelayCommand<MaterialOrderRequest> orderCommand)
        {
            Project = project;
            Stock = Project.Stock;

            Requests = new ObservableCollection<MaterialOrderRequest>(_materialOrderRequestService.GetAllByStockId(Stock.Id));
            OrderCommand = orderCommand;
            CanMakeOrder = true;
        }

        public RelayCommand<MaterialOrderRequest> OrderCommand { get; set; }
    }
}
