using Cinema.Data.Infrastructure;
using Cinema.Model.Models;

namespace Cinema.Data.Repositories
{
    public interface IScheduleRepository: IRepository<Schedule>
    {
    }

    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository() : base()
        {
        }
        public ScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}