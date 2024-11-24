namespace UniSpace.Data.Models
{
    public class Proffesseur : UserInfo
    {
        public List<Subject> TaughtSubjects { get; set; }

        public Proffesseur()
        {
            TaughtSubjects = new List<Subject>();
        }
    }
}
