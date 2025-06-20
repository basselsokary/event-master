namespace EventMaster.Application.EntityRequests.Events.Queries.Get.GetForParticipant;

public record GetEventsForParticipantQuery(
    string? Location = null,
    DateTime? Date = null,
    string? OrderBy = null) : IQuery<List<Response>>;
