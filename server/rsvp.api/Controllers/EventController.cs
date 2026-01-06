using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rsvp.api.DTOs.Event;
using rsvp.api.Extensions;
using rsvp.api.Mappers;
using rsvp.data.Interfaces;
using rsvp.data.Models;
using rsvp.data.Queries;

namespace rsvp.api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly UserManager<User> _userManager;

        public EventController(IEventRepository eventRepo, UserManager<User> userManager)
        {
            _eventRepo = eventRepo;
            _userManager = userManager;
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

        //Get another users events
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserEvents([FromRoute] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User doesn't exist.");
            }

            var ev = await _eventRepo.GetUserEvents(user.Id);
            var eventDto = ev.Select(ev => ev.ToEventDto()).ToList();

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

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEventRequestDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var eventModel = eventDto.ToEventFromCreateDto();
            eventModel.CreatedByUserId = appUser.Id;
            eventModel.CreatedAt = DateTime.UtcNow;

            await _eventRepo.CreateEventAsync(eventModel);
            return CreatedAtAction(nameof(GetById), new { id = eventModel.Id }, eventModel.ToEventDto());
        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEventRequestDto updateDto)
        {
            var username = User.GetUsername();

            if (username == null) return Unauthorized();

            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null) return Unauthorized();

            var ev = await _eventRepo.UpdateEventAsync(id, updateDto.ToEventFromUpdate(), appUser);
            if (ev == null) return NotFound("Event Not Found!");
            return Accepted(ev.ToEventDto());
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var username = User.GetUsername();

            if (username == null) return Unauthorized();

            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null) return Unauthorized();

            var eventModel = await _eventRepo.DeleteAsync(id, appUser);
            if (eventModel == false) return NotFound("Event does not exist");

            return Ok(eventModel);
        }
    }
}