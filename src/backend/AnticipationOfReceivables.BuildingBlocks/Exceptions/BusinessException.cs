namespace AnticipationOfReceivables.BuildingBlocks.Exceptions;

public sealed class BusinessException : Exception
{
    private readonly IReadOnlyCollection<BusinessError> _errors;

    public IReadOnlyCollection<BusinessError> Errors => _errors;

    public BusinessError FirstError => _errors.First();

    public BusinessException(IEnumerable<BusinessError> errors, Exception? innerException = null)
        : base(null, innerException)
    {
        if (errors is null)
            throw new ArgumentNullException(nameof(errors));

        _errors = errors.ToList();

        if (_errors.Count == 0)
            throw new ArgumentException("At least one BusinessError is required.");
    }

    public BusinessException(BusinessError error)
        : this(new[] { error })
    {
    }
}
