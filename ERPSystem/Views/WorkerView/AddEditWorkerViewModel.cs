using ERPSystem.Models;
using ERPSystem.Services.WorkerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.WorkerView
{
    public class AddEditWorkerViewModel : BindableBase
    {
        IWorkerService _workerService;

        Window _window;

        private Worker _currentWorker;

        public Worker CurrentWorker
        {
            get { return _currentWorker; }
            set { SetProperty(ref _currentWorker, value); }
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }


        public AddEditWorkerViewModel(IWorkerService workerService)
        {
            _workerService = workerService;

            AddNewWorkerCommand = new RelayCommand(OnAddNewWorker);
            SaveWorkerCommand = new RelayCommand(OnSaveWorker);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public void SetWorker(Worker worker, Window window)
        {
            _window = window;
            CurrentWorker = worker;
        }

        public RelayCommand AddNewWorkerCommand { get; set; }
        public event Action Done = delegate { };

        private void OnAddNewWorker()
        {
            if (_workerService != null && Validate())
            {
                _workerService.CreateWorker(CurrentWorker);
                Done();
            }
        }

        public RelayCommand SaveWorkerCommand { get; set; }

        private void OnSaveWorker()
        {
            if (_workerService != null && Validate())
            {
                _workerService.UpdateWorker(CurrentWorker);
                Done();
            }
        }

        public RelayCommand CancelCommand { get; set; }

        private void OnCancel()
        {
            Done();
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(CurrentWorker.FirstName) ||
                string.IsNullOrWhiteSpace(CurrentWorker.LastName) ||
                string.IsNullOrWhiteSpace(CurrentWorker.Email) ||
                string.IsNullOrWhiteSpace(CurrentWorker.Phone) ||
                string.IsNullOrWhiteSpace(CurrentWorker.Address) ||
                CurrentWorker.Salary != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
