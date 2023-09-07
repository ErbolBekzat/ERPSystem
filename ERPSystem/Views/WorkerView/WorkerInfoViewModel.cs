using ERPSystem.Models;
using ERPSystem.Services.AttendanceServices;
using ERPSystem.Services.WorkerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.WorkerView
{
    public class WorkerInfoViewModel :BindableBase
    {
        IWorkerService _workerService;
        IAttendanceService _attendanceService;

        private Worker _currentWorker;

        public Worker CurrentWorker
        {
            get { return _currentWorker; }
            set { SetProperty(ref _currentWorker, value); }
        }

        private ObservableCollection<Attendance> _workerAttendaces;

        public ObservableCollection<Attendance> WorkerAttendaces
        {
            get { return _workerAttendaces; }
            set { SetProperty(ref _workerAttendaces, value); }
        }




        public WorkerInfoViewModel(IWorkerService workerService, IAttendanceService attendanceService)
        {
            _workerService = workerService;
            _attendanceService = attendanceService;
        }


        public void SetWorker(Worker worker)
        {
            CurrentWorker = worker;
            WorkerAttendaces = new ObservableCollection<Attendance>(_attendanceService.GetAttendancesByWorkerId(CurrentWorker.Id));
        }
    }
}
