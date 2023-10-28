namespace Lesson03.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int NumberOfModules { get; set; }
        public double TotalHours { get; set; }

        public virtual ICollection<CourseGroup> Courses { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public Subject()
        {
            Assignments = new List<Assignment>();
            Courses = new List<CourseGroup>();
        }
    }
}
