using Microsoft.EntityFrameworkCore;

namespace Login.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}

        // our dbsets
        public DbSet<User> Users { get; set; }
    }
}
