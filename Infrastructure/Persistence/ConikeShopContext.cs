using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ConikeShopContext : DbContext
    {
        public ConikeShopContext(DbContextOptions<ConikeShopContext> options) : base(options)
        {
        }

        public DbSet<Product> Products{ get; set; }
    }
}