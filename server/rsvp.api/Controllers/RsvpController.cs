using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rsvp.api.DTOs.Rsvp;
using rsvp.api.Extensions;
using rsvp.api.Mappers;
using rsvp.data.Interfaces;
using rsvp.data.Models;

namespace rsvp.api.Controllers
{
    // Handles RSVP operations for retrieving and creating RSVPs for events.
    [Route("api/rsvp")]
    [ApiController]
    public class RsvpController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly IRsvpRepository _rsvpRepo;
        private readonly UserManager<User> _userManager;

        // Initializes the controller with dependencies for user management, event, and RSVP repositories.
        public RsvpController(UserManager<User> userManager, IEventRepository eventRepo, IRsvpRepository rsvpRepo)
        {
            _userManager = userManager;
            _eventRepo = eventRepo;
            _rsvpRepo = rsvpRepo;
        }

        // GET: api/rsvp/{id}
        // Retrieves an RSVP by its ID.
        // Parameters: id (int) - The ID of the RSVP.
        // Returns: 200 OK with RsvpDto, 400 Bad Request if model state is invalid, or 404 Not Found if RSVP doesn't exist.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rsvp = await _rsvpRepo.GetRsvpByIdAsync(id);
            if (rsvp == null) return NotFound();

            return Ok(rsvp.ToRsvpDto());
        }
        /*
            1. Fix Event RSVP Return
            -- Missing rsvp user info

            2. Only Create if Rsvp if user hasnt created on on specific event

            3. Connect FronEnd
        */

        // POST: api/rsvp/{eventId}
        // Creates an RSVP for an event. Requires authentication.
        // Parameters: eventId (int) - The event ID; rsvpDto (CreateRsvpRequestDto) - RSVP details.
        // Returns: 201 Created with RsvpDto, 400 Bad Request if invalid model or event, or 401 Unauthorized if user not authenticated.
        [HttpPost("{eventId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateRsvp([FromRoute] int eventId, CreateRsvpRequestDto rsvpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ev = await _eventRepo.GetEventByIdAsync(eventId);
            if (ev == null)
            {
                return BadRequest("Event does not exist");
            }

            var username = User.GetUsername();
            if (username == null) return Unauthorized();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return Unauthorized();

            var rsvpModel = rsvpDto.ToRsvpFromCreateRsvpDto(ev.Id);
            rsvpModel.UserId = user.Id;

            await _rsvpRepo.CreateRsvpAsync(rsvpModel);
            return CreatedAtAction(nameof(GetById), new { id = rsvpModel.Id }, rsvpModel.ToRsvpDto());
        }
    }
}