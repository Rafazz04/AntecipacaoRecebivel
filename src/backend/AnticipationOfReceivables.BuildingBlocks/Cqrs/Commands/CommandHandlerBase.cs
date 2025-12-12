using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using MediatR;

namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

public abstract class CommandHandlerBase<TCommand, TResponse>
    : IRequestHandler<TCommand, CommandResponse<TResponse>>
    where TCommand : Command<TResponse>
{

    public abstract Task<CommandResponse<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);

    /// <summary>
    /// Retorna um resultado de sucesso.
    /// </summary>
    protected CommandResponse<TResponse> Success(TResponse data, string message = "Operação realizada com sucesso.")
    {
        return new CommandResponse<TResponse>(data, true, "200", null, message);
    }
    /// <summary>
    /// Retorna um resultado de sucesso.
    /// </summary>
    protected CommandResponse<EmptyResponse> Success(string message = "Operação realizada com sucesso.")
    {
        return new CommandResponse<EmptyResponse>(EmptyResponse.Instance, true, "200", null, message);
    }

    /// <summary>
    /// Retorna um resultado de falha.
    /// </summary>
    protected CommandResponse<TResponse> Failure(string errorCode = "GEN_XXX", string errorMessage = "Requisição inválida, por favor, revise-a.", string statusCode = "400")
    {
        return new CommandResponse<TResponse>(default!, false, statusCode, errorCode, errorMessage);
    }
    
    

    /// <summary>
    /// Retorna um resultado de falha.
    /// </summary>
    protected CommandResponse<TResponse> Failure(List<string> errorMessages, string errorCode = "GEN_XXX", string statusCode = "400")
    {
        return new CommandResponse<TResponse>(default!, false, statusCode, errorCode, errorMessages);
    }
}
