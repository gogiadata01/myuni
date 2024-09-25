using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyUni.Data;
using MyUni.Models.Entities;
using Newtonsoft.Json;
using MyUni.Models;
using static MyUni.Models.Entities.EventCard;
using Microsoft.EntityFrameworkCore;


namespace MyUni.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventCardController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EventCardController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult GetAllEventCard()
        {
            // Eagerly load the related 'Types' property
            var allEventCards = dbContext.MyEventCard
                .Include(ec => ec.Types) // Include related 'Types' entity
                .ToList();

            return Ok(allEventCards);
        }


        [HttpPost]
        public IActionResult AddEventCard([FromBody] EventCardDto addEventCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Log received data
            Console.WriteLine(JsonConvert.SerializeObject(addEventCardDto, Formatting.Indented));

            // Map DTO to entity
            var eventCardEntity = new EventCard
            {
                Url = addEventCardDto.Url,
                Title = addEventCardDto.Title,
                Text = addEventCardDto.Text,
                Time = addEventCardDto.Time,
                isFeatured = addEventCardDto.isFeatured,
                Types = addEventCardDto.Types.Select(x => new EventType
                {
                    Type = x.Type
                }).ToList() // Fix: Convert to List
            };

            // Save entity to database
            dbContext.MyEventCard.Add(eventCardEntity);
            dbContext.SaveChanges();

            return Ok(eventCardEntity);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEventCard(int id)
        {
            // Find the event card by its ID
            var eventCard = dbContext.MyEventCard
                .Include(ec => ec.Types) // Include related 'Types' to ensure they are also deleted if necessary
                .FirstOrDefault(ec => ec.Id == id);

            // Check if the event card exists
            if (eventCard == null)
            {
                return NotFound(new { message = "Event card not found" });
            }

            // Remove the event card from the database
            dbContext.MyEventCard.Remove(eventCard);
            dbContext.SaveChanges();

            return Ok(new { message = "Event card deleted successfully" });
        }
        [HttpGet("{id}")]
        public IActionResult GetEventCardById(int id)
        {
            // Find the event card by its ID, including related 'Types'
            var eventCard = dbContext.MyEventCard
                .Include(ec => ec.Types)
                .FirstOrDefault(ec => ec.Id == id);

            // Check if the event card exists
            if (eventCard == null)
            {
                return NotFound(new { message = "Event card not found" });
            }

            // Map the entity to a DTO (if needed)
            var eventCardDto = new EventCard
            {
                Id = id,
                Url = eventCard.Url,
                Title = eventCard.Title,
                Text = eventCard.Text,
                Time = eventCard.Time,
                isFeatured = eventCard.isFeatured,
                Types = eventCard.Types.Select(type => new EventType
                {
                    Type = type.Type
                }).ToList()
            };

            // Return the found event card
            return Ok(eventCardDto);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEventCard(int id, [FromBody] EventCardDto updateEventCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing event card by its ID
            var existingEventCard = dbContext.MyEventCard
                .Include(ec => ec.Types) // Include related 'Types' to update them as well
                .FirstOrDefault(ec => ec.Id == id);

            // Check if the event card exists
            if (existingEventCard == null)
            {
                return NotFound(new { message = "Event card not found" });
            }

            // Update the event card properties
            existingEventCard.Url = updateEventCardDto.Url;
            existingEventCard.Title = updateEventCardDto.Title;
            existingEventCard.Text = updateEventCardDto.Text;
            existingEventCard.Time = updateEventCardDto.Time;
            existingEventCard.isFeatured = updateEventCardDto.isFeatured;

            // Clear the existing event types and update with the new ones
            existingEventCard.Types.Clear();
            existingEventCard.Types = updateEventCardDto.Types.Select(x => new EventType
            {
                Type = x.Type
            }).ToList();

            // Save the changes to the database
            dbContext.SaveChanges();

            // Return the updated event card
            return Ok(existingEventCard);
        }


    }
}