using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly MiniWmsDbContext _context;
        public EmployeesRepository(MiniWmsDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var newEmployee =  await _context.Employees.FirstOrDefaultAsync(u => u.EmailAddress == employee.EmailAddress);
            
            return newEmployee == null ? new() : newEmployee;

        }

        public async Task<Employee> GetEmployeeByEmailAddressAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(u => u.EmailAddress == email);
            return employee == null ? new() : employee;
        }
    }
}
