using InfoTrackTest.Models.Entities;

namespace InfoTrackTest.Repositories.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> CreateSingleAsync(TEntity entity);
        Task<int> SaveAsync();
        int Save();
    }
}
