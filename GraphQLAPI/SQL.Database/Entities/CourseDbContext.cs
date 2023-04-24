using Microsoft.EntityFrameworkCore;

namespace SQL.Database.Entities
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

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseStudent> CourseStudents { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString.StartsWith("inmemory://"))
            {
                optionsBuilder.UseInMemoryDatabase(_connectionString);
                optionsBuilder.ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning));

            }
            else
            {
                optionsBuilder.UseSqlServer(_connectionString)
                    .LogTo(s => Console.WriteLine(s));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_Course_Instructor");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_CourseStudent_Students");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Salary)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Review).HasColumnType("text");

                entity.Property(e => e.Start)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Rating_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Rating_Students");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gpa).HasColumnName("GPA");

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
