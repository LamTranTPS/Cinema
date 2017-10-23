using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface ICinemaChainRepository: IRepository<CinemaChain>
    {
    }

    public class CinemaChainRepository : RepositoryBase<CinemaChain>, ICinemaChainRepository
    {
        public CinemaChainRepository() : base()
        {
        }
        public CinemaChainRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}