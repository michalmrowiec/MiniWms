namespace miniWms.Domain.Entities
{
    public class Contractor
    {
        public Guid ContractorId { get; set; }
        public string ContractorName { get; set;}
        public string VatId { get; set;}
        public bool IsSupplier { get; set;}
        public bool IsRecipient { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set;}
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
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
