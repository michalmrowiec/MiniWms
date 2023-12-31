using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, WarehouseResponse>
    {
        private readonly IWarehousesRepository _warehouseRepository;
        public CreateWarehouseCommandHandler(IWarehousesRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<WarehouseResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            CreateWarehouseValidator validator = new();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new WarehouseResponse(validationResult);
            }

            var newWarehouse = new Warehouse()
            {
                WarehouseName = request.WarehouseName,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };

            var createdWarehouse = await _warehouseRepository.CreateAsync(newWarehouse);

            return new WarehouseResponse(createdWarehouse);
        }
    }
}
