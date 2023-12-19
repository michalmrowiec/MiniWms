using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        public Task<Employee> AddEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByEmailAddressAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
