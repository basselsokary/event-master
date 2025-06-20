using System.Linq.Expressions;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.Get;

internal class GetEventAttachmentsByEventIdQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetEventAttachmentsByEventIdQuery, List<Response>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<Response>>> Handle(GetEventAttachmentsByEventIdQuery request, CancellationToken cancellationToken)
    {
        var attachments = await _unitOfWork.Repository<EventAttachment>().GetAllProjectedAsync(
            filter: e => e.EventId == request.EventId,
            selector: GetProjection(),
            cancellationToken: cancellationToken);

        if (attachments == null)
            return Result.Failure<List<Response>>(EventErrors.NotFound(request.EventId));

        return Result.Success(attachments.ToList());
    }

    private static Expression<Func<EventAttachment, Response>> GetProjection()
    {
        return e => new (
            e.Id,
            e.Text,
            e.FileUrl,
            e.UploadedAt
        );
    }
}