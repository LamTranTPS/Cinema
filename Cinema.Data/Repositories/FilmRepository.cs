using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface IFilmRepository
    {
    }

    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}