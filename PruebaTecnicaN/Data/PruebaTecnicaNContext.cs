using Microsoft.EntityFrameworkCore;
using PruebaTecnicaN.Models;


namespace PruebaTecnicaN.Data
{
    public class PruebaTecnicaNContext : DbContext
    {
        public PruebaTecnicaNContext(DbContextOptions<PruebaTecnicaNContext> options) 
            : base(options)
        {
        }
        public DbSet<PruebaTecnicaN.Models.Users> Users { get; set; } = default!;
        public DbSet<PruebaTecnicaN.Models.UserRoles> UserRoles { get; set; } = default!;
        public DbSet<PruebaTecnicaN.Models.Roles> Roles { get; set; } = default!;
        public DbSet<PruebaTecnicaN.Models.Procedures> Procedures { get; set; } = default!;
        public DbSet<PruebaTecnicaN.Models.Fields> Fields { get; set; } = default!;
        public DbSet<PruebaTecnicaN.Models.Datasets> Datasets { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRoles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);
        }
    }
}
