using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] children);
        IEnumerable<T> GetAll(Expression<Func<T, object>> sortExpression, params string[] children);
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        IEnumerable<T> Find(ISpecification<T> specification);
        IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression);
        IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, params string[] children);
        IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, int page, int pageSize);
        IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, int page, int pageSize, params string[] children);
        IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending);
        IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending, params string[] children);
        IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, bool isAscending, int page, int pageSize);
        IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, bool isAscending, int page, int pageSize, params string[] children);
        IEnumerable<T> Find(Expression<Func<T, bool>> filter, params string[] children);
        T Single(Expression<Func<T, bool>> filter);
        T Single(ISpecification<T> specification);
        T Single(Expression<Func<T, bool>> filter, params string[] children);
        T Single(ISpecification<T> specification, params string[] children);
        int Count(ISpecification<T> specification);
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
        void UpdateWith(T entity, T old);
    }
}
