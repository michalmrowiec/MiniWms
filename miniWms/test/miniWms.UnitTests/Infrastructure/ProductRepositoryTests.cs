using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using miniWms.Infrastructure;
using miniWms.Infrastructure.Repositories;
using miniWms.UnitTests.Infrastructure.Helper;
using Sieve.Models;

namespace miniWms.UnitTests.Infrastructure.Repositories
{
    public class ProductRepositoryTests
    {
        public static IEnumerable<object[]> ValidDataForGetSortedAndFilteredProductsAsync => new List<object[]>
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
                    }
                },
                new SieveModel()
                {
                    Filters = "",
                    Page = 2,
                    PageSize = 2,
                    Sorts = "-productName"
                },
                new PagedResult<Product>(
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
                    }, 3, 2, 2)
            },
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        CategoryName = "CategoryTest1",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    },
                    new()
                    {
                        CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                        CategoryName = "CategoryTest2",
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
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "",
                    Page = 1,
                    PageSize = 2,
                    Sorts = "-productName"
                },
                new PagedResult<Product>(
                    new List<Product>()
                    {
                    new()
                    {
                        ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                        Category =
                        new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-2000-122000000000"),
                            CategoryName = "CategoryTest2",
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = true,
                        Unit = "kg",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        Category =
                        new()
                        {
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            CategoryName = "CategoryTest1",
                            CreatedAt = new DateTime(2021, 10, 9),
                            ModifiedAt = new DateTime(2021, 11, 23)
                        },
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                        }
                    }, 3, 2, 1)
            },
            new object[] {
                new List<Category>(),
                new List<Product>(),
                new SieveModel()
                {
                    Filters = "",
                    Page = 1,
                    PageSize = 10,
                    Sorts = ""
                },
                new PagedResult<Product>(new List<Product>(), 0, 10, 1)
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
                new PagedResult<Product>(
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
                    }, 1, 10, 1)
            },
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        CategoryName = "Cat1",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    },
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        CategoryName = "Cat2",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    }
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-100000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-300000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "categoryName@=*Cat1",
                    Page = 1,
                    PageSize = 10,
                    Sorts = "categoryName"
                },
                new PagedResult<Product>(
                    new List<Product>()
                    {
                        new()
                        {
                            ProductId = new Guid("00000000-0000-0000-0000-300000000000"),
                            ProductName = "Prod3",
                            ProductDescription = "ProdDesc3",
                            CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                            Category =
                            new()
                            {
                                CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                                CategoryName = "Cat1",
                                CreatedAt = new DateTime(2021, 10, 9),
                                ModifiedAt = new DateTime(2021, 11, 23)
                            },
                            IsWeight = true,
                            Unit = "kg",
                            CreatedAt = new DateTime(2022, 10, 9),
                            ModifiedAt = new DateTime(2022, 11, 21)
                        }
                    }, 1, 10, 1)
            },
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        CategoryName = "Cat1",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    },
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        CategoryName = "Cat2",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    }
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-100000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-300000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "categoryName@=*cat2,productName==Prod1",
                    Page = 1,
                    PageSize = 25,
                    Sorts = "-categoryName"
                },
                new PagedResult<Product>(
                    new List<Product>()
                    {
                        new()
                        {
                            ProductId = new Guid("00000000-0000-0000-0000-100000000000"),
                            ProductName = "Prod1",
                            ProductDescription = "ProdDesc1",
                            CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                            Category =
                            new()
                            {
                                CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                                CategoryName = "Cat2",
                                CreatedAt = new DateTime(2021, 10, 9),
                                ModifiedAt = new DateTime(2021, 11, 23)
                            },
                            IsWeight = false,
                            Unit = "szt",
                            CreatedAt = new DateTime(2022, 10, 9),
                            ModifiedAt = new DateTime(2022, 11, 21)
                        }
                    }, 1, 25, 1)
            },
            new object[]
            {
                new List<Category>()
                {
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        CategoryName = "Cat1",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    },
                    new()
                    {
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        CategoryName = "Cat2",
                        CreatedAt = new DateTime(2021, 10, 9),
                        ModifiedAt = new DateTime(2021, 11, 23)
                    }
                },
                new List<Product>()
                {
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-100000000000"),
                        ProductName = "Prod1",
                        ProductDescription = "ProdDesc1",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                        ProductName = "Prod2",
                        ProductDescription = "ProdDesc2",
                        CategoryId = new Guid("00000000-0000-0000-2000-000000000000"),
                        IsWeight = false,
                        Unit = "szt",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    },
                    new()
                    {
                        ProductId = new Guid("00000000-0000-0000-0000-300000000000"),
                        ProductName = "Prod3",
                        ProductDescription = "ProdDesc3",
                        CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "productName==Prod13",
                    Page = 3,
                    PageSize = 50,
                    Sorts = ""
                },
                new PagedResult<Product>([], 0, 50, 3)
            }
        };

        [Theory]
        [MemberData(nameof(ValidDataForGetSortedAndFilteredProductsAsync))]
        public async Task GetSortedAndFilteredProductsAsync_ForValidData_ReturnsPagedResults(
            List<Category> categories,
            List<Product> products,
            SieveModel sieveModel,
            PagedResult<Product> expectedResult)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            await context
                .Set<Category>()
                .AddRangeAsync(categories);

            await context
                .Set<Product>()
                .AddRangeAsync(products);

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<ProductsRepository>>();

            IOptions<SieveOptions> optionsParameter = Options.Create(new SieveOptions());

            ProductsRepository productRepository = new(context, logger.Object, new MiniWmsSieveProcessor(optionsParameter));

            var response = await productRepository.GetSortedAndFilteredProductsAsync(sieveModel);

            foreach (var i in response.Items)
            {
                i.Category!.Products = [];
            }

            response
                .Should()
                .NotBeNull();

            response.Should().BeEquivalentTo(expectedResult);

            response.Items
                .Should()
                .BeEquivalentTo(
                    expectedResult.Items,
                    options => options.WithStrictOrdering());
        }

        public static IEnumerable<object[]> InvalidDataForGetSortedAndFilteredProductsAsync => new List<object[]>
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
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "hgfdhfdhfdgh4%$^4",
                    Page = 1,
                    PageSize = 4,
                    Sorts = "dfgdfg"
                },
                new PagedResult<Product>(
                    new List<Product>
                    {
                        new()
                        {
                            ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                            ProductName = "Prod1",
                            ProductDescription = "ProdDesc1",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
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
                            ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
                            ProductName = "Prod2",
                            ProductDescription = "ProdDesc2",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
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
                            ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
                            ProductName = "Prod3",
                            ProductDescription = "ProdDesc3",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
                            {
                                CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                                CategoryName = "CategoryTest1",
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
                        }
                    }, 3, 4, 1)
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
                        CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                        IsWeight = true,
                        Unit = "kg",
                        CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                        CreatedAt = new DateTime(2022, 10, 9),
                        ModifiedAt = new DateTime(2022, 11, 21)
                    }
                },
                new SieveModel()
                {
                    Filters = "#sfg%Ygfhfgh<gh==ghghgfh",
                    Page = 3,
                    PageSize = 1,
                    Sorts = "-ProductDescription"
                },
                new PagedResult<Product>(
                    new List<Product>
                    {
                        new()
                        {
                            ProductId = new Guid("00003000-1000-1000-0000-122000000000"),
                            ProductName = "Prod1",
                            ProductDescription = "ProdDesc1",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
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
                            ProductId = new Guid("00003000-2000-1000-0000-122000000000"),
                            ProductName = "Prod2",
                            ProductDescription = "ProdDesc2",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
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
                            ProductId = new Guid("00003000-3000-1000-0000-122000000000"),
                            ProductName = "Prod3",
                            ProductDescription = "ProdDesc3",
                            CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                            Category =
                            new()
                            {
                                CategoryId = new Guid("00000020-0020-0000-1000-122000000000"),
                                CategoryName = "CategoryTest1",
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
                        }
                    }, 3, 1, 3)
            }
        };


        [Theory]
        [MemberData(nameof(InvalidDataForGetSortedAndFilteredProductsAsync))]
        public async Task GetSortedAndFilteredProductsAsync_ForInvalidData_ReturnsNotSortedAndFilteredList(
            List<Category> categories,
            List<Product> products,
            SieveModel sieveModel,
            PagedResult<Product> expectedResult)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            await context
                .Set<Category>()
                .AddRangeAsync(categories);

            await context
                .Set<Product>()
                .AddRangeAsync(products);

            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<ProductsRepository>>();

            IOptions<SieveOptions> optionsParameter = Options.Create(new SieveOptions());

            ProductsRepository productRepository = new(context, logger.Object, new MiniWmsSieveProcessor(optionsParameter));

            var response = await productRepository.GetSortedAndFilteredProductsAsync(sieveModel);

            foreach (var i in response.Items)
            {
                i.Category!.Products = [];
            }

            response
                .Should()
                .NotBeNull();

            response.Should().BeEquivalentTo(expectedResult);

            response.Items
                .Should()
                .BeEquivalentTo(
                    expectedResult.Items,
                    options => options.WithStrictOrdering());
        }

        public static IEnumerable<object[]> ValidDataForCreateAsync => new List<object[]>
        {
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    CategoryName = "Cat1",
                    CreatedAt = new DateTime(2021, 10, 9),
                    ModifiedAt = new DateTime(2021, 11, 23)
                },
                new Product()
                {
                    ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                    ProductName = "TestProd1",
                    ProductDescription = "ProdDesc2",
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    IsWeight = false,
                    Unit = "szt",
                    CreatedAt = new DateTime(2022, 10, 9),
                    ModifiedAt = new DateTime(2022, 11, 21)
                }
            },
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    CategoryName = "Cat1",
                    CreatedAt = new DateTime(2021, 10, 9),
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedAt = new DateTime(2021, 11, 23)
                },
                new Product()
                {
                    ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                    ProductName = "T1",
                    ProductDescription = "",
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    IsWeight = false,
                    Unit = "szt",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2022, 10, 9),
                    ModifiedAt = new DateTime(2022, 11, 21)
                }
            }
        };


        [Theory]
        [MemberData(nameof(ValidDataForCreateAsync))]
        public async Task CreateAsync_ForValidData_CreatesProduct(Category category, Product product)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            await context.Set<Category>().AddAsync(category);
            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<ProductsRepository>>();

            IOptions<SieveOptions> optionsParameter = Options.Create(new SieveOptions());

            ProductsRepository productRepository = new(context, logger.Object, new MiniWmsSieveProcessor(optionsParameter));

            var response = await productRepository.CreateAsync(product);

            response
                .Should()
                .NotBeNull();

            response
                .Should()
                .BeEquivalentTo(product);
        }

        public static IEnumerable<object[]> InvalidDataForCreateAsync => new List<object[]>
        {
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    CategoryName = "Cat1",
                    CreatedAt = new DateTime(2021, 10, 9),
                    ModifiedAt = new DateTime(2021, 11, 23)
                },
                new Product()
                {
                    ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                    ProductName = "TestProd1",
                    ProductDescription = "ProdDesc2",
                    CategoryId = new Guid("00000000-0000-0000-4000-000000000000"),
                    IsWeight = false,
                    Unit = "szt",
                    CreatedAt = new DateTime(2022, 10, 9),
                    ModifiedAt = new DateTime(2022, 11, 21)
                }
            },
            new object[]
            {
                new Category()
                {
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    CategoryName = "Cat1",
                    CreatedAt = new DateTime(2021, 10, 9),
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedAt = new DateTime(2021, 11, 23)
                },
                new Product()
                {
                    ProductId = new Guid("00000000-0000-0000-0000-200000000000"),
                    ProductName = "",
                    ProductDescription = "",
                    CategoryId = new Guid("00000000-0000-0000-1000-000000000000"),
                    IsWeight = false,
                    Unit = "szt",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2022, 10, 9),
                    ModifiedAt = new DateTime(2022, 11, 21)
                }
            }
        };


        [Theory]
        [MemberData(nameof(InvalidDataForCreateAsync))]
        public async Task CreateAsync_ForInvalidData_CreatesInvalidProduct(Category category, Product product)
        {
            await using var context = MiniWmsContextInMemoryFactory.Create();

            await context.Set<Category>().AddAsync(category);
            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<ProductsRepository>>();

            IOptions<SieveOptions> optionsParameter = Options.Create(new SieveOptions());

            ProductsRepository productRepository = new(context, logger.Object, new MiniWmsSieveProcessor(optionsParameter));

            var response = await productRepository.CreateAsync(product);

            response
                .Should()
                .NotBeNull();

            response
                .Should()
                .BeEquivalentTo(product);
        }
    }
}
