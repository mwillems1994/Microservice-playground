using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Playground.Nl.CustomerManagementAPI.Services.Helpers;

namespace Playground.Nl.CustomerManagementAPI.Nl.Helpers
{
    public class UserPrincipalAccessor : IUserPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPrincipalAccessor(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}
