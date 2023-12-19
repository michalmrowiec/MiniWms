using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeByEmailAddressAsync(string email);
        Task<Employee> AddEmployeeAsync(Employee employee);
    }
}
