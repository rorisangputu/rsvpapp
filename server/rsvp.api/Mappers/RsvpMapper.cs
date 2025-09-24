using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.api.DTOs.Rsvp;
using rsvp.data.Models;

namespace rsvp.api.Mappers
{
    public static class RsvpMapper
    {
        public static RsvpDto ToRsvpDto(this Rsvp rsvpModel)
        {
            return new RsvpDto
            {
                Id = rsvpModel.Id,
                IsAttending = rsvpModel.IsAttending,
                Comment = rsvpModel.Comment,
                EventId = rsvpModel.EventId,
                CreatedByUserName = rsvpModel.User?.UserName,
            };
        }

        public static Rsvp ToRsvpFromCreateRsvpDto(this CreateRsvpRequestDto rsvpDto, int eventId)
        {
            return new Rsvp
            {
                IsAttending = rsvpDto.IsAttending,
                Comment = rsvpDto?.Comment,
            };
        }
    }
}