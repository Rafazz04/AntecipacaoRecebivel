namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;

public abstract class Response<TData>
{
    public Response()
    {
    }
    public TData Data { get; set; } = default!;
    public bool Success { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
    public string StatusCode { get; set; } = "200";
    public string? ErrorCode { get; set; } = null;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public List<string> DetailedErrors { get; set; } = new List<string>();

    public virtual void AddMessage(string message)
    {
        Messages.Add(message);
    }

    public virtual void AddDetailedError(string error)
    {
        DetailedErrors.Add(error);
    }

    public void SetSuccess(bool success, string statusCode, string? errorCode = null)
    {
        Success = success;
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
