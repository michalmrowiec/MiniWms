using FluentValidation.Results;

namespace miniWms.Application.Functions.Employees.Commands
{
    public class AddEmployeeResponse : ResponseBase
    {
        public JwtToken? JwtToken { get; set; }
        public AddEmployeeResponse(JwtToken jwtToken) : base()
        {
            JwtToken = jwtToken;
            Success = true;
            ValidationErrors = new();
        }
        public AddEmployeeResponse(bool status, string message) : base(status, message)
        { }
        public AddEmployeeResponse(bool success, string? message, ValidationResult validationResult) : base(success, message, validationResult)
        { }

        public AddEmployeeResponse(ValidationResult validationResult) : base(validationResult)
        { }
    }
}
