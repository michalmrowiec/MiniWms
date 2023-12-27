using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Employees.Queries
{
    public class GetEmployeeByEmailQueryHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new Employee()
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-900000000000"),
                    RoleId = "ope",
                    FirstName = "Test",
                    LastName = "Test",
                    PasswordHash = "dsafsadfsaDFSADfSADfSAdfasdfP@$$w0rd",
                    HaveToChangePassword = true,
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Country = "Poland",
                    City = "Test",
                    Region = "Podlasie",
                    PostalCode = "12-341",
                    Address = "Szkolna 421",
                    IsActive = true,
                    CreatedBy = new Guid("00000000-0000-0000-0000-120000000000"),
                    ModifiedBy = new Guid("00000000-0000-0000-0000-120000000000"),
                    CreatedAt = new DateTime(2023, 12, 23),
                    ModifiedAt = new DateTime(2023, 12, 23)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task GetEmployeeByEmailQueryHandler_ForValidData_ReturnsEmployee(Employee employee)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.GetEmployeeByEmailAddressAsync(employee.EmailAddress))
                .ReturnsAsync(employee);

            GetEmployeeByEmailQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetEmployeeByEmailQuery(employee.EmailAddress), new CancellationToken());

            response.Should().BeEquivalentTo(employee);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                "ttest@test.com",
                new Employee()
            },
            new object[]
            {
                "",
                new Employee()
            },
            new object[]
            {
                "asdfasdfsadf.dsf",
                new Employee()
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task GetEmployeeByEmailQueryHandler_ForInvalidData_ReturnsEmptyEmployee(string emailAddress, Employee employee)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.GetEmployeeByEmailAddressAsync(emailAddress))
                .ReturnsAsync(employee);

            GetEmployeeByEmailQueryHandler handler = new(repo.Object);

            var response = await handler.Handle(new GetEmployeeByEmailQuery(emailAddress), new CancellationToken());

            response.Should().BeEquivalentTo(employee);
        }
    }
}
