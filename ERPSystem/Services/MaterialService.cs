using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly DataBaseContext _context;

        public MaterialService(DataBaseContext context)
        {
            _context = context;
        }

        public Material GetMaterialById(int id)
        {
            return _context.Materials
                .Include(m => m.TaskMaterials) // Include TaskMaterials
                .Include(m => m.StockMovements) // Include StockMovements
                .SingleOrDefault(m => m.Id == id);
        }


        public List<Material> GetAllMaterials()
        {
            return _context.Materials
                .Include(m => m.TaskMaterials) // Include TaskMaterials
                .Include(m => m.StockMovements) // Include StockMovements
                .ToList();
        }


        public List<Material> GetMaterialsByStockId(int stockId)
        {
            return _context.Materials
                .Include(m => m.TaskMaterials) // Include TaskMaterials
                .Include(m => m.StockMovements) // Include StockMovements
                .Where(m => m.StockId == stockId)
                .ToList();
        }


        public void AddMaterial(Material material)
        {
            _context.Materials.Add(material);
            _context.SaveChanges();
        }

        public void UpdateMaterial(Material material)
        {
            _context.Entry(material).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMaterial(Material material)
        {
            _context.Materials.Remove(material);
            _context.SaveChanges();
        }
    }
}
