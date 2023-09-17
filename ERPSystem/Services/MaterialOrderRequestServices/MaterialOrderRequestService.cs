using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services.MaterialOrderRequestServices
{
    public class MaterialOrderRequestService : IMaterialOrderRequestService
    {
        private readonly DataBaseContext _context;

        public MaterialOrderRequestService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<MaterialOrderRequest> GetAll()
        {
            return _context.MaterialOrderRequests.ToList();
        }

        public IEnumerable<MaterialOrderRequest> GetAllByStockId(int stockId)
        {
            return _context.MaterialOrderRequests
                .Where(request => request.StockId == stockId)
                .ToList();
        }

        public MaterialOrderRequest GetById(int id)
        {
            return _context.MaterialOrderRequests
                .SingleOrDefault(request => request.Id == id);
        }

        public void Create(MaterialOrderRequest request)
        {
            _context.MaterialOrderRequests.Add(request);
            _context.SaveChanges();
        }

        public void Edit(MaterialOrderRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var requestToDelete = GetById(id);

            if (requestToDelete != null)
            {
                _context.MaterialOrderRequests.Remove(requestToDelete);
                _context.SaveChanges();
            }
        }
    }
}
