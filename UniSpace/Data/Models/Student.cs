
namespace UniSpace.Data.Models
{
    public class Student : UserInfo
    {
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int Course { get; set; }
    }
}
