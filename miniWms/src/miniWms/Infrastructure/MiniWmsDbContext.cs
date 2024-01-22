using Microsoft.EntityFrameworkCore;
using miniWms.Domain.Entities;

namespace miniWms.Infrastructure
{
    public class MiniWmsDbContext : DbContext
    {
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentEntry> DocumentEntries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseEntry> WarehouseEntries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MiniWmsDbContext(DbContextOptions<MiniWmsDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(eb =>
            {
                eb.HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeId);

                eb.HasOne(d => d.ContractorSupplier)
                .WithMany(c => c.DocumentsAsSupplier)
                .HasForeignKey(d => d.SupplierId);

                eb.HasOne(d => d.ContractorRecipient)
                .WithMany(c => c.DocumentsAsRecipient)
                .HasForeignKey(d => d.RecipientId);

                eb.HasOne(d => d.WarehouseSupplier)
                .WithMany(w => w.DocumentsAsSupplier)
                .HasForeignKey(d => d.SupplierId);

                eb.HasOne(d => d.WarehouseRecipient)
                .WithMany(w => w.DocumentsAsRecipient)
                .HasForeignKey(d => d.RecipientId);
            });

            modelBuilder.Entity<DocumentEntry>(eb =>
            {
                eb.HasOne(de => de.Document)
                .WithMany(d => d.DocumentEntries)
                .HasForeignKey(de => de.DocumentId);

                eb.HasOne(de => de.Product)
                .WithMany(p => p.DocumentEntries)
                .HasForeignKey(de => de.ProductId);
            });

            modelBuilder.Entity<WarehouseEntry>(eb =>
            {
                eb.HasOne(we => we.Warehouse)
                .WithMany(w => w.WarehouseEntries)
                .HasForeignKey(we => we.WarehouseId);

                eb.HasOne(we => we.Product)
                .WithMany(p => p.WarehouseEntries)
                .HasForeignKey(we => we.ProductId);
            });

            modelBuilder.Entity<Product>(eb =>
            {
                eb.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Employee>(eb =>
            {
                eb.HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleId);

                eb.HasMany(e => e.CreatedCategories)
                .WithOne(ca => ca.CreatedByEmployee)
                .HasForeignKey(ca => ca.CreatedBy);

                eb.HasMany(e => e.ModifiedCategories)
                .WithOne(ca => ca.ModifiedByEmployee)
                .HasForeignKey(ca => ca.ModifiedBy);

                eb.HasMany(e => e.CreatedContractors)
                .WithOne(co => co.CreatedByEmployee)
                .HasForeignKey(co => co.CreatedBy);

                eb.HasMany(e => e.ModifiedContractors)
                .WithOne(co => co.ModifiedByEmployee)
                .HasForeignKey(co => co.ModifiedBy);

                eb.HasMany(e => e.CreatedDocuments)
                .WithOne(d => d.CreatedByEmployee)
                .HasForeignKey(d => d.CreatedBy);

                eb.HasMany(e => e.ModifiedDocuments)
                .WithOne(d => d.ModifiedByEmployee)
                .HasForeignKey(d => d.ModifiedBy);

                eb.HasMany(e => e.CreatedDocumentEntries)
                .WithOne(de => de.CreatedByEmployee)
                .HasForeignKey(de => de.CreatedBy);

                eb.HasMany(e => e.ModifiedDocumentEntries)
                .WithOne(de => de.ModifiedByEmployee)
                .HasForeignKey(de => de.ModifiedBy);

                eb.HasMany(e => e.CreatedDocumentTypes)
                .WithOne(dt => dt.CreatedByEmployee)
                .HasForeignKey(dt => dt.CreatedBy);

                eb.HasMany(e => e.ModifiedDocumentTypes)
                .WithOne(dt => dt.ModifiedByEmployee)
                .HasForeignKey(dt => dt.ModifiedBy);

                eb.HasMany(e => e.CreatedEmployees)
                .WithOne(e => e.CreatedByEmployee)
                .HasForeignKey(e => e.CreatedBy);

                eb.HasMany(e => e.ModifiedEmployees)
                .WithOne(e => e.ModifiedByEmployee)
                .HasForeignKey(e => e.ModifiedBy);

                eb.HasMany(e => e.CreatedProducts)
                .WithOne(p => p.CreatedByEmployee)
                .HasForeignKey(p => p.CreatedBy);

                eb.HasMany(e => e.ModifiedProducts)
                .WithOne(p => p.ModifiedByEmployee)
                .HasForeignKey(p => p.ModifiedBy);

                eb.HasMany(e => e.CreatedRoles)
                .WithOne(r => r.CreatedByEmployee)
                .HasForeignKey(r => r.CreatedBy);

                eb.HasMany(e => e.ModifiedRoles)
                .WithOne(r => r.ModifiedByEmployee)
                .HasForeignKey(r => r.ModifiedBy);

                eb.HasMany(e => e.CreatedWarehouses)
                .WithOne(w => w.CreatedByEmployee)
                .HasForeignKey(w => w.CreatedBy);

                eb.HasMany(e => e.ModifiedWarehouses)
                .WithOne(w => w.ModifiedByEmployee)
                .HasForeignKey(w => w.ModifiedBy);

                eb.HasMany(e => e.CreatedWarehouseEntries)
                .WithOne(we => we.CreatedByEmployee)
                .HasForeignKey(we => we.CreatedBy);

                eb.HasMany(e => e.ModifiedWarehouseEntries)
                .WithOne(we => we.ModifiedByEmployee)
                .HasForeignKey(we => we.ModifiedBy);
            });
        }
    }
}
