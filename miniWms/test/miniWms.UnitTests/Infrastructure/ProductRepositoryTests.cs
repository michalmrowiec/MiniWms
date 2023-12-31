using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using miniWms.Domain.Entities;
using miniWms.Infrastructure;
using miniWms.Infrastructure.Repositories;
using miniWms.UnitTests.Infrastructure.Helper;
using Sieve.Models;

namespace miniWms.UnitTests.Infrastructure.Repositories
{
    public class ProductRepositoryTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        CategoryName = "CategoryTest1",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    },
                    new()
                    {
                        CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                        CategoryName = "CategoryTest2",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    }
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                },
                new SieveModel()
                {
                    Filters = "",
                    Page = 1,
                    PageSize = 10,
                    Sorts = "-productName"
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                        Category = new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                            CategoryName = "CategoryTest2",
                            CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = true,
                        Unit = "kg",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        Category = new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            CategoryName = "CategoryTest1",
                            CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        Category = new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            CategoryName = "CategoryTest1",
                            CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                }
            },
            new object[] {
                new List<Category>(),
                new List<Product>(),
                new SieveModel()
                {
                    Filters = "",
                    Page = 1,
                    PageSize = 10,
                    Sorts = ""},
                new List<Product>()
            },
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        CategoryName = "CategoryTest1",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    }
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "",
                    Page = 1,
                    PageSize = 10,
                    Sorts = ""
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        Category = new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            CategoryName = "CategoryTest1",
                            CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = false,
                        Unit = "szt",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task TestGetSortedAndFilteredProductsAsync_ForValidData_ReturnsSertedAdnFilteredListOfProducts(
            List<Category> categories,
            List<Product> products,
            SieveModel sieveModel,
            List<Product> expectedResult)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            await context
                .Set<Category>()
                .AddRangeAsync(categories);

            await context
                .Set<Product>()
                .AddRangeAsync(products);

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<ProductRepository>>();

            IOptions<SieveOptions> optionsParameter = Options.Create(new SieveOptions());

            ProductRepository productRepository = new(context, logger.Object, new MiniWmsSieveProcessor(optionsParameter));

            var response = await productRepository.GetSortedAndFilteredProductsAsync(sieveModel);

            foreach (var i in response.Items)
            {
                i.Category!.Products = [];
            }

            response
                .Should()
                .NotBeNull();

            response.Items
                .Should()
                .BeEquivalentTo(
                    expectedResult,
                    options => options.WithStrictOrdering());
        }
    }
}
