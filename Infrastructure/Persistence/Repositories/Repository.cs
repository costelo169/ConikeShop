using System.Linq;

using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        protected DbContext Context => _context;
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(System.Collections.Generic.IEnumerable<T> entities)
        {
            _context.AddRange(entities);
        }

        public int Count(ISpecification<T> spec)
        {
            return ApplySpecification(spec).Count();
        }

        public System.Collections.Generic.IEnumerable<T> Find(ISpecification<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public System.Collections.Generic.IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetBy(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(System.Collections.Generic.IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            return SpecificationEvaluator<T>.Evaluate(query, spec);
        }
    }
}