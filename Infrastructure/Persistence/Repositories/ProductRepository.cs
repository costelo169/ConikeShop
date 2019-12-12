using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ConikeShopContext context) : base(context)
        {
            
        }
        public IEnumerable<string> GetGenres()
        {
            return Context.Products.Select(m => m.Genre).Distinct().ToList();


        }
        protected new ConikeShopContext Context
        {
            get { return base.Context as ConikeShopContext;}
        }
    }
}