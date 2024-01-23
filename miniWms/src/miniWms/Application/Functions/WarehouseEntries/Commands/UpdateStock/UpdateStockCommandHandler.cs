using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse;
using miniWms.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using miniWms.Domain.Entities;
using System.Reflection.Metadata;

namespace miniWms.Application.Functions.WarehouseEntries.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, ResponseBase<List<WarehouseEntry>>>
    {
        private readonly IWarehouseEntriesRepository _warehouseEntriesRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStockCommandHandler(IWarehouseEntriesRepository warehouseEntriesRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _warehouseEntriesRepository = warehouseEntriesRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<List<WarehouseEntry>>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            List<WarehouseEntry> stockToCreate = [];
            List<WarehouseEntry> stockToUpdate = [];

            _ = Guid.TryParse(request.Document.SupplierId.ToString(), out Guid supId);
            _ = Guid.TryParse(request.Document.RecipientId.ToString(), out Guid recId);

            var sup = await _mediator.Send(new GetForWarehouseQuery(supId));
            var rec = await _mediator.Send(new GetForWarehouseQuery(recId));

            var warehouses = await _mediator.Send(new GetAllWarehousesQuery());
            var warehousesId = warehouses.Select(w => w.WarehouseId).ToList();

            if (warehousesId.Contains(supId))
            {
                foreach (var de in request.Document.DocumentEntries)
                {
                    var prodStock = sup.FirstOrDefault(we => we.ProductId.Equals(de.ProductId));

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
                            Quantity = 0 - de.Quantity,
                            WarehouseId = supId,
                            CreatedAt = DateTime.UtcNow,
                            ModifiedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            if (warehousesId.Contains(recId))
            {
                foreach (var de in request.Document.DocumentEntries)
                {
                    var prodStock = rec.FirstOrDefault(we => we.ProductId.Equals(de.ProductId));

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
                            WarehouseId = supId,
                            CreatedAt = DateTime.UtcNow,
                            ModifiedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            var resultOfCreate = await _warehouseEntriesRepository.CreateRangeAsync(stockToCreate);
            var resultOfUpdate = await _warehouseEntriesRepository.UpdateRangeAsync(stockToUpdate);

            return new ResponseBase<List<WarehouseEntry>>(resultOfCreate.Concat(resultOfUpdate).ToList());
        }
    }
}
