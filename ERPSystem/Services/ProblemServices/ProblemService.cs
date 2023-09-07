using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.ProblemServices
{
    public class ProblemService : IProblemService
    {
        private readonly DataBaseContext _context;

        public ProblemService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Problem> GetAllProblems()
        {
            return _context.Problems.Include(p => p.AssignedUser).ToList();
        }

        public IEnumerable<Problem> GetAllProblemsBySubtaskId(int subtaskId)
        {
            return _context.Problems.Include(p => p.AssignedUser).Where(p => p.SubtaskId == subtaskId).ToList();
        }

        public Problem GetProblemById(int problemId)
        {
            return _context.Problems.Include(p => p.AssignedUser).FirstOrDefault(p => p.Id == problemId);
        }


        public void AddProblem(Problem problem)
        {
            _context.Problems.Add(problem);
            _context.SaveChanges();
        }

        public void UpdateProblem(Problem problem)
        {
            _context.Problems.Update(problem);
            _context.SaveChanges();
        }

        public void DeleteProblem(int problemId)
        {
            var problem = _context.Problems.Find(problemId);
            _context.Problems.Remove(problem);
            _context.SaveChanges();
        }
    }
}
