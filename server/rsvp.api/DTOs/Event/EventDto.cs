using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.api.DTOs.Rsvp;

namespace rsvp.api.DTOs.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        // Optional info about category
        public int? EventCategoryId { get; set; } 
        public string? CreatedByUserName { get; set; }
        public List<RsvpDto> Rsvps { get; set; } = new List<RsvpDto>();
    }

}