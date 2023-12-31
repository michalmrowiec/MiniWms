using miniWms.Application.Contracts;
using miniWms.Application.Functions.Categories.Queries.GetAllCategories;
using miniWms.Domain.Entities;
using Sieve.Models;

namespace miniWms.UnitTests.Application.Products.Queries
{
    public class GetSortedAndFilteredProductsQueryHandlerTests
    {
        //public static IEnumerable<object[]> ValidData => new List<object[]>
        //{
        //    new object[]
        //    {
        //        new List<Product> ()
        //        {
        //            new()
        //            {
        //                ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
        //                ProductName = "Prod1",
        //                ProductDescription = "ProdDesc1",
        //                CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                Category = new()
        //                {
        //                    CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                    CategoryName = "CategoryTest1",
        //                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    CreatedAt = new DateTime(2021, 10, 9),
        //                    ModifiedAt = new DateTime(2021, 11, 23)
        //                },
        //                IsWeight = false,
        //                Unit = "szt",
        //                CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                CreatedAt = new DateTime(2022, 10, 9),
        //                ModifiedAt = new DateTime(2022, 11, 21)
        //            },
        //            new()
        //            {
        //                ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
        //                ProductName = "Prod2",
        //                ProductDescription = "ProdDesc2",
        //                CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                Category = new()
        //                {
        //                    CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                    CategoryName = "CategoryTest1",
        //                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    CreatedAt = new DateTime(2021, 10, 9),
        //                    ModifiedAt = new DateTime(2021, 11, 23)
        //                },
        //                IsWeight = false,
        //                Unit = "szt",
        //                CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                CreatedAt = new DateTime(2022, 10, 9),
        //                ModifiedAt = new DateTime(2022, 11, 21)
        //            },
        //            new()
        //            {
        //                ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
        //                ProductName = "Prod3",
        //                ProductDescription = "ProdDesc3",
        //                CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
        //                Category = new()
        //                {
        //                    CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
        //                    CategoryName = "CategoryTest2",
        //                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    CreatedAt = new DateTime(2021, 10, 9),
        //                    ModifiedAt = new DateTime(2021, 11, 23)
        //                },
        //                IsWeight = true,
        //                Unit = "kg",
        //                CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                CreatedAt = new DateTime(2022, 10, 9),
        //                ModifiedAt = new DateTime(2022, 11, 21)
        //            },
        //        }
        //    },
        //    new object[]
        //    {
        //        new List<Product> ()
        //    },
        //    new object[]
        //    {
        //        new List<Product> ()
        //        {
        //            new()
        //            {
        //                ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
        //                ProductName = "Prod1",
        //                ProductDescription = "ProdDesc1",
        //                CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                Category = new()
        //                {
        //                    CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
        //                    CategoryName = "CategoryTest1",
        //                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                    CreatedAt = new DateTime(2021, 10, 9),
        //                    ModifiedAt = new DateTime(2021, 11, 23)
        //                },
        //                IsWeight = false,
        //                Unit = "szt",
        //                CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
        //                CreatedAt = new DateTime(2022, 10, 9),
        //                ModifiedAt = new DateTime(2022, 11, 21)
        //            },
        //        }
        //    }

        //};

        //[Theory]
        //[MemberData(nameof(ValidData))]
        //public async Task GetSortedAndFilteredProductsQueryHandler_ForValidData_ReturnsListOfProducts(List<Product> products)
        //{
        //    var repo = new Mock<IProductsRepository>();
        //    repo.Setup(m => m.GetSortedAndFilteredProductsAsync(It.IsAny<SieveModel>()))
        //        .ReturnsAsync(products);

        //    GetAllCategoriesQueryHandler handler = new(repo.Object);

        //    var response = await handler.Handle(new GetAllCategoriesQuery(), new CancellationToken());

        //    response.Should().NotBeNull().And.BeEquivalentTo(categories);
        //}
    }
}
