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
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = null,
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
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            Quantity = 70
                        }
                    }
                },
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
                    CreatedAt = new DateTime(2022, 10, 23),
                    ModifiedAt = new DateTime(2022, 10, 23)
                }
            },
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
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = null,
                    Country = "Poland",
                    City = "Bielsko-Biała",
                    Region = "Śląsk",
                    PostalCode = "43-300",
                    Address = "Leszczyńska 1500/1A",
                    CreatedBy = new Guid("00000003-0000-0000-0000-100000000000"),
                    DocumentEntries = new List<CreateDocumentEntry>()
                    {
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            Quantity = 70
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-560000000000"),
                            Quantity = 170
                        }
                    }
                },
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
                    Country = "Poland",
                    City = "Bielsko-Biała",
                    Region = "Śląsk",
                    PostalCode = "43-300",
                    Address = "Leszczyńska 1500/1A",
                    CreatedBy = new Guid("00000003-0000-0000-0000-100000000000"),
                    ModifiedBy = new Guid("00000003-0000-0000-0000-100000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new DocumentType()
                {
                    DocumentTypeId = "INI",
                    ActionType = ActionType.InternalIssue,
                    DocumentTypeName = "Internal Issue",
                    CreatedAt = new DateTime(2021, 10, 23),
                    ModifiedAt = new DateTime(2021, 10, 23)
                },

                new CreateDocumentCommand()
                {
                    DocumentTypeId = "INI",
                    ActionType = ActionType.InternalIssue,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DocumentEntries = new List<CreateDocumentEntry>()
                    {
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            Quantity = 8
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-560000000000"),
                            Quantity = 8
                        }
                    }
                },
                new Document()
                {
                    DocumentId = new Guid("12000001-0000-0000-0000-100000000000"),
                    DocumentTypeId = "INI",
                    ActionType = ActionType.InternalIssue,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    DateOfOperation = new DateTime(2022, 10, 23),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new DocumentType()
                {
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    DocumentTypeName = "Internal Transfer",
                    CreatedAt = new DateTime(2021, 10, 23),
                    ModifiedAt = new DateTime(2021, 10, 23)
                },

                new CreateDocumentCommand()
                {
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = null,
                    TargetWarehouseId = new Guid("78000002-0000-0000-0000-100000000000"),
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 25),
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
                            ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                            Quantity = 20
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-300000000000"),
                            Quantity = 1
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-400000000000"),
                            Quantity = 999999999
                        }
                    }
                },
                new Document()
                {
                    DocumentId = new Guid("12000002-0000-0000-0000-100000000000"),
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = null,
                    TargetWarehouseId = new Guid("78000002-0000-0000-0000-100000000000"),
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 25),
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

        public static IEnumerable<object[]> InvalidData => new List<object[]>
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
                    IsCompleted = false,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = null,
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
                }
            },
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
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = null,
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
                            Quantity = -1
                        },
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-100000000000"),
                            Quantity = 1
                        }
                    }
                }
            },
            new object[]
            {
                new DocumentType()
                {
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    DocumentTypeName = "Internal Transfer",
                    CreatedAt = new DateTime(2021, 10, 23),
                    ModifiedAt = new DateTime(2021, 10, 23)
                },

                new CreateDocumentCommand()
                {
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = null,
                    TargetWarehouseId = new Guid("78000002-0000-0000-0000-100000000000"),
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 22),
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
                            ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                            Quantity = 20
                        }
                    }
                }
            },
            new object[]
            {
                new DocumentType()
                {
                    DocumentTypeId = "INT",
                    ActionType = ActionType.InternalTransfer,
                    DocumentTypeName = "Internal Transfer",
                    CreatedAt = new DateTime(2021, 10, 23),
                    ModifiedAt = new DateTime(2021, 10, 23)
                },

                new CreateDocumentCommand()
                {
                    DocumentTypeId = "INR",
                    ActionType = ActionType.InternalReceipt,
                    MainWarehouseId = new Guid("78000001-0000-0000-0000-100000000000"),
                    ContractorId = null,
                    TargetWarehouseId = new Guid("78000002-0000-0000-0000-100000000000"),
                    IsCompleted = true,
                    DateOfOperation = new DateTime(2022, 10, 23),
                    DateOfOperationCompleted = new DateTime(2022, 10, 22),
                    Comments = "Some comments",
                    DocumentEntries = new List<CreateDocumentEntry>()
                    {
                        new CreateDocumentEntry()
                        {
                            ProductId =  new Guid("14000001-0000-0000-0000-200000000000"),
                            Quantity = 20
                        }
                    }
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateDocumentCommandHandler_ForInValidData_ReturnsErrors
            (DocumentType documentType, CreateDocumentCommand documentCommand)
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
                .ReturnsAsync(new Document());

            var unitOfTransaction = new Mock<IUnitOfTransaction>();

            CreateDocumentCommandHandler handler = new(repo.Object, mediator.Object, unitOfTransaction.Object);

            ResponseBase<Document> response = await handler.Handle(documentCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
