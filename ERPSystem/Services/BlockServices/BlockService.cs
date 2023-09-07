using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.BlockServices
{
    public class BlockService : IBlockService
    {
        private readonly DataBaseContext _context;

        public BlockService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Block> GetAllBlocks()
        {
            return _context.Blocks.ToList();
        }

        public IEnumerable<Block> GetBlocksByProjectId(int projectId)
        {
            return _context.Blocks.Where(b => b.ProjectId == projectId).ToList();
        }

        public Block GetBlockById(int blockId)
        {
            return _context.Blocks.FirstOrDefault(b => b.Id == blockId);
        }

        public void AddBlock(Block block)
        {
            _context.Blocks.Add(block);
            _context.SaveChanges();
        }

        public void UpdateBlock(Block block)
        {
            _context.Blocks.Update(block);
            _context.SaveChanges();
        }

        public void DeleteBlock(int blockId)
        {
            var block = _context.Blocks.Find(blockId);
            _context.Blocks.Remove(block);
            _context.SaveChanges();
        }
    }
}
