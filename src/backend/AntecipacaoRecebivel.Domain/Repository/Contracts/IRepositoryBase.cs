using System.Linq.Expressions;

namespace AnticipationOfReceivables.Domain.Repository.Contracts;

public interface IRepositoryBase
{
    Task<List<T>> ListAsync<T>(Func<IQueryable<T>, IQueryable<T>> configureQuery, CancellationToken cancellationToken) where T : class;
    Task<List<T>> ListWithIncludesAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes) where T : class;
    Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class;
    Task<T?> FirstOrDefaultAsync<T>(Func<IQueryable<T>, IQueryable<T>> configureQuery, CancellationToken cancellationToken) where T : class;
    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
    void Update<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    Task<bool> AnyAsync<T>(Func<IQueryable<T>, IQueryable<T>> predicate, CancellationToken cancellationToken) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
