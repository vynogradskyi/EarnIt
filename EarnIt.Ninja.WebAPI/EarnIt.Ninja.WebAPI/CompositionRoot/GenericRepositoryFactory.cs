using System;
using EarnIt.Ninja.Services.Contract.Factories;
using EarnIt.Ninja.Services.Contract.Repositories;
using Ninject;

namespace EarnIt.Ninja.WebAPI.CompositionRoot
{
    public class GenericRepositoryFactory : IRepositoryFactory
    {
        private readonly IKernel _kernel;

        public GenericRepositoryFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IRepository Resolve(Type type)
        {
            var handlerType = typeof (IRepository<>).MakeGenericType(type);

            var handler = (IRepository)_kernel.Get(handlerType);
            return handler;
        }
    }
}