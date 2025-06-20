
using EventMaster.Domain.Constants;

namespace EventMaster.Domain.Errors;

public class EventErrors
{
    public static IEnumerable<string> OrganizerIdInvalid()
        => [
        "Bad Request"
    ];

    public static IEnumerable<string> Unauthorized()
        => [
        "Unauthorized User"
    ];

    public static IEnumerable<string> NotFound(Guid eventId)
        => [
        "Event Is Not Found",
        $"The event with id = '{eventId}' was not found."
    ];

    public static IEnumerable<string> NotEventOwner()
        => [
        "Not the owner of the event."
    ];

    public static IEnumerable<string> TooManyAttachments()
    {
        const int maxAttachments = DomainConstants.Event.MaxEventAttachmentsPerEvent;
        return [
            "Too many attachments.",
            $"The maximum number of attachments for an event is {maxAttachments}."
        ];
    }

    public static IEnumerable<string> DuplicateAttachment(string fileUrl)
        => [
        $"Attachment with the same file URL: {fileUrl} already exists."
    ];

    public static IEnumerable<string> AlreadyApproved(Guid id)
        => [
        "Event Already Approved",
        $"The event with id = '{id}' is already approved."
    ];
}