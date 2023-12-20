using miniWms.Domain.Entities;
using miniWms.Domain.Models;

namespace miniWms.Application.Contracts
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeByEmailAddressAsync(string email);
        Task<Employee> CreateEmployeeAsync(CraeteEmployeeModel employee);
        Task<JwtToken> LoginEmployeeAsync(LoginEmployeeModel employee);
        Task<bool> ChangePassword(Guid employeeId, string newPassword);
    }
}
