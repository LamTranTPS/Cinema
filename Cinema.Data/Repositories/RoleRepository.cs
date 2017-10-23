using Cinema.Data.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetAll(string[] includes = null);
        IEnumerable<Role> GetByUser(int userId, string[] includes = null);
    }

    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository() : base()
        {
        }
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Role> GetAll(string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.Name), includes);
        public IEnumerable<Role> GetByUser(int userId, string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.Name), includes, r => r.Users.Select(u => u.UserId).Contains(userId));
    }
}