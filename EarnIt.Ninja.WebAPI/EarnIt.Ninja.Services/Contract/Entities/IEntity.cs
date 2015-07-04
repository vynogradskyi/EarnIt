using System;

namespace EarnIt.Ninja.Services.Contract.Entities
{
    public interface IEntity
    {
        Type Type { get; set; }
        dynamic Entity { get; set; }
    }
}