using ERPSystem.Data;
using ERPSystem.Services;
using ERPSystem.Services.AttendanceServices;
using ERPSystem.Services.BlockServices;
using ERPSystem.Services.BlockWorkforceSummaryServices;
using ERPSystem.Services.MaterialOrderRequestServices;
using ERPSystem.Services.ProblemServices;
using ERPSystem.Services.PurchaseOrderServices;
using ERPSystem.Services.Purhc;
using ERPSystem.Services.StockMovementServices;
using ERPSystem.Services.StockServices;
using ERPSystem.Services.SubtaskRelationshipServices;
using ERPSystem.Services.TaskMaterialServices;
using ERPSystem.Services.WorkerServices;
using Microsoft.Practices.Unity;
using System;

namespace ERPSystem
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;

        static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMaterialService, MaterialService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRoleService, RoleService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPermissionService, PermissionService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRolePermissionMappingService, RolePermissionMappingService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICurrentUser, CurrentUser>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISubtaskService, SubtaskService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IProjectService, ProjectService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISubtaskRelationshipService, SubtaskRelationshipService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IStockMovementService, StockMovementService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ITaskMaterialService, TaskMaterialService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IProblemService, ProblemService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IStockService, StockService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IBlockService, BlockService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWorkerService, WorkerService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAttendanceService, AttendanceService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IBlockWorkforceSummaryService, BlockWorkforceSummaryService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPurchaseOrderService, PurchaseOrderService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMaterialOrderRequestService, MaterialOrderRequestService>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
