using Microsoft.EntityFrameworkCore;
using N5Company.Entities;
using N5Company.Persistence;

namespace N5Company.Repositories
{
    public class Repository : IRepository
    {
        private readonly IDataContext _context;

        public Repository(IDataContext context)
        {
            _context = context;
        }

        public Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken = default)
          where T : class
        {
            return _context.Set<T>().ToListAsync(cancellationToken);
        }

        public T Add<T>(T entity) where T : IEntity
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Update<T>(T entity) where T : IEntity
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T?> GetById<T>(int id) where T : IEntity
        {
            return await _context.Set<T>().FindAsync(id);
        }

    }
}
