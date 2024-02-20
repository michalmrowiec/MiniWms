using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse
{
    public class GetWarehouseEntriesQueryHandler : IRequestHandler<GetWarehouseEntriesQuery, IList<WarehouseEntry>>
    {
        private readonly IWarehouseEntriesRepository _warehouseEntriesRepository;

        public GetWarehouseEntriesQueryHandler(IWarehouseEntriesRepository warehouseEntriesRepository)
        {
            _warehouseEntriesRepository = warehouseEntriesRepository;
        }

        public async Task<IList<WarehouseEntry>> Handle(GetWarehouseEntriesQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseEntriesRepository.GetForWarehouseAsync(request.warehouseId);
        }
    }
}
