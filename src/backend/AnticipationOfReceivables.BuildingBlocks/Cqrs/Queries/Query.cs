using MediatR;

namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Queries;

public abstract record Query<TResponse> : IRequest<QueryResponse<TResponse>>
{
    public Guid Id { get; set; }
}
