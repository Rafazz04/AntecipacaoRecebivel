using MediatR;

namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Queries;

public abstract class QueryHandlerBase<TQuery, TResponse> : IRequestHandler<TQuery, QueryResponse<TResponse>>
    where TQuery : Query<TResponse>
{
    public abstract Task<QueryResponse<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);

    /// <summary>
    /// Retorna um resultado de sucesso.
    /// </summary>
    protected QueryResponse<TResponse> Success(TResponse data, string message = "Operação realizada com sucesso.")
    {
        return new QueryResponse<TResponse>(data, true, "200", null, message);
    }

    /// <summary>
    /// Retorna um resultado de falha.
    /// </summary>
    protected QueryResponse<TResponse> Failure(string message = "Nenhum registro encontrado.")
    {
        return new QueryResponse<TResponse>(default!, false, "400", null, message);
    }
}
