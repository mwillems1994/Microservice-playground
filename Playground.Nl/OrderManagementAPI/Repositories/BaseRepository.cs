using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playground.Nl.OrderManagementAPI.Nl.Context;
using Playground.Nl.OrderManagementAPI.Nl.Helpers;
using Playground.Nl.OrderManagementAPI.Nl.Attributes;

namespace Playground.Nl.OrderManagementAPI.Nl.Repositories
{
    [DiClass]
    public abstract class BaseRepository<T>
        where T : class
    {
        internal readonly OrderManagementDbContext _db;
        internal readonly ClaimsPrincipal? _user;
        internal IPrincipal? User => _user;

        public BaseRepository(OrderManagementDbContext db,
            IUserPrincipalAccessor userPrincipalAccessor)
        {
            _db = db;
            _user = userPrincipalAccessor.User;
        }

        protected IQueryable<T> All => _db.Set<T>();

        protected IQueryable<T> AllNoTracking => All.AsNoTracking();

        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task RefreshAsync(T entity)
        {
            await _db.Entry(entity).ReloadAsync();
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}