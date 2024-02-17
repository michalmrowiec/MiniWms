using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Contracts.Utilities;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Contractors.Queries.GetContractorById;
using miniWms.Application.Functions.Documents.Commands;
using miniWms.Application.Functions.Documents.Commands.CreateDocument;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Application.Functions.Products.Queries.GetProductById;
using miniWms.Application.Functions.Warehouses.Queries.GetWarehouseById;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Documents.Commands
{
    public class CreateDocumentCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new DocumentType()
                {
                    DocumentTypeId = "EXR",
                    ActionType = ActionType.ExternalReceipt,
                    DocumentTypeName = "External Receipt",
                    CreatedAt = new DateTime(2021, 10, 23),
                    ModifiedAt = new DateTime(2021, 10, 23)
                },

                new CreateDocumentCommand()
                {
                    DocumentTypeId = "EXR",
                    ActionType = ActionType.ExternalReceipt,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = new Guid("99000001-0000-0000-0000-100000000000"),
                    TargetWarehouseId = null,
                    IsComplited = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationComplited = new DateTime(2022, 10, 23),
                    Comments = "Some comments",
                    Country = null,
                    City = null,
                    Region = null,
                    PostalCode = null,
                    Address = null,
                    CreatedBy = null,
                    DocumentEntries = new List<CreateDocumentEntry>()
                    {
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            Quantity = 70
                        }
                    }
                },
                new Document()
                {
                    ActionType = ActionType.ExternalReceipt,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = new Guid("99000001-0000-0000-0000-100000000000"),
                    TargetWarehouseId = null,
                    IsComplited = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationComplited = new DateTime(2022, 10, 23),
                    Comments = "Some comments",
                    Country = null,
                    City = null,
                    Region = null,
                    PostalCode = null,
                    Address = null,
                    CreatedBy = null,
                    ModifiedBy = null,
                    CreatedAt = new DateTime(2022, 10, 23),
                    ModifiedAt = new DateTime(2022, 10, 23)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateDocumentCommandHandler_ForValidData_ReturnsSucced
        (DocumentType documentType, CreateDocumentCommand documentCommand, Document document)
        {
            var mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), new CancellationToken()))
                .ReturnsAsync(new ResponseBase<Warehouse>(true, ""));

            mediator.Setup(m => m.Send(It.IsAny<GetContractorByIdQuery>(), new CancellationToken()))
                .ReturnsAsync(new ResponseBase<Contractor>(true, ""));

            mediator.Setup(m => m.Send(new GetAllDocumentTypesQuery(), new CancellationToken()))
                .ReturnsAsync(new List<DocumentType>() { documentType });

            mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), new CancellationToken()))
                .ReturnsAsync(new ResponseBase<Product>(true, ""));

            var repo = new Mock<IDocumentsRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Document>()))
                .ReturnsAsync(document);

            var unitOfTransaction = new Mock<IUnitOfTransaction>();

            CreateDocumentCommandHandler handler = new(repo.Object, mediator.Object, unitOfTransaction.Object);

            ResponseBase<Document> response = await handler.Handle(documentCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(document);
        }
    }
}
