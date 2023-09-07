using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using ERPSystem.Services.ProblemServices;
using ERPSystem.Utilities.GoogleStorage;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ERPSystem.Views.ProblemView
{
    public class AddEditProblemViewModel : BindableBase
    {
        IBlockService _blockService;
        ITaskService _taskService;
        ISubtaskService _subtaskService;
        IProblemService _problemService;
        IUserService _userService;

        private byte[] selectedImageBytes;

        private BitmapImage _bitmap;

        public BitmapImage Bitmap
        {
            get { return _bitmap; }
            set { SetProperty(ref _bitmap, value); }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }


        public AddEditProblemViewModel(IBlockService blockService, ITaskService taskService, ISubtaskService subtaskService, IProblemService problemService, IUserService userService)
        {
            _blockService = blockService;
            _taskService = taskService;
            _subtaskService = subtaskService;
            _problemService = problemService;
            _userService = userService;

            ChooseImageCommand = new RelayCommand(OnChooseImage);
            AddProblemCommand = new RelayCommand(OnAddProblem);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private bool _EditMode;

        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private Problem _editingProblem;

        public Problem EditingProblem
        {
            get { return _editingProblem; }
            set { SetProperty(ref _editingProblem, value); }
        }

        Block block;

        public void SetProblem(Subtask subtask)
        {
            EditingProblem = new Problem();
            EditingProblem.SubtaskId = subtask.Id;

            Users = new ObservableCollection<User>(_userService.GetAllUsers());

            block = _blockService.GetBlockById(subtask.Task.BlockId);
        }

        string imagePath;

        public RelayCommand ChooseImageCommand { get; set; }

        private void OnChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(imagePath); // Get the file name without the full path

                // Create the new object name with the prefix and file name
                string objectName = "40tribes_" + fileName;

                EditingProblem.Image = objectName;

                byte[] selectedImageBytes = File.ReadAllBytes(imagePath);
                BitmapImage bitmap = LoadImage(selectedImageBytes);
                Bitmap = bitmap;
            }
        }


        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (var stream = new System.IO.MemoryStream(imageData))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }

        public RelayCommand AddProblemCommand { get; set; }
        public event Action<Block> BackToBlockPlanRequested = delegate { };

        private void OnAddProblem()
        {
            var cloudStorageService = new CloudStorageService();
            cloudStorageService.UploadFile(imagePath, EditingProblem.Image);

            EditingProblem.ProblemStatus = Models.TaskStatus.InProgress;
            _problemService.AddProblem(EditingProblem);

            OnCancel();
        }

        public RelayCommand CancelCommand { get; set; }

        private void OnCancel()
        {
            Bitmap = new BitmapImage();
            BackToBlockPlanRequested(block);
        }
    }
}
