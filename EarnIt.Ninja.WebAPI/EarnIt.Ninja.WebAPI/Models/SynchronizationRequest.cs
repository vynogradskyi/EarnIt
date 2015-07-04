using System;
using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;
using EarnIt.Ninja.Services.Domain.Enums;

namespace EarnIt.Ninja.WebAPI.Models
{
    public class SynchronizationRequest : ISynchronizationRequest
    {
        public SynchronizationType RequestType { get; set; }
        public string UserId { get; set; }
        public bool Restricted { get; set; }
        public Type[] Types { get; set; }
        public List<IEntity> Entities { get; set; }
    }
}