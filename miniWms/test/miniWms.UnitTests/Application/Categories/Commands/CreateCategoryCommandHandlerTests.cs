using miniWms.Application.Contracts;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Categories.Commands.CreateCategory;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Categories.Commands
{
    public class CreateCategoryCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateCategoryCommand()
                {
                    CategoryName = "CategoryTest1",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = "CategoryTest1",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new CreateCategoryCommand()
                {
                    CategoryName = "CategoryTest2",
                },
                new Category()
                {
                    CategoryId = Guid.NewGuid(),
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
        public async Task CreateCategoryCommandHandler_ForValidData_ReturnsSucced(CreateCategoryCommand categoryCommand, Category category)
        {
            var repo = new Mock<ICategoriesRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync(category);

            CreateCategoryCommandHandler handler = new(repo.Object);

            ResponseBase<Category> response = (ResponseBase<Category>)await handler.Handle(categoryCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(category);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new CreateCategoryCommand()
                {
                    CategoryName = "",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateCategoryCommandHandler_ForInvalidData_ReturnsErrors(CreateCategoryCommand categoryCommand)
        {
            var repo = new Mock<ICategoriesRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync(new Category());

            CreateCategoryCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(categoryCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
