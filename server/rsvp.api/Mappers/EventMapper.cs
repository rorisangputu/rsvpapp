using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.api.DTOs.Event;
using rsvp.data.Models;

namespace rsvp.api.Mappers
{
    public static class EventMapper
    {
        public static EventDto ToEventDto(this Event eventModel)
        {
            return new EventDto
            {
                Id = eventModel.Id,
                Title = eventModel.Title,
                Description = eventModel.Description,
                Date = eventModel.Date,
                Location = eventModel.Location,
                IsPrivate = eventModel.IsPrivate,
                CreatedAt = eventModel.CreatedAt,
                EventCategoryId = eventModel.EventCategoryId,
                CreatedByUserName = eventModel.CreatedByUser.UserName,
                Rsvps = eventModel.RSVPs.Select(r => r.ToRsvpDto()).ToList()
            };
        }

        public static Event ToEventFromCreateDto(this CreateEventRequestDto eventDto)
        {
            return new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Location = eventDto.Location,
                Date = eventDto.Date,
                IsPrivate = eventDto.IsPrivate,
                CreatedAt = eventDto.CreatedAt,
                EventCategoryId = eventDto.EventCategoryId,
                CreatedByUserId = eventDto.CreatedByUserId,
            };
        }
    }
}