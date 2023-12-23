﻿using miniWms.Application.Functions.Employees.Commands.CreateEmployee;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;

namespace miniWms.Application.Contracts
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByEmailAddressAsync(string email);
        Task<Employee> CreateEmployeeAsync(CreateEmployeeCommand employee);
        Task<JwtToken> LoginEmployeeAsync(LoginEmployeeModel employee);
        Task<bool> ChangePassword(Guid employeeId, string newPassword);
    }
}
