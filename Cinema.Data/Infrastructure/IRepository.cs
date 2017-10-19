using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cinema.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        bool AddRange(IEnumerable<T> entitys);

        bool Update(T entity);

        bool Delete(T entity);

        bool Delete(int id);

        bool Delete(Expression<Func<T, bool>> expression);

        T Get(int id);

        T Get(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null, Expression<Func<T, bool>> expression = null);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetListPaging(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, out int total, int page = 0, int size = 50, Expression<Func<T, bool>> expression = null, string[] includes = null);

        int Count(Expression<Func<T, bool>> expression = null);

        bool Contains(Expression<Func<T, bool>> expression);

        bool Contains(int id);

        bool Contains(string id);
    }
}