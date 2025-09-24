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
    [Route("api/rsvp")]
    [ApiController]
    public class RsvpController : ControllerBase
    {

        private readonly IEventRepository _eventRepo;
        private readonly IRsvpRepository _rsvpRepo;
        private readonly UserManager<User> _userManager;

        public RsvpController(UserManager<User> userManager, IEventRepository eventRepo, IRsvpRepository rsvpRepo)
        {
            _userManager = userManager;
            _eventRepo = eventRepo;
            _rsvpRepo = rsvpRepo;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rsvp = await _rsvpRepo.GetRsvpByIdAsync(id);
            if (rsvp == null) return NotFound();

            return Ok(rsvp);

        }

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