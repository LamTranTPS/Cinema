using Cinema.Data.Infrastructure;
using Cinema.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cinema.Data.Repositories
{
    public interface IQuartzJobRepository : IRepository<QuartzJob>
    {
        IEnumerable<QuartzJob> GetAll(string[] includes = null, Expression<Func<QuartzJob, bool>> expression = null);
    }

    public class QuartzJobRepository : RepositoryBase<QuartzJob>, IQuartzJobRepository
    {
        public QuartzJobRepository() : base()
        {

        }
        public QuartzJobRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<QuartzJob> GetAll(string[] includes = null, Expression<Func<QuartzJob, bool>> expression = null)
            => GetAll(o => o.OrderBy(j => j.Name), includes, expression);
    }
}