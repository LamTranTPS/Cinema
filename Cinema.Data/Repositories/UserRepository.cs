using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cinema.Data.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null, string[] includes = null);
    }

    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        public UserRepository() : base()
        {

        }
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationUser> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null, string[] includes = null)
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(u => u.UserName), out total, page, size, null, includes);
            }
            else
            {
                return GetListPaging(o => o.OrderBy(u => u.UserName), out total, page, size, u => u.UserName.ToUpper().Contains(searchKey.ToUpper()) || u.Email.Contains(searchKey.ToUpper()), includes);
            }
        }
     
    }
}