using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.StockServices
{
    public interface IStockService
    {
        List<Stock> GetAllStocksByProjectId(int projectId);
        Stock GetStockByProjectId(int projectId);
        Stock GetStockById(int stockId);
        void CreateStock(Stock stock);
        void EditStock(Stock stock);
        void DeleteStock(int stockId);
    }
}
