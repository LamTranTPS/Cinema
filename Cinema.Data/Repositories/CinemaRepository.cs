using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface ICinemaRepository: IRepository<Model.Models.Cinema>
    {
        IEnumerable<Model.Models.Cinema> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null);
        Model.Models.Cinema Get(string id);
        bool Delete(string id);
    }

    public class CinemaRepository : RepositoryBase<Model.Models.Cinema>, ICinemaRepository
    {
        public CinemaRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public Model.Models.Cinema Get(string id)
        {
            return Get(c => c.ID == id);
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

        public bool Delete(string id)
        {
            var cinema = Get(id);
            return Delete(cinema);
        }
    }
}