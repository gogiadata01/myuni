using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUni.Data;
using MyUni.Models.Entities;
using MyUni.Models;

namespace MyUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramCardController : ControllerBase

    {
        private readonly ApplicationDbContext dbContext;
        public ProgramCardController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult GetAllProgramCards()
        {
            var allProgramCards = dbContext.MyprogramCard
                .Include(card => card.Fields)
                    .ThenInclude(field => field.ProgramNames)
                        .ThenInclude(program => program.CheckBoxes)
                .ToList(); 

            return Ok(allProgramCards);
        }

        [HttpGet("{id}")]
        public IActionResult GetProgramCardById(int id)
        {
            var programCard = dbContext.MyprogramCard
                .Include(card => card.Fields)
                    .ThenInclude(field => field.ProgramNames)
                        .ThenInclude(program => program.CheckBoxes)
                .FirstOrDefault(card => card.Id == id);

            if (programCard == null)
            {
                return NotFound();
            }

            return Ok(programCard);
        }
        [HttpGet("byProgramName/{programname}")]
        public IActionResult GetProgramCardByProgramName(string programname)
        {
            var programCard = dbContext.MyprogramCard
                .Include(card => card.Fields)
                    .ThenInclude(field => field.ProgramNames)
                        .ThenInclude(program => program.CheckBoxes)
                .Where(card => card.Fields.Any(field => field.ProgramNames
                                                .Any(p => p.programname == programname)))
                .ToList();

            if (programCard == null || !programCard.Any())
            {
                return NotFound();
            }

            return Ok(programCard);
        }

        // POST: api/ProgramCard
        //[HttpPost]
        //public IActionResult AddProgramCard([FromBody] ProgramCardDto addProgramCardDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var programCardEntity = new ProgramCard
        //    {
        //        Fields = addProgramCardDto.Fields?.Select(f => new ProgramCard.Field
        //        {
        //            FieldName = f.FieldName,
        //            ProgramNames = f.ProgramNames?.Select(p => new ProgramCard.ProgramNames
        //            {
        //                programname = p.programname,
        //                CheckBoxes = p.CheckBoxes?.Select(c => new ProgramCard.CheckBoxes
        //                {
        //                    ChackBoxName = c.ChackBoxName
        //                }).ToList()
        //            }).ToList()
        //        }).ToList()
        //    };

        //    dbContext.MyprogramCard.Add(programCardEntity);
        //    dbContext.SaveChanges();

        //    return Ok(programCardEntity);
        //}

        [HttpPost]
        public IActionResult AddProgramCard([FromBody] ProgramCardDto addProgramCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Log incoming data
            Console.WriteLine("Received ProgramCardDto:");
            foreach (var field in addProgramCardDto.Fields)
            {
                Console.WriteLine($"FieldName: {field.FieldName}");
                foreach (var programName in field.ProgramNames)
                {
                    Console.WriteLine($"ProgramName: {programName.programname}");
                    foreach (var checkBox in programName.CheckBoxes)
                    {
                        Console.WriteLine($"CheckBoxName: {checkBox.ChackBoxName}");
                    }
                }
            }

            var programCardEntity = new ProgramCard
            {
                Fields = addProgramCardDto.Fields?.Select(f => new ProgramCard.Field
                {
                    FieldName = f.FieldName,
                    ProgramNames = f.ProgramNames?.Select(p => new ProgramCard.ProgramNames
                    {
                        programname = p.programname,
                        CheckBoxes = p.CheckBoxes?.Select(c => new ProgramCard.CheckBoxes
                        {
                            ChackBoxName = c.ChackBoxName
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            dbContext.MyprogramCard.Add(programCardEntity);
            dbContext.SaveChanges();

            return Ok(programCardEntity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProgramCard(int id)
        {
            // Find the ProgramCard by ID
            var programCard = dbContext.MyprogramCard
                .Include(card => card.Fields)
                    .ThenInclude(field => field.ProgramNames)
                        .ThenInclude(program => program.CheckBoxes)
                .FirstOrDefault(card => card.Id == id);

            // If the ProgramCard is not found, return a 404 Not Found response
            if (programCard == null)
            {
                return NotFound();
            }

            // Remove the ProgramCard from the databas{e
            dbContext.MyprogramCard.Remove(programCard);
            dbContext.SaveChanges();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }
    }

}
