using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.MaterialView
{
    public class MaterialInfoViewModel : BindableBase
    {
		IMaterialService _materialService;

		private Material _material;

		public Material Material
		{
			get { return _material; }
			set { SetProperty(ref _material, value); }
		}

		Window currentWindow;

		public MaterialInfoViewModel(IMaterialService materialService, Window window)
		{
			_materialService = materialService;
			currentWindow = window;

			EditMaterialCommand = new RelayCommand(OnEditMaterial);
			DeleteMaterialCommand = new RelayCommand(OnDeleteMaterial);
			CloseWindowCommand = new RelayCommand(OnCloseWindow);
		}

		private int taskMaterials;

		public int TaskMaterials
		{
			get { return taskMaterials; }
			set { SetProperty(ref taskMaterials, value); }
		}

		private int stockMovements;

		public int StockMovements
		{
			get { return stockMovements; }
			set { SetProperty(ref stockMovements, value); }
		}



		public void SetMaterial(Material material)
		{
			Material = material;
			TaskMaterials = Material.TaskMaterials.Count;
			StockMovements = Material.StockMovements.Count;
		}

		public RelayCommand EditMaterialCommand { get; set; }
		public RelayCommand DeleteMaterialCommand { get; private set; }
		public RelayCommand CloseWindowCommand { get; set; }

		public event Action<Material> EditMaterialRequested = delegate { };
		public event Action DeleteMaterialRequested = delegate { };

		private void OnEditMaterial()
		{
            currentWindow.Close();
            EditMaterialRequested(Material);
		}

		private void OnDeleteMaterial()
		{
			if (Material.TaskMaterials.Count > 0 || Material.StockMovements.Count > 0)
			{
				MessageBox.Show("You can't delete Material with Task Material or Stock Movement", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
                _materialService.DeleteMaterial(Material);
            }

			DeleteMaterialRequested?.Invoke();
            currentWindow.Close();
        }

		private void OnCloseWindow()
		{
            currentWindow.Close();
        }
	}
}
