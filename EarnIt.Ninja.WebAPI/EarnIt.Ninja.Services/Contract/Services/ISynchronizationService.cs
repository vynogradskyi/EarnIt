using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;

namespace EarnIt.Ninja.Services.Contract.Services
{
    public interface ISynchronizationService
    {
        IList<IEntity> Synchronize(ISynchronizationRequest syncRequest);
    }
}