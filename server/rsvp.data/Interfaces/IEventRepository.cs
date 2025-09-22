using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rsvp.data.Models;
using rsvp.data.Queries;

namespace rsvp.data.Interfaces
{
    public interface IEventRepository
    {
        IAsyncEnumerable<Event> GetUserEvents(string userId);
        Task<List<Event>> GetAllEventsAsync(EventQuery query);
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventModel);
        Task<Event?> UpdateEventAsync(int id, Event ev, User user);
        Task<bool> DeleteAsync(int id, User user);
        Task<bool> EventExists(int id);
        Task<bool> EventPrivate(int id);
    }
}