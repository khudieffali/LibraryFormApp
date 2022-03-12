using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibraryApp.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<PublishHouse> PublishHouses { get; set; }
        public virtual DbSet<PublishHouseToBook> PublishHouseToBooks { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
        public virtual DbSet<ReaderToBook> ReaderToBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HOME-PC;Initial Catalog=Library;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Books__AuthorId__267ABA7A");
            });

            modelBuilder.Entity<PublishHouse>(entity =>
            {
                entity.ToTable("PublishHouse");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<PublishHouseToBook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.PublishHouseToBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__PublishHo__BookI__2B3F6F97");

                entity.HasOne(d => d.PublishHouse)
                    .WithMany(p => p.PublishHouseToBooks)
                    .HasForeignKey(d => d.PublishHouseId)
                    .HasConstraintName("FK__PublishHo__Publi__2C3393D0");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(100);
            });

            modelBuilder.Entity<ReaderToBook>(entity =>
            {
                entity.ToTable("ReaderToBook");

                entity.Property(e => e.GetBookDate).HasColumnType("date");

                entity.Property(e => e.ReturnBookDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReaderToBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__ReaderToB__BookI__31EC6D26");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.ReaderToBooks)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__ReaderToB__Reade__30F848ED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
