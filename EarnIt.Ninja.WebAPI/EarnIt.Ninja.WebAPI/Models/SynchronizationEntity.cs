using System;
using EarnIt.Ninja.Services.Contract.Entities;

namespace EarnIt.Ninja.WebAPI.Models
{
    public class SynchronizationEntity:IEntity
    {
        public Type Type { get; set; } 
        public dynamic Entity { get; set; } 
    }
}