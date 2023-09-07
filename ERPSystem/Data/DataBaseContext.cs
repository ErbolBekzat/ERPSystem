using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERPSystem.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Material> Materials { get; set; }
        public DbSet<TaskMaterials> TaskMaterials { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Problem> Problems { get; set; }

        public DbSet<BlockWorkforceSummary> BlockWorkforceSummaries { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<TaskRelationship> TaskRelationships { get; set; }
        public DbSet<SubtaskRelationship> SubtaskRelationships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ERPSystem;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(@"Server=35.224.54.25;Database=ERPSystem;Uid=sqlserver;Pwd=an38750301;TrustServerCertificate=True;");
                //optionsBuilder.UseSqlServer("Server=database-1.cfrtsnslg3vz.eu-north-1.rds.amazonaws.com,1433;Database=ERPSystem;User ID=admin;Password=12345678;TrustServerCertificate=True;");
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ERPSystem;Username=postgres;Password=admin;TimeZone=+06");
                //optionsBuilder.UseNpgsql("Host=master.0c948276-0413-4a48-aa82-4048fde9d591.c.dbaas.selcloud.ru;Port=5432;" +
                //    "Database=ERPSystem;Username=erbol;Password=admin;SSL Mode=Prefer;Trust Server Certificate=true;");


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .Property(t => t.Status)
                .HasDefaultValue(TaskStatus.NotStarted);

            modelBuilder.Entity<Subtask>()
                .HasOne(s => s.AssignedUser)
                .WithMany(u => u.Subtasks)
                .HasForeignKey(s => s.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subtask>()
                .HasOne(s => s.Task)
                .WithMany(t => t.Subtasks)
                .HasForeignKey(s => s.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskRelationship>()
                .HasOne(tr => tr.ParentTask)
                .WithMany(t => t.ParentTasks)
                .HasForeignKey(tr => tr.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskRelationship>()
                .HasOne(tr => tr.ChildTask)
                .WithMany(t => t.ChildTasks)
                .HasForeignKey(tr => tr.ChildTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubtaskRelationship>()
                .HasOne(sr => sr.ParentSubtask)
                .WithMany(s => s.ParentSubtasks)
                .HasForeignKey(sr => sr.ParentSubtaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubtaskRelationship>()
                .HasOne(sr => sr.ChildSubtask)
                .WithMany(s => s.ChildSubtasks)
                .HasForeignKey(sr => sr.ChildSubtaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMaterials>()
                .HasKey(tm => new { tm.TaskId, tm.MaterialId });

            modelBuilder.Entity<TaskMaterials>()
                .HasOne(tm => tm.Subtask)
                .WithMany(t => t.TaskMaterials)
                .HasForeignKey(tm => tm.TaskId);

            modelBuilder.Entity<TaskMaterials>()
                .HasOne(tm => tm.Material)
                .WithMany(m => m.TaskMaterials)
                .HasForeignKey(tm => tm.MaterialId);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Stock)
                .WithMany(s => s.Materials)
                .HasForeignKey(m => m.StockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Material>()
                .Property(m => m.QuantityInStock)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Material>()
                .Property(m => m.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<StockMovement>()
                .HasKey(sm => sm.MovementId);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Material)
                .WithMany(m => m.StockMovements)
                .HasForeignKey(sm => sm.MaterialId);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Subtask)
                .WithMany(t => t.StockMovements)
                .HasForeignKey(sm => sm.SubtaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockMovement>()
                .HasOne(sm => sm.Stock)
                .WithMany(s => s.StockMovements)
                .HasForeignKey(s => s.StockId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Problem>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Project)
                .WithOne(p => p.Stock)
                .HasForeignKey<Stock>(s => s.ProjectId)
                .IsRequired();

            modelBuilder.Entity<BlockWorkforceSummary>()
            .Property(e => e.Date)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Task>()
            .Property(e => e.StartDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Task>()
            .Property(e => e.EndDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Task>()
            .Property(e => e.CompletedDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Subtask>()
            .Property(e => e.StartDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Subtask>()
            .Property(e => e.EndDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Subtask>()
            .Property(e => e.CompletedDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Attendance>()
            .Property(e => e.AttendanceDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Project>()
            .Property(e => e.StartDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Project>()
            .Property(e => e.EndDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Project>()
            .Property(e => e.CompletedDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Project>()
            .Property(e => e.UpdateDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<StockMovement>()
            .Property(e => e.CreatedDate)
            .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<User>()
            .Property(e => e.LastOnline)
            .HasColumnType("timestamp without time zone");
        }
    }
}
