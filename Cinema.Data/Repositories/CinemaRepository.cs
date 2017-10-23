using Cinema.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface ICinemaRepository: IRepository<Model.Models.Cinema>
    {
        IEnumerable<Model.Models.Cinema> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null);
    }

    public class CinemaRepository : RepositoryBase<Model.Models.Cinema>, ICinemaRepository
    {
        public CinemaRepository() : base()
        {

        }
        public CinemaRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Model.Models.Cinema> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null)
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(c => c.Name), out total, page, size, null, new string[] { "CinemaChain", "Location" });
            }
            else
            {
                return GetListPaging(o => o.OrderBy(c => c.Name), out total, page, size, c => c.Name.ToUpper().Contains(searchKey.ToUpper()) || c.Address.Contains(searchKey.ToUpper()), new string[] { "CinemaChain", "Location" });
            }
        }

    }
}