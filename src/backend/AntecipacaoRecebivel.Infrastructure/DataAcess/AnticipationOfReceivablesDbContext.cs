using AnticipationOfReceivables.BuildingBlocks.Domain.Base;
using AnticipationOfReceivables.Domain.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace AnticipationOfReceivables.Infrastructure.DataAcess;

public class AnticipationOfReceivablesDbContext(DbContextOptions<AnticipationOfReceivablesDbContext> opt) : DbContext(opt), IRepositoryBase
{
    public async Task<List<T>> ListAsync<T>(Func<IQueryable<T>, IQueryable<T>> configureQuery, CancellationToken cancellationToken) where T : class
    {
        IQueryable<T> query = Set<T>();
        query = configureQuery(query);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListWithIncludesAsync<T>(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes) where T : class
    {
        IQueryable<T> query = Set<T>();

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync(cancellationToken);
    }
    public async Task<T?> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class =>
        await Set<T>().FindAsync(new object[] { id }, cancellationToken);

    public async Task<T?> FirstOrDefaultAsync<T>(
        Func<IQueryable<T>, IQueryable<T>> configureQuery,
        CancellationToken cancellationToken
    ) where T : class
    {
        var query = configureQuery(Set<T>());
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class =>
        await Set<T>().AddAsync(entity, cancellationToken);

    public void Update<T>(T entity) where T : class =>
        Set<T>().Update(entity);

    public void Remove<T>(T entity) where T : class =>
        Set<T>().Remove(entity);

    public async Task<bool> AnyAsync<T>(Func<IQueryable<T>, IQueryable<T>> predicate, CancellationToken cancellationToken) where T : class =>
        await predicate(Set<T>()).AnyAsync(cancellationToken);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<EntityBase>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(EntityBase.CreatedAt)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(EntityBase.UpdatedAt)).CurrentValue = null;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(EntityBase.CreatedAt)).IsModified = false;
                entry.Property(nameof(EntityBase.UpdatedAt)).CurrentValue = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("AnticipationOfReceivables");
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
