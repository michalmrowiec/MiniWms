using MediatR;

namespace miniWms.Application.Functions.Employees.Commands.Login
{
    public class LoginCommand : IRequest<EmployeeResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
