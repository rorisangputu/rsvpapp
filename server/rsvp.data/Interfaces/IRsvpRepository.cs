using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Models;

namespace rsvp.data.Interfaces
{
    public interface IRsvpRepository
    {
        Task<Rsvp> CreateRsvpAsync(Rsvp rsvpModel);
        Task<Rsvp?> GetRsvpByIdAsync(int id);
    }
}