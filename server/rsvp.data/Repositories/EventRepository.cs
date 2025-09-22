using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> DeleteAsync(int id, User user)
        {
            var eventModel = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (eventModel == null) return false;

            if (eventModel.CreatedByUserId != user.Id) throw new UnauthorizedAccessException("Action Not Authorized for User");

            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
            return true;
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
            var events = _context.Events
                .Include(e => e.CreatedByUser)
                .Include(c => c.EventCategory)
                .AsQueryable();

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

        public async Task<Event?> UpdateEventAsync(int id, Event ev, User user)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null) return null;

            if (existingEvent.CreatedByUserId != user.Id) throw new UnauthorizedAccessException("Action Not Authorized for User.");

            existingEvent.Title = ev.Title;
            existingEvent.Description = ev.Description;
            existingEvent.IsPrivate = ev.IsPrivate;
            existingEvent.Date = ev.Date;
            existingEvent.Location = ev.Location;
            existingEvent.EventCategoryId = ev.EventCategoryId;


            await _context.SaveChangesAsync();
            return existingEvent;
        }
    }
}