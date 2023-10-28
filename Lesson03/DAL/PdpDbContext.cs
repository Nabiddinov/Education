using Lesson03.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson03.DAL
{
    public class PdpDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<CourseGroup> Groups { get; set; }

        public PdpDbContext(DbContextOptions<PdpDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .ToTable(nameof(Student));
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .HasMaxLength(255);
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .HasConstraintName("Student_Enrollment_FK");

            modelBuilder.Entity<Teacher>()
                .ToTable(nameof(Teacher));
            modelBuilder.Entity<Teacher>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Teacher>()
                .Property(t => t.FirstName)
                .HasMaxLength(255);
            modelBuilder.Entity<Teacher>()
                .Property(t => t.LastName)
                .HasMaxLength(255);
            modelBuilder.Entity<Teacher>()
                .Property(t => t.PhoneNumber)
                .HasColumnName("Phone");
            modelBuilder.Entity<Teacher>()
                .Property(t => t.HourlyRate)
                .HasColumnType("money");
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Assignments)
                .WithOne(a => a.Teacher)
                .HasForeignKey(a => a.TeacherId)
                .HasConstraintName("Teacher_Assignment_FK");
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(cg => cg.Teacher)
                .HasForeignKey(cg => cg.TeacherId)
                .HasConstraintName("Teacher_Course_FK");

            modelBuilder.Entity<Subject>()
                .ToTable(nameof(Subject));
            modelBuilder.Entity<Subject>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Subject>()
                .Property(s => s.Price)
                .HasColumnType("money");
            modelBuilder.Entity<Subject>()
                .Property(s => s.Title)
                .HasMaxLength(255);
            modelBuilder.Entity<Subject>()
                .Property(s => s.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Courses)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .HasConstraintName("Subject_Course_FK");
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Assignments)
                .WithOne(a => a.Subject)
                .HasForeignKey(a => a.SubjectId)
                .HasConstraintName("Subject_Assignment_FK");

            modelBuilder.Entity<Enrollment>()
                .ToTable(nameof(Enrollment));
            modelBuilder.Entity<Enrollment>()
                .HasAlternateKey(e => new
                {
                    e.GroupId,
                    e.StudentId
                });
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Enrollments)
                .HasForeignKey(e => e.GroupId)
                .HasConstraintName("Enrollment_Course_FK");

            modelBuilder.Entity<CourseGroup>()
                .ToTable("Course_Group");
            modelBuilder.Entity<CourseGroup>()
                .HasKey(cg => cg.Id);
            modelBuilder.Entity<CourseGroup>()
                .Property(cg => cg.Name)
                .HasMaxLength(255);
            modelBuilder.Entity<CourseGroup>()
                .Property(cg => cg.StartDate)
                .HasColumnType("date");
            modelBuilder.Entity<CourseGroup>()
                .Property(cg => cg.ExpectedFinishDate)
                .HasColumnType("date")
                .IsRequired(false);
            modelBuilder.Entity<CourseGroup>()
                .Property(cg => cg.ActualFinishDate)
                .HasColumnType("date")
                .IsRequired(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}
