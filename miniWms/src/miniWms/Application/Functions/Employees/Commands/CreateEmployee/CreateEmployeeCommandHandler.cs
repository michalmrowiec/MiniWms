using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeesRepository;
        private readonly IMediator _mediator;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeesRepository, IMediator mediator)
        {
            _employeesRepository = employeesRepository;
            _mediator = mediator;
        }

        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            RegisterValidator validator = new(_mediator);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new EmployeeResponse(validationResult);
            }

            var createdEmployee = await _employeesRepository.CreateEmployeeAsync(request);

            if (createdEmployee.EmailAddress == null)
            {
                return new EmployeeResponse(false, "Something went wrong.");
            }

            return new EmployeeResponse(true, "");
        }
    }
}
