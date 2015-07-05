using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;

namespace EarnIt.Ninja.Services.Contract.Repositories
{
    public interface IRepository<T> : IRepository
    {
         
    }

    public interface IRepository
    {
        IEnumerable<IEntity> Get();
        IEnumerable<IEntity> Get(IEnumerable<string> ids);
        void SaveAll(IEnumerable<IEntity> entities);
    }
}