using ERPSystem.Converters;
using ERPSystem.Models;
using ERPSystem.Services.AttendanceServices;
using ERPSystem.Services.WorkerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.WorkerView
{
    public class WorkerListViewModel : BindableBase
    {
        IWorkerService _workerService;
        IAttendanceService _attendanceService;

        private ObservableCollection<Worker> _workers;

        public ObservableCollection<Worker> Workers
        {
            get { return _workers; }
            set { SetProperty(ref _workers, value); }
        }


        public WorkerListViewModel(IWorkerService workerService, IAttendanceService attendanceService)
        {
            _workerService = workerService;
            _attendanceService = attendanceService;

            WorkerInfoCommand = new RelayCommand<Worker>(OnWorkerInfo);
            AddWorkerCommand = new RelayCommand(OnAddWorker);
            AddAtendanceRecordCommand = new RelayCommand<int>(OnAddAtendanceRecord);
            AddCheckOutAtendanceRecordCommand = new RelayCommand<int>(OnAddCheckOutAtendanceRecordCommand);
        }


        public void LoadWorkers()
        {
            Workers = new ObservableCollection<Worker>(_workerService.GetAllWorkers());
        }

        public RelayCommand<Worker> WorkerInfoCommand { get; set; }
        public event Action<Worker> WorkerInfoRequested = delegate { };
        private void OnWorkerInfo(Worker worker)
        {
            WorkerInfoRequested(worker);
        }

        public RelayCommand AddWorkerCommand { get; set; }

        private void OnAddWorker()
        {
            AddEditWorkerView addEditWorkerView = new AddEditWorkerView();
            addEditWorkerView.ViewModel.EditMode = false;
            addEditWorkerView.ViewModel.SetWorker(new Worker(), addEditWorkerView);
            addEditWorkerView.ViewModel.Done += LoadWorkers;
            addEditWorkerView.ShowDialog();
        }

        private bool _hasAttendanceForToday;
        public bool HasAttendanceForToday
        {
            get { return _hasAttendanceForToday; }
            set { SetProperty(ref _hasAttendanceForToday, value); }
        }

        public RelayCommand<int> AddAtendanceRecordCommand { get; set; }

        private void OnAddAtendanceRecord(int workerId)
        {
            Attendance attendance = new Attendance();
            attendance.WorkerID = workerId;
            attendance.AttendanceDate = DateTime.Now;
            attendance.AttendanceStatus = "Присутствует";
            attendance.CheckInTime = DateTime.Now.TimeOfDay;

            _attendanceService.CreateAttendance(attendance);

            LoadWorkers();
        }

        public RelayCommand<int> AddCheckOutAtendanceRecordCommand { get; set; }

        private void OnAddCheckOutAtendanceRecordCommand(int workerId)
        {
            Attendance existingAttendance = _attendanceService.GetAttendanceByWorkerIdForToday(workerId);
            existingAttendance.CheckOutTime = DateTime.Now.TimeOfDay;

            _attendanceService.UpdateAttendance(existingAttendance);

            LoadWorkers();
        }

        public string LoadAttendanceInfo(int workerId)
        {
            string status = _workerService.GetAttendanceStatusForToday(workerId);
            return status;
        }
    }
}
