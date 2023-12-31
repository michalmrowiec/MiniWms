using miniWms.Application.Contracts;
using miniWms.Application.Functions.Categories.Queries.GetAllCategories;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Categories.Queries
{
    public class GetAllCategoriesQueryHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<Category> ()
                {
                    new Category()
                    {
                        CategoryId = Guid.NewGuid(),
                        CategoryName = "CategoryTest1",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2023, 10, 23),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    },
                    new Category()
                    {
                        CategoryId = Guid.NewGuid(),
                        CategoryName = "CategoryTest12",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2023, 10, 23),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    }
                }
            },
            new object[]
            {
                new List<Category> ()
            },
            new object[]
            {
                new List<Category> ()
                {
                    new Category()
                    {
                        CategoryId = Guid.NewGuid(),
                        CategoryName = "CategoryTest1",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2023, 10, 23),
                        ModifiedAt = new DateTime(2023, 10, 23)
                    }
                }
            }

        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task GetAllCategoriesHandler_ForValidData_ReturnsSuccedWithListOfCategories(List<Category> categories)
        {
            var repo = new Mock<ICategoriesRepository>();
            repo.Setup(m => m.GetAllAsync())
                .ReturnsAsync(categories);

            GetAllCategoriesQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetAllCategoriesQuery(), new CancellationToken());

            response.Should().NotBeNull().And.BeEquivalentTo(categories);
        }
    }
}
