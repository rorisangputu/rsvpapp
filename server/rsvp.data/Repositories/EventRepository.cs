using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Interfaces;
using rsvp.data.Models;
using rsvp.data.Queries;

namespace rsvp.data.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Task<Event> CreateEventAsync(Event eventModel)
        {
            throw new NotImplementedException();
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

        public Task<List<Event>> GetAllEventsAsync(EventQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<Event?> GetEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event?> UpdateEventAsync(Event ev)
        {
            throw new NotImplementedException();
        }
    }
}