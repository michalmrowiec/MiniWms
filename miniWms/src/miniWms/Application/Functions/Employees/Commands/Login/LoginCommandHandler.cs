using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.Employees.Commands.CreateEmployee;

namespace miniWms.Application.Functions.Employees.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeesRepository;

        public LoginCommandHandler(IEmployeeRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<EmployeeResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginValidator validator = new();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new EmployeeResponse(validationResult);
            }

            var jwtToken = await _employeesRepository.LoginEmployeeAsync(request);

            if (jwtToken.Token == null)
            {
                return new EmployeeResponse(false, "Email address or password are wrong.");
            }

            return new EmployeeResponse(jwtToken);
        }
    }
}
