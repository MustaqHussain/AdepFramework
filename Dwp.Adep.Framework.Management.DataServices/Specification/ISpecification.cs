using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public interface ISpecification<TEntity>  
    {
        TEntity SatisfyingEntityFrom(IQueryable<TEntity> query);
        IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query);

        ISpecification<TEntity> And(ISpecification<TEntity> specification);


        ISpecification<TEntity> And(Expression<Func<TEntity, bool>> predicate);


        ISpecification<TEntity> Or(ISpecification<TEntity> specification);


        ISpecification<TEntity> Or(Expression<Func<TEntity, bool>> predicate);

        Expression<Func<TEntity, bool>> Predicate { get; set; } 
    }
}
