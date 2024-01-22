using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using miniWms.Application.Contracts;
using miniWms.Application.Functions;
using miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Application.Functions.Roles.Queries.GetAllRoles;
using miniWms.Domain.Entities;
using System.Reflection.Metadata;

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
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new DocumentType()
                {
                    DocumentTypeId = "PZP",
                    DocumentTypeName = "Test doc type 1",
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
                    DocumentTypeName = "Test doc type 2"
                },
                new DocumentType()
                {
                    DocumentTypeId = "PZN",
                    DocumentTypeName = "Test doc type 2",
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
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
                        DocumentTypeName = "IPZ test test",
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
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
                        DocumentTypeName = "IPZ test",
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
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new List<DocumentType>()
                {
                    new DocumentType
                    {
                        DocumentTypeId = "IPZ",
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
