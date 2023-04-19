using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace Common.SqlRepository.Entities
{
    public partial class CourseDbContext : DbContext
    {
        private readonly string _connectionString;
        public CourseDbContext(string connection)
        {
            _connectionString = connection ?? throw new ArgumentNullException(connection);
        }

        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Section> Sections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString.StartsWith("inmemory://"))
            {
                optionsBuilder.UseInMemoryDatabase(_connectionString);
                optionsBuilder.ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning));

            }
            else
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Instructor).HasMaxLength(50);

                entity.Property(e => e.Level).HasMaxLength(50);

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lecture");

                entity.Property(e => e.Discriminator).HasMaxLength(50);

                entity.Property(e => e.Instructions).HasMaxLength(300);

                entity.Property(e => e.MediaUrl).HasMaxLength(2048);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Questions).HasMaxLength(300);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Lecture_Course");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Lecture_Section");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CourseId, e.StudentName });

                entity.ToTable("Rating");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Course");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Section_Table_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
