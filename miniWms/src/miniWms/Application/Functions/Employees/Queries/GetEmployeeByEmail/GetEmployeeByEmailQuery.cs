using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Employees.Queries.GetEmployeeByEmail
{
    public record GetEmployeeByEmailQuery(string EmployeeEmailAddress) : IRequest<Employee>;
}
