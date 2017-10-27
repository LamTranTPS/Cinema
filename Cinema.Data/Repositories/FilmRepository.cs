using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface IFilmRepository: IRepository<Film>
    {
        IEnumerable<Film> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null);
    }

    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository() : base()
        {
        }

        public FilmRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public IEnumerable<Film> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null)
        {
            var includes = new string[] { "Schedules" };
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(f => f.Name), out total, page, size, null, includes);
            }
            else
            {
                return GetListPaging(o => o.OrderBy(f => f.Name), out total, page, size, f => (f.Name + f.Actor + f.Director + f.Genre).ToUpper().Contains(searchKey.ToUpper()), includes);
            }
        }
    }
}