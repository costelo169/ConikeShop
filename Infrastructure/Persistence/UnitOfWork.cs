using ApplicationCore.Interfaces;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConikeShopContext _context;

        public UnitOfWork(ConikeShopContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }
        public IProductRepository Products {get; set;}

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}