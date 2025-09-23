using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rsvp.api.DTOs.Event;
using rsvp.api.Extensions;
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
        public async IAsyncEnumerable<Event> GetUserEvents()
        {
            var username = User.GetUsername();
            if (username == null)
            {
                Response.StatusCode = 401;
                yield break;
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                Response.StatusCode = 401;
                yield break;
            }

            await foreach (var ev in _eventRepo.GetUserEvents(user.Id))
            {
                yield return ev;
            }


        }

    }
}