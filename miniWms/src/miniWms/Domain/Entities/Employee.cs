using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [MinLength(3)]
        [MaxLength(3)]
        public string RoleId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        public bool HaveToChangePassword { get; set; }
        [MaxLength(20)]
        public string EmailAddress { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Region { get; set; }
        [MaxLength(200)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public Role? Role { get; set;}
        public IList<Category> CreatedCategories { get; set; } = new List<Category>();
        public IList<Category> ModifiedCategories { get; set; } = new List<Category>();
        public IList<Contractor> CreatedContractors { get; set; } = new List<Contractor>();
        public IList<Contractor> ModifiedContractors { get; set; } = new List<Contractor>();
        public IList<Document> CreatedDocuments { get; set; } = new List<Document>();
        public IList<Document> ModifiedDocuments { get; set; } = new List<Document>();
        public IList<DocumentEntry> CreatedDocumentEntries { get; set; } = new List<DocumentEntry>();
        public IList<DocumentEntry> ModifiedDocumentEntries { get; set; } = new List<DocumentEntry>();
        public IList<DocumentType> CreatedDocumentTypes { get; set; } = new List<DocumentType>();
        public IList<DocumentType> ModifiedDocumentTypes { get; set; } = new List<DocumentType>();
        public IList<Employee> CreatedEmployees { get; set; } = new List<Employee>();
        public IList<Employee> ModifiedEmployees { get; set; } = new List<Employee>();
        public IList<Product> CreatedProducts { get; set; } = new List<Product>();
        public IList<Product> ModifiedProducts { get; set; } = new List<Product>();
        public IList<Role> CreatedRoles { get; set; } = new List<Role>();
        public IList<Role> ModifiedRoles { get; set; } = new List<Role>();
        public IList<Warehouse> CreatedWarehouses { get; set; } = new List<Warehouse>();
        public IList<Warehouse> ModifiedWarehouses { get; set; } = new List<Warehouse>();
        public IList<WarehouseEntry> CreatedWarehouseEntries { get; set; } = new List<WarehouseEntry>();
        public IList<WarehouseEntry> ModifiedWarehouseEntries { get; set; } = new List<WarehouseEntry>();
    }
}
