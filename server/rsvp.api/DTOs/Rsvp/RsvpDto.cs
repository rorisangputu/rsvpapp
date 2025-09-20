using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Models;

namespace rsvp.api.DTOs.Rsvp
{
    public class RsvpDto
    {
        public int Id { get; set; }
        public bool IsAttending { get; set; }          // True = attending, False = not
        public string? Comment { get; set; }            // Optional message
        public string? UserId { get; set; }
        public string? CreatedByUserName { get; set; }
        public int EventId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}