using System.Linq.Expressions;

namespace HxH.App.Models
{
    public interface IRepository<TEntity> : IUnitOfWork
    {
        ValueTask<TEntity?> GetAsync(object id,
            CancellationToken cancellationToken = default,
            bool asNoTracking = false);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter,
            CancellationToken cancellationToken = default,
            bool asNoTracking = false);

        Task<List<TEntity>> Fetch(Expression<Func<TEntity, bool>>? filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
            bool asNoTracking = true,
            string includeProperties = default!,
            CancellationToken cancellationToken = default,
            int skip = 0,
            int take = int.MaxValue);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}