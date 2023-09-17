using ERPSystem.Models;
using System.Collections.Generic;

namespace ERPSystem.Services.MaterialOrderRequestServices
{
    public interface IMaterialOrderRequestService
    {
        IEnumerable<MaterialOrderRequest> GetAll();
        IEnumerable<MaterialOrderRequest> GetAllByStockId(int stockId);
        MaterialOrderRequest GetById(int id);
        void Create(MaterialOrderRequest request);
        void Edit(MaterialOrderRequest request);
        void Delete(int id);
    }
}
