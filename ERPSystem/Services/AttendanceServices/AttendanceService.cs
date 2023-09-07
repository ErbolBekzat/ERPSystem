using ERPSystem.Data;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services.AttendanceServices
{
    public class AttendanceService : IAttendanceService
    {
        private readonly DataBaseContext _dbContext;

        public AttendanceService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Attendance> GetAllAttendances()
        {
            return _dbContext.Attendances.ToList();
        }

        public List<Attendance> GetAttendancesByWorkerId(int workerId)
        {
            return _dbContext.Attendances.Where(a => a.WorkerID == workerId).ToList();
        }

        public Attendance GetAttendanceByWorkerIdForToday(int workerId)
        {
            DateTime today = DateTime.Now.Date;

            return _dbContext.Attendances
                .FirstOrDefault(a => a.WorkerID == workerId && a.AttendanceDate.Date == today);
        }


        public Attendance GetAttendanceById(int attendanceId)
        {
            return _dbContext.Attendances.Find(attendanceId);
        }

        public int CreateAttendance(Attendance attendance)
        {
            _dbContext.Attendances.Add(attendance);
            return _dbContext.SaveChanges();
        }

        public int UpdateAttendance(Attendance attendance)
        {
            _dbContext.Attendances.Update(attendance);
            return _dbContext.SaveChanges();
        }

        public int DeleteAttendance(int attendanceId)
        {
            var attendance = _dbContext.Attendances.Find(attendanceId);
            if (attendance != null)
            {
                _dbContext.Attendances.Remove(attendance);
                return _dbContext.SaveChanges();
            }
            return 0;
        }
    }
}
