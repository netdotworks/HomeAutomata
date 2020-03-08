using HomeAutomata.Core.Domain;
using System.Collections.Generic;

namespace HomeAutomata.Data.Services
{
    public interface ICrudService<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        int Add(TEntity entity);

        int Update(TEntity entity);

        int Delete(TEntity entity);
    }
}