using System.Linq.Expressions;
using EventMaster.Application.DTOs;
using EventMaster.Domain.Entities;

namespace EventMaster.Application.Mappers;

public static class EventMapper
{
    public static EventDto Map(this Event @event)
    {
        return new EventDto()
        {
            Id = @event.Id,

            OrganizerId = @event.OrganizerId,

            Title = @event.Title,
            Description = @event.Description,
            Venue = @event.Venue,
            Location = @event.Location,

            TicketPrice = @event.TicketPrice,

            TotalTickets = @event.TotalTickets,
            TicketsLeft = @event.TicketsLeft,

            Date = @event.Date,
        };
    }

    public static Expression<Func<Event, EventDto>> Project()
    {
        return e => new EventDto()
        {
            Id = e.Id,

            OrganizerId = e.OrganizerId,

            Title = e.Title,
            Description = e.Description,
            Venue = e.Venue,
            Location = e.Location,

            TicketPrice = e.TicketPrice,

            TotalTickets = e.TotalTickets,
            TicketsLeft = e.TicketsLeft,

            Date = e.Date,
        };
    }

}
