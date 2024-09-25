using Org.BouncyCastle.Asn1.Cms;
using static MyUni.Models.Entities.ProgramCard;

namespace MyUni.Models.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Time { get; set; }

        public ICollection<Question> Questions { get; set; }
        public class Question
        {
            public int Id { get; set; }
            public string question { get; set; }
            public string correctanswer { get; set; }
            public string img {  get; set; }
            public ICollection<inccorectanswer> IncorrectAnswers { get; set; }

        }
        public class inccorectanswer
        {
            public int Id { get; set; }

            public string InccorectAnswer { get; set; }
        }
    }
}
