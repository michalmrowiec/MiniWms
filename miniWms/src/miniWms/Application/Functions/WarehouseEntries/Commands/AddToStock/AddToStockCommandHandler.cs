using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock
{
    public class AddToStockCommandHandler : IRequestHandler<AddToStockCommand, ResponseBase<List<WarehouseEntry>>>
    {
        private readonly IWarehouseEntriesRepository _warehouseEntriesRepository;
        private readonly IMediator _mediator;
        private readonly ITransactionManager _unitOfWork;
        public AddToStockCommandHandler(IWarehouseEntriesRepository warehouseEntriesRepository, IMediator mediator, ITransactionManager unitOfWork)
        {
            _warehouseEntriesRepository = warehouseEntriesRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<List<WarehouseEntry>>> Handle(AddToStockCommand request, CancellationToken cancellationToken)
        {
            List<WarehouseEntry> stockToCreate = [];
            List<WarehouseEntry> stockToUpdate = [];

            var warehouseStock = await _mediator.Send(new GetForWarehouseQuery(request.WarehouseId));

            foreach (var de in request.DocumentEntries)
            {
                var prodStock = warehouseStock.FirstOrDefault(we => we.ProductId.Equals(de.ProductId));

                if (prodStock != null)
                {
                    prodStock.Quantity += de.Quantity;
                    stockToUpdate.Add(prodStock);
                }
                else
                {
                    stockToCreate.Add(new WarehouseEntry()
                    {
                        ProductId = de.ProductId,
                        Quantity = de.Quantity,
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
