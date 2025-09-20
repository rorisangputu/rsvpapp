using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rsvp.api.DTOs.Event;
using rsvp.api.Mappers;
using rsvp.data.Interfaces;
using rsvp.data.Queries;

namespace rsvp.api.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;

        public EventController(IEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllEvents([FromQuery] EventQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetAllEventsAsync(query);
            var eventDto = events.Select(e => e.ToEventDto()).ToList();
            return Ok(eventDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ev = await _eventRepo.GetEventByIdAsync(id);
            if (ev == null)
            {
                return NotFound();
            }

            return Ok(ev.ToEventDto());

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEventRequestDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventModel = eventDto.ToEventFromCreateDto();
            await _eventRepo.CreateEventAsync(eventModel);
            return CreatedAtAction(nameof(GetById), new { id = eventModel.Id }, eventModel.ToEventDto());
        }
    }
}