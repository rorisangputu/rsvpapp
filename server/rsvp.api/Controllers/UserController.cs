using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rsvp.api.DTOs.Event;
using rsvp.api.Extensions;
using rsvp.api.Mappers;
using rsvp.data.Interfaces;
using rsvp.data.Models;

namespace rsvp.api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly UserManager<User> _userManager;

        public UserController(IEventRepository eventRepo, UserManager<User> userManager)
        {
            _eventRepo = eventRepo;
            _userManager = userManager;
        }

        [HttpGet("events")]
        [Authorize]
        public async Task<IActionResult> GetUserEvents()
        {
            var username = User.GetUsername();
            if (username == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }

            var ev = await _eventRepo.GetUserEvents(user.Id);
            var eventModel = ev.Select(ev => ev.ToEventDto()).ToList();

            return Ok(eventModel);

        }

    }
}