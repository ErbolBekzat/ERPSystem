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
        IEnumerable<PurchaseOrder> GetAllPurchaseOrders();
        IEnumerable<PurchaseOrder> GetAllPurchaseOrdersWithStockId(int stockId);
        PurchaseOrder GetPurchaseOrderById(int purchaseOrderId);
        void CreatePurchaseOrder(PurchaseOrder purchaseOrder);
        void EditPurchaseOrder(PurchaseOrder purchaseOrder);
        void DeletePurchaseOrder(int purchaseOrderId);
    }
}
