using ERPSystem.Models;
using ERPSystem.Services.BlockWorkforceSummaryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.BlockWorkforceSummaryView
{
    public class AddEditBlockWorkforceSummaryViewModel : BindableBase
    {
        IBlockWorkforceSummaryService _blockWorkforceSummaryService;

        Window _window;

        private BlockWorkforceSummary _currentBWS;

        public BlockWorkforceSummary CurrentBWS
        {
            get { return _currentBWS; }
            set { SetProperty(ref _currentBWS, value); }
        }


        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }


        public AddEditBlockWorkforceSummaryViewModel(IBlockWorkforceSummaryService blockWorkforceSummaryService)
        {
            _blockWorkforceSummaryService = blockWorkforceSummaryService;

            CancelCommand = new RelayCommand(OnCancel);
            CreateBWSCommand = new RelayCommand(OnCreateBWS);
        }

        public void SetBWS(BlockWorkforceSummary blockWorkforceSummary, Window window)
        {
            _window = window;

            CurrentBWS = blockWorkforceSummary;
        }

        public RelayCommand CancelCommand { get; set; }
        public event Action Done = delegate { };

        private void OnCancel()
        {
            Done();
            _window.Close();
        }

        public RelayCommand CreateBWSCommand { get; set; }

        
        private void OnCreateBWS()
        {
            _blockWorkforceSummaryService.AddBlockWorkforceSummary(CurrentBWS);
            OnCancel();
        }
    }
}
