using EventMaster.Application.Common.Interfaces.Repositories;
using EventMaster.Domain.Entities;
using EventMaster.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Infrastructure.Repositories.Implementations;

internal class TicketRepository(AppDbContext context)
    : EntityBaseRepository<Ticket>(context),
    ITicketRepository
{
}
