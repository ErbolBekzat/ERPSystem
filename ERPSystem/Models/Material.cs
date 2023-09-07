using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class Material : BindableBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string unitOfMeasure;
        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { SetProperty(ref unitOfMeasure, value); }
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { SetProperty(ref unitPrice, value); }
        }

        private int _stockId;
        public int StockId
        {
            get { return _stockId; }
            set { SetProperty(ref _stockId, value); }
        }

        private Stock _stock;
        public Stock Stock
        {
            get { return _stock; }
            set { SetProperty(ref _stock, value); }
        }

        private decimal quantityInStock;
        public decimal QuantityInStock
        {
            get { return quantityInStock; }
            set { SetProperty(ref quantityInStock, value); }
        }

        private ICollection<TaskMaterials> taskMaterials;
        public ICollection<TaskMaterials> TaskMaterials
        {
            get { return taskMaterials; }
            set { SetProperty(ref taskMaterials, value); }

        }

        private ICollection<StockMovement> stockMovements;
        public ICollection<StockMovement> StockMovements
        {
            get { return stockMovements; }
            set { SetProperty(ref  stockMovements, value); }
        }
    }
}
