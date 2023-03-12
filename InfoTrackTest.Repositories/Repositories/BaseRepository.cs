using Core;
using InfoTrackTest.Models.Entities;
using InfoTrackTest.Repositories.Context;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace InfoTrackTest.Repositories.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : IEntity
    {
        public readonly InfoTrackTestContext _dbContext;
        private readonly bool _shouldSaveChanges;

        public BaseRepository(InfoTrackTestContext dbContext, bool shouldSaveChanges = false)
        {
            _dbContext = dbContext;
            _shouldSaveChanges = shouldSaveChanges;
        }

        private void BasicValidation<TAnyEntity>(TAnyEntity entity)
        {
            var result = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, vc, result, validateAllProperties: true);

            if (result.Count > 0)
            {
                var firstResult = result.First();
                throw new Core.Models.ValidationException($"{firstResult.MemberNames}: {firstResult.ErrorMessage}");
            }
        }

        public async Task<TEntity> CreateSingleAsync(TEntity entity)
        {
            BasicValidation(entity);
            entity.SetPropertyValue(nameof(IEntity.CreatedDateTime), DateTime.UtcNow);
            await _dbContext.Set<TEntity>().AddAsync(entity);

            if (_shouldSaveChanges)
                await SaveAsync();

            return entity;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

    }
}
