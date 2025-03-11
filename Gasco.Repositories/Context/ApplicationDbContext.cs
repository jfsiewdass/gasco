using Microsoft.EntityFrameworkCore;

using Gasco.Common.Entities;
using Microsoft.Extensions.Configuration;

namespace Gasco.Repositories.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Gasco")!;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, sqlServerOptionsAction: sqloptions =>
            {
                sqloptions.EnableRetryOnFailure();
            }); base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users").HasKey(x => x.Id);
                
                entity.Property(x => x.Id).HasColumnName("Id");
                entity.Property(x => x.Password).HasColumnName("Password");
                entity.Property(x => x.Name).HasColumnName("Name");
                entity.Property(x => x.Email).HasColumnName("Email");
            });
        }

        public DbSet<User> Users { get; set; }
    }
}
