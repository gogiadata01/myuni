using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUni.Data;
using MyUni.Models;
using MyUni.Models.Entities;

namespace MyUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public QuizController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult GetAllQuizzes()
        {
            var quizzes = dbContext.MyQuiz
                .Include(card => card.Questions)
                .ThenInclude(incorectanswer => incorectanswer.IncorrectAnswers)
                .ToList();

            return Ok(quizzes);
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public IActionResult GetQuizById(int id)
        {
            var quiz = dbContext.MyQuiz
                .Include(card => card.Questions)
                .ThenInclude(incorectanswer => incorectanswer.IncorrectAnswers)
                .FirstOrDefault(card => card.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // POST: api/Quiz
        [HttpPost]
        public IActionResult PostQuiz([FromBody] QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Log incoming data
            Console.WriteLine("Received QuizDto:");
            Console.WriteLine($"Time: {quizDto.Time}");
            foreach (var question in quizDto.Questions)
            {
                Console.WriteLine($"Question: {question.question}");
                Console.WriteLine($"Correct Answer: {question.correctanswer}");
                foreach (var incorrectAnswer in question.IncorrectAnswers)
                {
                    Console.WriteLine($"Incorrect Answer: {incorrectAnswer.InccorectAnswer}");
                }
            }

            var quizEntity = new Quiz
            {
                Time = quizDto.Time,
                Questions = quizDto.Questions?.Select(q => new Quiz.Question
                {
                    question = q.question,
                    correctanswer = q.correctanswer,
                    img = q.img,
                    IncorrectAnswers = q.IncorrectAnswers?.Select(ia => new Quiz.inccorectanswer
                    {
                        InccorectAnswer = ia.InccorectAnswer
                    }).ToList()
                }).ToList()
            };

            dbContext.MyQuiz.Add(quizEntity);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetQuizById), new { id = quizEntity.Id }, quizEntity);
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            // Find the Quiz by ID, including any related entities if necessary
            var quiz = dbContext.MyQuiz
                .Include(q => q.Questions)
                    .ThenInclude(q => q.IncorrectAnswers)
                .FirstOrDefault(q => q.Id == id);

            // If the Quiz is not found, return a 404 Not Found response
            if (quiz == null)
            {
                return NotFound();
            }

            // Remove the Quiz from the database
            dbContext.MyQuiz.Remove(quiz);
            dbContext.SaveChanges();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }

        // GET: api/Quiz/time/{time}
        [HttpGet("time/{time}")]
        public IActionResult GetQuizByTime(string time)
        {
            try
            {
                // Decode the time parameter
                time = Uri.UnescapeDataString(time);
                Console.WriteLine("Received time: " + time);  // Log the received time

                var quizzes = dbContext.MyQuiz
                    .Include(card => card.Questions)
                    .ThenInclude(incorrectAnswer => incorrectAnswer.IncorrectAnswers)
                    .Where(quiz => quiz.Time == time) // Filter by time
                    .ToList();

                if (!quizzes.Any())
                {
                    return NotFound(new { Message = "No quizzes found for the specified time." });
                }

                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while retrieving the quiz.", Error = ex.Message });
            }
        }


    }
}
