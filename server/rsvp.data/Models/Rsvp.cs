using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.data.Models
{
    public class Rsvp
    {
        public int Id { get; set; }
        public bool IsAttending { get; set; }          // True = attending, False = not
        public string? Comment { get; set; }            // Optional message
        public DateTime CreatedAt { get; set; }

        // Relationships
        public string? UserId { get; set; }
        public User? User { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}