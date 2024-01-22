using FluentValidation.Results;
using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentEntries.Command.CreateDocumentEntries
{
    public class CreateDocumentEntriesCommandHandler : IRequestHandler<CreateDocumentEntriesCommand, ResponseBase<List<DocumentEntry>>>
    {
        private readonly IDocumentEntriesRepository _documentEntriesRepository;
        public CreateDocumentEntriesCommandHandler(IDocumentEntriesRepository documentEntriesRepository)
        {
            _documentEntriesRepository = documentEntriesRepository;
        }
        public async Task<ResponseBase<List<DocumentEntry>>> Handle(CreateDocumentEntriesCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDocumentEntryValidator();
            var errors = new List<ValidationFailure>();

            foreach (var documentEntry in request.DocumentEntries)
            {
                var validatorResult = await validator.ValidateAsync(documentEntry);

                if (!validatorResult.IsValid)
                {
                    errors.AddRange(validatorResult.Errors);
                }
            }

            if (errors.Count != 0)
            {
                return new ResponseBase<List<DocumentEntry>>(new ValidationResult(errors));
            }

            var newDecumentEntries = new List<DocumentEntry>();

            foreach (var documentEntry in request.DocumentEntries)
            {
                newDecumentEntries.Add(new DocumentEntry
                    {
                        DocumentId = documentEntry.DocumentId,
                        ProductId = documentEntry.ProductId,
                        Quantity = documentEntry.Quantity,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        CreatedBy = request.CreatedBy,
                        ModifiedBy = request.CreatedBy
                    });
            }

            var createdDocumentEntries = await _documentEntriesRepository.CreateRangeAsync(newDecumentEntries);

            return new ResponseBase<List<DocumentEntry>>(createdDocumentEntries.ToList());

        }
    }
}
