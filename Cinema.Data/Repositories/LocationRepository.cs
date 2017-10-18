using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface ILocationRepository: IRepository<Location>
    {
    }

    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}