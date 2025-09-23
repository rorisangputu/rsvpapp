using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Data;
using rsvp.data.Interfaces;
using rsvp.data.Models;

namespace rsvp.data.Repositories
{
    public class RsvpRepository : IRsvpRepository
    {
        private readonly ApplicationDbContext _context;

        public RsvpRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Rsvp> CreateRsvpAsync(Rsvp rsvpModel)
        {
            await _context.Rsvps.AddAsync(rsvpModel);
            await _context.SaveChangesAsync();
            return rsvpModel;
        }

    }
}