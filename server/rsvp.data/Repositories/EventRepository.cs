using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rsvp.data.Data;
using rsvp.data.Interfaces;
using rsvp.data.Models;
using rsvp.data.Queries;

namespace rsvp.data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Event> CreateEventAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EventExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EventPrivate(int id)
        {
            throw new NotImplementedException();
        }

        //Get All Events and Query filtering
        public async Task<List<Event>> GetAllEventsAsync(EventQuery query)
        {
            var events = _context.Events.AsQueryable();

            if (query.CategoryId.HasValue)
                events = events.Where(e => e.EventCategoryId == query.CategoryId.Value);


            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                events = events.Where(e => e.Title.Contains(query.SearchTerm));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                events = query.SortBy.ToLower() switch
                {
                    "title" => events.OrderBy(e => e.Title),          // Alphabetical
                    "created" => events.OrderByDescending(e => e.CreatedAt), // Latest first
                    _ => events.OrderByDescending(e => e.CreatedAt) // Default fallback
                };
            }
            else
            {
                // Default if no sort specified
                events = events.OrderByDescending(e => e.CreatedAt);
            }

            return await events
                .Skip(query.Skip)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.Include(r => r.RSVPs).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<List<Event>> GetUserEvents(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Event?> UpdateEventAsync(Event ev)
        {
            throw new NotImplementedException();
        }
    }
}