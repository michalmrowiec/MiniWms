using Microsoft.Extensions.Logging;
using miniWms.Domain.Entities;
using miniWms.Infrastructure.Repositories;
using miniWms.UnitTests.Infrastructure.Helper;

namespace miniWms.UnitTests.Infrastructure
{
    public class CategoryRepositoryTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("10000000-0000-0000-0000-100000000000"),
                    CategoryName = "CategoryTest1",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("20000000-0000-0000-0000-100000000000"),
                    CategoryName = "CategoryTest2",
                    CreatedBy = null,
                    ModifiedBy = null,
                    CreatedAt = new DateTime(2023, 10, 12),
                    ModifiedAt = new DateTime(2023, 10, 12)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CategoryReppositoryCreate_ForValidData_CreatesCategories(Category category)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            var logger = new Mock<ILogger<CategoryRepository>>();

            var categoryRepository = new CategoryRepository(context, logger.Object);

            await categoryRepository.CreateAsync(category);

            var addedCategory = await context.Set<Category>().FindAsync(category.CategoryId);

            addedCategory.Should().NotBeNull();
            addedCategory.Should().BeEquivalentTo(category);
        }
    }
}
