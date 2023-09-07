using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public interface IMaterialService
    {
        Material GetMaterialById(int id);
        List<Material> GetAllMaterials();
        List<Material> GetMaterialsByStockId(int stockId);
        void AddMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(Material material);
    }
}
