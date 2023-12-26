using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Commands.CreateEmployee;
using miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail;
using miniWms.Application.Functions.Roles.Queries.GetAllRoles;
using miniWms.Domain.Entities;

namespace miniWms.UnitTests.Application.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "ope",
                        RoleName = "Operator",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    },
                    new()
                    {
                        RoleId = "mng",
                        RoleName = "Manager",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    },
                    new()
                    {
                        RoleId = "adm",
                        RoleName = "Admin",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    }
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
            List<Role> roles, CreateEmployeeCommand employeeCommand, Employee employee)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetEmployeeByEmailQuery(employeeCommand.EmailAddress), new CancellationToken()))
                .ReturnsAsync(new Employee());

            mediator.Setup(m => m.Send(new GetAllRolesQuery(), new CancellationToken()))
                .ReturnsAsync(roles);

            var repo = new Mock<IEmployeeRepository>();
            repo.Setup(m => m.CreateEmployeeAsync(It.IsAny<CreateEmployeeCommand>()))
                .ReturnsAsync(employee);

            CreateEmployeeCommandHandler handler = new(repo.Object, mediator.Object);

            var response = await handler.Handle(employeeCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeEmpty();
            response.ValidationErrors.Should().BeEmpty();
            response.JwtToken.Should().BeNull();
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "ope",
                        RoleName = "Operator",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    },
                    new()
                    {
                        RoleId = "mng",
                        RoleName = "Manager",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    },
                    new()
                    {
                        RoleId = "adm",
                        RoleName = "Admin",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    }
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
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "ope",
                        RoleName = "Operator",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    },
                    new()
                    {
                        RoleId = "mng",
                        RoleName = "Manager",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    },
                    new()
                    {
                        RoleId = "adm",
                        RoleName = "Admin",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2022, 12, 23)
                    }
                },
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
            },
            new object[]
            {
                new List<Role> ()
                {
                    new()
                    {
                        RoleId = "ope",
                        RoleName = "Operator",
                        CreatedBy = Guid.NewGuid(),
                        ModifiedBy = Guid.NewGuid(),
                        CreatedAt = new DateTime(2022, 11, 13),
                        ModifiedAt = new DateTime(2023, 12, 23)
                    }
                },
                new Employee(),
                new CreateEmployeeCommand()
                {
                    RoleId = "mng",
                    FirstName = "Test",
                    LastName = "Test",
                    Password = "P@$$w0rd",
                    HaveToChangePassword = true,
                    EmailAddress = "test@test12.com",
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
            List<Role> roles, Employee getEmployeeById, CreateEmployeeCommand employeeCommand)
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(new GetEmployeeByEmailQuery(employeeCommand.EmailAddress), new CancellationToken()))
                .ReturnsAsync(getEmployeeById);

            mediator.Setup(m => m.Send(new GetAllRolesQuery(), new CancellationToken()))
                .ReturnsAsync(roles);

            var repo = new Mock<IEmployeeRepository>();
            repo.Setup(m => m.CreateEmployeeAsync(It.IsAny<CreateEmployeeCommand>()))
                .ReturnsAsync(new Employee());

            CreateEmployeeCommandHandler handler = new(repo.Object, mediator.Object);

            var response = await handler.Handle(employeeCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().NotBeEmpty();
            response.ValidationErrors.Should().NotBeEmpty();
            response.JwtToken.Should().BeNull();
        }
    }
}
