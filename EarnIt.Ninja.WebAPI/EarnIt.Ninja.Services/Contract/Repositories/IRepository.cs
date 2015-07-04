using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;

namespace EarnIt.Ninja.Services.Contract.Repositories
{
    public interface IRepository<T> : IRepository
    {
         
    }

    public interface IRepository
    {
        List<IEntity> Get();
        List<IEntity> Get(IEnumerable<int> ids);
        void SaveAll(IEnumerable<IEntity> entities);
    }
}