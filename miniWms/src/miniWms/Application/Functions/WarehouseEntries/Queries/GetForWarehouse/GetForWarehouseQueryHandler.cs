using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse
{
    public class GetForWarehouseQueryHandler : IRequestHandler<GetForWarehouseQuery, IList<WarehouseEntry>>
    {
        private readonly IWarehouseEntriesRepository _warehouseEntriesRepository;

        public GetForWarehouseQueryHandler(IWarehouseEntriesRepository warehouseEntriesRepository)
        {
            _warehouseEntriesRepository = warehouseEntriesRepository;
        }

        public async Task<IList<WarehouseEntry>> Handle(GetForWarehouseQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseEntriesRepository.GetForWarehouseAsync(request.warehouseId);
        }
    }
}
