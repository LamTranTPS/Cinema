﻿using Cinema.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface ICinemaRepository: IRepository<Model.Models.Cinema>
    {
        IEnumerable<Model.Models.Cinema> GetByFilm(int filmId);
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

        public IEnumerable<Model.Models.Cinema> GetByFilm(int filmId)
            => GetAll(o => o.OrderBy(c => c.Name), new string[] { "Schedules" }, c => c.Schedules.Where(s => s.FilmID == filmId).Count() > 0);

        public IEnumerable<Model.Models.Cinema> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = null)
        {
            var includes = new string[] { "CinemaChain", "Location" };
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(c => c.Name), out total, page, size, null, includes);
            }
            else
            {
                return GetListPaging(o => o.OrderBy(c => c.Name), out total, page, size, c => (c.Name + c.Address).ToUpper().Contains(searchKey.ToUpper()), includes);
            }
        }

    }
}