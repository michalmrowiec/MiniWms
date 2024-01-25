using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType
{
    public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ResponseBase<Contractor>>
    {
        private readonly IContractorsRepository _contractorsRepository;

        public CreateContractorCommandHandler(IContractorsRepository contractorsRepository)
        {
            _contractorsRepository = contractorsRepository;
        }

        public async Task<ResponseBase<Contractor>> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateContractorValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new ResponseBase<Contractor>(validatorResult);
            }

            var newContractor = new Contractor
            {
                ContractorName = request.ContractorName,
                VatId = request.VatId,
                IsSupplier = request.IsSupplier,
                IsRecipient = request.IsRecipient,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            var createdDocumentType = await _contractorsRepository.CreateAsync(newContractor);

            return new ResponseBase<Contractor>(createdDocumentType);
        }
    }
}
