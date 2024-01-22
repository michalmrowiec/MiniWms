using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Contractor
    {
        public Guid ContractorId { get; set; }
        [MaxLength(250)]
        public string ContractorName { get; set;}
        [MaxLength(30)]
        public string VatId { get; set;}
        public bool IsSupplier { get; set;}
        public bool IsRecipient { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Region { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string Address { get; set;}
        [MaxLength(255)]
        public string EmailAddress { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<Document>? DocumentsAsSupplier { get; set; }
        public IList<Document>? DocumentsAsRecipient { get; set; }
    }
}
