using Microsoft.EntityFrameworkCore;

namespace Wedding.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}

        // our dbsets
        public DbSet<User> Users { get; set; }
        public DbSet<WeddingPlan> WeddingPlans { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
