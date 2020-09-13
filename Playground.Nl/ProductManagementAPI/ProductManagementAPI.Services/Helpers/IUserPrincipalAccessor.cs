using System.Security.Claims;

namespace Playground.Nl.ProductManagementAPI.Services.Helpers
{
    public interface IUserPrincipalAccessor
    {
        ClaimsPrincipal? User { get; }
    }
}