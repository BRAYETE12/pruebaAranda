using Application.Common.Interfaces;
using Domain.Common;
using Domain.Models;
using Domain.Models.TablasReferencia;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly DateTime _currentDateTime;
        private readonly CurrentUser _user;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {

            _currentDateTime = DateTime.Now;
            _user = currentUserService.User;
        }

        public DbSet<Product> Product => Set<Product>();
        public DbSet<Category> Category => Set<Category>();

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UserCreate = _user.UserName;
                        entry.Entity.CreatedAt = _currentDateTime;
                        entry.Entity.UserUpdate = _user.UserName;
                        entry.Entity.UpdatedAt = _currentDateTime;
                        entry.Entity.Estado = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UserUpdate = _user.UserName;
                        entry.Entity.UpdatedAt = _currentDateTime;
                        break;
                }
            }

            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
