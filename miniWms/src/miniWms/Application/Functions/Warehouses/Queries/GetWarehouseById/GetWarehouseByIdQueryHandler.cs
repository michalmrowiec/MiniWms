using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Queries.GetWarehouseById
{
    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, ResponseBase<Warehouse>>
    {
        private readonly IWarehousesRepository _warehousesRepository;
        public GetWarehouseByIdQueryHandler(IWarehousesRepository warehousesRepository)
        {
            _warehousesRepository = warehousesRepository;
        }
        public async Task<ResponseBase<Warehouse>> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            Warehouse warehouse;

            try
            {
                warehouse = await _warehousesRepository.GetByIdAsync(request.WarehouseId);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Warehouse>(false, ex.Message);
            }

            return new ResponseBase<Warehouse>(warehouse);
        }
    }
}
