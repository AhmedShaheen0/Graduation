using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Graduation.Models.Activity;
using Graduation.Models.Auth;

namespace Graduation
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ActivityModel>()
            .HasOne(a => a.User)
            .WithMany(u => u.Activities)
            .HasForeignKey(a => a.Userid);




            base.OnModelCreating(builder);
        }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<PlaceModel> Places { get; set; }

    }
}