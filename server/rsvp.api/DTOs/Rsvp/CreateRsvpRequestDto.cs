using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.api.DTOs.Rsvp
{
    public class CreateRsvpRequestDto
    {
        [Required]
        public bool IsAttending { get; set; }

        [MaxLength(255, ErrorMessage = "Comment is too long. Must be less than 255 characters")]
        public string Comment { get; set; } = string.Empty;

    }
}