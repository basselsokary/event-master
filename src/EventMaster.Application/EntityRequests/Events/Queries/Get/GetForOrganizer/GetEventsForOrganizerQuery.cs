namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForOrganizer;

public record GetEventsForOrganizerQuery() : IQuery<IEnumerable<Response>>;
