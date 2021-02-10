using Microsoft.EntityFrameworkCore;

namespace Crud.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        //dbset
        public DbSet<Dish> Dishes { get; set; }

    }

}