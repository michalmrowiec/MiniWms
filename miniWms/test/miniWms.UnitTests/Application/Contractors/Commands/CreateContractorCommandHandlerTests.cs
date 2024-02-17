using miniWms.Application.Contracts;
using miniWms.Application.Functions;
using miniWms.Application.Functions.Categories.Commands.CreateCategory;
using miniWms.Application.Functions.DocumentTypes.Commands.CreateDocumentType;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Contractors.Commands
{
    public class CreateContractorCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test1",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new Contractor()
                {
                    ContractorId = Guid.NewGuid(),
                    ContractorName = "Testdsfaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa2fds fsdgsdfgdsfdsaf sdafdsa fsad sda fsadf sad 341 13",
                    VatId = "12345678890sfdsgsdfgdfgdfsfsdf",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test1",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                },
                new Contractor()
                {
                    ContractorId = Guid.NewGuid(),
                    ContractorName = "Test2# !@#%^%&* ",
                    VatId = "1232m123123123",
                    IsSupplier = false,
                    IsRecipient = true,
                    Country = "test Test TEST",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test 900/12H",
                    EmailAddress = "test22@contracotr.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    ModifiedBy = new Guid("00000001-0000-0000-0000-122000000000"),
                    CreatedAt = new DateTime(2023, 10, 23),
                    ModifiedAt = new DateTime(2023, 10, 23)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateContractorCommandHandler_ForValidData_ReturnsSucced(CreateContractorCommand contractorCommand, Contractor contractor)
        {
            var repo = new Mock<IContractorsRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Contractor>()))
                .ReturnsAsync(contractor);

            CreateContractorCommandHandler handler = new(repo.Object);

            ResponseBase<Contractor> response = await handler.Handle(contractorCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().BeEmpty();
            response.ReturnedObj.Should().BeEquivalentTo(contractor);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "",
                    Country = "",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890",
                    IsSupplier = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "12345678890sfdsgsdfgdfgdfsfsdfftesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttestdd",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Testdsfaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa2fds fsdgsdfgdsfdsaf sdafdsa fsad sda fsadf sad 341 132",
                    VatId = "12345678890",
                    IsSupplier = false,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            },
            new object[]
            {
                new CreateContractorCommand()
                {
                    ContractorName = "Test",
                    VatId = "12345678890sfdsgsdfgdfgdfsfsdff",
                    IsSupplier = true,
                    IsRecipient = true,
                    Country = "test",
                    City = "test",
                    Region = "test",
                    PostalCode = "12-123",
                    Address = "test",
                    EmailAddress = "test1@contracotr.com.pl",
                    PhoneNumber = "123456789",
                    CreatedBy = new Guid("00000001-0000-0000-0000-122000000000")
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateContractorCommandHandler_ForInvalidData_ReturnsErrors(CreateContractorCommand contractorCommand)
        {
            var repo = new Mock<IContractorsRepository>();
            repo.Setup(m => m.CreateAsync(It.IsAny<Contractor>()))
                .ReturnsAsync(new Contractor());

            CreateContractorCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(contractorCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().BeNull();
            response.ValidationErrors.Should().NotBeEmpty();
        }
    }
}
