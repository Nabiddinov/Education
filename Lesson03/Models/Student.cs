namespace Lesson03.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
