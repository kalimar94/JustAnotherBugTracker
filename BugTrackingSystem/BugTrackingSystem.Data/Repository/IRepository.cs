using System.Collections.Generic;

namespace BugTrackingSystem.Data.Repositories
{
    public interface IRepository<TEntity> : IEnumerable<TEntity>
    {
        IEnumerable<TEntity> Including(params string[] includeItems);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        void SaveChanges();
    }
}
