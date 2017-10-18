using Cinema.Data.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
        IEnumerable<IdentityRole> GetAll(string[] includes = null);
        IEnumerable<IdentityRole> GetByUser(string userId, string[] includes = null);
    }

    public class RoleRepository : RepositoryBase<IdentityRole>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<IdentityRole> GetAll(string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.Name), includes);
        public IEnumerable<IdentityRole> GetByUser(string userId, string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.Name), includes, r => r.Users.Select(u => u.UserId).Contains(userId));
    }
}