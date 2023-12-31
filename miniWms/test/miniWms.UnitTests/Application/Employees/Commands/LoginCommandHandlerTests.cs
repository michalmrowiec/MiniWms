using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Commands.Login;
using miniWms.Domain.Models;

namespace miniWms.UnitTests.Application.Employees.Commands
{
    public class LoginCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new LoginCommand()
                {
                    Password = "P@$$w0rd",
                    EmailAddress = "test@test.com",
                },
                new JwtToken()
                {
                    UserEmail = "test@test.com",
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ7.eyJodHRwOi6vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InRlc3RAdGVzdC5jb20iLCJleHAiOjE2OTA0Nzc0NTcsImlzcyI6Imh0dHBzOi8vd3d3Lm1vbmV5bWFuYWdlci5ob3N0aW5nYXNwLnBsLyIsImF1ZCI6Imh0dHBzOi8vd3d3Lm1vbmV5bWFuYWdlci5ob3N0aW5nYXNwLnBsLyJ9.OXqnr6pjbT4nuuVdR9ld_BZzXzChrYGqxb7n-BhEM4o",
                    HaveToChangePassword = true
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task LoginCommandHandler_ForValidData_ReturnsSuccedWithJwt(
            LoginCommand loginCommand, JwtToken jwtToken)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.LoginEmployeeAsync(loginCommand))
                .ReturnsAsync(jwtToken);

            LoginCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(loginCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNullOrEmpty();
            response.ValidationErrors.Should().BeEmpty();
            response.JwtToken.Should().BeEquivalentTo(jwtToken);
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new LoginCommand()
                {
                    Password = "",
                    EmailAddress = "test@test.com",
                },
                new JwtToken()
            },
            new object[]
            {
                new LoginCommand()
                {
                    Password = "sdafsda",
                    EmailAddress = "",
                },
                new JwtToken()
            },
            new object[]
            {
                new LoginCommand()
                {
                    Password = "",
                    EmailAddress = "",
                },
                new JwtToken()
            },
            new object[]
            {
                new LoginCommand()
                {
                    Password = "@#$%34sdfgfdsg",
                    EmailAddress = "test.io",
                },
                new JwtToken()
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task LoginCommandHandler_ForInvalidData_ReturnsErrors(
            LoginCommand loginCommand, JwtToken jwtToken)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.LoginEmployeeAsync(loginCommand))
                .ReturnsAsync(jwtToken);

            LoginCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(loginCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().NotBeEmpty();
            response.ValidationErrors.Should().NotBeEmpty();
            response.JwtToken.Should().BeNull();
        }
    }
}
