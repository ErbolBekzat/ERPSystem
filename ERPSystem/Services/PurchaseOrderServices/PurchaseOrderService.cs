﻿using ERPSystem.Data;
using ERPSystem.Models;
using ERPSystem.Services.PurchaseOrderServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services.Purhc
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly DataBaseContext _context;

        public PurchaseOrderService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<MaterialPurchaseOrder> GetAllPurchaseOrders()
        {
            return _context.PurchaseOrders
                .ToList();
        }

        public IEnumerable<MaterialPurchaseOrder> GetAllPurchaseOrdersWithStockId(int stockId)
        {
            return _context.PurchaseOrders
                .Where(po => po.StockId == stockId)
                .ToList();
        }

        public MaterialPurchaseOrder GetPurchaseOrderById(int purchaseOrderId)
        {
            return _context.PurchaseOrders
                .SingleOrDefault(po => po.Id == purchaseOrderId);
        }

        public void CreatePurchaseOrder(MaterialPurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChanges();
        }

        public void EditPurchaseOrder(MaterialPurchaseOrder purchaseOrder)
        {
            _context.Entry(purchaseOrder).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePurchaseOrder(int purchaseOrderId)
        {
            var purchaseOrderToDelete = GetPurchaseOrderById(purchaseOrderId);

            if (purchaseOrderToDelete != null)
            {
                _context.PurchaseOrders.Remove(purchaseOrderToDelete);
                _context.SaveChanges();
            }
        }
    }
}
