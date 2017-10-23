using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface IFilmRepository: IRepository<Film>
    {
    }

    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository() : base()
        {
        }

        public FilmRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}