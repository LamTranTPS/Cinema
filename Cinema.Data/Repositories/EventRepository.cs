using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null);
    }

    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository() : base()
        {
        }

        public EventRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<Event> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null)
        {
            var includes = new string[] { "CinemaChain" };
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(f => f.Name), out total, page, size, null, includes);
            }
            else
            {
                return GetListPaging(o => o.OrderBy(f => f.Name), out total, page, size, e => e.Name.ToUpper().Contains(searchKey.ToUpper()), includes);
            }
        }
    }
}