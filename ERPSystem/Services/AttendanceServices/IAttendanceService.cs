using ERPSystem.Models;
using System;
using System.Collections.Generic;

namespace ERPSystem.Services.AttendanceServices
{
    public interface IAttendanceService
    {
        List<Attendance> GetAllAttendances();
        List<Attendance> GetAttendancesByWorkerId(int workerId);
        Attendance GetAttendanceByWorkerIdForToday(int workerId);
        Attendance GetAttendanceById(int attendanceId);
        
        int CreateAttendance(Attendance attendance);
        int UpdateAttendance(Attendance attendance);
        int DeleteAttendance(int attendanceId);
    }
}
