﻿// <auto-generated />
using System;
using ERPSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ERPSystem.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ERPSystem.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AttendanceID"));

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AttendanceStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("CheckInTime")
                        .HasColumnType("interval");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("interval");

                    b.Property<int>("WorkerID")
                        .HasColumnType("integer");

                    b.HasKey("AttendanceID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("ERPSystem.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ForemanId")
                        .HasColumnType("integer");

                    b.Property<int>("LeadWorkerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("ERPSystem.Models.BlockWorkforceSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AeratedBlockWall")
                        .HasColumnType("integer");

                    b.Property<string>("AeratedBlockWallForeman")
                        .HasColumnType("text");

                    b.Property<int>("BlockId")
                        .HasColumnType("integer");

                    b.Property<string>("BlockName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Cleaning")
                        .HasColumnType("integer");

                    b.Property<string>("CleaningForeman")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("Earthworks")
                        .HasColumnType("integer");

                    b.Property<string>("EarthworksForeman")
                        .HasColumnType("text");

                    b.Property<int?>("ElectricalInstallations")
                        .HasColumnType("integer");

                    b.Property<string>("ElectricalInstallationsForeman")
                        .HasColumnType("text");

                    b.Property<int?>("Facade")
                        .HasColumnType("integer");

                    b.Property<string>("FacadeForeman")
                        .HasColumnType("text");

                    b.Property<string>("Foreman")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LeadWorker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MetalLiftStructureInstallation")
                        .HasColumnType("integer");

                    b.Property<string>("MetalLiftStructureInstallationForeman")
                        .HasColumnType("text");

                    b.Property<int?>("MonolithicReinforcedConcrete")
                        .HasColumnType("integer");

                    b.Property<string>("MonolithicReinforcedConcreteForeman")
                        .HasColumnType("text");

                    b.Property<int?>("PlumbingWorks")
                        .HasColumnType("integer");

                    b.Property<string>("PlumbingWorksForeman")
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SemiDryFloorScreed")
                        .HasColumnType("integer");

                    b.Property<string>("SemiDryFloorScreedForeman")
                        .HasColumnType("text");

                    b.Property<int?>("VentilationSystemInstallation")
                        .HasColumnType("integer");

                    b.Property<string>("VentilationSystemInstallationForeman")
                        .HasColumnType("text");

                    b.Property<int?>("WallPlastering")
                        .HasColumnType("integer");

                    b.Property<string>("WallPlasteringForeman")
                        .HasColumnType("text");

                    b.Property<int?>("WallReinforcement")
                        .HasColumnType("integer");

                    b.Property<string>("WallReinforcementForeman")
                        .HasColumnType("text");

                    b.Property<int?>("Waterproofing")
                        .HasColumnType("integer");

                    b.Property<string>("WaterproofingForeman")
                        .HasColumnType("text");

                    b.Property<int?>("WindowBlockInstallation")
                        .HasColumnType("integer");

                    b.Property<string>("WindowBlockInstallationForeman")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BlockWorkforceSummaries");
                });

            modelBuilder.Entity("ERPSystem.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("QuantityInStock")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubtaskId")
                        .HasColumnType("integer");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("UnitOfMeasure")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("SubtaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("ERPSystem.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PermissionId"));

                    b.Property<string>("PermissionDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("ERPSystem.Models.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("ProblemStatus")
                        .HasColumnType("integer");

                    b.Property<int>("SubtaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("SubtaskId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("ERPSystem.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ImageLink")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublishedStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("Version")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ERPSystem.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorizedById")
                        .HasColumnType("integer");

                    b.Property<string>("AuthorizedByName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ExpectedDeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime?>("ReceivedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RequestedById")
                        .HasColumnType("integer");

                    b.Property<string>("RequestedByName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("ERPSystem.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ERPSystem.Models.RolePermissionMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RolePermissionMappings");
                });

            modelBuilder.Entity("ERPSystem.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("StockManagerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.HasIndex("StockManagerId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("ERPSystem.Models.StockMovement", b =>
                {
                    b.Property<int>("MovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MovementId"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("MovementType")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubtaskId")
                        .HasColumnType("integer");

                    b.Property<int?>("TaskId")
                        .HasColumnType("integer");

                    b.HasKey("MovementId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("StockId");

                    b.HasIndex("SubtaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("StockMovements");
                });

            modelBuilder.Entity("ERPSystem.Models.Subtask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtasks");
                });

            modelBuilder.Entity("ERPSystem.Models.SubtaskRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildSubtaskId")
                        .HasColumnType("integer");

                    b.Property<int>("ParentSubtaskId")
                        .HasColumnType("integer");

                    b.Property<int>("RelationshipType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChildSubtaskId");

                    b.HasIndex("ParentSubtaskId");

                    b.ToTable("SubtaskRelationships");
                });

            modelBuilder.Entity("ERPSystem.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignedUserId")
                        .HasColumnType("integer");

                    b.Property<int>("BlockId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ERPSystem.Models.TaskRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildTaskId")
                        .HasColumnType("integer");

                    b.Property<int>("ParentTaskId")
                        .HasColumnType("integer");

                    b.Property<int>("RelationshipType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChildTaskId");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("TaskRelationships");
                });

            modelBuilder.Entity("ERPSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastOnline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ERPSystem.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("TaskMaterials", b =>
                {
                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("Cost")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityRequired")
                        .HasColumnType("integer");

                    b.HasKey("TaskId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("TaskMaterials");
                });

            modelBuilder.Entity("ERPSystem.Models.Material", b =>
                {
                    b.HasOne("ERPSystem.Models.Stock", "Stock")
                        .WithMany("Materials")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Subtask", null)
                        .WithMany("Materials")
                        .HasForeignKey("SubtaskId");

                    b.HasOne("ERPSystem.Models.Task", null)
                        .WithMany("Materials")
                        .HasForeignKey("TaskId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("ERPSystem.Models.Problem", b =>
                {
                    b.HasOne("ERPSystem.Models.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Subtask", "Subtask")
                        .WithMany()
                        .HasForeignKey("SubtaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("Subtask");
                });

            modelBuilder.Entity("ERPSystem.Models.Stock", b =>
                {
                    b.HasOne("ERPSystem.Models.Project", "Project")
                        .WithOne("Stock")
                        .HasForeignKey("ERPSystem.Models.Stock", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.User", "StockManager")
                        .WithMany()
                        .HasForeignKey("StockManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("StockManager");
                });

            modelBuilder.Entity("ERPSystem.Models.StockMovement", b =>
                {
                    b.HasOne("ERPSystem.Models.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("ERPSystem.Models.Material", "Material")
                        .WithMany("StockMovements")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Stock", "Stock")
                        .WithMany("StockMovements")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Subtask", "Subtask")
                        .WithMany("StockMovements")
                        .HasForeignKey("SubtaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ERPSystem.Models.Task", null)
                        .WithMany("StockMovements")
                        .HasForeignKey("TaskId");

                    b.Navigation("CreatedUser");

                    b.Navigation("Material");

                    b.Navigation("Stock");

                    b.Navigation("Subtask");
                });

            modelBuilder.Entity("ERPSystem.Models.Subtask", b =>
                {
                    b.HasOne("ERPSystem.Models.User", "AssignedUser")
                        .WithMany("Subtasks")
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Project", null)
                        .WithMany("Subtasks")
                        .HasForeignKey("ProjectId");

                    b.HasOne("ERPSystem.Models.Task", "Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("ERPSystem.Models.SubtaskRelationship", b =>
                {
                    b.HasOne("ERPSystem.Models.Subtask", "ChildSubtask")
                        .WithMany("ChildSubtasks")
                        .HasForeignKey("ChildSubtaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Subtask", "ParentSubtask")
                        .WithMany("ParentSubtasks")
                        .HasForeignKey("ParentSubtaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChildSubtask");

                    b.Navigation("ParentSubtask");
                });

            modelBuilder.Entity("ERPSystem.Models.Task", b =>
                {
                    b.HasOne("ERPSystem.Models.User", "AssignedUser")
                        .WithMany("Tasks")
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ERPSystem.Models.TaskRelationship", b =>
                {
                    b.HasOne("ERPSystem.Models.Task", "ChildTask")
                        .WithMany("ChildTasks")
                        .HasForeignKey("ChildTaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Task", "ParentTask")
                        .WithMany("ParentTasks")
                        .HasForeignKey("ParentTaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChildTask");

                    b.Navigation("ParentTask");
                });

            modelBuilder.Entity("TaskMaterials", b =>
                {
                    b.HasOne("ERPSystem.Models.Material", "Material")
                        .WithMany("TaskMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERPSystem.Models.Subtask", "Subtask")
                        .WithMany("TaskMaterials")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Subtask");
                });

            modelBuilder.Entity("ERPSystem.Models.Material", b =>
                {
                    b.Navigation("StockMovements");

                    b.Navigation("TaskMaterials");
                });

            modelBuilder.Entity("ERPSystem.Models.Project", b =>
                {
                    b.Navigation("Stock")
                        .IsRequired();

                    b.Navigation("Subtasks");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ERPSystem.Models.Stock", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("StockMovements");
                });

            modelBuilder.Entity("ERPSystem.Models.Subtask", b =>
                {
                    b.Navigation("ChildSubtasks");

                    b.Navigation("Materials");

                    b.Navigation("ParentSubtasks");

                    b.Navigation("StockMovements");

                    b.Navigation("TaskMaterials");
                });

            modelBuilder.Entity("ERPSystem.Models.Task", b =>
                {
                    b.Navigation("ChildTasks");

                    b.Navigation("Materials");

                    b.Navigation("ParentTasks");

                    b.Navigation("StockMovements");

                    b.Navigation("Subtasks");
                });

            modelBuilder.Entity("ERPSystem.Models.User", b =>
                {
                    b.Navigation("Subtasks");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}