using System;
using System.Collections.Generic;
using EarnIt.Ninja.Services.Domain.Enums;

namespace EarnIt.Ninja.Services.Contract.Entities
{
    public interface ISynchronizationRequest
    {
        SynchronizationType RequestType { get; set; }
        string UserId { get; }
        bool Restricted { get; }
        Type[] Types { get; }
        List<IEntity> Entities { get; }
    }
}