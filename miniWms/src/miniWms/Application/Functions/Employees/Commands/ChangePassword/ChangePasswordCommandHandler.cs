using MediatR;
using miniWms.Application.Contracts;

namespace miniWms.Application.Functions.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, EmployeeResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;

        public ChangePasswordCommandHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<EmployeeResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangePasswordCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new EmployeeResponse(validatorResult);

            var changePasswordResult = await _employeesRepository.ChangePassword(request.EmployeeId, request.NewPassword);

            if (changePasswordResult is false)
                return new EmployeeResponse(false, "Something went wrong.");

            return new EmployeeResponse();
        }
    }
}
