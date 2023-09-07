using ERPSystem;
using ERPSystem.Models;
using System;

namespace ERPSystem.Models
{
    public class StockMovement : BindableBase
    {
        private string? description;
        private int _movementId;
        private int _materialId;
        private Material _material;
        private int? _subtaskId;
        private ERPSystem.Models.Subtask? _subtask;
        private StockMovementType _movementType;
        private int _quantity;
        private decimal _cost;
        private int _stockId;
        private Stock _stock;
        private DateTime _createdDate;
        private User? _createdUser;

        public string? Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public int MovementId
        {
            get { return _movementId; }
            set { SetProperty(ref _movementId, value); }
        }

        public int MaterialId
        {
            get { return _materialId; }
            set { SetProperty(ref _materialId, value); }
        }

        public Material Material
        {
            get { return _material; }
            set { SetProperty(ref _material, value); }
        }

        public int? SubtaskId
        {
            get { return _subtaskId; }
            set { SetProperty(ref _subtaskId, value); }
        }

        public ERPSystem.Models.Subtask? Subtask
        {
            get { return _subtask; }
            set { SetProperty(ref _subtask, value); }
        }

        public StockMovementType MovementType
        {
            get { return _movementType; }
            set { SetProperty(ref _movementType, value); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        public decimal Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }

        public int StockId
        {
            get { return _stockId; }
            set { SetProperty(ref _stockId, value); }
        }

        public Stock Stock
        {
            get { return _stock; }
            set { SetProperty(ref _stock, value); }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { SetProperty(ref _createdDate, value); }
        }

        public User? CreatedUser
        {
            get { return _createdUser; }
            set { SetProperty(ref _createdUser, value); }
        }
    }

    public enum StockMovementType
    {
        Purchase,
        Subtraction
    }
}
