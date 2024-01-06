using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        [MaxLength(50)]
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
