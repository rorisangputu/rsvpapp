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
        Task<List<Event>> GetUserEvents(User user);
        Task<List<Event>> GetAllEventsAsync(EventQuery query);
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventModel);
        Task<Event?> UpdateEventAsync(Event ev);
        Task<bool> DeleteAsync(int id);
        Task<bool> EventExists(int id);
        Task<bool> EventPrivate(int id);
    }
}