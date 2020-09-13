using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Playground.Nl.ProductManagementAPI.Services.Helpers;

namespace Playground.Nl.ProductManagementAPI.API.Helpers
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