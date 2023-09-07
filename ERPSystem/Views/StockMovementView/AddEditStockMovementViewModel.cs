using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.StockMovementServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ERPSystem.Views.StockMovementView
{
    public class AddEditStockMovementViewModel : BindableBase
    {
        IStockMovementService _stockMovementService;
        IMaterialService _materialService;

        private ObservableCollection<Material> materials;
        public ObservableCollection<Material> Materials
        {
            get { return materials; }
            set { SetProperty(ref materials, value); }
        }

        private List<StockMovementType> stockMovementTypes;
        public List<StockMovementType> StockMovementTypes
        {
            get { return stockMovementTypes; }
            set { SetProperty(ref stockMovementTypes, value); }
        }

        public AddEditStockMovementViewModel(IStockMovementService stockMovementService, IMaterialService materialService) 
        {
            _stockMovementService = stockMovementService;
            _materialService = materialService;

            StockMovementTypes = Enum.GetValues(typeof(StockMovementType)).Cast<StockMovementType>().ToList();

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private Models.StockMovement stockMovement;

        public Models.StockMovement StockMovement
        {
            get { return stockMovement; }
            set { SetProperty(ref stockMovement, value); }
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; }
        }

        private bool inputError;

        public bool InputError
        {
            get { return inputError; }
            set { SetProperty(ref inputError, value); }
        }

        Window thisWindow;
        Project project;

        public bool SetStockMovement(Models.StockMovement stockMovement, Window window, Project project)
        {
            Materials = new ObservableCollection<Material>(_materialService.GetMaterialsByStockId(stockMovement.StockId));
            this.thisWindow = window;
            this.project = project;

            if (Materials.Count > 0)
            {
                StockMovement = stockMovement;
                StockMovement.MaterialId = Materials[0].Id;
                InputError = false;
                return true;
            }
            else
            {
                MessageBox.Show("Нет материала на складе", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public event Action<Project> SaveRequested = delegate { };

        private void OnSave()
        {
            if (ValidateInputs())
            {
                if (EditMode)
                {
                    _stockMovementService.UpdateStockMovement(StockMovement);
                }
                else
                {
                    StockMovement.CreatedDate = DateTime.Today;
                    StockMovement.Cost = 0;
                    _stockMovementService.AddStockMovement(StockMovement);
                }

                Material material = Materials.FirstOrDefault(m => m.Id == StockMovement.MaterialId);

                if (material != null)
                {
                    if (StockMovement.MovementType == StockMovementType.Purchase)
                    {
                        material.QuantityInStock += StockMovement.Quantity;
                    }
                    else
                    {
                        material.QuantityInStock -= StockMovement.Quantity;
                    }
                }
                

                _materialService.UpdateMaterial(material);

                SaveRequested(project);

                OnCancel();

            }
            else
            {
                InputError = true;
            }
        }



        private void OnCancel()
        {
            thisWindow.Close();
        }

        private bool ValidateInputs()
        {
            if (StockMovement.Description == null || StockMovement.MaterialId == null || StockMovement.MovementType == null || StockMovement.Quantity == null) return false;
            return true;
        }
    }
}
