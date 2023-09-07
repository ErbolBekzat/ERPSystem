using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.StockMovementServices
{
    public class StockMovementService : IStockMovementService
    {
        private readonly DataBaseContext _context;

        public StockMovementService(DataBaseContext context)
        {
            _context = context;
        }

        public StockMovement GetStockMovementById(int id)
        {
            return _context.StockMovements.Include(sm => sm.Material).FirstOrDefault(sm => sm.MovementId == id);
        }

        public List<StockMovement> GetStockMovementsByStockId(int stockId)
        {
            return _context.StockMovements
                .Where(sm => sm.StockId == stockId)
                .Include(sm => sm.Material)
                .ToList();
        }


        public List<StockMovement> GetAllStockMovements()
        {
            return _context.StockMovements.Include(sm => sm.Material).ToList();
        }


        public void AddStockMovement(StockMovement stockMovement)
        {
            _context.StockMovements.Add(stockMovement);
            _context.SaveChanges();
        }

        public void UpdateStockMovement(StockMovement stockMovement)
        {
            _context.Entry(stockMovement).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteStockMovement(StockMovement stockMovement)
        {
            _context.StockMovements.Remove(stockMovement);
            _context.SaveChanges();
        }
    }
}
