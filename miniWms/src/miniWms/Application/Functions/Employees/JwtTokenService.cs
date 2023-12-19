using Microsoft.IdentityModel.Tokens;
using miniWms.Domain.Entities;
using miniWms.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace miniWms.Application.Functions.Employees
{
    public class JwtTokenService
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public JwtTokenService(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(Employee employee)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                new(ClaimTypes.Name, employee.FirstName),
                new(ClaimTypes.Surname, employee.LastName),
                new(ClaimTypes.Role, employee.RoleId),
                new(ClaimTypes.Email, employee.EmailAddress),
                new(ClaimTypes.MobilePhone, employee.PhoneNumber),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiress = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDaysForNormalLogin);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expiress,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
