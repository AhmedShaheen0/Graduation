using Graduation.Models.Activity;
using Graduation.Models.Auth;
using Graduation.Models.ML;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Graduation
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ActionModel> Actions { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<PlaceModel> Places { get; set; }
    }
}