using ERPSystem.Data;
using ERPSystem.Models;
using ERPSystem.Services.WorkerServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services.WorkerServices
{
    public class WorkerService : IWorkerService
    {
        private readonly DataBaseContext _dbContext;

        public WorkerService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Worker> GetAllWorkers()
        {
            return _dbContext.Workers.ToList();
        }

        public Worker GetWorkerById(int workerId)
        {
            return _dbContext.Workers.Find(workerId);
        }

        public int CreateWorker(Worker worker)
        {
            _dbContext.Workers.Add(worker);
            return _dbContext.SaveChanges();
        }

        public int UpdateWorker(Worker worker)
        {
            _dbContext.Workers.Update(worker);
            return _dbContext.SaveChanges();
        }

        public int DeleteWorker(Worker worker)
        {
            _dbContext.Workers.Remove(worker);
            return _dbContext.SaveChanges();
        }

        public string GetAttendanceStatusForToday(int workerId)
        {
            DateTime today = DateTime.Now;

            var existingAttendance = _dbContext.Attendances.FirstOrDefault(a => a.WorkerID == workerId && a.AttendanceDate.Date == today.Date);

            if (existingAttendance == null)
            {
                return "not exists";
            }
            else if (existingAttendance.CheckOutTime == null)
            {
                return "check out time";
            }
            else
            {
                return "exists";
            }
        }

    }
}
