using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class Stock : BindableBase
    {
        private int _id;
        private string _name;
        private string _description;
        private int _projectId;
        private Project _project;
        private int _stockManagerId;
        private User _stockManager;
        private ICollection<Material> _materials;
        private ICollection<StockMovement> _stockMovements;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }
        public int ProjectId
        {
            get { return _projectId; }
            set
            {
                SetProperty(ref _projectId, value);
            }
        }
        public Project Project
        {
            get { return _project; }
            set
            {
                SetProperty(ref _project, value);
            }
        }
        public int StockManagerId
        {
            get { return _stockManagerId; }
            set 
            { 
                SetProperty(ref _stockManagerId, value); 
            }
        }
        public User StockManager
        {
            get { return _stockManager; }
            set 
            { 
                SetProperty(ref _stockManager, value); 
            }
        }
        public ICollection<Material> Materials
        {
            get { return _materials; }
            set { SetProperty(ref _materials, value); }
        }
        public ICollection<StockMovement> StockMovements
        {
            get { return _stockMovements; }
            set { SetProperty(ref _stockMovements, value); }
        }
    }
}
