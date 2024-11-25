using UniSpace.Data.Models.Enums;

namespace UniSpace.Data.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CoursesEnum> Courses { get; set; }

    }
}
