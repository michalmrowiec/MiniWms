using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateContractorCommand : IRequest<ResponseBase<Contractor>>
    {
        public string ContractorName { get; set; }
        public string VatId { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsRecipient { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
