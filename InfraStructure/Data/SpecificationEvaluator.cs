using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Entities;

namespace InfraStructure.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> q, ISpecification<TEntity> specification)
        {
            var query = q.AsQueryable();

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            
            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPagingEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);

            if (specification.Includes != null && specification.Includes.Count > 0)
                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (specification.IncludeStrings != null && specification.IncludeStrings.Count > 0)
                query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
