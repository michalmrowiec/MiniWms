using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Employees.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, EmployeeResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public LoginCommandHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<EmployeeResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginEmployee = new LoginEmployeeModel()
            {
                EmailAddress = request.EmailAddress,
                Password = request.Password
            };

            var jwtToken = await _employeesRepository.LoginEmployeeAsync(loginEmployee);

            if (jwtToken.Token == null)
            {
                return new EmployeeResponse(false, "Email address or password are wrong.");
            }

            return new EmployeeResponse(jwtToken);
        }
    }
}
