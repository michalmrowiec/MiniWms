using FluentAssertions;
using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Commands;
using miniWms.Application.Functions.Employees.Commands.CreateEmployee;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Domain.Entities;
using Moq;

namespace miniWms.UnitTests.Application.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateEmployeeCommand()
                {
                    RoleId = "ope",
                    FirstName = "Test",
                    LastName = "Test",
                    Password = "P@$$w0rd",
                    HaveToChangePassword = true,
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Country = "Poland",
                    City = "Test",
                    Region = "Podlasie",
                    PostalCode = "12-341",
                    Address = "Szkolna 421",
                    IsActive = true,
                    CreatedBy = new Guid("00000000-0000-0000-0000-120000000000")
                },
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
        public async Task CreateEmployeeCommandHandler_ForValidData_ReturnsSucced(
            CreateEmployeeCommand employeeCommand, Employee employee)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetEmployeeByEmailQuery(employeeCommand.EmailAddress), new CancellationToken()))
                .ReturnsAsync(new Employee());

            var repo = new Mock<IEmployeeRepository>();
            repo.Setup(m => m.CreateEmployeeAsync(It.IsAny<CreateEmployeeCommand>()))
                .ReturnsAsync(employee);

            CreateEmployeeCommandHandler handler = new(repo.Object, mediator.Object);

            var response = await handler.Handle(employeeCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeEmpty();
            response.ValidationErrors.Should().BeEmpty();
            response.JwtToken.Should().BeNull();
            response.HaveToChangePassword.Should().BeNull();
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
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
                },
                new CreateEmployeeCommand()
                {
                    RoleId = "ope",
                    FirstName = "Test",
                    LastName = "Test",
                    Password = "P@$$w0rd",
                    HaveToChangePassword = true,
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Country = "Poland",
                    City = "Test",
                    Region = "Podlasie",
                    PostalCode = "12-341",
                    Address = "Szkolna 421",
                    IsActive = true,
                    CreatedBy = new Guid("00000000-0000-0000-0000-120000000000")
                }
            },
            new object[]
            {
                new Employee(),
                new CreateEmployeeCommand()
                {
                    RoleId = "ope",
                    FirstName = "",
                    LastName = "Test",
                    Password = "P@$$w0rd",
                    HaveToChangePassword = true,
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Country = "Poland",
                    City = "Test",
                    Region = "Podlasie",
                    PostalCode = "12-341",
                    Address = "Szkolna 421",
                    IsActive = true,
                    CreatedBy = new Guid("00000000-0000-0000-0000-120000000000")
                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateEmployeeCommandHandler_ForInvalidData_ReturnsErrors(
            Employee getEmployeeById, CreateEmployeeCommand employeeCommand)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetEmployeeByEmailQuery(employeeCommand.EmailAddress), new CancellationToken()))
                .ReturnsAsync(getEmployeeById);

            var repo = new Mock<IEmployeeRepository>();
            repo.Setup(m => m.CreateEmployeeAsync(It.IsAny<CreateEmployeeCommand>()))
                .ReturnsAsync(new Employee());

            CreateEmployeeCommandHandler handler = new(repo.Object, mediator.Object);

            var response = await handler.Handle(employeeCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().NotBeEmpty();
            response.ValidationErrors.Should().NotBeEmpty();
            response.JwtToken.Should().BeNull();
            response.HaveToChangePassword.Should().BeNull();
        }
    }
}
