using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class PurchaseOrder : BindableBase
    {
        private int _id;
        private int _stockId;
        private int _materialId;
        private string _materialName;
        private string _description;
        private DateTime _orderDate;
        private DateTime? _expectedDeliveryDate;
        private DateTime? _receivedDate;
        private int _requestedById;
        private string _requestedByName;
        private int _authorizedById;
        private string _authorizedByName;
        private PurchaseOrderStatus _status;
        private float _price;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public int StockId
        {
            get { return _stockId; }
            set { SetProperty(ref _stockId, value); }
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { SetProperty(ref _materialId, value); }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { SetProperty(ref _materialName, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { SetProperty(ref _orderDate, value); }
        }

        public DateTime? ExpectedDeliveryDate
        {
            get { return _expectedDeliveryDate; }
            set { SetProperty(ref _expectedDeliveryDate, value); }
        }

        public DateTime? ReceivedDate
        {
            get { return _receivedDate; }
            set { SetProperty(ref _receivedDate, value); }
        }

        public int RequestedById
        {
            get { return _requestedById; }
            set { SetProperty(ref _requestedById, value); }
        }

        public string RequestedByName
        {
            get { return _requestedByName; }
            set { SetProperty(ref _requestedByName, value); }
        }

        public int AuthorizedById
        {
            get { return _authorizedById; }
            set { SetProperty(ref _authorizedById, value); }
        }

        public string AuthorizedByName
        {
            get { return _authorizedByName; }
            set { SetProperty(ref _authorizedByName, value); }
        }

        public PurchaseOrderStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public float Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
    }

    public enum PurchaseOrderStatus
    {
        Pending,
        Received
    }
}
