using MediatR;
using Microsoft.AspNetCore.Identity;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Infrastructure;

namespace miniWms.Application.Functions.Employees.Commands
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMediator _mediator;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public AddEmployeeCommandHandler(IEmployeesRepository employeesRepository, IMediator mediator, AuthenticationSettings authenticationSettings, IPasswordHasher<Employee> passwordHasher)
        {
            _employeesRepository = employeesRepository;
            _mediator = mediator;
            _authenticationSettings = authenticationSettings;
            _passwordHasher = passwordHasher;
        }

        public async Task<AddEmployeeResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            RegisterValidator validator = new(_mediator);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new AddEmployeeResponse(validationResult);
            }

            var newEmployee = new Employee()
            {
                RoleId = request.RoleId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                HaveToChangePassword = request.HaveToChangePassword,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                IsActive = request.IsActive,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy
            };
            newEmployee.PasswordHash = _passwordHasher.HashPassword(newEmployee, request.Password);

            var addedUser = await _employeesRepository.AddEmployeeAsync(newEmployee);


            if (addedUser == null)
            {
                return new AddEmployeeResponse(false, "Something went wrong.");
            }

            JwtTokenService tokenService = new(_authenticationSettings);
            JwtToken jwtToken = new()
            {
                UserEmail = request.EmailAddress,
                Token = tokenService.GenerateJwt(addedUser)
            };

            return new AddEmployeeResponse(jwtToken);
        }
    }
}
