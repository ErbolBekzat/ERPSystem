using ERPSystem;
using ERPSystem.Models;
using System.ComponentModel.DataAnnotations;

public class TaskMaterials : BindableBase
{
    private int _taskId;
    private ERPSystem.Models.Subtask _subtask;
    private int _materialId;
    private Material _material;
    private int _quantityRequired;
    private int _cost;


    public int TaskId
    {
        get { return _taskId; }
        set { SetProperty(ref _taskId, value); }
    }

    public ERPSystem.Models.Subtask Subtask
    {
        get { return _subtask; }
        set { SetProperty(ref _subtask, value); }
    }

    public int MaterialId
    {
        get { return _materialId; }
        set { SetProperty(ref _materialId, value); }
    }

    public Material Material
    {
        get { return _material; }
        set { SetProperty(ref _material, value); }
    }

    public int QuantityRequired
    {
        get { return _quantityRequired; }
        set { SetProperty(ref _quantityRequired, value); }
    }

    public int Cost
    {
        get { return _cost; }
        set { SetProperty(ref _cost, value); }
    }
}