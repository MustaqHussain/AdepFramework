using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using Dwp.Adep.Framework.Management.IoC.ServiceLocation;
using Dwp.Adep.Framework.Management.DataServices.Repositories;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Data.Entity;
using Dwp.Adep.Framework.Management.DataServices.Models;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public class Repository<T> : IRepository<T> where T : class, IAuditable
    {
        protected readonly IObjectContext _objectContext;
        protected readonly IObjectSet<T> _objectSet;
        protected readonly IObjectSet<Audit> _objectSetAudit;
        protected readonly string _userName;

        public Repository(string userName, string userProxied, string appID, string level)
            : this(new NullObjectContext(), userName, userProxied, appID, level)
        {
        }

        public Repository(IObjectContext objectContext, string userName, string userProxied, string appID, string level)
        {
            if (!(objectContext is NullObjectContext))
            {
                _objectContext = objectContext;
            }
            else
            {
                _objectContext = SimpleServiceLocator.Instance.Get<IObjectContext>();
            }

            _objectSet = _objectContext.CreateObjectSet<T>();
            _objectSetAudit = _objectContext.CreateObjectSet<Audit>();
            _userName = userName;

        }

        #region All the Gets

        public IQueryable<T> GetQuery()
        {
            return _objectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public IEnumerable<T> GetAll(params string[] children)
        {
            return EagerQuery(children).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>> sortExpression, params string[] children)
        {
            var query = EagerQuery(children).OrderBy(sortExpression);
            return query.ToList();
        }

        public IEnumerable<T> Find(ISpecification<T> specification)
        {
            return specification.SatisfyingEntitiesFrom(GetQuery());
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression)
        {
            return Find(specification, sortExpression, true);
        }

        //OrderBy versions of Find
        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending)
        {
            var query = specification.SatisfyingEntitiesFrom(GetQuery());
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query;
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, params string[] children)
        {
            return Find(specification, sortExpression, true, children);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, Expression<Func<T, object>> sortExpression, bool isAscending, params string[] children)
        {
            var query = specification.SatisfyingEntitiesFrom(EagerQuery(children));
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query;
        }

        //Paging versions of Find
        public IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, int page, int pageSize)
        {
            return Find(specification, sortExpression, true, page, pageSize);
        }
        public IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, bool isAscending, int page, int pageSize)
        {
            var query = specification.SatisfyingEntitiesFrom(GetQuery());
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, int page, int pageSize, params string[] children)
        {
            return Find(specification, sortExpression, true, page, pageSize, children);
        }

        public IEnumerable<T> Find<U>(ISpecification<T> specification, Expression<Func<T, U>> sortExpression, bool isAscending, int page, int pageSize, params string[] children)
        {
            var query = specification.SatisfyingEntitiesFrom(EagerQuery(children));
            query = (isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression));
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _objectSet.Where(filter);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter, params string[] children)
        {
            return EagerQuery(children).Where(filter);

        }

        public T Single(ISpecification<T> specification)
        {
            return specification.SatisfyingEntityFrom(GetQuery());
        }

        public T Single(ISpecification<T> specification, params string[] children)
        {
            return specification.SatisfyingEntityFrom(EagerQuery(children));
        }

        public T Single(Expression<Func<T, bool>> filter)
        {
            return _objectSet.Single(filter);
        }

        public T Single(Expression<Func<T, bool>> filter, params string[] children)
        {
            return EagerQuery(children).Single(filter);
        }

        public T Find(Guid code)
        {
            return _objectSet.SingleOrDefault((e) => e.Code.Equals(code));
        }

        protected IQueryable<T> EagerQuery(params string[] children)
        {
            IQueryable<T> query = (IQueryable<T>)_objectSet;

            foreach (string child in children)
            {
                query = query.Include(child);

            }
            return query;
        }

        #endregion

        public int Count(ISpecification<T> specification)
        {
            return specification.SatisfyingEntitiesFrom(GetQuery()).Count();
        }

        #region Delete

        public void Delete(T entity)
        {
            //attach as modified as may already be in the context
            _objectSet.AttachAsModified(entity, true);
            _objectSet.DeleteObject(entity);
        }

        #endregion

        #region Update

        public void Update(T entity)
        {
            _objectSet.AttachAsModified(entity, true);

        }

        public void UpdateWith(T entity, T old)
        {
            _objectSet.AttachAsModified(entity, old, true);

        }

        #endregion

        #region Add

        public void Add(T entity)
        {
            _objectSet.AddObject(entity);
        }

        #endregion

        #region AuditLog

        private void AuditLog(Audit audit)
        {
            _objectSetAudit.AddObject(audit);
        }

        #endregion

    }
}
