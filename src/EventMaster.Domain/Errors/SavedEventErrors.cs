namespace EventMaster.Domain.Errors;

public class BasketErrors
{
    public static IEnumerable<string> SavedEventAlreadyExists(Guid eventId) =>
    [
        "Saved Event Already Exists",
        $"The event with id = '{eventId}' already exists in your saved events collection."
    ];

    public static IEnumerable<string> NotFound() =>
    [
        "Basket Was Not Found"
    ];

    public static IEnumerable<string> Unauthorized() =>
    [
        "Unauthorized User",
        "You are not authorized to perform this action."
    ];
}
