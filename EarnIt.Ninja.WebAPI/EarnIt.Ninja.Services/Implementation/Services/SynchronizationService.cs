using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EarnIt.Ninja.Services.Contract.Entities;
using EarnIt.Ninja.Services.Contract.Factories;
using EarnIt.Ninja.Services.Contract.Services;
using EarnIt.Ninja.Services.Domain.Enums;

namespace EarnIt.Ninja.Services.Implementation.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly Dictionary<SynchronizationType, Func<ISynchronizationRequest, IList<IEntity>>> _resolver =
            new Dictionary<SynchronizationType, Func<ISynchronizationRequest, IList<IEntity>>>();

        public SynchronizationService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _resolver.Add(SynchronizationType.Get, Get);
            _resolver.Add(SynchronizationType.Post, Post);
            _resolver.Add(SynchronizationType.Both, Both);
        }


        public IList<IEntity> Synchronize(ISynchronizationRequest syncRequest)
        {
            return _resolver[syncRequest.RequestType](syncRequest);
        }

        private IList<IEntity> Get(ISynchronizationRequest syncRequest)
        {
            var result = new List<IEntity>();
            foreach (var type in syncRequest.Types)
            {
                var localType = type;
                var repository = _repositoryFactory.Resolve(localType);
                result.AddRange(!syncRequest.Restricted
                    ? repository.Get()
                    : repository.Get(
                        syncRequest
                            .Entities
                            .Where(e => e.Type.IsAssignableFrom(localType))
                            .Select(e => (string)e.Entity.Id)
                       )
                 );
            }
            return result;
        }

        private IList<IEntity> Post(ISynchronizationRequest syncRequest)
        {
            foreach (var group in syncRequest.Entities.GroupBy(e => e.Type))
            {
                var repository = _repositoryFactory.Resolve(group.Key);
                repository.SaveAll(group);
            }

            return new List<IEntity>();

        }

        private IList<IEntity> Both(ISynchronizationRequest syncRequest)
        {
            Post(syncRequest);
            return Get(syncRequest);
        }

    }

}