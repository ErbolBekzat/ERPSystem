using ERPSystem.Data;
using ERPSystem.Models;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly DataBaseContext dbContext;

        public StockService(DataBaseContext context)
        {
            dbContext = context;
        }

        public List<Stock> GetAllStocksByProjectId(int projectId)
        {
            return dbContext.Stocks
                .Include(s => s.Materials)
                .Include(s => s.StockMovements)
                .Include(s => s.StockManager)
                .Where(s => s.ProjectId == projectId)
                .ToList();
        }

        public Stock GetStockByProjectId(int projectId)
        {
            return dbContext.Stocks
                .Include(s => s.Materials)
                .Include(s => s.StockMovements)
                .Include(s => s.StockManager)
                .FirstOrDefault(s => s.ProjectId == projectId);
        }

        public Stock GetStockById(int stockId)
        {
            return dbContext.Stocks
                .Include(s => s.Materials)
                .Include(s => s.StockMovements)
                .Include(s => s.StockManager)
                .FirstOrDefault(s => s.Id == stockId);
        }

        public void CreateStock(Stock stock)
        {
            dbContext.Stocks.Add(stock);
            dbContext.SaveChanges();
        }

        public void EditStock(Stock stock)
        {
            dbContext.Stocks.Update(stock);
            dbContext.SaveChanges();
        }

        public void DeleteStock(int stockId)
        {
            var stock = dbContext.Stocks.FirstOrDefault(s => s.Id == stockId);
            if (stock != null)
            {
                dbContext.Stocks.Remove(stock);
                dbContext.SaveChanges();
            }
        }
    }
}
