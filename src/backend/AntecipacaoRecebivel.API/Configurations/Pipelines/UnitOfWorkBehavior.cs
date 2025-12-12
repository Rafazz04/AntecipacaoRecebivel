using AnticipationOfReceivables.Domain.Repository.Contracts;
using MediatR;

namespace AnticipationOfReceivables.API.Configurations.Pipelines;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IRepositoryBase _repository;

    public UnitOfWorkBehavior(IRepositoryBase repository)
        => _repository = repository;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.GetType().Name.EndsWith("Command"))
            await _repository.SaveChangesAsync(cancellationToken);

        return response;
    }
}