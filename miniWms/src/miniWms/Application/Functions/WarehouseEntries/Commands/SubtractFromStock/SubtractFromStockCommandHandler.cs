using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock
{
    public class SubtractFromStockCommandHandler : IRequestHandler<SubtractFromStockCommand, ResponseBase<List<WarehouseEntry>>>
    {
        private readonly IWarehouseEntriesRepository _warehouseEntriesRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public SubtractFromStockCommandHandler(IWarehouseEntriesRepository warehouseEntriesRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _warehouseEntriesRepository = warehouseEntriesRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<List<WarehouseEntry>>> Handle(SubtractFromStockCommand request, CancellationToken cancellationToken)
        {
            List<WarehouseEntry> stockToCreate = [];
            List<WarehouseEntry> stockToUpdate = [];

            var warehouseStock = await _mediator.Send(new GetForWarehouseQuery(request.WarehouseId));

            foreach (var de in request.DocumentEntries)
            {
                var prodStock = warehouseStock.FirstOrDefault(we => we.ProductId.Equals(de.ProductId));

                if (prodStock != null)
                {
                    prodStock.Quantity -= de.Quantity;
                    stockToUpdate.Add(prodStock);
                }
                else
                {
                    stockToCreate.Add(new WarehouseEntry()
                    {
                        ProductId = de.ProductId,
                        Quantity = 0 -de.Quantity,
                        WarehouseId = request.WarehouseId,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow
                    });
                }
            }

            var resultOfCreate = await _warehouseEntriesRepository.CreateRangeAsync(stockToCreate);
            var resultOfUpdate = await _warehouseEntriesRepository.UpdateRangeAsync(stockToUpdate);

            return new ResponseBase<List<WarehouseEntry>>(resultOfCreate.Concat(resultOfUpdate).ToList());
        }
    }
}
