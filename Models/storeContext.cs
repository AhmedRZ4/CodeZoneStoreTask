using Microsoft.EntityFrameworkCore;
namespace CodeZoneStoreTask.Models
{
    public class storeContext:DbContext
    {
        public storeContext(DbContextOptions<storeContext> options) : base(options)
        {

        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sell> Sells { get; set; }
    }
}
