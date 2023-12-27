using FluentAssertions;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse;
using miniWms.Domain.Entities;
using Moq;

namespace miniWms.UnitTests.Application.Warehouses.Commands
{
    public class CreateWarehouseCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test1",
                    Country = "Poland",
                    Region = "Mazowieckie",
                    City = "Test",
                    PostalCode = "12-345",
                    Address = "Górna 17B",
                    CreatedBy = new Guid("00000000-0000-0000-0000-120000000000")
                },
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
                }
            },
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test2",
                    Country = "Poland",
                    Region = "Mazowieckie",
                    City = "Test2",
                    PostalCode = "12-343",
                    Address = "Dolna 11/4"
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
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateWarehouseCommandHandler_ForValidData_ReturnsSucced(CreateWarehouseCommand warehouseCommand, Warehouse warehouse)
        {
            var repo = new Mock<IWarehouseRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Warehouse>()))
                .ReturnsAsync(warehouse);

            CreateWarehouseCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(warehouseCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.Warehouse.Should().BeEquivalentTo(warehouse);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "",
                    Country = "Poland",
                    Region = "Mazowieckie",
                    City = "Test",
                    PostalCode = "12-345",
                    Address = "Górna 17B",
                    CreatedBy = new Guid("00000000-0000-0000-0000-220000000000")
                }
            },
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test2",
                    Country = "",
                    Region = "Mazowieckie",
                    City = "Test2",
                    PostalCode = "12-343",
                    Address = "Dolna 11/4",
                    CreatedBy = new Guid("00000000-0000-0000-0000-230000000000")
                }
            },
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test2",
                    Country = "Test34",
                    Region = "Mazowieckie",
                    City = "",
                    PostalCode = "12-343",
                    Address = "Dolna 11/4",
                    CreatedBy = new Guid("00000000-0000-0000-0000-230000000000")
                }
            },
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test2",
                    Country = "Test12",
                    Region = "Mazowieckie",
                    City = "Test2",
                    PostalCode = "",
                    Address = "Dolna 11/4",
                    CreatedBy = new Guid("00000000-0000-0000-0000-230000000000")
                }
            },
            new object[]
            {
                new CreateWarehouseCommand()
                {
                    WarehouseName = "Test2",
                    Country = "Test12",
                    Region = "Mazowieckie",
                    City = "Test2",
                    PostalCode = "12-343",
                    Address = "",
                    CreatedBy = new Guid("00000000-0000-0000-0000-230000000000")
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateWarehouseCommandHandler_ForInvalidData_ReturnsErrors(CreateWarehouseCommand warehouseCommand)
        {
            var repo = new Mock<IWarehouseRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Warehouse>()))
                .ReturnsAsync(new Warehouse());

            CreateWarehouseCommandHandler handler = new CreateWarehouseCommandHandler(repo.Object);

            var response = await handler.Handle(warehouseCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().NotBeEmpty();
            response.Warehouse.Should().BeNull();
        }
    }
}