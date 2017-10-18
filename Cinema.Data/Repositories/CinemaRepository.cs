using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface ICinemaRepository
    {
    }

    public class CinemaRepository : RepositoryBase<Model.Models.Cinema>, ICinemaRepository
    {
        public CinemaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}