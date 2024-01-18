using miniWms.Application.Contracts;
using miniWms.Application.Functions.Categories.Queries.GetAllCategories;
using miniWms.Application.Functions.DocumentTypes.Queries.GetAllDocumentTypes;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.DocumentTypes.Queries
{
    public class GetAllDocumentTypesQueryHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<DocumentType> ()
                {
                    new DocumentType()
                    {
                        DocumentTypeId = "PZP",
                        DocumentTypeName = "Document Type Test 1",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 23),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    },
                    new DocumentType()
                    {
                        DocumentTypeId = "MMZ",
                        DocumentTypeName = "Document Type Test 2",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 8, 23),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    }
                }
            },
            new object[]
            {
                new List<DocumentType> ()
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task GetAllDocumentTypesHandler_ForValidData_ReturnsSuccedWithListOfDocumentTypes(List<DocumentType> documentTypes)
        {
            var repo = new Mock<IDocumentTypesRepository>();
            repo.Setup(m => m.GetAllAsync())
                .ReturnsAsync(documentTypes);

            GetAllDocumentTypesQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetAllDocumentTypesQuery(), new CancellationToken());

            response.Should().NotBeNull().And.BeEquivalentTo(documentTypes);
        }
    }
}
