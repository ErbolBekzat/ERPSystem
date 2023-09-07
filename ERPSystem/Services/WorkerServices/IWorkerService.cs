using System.Collections.Generic;
using ERPSystem.Models;

namespace ERPSystem.Services.WorkerServices
{
    public interface IWorkerService
    {
        List<Worker> GetAllWorkers();
        Worker GetWorkerById(int workerId);
        int CreateWorker(Worker worker);
        int UpdateWorker(Worker worker);
        int DeleteWorker(Worker worker);
        string GetAttendanceStatusForToday(int workerId);
    }
}
