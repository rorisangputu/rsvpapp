using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }              // Event name
        public string? Description { get; set; }        // Optional description
        public DateTime Date { get; set; }             // Event date
        public string? Location { get; set; }           // Event location
        public bool IsPrivate { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships
        public string? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public int? EventCategoryId { get; set; }
        public EventCategory? EventCategory { get; set; }

        public ICollection<Rsvp> RSVPs { get; set; } = new List<Rsvp>();
    }
}