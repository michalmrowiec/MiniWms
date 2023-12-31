using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Queries.GetAllWarehouses
{
    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, IList<Warehouse>>
    {
        private readonly IWarehousesRepository _warehouseRepository;
        public GetAllWarehousesQueryHandler(IWarehousesRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IList<Warehouse>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}
