using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Earnit.Ninja.DataAccess.Models;
using EarnIt.Ninja.Services.Contract.Entities;
using EarnIt.Ninja.Services.Contract.Repositories;
using EarnIt.Ninja.Services.Domain.Models;

namespace Earnit.Ninja.DataAccess.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IGenericRepository _genericRepository;

        public UserRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public IEnumerable<IEntity> Get()
        {
            return _genericRepository.Get<User>().AsEntities();
        }

        public IEnumerable<IEntity> Get(IEnumerable<string> ids)
        {
            return _genericRepository.Get<User>(u => ids.Contains(u.UserId)).AsEntities();
        }

        public void SaveAll(IEnumerable<IEntity> entities)
        {
            _genericRepository.Add(entities.Select(e => e.Entity));
        }
    }



    static class SelfExtensions
    {
        //todo: move to Helper class
        public static IEnumerable<IEntity> AsEntities(this Task<List<User>> entities)
        {
            return entities.Result.Select(u => new MongoEntity
            {
                Type = typeof(User),
                Entity = u
            });
        }
    }
}