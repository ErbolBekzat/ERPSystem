using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.PurchaseOrderServices
{
    public interface IPurchaseOrderService
    {
        IEnumerable<MaterialPurchaseOrder> GetAllPurchaseOrders();
        IEnumerable<MaterialPurchaseOrder> GetAllPurchaseOrdersWithStockId(int stockId);
        MaterialPurchaseOrder GetPurchaseOrderById(int purchaseOrderId);
        void CreatePurchaseOrder(MaterialPurchaseOrder purchaseOrder);
        void EditPurchaseOrder(MaterialPurchaseOrder purchaseOrder);
        void DeletePurchaseOrder(int purchaseOrderId);
    }
}
