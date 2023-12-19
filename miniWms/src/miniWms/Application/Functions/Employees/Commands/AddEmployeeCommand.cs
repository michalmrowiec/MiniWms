using MediatR;

namespace miniWms.Application.Functions.Employees.Commands
{
    public class AddEmployeeCommand : IRequest<AddEmployeeResponse>
    {
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool HaveToChangePassword { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
