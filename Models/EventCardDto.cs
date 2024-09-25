using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class EventCardDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string Title { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public bool isFeatured { get; set; }

        public ICollection<EventTypeDto> Types { get; set; }
    }

    public class EventTypeDto
    {
        public string Type { get; set; }
    }
}

