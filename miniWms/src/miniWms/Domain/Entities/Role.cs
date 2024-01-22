using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Role
    {
        [MinLength(3)]
        [MaxLength(3)]
        public string RoleId { get; set; }
        [MaxLength(50)]
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<Employee> Employees { get; set; } = new List<Employee>();
    }
}
