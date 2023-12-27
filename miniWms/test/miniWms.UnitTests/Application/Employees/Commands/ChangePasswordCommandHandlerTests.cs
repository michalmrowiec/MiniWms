using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Commands.ChangePassword;

namespace miniWms.UnitTests.Application.Employees.Commands
{
    public class ChangePasswordCommandHandlerTests
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new ChangePasswordCommand()
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-900000000000"),
                    NewPassword = "qwertyy",
                    RepeatPassword = "qwertyy"
                }
            }
        };

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task ChangePasswordCommandHandler_ForValidData_ReturnsSucced(ChangePasswordCommand changePasswordCommand)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.ChangePassword(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            ChangePasswordCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(changePasswordCommand, new CancellationToken());

            response.Success.Should().BeTrue();
            response.Message.Should().BeNullOrEmpty();
            response.ValidationErrors.Should().BeEmpty();
            response.JwtToken.Should().BeNull();
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[]
            {
                new ChangePasswordCommand()
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-900000000000"),
                    NewPassword = "qwertyy",
                    RepeatPassword = "qwerty"

                }
            },
            new object[]
            {
                new ChangePasswordCommand()
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-900000000000"),
                    NewPassword = "",
                    RepeatPassword = ""

                }
            },
            new object[]
            {
                new ChangePasswordCommand()
                {
                    EmployeeId = new Guid("00000000-0000-0000-0000-900000000000"),
                    NewPassword = "",
                    RepeatPassword = "34fdg#$%TEntity$TEsgdsfg"

                }
            }
        };

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task ChangePasswordCommandHandler_ForInvalidData_ReturnsErrors(ChangePasswordCommand changePasswordCommand)
        {
            var repo = new Mock<IEmployeesRepository>();
            repo.Setup(m => m.ChangePassword(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            ChangePasswordCommandHandler handler = new(repo.Object);

            var response = await handler.Handle(changePasswordCommand, new CancellationToken());

            response.Success.Should().BeFalse();
            response.Message.Should().NotBeEmpty();
            response.ValidationErrors.Should().NotBeEmpty();
            response.JwtToken.Should().BeNull();
        }
    }
}
