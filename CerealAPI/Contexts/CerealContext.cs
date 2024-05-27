using Microsoft.EntityFrameworkCore;

using CerealAPI.Models;

namespace CerealAPI.Contexts
{
    public partial class CerealContext : DbContext
    {
        public CerealContext()
        {
        }

        public CerealContext(DbContextOptions<CerealContext> options)
        : base(options)
        {
        }

        public virtual DbSet<CerealProduct> Cereals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CerealDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CerealProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Manufacturer).IsRequired();
                entity.Property(e => e.CerealType).IsRequired();
                entity.Property(e => e.Calories).IsRequired();
                entity.Property(e => e.Protein).IsRequired();
                entity.Property(e => e.Fat).IsRequired();
                entity.Property(e => e.Sodium).IsRequired();
                entity.Property(e => e.Fiber).IsRequired();
                entity.Property(e => e.Carbohydrates).IsRequired();
                entity.Property(e => e.Sugars).IsRequired();
                entity.Property(e => e.Potassium).IsRequired();
                entity.Property(e => e.Vitamins).IsRequired();
                entity.Property(e => e.Shelf).IsRequired();
                entity.Property(e => e.Weight).IsRequired();
                entity.Property(e => e.Cups).IsRequired();
                entity.Property(e => e.Rating).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
