using MediatR;
using System.Text.Json.Serialization;

namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

public abstract record Command<TResponse> : IRequest<CommandResponse<TResponse>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
