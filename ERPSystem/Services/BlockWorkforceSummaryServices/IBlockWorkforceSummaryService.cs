using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.BlockWorkforceSummaryServices
{
    public interface IBlockWorkforceSummaryService
    {
        IEnumerable<BlockWorkforceSummary> GetAllBlockWorkforceSummaries();
        IEnumerable<BlockWorkforceSummary> GetMonthBlockWorkforceSummariesWithBlockId(int blockId);
        IEnumerable<BlockWorkforceSummary> GetAllBlockWorkforceSummariesForToday();

        BlockWorkforceSummary GetBlockWorkforceSummaryById(int id);
        void AddBlockWorkforceSummary(BlockWorkforceSummary summary);
        void UpdateBlockWorkforceSummary(BlockWorkforceSummary summary);
        void DeleteBlockWorkforceSummary(int id);
    }
}
