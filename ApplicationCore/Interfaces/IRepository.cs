using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T GetBy(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(ISpecification<T> spec);
        int Count(ISpecification<T> spec);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}