using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Documents.Commands.DeleteDocument;
using miniWms.Application.Functions.Documents.Queries.GetDocumentById;
using miniWms.Application.Functions.WarehouseEntries.Commands.AddToStock;
using miniWms.Application.Functions.WarehouseEntries.Commands.SubtractFromStock;
using miniWms.Application.Functions.WarehouseEntries.Queries.GetForWarehouse;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Documents.Commands
{
    public class DeleteDocumentCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new Guid("12000001-0000-0000-0000-100000000000"),
                new Document()
                {
                    DocumentId = new Guid("12000001-0000-0000-0000-100000000000"),
                    DocumentTypeId = "EXR",
                    ActionType = ActionType.ExternalReceipt,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = new Guid("99000001-0000-0000-0000-100000000000"),
                    TargetWarehouseId = null,
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 23),
                    Comments = "Some comments",
                    Country = null,
                    City = null,
                    Region = null,
                    PostalCode = null,
                    Address = null,
                    CreatedBy = null,
                    ModifiedBy = null,
                    DocumentEntries = new List<DocumentEntry>()
                    {
                        new DocumentEntry()
                        {
                            DocumentEntryId = new Guid("78000001-0000-0000-0000-100000000000"),
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            DocumentId = new Guid("12000001-0000-0000-0000-100000000000"),
                            Quantity = 70,
                            CreatedAt = new DateTime(2022, 10, 23),
                            ModifiedAt = new DateTime(2022, 10, 23)
                        }
                    },
                    CreatedAt = new DateTime(2022, 10, 23),
                    ModifiedAt = new DateTime(2022, 10, 23)
                },
                new List<WarehouseEntry>() // startMainWarehouse
                {
                    new WarehouseEntry()
                    {
                        WarehouseEntryId = new Guid("08600001-0000-0000-0000-100000000000"),
                        ProductId = new Guid("14000001-0000-0000-0000-100000000000"),
                        WarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                        Quantity = 90,
                        CreatedAt = new DateTime(2022, 05, 04),
                        ModifiedAt = new DateTime(2022, 06, 17)
                    }
                },
                new List<WarehouseEntry>(), // startTargetWarehouse
                new List<WarehouseEntry>(), // AddCreateRangeAsyncReturns
                new List<WarehouseEntry>(),  // AddUpdateRangeAsyncReturns
                new List<WarehouseEntry>(), // SubtractCreateRangeAsyncReturns
                new List<WarehouseEntry>()  // SubtractUpdateRangeAsyncReturns
                {
                    new WarehouseEntry()
                    {
                        WarehouseEntryId = new Guid("08600001-0000-0000-0000-100000000000"),
                        ProductId = new Guid("14000001-0000-0000-0000-100000000000"),
                        WarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                        Quantity = 70,
                        CreatedAt = new DateTime(2022, 05, 04),
                        ModifiedAt = new DateTime(2022, 10, 23)
                    }
                }
            },
            new object[]
            {
                new Guid("12000001-0000-0000-0000-100000000000"),
                new Document()
                {
                    DocumentId = new Guid("12000001-0000-0000-0000-100000000000"),
                    DocumentTypeId = "EXI",
                    ActionType = ActionType.ExternalIssue,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = new Guid("99000001-0000-0000-0000-100000000000"),
                    TargetWarehouseId = null,
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 23),
                    Comments = "Some comments",
                    Country = "Poland",
                    City = "Bielsko-Biała",
                    Region = "Śląsk",
                    PostalCode = "43-300",
                    Address = "Leszczyńska 1500/1A",
                    DocumentEntries = new List<DocumentEntry>()
                    {
                        new DocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                            Quantity = 8
                        }
                    },
                    CreatedBy = new Guid("00000003-0000-0000-0000-100000000000"),
                    ModifiedBy = new Guid("00000003-0000-0000-0000-100000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                },
                new List<WarehouseEntry>() // startMainWarehouse
                {
                    new WarehouseEntry()
                    {
                        WarehouseEntryId = new Guid("08600001-0000-0000-0000-200000000000"),
                        ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                        WarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                        Quantity = 1,
                        CreatedAt = new DateTime(2022, 05, 08),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    }
                },
                new List<WarehouseEntry>(), // startTargetWarehouse
                new List<WarehouseEntry>(), // AddCreateRangeAsyncReturns
                new List<WarehouseEntry>()  // AddUpdateRangeAsyncReturns
                {
                    new WarehouseEntry()
                    {
                        WarehouseEntryId = new Guid("08600001-0000-0000-0000-200000000000"),
                        ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                        WarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                        Quantity = 9,
                        CreatedAt = new DateTime(2022, 05, 08),
                        ModifiedAt = new DateTime(2022, 10, 23)
                    }
                },
                new List<WarehouseEntry>(), // SubtractCreateRangeAsyncReturns
                new List<WarehouseEntry>()  // SubtractUpdateRangeAsyncReturns
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task DeleteDocumentCommandHandler_ForExistDocument_ReturnsSucced
        (Guid documentId, Document document,
            List<WarehouseEntry> startMaianWarehouseEntries, List<WarehouseEntry> startTargetWarehouseEntries,
            List<WarehouseEntry> returnsOfAddCreateRangeAsync, List<WarehouseEntry> returnsOfAddUpdateRangeAsync,
            List<WarehouseEntry> returnsOfSubtractCreateRangeAsync, List<WarehouseEntry> returnsOfSubtractUpdateRangeAsync)
        {
            var mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<GetDocumentByIdWithEntriesQuery>(), new CancellationToken()))
                .ReturnsAsync(new ResponseBase<Document>(document));

            mediator.Setup(m => m.Send(new GetWarehouseEntriesQuery(document.MainWarehouseId), new CancellationToken()))
                .ReturnsAsync(startMaianWarehouseEntries);



            if (document.TargetWarehouseId != null)
                mediator.Setup(m => m.Send(new GetWarehouseEntriesQuery((Guid)document.TargetWarehouseId), new CancellationToken()))
                    .ReturnsAsync(startTargetWarehouseEntries);

            var repo = new Mock<IDocumentsRepository>();

            repo.Setup(m => m.DeleteAsync(It.IsAny<Document>()))
                .ReturnsAsync(true);

            var weRepo = new Mock<IWarehouseEntriesRepository>();

            var unitOfTransaction = new Mock<ITransactionManager>();

            if (document.ActionType.Equals(ActionType.InternalTransfer) ||
                document.ActionType.Equals(ActionType.InternalIssue) ||
                document.ActionType.Equals(ActionType.ExternalIssue))
            {
                weRepo.Setup(m => m.CreateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                    .ReturnsAsync(returnsOfAddCreateRangeAsync);
                weRepo.Setup(m => m.UpdateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                    .ReturnsAsync(returnsOfAddUpdateRangeAsync);

                var addToStockCommandHandlerMainWarehouse = new AddToStockCommandHandler(weRepo.Object, mediator.Object, unitOfTransaction.Object);
                var addToStockCommandResultMainWarehouse = await addToStockCommandHandlerMainWarehouse.Handle(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), new CancellationToken());
                mediator.Setup(m => m.Send(new AddToStockCommand(document.MainWarehouseId, document.DocumentEntries), new CancellationToken()))
                                    .ReturnsAsync(addToStockCommandResultMainWarehouse);
            }
            else
            {
                weRepo.Setup(m => m.CreateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                .ReturnsAsync(returnsOfSubtractCreateRangeAsync);
                weRepo.Setup(m => m.UpdateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                    .ReturnsAsync(returnsOfSubtractUpdateRangeAsync);

                var subtractFromStockCommandHandlerMainWarehouse = new SubtractFromStockCommandHandler(weRepo.Object, mediator.Object, unitOfTransaction.Object);
                var subtractFromStockCommandResultMainWarehouse = await subtractFromStockCommandHandlerMainWarehouse.Handle(new SubtractFromStockCommand(document.MainWarehouseId, document.DocumentEntries), new CancellationToken());
                mediator.Setup(m => m.Send(new SubtractFromStockCommand(document.MainWarehouseId, document.DocumentEntries), new CancellationToken()))
                        .ReturnsAsync(subtractFromStockCommandResultMainWarehouse);
            }

            if (document.ActionType.Equals(ActionType.InternalTransfer) && document.IsCompleted)
            {
                weRepo.Setup(m => m.CreateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                .ReturnsAsync(returnsOfSubtractCreateRangeAsync);
                weRepo.Setup(m => m.UpdateRangeAsync(It.IsAny<List<WarehouseEntry>>()))
                    .ReturnsAsync(returnsOfSubtractUpdateRangeAsync);

                var subtractFromStockCommandHandlerTargetWarehouse = new SubtractFromStockCommandHandler(weRepo.Object, mediator.Object, unitOfTransaction.Object);
                var subtractFromStockCommandResultTargetWarehouse = await subtractFromStockCommandHandlerTargetWarehouse.Handle(new SubtractFromStockCommand((Guid)document.TargetWarehouseId, document.DocumentEntries), new CancellationToken());

                mediator.Setup(m => m.Send(new SubtractFromStockCommand((Guid)document.TargetWarehouseId, document.DocumentEntries), new CancellationToken()))
                        .ReturnsAsync(subtractFromStockCommandResultTargetWarehouse);
            }

            DeleteDocumentCommandHandler deleteDocumentHandler = new(repo.Object, mediator.Object, unitOfTransaction.Object);

            ResponseBase response = await deleteDocumentHandler
                .Handle(new DeleteDocumentCommand(documentId), new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
        }
    }
}
