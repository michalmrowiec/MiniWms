using MediatR;

namespace miniWms.Application.Functions.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<EmployeeResponse>
    {
        public Guid EmployeeId { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
