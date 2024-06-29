using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommand : IRequest<ResponseBase<Document>>
    {
        public Guid DocumentId { get; set; }
        public DateTime? DateOfOperation { get; set; }
        public string? Comments { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
