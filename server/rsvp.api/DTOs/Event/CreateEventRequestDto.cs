using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.api.DTOs.Event
{
    public class CreateEventRequestDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Title cannot be more than 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Location cannot be more than 250 characters")]
        public string Location { get; set; } = string.Empty;

        [Required]
        public bool IsPrivate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int EventCategoryId { get; set; }
        [Required]
        public string CreatedByUserId { get; set; }

    }
}