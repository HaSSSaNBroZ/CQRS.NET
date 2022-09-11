using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Contracts;
public interface IBaseRepository<TEntity> : IDisposable
{


    Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
    Task CreateRange(List<TEntity> entities, CancellationToken cancellationToken = default);
    IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
    Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query, CancellationToken cancellationToken = default, params object[] parameters);
    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int take = -1,
        int skip = -1);
    (IEnumerable<TResult>, int) Get<TResult>(
    Expression<Func<TEntity, bool>> filter = null,
    Expression<Func<TEntity, TResult>> selector = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
    int take = -1, int skip = -1);


    Task<(IEnumerable<TResult>, int)> GetAsync<TResult>(
    Expression<Func<TEntity, bool>> filter = null,
    Expression<Func<TEntity, TResult>> selector = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
    int take = -1, int skip = -1, CancellationToken cancellationToken = default);

    IQueryable<TEntity> Query(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

    bool Update(TEntity entity);
    bool UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task<TEntity> Find(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, CancellationToken cancellationToken = default);
}