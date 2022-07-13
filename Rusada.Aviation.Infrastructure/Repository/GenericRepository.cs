using Microsoft.EntityFrameworkCore;
using Rusada.Aviation.Core.Interface;
using Rusada.Aviation.Infrastructure.Persistence;

namespace Rusada.Aviation.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private RusadaDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(RusadaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return _dbSet.AsQueryable<T>();
        }

        public async Task Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
