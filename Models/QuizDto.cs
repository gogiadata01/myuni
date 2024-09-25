using static MyUni.Models.Entities.Quiz;

namespace MyUni.Models
{
    public class QuizDto
    {
        public string Time { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

        public class QuestionDto
        {
            public string question { get; set; }
            public string correctanswer { get; set; }
            public string img { get; set; } = null;
            public ICollection<inccorectanswerDto> IncorrectAnswers { get; set; }

        }
        public class inccorectanswerDto
        {

            public string InccorectAnswer { get; set; }
        }
    }

}
