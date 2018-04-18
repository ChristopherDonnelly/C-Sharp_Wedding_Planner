using Microsoft.EntityFrameworkCore;
 
namespace Wedding_Planner.Models
{
    public class WeddingPlannerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingPlannerContext(DbContextOptions<WeddingPlannerContext> options) : base(options) { }

         public DbSet<User> user { get; set; }
         public DbSet<WeddingPlan> weddingplan { get; set; }
         public DbSet<WeddingInfo> weddinginfo { get; set; }
    }
}