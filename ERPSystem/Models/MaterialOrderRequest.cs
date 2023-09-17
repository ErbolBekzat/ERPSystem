using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class MaterialOrderRequest : BindableBase
    {
        private int _id;
        private int _stockId;
        private int _materialId;
        private int _subtaskId;
        private string _materialName;
        private float _materialAmount;
        private int _requestedById;
        private string _requestedByName;
        private DateTime _requestedDate;
        private DateTime? _orderedDate;
        private int? _orderId;
        private MaterialOrderRequestStatus _status;

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

        public int SubtaskId
        {
            get { return _subtaskId; }
            set { SetProperty(ref _subtaskId, value); }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { SetProperty(ref _materialName, value); }
        }

        public float MaterialAmount
        {
            get { return _materialAmount; }
            set { SetProperty(ref _materialAmount, value); }
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

        public DateTime RequestedDate
        {
            get { return _requestedDate; }
            set { SetProperty(ref _requestedDate, value); }
        }

        public DateTime? OrderedDate
        {
            get { return _orderedDate; }
            set { SetProperty(ref _orderedDate, value); }
        }

        public int? OrderId 
        { 
            get { return _orderId; } 
            set { SetProperty(ref _orderId, value); }
        }

        public MaterialOrderRequestStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
    }

    public enum MaterialOrderRequestStatus
    {
        Pending,
        Ordered
    }
}
