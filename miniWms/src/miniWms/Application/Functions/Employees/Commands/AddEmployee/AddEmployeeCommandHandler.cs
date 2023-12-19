using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMediator _mediator;

        public AddEmployeeCommandHandler(IEmployeesRepository employeesRepository, IMediator mediator)
        {
            _employeesRepository = employeesRepository;
            _mediator = mediator;
        }

        public async Task<EmployeeResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            RegisterValidator validator = new(_mediator);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new EmployeeResponse(validationResult);
            }

            var newEmployee = new AddEmployeeModel()
            {
                RoleId = request.RoleId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                HaveToChangePassword = request.HaveToChangePassword,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Address = request.Address,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
            };

            var jwtToken = await _employeesRepository.AddEmployeeAsync(newEmployee);

            if (jwtToken.EmailAddress == null)
            {
                return new EmployeeResponse(false, "Something went wrong.");
            }

            return new EmployeeResponse(true, "");
        }
    }
}
