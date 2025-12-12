namespace AnticipationOfReceivables.BuildingBlocks.Domain.Base;

public class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; } = null;
    public void MarkAsCreated()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
    public void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
