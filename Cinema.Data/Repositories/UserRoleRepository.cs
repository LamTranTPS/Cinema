using Cinema.Data.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        IEnumerable<UserRole> GetAll(string[] includes = null);
        IEnumerable<UserRole> GetByUser(int userId, string[] includes = null);
        bool UpdateByUser(List<UserRole> userRoles);
    }

    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository() : base()
        {
        }
        public UserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<UserRole> GetAll(string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.RoleId), includes);
        public IEnumerable<UserRole> GetByUser(int userId, string[] includes = null)
            => GetAll(o => o.OrderBy(r => r.RoleId), includes, r => r.UserId == userId);

        public bool UpdateByUser(List<UserRole> userRoles)
        {
            try
            {
                if (userRoles.Count > 0)
                {
                    var userId = userRoles[0].UserId;
                    Delete(r => r.UserId == userId);
                    AddRange(userRoles);
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}