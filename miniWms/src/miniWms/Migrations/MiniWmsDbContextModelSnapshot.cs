﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using miniWms.Infrastructure;

#nullable disable

namespace miniWms.Migrations
{
    [DbContext(typeof(MiniWmsDbContext))]
    partial class MiniWmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("miniWms.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Contractor", b =>
                {
                    b.Property<Guid>("ContractorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRecipient")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSupplier")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VatId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContractorId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.DocumentEntry", b =>
                {
                    b.Property<Guid>("DocumentEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("DocumentEntryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("ProductId");

                    b.ToTable("DocumentEntries");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.DocumentType", b =>
                {
                    b.Property<string>("DocumentTypeId")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentTypeId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HaveToChangePassword")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsWeight")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.WarehouseEntry", b =>
                {
                    b.Property<Guid>("WarehouseEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WarehouseEntryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseEntries");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Category", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedCategories")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedCategories")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Contractor", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedContractors")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedContractors")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Document", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedDocuments")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedDocuments")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("miniWms.Domain.Entities.Contractor", "ContractorRecipient")
                        .WithMany("DocumentsAsRecipient")
                        .HasForeignKey("RecipientId");

                    b.HasOne("miniWms.Domain.Entities.Warehouse", "WarehouseRecipient")
                        .WithMany("DocumentsAsRecipient")
                        .HasForeignKey("RecipientId");

                    b.HasOne("miniWms.Domain.Entities.Contractor", "ContractorSupplier")
                        .WithMany("DocumentsAsSupplier")
                        .HasForeignKey("SupplierId");

                    b.HasOne("miniWms.Domain.Entities.Warehouse", "WarehouseSupplier")
                        .WithMany("DocumentsAsSupplier")
                        .HasForeignKey("SupplierId");

                    b.Navigation("ContractorRecipient");

                    b.Navigation("ContractorSupplier");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("DocumentType");

                    b.Navigation("ModifiedByEmployee");

                    b.Navigation("WarehouseRecipient");

                    b.Navigation("WarehouseSupplier");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.DocumentEntry", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedDocumentEntries")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Document", "Document")
                        .WithMany("DocumentEntries")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedDocumentEntries")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("miniWms.Domain.Entities.Product", "Product")
                        .WithMany("DocumentEntries")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("Document");

                    b.Navigation("ModifiedByEmployee");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.DocumentType", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedDocumentTypes")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedDocumentTypes")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Employee", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedEmployees")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedEmployees")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("miniWms.Domain.Entities.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Product", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedProducts")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedProducts")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("Category");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Role", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedRoles")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedRoles")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Warehouse", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedWarehouses")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedWarehouses")
                        .HasForeignKey("ModifiedBy");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.WarehouseEntry", b =>
                {
                    b.HasOne("miniWms.Domain.Entities.Employee", "CreatedByEmployee")
                        .WithMany("CreatedWarehouseEntries")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("miniWms.Domain.Entities.Employee", "ModifiedByEmployee")
                        .WithMany("ModifiedWarehouseEntries")
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("miniWms.Domain.Entities.Product", "Product")
                        .WithMany("WarehouseEntries")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("miniWms.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("WarehouseEntries")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("ModifiedByEmployee");

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Contractor", b =>
                {
                    b.Navigation("DocumentsAsRecipient");

                    b.Navigation("DocumentsAsSupplier");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Document", b =>
                {
                    b.Navigation("DocumentEntries");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.DocumentType", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Employee", b =>
                {
                    b.Navigation("CreatedCategories");

                    b.Navigation("CreatedContractors");

                    b.Navigation("CreatedDocumentEntries");

                    b.Navigation("CreatedDocumentTypes");

                    b.Navigation("CreatedDocuments");

                    b.Navigation("CreatedEmployees");

                    b.Navigation("CreatedProducts");

                    b.Navigation("CreatedRoles");

                    b.Navigation("CreatedWarehouseEntries");

                    b.Navigation("CreatedWarehouses");

                    b.Navigation("ModifiedCategories");

                    b.Navigation("ModifiedContractors");

                    b.Navigation("ModifiedDocumentEntries");

                    b.Navigation("ModifiedDocumentTypes");

                    b.Navigation("ModifiedDocuments");

                    b.Navigation("ModifiedEmployees");

                    b.Navigation("ModifiedProducts");

                    b.Navigation("ModifiedRoles");

                    b.Navigation("ModifiedWarehouseEntries");

                    b.Navigation("ModifiedWarehouses");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Product", b =>
                {
                    b.Navigation("DocumentEntries");

                    b.Navigation("WarehouseEntries");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("miniWms.Domain.Entities.Warehouse", b =>
                {
                    b.Navigation("DocumentsAsRecipient");

                    b.Navigation("DocumentsAsSupplier");

                    b.Navigation("WarehouseEntries");
                });
#pragma warning restore 612, 618
        }
    }
}