namespace AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;

public sealed record EmptyResponse(bool Success = true)
{
    public static EmptyResponse Instance => new();
    public static Task<EmptyResponse> NewInstance<T>(T _) => Task.FromResult(Instance);
}
