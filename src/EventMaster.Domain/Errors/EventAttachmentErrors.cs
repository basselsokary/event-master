namespace EventMaster.Domain.Errors;

public class EventAttachmentErrors
{
    public static IEnumerable<string> NotFound(Guid id) =>
    [
        "Event Attachment Was Not Found",
        $"Attachment with Id = {id} was not found."
    ];

    public static IEnumerable<string> Unauthorized() =>
    [
        "Unauthorized User"
    ];
}
