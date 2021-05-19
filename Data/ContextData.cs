using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ContextData : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public ContextData(DbContextOptions<ContextData> options) : base(options) {}         
        //public DbSet<AppUser> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageStatus> PackageStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   

            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            modelBuilder.Entity<Package>()
                .HasOne(p => p.DeliveryMan)
                .WithMany(p => p.PackagesInDelivery)
                .HasForeignKey(p => p.DeliveryManId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackageStatus>()
                .HasOne(p => p.Package)
                .WithMany(p => p.Statuses)
                .HasForeignKey(p => p.PackageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}