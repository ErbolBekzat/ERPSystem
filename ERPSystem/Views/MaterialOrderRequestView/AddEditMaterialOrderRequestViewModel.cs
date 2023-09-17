using ERPSystem.Models;
using ERPSystem.Services.MaterialOrderRequestServices;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.MaterialOrderRequestView
{
    public class AddEditMaterialOrderRequestViewModel : BindableBase
    {
        Window _window;


        IMaterialOrderRequestService _materialOrderRequestService;

        ICurrentUser _currentUser;

        private Material _seletedMaterial;

        public Material SelectedMaterial
        {
            get { return _seletedMaterial; }
            set { SetProperty(ref _seletedMaterial, value); }
        }

        private ObservableCollection<Material> _materials;

        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set { SetProperty(ref _materials, value); }
        }

        private MaterialOrderRequest _request;

        public MaterialOrderRequest Request
        {
            get { return _request; }
            set { _request = value; }
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; }
        }


        public AddEditMaterialOrderRequestViewModel(IMaterialOrderRequestService materialOrderRequestService, ICurrentUser currentUser)
        {
            _materialOrderRequestService = materialOrderRequestService;
            _currentUser = currentUser;

            NewMaterialCommand = new RelayCommand(OnNewMaterial);
            CancelCommand = new RelayCommand(OnCacel);
        }

        public void SetRequest(MaterialOrderRequest request, ObservableCollection<Material> materials, Window window)
        {
            _window = window;
            Materials = materials;

            Request = request;
        }

        public event Action Done = delegate { };

        public RelayCommand NewMaterialCommand { get; set; }
        private void OnNewMaterial()
        {
            if (SelectedMaterial == null)
            {
                return;
            }

            if (Request != null && Request.MaterialAmount != null)
            {
                Request.MaterialId = SelectedMaterial.Id;
                Request.MaterialName = SelectedMaterial.Name;
                Request.RequestedById = _currentUser.Id;
                Request.RequestedByName = _currentUser.FirstName + " " + _currentUser.LastName;
                Request.RequestedDate = DateTime.Now;
                Request.Status = MaterialOrderRequestStatus.Pending;

                _materialOrderRequestService.Create(Request);

                Done();
                _window.Close();
            }
        }

        public RelayCommand CancelCommand { get; set; }
        private void OnCacel()
        {
            _window.Close();
        }
    }
}
