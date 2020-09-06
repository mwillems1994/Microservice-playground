using System.Security.Claims;
using System.Threading.Tasks;
using Playground.Nl.CustomerManagementAPI.Database.Context;
using Playground.Nl.CustomerManagementAPI.Services.Attributes;
using Playground.Nl.CustomerManagementAPI.Services.Helpers;

namespace Playground.Nl.CustomerManagementAPI.Services.Services
{
    [DiClass]
    public abstract class BaseService
    {
        private readonly CustomerManagementDBContext _db;
        internal readonly ClaimsPrincipal? _user;

        public BaseService(
            CustomerManagementDBContext db,
            IUserPrincipalAccessor userPrincipalAccessor)
        {
            _db = db;
            _user = userPrincipalAccessor?.User;
        }
        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}