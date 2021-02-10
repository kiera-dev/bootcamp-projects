using Microsoft.EntityFrameworkCore;

namespace ChefDish.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        //dbset
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }

    }

}
