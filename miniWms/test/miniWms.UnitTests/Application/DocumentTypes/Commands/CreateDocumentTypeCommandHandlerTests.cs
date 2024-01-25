using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions;
using miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.DocumentTypes.Commands
{
    public class CreateDocumentTypeCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "PZP",
                    DocumentTypeName = "Test doc type 1",
                    ActionType = ActionType.ExternalReceipt,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new DocumentType()
                {
                    DocumentTypeId = "PZP",
                    DocumentTypeName = "Test doc type 1",
                    ActionType = ActionType.ExternalReceipt,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "PZN",
                    ActionType = ActionType.InternalTransfer,
                    DocumentTypeName = "Test doc type 2"
                },
                new DocumentType()
                {
                    DocumentTypeId = "PZN",
                    DocumentTypeName = "Test doc type 2",
                    ActionType = ActionType.InternalTransfer,
                    CreatedBy = null,
                    ModifiedBy = null,
                    CreatedAt = new DateTime(2023, 10, 12),
                    ModifiedAt = new DateTime(2023, 10, 12)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateDocumentTypeCommandHandler_ForValidData_ReturnsSucced
            (CreateDocumentTypeCommand documentTypeCommand, DocumentType documentType)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetAllDocumentTypesQuery(), new CancellationToken()))
                .ReturnsAsync(new List<DocumentType>());

            var repo = new Mock<IDocumentTypesRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<DocumentType>()))
                .ReturnsAsync(documentType);

            CreateDocumentTypeCommandHandler handler = new(repo.Object, mediator.Object);

            ResponseBase<DocumentType> response = await handler.Handle(documentTypeCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(documentType);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "",
                    DocumentTypeName = "",
                    ActionType = ActionType.InternalIssue,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "",
                    DocumentTypeName = "Test name of doc type 1",
                    ActionType = ActionType.ExternalIssue,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "PDA",
                    DocumentTypeName = "",
                    ActionType = ActionType.InternalIssue,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "PD",
                    DocumentTypeName = "Test doc",
                    ActionType = ActionType.InternalReceipt,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "P",
                    DocumentTypeName = "Test doc 231",
                    ActionType = ActionType.InternalTransfer,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "PPZD",
                    DocumentTypeName = "Test doc test 42",
                    ActionType = ActionType.InternalIssue,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "IPZ",
                    DocumentTypeName = "IPZ test",
                    ActionType = ActionType.InternalTransfer,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
                        DocumentTypeName = "IPZ test test",
                        ActionType = ActionType.InternalTransfer,
                        CreatedBy = null,
                        ModifiedBy = null,
                        CreatedAt = new DateTime(2023, 10, 12),
                        ModifiedAt = new DateTime(2023, 10, 12)
                    }
                }
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "IIP",
                    DocumentTypeName = "IPZ test",
                    ActionType = ActionType.InternalIssue,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
                        DocumentTypeName = "IPZ test",
                        ActionType = ActionType.ExternalIssue,
                        CreatedBy = null,
                        ModifiedBy = null,
                        CreatedAt = new DateTime(2023, 10, 12),
                        ModifiedAt = new DateTime(2023, 10, 12)
                    }
                }
            },
            new object[]
            {
                new CreateDocumentTypeCommand()
                {
                    DocumentTypeId = "IPZ",
                    DocumentTypeName = "IPZ test",
                    ActionType = ActionType.ExternalReceipt,
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
                        ActionType = ActionType.InternalTransfer,
                        DocumentTypeName = "IPZ test",
                        CreatedBy = null,
                        ModifiedBy = null,
                        CreatedAt = new DateTime(2023, 10, 12),
                        ModifiedAt = new DateTime(2023, 10, 12)
                    }
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateDocumentTypeCommandHandler_ForInvalidData_ReturnsErrors
            (CreateDocumentTypeCommand documentTypeCommand, List<DocumentType> existingDocumentTypes)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetAllDocumentTypesQuery(), new CancellationToken()))
                .ReturnsAsync(existingDocumentTypes);

            var repo = new Mock<IDocumentTypesRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<DocumentType>()))
                .ReturnsAsync(new DocumentType());

            CreateDocumentTypeCommandHandler handler = new(repo.Object, mediator.Object);

            var response = await handler.Handle(documentTypeCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
