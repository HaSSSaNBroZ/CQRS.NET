namespace Persistence.Repositories;


public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
{
    public ApplicationDbContext DbContext;
    internal DbSet<TEntity> dbSet;
    #region IDisposable Support
    private bool _disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing)
        {
        }
        _disposedValue = true;
    }

    ~BaseRepository()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        Dispose(true);
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }


    #endregion
    public BaseRepository(ApplicationDbContext context)
    {
        this.DbContext = context;
        this.dbSet = context.Set<TEntity>();
    }
    public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await dbSet.AddAsync(entity, cancellationToken);
        DbContext.SaveChanges();
        return result.Entity;
    }
    public async Task CreateRange(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(entities, cancellationToken);
        DbContext.SaveChanges();
    }
    public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
    {
        return dbSet.FromSqlRaw(query, parameters).ToList();
    }
    public virtual async Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query, CancellationToken cancellationToken = default, params object[] parameters)
    {
        return await dbSet.FromSqlRaw(query, parameters).ToListAsync(cancellationToken: cancellationToken);
    }
    public virtual bool Update(TEntity entity)
    {
        dbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        return (DbContext.SaveChanges() > 0);
    }
    public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {
            query = include(query);
        }
        if (orderBy != null)
        {
            return await orderBy(query).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
        else
        {
            return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {

            query = include(query);
        }
        return await query.CountAsync(cancellationToken: cancellationToken);
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, int take, int skip)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {

            query = include(query);
        }
        if (orderBy != null)
        {

            if (take == -1 && skip == -1)
            {
                return orderBy(query).ToList();
            }
            else if (take == -1)
            {
                return orderBy(query).Skip(skip).ToList();
            }
            else if (skip == -1)
            {
                return orderBy(query).Take(take).ToList();
            }
            else
            {
                return (query).Skip(skip).Take(take).ToList();
            }
        }
        else
        {
            if (take == -1 && skip == -1)
            {
                return query.ToList();
            }
            else if (take == -1)
            {
                return query.Skip(skip).ToList();
            }
            else if (skip == -1)
            {
                return query.Take(take).ToList();
            }
            else
            {
                return query.Skip(skip).Take(take).ToList();
            }
        }
    }

    public virtual (IEnumerable<TResult>, int) Get<TResult>(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TResult>> selector = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int take = -1, int skip = -1)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {
            query = include(query);
        }
        if (orderBy != null)
        {

            if (take == -1 && skip == -1)
            {
                return (orderBy(query).Select(selector).ToList(), query.Count());
            }
            else if (take == -1)
            {
                return (orderBy(query).Skip(skip).Select(selector).ToList(), query.Count());
            }
            else if (skip == -1)
            {
                return (orderBy(query).Take(take).Select(selector).ToList(), query.Count());
            }
            else
            {
                return ((query).Skip(skip).Take(take).Select(selector).ToList(), query.Count());
            }
        }
        else
        {
            if (take == -1 && skip == -1)
            {
                return (query.Select(selector).ToList(), query.Count());
            }
            else if (take == -1)
            {
                return (query.Skip(skip).Select(selector).ToList(), query.Count());
            }
            else if (skip == -1)
            {
                return (query.Take(take).Select(selector).ToList(), query.Count());
            }
            else
            {
                return (query.Skip(skip).Take(take).Select(selector).ToList(), query.Count());
            }
        }
    }

    public async Task<(IEnumerable<TResult>, int)> GetAsync<TResult>(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TResult>> selector = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int take = -1, int skip = -1, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {
            query = include(query);
        }
        if (orderBy != null)
        {

            if (take == -1 && skip == -1)
            {
                return (await orderBy(query).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else if (take == -1)
            {
                return (await orderBy(query).Skip(skip).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else if (skip == -1)
            {
                return (await orderBy(query).Take(take).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else
            {
                return (await orderBy(query).Skip(skip).Take(take).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
        }
        else
        {
            if (take == -1 && skip == -1)
            {
                return (await query.Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else if (take == -1)
            {
                return (await query.Skip(skip).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else if (skip == -1)
            {
                return (await query.Take(take).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
            else
            {
                return (await query.Skip(skip).Take(take).Select(selector).ToListAsync(cancellationToken: cancellationToken), await query.CountAsync(cancellationToken: cancellationToken));
            }
        }
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {
            query = include(query);
        }
        return query;
    }

    public bool UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        dbSet.UpdateRange(entities);
        return (DbContext.SaveChanges() > 0);
    }

}