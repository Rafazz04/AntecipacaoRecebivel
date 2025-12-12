using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;

namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

public class CommandResponse<TData> : Response<TData>
{
    public CommandResponse(TData data, bool success = true, string statusCode = "200",
        string? errorCode = null, string? message = null)
    {
        Data = data;
        Success = success;
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Timestamp = DateTime.UtcNow;

        if (!string.IsNullOrEmpty(message))
        {
            AddMessage(message);
        }
    }

    public CommandResponse(TData data, bool success = true, string statusCode = "200",
        string? errorCode = null, List<string>? messages = null)
    {
        Data = data;
        Success = success;
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Timestamp = DateTime.UtcNow;

        if (messages != null && messages.Any())
        {
            foreach (var message in messages)
            {
                AddMessage(message);
            }
        }
    }
}
