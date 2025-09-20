using Microsoft.AspNetCore.Identity;


namespace rsvp.data.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; }

        // Relationships
        public ICollection<Event> EventsCreated { get; set; } = new List<Event>();
        public ICollection<Rsvp> RSVPs { get; set; } = new List<Rsvp>();

    }
}