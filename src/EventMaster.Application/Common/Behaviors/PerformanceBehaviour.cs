using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EventMaster.Application.Common.Behaviors;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    // private readonly IUserContext _user;
    // private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PerformanceBehaviour: Handling {Name} with request {@Request}", typeof(TRequest).Name, request);

        _timer.Start();

        var response = await next(cancellationToken);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            // var userId = _user.Id;
            var userId = string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                // userName = await _identityService.GetUserNameAsync(userId);
                userName = "UserName";
            }

            _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
