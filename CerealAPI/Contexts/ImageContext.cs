using Microsoft.EntityFrameworkCore;

using CerealAPI.Models;

namespace CerealAPI.Contexts
{
    public partial class ImageContext : DbContext
    {
        public ImageContext()
        {
        }

        public ImageContext(DbContextOptions<ImageContext> options)
        : base(options)
        {
        }

        public virtual DbSet<ImageEntry> Images { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=CerealDatabase;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30;" +
                    "Encrypt=False;" +
                    "Trust Server Certificate=False;" + 
                    "Application Intent=ReadWrite;" +
                    "Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ImageEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.CerealId).IsRequired();
                entity.Property(e => e.Path).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
