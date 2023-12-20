using System.Security.Claims;

namespace miniWms.Api.Services
{
    public interface IUserContextService
    {
        Guid? GetUserId { get; }
        ClaimsPrincipal? User { get; }
    }
}
