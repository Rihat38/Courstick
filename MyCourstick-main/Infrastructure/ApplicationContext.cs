using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Courstick.Core.Models;

namespace Courstick.Infrastructure
{
    public class ApplicationContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubType> SubTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>().HasMany(c => c.Author).WithMany(c => c.AuthorOf).UsingEntity("AuthorOfCourse");
            modelBuilder.Entity<Course>().HasMany(c => c.Users).WithMany(c => c.Courses).UsingEntity("UserAndCourse");
        }
    }

    public class Seeding
    {
        private readonly RoleManager<Role> _roleManager;

        public Seeding(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            if (!await _roleManager.RoleExistsAsync("defaultUser"))
            {
                await _roleManager.CreateAsync(new Role("defaultUser"));
            }
        }
    }
}
