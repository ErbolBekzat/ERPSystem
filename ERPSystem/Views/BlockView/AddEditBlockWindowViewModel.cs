using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.BlockView
{
    public class AddEditBlockWindowViewModel : BindableBase
    {
        IBlockService _blockService;
        IUserService _userService;
        
        Window _window;

        private Block _currentBlock;

        public Block CurrentBlock
        {
            get { return _currentBlock; }
            set { SetProperty(ref _currentBlock, value); }
        }

        public AddEditBlockWindowViewModel(IBlockService blockService, IUserService userService)
        {
            _blockService = blockService;
            _userService = userService;

            AddBlockCommand = new RelayCommand(OnAddBlock);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }


        public void SetBlock(Block block, Window window)
        {
            _window = window;
            CurrentBlock = block;
            Users = new ObservableCollection<User>( _userService.GetAllUsers() );
        }


        public RelayCommand AddBlockCommand { get; set; }

        public event Action RefreshBlockListRequested = delegate { };

        private void OnAddBlock()
        {
            if (!string.IsNullOrEmpty(CurrentBlock.Name) && CurrentBlock.ForemanId != null && CurrentBlock.LeadWorkerId != null)
            {
                _blockService.AddBlock(CurrentBlock);
                RefreshBlockListRequested();
                int id = CurrentBlock.ProjectId;
                CurrentBlock = new Block() { ProjectId = id };
            }
        }

        public RelayCommand CancelCommand { get; private set; }

        private void OnCancel()
        {
            _window.Close();
        }
    }
}
