using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using miniWms.Infrastructure.Services;

namespace miniWms.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public EmployeesRepository(MiniWmsDbContext context, AuthenticationSettings authenticationSettings, IPasswordHasher<Employee> passwordHasher)
        {
            _context = context;
            _authenticationSettings = authenticationSettings;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ChangePassword(Guid employeeId, string newPassword)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return false;
            }

            employee.PasswordHash = _passwordHasher.HashPassword(employee, newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Employee> CreateEmployeeAsync(CraeteEmployeeModel employee)
        {
            var newEmployee = new Employee()
            {
                RoleId = employee.RoleId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                HaveToChangePassword = employee.HaveToChangePassword,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Country = employee.Country,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Address = employee.Address,
                IsActive = employee.IsActive,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = employee.CreatedBy,
                ModifiedBy = employee.CreatedBy
            };
            newEmployee.PasswordHash = _passwordHasher.HashPassword(newEmployee, employee.Password);

            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            var addedEmployee = await GetEmployeeByEmailAddressAsync(newEmployee.EmailAddress);

            if (addedEmployee.EmailAddress == null)
            {
                return new();
            }

            return addedEmployee;
        }

        public async Task<Employee> GetEmployeeByEmailAddressAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(u => u.EmailAddress == email);
            return employee ?? new();
        }

        public async Task<JwtToken> LoginEmployeeAsync(LoginEmployeeModel loginEmployee)
        {
            var employee = await GetEmployeeByEmailAddressAsync(loginEmployee.EmailAddress);

            if (employee.EmailAddress == null)
            {
                return new();
            }

            var veryfication = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, loginEmployee.Password);

            if (veryfication == PasswordVerificationResult.Failed)
            {
                return new();
            }

            JwtTokenService tokenService = new(_authenticationSettings);
            JwtToken jwtToken = new()
            {
                UserEmail = employee.EmailAddress,
                Token = tokenService.GenerateJwt(employee),
                HaveToChangePassword = employee.HaveToChangePassword
            };

            return jwtToken;
        }
    }
}
