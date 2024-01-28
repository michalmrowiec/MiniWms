using FluentValidation;
using MediatR;
using miniWms.Application.Functions.Contractors.Queries.GetContractorById;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Application.Functions.Warehouses.Queries.GetWarehouseById;

namespace miniWms.Application.Functions.Documents.Documents.Commands.CreateDocument
{
    public class CreateDocumentValidator : AbstractValidator<CreateDocumentCommand>
    {
        protected readonly IMediator _mediator;
        public CreateDocumentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(d => d.MainWarehouseId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Custom((value, context) =>
                {
                    var warehouse = _mediator.Send(new GetWarehouseByIdQuery((Guid)value)).Result;
                    if (!warehouse.Success)
                        context.AddFailure("MainWarehouseId", "Main warehouse doesn't exist");
                });

            RuleFor(d => d.TargetWarehouseId)
                .Null().When(d => d.ContractorId.HasValue)
                .WithMessage("Cannot specify the contractor and the target warehouse at the same time.")
                .Custom((value, context) =>
                {
                    if (value.HasValue)
                    {
                        var warehouse = _mediator.Send(new GetWarehouseByIdQuery((Guid)value)).Result;
                        if (!warehouse.Success)
                            context.AddFailure("TargetWarehouseId", "Target warehouse doesn't exist");
                    }
                });

            RuleFor(d => d.ContractorId)
                .Null().When(d => d.TargetWarehouseId.HasValue)
                .WithMessage("Cannot specify the contractor and the target warehouse at the same time.")
                .Custom((value, context) =>
                {
                    if (value.HasValue)
                    {
                        var contractor = _mediator.Send(new GetContractorByIdQuery((Guid)value)).Result;
                        if (!contractor.Success)
                            context.AddFailure("ContractorId", "Contractor doesn't exist");
                    }
                });

            RuleFor(d => d)
                .Custom((value, context) =>
                {
                    var documentTypes = _mediator.Send(new GetAllDocumentTypesQuery()).Result;

                    if (!documentTypes.Select(r => r.DocumentTypeId).Contains(value.DocumentTypeId))
                        context.AddFailure("DocumentTypeId", "Document type doesn't exist");

                    var documentType = documentTypes.FirstOrDefault(dt => dt.DocumentTypeId.Equals(value.DocumentTypeId));
                    if (documentType != null && !documentType.ActionType.Equals(value.ActionType))
                        context.AddFailure("ActionType", "Type of action doesn't exist");
                });

            RuleFor(d => d.Country)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.City)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.Region)
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(d => d.PostalCode)
                .MaximumLength(20)
                .WithMessage("{PropertyName} must not exceed 20 characters");

            RuleFor(d => d.Address)
                .MaximumLength(250)
                .WithMessage("{PropertyName} must not exceed 250 characters");

            RuleForEach(d => d.DocumentEntries)
            .SetValidator(new CreateDocumentEntryValidator());
        }
    }
}
