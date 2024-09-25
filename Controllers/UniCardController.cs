using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUni.Data;
using MyUni.Models;
using MyUni.Models.Entities;
using System.Xml;
using Newtonsoft.Json;

namespace MyUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniCardController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public UniCardController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 

        }
        [HttpGet]
        public IActionResult GetAllUniCard()
        {
            var AllUniCard = dbContext.MyUniCard.Include(card => card.Events)
        .Include(card => card.Sections)
            .ThenInclude(section => section.ProgramNames)
        .Include(card => card.Sections2)
            .ThenInclude(section2 => section2.SavaldebuloSagnebi)
        .Include(card => card.ArchevitiSavaldebuloSaganebi)
            .ThenInclude(archeviti => archeviti.ArchevitiSavaldebuloSagnebi)
        .ToList();
            return Ok(AllUniCard);
        }



        [HttpGet("{id}")]
        public IActionResult GetUniCardById(int id)
        {
            var UniCard = dbContext.MyUniCard
                .Include(card => card.Events)
                .Include(card => card.Sections)
                    .ThenInclude(section => section.ProgramNames)
                .Include(card => card.Sections2)
                    .ThenInclude(section2 => section2.SavaldebuloSagnebi)
                .Include(card => card.ArchevitiSavaldebuloSaganebi)
                    .ThenInclude(archeviti => archeviti.ArchevitiSavaldebuloSagnebi)
                .FirstOrDefault(card => card.Id == id);

            if (UniCard is null)
            {
                return NotFound();
            }
            return Ok(UniCard);
        }
        [HttpPost]
        public IActionResult AddUniCard([FromBody] UniCardDto addUniCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Log received data
            Console.WriteLine(JsonConvert.SerializeObject(addUniCardDto, Newtonsoft.Json.Formatting.Indented));

            // Existing code to map and save entity...
            var UniCardEntity = new UniCard
            {
                Url = addUniCardDto.url,
                Title = addUniCardDto.title,
                MainText = addUniCardDto.mainText,
                History = addUniCardDto.history,
                ForPupil = addUniCardDto.forPupil,
                ScholarshipAndFunding = addUniCardDto.scholarshipAndFunding,
                ExchangePrograms = addUniCardDto.exchangePrograms,
                Labs = addUniCardDto.labs,
                StudentsLife = addUniCardDto.studentsLife,
                PaymentMethods = addUniCardDto.paymentMethods,
                Events = addUniCardDto.events?.Select(e => new UniCard.Event
                {
                    Url = e.url,
                    Title = e.title,
                    Text = e.text
                }).ToList(),
                Sections = addUniCardDto.sections?.Select(s => new UniCard.Section
                {
                    Title = s.title,
                    ProgramNames = s.programNames?.Select(p => new UniCard.Programname
                    {
                        ProgramName = p.programName,
                        Jobs = p.Jobs,
                        SwavlebisEna = p.SwavlebisEna,
                        Kvalifikacia = p.Kvalifikacia,
                        Dafinanseba = p.Dafinanseba,
                        KreditebisRaodenoba = p.KreditebisRaodenoba,
                        AdgilebisRaodenoba = p.AdgilebisRaodenoba,
                        Fasi = p.Fasi,
                        Kodi = p.Kodi,
                        ProgramisAgwera = p.ProgramisAgwera,
                    }).ToList()
                }).ToList(),
                Sections2 = addUniCardDto.sections2?.Select(s2 => new UniCard.Section2
                {
                    Title = s2.title,
                    SavaldebuloSagnebi = s2.savaldebuloSagnebi?.Select(ss => new UniCard.SavaldebuloSagnebi
                    {
                        SagnisSaxeli = ss.sagnisSaxeli,
                        Koeficienti = ss.koeficienti,
                        MinimaluriZgvari = ss.minimaluriZgvari,
                        Prioriteti = ss.prioriteti,
                        AdgilebisRaodenoba = ss.AdgilebisRaodenoba,

                    }).ToList()
                }).ToList(),
                ArchevitiSavaldebuloSaganebi = addUniCardDto.archevitiSavaldebuloSaganebi?.Select(a => new UniCard.ArchevitiSavaldebuloSagani
                {
                    Title = a.title,
                    ArchevitiSavaldebuloSagnebi = a.archevitiSavaldebuloSagnebi?.Select(asb => new UniCard.ArchevitiSavaldebuloSagnebi
                    {
                        SagnisSaxeli = asb.sagnisSaxeli,
                        Koeficienti = asb.koeficienti,
                        MinimaluriZgvari = asb.minimaluriZgvari,
                        Prioriteti = asb.prioriteti,
                        AdgilebisRaodenoba = asb.AdgilebisRaodenoba
                    }).ToList()
                }).ToList()
            };

            dbContext.MyUniCard.Add(UniCardEntity);
            dbContext.SaveChanges();

            return Ok(UniCardEntity);
        }
        [HttpGet("search")]
        public IActionResult GetUniCardByTitleAndProgramName([FromQuery] string title, [FromQuery] string programName)
        {
            try
            {
                var result = dbContext.MyUniCard
                    .Include(card => card.Sections)
                        .ThenInclude(section => section.ProgramNames)
                    .Include(card => card.Sections2)
                        .ThenInclude(section2 => section2.SavaldebuloSagnebi)
                    .Include(card => card.ArchevitiSavaldebuloSaganebi)
                        .ThenInclude(archeviti => archeviti.ArchevitiSavaldebuloSagnebi)
                    .Where(card => card.Title == title &&
                                   card.Sections.Any(section => section.ProgramNames
                                                                .Any(program => program.ProgramName == programName)))
                    .ToList();

                if (!result.Any())
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (you might use a logging framework)
                Console.Error.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteUniCard(int id)
        {
            // Find the UniCard by ID
            var uniCard = dbContext.MyUniCard
                .Include(card => card.Events)
                .Include(card => card.Sections)
                    .ThenInclude(section => section.ProgramNames)
                .Include(card => card.Sections2)
                    .ThenInclude(section2 => section2.SavaldebuloSagnebi)
                .Include(card => card.ArchevitiSavaldebuloSaganebi)
                    .ThenInclude(archeviti => archeviti.ArchevitiSavaldebuloSagnebi)
                .FirstOrDefault(card => card.Id == id);

            // If the UniCard is not found, return a 404 Not Found response
            if (uniCard == null)
            {
                return NotFound();
            }

            // Remove the UniCard from the database
            dbContext.MyUniCard.Remove(uniCard);
            dbContext.SaveChanges();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }
    }

}



