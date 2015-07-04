using System;
using System.Collections.Generic;
using EarnIt.Ninja.Services.Contract.Entities;

namespace EarnIt.Ninja.WebAPI.Models
{
    public class SynchronizationResponse
    {
        public Type[] Types { get; set; }
        public IList<IEntity> Entities { get; set; }
        public EarnItStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}