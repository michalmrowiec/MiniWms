﻿using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail
{
    public class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, Employee>
    {
        private readonly IEmployeesRepository _employeesRepository;
        public GetEmployeeByEmailQueryHandler(IEmployeesRepository userRepository)
        {
            _employeesRepository = userRepository;
        }

        public async Task<Employee> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeesRepository.GetEmployeeByEmailAddressAsync(request.EmployeeEmailAddress);
            return employee;
        }
    }
}
