using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.ProblemServices
{
    public interface IProblemService
    {
        IEnumerable<Problem> GetAllProblems();
        IEnumerable<Problem> GetAllProblemsBySubtaskId(int subtaskId);
        Problem GetProblemById(int problemId);
        void AddProblem(Problem problem);
        void UpdateProblem(Problem problem);
        void DeleteProblem(int problemId);
    }
}
