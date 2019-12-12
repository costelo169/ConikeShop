using System.Linq;
using ApplicationCore.Interfaces;
namespace Infrastructure.Persistence
{
    public class SpecificationEvaluator<T>
    {
        public static IQueryable<T> Evaluate(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.IsPaginated)
            {
                query = query.Skip((spec.PageIndex - 1) * spec.PageSize).Take(spec.PageSize);
            }

            return query;
        }
    }
}