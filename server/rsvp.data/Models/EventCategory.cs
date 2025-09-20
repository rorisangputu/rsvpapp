using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.data.Models
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }               // e.g., Party, Meeting

        // Relationships
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}