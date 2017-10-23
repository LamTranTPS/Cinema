using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cinema.Data.Repositories
{
    public interface IQuartzScheduleRepository : IRepository<QuartzSchedule>
    {
        IEnumerable<QuartzSchedule> GetAll(string[] includes = null, Expression<Func<QuartzSchedule, bool>> expression = null);
        IEnumerable<QuartzSchedule> GetListPaging(out int total, int page = 0, int size = 50, string[] includes = null, string searchKey = "");
    }

    public class QuartzScheduleRepository : RepositoryBase<QuartzSchedule>, IQuartzScheduleRepository
    {
        public QuartzScheduleRepository() : base()
        {

        }
        public QuartzScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<QuartzSchedule> GetAll(string[] includes = null, Expression<Func<QuartzSchedule, bool>> expression = null)
            => GetAll(o => o.OrderBy(s => s.Name), includes, expression);
        public IEnumerable<QuartzSchedule> GetListPaging(out int total, int page = 0, int size = 50, string[] includes = null, string searchKey = "")
        {
            if (string.IsNullOrEmpty(searchKey))
            {
                return GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, null, includes);
            }
            else
            {
                return GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, s => s.Name.ToUpper().Contains(searchKey.ToUpper()), includes);
            }
        }
    }
}