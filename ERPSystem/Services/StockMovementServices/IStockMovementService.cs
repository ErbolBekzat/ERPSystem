using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.StockMovementServices
{
    public interface IStockMovementService
    {
        StockMovement GetStockMovementById(int id);
        List<StockMovement> GetStockMovementsByStockId(int stockId);
        List<StockMovement> GetAllStockMovements();
        void AddStockMovement(StockMovement stockMovement);
        void UpdateStockMovement(StockMovement stockMovement);
        void DeleteStockMovement(StockMovement stockMovement);
    }
}
