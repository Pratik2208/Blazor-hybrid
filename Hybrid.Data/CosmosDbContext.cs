using Hybrid.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hybrid.Data
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasNoDiscriminator()
                .ToContainer("Notes")
                .HasPartitionKey(n => n.UserName)
                .HasKey(n => n.Id);

            modelBuilder.Entity<Note>()
                .Property(n => n.Id)
                .ToJsonProperty("id");
        }
    }
}
