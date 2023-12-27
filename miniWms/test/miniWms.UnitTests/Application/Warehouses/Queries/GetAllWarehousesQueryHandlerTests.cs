using miniWms.Application.Contracts;
using miniWms.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Warehouses.Queries
{
    public class GetAllWarehousesQueryHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<Warehouse> ()
                {
                    new Warehouse()
                    {
                        WarehouseId = Guid.NewGuid(),
                        WarehouseName = "Test1",
                        Country = "Poland",
                        Region = "Mazowieckie",
                        City = "Test",
                        PostalCode = "12-345",
                        Address = "Górna 17B",
                        CreatedBy = new Guid("00000000-0000-0000-0000-120000000000"),
                        ModifiedBy = new Guid("00000000-0000-0000-0000-120000000000"),
                        CreatedAt = new DateTime(2023, 12, 23),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    },
                    new Warehouse()
                    {
                        WarehouseId = Guid.NewGuid(),
                        WarehouseName = "Test2",
                        Country = "Poland",
                        Region = "Mazowieckie",
                        City = "Test2",
                        PostalCode = "12-343",
                        Address = "Dolna 11/4",
                        CreatedBy = null,
                        ModifiedBy = null,
                        CreatedAt = new DateTime(2023, 12, 22),
                        ModifiedAt = new DateTime(2023, 12, 22)
                    }
                }
            },
            new object[]
            {
                new List<Warehouse> ()
            },
            new object[]
            {
                new List<Warehouse> ()
                {
                    new Warehouse()
                    {
                        WarehouseId = Guid.NewGuid(),
                        WarehouseName = "Test2",
                        Country = "Poland",
                        Region = "Mazowieckie",
                        City = "Test23",
                        PostalCode = "12-445",
                        Address = "Górna 11/7",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2023, 12, 23),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    }
                }
            }

        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task GetAllWarehousesHandler_ForValidData_ReturnsSuccedWithListOfWarehouses(List<Warehouse> warehouses)
        {
            var repo = new Mock<IWarehouseRepository>();
            repo.Setup(m => m.GetAllAsync())
                .ReturnsAsync(warehouses);

            GetAllWarehousesQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetAllWarehousesQuery(), new CancellationToken());

            response.Should().NotBeNull().And.BeEquivalentTo(warehouses);
        }
    }
}
