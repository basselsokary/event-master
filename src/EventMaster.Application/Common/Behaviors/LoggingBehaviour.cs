using Microsoft.Extensions.Logging;

namespace EventMaster.Application.Common.Behaviors;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    // private readonly IUserContext _user;
    // private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LoggingBehaviour: Handling request of type {@Request}", request.GetType().Name);
        
        var requestName = typeof(TRequest).Name;
        // var userId = _user.Id;
        var userId = string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            // userName = await _identityService.GetUserNameAsync(userId);
            userName = "UserName";
        }

        _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);

        return await next(cancellationToken);
    }
}
