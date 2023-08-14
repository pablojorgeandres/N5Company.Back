using N5Company.Entities;

namespace N5Company.Repositories
{
    public interface IRepository
    {
        Task<T?> GetById<T>(int id) where T : IEntity;

        T Add<T>(T entity) where T : IEntity;
        Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken = default) where T : class;
        void Update<T>(T entity) where T : IEntity;
    }
}
