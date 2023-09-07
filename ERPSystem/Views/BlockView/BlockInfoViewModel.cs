using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ERPSystem.Views.BlockView
{
    public class BlockInfoViewModel: BindableBase
    {
        IProjectService _projectService;
        IBlockService _blockService;
        ITaskService _taskService;

        private Block _currentBlock;

        public Block CurrentBlock
        {
            get { return _currentBlock; }
            set { SetProperty(ref _currentBlock, value); }
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        Project project;


        public BlockInfoViewModel(IProjectService projectService, IBlockService blockService, ITaskService taskService)
        {
            _projectService = projectService;
            _blockService = blockService;
            _taskService = taskService;

            EditBlockCommand = new RelayCommand(OnEditBlock);
            SaveBlockCommand = new RelayCommand(OnSaveBlcok);
            DeleteBlockCommand = new RelayCommand(OnDeleteBlcok);
            BackCommand = new RelayCommand(OnBack);
        }

        public async void SetBlock(Block block)
        {
            project = await _projectService.GetProjectByIdAsync(block.ProjectId);
            CurrentBlock = block;
            EditMode = false;
        }


        public RelayCommand EditBlockCommand { get; set; }

        private void OnEditBlock()
        {
            EditMode = !EditMode;
        }

        public RelayCommand SaveBlockCommand { get; set; }

        private void OnSaveBlcok()
        {
            if (!string.IsNullOrWhiteSpace(CurrentBlock.Name))
            {
                _blockService.UpdateBlock(CurrentBlock);
                EditMode = false;
            }
        }

        public RelayCommand DeleteBlockCommand { get; private set; }
        public event Action<Project> Done = delegate { };

        private void OnDeleteBlcok()
        {
            ObservableCollection<Task> tasks = new ObservableCollection<Task>( _taskService.GetTasksWithBlockId(CurrentBlock.Id) );

            if (tasks.Count == 0 || tasks == null)
            {
                _blockService.DeleteBlock(CurrentBlock.Id);

                Done(project);
            }
            else
            {

            }
        }

        public RelayCommand BackCommand { get; private set; }

        private void OnBack()
        {
            Done(project);
        }
    }
}
