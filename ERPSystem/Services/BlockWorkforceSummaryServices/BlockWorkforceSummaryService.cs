using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.BlockWorkforceSummaryServices
{
    public class BlockWorkforceSummaryService : IBlockWorkforceSummaryService
    {
        private readonly DataBaseContext _context;

        public BlockWorkforceSummaryService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<BlockWorkforceSummary> GetAllBlockWorkforceSummaries()
        {
            return _context.BlockWorkforceSummaries.ToList();
        }

        public IEnumerable<BlockWorkforceSummary> GetAllBlockWorkforceSummariesForToday()
        {
            var currentDateWithZeroTime = DateTime.Now.Date;

            return _context.BlockWorkforceSummaries
                .Where(summary => summary.Date.Date == currentDateWithZeroTime)
                .ToList();
        }

        public IEnumerable<BlockWorkforceSummary> GetMonthBlockWorkforceSummariesWithBlockId(int blockId)
        {
            var currentDate = DateTime.Today;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            return _context.BlockWorkforceSummaries
                .Where(summary => summary.BlockId == blockId &&
                                  summary.Date >= firstDayOfMonth &&
                                  summary.Date <= lastDayOfMonth)
                .ToList();
        }

        public BlockWorkforceSummary GetBlockWorkforceSummaryById(int id)
        {
            return _context.BlockWorkforceSummaries.FirstOrDefault(summary => summary.Id == id);
        }

        public void AddBlockWorkforceSummary(BlockWorkforceSummary summary)
        {
            _context.BlockWorkforceSummaries.Add(summary);
            _context.SaveChanges();
        }

        public void UpdateBlockWorkforceSummary(BlockWorkforceSummary summary)
        {
            _context.BlockWorkforceSummaries.Update(summary);
            _context.SaveChanges();
        }

        public void DeleteBlockWorkforceSummary(int id)
        {
            var summary = _context.BlockWorkforceSummaries.Find(id);
            if (summary != null)
            {
                _context.BlockWorkforceSummaries.Remove(summary);
                _context.SaveChanges();
            }
        }
    }
}
