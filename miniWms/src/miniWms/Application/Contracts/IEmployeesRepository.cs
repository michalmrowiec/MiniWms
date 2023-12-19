using miniWms.Domain.Entities;
using miniWms.Domain.Models;

namespace miniWms.Application.Contracts
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeByEmailAddressAsync(string email);
        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<Employee> AddEmployeeAsync(AddEmployeeModel employee);
        Task<JwtToken> LoginEmployeeAsync(LoginEmployeeModel employee);

    }
}
