using FASTUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FASTUniversity.DAL
{
    public class SchoolContext : DbContext
    {
      
        public SchoolContext() : base("SchoolContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>().HasMany(x => x.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID").MapLeftKey("InstrutorID").ToTable("CourseInstructor"));

            modelBuilder.Entity<Instructor>()
                .HasOptional(model => model.OfficeAssignment)
                .WithRequired(x => x.Instructor);
        }

        public System.Data.Entity.DbSet<FASTUniversity.Models.Department> Departments { get; set; }
    }
}