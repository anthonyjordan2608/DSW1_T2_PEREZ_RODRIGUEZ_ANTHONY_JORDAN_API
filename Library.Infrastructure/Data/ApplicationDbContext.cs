using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ISBN).IsUnique();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Stock)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            // Configuración de Loan
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Active");

                entity.Property(e => e.LoanDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                // Relación con Book
                entity.HasOne(e => e.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(e => e.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
