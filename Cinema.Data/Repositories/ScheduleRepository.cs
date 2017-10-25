using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Data.Repositories
{
    public interface IScheduleRepository: IRepository<Schedule>
    {
        IEnumerable<Schedule> GetByCinema(int cinemaId, out int total, int page = 0, int size = 50);
        IEnumerable<Schedule> GetByFilm(int filmId, out int total, int page = 0, int size = 50);
        IEnumerable<Schedule> GetByCinemaAndFilm(int cinemaId, int filmId, out int total, int page = 0, int size = 50);
    }

    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository() : base()
        {

        }
        public ScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public IEnumerable<Schedule> GetByCinema(int cinemaId, out int total, int page = 0, int size = 50)
            => GetListPaging(o => o.OrderBy(s => s.FilmID).ThenBy(s => s.DateTime), out total, page, size, s => s.CinemaID == cinemaId, new string[] { "Cinema", "Film" });
        public IEnumerable<Schedule> GetByFilm(int filmId, out int total, int page = 0, int size = 50)
            => GetListPaging(o => o.OrderBy(s => s.CinemaID).ThenBy(s => s.DateTime), out total, page, size, s => s.FilmID == filmId, new string[] { "Cinema", "Film" });
        public IEnumerable<Schedule> GetByCinemaAndFilm(int cinemaId, int filmId, out int total, int page = 0, int size = 50)
            => GetListPaging(o => o.OrderBy(s => s.DateTime), out total, page, size, s => s.CinemaID == cinemaId && s.FilmID == filmId, new string[] { "Cinema", "Film" });
    }
}