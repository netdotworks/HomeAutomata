using HomeAutomata.Core.Domain;
using HomeAutomata.Data.Repositories;
using System.Collections.Generic;

namespace HomeAutomata.Data.Services
{
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : BaseEntity, new()
    {
        public IRepo<TEntity> Repository { get; }

        public CrudService(IRepo<TEntity> repo)
        {
            Repository = repo;
        }

        public int Add(TEntity entity)
        {
            return Repository.Add(entity);
        }

        public int Delete(TEntity entity)
        {
            return Repository.Delete(entity);
        }

        public TEntity Get(int id)
        {
            return Repository.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public int Update(TEntity entity)
        {
            return Repository.Update(entity);
        }
    }
}