using System;
using EarnIt.Ninja.Services.Contract.Repositories;

namespace EarnIt.Ninja.Services.Contract.Factories
{
    public interface IRepositoryFactory
    {
        IRepository Resolve(Type type);
    }
}