using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.BlockServices
{
    public interface IBlockService
    {
        IEnumerable<Block> GetAllBlocks();
        IEnumerable<Block> GetBlocksByProjectId(int projectId);
        Block GetBlockById(int blockId);
        void AddBlock(Block block);
        void UpdateBlock(Block block);
        void DeleteBlock(int blockId);
    }
}
