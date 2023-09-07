using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using ERPSystem.Views.BlockWorkforceSummaryView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.BlockView
{
    public class BlockListViewModel : BindableBase
    {
        IBlockService _blockService;
        IUserService _userService;

        private Project _currentProject;

        public Project CurrentProject
        {
            get { return _currentProject; }
            set { SetProperty(ref _currentProject, value); }
        }


        public BlockListViewModel(IBlockService blockService, IUserService userService)
        {
            _blockService = blockService;
            _userService = userService;

            AddBlockCommand = new RelayCommand(OnAddBlock);
            BlockInfoCommand = new RelayCommand<Block>(OnBlockInfo);
            NavToBlockCommand = new RelayCommand<Block>(OnNavToBlock);
            CreateBlockWorkforceSummaryCommand = new RelayCommand<Block>(OnCreateBlockWorkforceSummary);
            ShowReportCommand = new RelayCommand(OnShowReport);
        }

        private ObservableCollection<Block> _blocks;

        public ObservableCollection<Block> Blocks
        {
            get { return _blocks; }
            set { SetProperty(ref _blocks, value); }
        }


        public void DisplayBlocks(Project project)
        {
            CurrentProject = project;

            Blocks = new ObservableCollection<Block>(_blockService.GetBlocksByProjectId(CurrentProject.Id)
                .OrderBy(block => block.Name.Length)
                .ToList());

        }

        public void DisplayBlocks()
        {
            Blocks = new ObservableCollection<Block>(_blockService.GetBlocksByProjectId(CurrentProject.Id)
                .OrderBy(block => block.Name.Length)
                .ToList());

        }

        public RelayCommand AddBlockCommand { get; set; }

        private void OnAddBlock()
        {
            AddEditBlockWindowView addEditBlockWindowView = new AddEditBlockWindowView();
            addEditBlockWindowView.ViewModel.EditMode = false;
            addEditBlockWindowView.ViewModel.SetBlock(new Block() { ProjectId = CurrentProject.Id }, addEditBlockWindowView);
            addEditBlockWindowView.ViewModel.RefreshBlockListRequested += DisplayBlocks;
            addEditBlockWindowView.ShowDialog();
        }

        public RelayCommand<Block> BlockInfoCommand { get; set; }
        public event Action<Block> BlockInfoRequested = delegate { };

        private void OnBlockInfo(Block block)
        {
            BlockInfoRequested(block);
        }

        public RelayCommand<Block> NavToBlockCommand { get; set; }
        public event Action<Block> NavToBlockTasksRequested = delegate { };

        private void OnNavToBlock(Block block)
        {
            NavToBlockTasksRequested(block);
        }

        public RelayCommand<Block> CreateBlockWorkforceSummaryCommand { get; set; }

        private void OnCreateBlockWorkforceSummary(Block block)
        {
            User foreman = _userService.GetUserById(block.ForemanId);
            User leadWorker = _userService.GetUserById(block.LeadWorkerId);

            BlockWorkforceSummary bws = new BlockWorkforceSummary();
            bws.ProjectId = CurrentProject.Id;
            bws.ProjectName = CurrentProject.Name;
            bws.BlockId = block.Id;
            bws.BlockName = block.Name;
            bws.Foreman = foreman.FirstName + " " + foreman.LastName;
            bws.LeadWorker = leadWorker.FirstName + " " + leadWorker.LastName;
            bws.Date = DateTime.Today;

            AddEditBlockWorkforceSummaryView addEditbwsView = new AddEditBlockWorkforceSummaryView();
            addEditbwsView.ViewModel.EditMode = false;
            addEditbwsView.ViewModel.SetBWS(bws, addEditbwsView);
            addEditbwsView.ShowDialog();
        }

        public RelayCommand ShowReportCommand { get; set; }
        public event Action ShowReportRequested = delegate { };

        private void OnShowReport()
        {
            ShowReportRequested(); ;
        }
    }
}
