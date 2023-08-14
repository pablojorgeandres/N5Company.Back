using Microsoft.EntityFrameworkCore;
using N5Company.Entities;

namespace N5Company.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.PermissionTypes)
                    .WithMany(e => e.Permission)
                    .HasForeignKey(e => e.TipoPermiso);
            });

            modelBuilder.Entity<PermissionType>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PermissionType>().HasData(
                new PermissionType { Id = 1, Descripcion = "Admin" },
                new PermissionType { Id = 2, Descripcion = "User" }
            );
        }
    }
}
