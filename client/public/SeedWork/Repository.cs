using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HxH.App.Models
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual void Create(TEntity entityToCreate)
        {
            _dbSet.Add(entityToCreate);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual Task<List<TEntity>> Fetch(Expression<Func<TEntity, bool>>? filter = default,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default,
            bool asNoTracking = true,
            string includeProperties = default!,
            CancellationToken cancellationToken = default,
            int skip = 0,
            int take = int.MaxValue)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                ([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            query = query.Skip(skip)
                .Take(take);

            return _dbSet.ToListAsync(cancellationToken);
        }

        public virtual ValueTask<TEntity?> GetAsync(object id,
            CancellationToken cancellationToken = default,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return _dbSet.FindAsync(id, cancellationToken);
        }

        public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter,
            CancellationToken cancellationToken = default,
            bool asNoTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return _dbSet.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}