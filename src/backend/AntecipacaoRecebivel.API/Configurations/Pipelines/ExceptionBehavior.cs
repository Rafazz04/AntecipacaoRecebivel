using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;

namespace AnticipationOfReceivables.API.Configurations.Pipelines;

public sealed class ExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            return CreateResponse(
                success: false,
                statusCode: "400",
                errorCode: "VALIDATION_ERROR",
                messages: ex.Errors.Select(e => e.ErrorMessage).ToList()
            );
        }
        catch (BusinessException ex)
        {
            return CreateResponse(
                success: false,
                statusCode: "422",
                errorCode: ex.FirstError.Code,
                messages: ex.Errors.Select(e => e.Message).ToList()
            );
        }
        catch (Exception ex)
        {
            return CreateResponse(
                success: false,
                statusCode: "500",
                errorCode: "GEN_500",
                messages: [$"An unexpected error occurred: {ex.Message}", ex.StackTrace!]
            );
        }
    }

    private static TResponse CreateResponse(
        bool success,
        string statusCode,
        string errorCode,
        List<string> messages)
    {
        var responseType = typeof(TResponse);

        return (TResponse)Activator.CreateInstance(
            responseType,
            default!,
            success,
            statusCode,
            errorCode,
            messages
        )!;
    }
}
