using FluentValidation.Results;
using miniWms.Domain.Models;

namespace miniWms.Application.Functions.Employees.Commands
{
    public class EmployeeResponse : ResponseBase
    {
        public JwtToken? JwtToken { get; set; }
        public bool? HaveToChangePassword { get; set; }

        public EmployeeResponse(JwtToken jwtToken) : base()
        {
            JwtToken = jwtToken;
            Success = true;
            ValidationErrors = new();
        }

        public EmployeeResponse(bool status, string message) : base(status, message)
        { }

        public EmployeeResponse(bool success, string? message, ValidationResult validationResult) : base(success, message, validationResult)
        { }

        public EmployeeResponse(ValidationResult validationResult) : base(validationResult)
        { }

        public EmployeeResponse() : base()
        { }
    }
}
