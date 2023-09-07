using System;

namespace ERPSystem.Models
{
    public class Attendance : BindableBase
    {
        private int _attendanceID;
        private int _workerID;
        private DateTime _attendanceDate;
        private string _attendanceStatus;
        private TimeSpan? _checkInTime;
        private TimeSpan? _checkOutTime;

        public int AttendanceID
        {
            get { return _attendanceID; }
            set { SetProperty(ref _attendanceID, value); }
        }

        public int WorkerID
        {
            get { return _workerID; }
            set { SetProperty(ref _workerID, value); }
        }

        public DateTime AttendanceDate
        {
            get { return _attendanceDate; }
            set { SetProperty(ref _attendanceDate, value); }
        }

        public string AttendanceStatus
        {
            get { return _attendanceStatus; }
            set { SetProperty(ref _attendanceStatus, value); }
        }

        public TimeSpan? CheckInTime
        {
            get { return _checkInTime; }
            set { SetProperty(ref _checkInTime, value); }
        }

        public TimeSpan? CheckOutTime
        {
            get { return _checkOutTime; }
            set { SetProperty(ref _checkOutTime, value); }
        }
    }
}
