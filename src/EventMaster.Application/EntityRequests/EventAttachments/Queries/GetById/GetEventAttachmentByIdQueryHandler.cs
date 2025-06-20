using System.Linq.Expressions;
using EventMaster.Domain.Entities;
using EventMaster.Domain.Errors;

namespace EventMaster.Application.EntityRequests.EventAttachments.Queries.GetById;

internal class GetEventAttachmentByIdQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetEventAttachmentByIdQuery, Response>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Response>> Handle(GetEventAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        Response? attachment = await _unitOfWork.Repository<EventAttachment>().GetProjectedAsync(
            filter: e => e.EventId == request.EventId && e.Id == request.Id,
            selector: GetProjection(),
            asSplitQuery: true,
            cancellationToken: cancellationToken);

        if (attachment == null)
            return Result.Failure<Response>(["Event/EventAttachment was not found."]);

        return Result.Success(attachment);
    }
    
    private static Expression<Func<EventAttachment, Response>> GetProjection()
    {
        return a => new Response(
            a.Id,
            a.EventId,
            a.Text,
            a.FileUrl,
            a.UploadedAt);
    }
}
