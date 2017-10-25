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
        new QuartzSchedule Get(int id);
        IEnumerable<QuartzSchedule> GetAll(bool onlyStart = false);
        IEnumerable<QuartzSchedule> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = "", bool onlyStart = false);
        bool UpdateStatus(int id, bool status);
    }

    public class QuartzScheduleRepository : RepositoryBase<QuartzSchedule>, IQuartzScheduleRepository
    {
        public QuartzScheduleRepository() : base()
        {

        }
        public QuartzScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public new QuartzSchedule Get(int id)
        {
            return Get(s => s.ID == id, new string[] { "Job" });
        }

        public IEnumerable<QuartzSchedule> GetAll(bool onlyStart = false)
        {
            var includes = new string[] { "Job" };
            return onlyStart ?
                GetAll(o => o.OrderBy(s => s.Name), includes, s => s.Status) :
                GetAll(o => o.OrderBy(s => s.Name), includes);
        }
        public IEnumerable<QuartzSchedule> GetListPaging(out int total, int page = 0, int size = 50, string searchKey = "", bool onlyStart = false)
        {
            var includes = new string[] { "Job" };
            if (string.IsNullOrEmpty(searchKey))
            {
                return onlyStart ?
                        GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, s => s.Status, includes) :
                        GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, null, includes);
            }
            else
            {
                return onlyStart ?
                    GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, s => s.Name.ToUpper().Contains(searchKey.ToUpper()) && s.Status, includes):
                    GetListPaging(o => o.OrderBy(s => s.Name), out total, page, size, s => s.Name.ToUpper().Contains(searchKey.ToUpper()), includes);
            }
        }

        public bool UpdateStatus(int id, bool status)
        {
            var schedule = Get(id);
            schedule.Status = status;
            return Update(schedule);
        }
    }
}