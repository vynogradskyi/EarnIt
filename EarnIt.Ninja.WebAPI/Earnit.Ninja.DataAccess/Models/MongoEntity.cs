using System;
using EarnIt.Ninja.Services.Contract.Entities;

namespace Earnit.Ninja.DataAccess.Models
{
    public class MongoEntity:IEntity
    {
        public Type Type { get; set; }

        public dynamic Entity { get; set; }
    }
}